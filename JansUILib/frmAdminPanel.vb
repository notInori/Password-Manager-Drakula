﻿Public Class AdminPanel

    '---Init'

    'Client Info Variables
    Public Shared ReadOnly businessName As String = ""
    Public Shared ReadOnly versionNumber As String = "[Dev Build]"
    ReadOnly currentUser As String = "Admin"

    'Variables Init'
    Public Shared accentColor As Color = Color.FromArgb(255, 255, 255)
    ReadOnly cDialog As New ColorDialog()

    'Toggles Variables Init
    Public toggle1 As Boolean = False
    Public toggle2 As Boolean = False
    Public toggle3 As Boolean = False

    'Databases
    Friend Class UserDataDataSet
    End Class

    Friend Class UserDataDataSetTableAdapters
    End Class

    '---Winforms Init' 

    'Init tab system and load accent color
    Private Sub POSSystem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For Each cntrl As Control In TblTabsContainer.Controls.OfType(Of Panel)
            cntrl.Width = 0
        Next
        lblCurrentUser.Text = currentUser
        UpdateAccent()
        ChangeTab(lblTabSel1, e)

        btnDummy.Focus()
    End Sub

    '---UI Library Fixes'

    Public Sub AntiFocus() Handles pnlMiscPage.Click, pnlSettingsPage.Click, Panel38.Click
        Panel169.Visible = False
        btnDummy.Focus()
        Panel164.BackColor = Color.FromArgb(0, 0, 0)
    End Sub

    '---UI Library Functions'

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

    '---Tab Changing System

    Private Sub ChangeTab(sender As Object, e As EventArgs) Handles lblTabSel1.Click, lblTabSel3.Click

        'Hides selected tab indicator
        For Each cntrl As Control In TblTabsContainer.Controls.OfType(Of Panel)
            cntrl.Visible = False
        Next

        'Darkens all tab indicator text
        For Each lbl As Control In TblTabsContainer.Controls.OfType(Of Label)
            lbl.ForeColor = Color.FromArgb(150, 150, 150)
        Next

        'Hightlights selected tab with accent color
        sender.ForeColor = accentColor

        'Undocks all tab panels and hides them
        For Each menuscreen As Control In Panel1.Controls.OfType(Of Panel)
            menuscreen.Dock = DockStyle.None
            menuscreen.Height = 0
        Next

        'Docks the selected tab panel and accents selected tab indicator
        If sender Is lblTabSel1 Then
            pnlMainPage.Dock = DockStyle.Fill
            pnlTabHighlight1.Visible = True
        ElseIf sender Is lblTabSel2 Then

        ElseIf sender Is lblTabSel3 Then
            pnlSettingsPage.Dock = DockStyle.Fill
            pnlTabHighlight3.Visible = True
        End If
        AntiFocus()
    End Sub

    '---Change Colourisable Accents in UI

    Private Sub UpdateAccent()
        'Groupbox Topbar Color Updating
        Panel8.BackColor = accentColor
        For Each menuscreen As Control In Panel1.Controls.OfType(Of Panel)
            For Each findGroupbox As Control In menuscreen.Controls.OfType(Of TableLayoutPanel)
                If findGroupbox.Tag = "groupbox" Then 'Finds groupboxes in menu panels
                    For Each findGroupboxHeader As Control In findGroupbox.Controls.OfType(Of Panel)
                        For Each findBarTable As Control In findGroupboxHeader.Controls.OfType(Of TableLayoutPanel)
                            For Each findBarOuter As Control In findBarTable.Controls
                                For Each findBarInner As Control In findBarOuter.Controls
                                    If findBarInner.Tag = "colorise" Then
                                        findBarInner.BackColor = accentColor 'Sets top border to new accent color
                                    End If
                                Next
                            Next
                        Next
                    Next
                End If
            Next
        Next

        'Update Color Picker UI Preview
        pnlColorPicker.BackColor = accentColor

        'Toggle Control Accent Updating
        If toggle1 Then
            Panel96.BackColor = accentColor
        End If

        If toggle2 Then
            Panel134.BackColor = accentColor
        End If

        If toggle3 Then
            Panel139.BackColor = accentColor
        End If

        'Tab Highlight Accent Updating
        For Each cntrl As Control In TblTabsContainer.Controls.OfType(Of Panel)
            cntrl.BackColor = accentColor
        Next

        'Tab Label Accent Updating
        If lblTabSel1.ForeColor = Color.FromArgb(150, 150, 150) Then
        Else
            lblTabSel1.ForeColor = accentColor
        End If

        If lblTabSel3.ForeColor = Color.FromArgb(150, 150, 150) Then
        Else
            lblTabSel3.ForeColor = accentColor
        End If
    End Sub

    '---Application Code

    'Misc Tab

    'Example Toggle Switches
    Private Sub toggle1_Click(sender As Object, e As EventArgs) Handles Panel93.Click, Panel94.Click, Panel95.Click, Panel96.Click, Label3.Click
        btnDummy.Focus()
        If toggle1 Then
            Panel96.BackColor = Color.FromArgb(30, 30, 30)
            toggle1 = False
            Label3.ForeColor = Color.FromArgb(150, 150, 150)
        Else
            Panel96.BackColor = accentColor
            toggle1 = True
            Label3.ForeColor = Color.FromArgb(255, 255, 255)
            Me.Refresh()
        End If
    End Sub

    Private Sub toggle2_Click(sender As Object, e As EventArgs) Handles Panel134.Click, Panel133.Click, Panel90.Click, Label12.Click
        If toggle2 Then
            Panel134.BackColor = Color.FromArgb(30, 30, 30)
            toggle2 = False
            Label12.ForeColor = Color.FromArgb(150, 150, 150)
        Else
            Panel134.BackColor = accentColor
            toggle2 = True
            Label12.ForeColor = Color.FromArgb(255, 255, 255)
            Me.Refresh()
        End If

    End Sub

    Private Sub toggle3_Click(sender As Object, e As EventArgs) Handles Panel139.Click, Panel136.Click, Panel137.Click, Panel138.Click, Panel139.Click, Label13.Click
        If toggle3 Then
            Panel139.BackColor = Color.FromArgb(30, 30, 30)
            toggle3 = False
            Label13.ForeColor = Color.FromArgb(150, 150, 150)
        Else
            Panel139.BackColor = accentColor
            toggle3 = True
            Label13.ForeColor = Color.FromArgb(255, 255, 255)
            Me.Refresh()
        End If
    End Sub

    'Hides dropdown when another control in focus
    Private Sub DropDown_Hide(sender As Object, e As EventArgs) Handles Label24.Click
        If Panel169.Visible = False Then
            Panel169.Visible = True
        Else
            Panel169.Visible = False
        End If
    End Sub

    'Settings Tab 

    'UI Accent Colour Picker
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles pnlColorPicker.Click
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

End Class