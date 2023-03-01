Public Class POSSystem

    '---Init'

    'Variable Init'
    Public Shared accentColor As Color = Color.FromArgb(255, 255, 255)
    ReadOnly cDialog As New ColorDialog()
    ReadOnly currentUser As String = "Dev"
    ReadOnly versionNumber As String = "[Dev Build]"

    'Toggles Variables Init
    Public toggle2 As Boolean = False
    Public toggle3 As Boolean = False
    Public toggle4 As Boolean = False

    '---Winforms Init' 
    Private Sub POSSystem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For Each cntrl As Control In TblTabsContainer.Controls.OfType(Of Panel)
            cntrl.Width = 0
        Next
        UpdateAccent()
        ChangeTab(Label7, e)
        btnDummy.Focus()
    End Sub

    '---UI Library Fixes'

    Public Sub AntiFocus() Handles pnlMiscPage.Click, pnlSettingsPage.Click, Panel38.Click
        Panel169.Visible = False
        btnDummy.Focus()

        Panel164.BackColor = Color.FromArgb(0, 0, 0)

    End Sub

    '---UI Library Functions'

    'Clear Textbox on Double Click
    Private Sub ClearTextbox_DoubleClick(sender As Object, e As EventArgs)
        sender.Text = ""
    End Sub

    'Control Highlighting
    Private Sub Input_MouseEnter(sender As Object, e As EventArgs) Handles Panel93.MouseEnter, Panel94.MouseEnter, Panel95.MouseEnter, Panel96.MouseEnter, Label3.MouseEnter
        If sender Is Panel93 Or sender Is Panel94 Or sender Is Panel95 Or sender Is Panel96 Or sender Is Label3 Then
            Panel93.BackColor = accentColor

        End If
    End Sub

    Private Sub Input_MouseLeave(sender As Object, e As EventArgs) Handles Panel93.MouseLeave, Panel94.MouseLeave, Panel95.MouseLeave, Panel96.MouseLeave, Label3.MouseLeave
        If sender Is Panel93 Or sender Is Panel94 Or sender Is Panel95 Or sender Is Panel96 Or sender Is Label3 Then
            Panel93.BackColor = Color.FromArgb(0, 0, 0)
        End If
    End Sub

    Private Sub ControlHighlight_mouseEnter(sender As Object, e As EventArgs) Handles Panel164.MouseEnter, Panel165.MouseEnter, Panel166.MouseEnter, Panel167.MouseEnter, Panel134.MouseEnter, Panel133.MouseEnter, Panel91.MouseEnter, Panel90.MouseEnter, Label12.MouseEnter, Panel136.MouseEnter, Panel137.MouseEnter, Panel138.MouseEnter, Panel139.MouseEnter, Label13.MouseEnter, Label24.MouseEnter
        If sender Is Panel164 Or sender Is Panel165 Or sender Is Panel166 Or sender Is Panel167 Or sender Is Label24 Then
            Panel164.BackColor = accentColor

        ElseIf sender Is Panel134 Or sender Is Panel133 Or sender Is Panel90 Or sender Is Panel91 Or sender Is Label12 Then
            Panel90.BackColor = accentColor

        ElseIf sender Is Panel136 Or sender Is Panel137 Or sender Is Panel138 Or sender Is Panel139 Or sender Is Label13 Then
            Panel136.BackColor = accentColor
        End If
    End Sub

    Private Sub ControlHighlight_mouseLeave(sender As Object, e As EventArgs) Handles Panel164.MouseLeave, Panel165.MouseLeave, Panel166.MouseLeave, Panel167.MouseLeave, Panel134.MouseLeave, Panel133.MouseLeave, Panel91.MouseLeave, Panel90.MouseLeave, Label12.MouseLeave, Panel136.MouseLeave, Panel137.MouseLeave, Panel138.MouseLeave, Panel139.MouseLeave, Label13.MouseLeave, Label24.MouseLeave
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
        For Each cntrl As Control In TblTabsContainer.Controls.OfType(Of Panel)
            cntrl.Visible = False

        Next
        For Each lbl As Control In TblTabsContainer.Controls.OfType(Of Label)
            lbl.ForeColor = Color.FromArgb(150, 150, 150)
        Next
        For Each menuscreen As Control In Panel1.Controls.OfType(Of Panel)
            menuscreen.Dock = DockStyle.None
            menuscreen.Height = 0
        Next

        sender.ForeColor = accentColor
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
        AntiFocus()
    End Sub

    '---Change Colourisable Accents in UI

    Private Sub UpdateAccent()
        'Groupbox Topbar Color Updating
        Panel8.BackColor = accentColor
        For Each menuscreen As Control In Panel1.Controls.OfType(Of Panel)
            For Each findGroupbox As Control In menuscreen.Controls.OfType(Of TableLayoutPanel)
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

        'Tab Highlight Color Updating
        For Each cntrl As Control In TblTabsContainer.Controls.OfType(Of Panel)
            cntrl.BackColor = accentColor
        Next

        'Tab Label Color Updating
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
        UpdateAccent()
    End Sub

    'User Logout Button
    Private Sub UserLogOut(sender As Object, e As EventArgs) Handles BtnLogOut.Click
        AntiFocus()
        Me.Close()
        AuthLogin.Show()
    End Sub

    '---Watermark

    'Timer Tick Update
    Private Sub TmrMain_Tick(sender As Object, e As EventArgs) Handles tmrMain.Tick
        lblTitle.Text = "POS SYSTEM | " & versionNumber & " | " & currentUser & " | " & DateTime.Now.ToString("HH:mm:ss") & " | " & DateTime.Now.ToString("dd MMM. yyyy")
    End Sub

    Friend Class UserDataDataSet
    End Class

    Friend Class UserDataDataSetTableAdapters
    End Class

    Private Sub AntiFocus(sender As Object, e As EventArgs) Handles pnlSettingsPage.Click, pnlMiscPage.Click, Panel38.Click

    End Sub
End Class
