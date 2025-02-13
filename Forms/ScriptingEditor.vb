﻿Imports StaxRip.UI
Imports System.ComponentModel
Imports Microsoft.Win32
Imports System.Text.RegularExpressions

Class ScriptingEditor
    Property ActiveTable As FilterTable
    Property Engine As ScriptingEngine

    Sub New(doc As VideoScript)
        InitializeComponent()

        KeyPreview = True

        MainFlowLayoutPanel.Margin = New Padding(0)
        MainFlowLayoutPanel.Padding = New Padding(0, 3, 0, 0)
        MainFlowLayoutPanel.SuspendLayout()

        Engine = doc.Engine

        For Each i In doc.Filters
            MainFlowLayoutPanel.Controls.Add(GetFilterTable(i))
        Next

        MainFlowLayoutPanel.ResumeLayout()
        AddHandler MainFlowLayoutPanel.Layout, AddressOf MainFlowLayoutPanelLayout

        AutoSizeMode = AutoSizeMode.GrowAndShrink
        AutoSize = True
    End Sub

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        Select Case e.KeyData
            Case Keys.F5
                VideoPreview()
            Case Keys.Control Or Keys.Delete
                If Not ActiveTable Is Nothing Then ActiveTable.RemoveClick()
            Case Keys.Control Or Keys.Up
                If Not ActiveTable Is Nothing Then ActiveTable.MoveUp()
            Case Keys.Control Or Keys.Down
                If Not ActiveTable Is Nothing Then ActiveTable.MoveDown()
        End Select

        MyBase.OnKeyDown(e)
    End Sub

    Shared Function GetFilterTable(filter As VideoFilter) As FilterTable
        Dim ret As New FilterTable

        ret.Margin = New Padding(3, 0, 3, 0)
        ret.Size = New Size(950, 50)
        ret.cbActive.Checked = filter.Active
        ret.cbActive.Text = filter.Category
        ret.tbName.Text = filter.Name
        ret.rtbScript.Text = If(filter.Script = "", "", filter.Script + CrLf)
        ret.SetColor()

        Return ret
    End Function

    Function GetFilters() As List(Of VideoFilter)
        Dim ret As New List(Of VideoFilter)

        For Each i As FilterTable In MainFlowLayoutPanel.Controls
            Dim f As New VideoFilter()
            f.Active = i.cbActive.Checked
            f.Category = i.cbActive.Text
            f.Path = i.tbName.Text
            f.Script = i.rtbScript.Text.FixBreak.Trim
            ret.Add(f)
        Next

        Return ret
    End Function

    Sub Play()
        If p.SourceFile = "" Then Exit Sub
        Dim doc As New VideoScript
        doc.Engine = Engine
        doc.Path = p.TempDir + p.Name + "_scriptEditor." + doc.FileType
        doc.Filters = GetFilters()
        g.PlayScript(doc)
    End Sub

    Sub VideoPreview()
        If p.SourceFile = "" Then Exit Sub
        Dim doc As New VideoScript
        doc.Engine = Engine
        doc.Path = p.TempDir + p.Name + "_avsEditor." + doc.FileType
        doc.Filters = GetFilters()
        doc.Synchronize(True)

        Dim f As New PreviewForm(doc)
        f.Owner = g.MainForm
        f.Show()
    End Sub

    Sub MainFlowLayoutPanelLayout(sender As Object, e As LayoutEventArgs)
        Dim filterTables = MainFlowLayoutPanel.Controls.OfType(Of FilterTable)
        If filterTables.Count = 0 Then Exit Sub
        Dim maxTextWidth = Aggregate i In filterTables Into Max(i.TextSize.Width)

        For Each i As FilterTable In MainFlowLayoutPanel.Controls
            Dim rtbSize As Size
            rtbSize.Width = maxTextWidth + FontHeight
            If rtbSize.Width < 300 Then rtbSize.Width = 300
            rtbSize.Height = i.TextSize.Height + CInt(FontHeight * 0.5)
            i.rtbScript.Size = rtbSize
            i.rtbScript.Refresh()
        Next
    End Sub

    Class FilterTable
        Inherits TableLayoutPanel

        Property tbName As New TextEdit
        Property rtbScript As RichTextBoxEx
        Property cbActive As New CheckBox
        Property Menu As New ContextMenuStripEx
        Property LastTextSize As Size
        Property Editor As ScriptingEditor

        Sub New()
            AutoSize = True
            cbActive.AutoSize = True
            cbActive.Anchor = AnchorStyles.Left Or AnchorStyles.Right
            tbName.Dock = DockStyle.Top
            tbName.Height = 36
            tbName.Width = 180

            rtbScript = New RichTextBoxEx(Menu)
            rtbScript.EnableAutoDragDrop = True
            rtbScript.Dock = DockStyle.Fill
            rtbScript.WordWrap = False
            rtbScript.ScrollBars = RichTextBoxScrollBars.None

            AddHandler Disposed, Sub() Menu.Dispose()
            AddHandler Menu.Opening, AddressOf MenuOpening
            AddHandler cbActive.CheckedChanged, Sub() SetColor()

            AddHandler rtbScript.Enter, Sub() Editor.ActiveTable = Me
            AddHandler rtbScript.TextChanged, Sub()
                                                  If Parent Is Nothing Then Exit Sub
                                                  Dim filterTables = Parent.Controls.OfType(Of FilterTable)
                                                  Dim maxTextWidth = Aggregate i In filterTables Into Max(i.TextSize.Width)

                                                  Dim textSizeVar = TextSize

                                                  If textSizeVar.Width > maxTextWidth OrElse
                                                      (textSizeVar.Width = maxTextWidth AndAlso
                                                      textSizeVar.Width <> LastTextSize.Width) OrElse
                                                      LastTextSize.Height <> textSizeVar.Height AndAlso
                                                      textSizeVar.Height > FontHeight * 2 Then

                                                      Parent.PerformLayout()
                                                      LastTextSize = TextSize
                                                  End If

                                                  rtbScript.ScrollToCaret()
                                              End Sub

            ColumnCount = 2
            ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            RowCount = 1
            RowStyles.Add(New RowStyle(SizeType.AutoSize))
            Dim t As New TableLayoutPanel
            t.AutoSize = True
            t.SuspendLayout()
            t.Dock = DockStyle.Fill
            t.Margin = New Padding(0)
            t.ColumnCount = 1
            t.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            t.RowCount = 2
            t.RowStyles.Add(New RowStyle(SizeType.AutoSize))
            t.RowStyles.Add(New RowStyle(SizeType.AutoSize))
            t.Controls.Add(cbActive, 0, 0)
            t.Controls.Add(tbName, 0, 1)
            t.ResumeLayout()
            Controls.Add(t, 0, 0)
            Controls.Add(rtbScript, 1, 0)
        End Sub

        Protected Overrides Sub OnHandleCreated(e As EventArgs)
            Menu.Form = FindForm()
            Editor = DirectCast(Menu.Form, ScriptingEditor)
            MyBase.OnHandleCreated(e)
        End Sub

        Sub SetColor()
            If cbActive.Checked Then
                rtbScript.ForeColor = Color.Black
                tbName.TextBox.ForeColor = Color.Black
                cbActive.ForeColor = Color.Black
            Else
                rtbScript.ForeColor = Color.Gray
                tbName.TextBox.ForeColor = Color.Gray
                cbActive.ForeColor = Color.Gray
            End If
        End Sub

        ReadOnly Property TextSize As Size
            Get
                Return TextRenderer.MeasureText(rtbScript.Text, rtbScript.Font, New Size(2000, 2000))
            End Get
        End Property

        Sub SetParameters(parameters As FilterParameters)
            Dim code = rtbScript.Text.FixBreak
            Dim match = Regex.Match(code, parameters.FunctionName + "\((.+)\)")
            Dim args = FilterParameters.SplitCSV(match.Groups(1).Value)
            Dim newParameters As New List(Of String)

            For Each argument In args
                Dim skip = False

                For Each parameter In parameters.Parameters
                    If argument Like "*" + parameter.Name + "*=*" Then
                        skip = True
                    End If
                Next

                If Not skip Then
                    newParameters.Add(argument)
                End If
            Next

            For Each parameter In parameters.Parameters
                newParameters.Add(parameter.Name + " = " + parameter.Value)
            Next

            rtbScript.Text = Regex.Replace(code, parameters.FunctionName + "\((.+)\)",
                                           parameters.FunctionName + "(" + newParameters.Join(", ") + ")")
        End Sub

        Sub MenuOpening(sender As Object, e As CancelEventArgs)
            Dim filterProfiles As List(Of FilterCategory)

            If p.Script.Engine = ScriptingEngine.AviSynth Then
                filterProfiles = s.AviSynthProfiles
            Else
                filterProfiles = s.VapourSynthProfiles
            End If

            Menu.Items.Clear()

            Dim code = rtbScript.Text.FixBreak

            For Each i In FilterParameters.Definitions
                If code.Contains(i.FunctionName + "(") Then
                    Dim match = Regex.Match(code, i.FunctionName + "\((.+)\)")

                    If match.Success Then
                        ActionMenuItem.Add(Menu.Items, i.Text, AddressOf SetParameters, i)
                    End If
                End If
            Next

            If Menu.Items.Count > 0 Then Menu.Items.Add(New ToolStripSeparator)

            For Each i In filterProfiles
                If i.Name = cbActive.Text Then
                    For Each i2 In i.Filters
                        Dim tip = i2.Script
                        If Not tip.Contains("%app:") Then tip = Macro.Solve(tip)
                        ActionMenuItem.Add(Menu.Items, i2.Path, AddressOf ReplaceClick, i2.GetCopy, tip)
                    Next
                End If
            Next

            ActionMenuItem.Add(Menu.Items, "Blank", AddressOf ReplaceClick, New VideoFilter("Misc", "", "", True))

            Menu.Items.Add(New ToolStripSeparator)

            Dim replace As New MenuItemEx("Replace")
            Dim insert As New MenuItemEx("Insert")

            Menu.Items.Add(replace)
            Menu.Items.Add(insert)

            For Each i In filterProfiles
                For Each i2 In i.Filters
                    Dim tip = i2.Script

                    If Not tip.Contains("%app:") Then
                        tip = Macro.Solve(tip)
                    End If

                    ActionMenuItem.Add(replace.DropDownItems,
                        i.Name + " | " + i2.Path, AddressOf ReplaceClick, i2.GetCopy, tip)

                    ActionMenuItem.Add(insert.DropDownItems,
                        i.Name + " | " + i2.Path, AddressOf InsertClick, i2.GetCopy, tip)
                Next
            Next

            ActionMenuItem.Add(replace.DropDownItems, "Blank", AddressOf ReplaceClick, New VideoFilter("Misc", "", "", True))
            ActionMenuItem.Add(insert.DropDownItems, "Blank", AddressOf InsertClick, New VideoFilter("Misc", "", "", True))

            Dim add As New MenuItemEx("Add")
            Menu.Items.Add(add)

            For Each i In filterProfiles
                For Each i2 In i.Filters
                    Dim tip = i2.Script

                    If Not tip.Contains("%app:") Then
                        tip = Macro.Solve(tip)
                    End If

                    ActionMenuItem.Add(add.DropDownItems, i.Name + " | " + i2.Path, AddressOf AddClick, i2.GetCopy, tip)
                Next
            Next

            ActionMenuItem.Add(add.DropDownItems, "Blank", AddressOf AddClick, New VideoFilter("Misc", "", "", True))

            Menu.Items.Add(New ToolStripSeparator)

            Menu.Add("Remove", AddressOf RemoveClick).ShortcutKeyDisplayString = KeysHelp.GetKeyString(Keys.Control Or Keys.Delete)
            Menu.Add("Profiles...", AddressOf g.MainForm.OpenFilterProfilesDialog, "Dialog to edit profiles.")
            Menu.Add("Macros...", AddressOf MacrosForm.ShowDialogForm, "Dialog to edit profiles.")
            Menu.Add("Script Preview...", AddressOf CodePreview, "Previews the script with solved macros.")

            Dim mi = Menu.Add("Video Preview...", AddressOf Editor.VideoPreview, "Previews the script with solved macros.")
            mi.Enabled = p.SourceFile <> ""
            mi.ShortcutKeyDisplayString = "F5"

            Menu.Add("Play...", AddressOf Editor.Play, "Plays the current script with MPC.")

            Menu.Items.Add(New ToolStripSeparator)

            Menu.Add("Move Up", AddressOf MoveUp).ShortcutKeyDisplayString = KeysHelp.GetKeyString(Keys.Control Or Keys.Up)
            Menu.Add("Move Down", AddressOf MoveDown).ShortcutKeyDisplayString = KeysHelp.GetKeyString(Keys.Control Or Keys.Down)

            Menu.Items.Add(New ToolStripSeparator)

            Dim cutAction = Sub()
                                Clipboard.SetText(rtbScript.SelectedText)
                                rtbScript.SelectedText = ""
                            End Sub

            Dim copyAction = Sub() Clipboard.SetText(rtbScript.SelectedText)

            Dim pasteAction = Sub()
                                  rtbScript.Paste()
                                  rtbScript.ScrollToCaret()
                              End Sub

            Menu.Add("Cut", cutAction).Enabled = rtbScript.SelectionLength > 0 AndAlso Not rtbScript.ReadOnly
            Menu.Add("Copy", copyAction).Enabled = rtbScript.SelectionLength > 0
            Menu.Add("Paste", pasteAction).Enabled = Clipboard.GetText <> "" AndAlso Not rtbScript.ReadOnly

            Menu.Items.Add(New ToolStripSeparator)

            For Each i In Packs.Packages.OfType(Of PluginPackage)()
                If Not i.AviSynthFilterNames Is Nothing Then
                    For Each i2 In i.AviSynthFilterNames
                        If rtbScript.Text.Contains(i2) Then
                            Dim path = i.GetHelpPath()

                            If path <> "" Then
                                Menu.Add("Help | " + i.Name, Sub() g.ShellExecute(path), path)
                            End If
                        End If
                    Next
                End If
            Next

            Dim installDir = Registry.LocalMachine.GetString("SOFTWARE\AviSynth", Nothing)
            Dim helpText = rtbScript.Text.Left("(")

            If helpText.EndsWith("Resize") Then helpText = "Resize"
            If helpText.StartsWith("ConvertTo") Then helpText = "Convert"

            Dim filterPath = installDir + "\Docs\English\corefilters\" + helpText + ".htm"

            If File.Exists(filterPath) Then
                Menu.Add("Help | " + helpText, Sub() g.ShellExecute(filterPath), filterPath)
            End If

            Dim helpIndex = installDir + "\Docs\English\overview.htm"

            If File.Exists(helpIndex) Then
                Menu.Add("Help | AviSynth", Sub() g.ShellExecute(helpIndex), helpIndex)
            End If

            Menu.Add("Help | AviSynth.nl", Sub() g.ShellExecute("http://avisynth.nl"), "http://avisynth.nl")
            Menu.Add("Help | vapoursynth.com", Sub() g.ShellExecute("http://www.vapoursynth.com"), "http://www.vapoursynth.com")

            For Each i In Packs.Packages
                If TypeOf i Is PluginPackage Then
                    Dim helpPath = i.GetHelpPath

                    If helpPath <> "" Then
                        Menu.Add("Help | Plugins | " + i.Name, Sub() g.ShellExecute(helpPath), i.Description)
                    End If
                End If
            Next
        End Sub

        Sub MoveUp()
            Dim flow = DirectCast(Parent, FlowLayoutPanel)
            Dim index = flow.Controls.IndexOf(Me)
            index -= 1
            If index < 0 Then index = 0
            flow.Controls.SetChildIndex(Me, index)
        End Sub

        Sub MoveDown()
            Dim flow = DirectCast(Parent, FlowLayoutPanel)
            Dim index = flow.Controls.IndexOf(Me)
            index += 1
            If index >= flow.Controls.Count - 1 Then index = flow.Controls.Count - 1
            flow.Controls.SetChildIndex(Me, index)
        End Sub

        Sub CodePreview()
            Dim script As New VideoScript
            script.Engine = Editor.Engine
            script.Filters = Editor.GetFilters()

            Using f As New StringEditorForm
                f.tb.ReadOnly = True
                f.cbWrap.Checked = False
                f.cbWrap.Visible = False
                f.tb.Text = Macro.Solve(VideoScript.ModifyScript(script.GetScript, script.Engine)).Trim
                f.tb.SelectionStart = f.tb.Text.Length
                f.tb.SelectionLength = 0
                f.Text = "Script Preview"
                f.Width = 800
                f.Height = 500
                f.bOK.Visible = False
                f.bCancel.Text = "Close"
                f.ShowDialog()
            End Using
        End Sub

        Sub RemoveClick()
            Dim flow = DirectCast(Parent, FlowLayoutPanel)

            If flow.Controls.Count > 1 Then
                flow.Controls.Remove(Me)
                Dispose()
                Editor.ActiveTable = Nothing
            End If
        End Sub

        Sub ReplaceClick(f As VideoFilter)
            cbActive.Checked = f.Active
            cbActive.Text = f.Category
            tbName.Text = f.Name
            rtbScript.Text = f.Script
        End Sub

        Sub InsertClick(f As VideoFilter)
            Dim flow = DirectCast(Parent, FlowLayoutPanel)
            Dim index = flow.Controls.IndexOf(Me)
            Dim filterTable = ScriptingEditor.GetFilterTable(f)
            flow.SuspendLayout()
            flow.Controls.Add(filterTable)
            flow.Controls.SetChildIndex(filterTable, index)
            flow.ResumeLayout()
        End Sub

        Sub AddClick(f As VideoFilter)
            Dim flow = DirectCast(Parent, FlowLayoutPanel)
            Dim filterTable = ScriptingEditor.GetFilterTable(f)
            flow.Controls.Add(filterTable)
        End Sub
    End Class
End Class