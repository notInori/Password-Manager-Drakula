Module WindowMouseHandling

    Private Property MoveForm As Boolean
    Private Property MoveForm_MousePositiion As Point

    Public Sub HandleMouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
        If e.Button = MouseButtons.Left And sender.WindowState <> FormWindowState.Maximized Then
            MoveForm = True
            sender.Cursor = Cursors.Default
            MoveForm_MousePositiion = e.Location
        End If
    End Sub

    Public Sub HandleMouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            MoveForm = False
            sender.Cursor = Cursors.Default
        End If
    End Sub

    Public Sub HandleMouseMove(sender As Object, e As MouseEventArgs)
        If MoveForm Then
            sender.Location += (e.Location - MoveForm_MousePositiion)
        End If
    End Sub
End Module
