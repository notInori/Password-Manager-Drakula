Imports System.Drawing
Imports System.Security.Cryptography

Public Class AuthLogin

    '---Init'

    'Load Usernames
    Private Function getUsernames() As DataTable
        Dim dtUsers As New DataTable
        Return dtUsers
    End Function

    '---Winforms Dragging

    'Winforms Init' 
    Private Sub UserLogin_OnLoad(ByVal qsender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.DataSource = getUsernames() 'Set usernames selector datasource to usernames stored in database
        lblCurrentVersion.Text = POSSystem.versionNumber
        lblShopName.Text = POSSystem.businessName
        Antifocus()
    End Sub

    'Winforms Variable Init'

    Private Property MoveForm As Boolean
    Private Property MoveForm_MousePositiion As Point

    'Winforms Dragging Events
    Private Sub WindowDragging_MouseDown(sender As Object, e As MouseEventArgs) Handles tblWindow.MouseDown, pnlBackground.MouseDown, pnlWindowContents.MouseDown, pnlGroupBoxInner.MouseDown, pnlGroupUsernameTextbox.MouseDown, lblUsername.MouseDown, TableLayoutPanel2.MouseDown, TableLayoutPanel1.MouseDown, lblTitle.MouseDown, Panel5.MouseDown, Panel314.MouseDown, lblShopName.MouseDown, Label33.MouseDown, Label2.MouseDown, lblCurrentVersion.MouseDown
        btnDummy.Focus()
        If e.Button = MouseButtons.Left And Me.WindowState <> FormWindowState.Maximized Then
            MoveForm = True
            Me.Cursor = Cursors.Default
            MoveForm_MousePositiion = e.Location
        End If
    End Sub

    Private Sub WindowDragging_MouseUp(sender As Object, e As MouseEventArgs) Handles tblWindow.MouseUp, pnlBackground.MouseUp, pnlWindowContents.MouseUp, pnlGroupBoxInner.MouseUp, pnlGroupUsernameTextbox.MouseUp, lblUsername.MouseUp, TableLayoutPanel2.MouseUp, TableLayoutPanel1.MouseUp, lblTitle.MouseUp, Panel5.MouseUp, Panel314.MouseUp, lblShopName.MouseUp, Label33.MouseUp, Label2.MouseUp, lblCurrentVersion.MouseUp
        btnDummy.Focus()
        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub WindowDragging_MouseMove(sender As Object, e As MouseEventArgs) Handles tblWindow.MouseMove, pnlBackground.MouseMove, pnlWindowContents.MouseMove, pnlGroupBoxInner.MouseMove, pnlGroupUsernameTextbox.MouseMove, lblUsername.MouseMove, TableLayoutPanel2.MouseMove, TableLayoutPanel1.MouseMove, lblTitle.MouseMove, Panel5.MouseMove, Panel314.MouseMove, lblShopName.MouseMove, Label33.MouseMove, Label2.MouseMove, lblCurrentVersion.MouseMove
        If MoveForm Then
            Me.Location += (e.Location - MoveForm_MousePositiion)
        End If
    End Sub

    '---UI Library Fixes'

    'Remove Highlight Box
    Private Sub Antifocus() Handles pnlGroupBoxInner.MouseClick, btnLogin.Click, pnlGroupUsernameTextbox.Click, lblUsername.Click
        btnDummy.Focus()
    End Sub

    '---Aplication Code

    'User Auth Button
    Private Sub AuthUser(sender As Object, e As EventArgs) Handles btnLogin.Click
        If LCase(ComboBox1.Text) = "admin" Then
            AdminPanel.Show()
        Else
            POSSystem.currentUser = ComboBox1.Text
            POSSystem.Show()
        End If
        Antifocus()
        ComboBox1.Text = ""
        Me.Hide()
    End Sub

    'Titlebar Button Events'

    'Exit Program
    Private Sub WindowExit(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub

End Class
