﻿Imports System.Data.OleDb
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class AuthLogin

    Dim database As New DatabaseUtil
    Dim Authorisation As New Authorisation(Nothing, Nothing)
    Public Shared MainProgram
    '---Init'

    '---Setting Database Path

    'Database Connection Variables

    'Database Path Location
    Private ReadOnly localUserDataPath As String = ".\UserData.accdb"
    'Create Global Connection String For User Data
    Public conn As New OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=" & localUserDataPath)
    'Init Reader
    Dim myReader As OleDbDataReader

    '---Database Functions

    'Read From Database

    'Load Usernames
    Public Sub LoadUsernames()
        'CbxUsername.Items.Clear()
        Dim Usernames As Object() = database.SqlReadColumn("SELECT Username FROM UserAuth")
        For Each item As String In Usernames
            CbxUsername.Items.Add(item)
        Next
        CbxUsername.SelectedIndex = 0
    End Sub

    '---Functions

    'MD5 Hash Algorithm 
    'https://stackoverflow.com/questions/34637059/equivalent-password-hash-function-for-vb-net

    Public Function MD5(ByVal pass As String) As String
        Try
            Dim MD5p As New System.Security.Cryptography.MD5CryptoServiceProvider
            Dim baytlar As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(pass)
            Dim hash As Byte() = MD5p.ComputeHash(baytlar)
            Dim kapasite As Integer = (hash.Length * 2 + (hash.Length / 8))
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder(kapasite)
            Dim I As Integer
            For I = 0 To hash.Length - 1
                sb.Append(BitConverter.ToString(hash, I, 1))
            Next I
            Return sb.ToString().TrimEnd(New Char() {" "c})
        Catch ex As Exception
            Return "0"
        End Try
    End Function

    '---Winforms Dragging

    'Winforms Init' 
    Private Sub UserLogin_OnLoad(ByVal qsender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = 0
        Me.Width = 0
        LoadUsernames()
        'lblCurrentVersion.Text = MainProgram.versionNumber
        'lblShopName.Text = MainProgram.programName
        pnlWindowContents.Dock = DockStyle.Fill
        pnlWindowContents.BringToFront()
        Me.TopMost = True
        Me.Focus()
        tmrAnimation.Start()
    End Sub

    '---Winforms Init

    'Winforms Dragging Events
    Private Sub WindowDragging_MouseDown(sender As Object, e As MouseEventArgs) Handles lblTitle.MouseDown
        WindowMouseHandling.HandleMouseDown(Me, e)
    End Sub

    Private Sub WindowDragging_MouseUp(sender As Object, e As MouseEventArgs) Handles lblTitle.MouseUp
        WindowMouseHandling.HandleMouseUp(Me, e)
    End Sub

    Private Sub WindowDragging_MouseMove(sender As Object, e As MouseEventArgs) Handles lblTitle.MouseMove
        WindowMouseHandling.HandleMouseMove(Me, e)
    End Sub

    '---Aplication Code

    'User Auth Button
    Private Sub AuthUser(sender As Object, e As EventArgs) Handles btnLogin.Click
        Authorisation.Username = CbxUsername.Text
        Authorisation.Password = TbxPassword.Text
        If Authorisation.AuthUser Then
            MainProgram = New MainProgram(Authorisation.Username, Authorisation.Password)
            MainProgram.Show()
            CbxUsername.ResetText()
            TbxPassword.Clear()
            Me.Hide()
        Else
            pnlNotification.Dock = DockStyle.Fill
            pnlNotification.BringToFront()
        End If

    End Sub

    'Dismiss Notification Button
    Private Sub DimissNotification(sender As Object, e As EventArgs) Handles BtnContinue.Click
        pnlNotification.Dock = DockStyle.None
        pnlNotification.Height = 0
    End Sub

    'Titlebar Button Events'

    'Exit Program
    Private Sub WindowExit(sender As Object, e As EventArgs) Handles btnExit.Click
        conn.Close()
        Application.Exit()
    End Sub

    '---Animations

    'Window Opening Animation
    Private Sub tmrAnimation_Tick(sender As Object, e As EventArgs) Handles tmrAnimation.Tick
        While Me.Width < 500
            Me.Width += 10
        End While
        Me.Width = 500
        If Me.Height < 250 Then
            Me.Height += 20
        ElseIf Me.Height < 300 Then
            Me.Height += 5
        ElseIf Me.Height < 309 Then
            Me.Height += 2
        Else
            Me.Height = 310
            tmrAnimation.Stop()
            Me.TopMost = False
        End If
        Application.DoEvents()
    End Sub

End Class
