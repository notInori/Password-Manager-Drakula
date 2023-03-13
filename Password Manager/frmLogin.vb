﻿Imports System.Data.OleDb

Public Class AuthLogin

    '---Init'

    '---Setting Database Path

    'Database Connection Variables
    ReadOnly conn As New OleDbConnection(UserDataConnectionString)
    Dim myReader As OleDbDataReader
    'Database Path Location
    Public Shared localUserDataPath As String = ".\UserData.accdb"
    'Create Global Connection String For User Data
    Public Shared UserDataConnectionString As String = "Provider=Microsoft.Ace.Oledb.12.0;Data Source=" & AuthLogin.localUserDataPath

    '---Database Functions

    'Read From Database
    Public Function SqlReadValue(command As String)
        Dim cmd As New OleDbCommand(command, conn)
        myReader = cmd.ExecuteReader
        While myReader.Read()
            Return myReader.GetValue(0)
        End While
        Return Nothing
    End Function

    'Load Usernames
    Public Sub LoadUsernames()
        conn.Open()
        CbxUsername.Items.Clear()
        Dim cmd As New OleDbCommand("SELECT Username FROM UserAuth", conn)
        myReader = cmd.ExecuteReader
        While myReader.Read
            CbxUsername.Items.Add(myReader("Username"))
            CbxUsername.SelectedText = myReader(0)
        End While
    End Sub

    'Authenticates the User
    Private Function AuthUser(ByVal username As String, ByVal password As String)
        Dim storedPassword = SqlReadValue("SELECT PIN FROM UserAuth WHERE (Username='" & username & "')")
        If password = CStr(storedPassword) And CStr(storedPassword) <> "" Then
            Return True 'Returns true if combination of username and password is correct
        Else
            Return False 'Returns false if combination is inccorrect or fields are empty
        End If
    End Function

    '---Functions 

    'Wait Function Without Application Freeze
    'https://stackoverflow.com/questions/15857893/wait-5-seconds-before-continuing-code-vb-net
    Private Sub wait(ByVal seconds As Integer)
        For i As Integer = 0 To seconds * 100
            System.Threading.Thread.Sleep(10)
            Application.DoEvents()
        Next
    End Sub

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
        lblCurrentVersion.Text = MainProgram.versionNumber
        lblShopName.Text = MainProgram.programName
        pnlWindowContents.Dock = DockStyle.Fill
        pnlWindowContents.BringToFront()
        tmrAnimation.Start()
    End Sub

    'Winforms Draggable Variables Init'
    Private Property MoveForm As Boolean
    Private Property MoveForm_MousePositiion As Point

    'Winforms Dragging Events
    Private Sub WindowDragging_MouseDown(sender As Object, e As MouseEventArgs) Handles tblWindow.MouseDown, pnlBackground.MouseDown, pnlWindowContents.MouseDown, pnlGroupBoxInner.MouseDown, pnlGroupUsernameTextbox.MouseDown, lblUsername.MouseDown, TableLayoutPanel2.MouseDown, TableLayoutPanel1.MouseDown, lblTitle.MouseDown, Panel5.MouseDown, Panel314.MouseDown, lblShopName.MouseDown, Label33.MouseDown, Label2.MouseDown, lblCurrentVersion.MouseDown
        If e.Button = MouseButtons.Left And Me.WindowState <> FormWindowState.Maximized Then
            MoveForm = True
            Me.Cursor = Cursors.Default
            MoveForm_MousePositiion = e.Location
        End If
    End Sub

    Private Sub WindowDragging_MouseUp(sender As Object, e As MouseEventArgs) Handles tblWindow.MouseUp, pnlBackground.MouseUp, pnlWindowContents.MouseUp, pnlGroupBoxInner.MouseUp, pnlGroupUsernameTextbox.MouseUp, lblUsername.MouseUp, TableLayoutPanel2.MouseUp, TableLayoutPanel1.MouseUp, lblTitle.MouseUp, Panel5.MouseUp, Panel314.MouseUp, lblShopName.MouseUp, Label33.MouseUp, Label2.MouseUp, lblCurrentVersion.MouseUp
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

    '---Aplication Code

    'User Auth Button
    Private Sub AuthUser(sender As Object, e As EventArgs) Handles btnLogin.Click
        If AuthUser(CbxUsername.Text, MD5(TbxPassword.Text)) Then
            MainProgram.currentUser = CbxUsername.Text
            MainProgram.Show()
        Else
            pnlNotification.Dock = DockStyle.Fill
            pnlNotification.BringToFront()
        End If
        If AuthUser(CbxUsername.Text, MD5(TbxPassword.Text)) Then
            MainProgram.localPassword = TbxPassword.Text
            CbxUsername.Text = ""
            TbxPassword.Text = ""
            Me.Hide()
            conn.Close()
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
        Close()
    End Sub

    Private Sub tmrAnimation_Tick(sender As Object, e As EventArgs) Handles tmrAnimation.Tick
        Me.TopMost = True
        Me.Focus()
        While Me.Width < 500
            Me.Width += 10
            Application.DoEvents()
        End While
        While Me.Height < 275
            Me.Height += 10
            Application.DoEvents()
        End While
        While Me.Height < 300
            Me.Height += 2
            Application.DoEvents()
        End While
        While Me.Height < 309
            Me.Height += 1
            Application.DoEvents()
        End While
        Me.Height = 310
        tmrAnimation.Stop()
        Me.TopMost = False
    End Sub

End Class
