﻿Imports System.Drawing
Imports System.Security.Cryptography
Imports System.Data.OleDb
Imports System.Web.UI.WebControls.Expressions
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Web

Public Class AuthLogin

    '---Init'

    'Load Usernames
    Private Sub loadUsernames()
        Dim conn As New OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=C:\Users\nicks\Downloads\POS System\JansUILib\JansUILib\UserData.accdb")
        conn.Open()
        Dim cmd As New OleDbCommand("SELECT Username FROM UserAuth", conn)
        Dim myReader As OleDbDataReader = cmd.ExecuteReader
        CbxUsername.Items.Clear()
        While myReader.Read
            CbxUsername.Items.Add(myReader("Username"))
        End While
        conn.Close()
    End Sub

    'Authenticates the User
    Private Function authUser(ByVal username As String, ByVal password As String)
        Dim conn As New OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=C:\Users\nicks\Downloads\POS System\JansUILib\JansUILib\UserData.accdb")
        conn.Open()
        Dim cmdInput As String = "SELECT PIN FROM UserAuth WHERE (Username='" & username & "')"
        Dim storedPassword As String = ""
        Dim cmd As New OleDbCommand(cmdInput, conn)
        Dim myReader As OleDbDataReader = cmd.ExecuteReader
        While myReader.Read()
            storedPassword = myReader("PIN").ToString
        End While
        If password = storedPassword And storedPassword <> "" Then
            Return True 'Returns true if combination of username and password is correct
        Else
            Return False 'Returns false if combination is inccorrect or fields are empty
        End If
        conn.Close()
    End Function

  

    '---Winforms Dragging

    'Winforms Init' 
    Private Sub UserLogin_OnLoad(ByVal qsender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadUsernames()
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
        If CbxUsername.Text = "admin" And authUser(CbxUsername.Text, TbxPassword.Text) Then
            AdminPanel.Show()
        ElseIf authUser(CbxUsername.Text, TbxPassword.Text) Then
            POSSystem.currentUser = CbxUsername.Text
            POSSystem.Show()
        End If
        If authUser(CbxUsername.Text, TbxPassword.Text) Then
            authUser(CbxUsername.Text, TbxPassword.Text)
            Me.Hide()
        End If
    End Sub

    'Titlebar Button Events'

    'Exit Program
    Private Sub WindowExit(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub

End Class
