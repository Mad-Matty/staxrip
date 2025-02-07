Imports System.ComponentModel

Imports StaxRip.UI

Class PreviewForm
    Inherits DialogBase

#Region " Designer "
    Protected Overloads Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    Friend WithEvents pVideo As System.Windows.Forms.Panel
    Friend WithEvents pTrack As System.Windows.Forms.Panel
    Private WithEvents bDeleteRange As System.Windows.Forms.Button
    Private WithEvents bForward1 As System.Windows.Forms.Button
    Private WithEvents bRangeStart As System.Windows.Forms.Button
    Private WithEvents bRangeEnd As System.Windows.Forms.Button
    Private WithEvents bForward10 As System.Windows.Forms.Button
    Private WithEvents bBackward10 As System.Windows.Forms.Button
    Private WithEvents bExtras As ButtonEx
    Private WithEvents bForward100 As System.Windows.Forms.Button
    Private WithEvents bBackward100 As System.Windows.Forms.Button
    Private WithEvents bBackward1 As System.Windows.Forms.Button
    Friend WithEvents cmsMain As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip

    '<System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PreviewForm))
        Me.bDeleteRange = New System.Windows.Forms.Button()
        Me.bForward1 = New System.Windows.Forms.Button()
        Me.bBackward1 = New System.Windows.Forms.Button()
        Me.bRangeStart = New System.Windows.Forms.Button()
        Me.bRangeEnd = New System.Windows.Forms.Button()
        Me.bForward10 = New System.Windows.Forms.Button()
        Me.bBackward10 = New System.Windows.Forms.Button()
        Me.bExtras = New StaxRip.UI.ButtonEx()
        Me.bForward100 = New System.Windows.Forms.Button()
        Me.bBackward100 = New System.Windows.Forms.Button()
        Me.pVideo = New System.Windows.Forms.Panel()
        Me.cmsMain = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.pTrack = New System.Windows.Forms.Panel()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.pVideo.SuspendLayout()
        Me.SuspendLayout()
        '
        'bDeleteRange
        '
        Me.bDeleteRange.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.bDeleteRange.BackColor = System.Drawing.Color.White
        Me.bDeleteRange.BackgroundImage = Global.StaxRip.My.Resources.Resources.X
        Me.bDeleteRange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.bDeleteRange.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bDeleteRange.Location = New System.Drawing.Point(360, 274)
        Me.bDeleteRange.Name = "bDeleteRange"
        Me.bDeleteRange.Size = New System.Drawing.Size(32, 32)
        Me.bDeleteRange.TabIndex = 47
        Me.bDeleteRange.TabStop = False
        Me.ToolTip.SetToolTip(Me.bDeleteRange, "Deletes the cut selection that encloses the current position.")
        Me.bDeleteRange.UseVisualStyleBackColor = False
        '
        'bForward1
        '
        Me.bForward1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.bForward1.BackColor = System.Drawing.Color.White
        Me.bForward1.BackgroundImage = Global.StaxRip.My.Resources.Resources.Right1
        Me.bForward1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.bForward1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bForward1.Location = New System.Drawing.Point(240, 274)
        Me.bForward1.Name = "bForward1"
        Me.bForward1.Size = New System.Drawing.Size(32, 32)
        Me.bForward1.TabIndex = 46
        Me.bForward1.TabStop = False
        Me.ToolTip.SetToolTip(Me.bForward1, "Forward 1 Frames")
        Me.bForward1.UseVisualStyleBackColor = False
        '
        'bBackward1
        '
        Me.bBackward1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.bBackward1.BackColor = System.Drawing.Color.White
        Me.bBackward1.BackgroundImage = Global.StaxRip.My.Resources.Resources.Left1
        Me.bBackward1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.bBackward1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bBackward1.Location = New System.Drawing.Point(120, 274)
        Me.bBackward1.Name = "bBackward1"
        Me.bBackward1.Size = New System.Drawing.Size(32, 32)
        Me.bBackward1.TabIndex = 45
        Me.bBackward1.TabStop = False
        Me.ToolTip.SetToolTip(Me.bBackward1, "Backward 1 Frame")
        Me.bBackward1.UseVisualStyleBackColor = False
        '
        'bRangeStart
        '
        Me.bRangeStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.bRangeStart.BackColor = System.Drawing.Color.White
        Me.bRangeStart.BackgroundImage = Global.StaxRip.My.Resources.Resources.BracketOpen
        Me.bRangeStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.bRangeStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bRangeStart.Location = New System.Drawing.Point(160, 274)
        Me.bRangeStart.Name = "bRangeStart"
        Me.bRangeStart.Size = New System.Drawing.Size(32, 32)
        Me.bRangeStart.TabIndex = 44
        Me.bRangeStart.TabStop = False
        Me.ToolTip.SetToolTip(Me.bRangeStart, "Sets a start cut point. Press F1 for help about cutting")
        Me.bRangeStart.UseVisualStyleBackColor = False
        '
        'bRangeEnd
        '
        Me.bRangeEnd.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.bRangeEnd.BackColor = System.Drawing.Color.White
        Me.bRangeEnd.BackgroundImage = Global.StaxRip.My.Resources.Resources.BracketClose
        Me.bRangeEnd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.bRangeEnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bRangeEnd.Location = New System.Drawing.Point(200, 274)
        Me.bRangeEnd.Name = "bRangeEnd"
        Me.bRangeEnd.Size = New System.Drawing.Size(32, 32)
        Me.bRangeEnd.TabIndex = 43
        Me.bRangeEnd.TabStop = False
        Me.ToolTip.SetToolTip(Me.bRangeEnd, "Sets a end cut point. Press F1 for help about cutting")
        Me.bRangeEnd.UseVisualStyleBackColor = False
        '
        'bForward10
        '
        Me.bForward10.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.bForward10.BackColor = System.Drawing.Color.White
        Me.bForward10.BackgroundImage = Global.StaxRip.My.Resources.Resources.Right2
        Me.bForward10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.bForward10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bForward10.Location = New System.Drawing.Point(280, 274)
        Me.bForward10.Name = "bForward10"
        Me.bForward10.Size = New System.Drawing.Size(32, 32)
        Me.bForward10.TabIndex = 42
        Me.bForward10.TabStop = False
        Me.ToolTip.SetToolTip(Me.bForward10, "Forward 10 Frames")
        Me.bForward10.UseVisualStyleBackColor = False
        '
        'bBackward10
        '
        Me.bBackward10.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.bBackward10.BackColor = System.Drawing.Color.White
        Me.bBackward10.BackgroundImage = CType(resources.GetObject("bBackward10.BackgroundImage"), System.Drawing.Image)
        Me.bBackward10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.bBackward10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bBackward10.Location = New System.Drawing.Point(80, 274)
        Me.bBackward10.Name = "bBackward10"
        Me.bBackward10.Size = New System.Drawing.Size(32, 32)
        Me.bBackward10.TabIndex = 41
        Me.bBackward10.TabStop = False
        Me.ToolTip.SetToolTip(Me.bBackward10, "Backward 10 Frames")
        Me.bBackward10.UseVisualStyleBackColor = False
        '
        'bExtras
        '
        Me.bExtras.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.bExtras.BackColor = System.Drawing.Color.White
        Me.bExtras.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bExtras.ForeColor = System.Drawing.SystemColors.ControlText
        Me.bExtras.Location = New System.Drawing.Point(400, 274)
        Me.bExtras.ShowMenuSymbol = True
        Me.bExtras.Size = New System.Drawing.Size(32, 32)
        Me.bExtras.TabStop = False
        Me.ToolTip.SetToolTip(Me.bExtras, "Shows the menu")
        '
        'bForward100
        '
        Me.bForward100.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.bForward100.BackColor = System.Drawing.Color.White
        Me.bForward100.BackgroundImage = Global.StaxRip.My.Resources.Resources.Right3
        Me.bForward100.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.bForward100.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bForward100.ForeColor = System.Drawing.SystemColors.ControlText
        Me.bForward100.Location = New System.Drawing.Point(320, 274)
        Me.bForward100.Name = "bForward100"
        Me.bForward100.Size = New System.Drawing.Size(32, 32)
        Me.bForward100.TabIndex = 49
        Me.bForward100.TabStop = False
        Me.ToolTip.SetToolTip(Me.bForward100, "Forward 100 Frames")
        Me.bForward100.UseVisualStyleBackColor = False
        '
        'bBackward100
        '
        Me.bBackward100.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.bBackward100.BackColor = System.Drawing.Color.White
        Me.bBackward100.BackgroundImage = CType(resources.GetObject("bBackward100.BackgroundImage"), System.Drawing.Image)
        Me.bBackward100.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.bBackward100.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bBackward100.Font = New System.Drawing.Font("Segoe UI", 4.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bBackward100.ForeColor = System.Drawing.SystemColors.ControlText
        Me.bBackward100.Location = New System.Drawing.Point(40, 274)
        Me.bBackward100.Name = "bBackward100"
        Me.bBackward100.Size = New System.Drawing.Size(32, 32)
        Me.bBackward100.TabIndex = 48
        Me.bBackward100.TabStop = False
        Me.ToolTip.SetToolTip(Me.bBackward100, "Backward 100 Frames")
        Me.bBackward100.UseVisualStyleBackColor = False
        '
        'pVideo
        '
        Me.pVideo.ContextMenuStrip = Me.cmsMain
        Me.pVideo.Controls.Add(Me.pTrack)
        Me.pVideo.Controls.Add(Me.bRangeEnd)
        Me.pVideo.Controls.Add(Me.bRangeStart)
        Me.pVideo.Controls.Add(Me.bBackward1)
        Me.pVideo.Controls.Add(Me.bForward1)
        Me.pVideo.Controls.Add(Me.bDeleteRange)
        Me.pVideo.Controls.Add(Me.bBackward100)
        Me.pVideo.Controls.Add(Me.bForward100)
        Me.pVideo.Controls.Add(Me.bExtras)
        Me.pVideo.Controls.Add(Me.bBackward10)
        Me.pVideo.Controls.Add(Me.bForward10)
        Me.pVideo.Location = New System.Drawing.Point(16, 16)
        Me.pVideo.Name = "pVideo"
        Me.pVideo.Size = New System.Drawing.Size(472, 312)
        Me.pVideo.TabIndex = 50
        '
        'cmsMain
        '
        Me.cmsMain.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.cmsMain.Name = "cmsMain"
        Me.cmsMain.Size = New System.Drawing.Size(61, 4)
        '
        'pTrack
        '
        Me.pTrack.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pTrack.BackColor = System.Drawing.SystemColors.Control
        Me.pTrack.Cursor = System.Windows.Forms.Cursors.SizeNS
        Me.pTrack.Location = New System.Drawing.Point(16, 256)
        Me.pTrack.Name = "pTrack"
        Me.pTrack.Size = New System.Drawing.Size(440, 12)
        Me.pTrack.TabIndex = 51
        '
        'ToolTip
        '
        Me.ToolTip.AutoPopDelay = 5000
        Me.ToolTip.BackColor = System.Drawing.Color.White
        Me.ToolTip.InitialDelay = 100
        Me.ToolTip.ReshowDelay = 100
        '
        'PreviewForm
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(506, 352)
        Me.Controls.Add(Me.pVideo)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "PreviewForm"
        Me.Text = "Preview"
        Me.pVideo.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public AVI As AVIFile
    Public RangeStart As Integer = -1

    Private InfoAviSynthDocument As VideoScript
    Private AviSynthDocument As VideoScript
    Private SizeFactor As Double = 1
    Private TargetFrames As Integer
    Private WithEvents GenericMenu As CustomMenu
    Private CommandManager As New CommandManager
    Private Drawer As VideoDrawer

    Private Const TrackBarBorder As Integer = 1
    Private Const TrackBarGap As Integer = 1
    Private Const TrackBarPosition As Integer = 4

    Private Shared Instances As New List(Of PreviewForm)

    Sub New(aviSynthDocument As VideoScript)
        MyBase.New()
        InitializeComponent()
        Icon = My.Resources.MainIcon

        CommandManager.AddCommandsFromObject(Me)
        CommandManager.AddCommandsFromObject(g.DefaultCommands)

        GenericMenu = New CustomMenu(AddressOf GetDefaultMenuPreview,
            s.CustomMenuPreview, CommandManager, cmsMain)

        GenericMenu.AddKeyDownHandler(Me)
        GenericMenu.BuildMenu()

        Instances.Add(Me)

        If Instances.Count > 1 Then
            For Each i In Instances
                i.ShowInTaskbar = True
            Next
        End If

        Me.AviSynthDocument = aviSynthDocument

        RefreshScript()

        If WindowState <> s.WindowStatePreview Then
            SwitchWindowState()
        End If

        ShowButtons(Not s.HidePreviewButtons)
    End Sub

    Sub RefreshScript()
        TargetFrames = p.Script.GetFrames
        If Not AVI Is Nothing Then AVI.Dispose()
        AVI = New AVIFile(AviSynthDocument.Path)
        Drawer = New VideoDrawer(pVideo, AVI)

        If WindowState = FormWindowState.Normal Then
            Normal()
        Else
            Full()
        End If

        If s.LastPosition < (AVI.FrameCount - 1) Then
            AVI.Position = s.LastPosition
        End If

        Drawer.Draw()

        AfterPositionChanged()
    End Sub

    Private Sub Full()
        pTrack.Visible = False

        FormBorderStyle = Windows.Forms.FormBorderStyle.None
        WindowState = FormWindowState.Maximized

        Dim ratio As Double

        If Calc.IsARSignalingRequired Then
            ratio = Calc.GetTargetDAR
        Else
            ratio = AVI.FrameSize.Width / AVI.FrameSize.Height
        End If

        Dim b = Screen.FromControl(Me).Bounds

        pVideo.Dock = DockStyle.None

        If ratio > b.Width / b.Height Then
            Dim h = CInt(b.Width / ratio)
            pVideo.Left = 0
            pVideo.Top = CInt((b.Height - h) / 2)
            pVideo.Width = b.Width
            pVideo.Height = h
        Else
            Dim w = CInt(b.Height * ratio)
            pVideo.Left = CInt((b.Width - w) / 2)
            pVideo.Top = 0
            pVideo.Width = w
            pVideo.Height = b.Height
        End If

        pTrack.Visible = True
    End Sub

    Private Sub Normal()
        WindowState = FormWindowState.Normal
        FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
        pVideo.Dock = DockStyle.Fill

        Dim w = AVI.FrameSize.Width, h = AVI.FrameSize.Height
        Dim newSize As Size
        Dim b = Screen.FromControl(Me).WorkingArea

        If Calc.IsARSignalingRequired Then
            newSize = New Size(CInt(h * SizeFactor * Calc.GetTargetDAR), CInt(h * SizeFactor))
        Else
            newSize = New Size(CInt(w * SizeFactor), CInt(h * SizeFactor))
        End If

        If newSize.Width > b.Width * 0.8 Then
            Dim w2 = CInt(b.Width * 0.8)
            newSize.Height = CInt(w2 * newSize.Height / newSize.Width)
            newSize.Width = w2
        End If

        ClientSize = newSize
    End Sub

    <Command("Perform | Switch Window State", "Switches the window state between full and normal.")>
    Sub SwitchWindowState()
        If WindowState = FormWindowState.Normal Then
            Full()
        Else
            Normal()
        End If

        AfterPositionChanged()
    End Sub

    Sub ShowButtons(vis As Boolean)
        bForward1.Visible = vis
        bBackward1.Visible = vis
        bRangeStart.Visible = vis
        bRangeEnd.Visible = vis
        bDeleteRange.Visible = vis
        bForward10.Visible = vis
        bBackward10.Visible = vis
        bForward100.Visible = vis
        bBackward100.Visible = vis
        bExtras.Visible = vis
    End Sub

    Private Sub bForward1_Click() Handles bForward1.Click
        For Each i As PreviewForm In Instances
            SetRelativePos(1)
        Next
    End Sub

    Private Sub bBackward1_Click() Handles bBackward1.Click
        For Each i As PreviewForm In Instances
            SetRelativePos(-1)
        Next
    End Sub

    Private Sub bRangeStart_Click() Handles bRangeStart.Click
        For Each i As PreviewForm In Instances
            SetRangeStart()
        Next
    End Sub

    Private Sub bRangeEnd_Click() Handles bRangeEnd.Click
        For Each i As PreviewForm In Instances
            SetRangeEnd()
        Next
    End Sub

    Private Sub bBackward10_Click() Handles bBackward10.Click
        For Each i As PreviewForm In Instances
            SetRelativePos(-10)
        Next
    End Sub

    Private Sub bForward10_Click() Handles bForward10.Click
        For Each i As PreviewForm In Instances
            SetRelativePos(10)
        Next
    End Sub

    Private Sub bDeleteRange_Click() Handles bDeleteRange.Click
        For Each i As PreviewForm In Instances
            DeleteRange()
        Next
    End Sub

    Private Sub bBackward100_Click() Handles bBackward100.Click
        For Each i As PreviewForm In Instances
            i.SetRelativePos(-100)
        Next
    End Sub

    Private Sub bForward100_Click() Handles bForward100.Click
        For Each i As PreviewForm In Instances
            SetRelativePos(100)
        Next
    End Sub

    Private Sub PreviewForm_Shown() Handles Me.Shown
        GenericMenu.Check("ToggleInfos", s.PreviewToggleInfos)
    End Sub

    Private Sub Wheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
        If e.Delta > 0 Then
            If Control.ModifierKeys = Keys.Alt Then
                AVI.Position += 1000
            ElseIf Control.ModifierKeys = Keys.Shift Then
                AVI.Position += 100
            ElseIf Control.ModifierKeys = Keys.Control Then
                AVI.Position += 10
            Else
                AVI.Position += 1
            End If
        Else
            If Control.ModifierKeys = Keys.Alt Then
                AVI.Position -= 1000
            ElseIf Control.ModifierKeys = Keys.Shift Then
                AVI.Position -= 100
            ElseIf Control.ModifierKeys = Keys.Control Then
                AVI.Position -= 10
            Else
                AVI.Position -= 1
            End If
        End If

        Drawer.Draw()
        DrawTrack()
    End Sub

    Sub DrawTrack()
        Dim g = pTrack.CreateGraphics()
        g.FillRectangle(Brushes.White, pTrack.ClientRectangle)

        Dim trackWidth = pTrack.Height - TrackBarBorder * 2 - TrackBarGap * 2

        Dim borderPen As New Pen(Color.Black, TrackBarBorder)
        borderPen.Alignment = Drawing2D.PenAlignment.Inset
        g.DrawRectangle(borderPen, 0, 0, pTrack.Width - 1, pTrack.Height - 1)
        borderPen.Dispose()

        If p.Ranges.Count > 0 Then
            For x = 0 To p.Ranges.Count - 1
                Dim c As Color

                If (x Mod 2) = 0 Then
                    c = Color.Green
                Else
                    c = Color.LimeGreen
                End If

                Using rangePen As New Pen(c, trackWidth)
                    g.DrawLine(rangePen, GetDrawPos(p.Ranges(x).Start) - TrackBarPosition \ 2,
                        pTrack.Height \ 2, GetDrawPos(p.Ranges(x).End) + TrackBarPosition \ 2, pTrack.Height \ 2)
                End Using
            Next
        End If

        Dim rangeSetPen As New Pen(Color.DarkOrange, trackWidth)

        If RangeStart > -1 AndAlso RangeStart <= AVI.Position Then
            g.DrawLine(rangeSetPen, GetDrawPos(RangeStart) - TrackBarPosition \ 2,
                pTrack.Height \ 2, GetDrawPos(AVI.Position) +
                TrackBarPosition \ 2, pTrack.Height \ 2)
        End If

        rangeSetPen.Dispose()

        Dim posPen As Pen

        If RangeStart > -1 Then
            posPen = New Pen(Color.DarkOrange, trackWidth)
        Else
            posPen = New Pen(Color.Blue, trackWidth)
        End If

        Dim pos = GetDrawPos(AVI.Position)
        g.DrawLine(posPen, pos - TrackBarPosition \ 2, pTrack.Height \ 2, pos + TrackBarPosition \ 2, pTrack.Height \ 2)
        posPen.Dispose()

        g.Dispose()
    End Sub

    Private Function GetDrawPos(frame As Integer) As Integer
        Dim values = TrackBarBorder * 2 + TrackBarGap * 2 + TrackBarPosition
        Dim width = CInt(((pTrack.Width - values) / TargetFrames) * frame)
        Return width + values \ 2
    End Function

    Private Sub pTrack_MouseMove(sender As Object, e As MouseEventArgs) Handles pTrack.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            HandleMouseOntrackBar()
        End If
    End Sub

    Private Sub pTrack_MouseDown(sender As Object, e As MouseEventArgs) Handles pTrack.MouseDown
        HandleMouseOntrackBar()
    End Sub

    Private Sub HandleMouseOntrackBar()
        Dim pos = CInt((TargetFrames / pTrack.Width) * pTrack.PointToClient(Control.MousePosition).X)

        Dim remainder = pos Mod 4

        If remainder <> 0 Then
            pos -= remainder
        End If

        For Each i In Instances
            i.SetAbsolutePos(pos)
        Next
    End Sub

    Private Sub pTrack_Paint(sender As Object, e As PaintEventArgs) Handles pTrack.Paint
        DrawTrack()
    End Sub

    Private Sub bExtras_Click() Handles bExtras.Click
        cmsMain.Show(bExtras, New Point(1, bExtras.Height))
    End Sub

    <Command("Parameter | Absolute Position", "Jumps to a given frame.")>
    Sub SetAbsolutePos(<DispName("Position")> pos As Integer)
        AVI.Position = pos
        Drawer.Draw()
        AfterPositionChanged()
    End Sub

    Private Sub AfterPositionChanged()
        s.LastPosition = AVI.Position
        DrawTrack()
        Text = "Preview " & s.LastPosition
    End Sub

    <Command("Dialog | Go To Time", "Dialog to jump to a specific time.")>
    Sub GoToTime()
        Dim d As Date
        d = d.AddSeconds(AVI.Position / AVI.FrameRate)
        Dim value = InputBox.Show("Time:", "Go To Time", d.ToString("HH:mm:ss.fff"))

        If value <> "" Then
            Try
                AVI.Position = CInt((TimeSpan.Parse(value).TotalMilliseconds / 1000) * AVI.FrameRate)
                Drawer.Draw()
                AfterPositionChanged()
            Catch
            End Try
        End If
    End Sub

    <Command("Dialog | Go To Frame", "Dialog to jump to a specific frame.")>
    Sub GoToFrame()
        Dim value = InputBox.Show("Frame:", "Go To Frame", AVI.Position.ToString)
        Dim pos As Integer

        If Integer.TryParse(value, pos) Then
            AVI.Position = pos
            Drawer.Draw()
            AfterPositionChanged()
        End If
    End Sub

    <Command("Parameter | Relative Position", "Jumps a given frame count.")>
    Sub SetRelativePos(<DispName("Position"),
        Description("Frames to jump, negative values jump backward.")>
        pos As Integer)

        AVI.Position += pos
        Drawer.Draw()
        AfterPositionChanged()
    End Sub

    <Command("Dialog | Menu Editor", "Opens the menu editor.")>
    Sub OpenMenuEditor()
        s.CustomMenuPreview = GenericMenu.Edit()
        g.MainForm.SaveSettings()
    End Sub

    <Command("Dialog | Help", "Opens the help.")>
    Sub OpenHelp()
        Dim f As New HelpForm
        f.Doc.WriteStart("Preview")
        f.Doc.WriteP("The preview dialog allows to preview and cut the video. Cutting can be achieved by selecting the sections that should be kept. Selected sections appear green in the trackbar. Everything not green will be trimmed off. It's possible to open more than one instance of the preview for filter comparisons.")
        f.Doc.WriteTips(GenericMenu.GetTips)
        f.Doc.WriteTable("Shortcut Keys", GenericMenu.GetKeys, False)
        f.Show()
    End Sub

    <Command("Perform | Set Cut Start", "Sets the start cut position.")>
    Sub SetRangeStart()
        Dim r = GetCurrentRange()

        If r Is Nothing OrElse AVI.Position = r.End Then
            RangeStart = AVI.Position
        Else
            r.Start = AVI.Position
        End If

        MergeRanges()
        AfterPositionChanged()
    End Sub

    <Command("Perform | Set Cut End", "Sets the end cut position.")>
    Sub SetRangeEnd()
        If RangeStart > -1 Then
            p.Ranges.Add(New Range(RangeStart, AVI.Position))
            p.Ranges.Sort()
            RangeStart = -1
        Else
            Dim r = GetCurrentRange()

            If Not r Is Nothing Then
                r.End = AVI.Position
                RangeStart = -1
            End If
        End If

        MergeRanges()
        AfterPositionChanged()
    End Sub

    Sub MergeRanges()
        For Each i In p.Ranges.ToArray
            For Each i2 In p.Ranges.ToArray
                If Not i Is i2 Then
                    If (i2.Start >= i.Start AndAlso i2.Start <= i.End) OrElse
                        (i2.End >= i.Start AndAlso i2.End <= i.End) Then

                        p.Ranges.Remove(i)
                        p.Ranges.Remove(i2)
                        p.Ranges.Add(New Range(Math.Min(i.Start, i2.Start), Math.Max(i.End, i2.End)))
                        MergeRanges()
                    End If
                End If
            Next
        Next

        p.Ranges.Sort()
    End Sub

    <Command("Perform | Split", "Splits the clip or selection into two selections.")>
    Sub SplitRange()
        If p.Ranges.Count = 0 Then
            p.Ranges.Add(New Range(0, AVI.FrameCount - 1))
        End If

        For Each i In p.Ranges.ToArray
            If i.Start < AVI.Position AndAlso i.End > AVI.Position Then
                p.Ranges.Add(New Range(i.Start, AVI.Position))
                p.Ranges.Add(New Range(AVI.Position + 1, i.End))
                p.Ranges.Remove(i)
                Exit For
            End If
        Next

        p.Ranges.Sort()

        AfterPositionChanged()
    End Sub

    <Command("Perform | Clear Cuts", "Clears all cuts.")>
    Sub ClearAllRanges()
        p.Ranges.Clear()
        AfterPositionChanged()
    End Sub

    <Command("Perform | Delete Current Range", "Deletes the range that encloses the current position.")>
    Sub DeleteRange()
        For Each i As Range In p.Ranges.ToArray
            If AVI.Position >= i.Start AndAlso AVI.Position <= i.End Then
                p.Ranges.Remove(i)
            End If
        Next

        AfterPositionChanged()
    End Sub

    <Command("Perform | Show Hide Buttons", "Shows/hides the buttons.")>
    Sub ShowHideButtons()
        s.HidePreviewButtons = Not s.HidePreviewButtons
        ShowButtons(Not s.HidePreviewButtons)
        AfterPositionChanged()
    End Sub

    <Command("Perform | Show Hide Trackbar", "Shows/hides the trackbar.")>
    Sub ShowHideTrackbar()
        pTrack.Visible = Not pTrack.Visible
        AfterPositionChanged()
    End Sub

    <Command("Parameter | Change Size", "Changes the size.")>
    Sub Zoom(<DispName("Factor")> factor As Single)
        SizeFactor += factor
        Normal()

        Left = (Screen.FromControl(Me).WorkingArea.Width - Width) \ 2
        Top = (Screen.FromControl(Me).WorkingArea.Height - Height) \ 2

        AfterPositionChanged()
    End Sub

    <Command("Perform | Show/Hide Infos", "Shows/hides various infos.")>
    Sub ToggleInfos()
        s.PreviewToggleInfos = Not s.PreviewToggleInfos
        Drawer.ShowInfos = s.PreviewToggleInfos
        Drawer.Draw()
        GenericMenu.Check("ToggleInfos", s.PreviewToggleInfos)
    End Sub

    <Command("Perform | Show External Player", "Shows the AviSynth script using the player currently associated with AVI files.")>
    Sub ShowExternalPlayer()
        UpdateTrim()
        g.PlayScript(p.Script)
    End Sub

    <Command("Perform | Reload Script", "Reloads the script.")>
    Sub Reload()
        RefreshScript()
        AfterPositionChanged()
    End Sub

    <Command("Perform | Exit", "Closes the dialog.")>
    Sub CloseDialog()
        Close()
    End Sub

    Private Sub pVideo_DoubleClick() Handles pVideo.DoubleClick
        SwitchWindowState()
    End Sub

    Private Sub pVideo_MouseMove(sender As Object, e As MouseEventArgs) Handles pVideo.MouseMove
        If Not WindowState = FormWindowState.Maximized AndAlso e.Button = MouseButtons.Left Then
            Dim HTCAPTION = New IntPtr(2)
            Native.ReleaseCapture()
            Native.PostMessage(Handle, Native.WM_NCLBUTTONDOWN, HTCAPTION, IntPtr.Zero)
        End If
    End Sub

    <Command("Perform | Previous Cut Point", "Jumps to the previous cut point.")>
    Sub JumpToThePreviousRangePos()
        Dim list As New List(Of Object)

        For Each i As Range In p.Ranges
            list.Add(i.Start)
            list.Add(i.End)
        Next

        list.Add(AVI.Position - 1)

        list.Sort()

        Dim index = list.IndexOf(AVI.Position - 1) - 1

        If index >= 0 AndAlso index < list.Count Then
            AVI.Position = CInt(list(index))
            Drawer.Draw()
            AfterPositionChanged()
        End If
    End Sub

    <Command("Perform | Copy Time", "Copies the time of the current position.")>
    Sub CopyTime()
        Dim d As Date
        d = d.AddSeconds(AVI.Position / AVI.FrameRate)
        Clipboard.SetText(d.ToString("HH:mm:ss.fff"))
    End Sub

    <Command("Perform | Next Cut Point", "Jumps to the next cut point.")>
    Sub JumpToTheNextRangePos()
        Dim list As New List(Of Object)

        For Each i As Range In p.Ranges
            list.Add(i.Start)
            list.Add(i.End)
        Next

        list.Add(AVI.Position + 1)
        list.Sort()

        Dim index = list.IndexOf(AVI.Position + 1) + 1

        If index >= 0 AndAlso index < list.Count Then
            AVI.Position = CInt(list(index))
            Drawer.Draw()
            AfterPositionChanged()
        End If
    End Sub

    <Command("Perform | Save Bitmap", "Saves the current frame as bitmap.")>
    Sub SaveBitmap()
        Using d As New SaveFileDialog
            d.SetFilter("bmp")
            d.FileName = Filepath.GetBase(p.TargetFile) + " - " & AVI.Position

            If d.ShowDialog = DialogResult.OK Then
                AVI.GetBitmap.Save(d.FileName)
            End If
        End Using
    End Sub

    <Command("Perform | Save JPG", "Saves the current frame as JPG.")>
    Sub SaveJPG()
        Dim quality = InputBox.Show("Enter quality up to 100%", "Quality", "90")

        If quality <> "" Then
            Using d As New SaveFileDialog
                d.SetFilter("jpg")
                d.FileName = Filepath.GetBase(p.TargetFile) + " - " & AVI.Position

                If d.ShowDialog = DialogResult.OK Then
                    Dim encoder = Imaging.ImageCodecInfo.GetImageDecoders.Where(
                        Function(a) a.FormatID = Imaging.ImageFormat.Jpeg.Guid).First()

                    Dim params As New Imaging.EncoderParameters(1)
                    Dim param As New Imaging.EncoderParameter(Imaging.Encoder.Quality, quality.ToInt)
                    params.Param(0) = param
                    AVI.GetBitmap.Save(d.FileName, encoder, params)
                End If
            End Using
        End If
    End Sub

    Shared Function GetDefaultMenuPreview() As CustomMenuItem
        Dim r As New CustomMenuItem("Root")

        r.Add("Navigation|Go To Start", "SetAbsolutePos", Keys.Control Or Keys.Left, 0)
        r.Add("Navigation|Go To End", "SetAbsolutePos", Keys.Control Or Keys.Right, 1000000000)
        r.Add("Navigation|-")
        r.Add("Navigation|Go To Frame...", "GoToFrame", Keys.Control Or Keys.G)
        r.Add("Navigation|Go To Time...", "GoToTime")
        r.Add("Navigation|-")
        r.Add("Navigation|Go To Previous Cut Point", "JumpToThePreviousRangePos", Keys.Control Or Keys.Up)
        r.Add("Navigation|Go To Next Cut Point", "JumpToTheNextRangePos", Keys.Control Or Keys.Down)
        r.Add("Navigation|-")
        r.Add("Navigation|Backward 1 Frame", "SetRelativePos", Keys.Left, -1)
        r.Add("Navigation|Backward 10 Frames", "SetRelativePos", Keys.Up, -10)
        r.Add("Navigation|Backward 100 Frames", "SetRelativePos", Keys.Prior, -100)
        r.Add("Navigation|Backward 1000 Frames", "SetRelativePos", Keys.Subtract, -1000)
        r.Add("Navigation|-")
        r.Add("Navigation|Forward 1000 Frames", "SetRelativePos", Keys.Add, 1000)
        r.Add("Navigation|Forward 100 Frames", "SetRelativePos", Keys.Next, 100)
        r.Add("Navigation|Forward 10 Frames", "SetRelativePos", Keys.Down, 10)
        r.Add("Navigation|Forward 1 Frames", "SetRelativePos", Keys.Right, 1)

        r.Add("Cut|Begin Selection", "SetRangeStart", Keys.Home)
        r.Add("Cut|End Selection", "SetRangeEnd", Keys.End)
        r.Add("Cut|-")
        r.Add("Cut|Split", "SplitRange", Keys.S)
        r.Add("Cut|-")
        r.Add("Cut|Delete Selection", "DeleteRange", Keys.Delete)
        r.Add("Cut|Delete All Selections", "ClearAllRanges", Keys.Control Or Keys.Delete)

        r.Add("View|Infos", "ToggleInfos", Keys.I)
        r.Add("View|Fullscreen", "SwitchWindowState", Keys.Enter)
        r.Add("View|-")
        r.Add("View|Zoom In", "Zoom", Keys.OemMinus, -0.25F)
        r.Add("View|Zoom Out", "Zoom", Keys.Oemplus, 0.25F)
        r.Add("View|-")
        r.Add("View|Buttons", "ShowHideButtons", Keys.B)
        r.Add("View|Trackbar", "ShowHideTrackbar", Keys.T)

        r.Add("Tools|Reload", "Reload", Keys.R)
        r.Add("Tools|External Player", "ShowExternalPlayer", Keys.E)
        r.Add("Tools|-")
        r.Add("Tools|Copy Frame Number", "CopyToClipboard", "%pos_frame%")
        r.Add("Tools|Copy Time", "CopyTime")
        r.Add("Tools|-")
        r.Add("Tools|Save Bitmap", "SaveBitmap", Keys.Control Or Keys.S)
        r.Add("Tools|Save JPG", "SaveJPG")
        r.Add("Tools|-")
        r.Add("Tools|Set Start Zone", "AddX264Zone", "0", "%pos_frame%", "q=30")
        r.Add("Tools|Set End Zone", "AddX264Zone", "%pos_frame%", "%eval:%source_frames%-1%", "q=35")
        r.Add("Tools|-")
        r.Add("Tools|Add Selection Zone", "AddX264Zone", "%sel_start%", "%sel_end%", "q=40")

        r.Add("Edit Menu...", "OpenMenuEditor", Keys.M)
        r.Add("Help...", "OpenHelp", Keys.F1)
        r.Add("Exit", "CloseDialog", Keys.Escape)

        Return r
    End Function

    Private Sub pVideo_Paint(sender As Object, e As PaintEventArgs) Handles pVideo.Paint
        Drawer.Draw(e.Graphics)
    End Sub

    Private Sub GenericMenu_Command(e As CustomMenuItemEventArgs) Handles GenericMenu.Command
        e.Handled = True

        If e.Item.MethodName = "CloseDialog" Then
            ProcessMenu(e.Item)
        Else
            For Each i As PreviewForm In Instances
                i.ProcessMenu(e.Item)
            Next
        End If
    End Sub

    Sub ProcessMenu(item As CustomMenuItem)
        Drawer.Draw()
        GenericMenu.Process(item)
    End Sub

    Private Sub PreviewForm_Load() Handles Me.Load
        Drawer.ShowInfos = s.PreviewToggleInfos
        Drawer.Draw()

        If WindowState = FormWindowState.Normal Then
            Normal()
        Else
            Full()
        End If
    End Sub

    Private Sub PreviewForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Instances.Remove(Me)
        s.LastPosition = AVI.Position
        s.WindowStatePreview = WindowState
        AVI.Dispose()
        UpdateTrim()
        g.MainForm.UpdateFilters()
    End Sub

    Sub UpdateTrim()
        p.CutFrameCount = AviSynthDocument.GetFrames
        p.CutFrameRate = AviSynthDocument.GetFramerate

        Dim cut = p.Script.GetFilter("Cutting")

        If p.Ranges.Count = 0 Then
            If Not cut Is Nothing Then
                p.Script.Filters.Remove(cut)
            End If
        Else
            If cut Is Nothing Then
                cut = New VideoFilter
                cut.Path = "Cutting"
                cut.Category = "Cutting"
                cut.Script = GetTrim()
                cut.Active = True
                p.Script.Filters.Add(cut)
            End If
        End If
    End Sub

    Function GetTrim() As String
        Dim ret As String

        For Each i In p.Ranges
            If ret <> "" Then ret += " + "

            If p.Script.Engine = ScriptingEngine.AviSynth Then
                ret += "Trim(" & i.Start & ", " & i.End & ")"

                If p.TrimCode <> "" Then
                    ret += "." + p.TrimCode.TrimStart("."c)
                End If
            Else
                ret += "core.std.Trim(clip, " & i.Start & ", " & i.End & ")"
            End If
        Next

        If p.Script.Engine = ScriptingEngine.AviSynth Then
            Return ret
        Else
            Return "clip = " + ret
        End If
    End Function

    Private Sub Control_Enter() Handles bBackward100.Enter, bBackward10.Enter, bBackward1.Enter, bForward1.Enter, bForward10.Enter, bForward100.Enter, bRangeEnd.Enter, bRangeStart.Enter, bDeleteRange.Enter, bExtras.Enter
        ActiveControl = Nothing
    End Sub

    Function GetCurrentRange() As Range
        For Each i In p.Ranges
            If AVI.Position >= i.Start AndAlso AVI.Position <= i.End Then
                Return i
            End If
        Next
    End Function

    Protected Overrides Sub OnHelpButtonClicked(e As CancelEventArgs)
        e.Cancel = True
        MyBase.OnHelpButtonClicked(e)
        OpenHelp()
    End Sub
End Class