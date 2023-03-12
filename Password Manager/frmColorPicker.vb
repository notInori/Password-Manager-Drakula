﻿Imports System.Data.OleDb
Imports System.Runtime.Remoting
Imports Microsoft.VisualBasic.Devices

Public Class ColorPicker

    '---Init'
    Dim r As Integer = 255
    Dim g As Integer = 255
    Dim b As Integer = 255
    ReadOnly slidervalue As Integer
    Dim currentSlider As Object
    Dim hvalue As Integer = 0
    Dim svalue As Double = 0.5
    Dim lvalue As Double = 0.5

    Public Shared colorpickerlocation As Point

    'Winforms Init' 

    Private Sub ColorPicker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Location = New Point(colorpickerlocation.X - Me.Width + 31, colorpickerlocation.Y + 15) 'Set location relative to main program colorpicker control
        RgbToHls(MainProgram.accentColor.R, MainProgram.accentColor.G, MainProgram.accentColor.B) 'Converts RGB back to HSL to set sliders

        'Update Labels of Sliders
        Label7.Text = hvalue
        Label3.Text = Math.Round(svalue * 100, 0)
        Label9.Text = Math.Round(lvalue * 100, 0)

        'Sets Slider Positions
        Panel1.Width = hvalue / 360 * Panel27.Width
        Panel60.Width = svalue * Panel57.Width
        Panel66.Width = lvalue * Panel65.Width
    End Sub

    'Functions

    '---RGB <=> HSL conversion Function
    'http://www.vb-helper.com/howto_rgb_to_hls.html

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
            hue -= -360
        ElseIf hue < 0 Then
            hue += 360
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
    Private Sub RgbToHls(ByVal R As Double, ByVal G As Double,
    ByVal B As Double)
        R /= 255
        G /= 255
        B /= 255
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

            hvalue *= 60
            If hvalue < 0 Then hvalue += 360
        End If
    End Sub

    '---Slider Code

    'Detects when slider is held down
    Private Sub SliderDragging(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown, Panel27.MouseDown, Panel57.MouseDown, Panel60.MouseDown, Panel66.MouseDown, Panel65.MouseDown
        'Locate Slider Panel
        If sender.tag = "slider" Then 'If slider is clicked
            currentSlider = sender
            Timer1.Start()
        Else
            For Each ctrl As Control In sender.Controls 'If slider parent/slider track is clicked
                If ctrl.Tag = "slider" Then
                    currentSlider = ctrl
                    Timer1.Start()
                End If
            Next
        End If
    End Sub

    'Detects When Slider Released
    Private Sub StopSlider(sender As Object, e As MouseEventArgs) Handles Panel1.MouseUp, Panel27.MouseUp, Panel57.MouseUp, Panel60.MouseUp, Panel66.MouseUp, Panel65.MouseUp
        Timer1.Stop()
    End Sub

    'Updates Slider Progress
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim mousePosition As Point = currentSlider.parent.PointToClient(Cursor.Position) ' Get the position of the mouse cursor relative to the control
        Dim mouseX As Integer = mousePosition.X ' get the X-coordinate of the mouse cursor relative to the control

        'Calcuate Percentage and Prevent Out of Bound Values
        If mouseX <= currentSlider.parent.Width Then
            currentSlider.Width = mouseX
        ElseIf mouseX <= 0 Then
            currentSlider.Width = 0
        Else
            currentSlider.Width = currentSlider.parent.Width
        End If

        'Detect Slide Being Used
        If currentSlider Is Panel1 Then
            hvalue = currentSlider.Width / currentSlider.Parent.Width * 360
        ElseIf currentSlider Is Panel60 Then
            svalue = currentSlider.Width / currentSlider.Parent.Width + 0.0000000001
        Else
            lvalue = currentSlider.Width / currentSlider.Parent.Width
        End If

        'Update Slide Labels
        Label7.Text = hvalue
        Label3.Text = Math.Round(svalue * 100, 0)
        Label9.Text = Math.Round(lvalue * 100, 0)

        'Calculate RGB value from HSL and Pass To Main Program
        HlsToRgb(hvalue, lvalue, svalue)
        MainProgram.accentColor = Color.FromArgb(r, g, b)
        MainProgram.UpdateAccent()
    End Sub

    Public Sub updateAccent()
        'Panel1.BackColor = MainProgram.accentColor
        'Panel60.BackColor = MainProgram.accentColor
        'Panel66.BackColor = MainProgram.accentColor
    End Sub

    'Auto Hide Colour Picker When Focus Lost
    Private Sub Form1_LostFocus(sender As Object, e As System.EventArgs) Handles Me.LostFocus
        Me.Close()
    End Sub

End Class
