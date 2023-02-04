Imports System.Drawing

Public Class AuthLogin

    'Init'

    'Variable Init'

    Private Property MoveForm As Boolean
    Private Property MoveForm_MousePositiion As Point

    'Winforms Init' 
    Private Sub UserLogin_OnLoad(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnDummy.Focus()
    End Sub

    'Winforms Borderless Dragging'

    Private Sub WindowDragging_MouseDown(sender As Object, e As MouseEventArgs) Handles tblWindow.MouseDown, pnlBackground.MouseDown, pnlWindowContents.MouseDown, pnlGroupBoxInner.MouseDown, pnlGroupUsernameTextbox.MouseDown, lblUsername.MouseDown
        btnDummy.Focus()
        If e.Button = MouseButtons.Left And Me.WindowState <> FormWindowState.Maximized Then
            MoveForm = True
            Me.Cursor = Cursors.Default
            MoveForm_MousePositiion = e.Location
        End If
    End Sub

    Private Sub WindowDragging_MouseUp(sender As Object, e As MouseEventArgs) Handles tblWindow.MouseUp, pnlBackground.MouseUp, pnlWindowContents.MouseUp, pnlGroupBoxInner.MouseUp, pnlGroupUsernameTextbox.MouseUp, lblUsername.MouseUp
        btnDummy.Focus()
        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub WindowDragging_MouseMove(sender As Object, e As MouseEventArgs) Handles tblWindow.MouseMove, pnlBackground.MouseMove, pnlWindowContents.MouseMove, pnlGroupBoxInner.MouseMove, pnlGroupUsernameTextbox.MouseMove, lblUsername.MouseMove
        If MoveForm Then
            Me.Location += (e.Location - MoveForm_MousePositiion)
        End If
    End Sub

    Private Sub LoseFocus() Handles pnlGroupBoxInner.MouseClick, BtnLogin.Click, pnlGroupUsernameTextbox.Click, lblUsername.Click
        btnDummy.Focus()
    End Sub
    Private Sub AuthUser(sender As Object, e As EventArgs) Handles btnLogin.Click
        LoseFocus()
        POSSystem.Show()
        Me.Hide()
    End Sub

    'Titlebar Button Events'

    Private Sub WindowExit(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub


    'Application Code'

End Class
