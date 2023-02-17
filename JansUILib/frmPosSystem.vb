Imports System.Drawing
Imports System.Windows.Forms.VisualStyles

Public Class POSSystem

    '---Init'

    'Variable Init'
    Public Shared accentColor As Color = Color.FromArgb(255, 255, 255)
    Dim cDialog As New ColorDialog()

    Public toggle2 As Boolean = False
    Public toggle3 As Boolean = False
    Public toggle4 As Boolean = False

    '---Winforms Init' 
    Private Sub POSSystem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For Each cntrl As Control In TblTabsContainer.Controls.OfType(Of Panel)
            cntrl.Width = 0
        Next
        ChangeTab(Label7, e)
        btnDummy.Focus()
    End Sub

    'Titlebar Button Events'

    Private Sub btnExit_Click(sender As Object, e As EventArgs)
        AuthLogin.Close()
    End Sub

    '---UI Library Fixes'

    Public Sub AntiFocus() Handles ListBox1.Click, pnlMiscPage.Click, pnlSettingsPage.Click, Panel38.Click, Panel36.Click, Label4.Click
        Panel169.Visible = False
        btnDummy.Focus()
        Panel33.BackColor = Color.FromArgb(0, 0, 0)
        Panel164.BackColor = Color.FromArgb(0, 0, 0)

    End Sub

    '---UI Library Functions'

    'Clear Textbox on Double Click
    Private Sub clearTextbox_DoubleClick(sender As Object, e As EventArgs) Handles input.DoubleClick
        sender.Text = ""
    End Sub

    'Control Highlighting
    Private Sub input_MouseEnter(sender As Object, e As EventArgs) Handles input.MouseEnter, Panel35.MouseEnter, Panel34.MouseEnter, Panel33.MouseEnter, Panel32.MouseEnter, btnButton.MouseEnter, BtnRemove.MouseEnter, btnClear.MouseEnter, ListBox1.MouseEnter, Panel93.MouseEnter, Panel94.MouseEnter, Panel95.MouseEnter, Panel96.MouseEnter, Label3.MouseEnter

        If sender Is input Or sender Is Panel35 Or sender Is Panel34 Or sender Is Panel33 Or sender Is Panel32 Then
            Panel33.BackColor = accentColor
        ElseIf sender Is btnButton Then
            Panel4.BackColor = accentColor
        ElseIf sender Is BtnRemove Then
            Panel54.BackColor = accentColor
        ElseIf sender Is btnClear Then
            Panel51.BackColor = accentColor
        ElseIf sender Is ListBox1 Or sender Is Panel58 Or sender Is Panel57 Then
            Panel56.BackColor = accentColor

        ElseIf sender Is Panel93 Or sender Is Panel94 Or sender Is Panel95 Or sender Is Panel96 Or sender Is Label3 Then
            Panel93.BackColor = accentColor

        End If
    End Sub

    Private Sub input_MouseLeave(sender As Object, e As EventArgs) Handles input.MouseLeave, Panel35.MouseLeave, Panel34.MouseLeave, Panel33.MouseLeave, Panel32.MouseLeave, btnButton.MouseLeave, BtnRemove.MouseLeave, btnClear.MouseLeave, ListBox1.MouseLeave, Panel93.MouseLeave, Panel94.MouseLeave, Panel95.MouseLeave, Panel96.MouseLeave, Label3.MouseLeave
        If sender Is input Or sender Is Panel35 Or sender Is Panel34 Or sender Is Panel33 Or sender Is Panel32 Then
            If input.Focused() Then
            Else
                Panel33.BackColor = Color.FromArgb(0, 0, 0)
            End If
        ElseIf sender Is btnButton Then
            Panel4.BackColor = Color.FromArgb(0, 0, 0)
        ElseIf sender Is BtnRemove Then
            Panel54.BackColor = Color.FromArgb(0, 0, 0)
        ElseIf sender Is btnClear Then
            Panel51.BackColor = Color.FromArgb(0, 0, 0)
        ElseIf sender Is ListBox1 Or sender Is Panel58 Or sender Is Panel57 Then
            Panel56.BackColor = Color.FromArgb(0, 0, 0)
        ElseIf sender Is Panel93 Or sender Is Panel94 Or sender Is Panel95 Or sender Is Panel96 Or sender Is Label3 Then
            Panel93.BackColor = Color.FromArgb(0, 0, 0)
        End If
    End Sub

    Private Sub controlHighlight_mouseEnter(sender As Object, e As EventArgs) Handles Panel164.MouseEnter, Panel165.MouseEnter, Panel166.MouseEnter, Panel167.MouseEnter, Panel134.MouseEnter, Panel133.MouseEnter, Panel91.MouseEnter, Panel90.MouseEnter, Label12.MouseEnter, Panel136.MouseEnter, Panel137.MouseEnter, Panel138.MouseEnter, Panel139.MouseEnter, Label13.MouseEnter, Label24.MouseEnter

        If sender Is Panel164 Or sender Is Panel165 Or sender Is Panel166 Or sender Is Panel167 Or sender Is Label24 Then
            Panel164.BackColor = accentColor

        ElseIf sender Is Panel134 Or sender Is Panel133 Or sender Is Panel90 Or sender Is Panel91 Or sender Is Label12 Then
            Panel90.BackColor = accentColor

        ElseIf sender Is Panel136 Or sender Is Panel137 Or sender Is Panel138 Or sender Is Panel139 Or sender Is Label13 Then
            Panel136.BackColor = accentColor
        End If
    End Sub

    Private Sub controlHighlight_mouseLeave(sender As Object, e As EventArgs) Handles Panel164.MouseLeave, Panel165.MouseLeave, Panel166.MouseLeave, Panel167.MouseLeave, Panel134.MouseLeave, Panel133.MouseLeave, Panel91.MouseLeave, Panel90.MouseLeave, Label12.MouseLeave, Panel136.MouseLeave, Panel137.MouseLeave, Panel138.MouseLeave, Panel139.MouseLeave, Label13.MouseLeave, Label24.MouseLeave

        If sender Is Panel164 Or sender Is Panel165 Or sender Is Panel166 Or sender Is Panel167 Or sender Is Label24 Then
            If Panel169.Visible = False Then
                Panel164.BackColor = Color.FromArgb(0, 0, 0)
            End If
        ElseIf sender Is Panel134 Or sender Is Panel133 Or sender Is Panel90 Or sender Is Label12 Then
            Panel90.BackColor = Color.FromArgb(0, 0, 0)
        ElseIf sender Is Panel136 Or sender Is Panel137 Or sender Is Panel138 Or sender Is Panel139 Or sender Is Label13 Then
            Panel136.BackColor = Color.FromArgb(0, 0, 0)
        End If
    End Sub

    Private Sub ChangeTab(sender As Object, e As EventArgs) Handles Label7.Click, Label8.Click, Label9.Click

        'Clean this up
        sel1.Visible = False
        sel2.Visible = False
        sel3.Visible = False
        pnlMiscPage.Dock = DockStyle.None
        pnlMiscPage.Height = 0
        pnlSettingsPage.Dock = DockStyle.None
        pnlSettingsPage.Height = 0
        pnlMainPage.Dock = DockStyle.None
        pnlMainPage.Height = 0
        Label7.ForeColor = Color.FromArgb(150, 150, 150)
        Label8.ForeColor = Color.FromArgb(150, 150, 150)
        Label9.ForeColor = Color.FromArgb(150, 150, 150)
        sender.ForeColor = accentColor
        AntiFocus()
        If sender Is Label7 Then
            pnlMainPage.Dock = DockStyle.Fill
            sel1.Visible = True

        ElseIf sender Is Label8 Then
            pnlMiscPage.Dock = DockStyle.Fill
            sel2.Visible = True
        ElseIf sender Is Label9 Then
            pnlSettingsPage.Dock = DockStyle.Fill
            sel3.Visible = True

        End If

    End Sub

    '---Change Colourisable Accents in UI

    Private Sub updateAccent()

        'Update the rest of the ui to use the new system
        Panel8.BackColor = accentColor
        Panel104.BackColor = accentColor
        Panel111.BackColor = accentColor
        Panel46.BackColor = accentColor
        Panel39.BackColor = accentColor
        Panel309.BackColor = accentColor
        Panel300.BackColor = accentColor
        Panel179.BackColor = accentColor
        Panel186.BackColor = accentColor
        Panel194.BackColor = accentColor
        Panel201.BackColor = accentColor
        Panel264.BackColor = accentColor
        Panel256.BackColor = accentColor
        Panel287.BackColor = accentColor
        Panel294.BackColor = accentColor

        For Each findGroupbox As Control In pnlSettingsPage.Controls.OfType(Of TableLayoutPanel)
            If findGroupbox.Tag = "groupbox" Then
                For Each findGroupboxHeader As Control In findGroupbox.Controls.OfType(Of Panel)
                    For Each findBarTable As Control In findGroupboxHeader.Controls.OfType(Of TableLayoutPanel)
                        For Each findBarOuter As Control In findBarTable.Controls
                            For Each findBarInner As Control In findBarOuter.Controls
                                If findBarInner.Tag = "colorise" Then
                                    findBarInner.BackColor = accentColor
                                End If
                            Next
                        Next
                    Next
                Next
            End If
        Next

        'Toggle Accent Updating
        If toggle2 Then
            Panel96.BackColor = accentColor
        End If
        If toggle3 Then
            Panel134.BackColor = accentColor
        End If
        If toggle4 Then
            Panel139.BackColor = accentColor
        End If
        sel1.BackColor = accentColor
        sel2.BackColor = accentColor
        sel3.BackColor = accentColor

        If Label7.ForeColor = Color.FromArgb(150, 150, 150) Then
        Else
            Label7.ForeColor = accentColor
        End If
        If Label8.ForeColor = Color.FromArgb(150, 150, 150) Then
        Else
            Label8.ForeColor = accentColor
        End If
        If Label9.ForeColor = Color.FromArgb(150, 150, 150) Then
        Else
            Label9.ForeColor = accentColor
        End If
    End Sub

    '---Application Code

    'Misc Tab

    'Example Toggle Switches
    Private Sub Toggle2_Click(sender As Object, e As EventArgs) Handles Panel93.Click, Panel94.Click, Panel95.Click, Panel96.Click, Label3.Click
        btnDummy.Focus()
        If toggle2 Then
            Panel96.BackColor = Color.FromArgb(30, 30, 30)
            toggle2 = False
            Label3.ForeColor = Color.FromArgb(150, 150, 150)
        Else
            Panel96.BackColor = accentColor
            toggle2 = True
            Label3.ForeColor = Color.FromArgb(255, 255, 255)
            Me.Refresh()
        End If
    End Sub

    Private Sub Toggle3_Click(sender As Object, e As EventArgs) Handles Panel134.Click, Panel133.Click, Panel90.Click, Label12.Click
        If toggle3 Then
            Panel134.BackColor = Color.FromArgb(30, 30, 30)
            toggle3 = False
            Label12.ForeColor = Color.FromArgb(150, 150, 150)
        Else
            Panel134.BackColor = accentColor
            toggle3 = True
            Label12.ForeColor = Color.FromArgb(255, 255, 255)
            Me.Refresh()
        End If

    End Sub

    Private Sub Toggle4_Click(sender As Object, e As EventArgs) Handles Panel139.Click, Panel136.Click, Panel137.Click, Panel138.Click, Panel139.Click, Label13.Click
        If toggle4 Then
            Panel139.BackColor = Color.FromArgb(30, 30, 30)
            toggle4 = False
            Label13.ForeColor = Color.FromArgb(150, 150, 150)
        Else
            Panel139.BackColor = accentColor
            toggle4 = True
            Label13.ForeColor = Color.FromArgb(255, 255, 255)
            Me.Refresh()
        End If
    End Sub

    Private Sub DropDown_Hide(sender As Object, e As EventArgs) Handles Label24.Click
        If Panel169.Visible = False Then
            Panel169.Visible = True
        Else
            Panel169.Visible = False
        End If
    End Sub

    'Settings Tab 

    'UI Accent Colour Picker
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button7.Click
        AntiFocus()
        If (cDialog.ShowDialog() = DialogResult.OK) Then
            accentColor = cDialog.Color ' update with user selected color.
        End If
        updateAccent()
    End Sub

    'User Logout Button
    Private Sub UserLogOut(sender As Object, e As EventArgs) Handles BtnLogOut.Click
        AntiFocus()
        Me.Close()
        AuthLogin.Show()
    End Sub

    'Config Buttons
    Private Sub btnButton_Click(sender As Object, e As EventArgs) Handles btnButton.Click
        AntiFocus()
        If ListBox1.Items.Contains(input.Text) Or input.TextLength < 1 Then
        Else
            ListBox1.Items.Add(input.Text)
        End If
    End Sub

    Private Sub BtnRemoveItem_Click(sender As Object, e As EventArgs) Handles BtnRemove.Click
        AntiFocus()
        ListBox1.Items.Remove(input.Text)
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        AntiFocus()
        ListBox1.Items.Clear()
    End Sub
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.GetItemText(ListBox1.SelectedItem) IsNot "" Then
            input.Text = ListBox1.GetItemText(ListBox1.SelectedItem)
        End If
    End Sub

    '---Watermark
    'Timer Tick Update
    Private Sub tmrMain_Tick(sender As Object, e As EventArgs) Handles tmrMain.Tick
        lblTitle.Text = "POS SYSTEM | [BUILD] | [USER] | " & DateTime.Now.ToString("HH:mm:ss") & " | " & DateTime.Now.ToString("dd MMM. yyyy")
    End Sub

    Friend Class UserDataDataSet
    End Class

    Friend Class UserDataDataSetTableAdapters
    End Class
End Class
