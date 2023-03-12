Imports System.Data.OleDb
Imports Microsoft.VisualBasic.Devices

Public Class ColorPicker

    '---Init'
    Dim r As Integer
    Dim g As Integer
    Dim b As Integer
    Dim slidervalue As Integer
    Dim currentSlider As Object
    Dim hvalue As Integer = 0
    Dim svalue As Double = 0.5
    Dim lvalue As Double = 0.5
    Public Shared colorpickerlocation As Point

    '---Winforms Dragging

    'Winforms Init' 

    'Winforms Draggable Variables Init'
    Private Property MoveForm As Boolean
    Private Property MoveForm_MousePositiion As Point

    'Winforms Dragging Events
    Private Sub WindowDragging_MouseDown(sender As Object, e As MouseEventArgs) Handles tblWindow.MouseDown, pnlBackground.MouseDown, pnlGroupBoxInner.MouseDown
        If e.Button = MouseButtons.Left And Me.WindowState <> FormWindowState.Maximized Then
            MoveForm = True
            Me.Cursor = Cursors.Default
            MoveForm_MousePositiion = e.Location
        End If
    End Sub

    Private Sub WindowDragging_MouseUp(sender As Object, e As MouseEventArgs) Handles tblWindow.MouseUp, pnlBackground.MouseUp, pnlGroupBoxInner.MouseUp
        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub WindowDragging_MouseMove(sender As Object, e As MouseEventArgs) Handles tblWindow.MouseMove, pnlBackground.MouseMove, pnlGroupBoxInner.MouseMove
        If MoveForm Then
            Me.Location += (e.Location - MoveForm_MousePositiion)
        End If
    End Sub

    '---Aplication Code

    'User Auth Button
    Private Sub AuthUser(sender As Object, e As EventArgs)
    End Sub

    'Titlebar Button Events'

    'Exit Program
    Private Sub WindowExit(sender As Object, e As EventArgs)
        Close()
    End Sub

    Private Sub SliderDragging(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown, Panel27.MouseDown, Panel57.MouseDown, Panel60.MouseDown, Panel66.MouseDown, Panel65.MouseDown

        If sender.tag = "slider" Then

            currentSlider = sender
            Timer1.Start()
        Else
            For Each ctrl As Control In sender.Controls
                If ctrl.Tag = "slider" Then
                    currentSlider = ctrl
                    Timer1.Start()
                End If
            Next
        End If
    End Sub

    Private Sub slider(sender As Object, e As MouseEventArgs) Handles Panel1.MouseUp, Panel27.MouseUp, Panel57.MouseUp, Panel60.MouseUp, Panel66.MouseUp, Panel65.MouseUp
        Timer1.Stop()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim mousePosition As Point = currentSlider.parent.PointToClient(Cursor.Position) ' get the position of the mouse cursor relative to the control
        Dim mouseX As Integer = mousePosition.X ' get the X-coordinate of the mouse cursor relative to the control
        If mouseX <= currentSlider.parent.Width Then
            currentSlider.Width = mouseX
        ElseIf mouseX <= 0 Then
            currentSlider.Width = 0
        Else
            currentSlider.Width = currentSlider.parent.Width
        End If

        If currentSlider Is Panel1 Then
            hvalue = currentSlider.Width / currentSlider.Parent.Width * 360
        ElseIf currentSlider Is Panel60 Then
            svalue = currentSlider.Width / currentSlider.Parent.Width + 0.0000000001
        Else
            lvalue = currentSlider.Width / currentSlider.Parent.Width
        End If
        Label7.Text = hvalue
        Label3.Text = Math.Round(svalue * 100, 0)
        Label9.Text = Math.Round(lvalue * 100, 0)
        HlsToRgb(hvalue, lvalue, svalue)
        MainProgram.accentColor = Color.FromArgb(r, g, b)
        MainProgram.UpdateAccent()
    End Sub

    ' Convert an HLS value into an RGB value.
    Private Sub HlsToRgb(ByVal H As Double, ByVal L As Double,
        ByVal S As Double)
        Dim p1 As Double
        Dim p2 As Double

        If L <= 0.5 Then
            p2 = L * (1 + S)
        Else
            p2 = L + S - L * S
        End If
        p1 = 2 * L - p2
        If S = 0 Then
            r = L
            g = L
            b = L
        Else
            r = QqhToRgb(p1, p2, H + 120) * 255
            g = QqhToRgb(p1, p2, H) * 255
            b = QqhToRgb(p1, p2, H - 120) * 255
        End If
    End Sub

    Private Function QqhToRgb(ByVal q1 As Double, ByVal q2 As _
        Double, ByVal hue As Double) As Double
        If hue > 360 Then
            hue = hue - 360
        ElseIf hue < 0 Then
            hue = hue + 360
        End If
        If hue < 60 Then
            QqhToRgb = q1 + (q2 - q1) * hue / 60
        ElseIf hue < 180 Then
            QqhToRgb = q2
        ElseIf hue < 240 Then
            QqhToRgb = q1 + (q2 - q1) * (240 - hue) / 60
        Else
            QqhToRgb = q1
        End If
    End Function

    ' Convert an RGB value into an HLS value.
    ' Convert an RGB value into an HLS value.
    Private Sub RgbToHls(ByVal R As Double, ByVal G As Double,
    ByVal B As Double)
        R = R / 255
        G = G / 255
        B = B / 255
        Dim max As Double
        Dim min As Double
        Dim diff As Double
        Dim r_dist As Double
        Dim g_dist As Double
        Dim b_dist As Double

        ' Get the maximum and minimum RGB components.
        max = R
        If max < G Then max = G
        If max < B Then max = B

        min = R
        If min > G Then min = G
        If min > B Then min = B

        diff = max - min
        lvalue = (max + min) / 2
        If Math.Abs(diff) < 0.00001 Then
            svalue = 0.0000000001
            hvalue = 0   ' H is really undefined.
        Else
            If lvalue <= 0.5 Then
                svalue = diff / (max + min)
            Else
                svalue = diff / (2 - max - min)
            End If

            r_dist = (max - R) / diff
            g_dist = (max - G) / diff
            b_dist = (max - B) / diff

            If R = max Then
                hvalue = b_dist - g_dist
            ElseIf G = max Then
                hvalue = 2 + r_dist - b_dist
            Else
                hvalue = 4 + g_dist - r_dist
            End If

            hvalue = hvalue * 60
            If hvalue < 0 Then hvalue = hvalue + 360
        End If
    End Sub

    Private Sub Form1_LostFocus(sender As Object, e As System.EventArgs) Handles Me.LostFocus
        Me.Close()
    End Sub

    Private Sub ColorPicker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Location = New Point(colorpickerlocation.X - Me.Width + 31, colorpickerlocation.Y + 15)
        RgbToHls(MainProgram.accentColor.R, MainProgram.accentColor.G, MainProgram.accentColor.B)
        Label7.Text = hvalue
        Label3.Text = Math.Round(svalue * 100, 0)
        Label9.Text = Math.Round(lvalue * 100, 0)
        Panel1.Width = hvalue / 360 * Panel27.Width
        Panel60.Width = svalue * Panel57.Width
        Panel66.Width = lvalue & Panel65.Width
    End Sub
End Class
