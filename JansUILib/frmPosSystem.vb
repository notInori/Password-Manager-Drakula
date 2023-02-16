Imports System.Drawing

Public Class POSSystem

    'Init'

    'Variable Init'

    Private Property MoveForm As Boolean
    Private Property Fullscreen
    Private Property Maxscreen
    Private Property MoveForm_MousePositiion As Point
    Public Shared accentColor As Color = Color.FromArgb(255, 255, 255)
    Dim cDialog As New ColorDialog()

    Public toggle1 As Boolean = False
    Public toggle2 As Boolean = False
    Public toggle3 As Boolean = False
    Public toggle4 As Boolean = False

    Const ImaginaryBorderSize As Integer = 16
    Private Const HTLEFT As Integer = 10, HTRIGHT As Integer = 11, HTTOP As Integer = 12, HTTOPLEFT As Integer = 13, HTTOPRIGHT As Integer = 14, HTBOTTOM As Integer = 15, HTBOTTOMLEFT As Integer = 16, HTBOTTOMRIGHT As Integer = 17

    'Winforms Init' 
    Private Sub TestApp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.None
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        btnDummy.Focus()
        sel1.Width = 0
        sel2.Width = 0
        sel3.Width = 0
        ChangeTab(Label7, e)
        Panel169.Visible = False


    End Sub

    'Winforms Borderless Dragging'

    'Resizeable Window Init'

    'Titlebar Button Events'

    Private Sub btnExit_Click(sender As Object, e As EventArgs)
        AuthLogin.Close()
    End Sub



    'UI Library Fixes'

    Private Sub input_DoubleClick(sender As Object, e As EventArgs) Handles input.DoubleClick
        input.Text = ""
    End Sub

    Public Sub AntiFocus() Handles ListBox1.Click, pnlMiscPage.Click, pnlSettingsPage.Click, Panel79.Click, Panel224.Click, Panel38.Click, Panel36.Click, Label28.Click, Label4.Click
        Panel169.Visible = False
        btnDummy.Focus()
        Panel33.BackColor = Color.FromArgb(0, 0, 0)
        Panel164.BackColor = Color.FromArgb(0, 0, 0)

    End Sub


    'UI Library Functions'

    Private Sub input_MouseEnter(sender As Object, e As EventArgs) Handles input.MouseEnter, Panel35.MouseEnter, Panel34.MouseEnter, Panel33.MouseEnter, Panel32.MouseEnter, btnButton.MouseEnter, Button1.MouseEnter, btnClear.MouseEnter, ListBox1.MouseEnter, Button3.MouseEnter, Panel93.MouseEnter, Panel94.MouseEnter, Panel95.MouseEnter, Panel96.MouseEnter, Label3.MouseEnter

        If sender Is input Or sender Is Panel35 Or sender Is Panel34 Or sender Is Panel33 Or sender Is Panel32 Then
            Panel33.BackColor = accentColor
        ElseIf sender Is btnButton Then
            Panel4.BackColor = accentColor
        ElseIf sender Is Button1 Then
            Panel54.BackColor = accentColor
        ElseIf sender Is btnClear Then
            Panel51.BackColor = accentColor
        ElseIf sender Is ListBox1 Or sender Is Panel58 Or sender Is Panel57 Then
            Panel56.BackColor = accentColor
        ElseIf sender Is Button3 Then
            Panel80.BackColor = accentColor
        ElseIf sender Is Panel93 Or sender Is Panel94 Or sender Is Panel95 Or sender Is Panel96 Or sender Is Label3 Then
            Panel93.BackColor = accentColor

        End If
    End Sub

    Private Sub input_MouseLeave(sender As Object, e As EventArgs) Handles input.MouseLeave, Panel35.MouseLeave, Panel34.MouseLeave, Panel33.MouseLeave, Panel32.MouseLeave, btnButton.MouseLeave, Button1.MouseLeave, btnClear.MouseLeave, ListBox1.MouseLeave, Button3.MouseLeave, Panel93.MouseLeave, Panel94.MouseLeave, Panel95.MouseLeave, Panel96.MouseLeave, Label3.MouseLeave
        If sender Is input Or sender Is Panel35 Or sender Is Panel34 Or sender Is Panel33 Or sender Is Panel32 Then
            If input.Focused() Then
            Else
                Panel33.BackColor = Color.FromArgb(0, 0, 0)
            End If
        ElseIf sender Is btnButton Then
            Panel4.BackColor = Color.FromArgb(0, 0, 0)
        ElseIf sender Is Button1 Then
            Panel54.BackColor = Color.FromArgb(0, 0, 0)
        ElseIf sender Is btnClear Then
            Panel51.BackColor = Color.FromArgb(0, 0, 0)
        ElseIf sender Is ListBox1 Or sender Is Panel58 Or sender Is Panel57 Then
            Panel56.BackColor = Color.FromArgb(0, 0, 0)
        ElseIf sender Is Button3 Then
            Panel80.BackColor = Color.FromArgb(0, 0, 0)
        ElseIf sender Is Panel93 Or sender Is Panel94 Or sender Is Panel95 Or sender Is Panel96 Or sender Is Label3 Then
            Panel93.BackColor = Color.FromArgb(0, 0, 0)
        End If
    End Sub

    Private Sub panel_MouseEnter(sender As Object, e As EventArgs) Handles Panel164.MouseEnter, Panel165.MouseEnter, Panel166.MouseEnter, Panel167.MouseEnter, Panel134.MouseEnter, Panel133.MouseEnter, Panel91.MouseEnter, Panel90.MouseEnter, Label12.MouseEnter, Panel136.MouseEnter, Panel137.MouseEnter, Panel138.MouseEnter, Panel139.MouseEnter, Label13.MouseEnter, Label24.MouseEnter, Panel225.MouseEnter, Panel226.MouseEnter, Panel227.MouseEnter, Button4.MouseEnter

        If sender Is Panel164 Or sender Is Panel165 Or sender Is Panel166 Or sender Is Panel167 Or sender Is Label24 Then
            Panel164.BackColor = accentColor

        ElseIf sender Is Panel134 Or sender Is Panel133 Or sender Is Panel90 Or sender Is Panel91 Or sender Is Label12 Then
            Panel90.BackColor = accentColor

        ElseIf sender Is Panel136 Or sender Is Panel137 Or sender Is Panel138 Or sender Is Panel139 Or sender Is Label13 Then
            Panel136.BackColor = accentColor
        ElseIf sender Is Button4 Then
            Panel225.BackColor = accentColor
        End If
    End Sub

    Private Sub panel_MouseLeave(sender As Object, e As EventArgs) Handles Panel164.MouseLeave, Panel165.MouseLeave, Panel166.MouseLeave, Panel167.MouseLeave, Panel134.MouseLeave, Panel133.MouseLeave, Panel91.MouseLeave, Panel90.MouseLeave, Label12.MouseLeave, Panel136.MouseLeave, Panel137.MouseLeave, Panel138.MouseLeave, Panel139.MouseLeave, Label13.MouseLeave, Label24.MouseLeave, Panel225.MouseLeave, Panel226.MouseLeave, Panel227.MouseLeave, Button4.MouseLeave

        If sender Is Panel164 Or sender Is Panel165 Or sender Is Panel166 Or sender Is Panel167 Or sender Is Label24 Then
            If Panel169.Visible = False Then
                Panel164.BackColor = Color.FromArgb(0, 0, 0)
            End If
        ElseIf sender Is Panel134 Or sender Is Panel133 Or sender Is Panel90 Or sender Is Label12 Then
            Panel90.BackColor = Color.FromArgb(0, 0, 0)
        ElseIf sender Is Panel136 Or sender Is Panel137 Or sender Is Panel138 Or sender Is Panel139 Or sender Is Label13 Then
            Panel136.BackColor = Color.FromArgb(0, 0, 0)
        ElseIf sender Is Button4 Then
            Panel225.BackColor = Color.FromArgb(0, 0, 0)
        End If
    End Sub




    Private Sub ChangeTab(sender As Object, e As EventArgs) Handles Label7.Click, Label8.Click, Label9.Click
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


    'Updates the accent colour in the UI

    Private Sub updateAccent()
        Panel8.BackColor = accentColor
        Panel25.BackColor = accentColor
        Panel26.BackColor = accentColor
        Panel68.BackColor = accentColor
        Panel75.BackColor = accentColor
        Panel104.BackColor = accentColor
        Panel111.BackColor = accentColor
        Panel46.BackColor = accentColor
        Panel39.BackColor = accentColor
        Panel309.BackColor = accentColor
        Panel300.BackColor = accentColor
        Panel179.BackColor = accentColor
        Panel186.BackColor = accentColor
        Panel213.BackColor = accentColor
        Panel220.BackColor = accentColor
        Panel194.BackColor = accentColor
        Panel201.BackColor = accentColor
        Panel264.BackColor = accentColor
        Panel256.BackColor = accentColor
        Panel287.BackColor = accentColor
        Panel294.BackColor = accentColor

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



    'Application Code
    Private Sub btnButton_Click(sender As Object, e As EventArgs) Handles btnButton.Click
        AntiFocus()
        If ListBox1.Items.Contains(input.Text) Or input.TextLength < 1 Then
        Else
            ListBox1.Items.Add(input.Text)
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        AntiFocus()
        ListBox1.Items.Clear()
    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs)
        Panel169.Visible = True
    End Sub

    Private Sub Label24_Click(sender As Object, e As EventArgs) Handles Label24.Click
        If Panel169.Visible = False Then
            Panel169.Visible = True
        Else
            Panel169.Visible = False
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        AntiFocus()
        Me.Hide()
        AuthLogin.Show()
    End Sub

    'Keeps watermark updated
    Private Sub tmrMain_Tick(sender As Object, e As EventArgs) Handles tmrMain.Tick
        lblTitle.Text = "POS SYSTEM | [BUILD] | [USER] | " & DateTime.Now.ToString("HH:mm:ss") & " | " & DateTime.Now.ToString("dd MMM. yyyy")
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.GetItemText(ListBox1.SelectedItem) IsNot "" Then
            input.Text = ListBox1.GetItemText(ListBox1.SelectedItem)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AntiFocus()
        ListBox1.Items.Remove(input.Text)
    End Sub

    'UI Accent Colour Picker
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        AntiFocus()
        If (cDialog.ShowDialog() = DialogResult.OK) Then
            accentColor = cDialog.Color ' update with user selected color.
        End If
        updateAccent()
    End Sub

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

    Friend Class UserDataDataSet
    End Class

    Friend Class UserDataDataSetTableAdapters
    End Class
End Class
