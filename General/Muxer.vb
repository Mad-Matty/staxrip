Imports System.Text
Imports System.Runtime.Serialization
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports System.Globalization

Imports vb6 = Microsoft.VisualBasic

Imports StaxRip.UI

<Serializable()>
Public MustInherit Class Muxer
    Inherits Profile

    Property ChapterFile As String

    MustOverride Sub Mux()

    MustOverride ReadOnly Property OutputType As String

    Overridable ReadOnly Property SupportedInputTypes As String()
        Get
            Return New String() {}
        End Get
    End Property

    Sub New(name As String)
        MyBase.New(name)
        CanEditValue = True
    End Sub

    Private AdditionalSwitchesValue As String

    Property AdditionalSwitches() As String
        Get
            Return AdditionalSwitchesValue
        End Get
        Set(Value As String)
            If Value = "" Then
                AdditionalSwitchesValue = Nothing
            Else
                AdditionalSwitchesValue = Value
            End If
        End Set
    End Property

    Private SubtitlesValue As List(Of Subtitle)

    Property Subtitles() As List(Of Subtitle)
        Get
            If SubtitlesValue Is Nothing Then
                SubtitlesValue = New List(Of Subtitle)
            End If

            Return SubtitlesValue
        End Get
        Set(value As List(Of Subtitle))
            SubtitlesValue = value
        End Set
    End Property

    Overrides Sub Clean()
        Subtitles = Nothing
        ChapterFile = Nothing
    End Sub

    Overridable Function GetError() As String
        Return Nothing
    End Function

    Overridable Function GetCommandLine() As String
        Return Nothing
    End Function

    Overridable Function GetExtension() As String
        Return "." + OutputType.ToString.ToLower
    End Function

    Overridable Sub Init()
        If Not File.Exists(p.SourceFile) Then Exit Sub

        Dim files = g.GetFilesInTempDirAndParent
        Dim pattern = "^.+\.(\d\d)\.(\w\w\w)\.(.+)\..+$"

        Dim so As New Sorter(Of String)

        For Each i In files
            Dim m = Regex.Match(i, pattern)

            If m.Success Then
                so.Add(m.Groups(1).Value, i)
            Else
                so.Add(i, i)
            End If
        Next

        files = so.GetSortedList()

        For Each iFile In files
            If Filepath.GetExtFull(iFile) = ".idx" Then
                Dim v = File.ReadAllText(iFile, Encoding.Default)

                If v.Contains(vb6.ChrW(&HA) + vb6.ChrW(&H0) + vb6.ChrW(&HD) + vb6.ChrW(&HA)) Then
                    v = v.FixBreak
                    v = v.Replace(CrLf + vb6.ChrW(&H0) + CrLf, CrLf + "langidx: 0" + CrLf)
                    File.WriteAllText(iFile, v, Encoding.Default)
                End If
            End If

            If FileTypes.SubtitleExludingContainers.Contains(Filepath.GetExt(iFile)) AndAlso
                g.IsSourceSameOrSimilar(iFile) AndAlso Not iFile.Contains("_Preview.") AndAlso
                Not iFile.Contains("_Temp.") Then

                If p.ConvertSup2Sub AndAlso Filepath.GetExtFull(iFile) = ".sup" Then
                    Continue For
                End If

                If TypeOf Me Is MP4Muxer AndAlso Not {"idx", "srt"}.Contains(Filepath.GetExt(iFile)) Then
                    Continue For
                End If

                If iFile.Contains("_Forced.") AndAlso Not Filepath.GetBase(iFile).Contains(Language.CurrentCulture.Name) Then
                    Continue For
                End If

                For Each iSubtitle In Subtitle.Create(iFile)
                    If p.AutoSubtitles <> "" Then
                        iSubtitle.Enabled = False

                        For Each i3 In p.AutoSubtitles.SplitNoEmptyAndWhiteSpace(",", ";", " ")
                            If i3.ToLower = "all" OrElse i3.ToLower = iSubtitle.Language.TwoLetterCode.ToLower Then
                                iSubtitle.Enabled = True
                            End If
                        Next

                        Dim m = Regex.Match(iFile, pattern)

                        If m.Success Then
                            For Each i4 In Language.Languages
                                If i4.ThreeLetterCode = m.Groups(2).Value Then
                                    iSubtitle.Language = i4
                                End If
                            Next

                            iSubtitle.Title = m.Groups(3).Value
                        End If

                        If iFile.Contains("_Forced.") Then
                            Static forcedAdded As Boolean

                            If forcedAdded Then
                                Continue For
                            Else
                                iSubtitle.Forced = True
                                forcedAdded = True
                            End If
                        End If

                        Subtitles.Add(iSubtitle)
                    End If
                Next
            End If
        Next

        If p.AutoSubtitles <> "" AndAlso Subtitles.Count = 0 AndAlso
            {"mkv", "mp4"}.Contains(p.SourceFile.Ext) AndAlso
            MediaInfo.GetSubtitleCount(p.SourceFile) > 0 AndAlso
            TypeOf Me Is MkvMuxer Then

            For Each i In Subtitle.Create(p.SourceFile)
                For Each i2 In p.AutoSubtitles.SplitNoEmptyAndWhiteSpace(",", ";", " ")
                    If i2.ToLower = "all" OrElse i2.ToLower = i.Language.TwoLetterCode.ToLower Then
                        Subtitles.Add(i)
                    End If
                Next
            Next
        End If

        For Each i In files
            If g.IsSourceSameOrSimilar(i) Then
                If Not TypeOf Me Is WebMMuxer Then
                    If i.ToLower Like "*chapter*txt" Then
                        ChapterFile = i
                    End If

                    If i.ToLower.EndsWith(".xml") AndAlso
                        File.ReadAllText(i).Contains("<Chapters>") Then

                        ChapterFile = i
                    End If
                End If

                If TypeOf Me Is MkvMuxer AndAlso i.Contains("_attachment_") Then
                    AdditionalSwitches += " --attachment-name """ + i.Right("_attachment_") + """ --attach-file """ + i + """"
                End If
            End If
        Next

        If AdditionalSwitches <> "" AndAlso AdditionalSwitches.StartsWith(" ") Then
            AdditionalSwitches = AdditionalSwitches.TrimStart
        End If
    End Sub

    Overridable Function IsSupported(fileType As String) As Boolean
        Return SupportedInputTypes.Contains(fileType)
    End Function

    Shared Function GetDefaults() As List(Of Muxer)
        Dim ret As New List(Of Muxer)

        ret.Add(New MkvMuxer())
        ret.Add(New MP4Muxer("MP4"))
        ret.Add(New ffmpegMuxer("AVI"))
        ret.Add(New WebMMuxer())
        ret.Add(New DivXPluxMuxer())
        ret.Add(New BatchMuxer("Command Line"))
        ret.Add(New NullMuxer("No Muxing"))

        Return ret
    End Function
End Class

<Serializable()>
Public MustInherit Class EncoderMuxerBase
    Inherits Muxer

    MustOverride ReadOnly Property Encoder() As Type

    Sub New(name As String)
        MyBase.New(name)
    End Sub

    Overrides Function GetExtension() As String
        Return "." + p.VideoEncoder.OutputFileType
    End Function

    Overrides Sub Mux()
    End Sub
End Class

<Serializable()>
Public Class MP4Muxer
    Inherits Muxer

    Sub New(name As String)
        MyBase.New(name)
    End Sub

    Overrides ReadOnly Property OutputType As String
        Get
            Return "mp4"
        End Get
    End Property

    Overrides Function Edit() As DialogResult
        Using f As New MuxerForm(Me)
            Return f.ShowDialog()
        End Using
    End Function

    Overrides Function GetCommandLine() As String
        Return """" + Packs.MP4Box.GetPath + """ " + GetArgs()
    End Function

    Private Function GetArgs() As String
        Dim args As New StringBuilder

        args.Append(" -fps " + p.VideoEncoder.GetFrameRate.ToString("f6", CultureInfo.InvariantCulture))

        Dim temp As String = Nothing
        Dim par = Calc.GetTargetPAR

        If TypeOf p.VideoEncoder Is NullEncoder Then
            temp = ":par=" & par.X & ":" & par.Y
        End If

        args.Append(" -add """ + p.VideoEncoder.OutputPath + "#video" + temp + """")

        If File.Exists(p.Audio0.File) AndAlso IsSupported(p.Audio0.OutputFileType) Then
            args.Append(" -add """ + p.Audio0.File)

            If p.Audio0.HasStream AndAlso Filepath.GetExtFull(p.Audio0.File) = ".mp4" Then
                args.Append("#trackID=" & p.Audio0.Stream.ID)
            Else
                args.Append("#audio")
            End If

            args.Append(":lang=" + p.Audio0.Language.ThreeLetterCode)

            If p.Audio0.Delay <> 0 AndAlso Not p.Audio0.HandlesDelay Then
                args.Append(":delay=" + p.Audio0.Delay.ToString)
            End If

            args.Append(":name=" + p.Audio0.SolveMacros(p.Audio0.StreamName))
            args.Append("""")
        End If

        If File.Exists(p.Audio1.File) AndAlso IsSupported(p.Audio1.OutputFileType) AndAlso p.Audio1.File <> "" Then
            args.Append(" -add """ + p.Audio1.File)

            If p.Audio1.HasStream AndAlso Filepath.GetExtFull(p.Audio1.File) = ".mp4" Then
                args.Append("#trackID=" & p.Audio1.Stream.ID)
            Else
                args.Append("#audio")
            End If

            args.Append(":lang=" + p.Audio1.Language.ThreeLetterCode)

            If p.Audio1.Delay <> 0 AndAlso Not p.Audio1.HandlesDelay Then
                args.Append(":delay=" + p.Audio1.Delay.ToString)
            End If

            args.Append(":name=" + p.Audio1.SolveMacros(p.Audio1.StreamName))
            args.Append("""")
        End If

        For Each i In Subtitles
            If i.Enabled AndAlso File.Exists(i.Path) Then
                If Filepath.GetExtFull(i.Path) = ".idx" Then
                    If i.Title = "" Then i.Title = " "
                    args.Append(" -add """ + i.Path + "#" & i.IndexIDX + 1 & ":name=" + Macro.Solve(i.Title, True) & """")
                Else
                    If i.Title = "" Then i.Title = " "
                    args.Append(" -add """ + i.Path + ":lang=" + i.Language.ThreeLetterCode + ":name=" + Macro.Solve(i.Title, True) + """")
                End If
            End If
        Next

        If File.Exists(ChapterFile) Then args.Append(" -chap """ + ChapterFile + """")
        If AdditionalSwitches <> "" Then args.Append(" " + Macro.Solve(AdditionalSwitches))

        args.Append(" -new """ + p.TargetFile + """")

        Return args.ToString.Trim
    End Function

    Overrides Sub Mux()
        Using proc As New Proc
            proc.Init("Muxing using MP4Box", {"|"})
            proc.File = Packs.MP4Box.GetPath
            proc.Arguments = GetArgs()
            proc.Process.StartInfo.EnvironmentVariables("TEMP") = p.TempDir
            proc.Start()
        End Using

        If Not g.WasFileJustWritten(p.TargetFile) Then
            Throw New ErrorAbortException("Error MP4 output file is missing.", GetArgs())
        End If

        Log.WriteLine(MediaInfo.GetSummary(p.TargetFile))
    End Sub

    Overrides Sub Clean()
        Subtitles = Nothing
    End Sub

    Overrides ReadOnly Property SupportedInputTypes() As String()
        Get
            Return {"ts", "m2ts",
                    "mpg", "m2v",
                    "avi", "ac3",
                    "mp4", "m4a", "aac",
                    "264", "h264", "avc",
                    "265", "h265", "hevc",
                    "mp2", "mpa", "mp3"}
        End Get
    End Property
End Class

<Serializable()>
Public Class NullMuxer
    Inherits Muxer

    Sub New(name As String)
        MyBase.New(name)
        CanEditValue = False
    End Sub

    Overrides Function IsSupported(type As String) As Boolean
        Return True
    End Function

    Overrides ReadOnly Property OutputType As String
        Get
            Return "N/A"
        End Get
    End Property

    Overrides Function GetExtension() As String
        Return "." + p.VideoEncoder.OutputFileType
    End Function

    Overrides Sub Mux()
    End Sub

    Public Overrides Sub Init()
    End Sub
End Class

<Serializable()>
Public Class BatchMuxer
    Inherits Muxer

    Property OutputTypeValue As String = "mp4"
    Property CommandLines As String = """%app:MP4Box%"" -nodrop -add ""%encoder_out_file%#video"" -add ""%audio_file1%"" -new ""%target_file%"""

    Sub New(name As String)
        MyBase.New(name)
    End Sub

    Public Overrides ReadOnly Property OutputType As String
        Get
            Return OutputTypeValue
        End Get
    End Property

    Overrides Function IsSupported(type As String) As Boolean
        Return True
    End Function

    Overrides Sub Mux()
        Log.WriteHeader("Batch Muxing")

        Dim commands = Macro.Solve(CommandLines)
        Dim batchPath = p.TempDir + Filepath.GetBase(p.TargetFile) + "_mux.bat"
        File.WriteAllText(batchPath, commands, Encoding.GetEncoding(850))

        Using proc As New Proc
            proc.Init("Encoding video command line encoder: " + Name)
            proc.WriteLine(commands + CrLf2)
            proc.File = "cmd.exe"
            proc.Arguments = "/C call """ + batchPath + """"
            proc.BatchCode = commands

            Try
                proc.Start()
            Catch ex As AbortException
                Throw ex
            Catch ex As Exception
                ProcessForm.CloseProcessForm()
                g.ShowException(ex)
                Throw New AbortException
            End Try
        End Using
    End Sub

    Overrides Function Edit() As DialogResult
        Using f As New SimpleSettingsForm("Batch Muxer", "The Batch Muxer dialog allows to configure StaxRip to use a command line or batch code as muxer.")
            f.Size = New Size(1100, 600)

            Dim ui = f.SimpleUI
            Dim page = ui.CreateFlowPage("main page")

            Dim tb = ui.AddTextBlock(page)
            tb.Label.Offset = 7
            tb.Label.Text = "Output File Type:"
            tb.Edit.Text = OutputTypeValue
            tb.Edit.SaveAction = Sub(value) OutputTypeValue = value

            Dim l = ui.AddLabel(page, "Batch Script:")
            l.MarginTop = f.Font.Height
            l.Tooltip = "Batch script which may contain macros."

            tb = ui.AddTextBlock(page)
            tb.Label.Visible = False
            tb.Expand(tb.Edit)
            tb.Edit.Height = f.Font.Height * 15
            tb.Edit.TextBox.Multiline = True
            tb.Edit.Text = CommandLines
            tb.Edit.UseCommandlineEditor = True
            tb.Edit.SaveAction = Sub(value) CommandLines = value

            Dim ret = f.ShowDialog()
            If ret = DialogResult.OK Then ui.Save()

            Return ret
        End Using
    End Function
End Class

<Serializable()>
Public Class MkvMuxer
    Inherits Muxer

    Property VideoTrackName As String = ""
    Property VideoTrackLanguage As New Language(CultureInfo.InvariantCulture)
    Property Title As String = ""

    Sub New(Optional name As String = "MKV")
        MyBase.New(name)
    End Sub

    Overrides ReadOnly Property OutputType As String
        Get
            Return "mkv"
        End Get
    End Property

    Overrides Function Edit() As DialogResult
        Using f As New MuxerForm(Me)
            Return f.ShowDialog()
        End Using
    End Function

    Overrides Sub Mux()
        Using proc As New Proc
            proc.Init("Muxing using mkvmerge", "Progress: ")
            proc.Encoding = Encoding.UTF8
            proc.File = Packs.Mkvmerge.GetPath
            proc.Arguments = GetArgs()
            proc.AllowedExitCodes = {0, 1}
            proc.Start()
        End Using

        If Not g.WasFileJustWritten(p.TargetFile) Then
            Log.Write("Error MKV output file is missing", p.TargetFile)
        End If

        Log.WriteLine(MediaInfo.GetSummary(p.TargetFile))
    End Sub

    Overrides Function GetCommandLine() As String
        Return """" + Packs.Mkvmerge.GetPath + """ " + GetArgs()
    End Function

    Private Function GetArgs() As String
        Dim args As New StringBuilder("-o """ + p.TargetFile + """")

        Dim vID = -1 '-1 means all

        args.Append(" --noaudio --nosubs --no-chapters --no-attachments --no-track-tags --no-global-tags")

        If VideoTrackLanguage.ThreeLetterCode <> "und" Then
            args.Append(" --language " & vID & ":" + VideoTrackLanguage.ThreeLetterCode)
        End If

        If VideoTrackName <> "" Then
            args.Append(" --track-name """ & vID & ":" + Macro.Solve(VideoTrackName) + """")
        End If

        If FileTypes.VideoRaw.Contains(p.VideoEncoder.OutputFileType) Then
            args.Append(" --default-duration 0:" + p.VideoEncoder.GetFrameRate.ToString("f6", CultureInfo.InvariantCulture) + "fps")
        End If

        args.Append(" """ + p.VideoEncoder.OutputPath + """")

        AddAudioArgs(p.Audio0, args)
        AddAudioArgs(p.Audio1, args)

        For Each i In Subtitles
            If i.Enabled AndAlso File.Exists(i.Path) Then
                Dim id = i.StreamOrder

                If {"mkv", "mp4", "idx"}.Contains(Filepath.GetExt(i.Path)) Then
                    args.Append(" --no-audio --no-video --no-chapters --no-attachments --no-track-tags --no-global-tags")
                    args.Append(" --subtitle-tracks " & id)
                Else
                    id = 0
                End If

                args.Append(" --forced-track " & id & ":" & If(i.Forced, 1, 0))
                args.Append(" --default-track " & id & ":" & If(i.Default, 1, 0))
                args.Append(" --language " & id & ":" + i.Language.ThreeLetterCode)

                If i.Title <> "" AndAlso i.Title <> " " Then
                    args.Append(" --track-name """ & id & ":" + i.Title + """")
                End If

                args.Append(" """ + i.Path + """")
            End If
        Next

        If Not TypeOf Me Is WebMMuxer AndAlso File.Exists(ChapterFile) Then
            Dim chapterFileContent = File.ReadAllText(ChapterFile)

            If chapterFileContent.Contains("</EBMLVoid>") Then
                Stop
                chapterFileContent = Regex.Replace(chapterFileContent, "<EBMLVoid.+?EBMLVoid>", "<!-- $0 -->")
                File.WriteAllText(ChapterFile, chapterFileContent)
            End If

            args.Append(" --chapters """ + ChapterFile + """")
        End If

        If Title <> "" Then
            args.Append(" --title """ + Macro.Solve(Title) + """")
        End If

        args.Append(" --ui-language en")

        If AdditionalSwitches <> "" Then
            args.Append(" " + Macro.Solve(AdditionalSwitches))
        End If

        Return args.ToString
    End Function

    Sub AddAudioArgs(ap As AudioProfile, args As StringBuilder)
        If File.Exists(ap.File) AndAlso IsSupported(ap.OutputFileType) Then
            Dim tid = 0
            Dim isCombo As Boolean

            If Not ap.Stream Is Nothing Then
                tid = ap.Stream.StreamOrder
                isCombo = ap.Stream.Name.Contains("THD+AC3")
            Else
                tid = MediaInfo.GetAudio(ap.File, "StreamOrder").ToInt
                isCombo = ap.File.Ext = "thd+ac3"
            End If

            args.Append(" --novideo --nosubs --no-chapters --no-attachments --no-track-tags --no-global-tags --audio-tracks " + If(isCombo, tid & "," & tid + 1, tid.ToString))

            args.Append(" --language " & tid & ":" + ap.Language.ThreeLetterCode)
            If isCombo Then args.Append(" --language " & tid + 1 & ":" + ap.Language.ThreeLetterCode)

            If ap.OutputFileType = "aac" AndAlso ap.File.ToLower.Contains("sbr") Then
                args.Append(" --aac-is-sbr " & tid)
            End If

            If ap.StreamName <> "" Then
                args.Append(" --track-name """ & tid & ":" + ap.SolveMacros(ap.StreamName, False) + """")
                If isCombo Then args.Append(" --track-name """ & tid + 1 & ":" + ap.SolveMacros(ap.StreamName, False) + """")
            End If

            If ap.Delay <> 0 AndAlso Not ap.HandlesDelay AndAlso Not (ap.HasStream AndAlso ap.Stream.Delay <> 0) Then
                args.Append(" --sync " & tid & ":" + ap.Delay.ToString)
                If isCombo Then args.Append(" --sync " & tid + 1 & ":" + ap.Delay.ToString)
            End If

            args.Append(" """ + ap.File + """")
        End If
    End Sub

    Overrides ReadOnly Property SupportedInputTypes() As String()
        Get
            Return FileTypes.mkvmergeInput
        End Get
    End Property
End Class

<Serializable()>
Public Class DivXPluxMuxer
    Inherits MkvMuxer

    Sub New()
        MyBase.New("MKV for DivX Plus")
    End Sub

    Overrides ReadOnly Property SupportedInputTypes() As String()
        Get
            Return {"h264", "mkv", "m4a", "mp4", "aac", "ac3"}
        End Get
    End Property
End Class

<Serializable()>
Public Class WebMMuxer
    Inherits MkvMuxer

    Sub New()
        MyBase.New("WebM")
    End Sub

    Overrides ReadOnly Property SupportedInputTypes() As String()
        Get
            Return {"mkv", "webm", "mka", "ogg", "opus"}
        End Get
    End Property

    Overrides ReadOnly Property OutputType As String
        Get
            Return "webm"
        End Get
    End Property
End Class

<Serializable()>
Public Class ffmpegMuxer
    Inherits Muxer

    Property OutputTypeValue As String = "avi"

    Sub New(name As String)
        MyBase.New(name)
    End Sub

    ReadOnly Property AVITypes As String()
        Get
            Return {"avi", "mp2", "mp3", "ac3", "mpa", "wav"}
        End Get
    End Property

    Public Overrides ReadOnly Property OutputType As String
        Get
            Return OutputTypeValue
        End Get
    End Property

    Public Overrides ReadOnly Property SupportedInputTypes As String()
        Get
            If OutputType = "avi" Then Return AVITypes
            Return MyBase.SupportedInputTypes
        End Get
    End Property

    Overrides Function IsSupported(type As String) As Boolean
        If OutputType = "avi" Then Return AVITypes.Contains(type)
        Return True
    End Function

    Overrides Sub Mux()
        Dim args = "-i """ + p.VideoEncoder.OutputPath + """"

        If File.Exists(p.Audio0.File) Then
            args += " -i """ + p.Audio0.File + """"
        End If

        If File.Exists(p.Audio1.File) Then
            args += " -i """ + p.Audio1.File + """"
        End If

        args += " -c:v copy -c:a copy -y"

        args += " """ + p.TargetFile + """"

        Using proc As New Proc
            proc.Init("Muxing to " + OutputTypeValue + " using ffmpeg", "frame=")
            proc.Encoding = Encoding.UTF8
            proc.File = Packs.ffmpeg.GetPath
            proc.Arguments = args
            proc.Start()
        End Using
    End Sub

    Overrides Function Edit() As DialogResult
        Using f As New SimpleSettingsForm(Name)
            f.Size = New Size(500, 200)

            Dim ui = f.SimpleUI

            Dim page = ui.CreateFlowPage("main page")

            Dim tb = ui.AddTextBlock(page)
            tb.Label.Text = "Output File Type:"
            tb.Edit.Text = OutputTypeValue
            tb.Edit.SaveAction = Sub(value) OutputTypeValue = value

            Dim ret = f.ShowDialog()

            If ret = DialogResult.OK Then
                ui.Save()
            End If

            Return ret
        End Using
    End Function
End Class