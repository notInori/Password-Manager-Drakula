Imports System.Data.OleDb
Imports System.Security.Cryptography

Public Class MainProgram

    '---Init'

    Dim database As New DatabaseUtil
    Dim Authorisation As New Authorisation(Nothing, Nothing)

    'Client Info Config
    Public ReadOnly programName As String = "Password Manager"
    Public ReadOnly versionNumber As String = "[Dev Build]"

    'Client Info Variables
    Public currentUser As String = "[USER]"

    'Variables Init'
    Public accentColor As Color = Color.FromArgb(255, 255, 255)
    Dim selectedUID As New Integer
    Public localPassword As String

    'Variables Init'
    Dim data As Integer

    'Database Variables Init
    Dim myReader As OleDbDataReader

    '---Winforms Init' 

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

    '---Resizable Windows 

    'Reisizeable Window Variables
    Private Property Fullscreen
    Private Property Maxscreen
    Dim WindowsState As String = "normal"
    Dim storedClientSize As Size
    Const ImaginaryBorderSize As Integer = 16
    Private Const HTLEFT As Integer = 10, HTRIGHT As Integer = 11, HTTOP As Integer = 12, HTTOPLEFT As Integer = 13, HTTOPRIGHT As Integer = 14, HTBOTTOM As Integer = 15, HTBOTTOMLEFT As Integer = 16, HTBOTTOMRIGHT As Integer = 17

    'Resizeable Window Init'

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        e.Graphics.FillRectangle(Brushes.Transparent, Top)
        e.Graphics.FillRectangle(Brushes.Transparent, Left)
        e.Graphics.FillRectangle(Brushes.Transparent, Right)
        e.Graphics.FillRectangle(Brushes.Transparent, Bottom)

    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Const WM_SYSCOMMAND As Integer = &H112
        Const SC_CLOSE As Integer = &HF060
        If m.Msg = WM_SYSCOMMAND AndAlso m.WParam.ToInt32() = SC_CLOSE Then
            Application.Exit() 'Patch bug where process not killed due to main form being hidden
        ElseIf m.Msg = &H84 Then
            Dim mp = Me.PointToClient(Cursor.Position)
            If TopLeft.Contains(mp) Then
                m.Result = CType(HTTOPLEFT, IntPtr)
            ElseIf TopRight.Contains(mp) Then
                m.Result = CType(HTTOPRIGHT, IntPtr)
            ElseIf BottomLeft.Contains(mp) Then
                m.Result = CType(HTBOTTOMLEFT, IntPtr)
            ElseIf BottomRight.Contains(mp) Then
                m.Result = CType(HTBOTTOMRIGHT, IntPtr)
            ElseIf Top.Contains(mp) Then
                m.Result = CType(HTTOP, IntPtr)
            ElseIf Left.Contains(mp) Then
                m.Result = CType(HTLEFT, IntPtr)
            ElseIf Right.Contains(mp) Then
                m.Result = CType(HTRIGHT, IntPtr)
            ElseIf Bottom.Contains(mp) Then
                m.Result = CType(HTBOTTOM, IntPtr)
            End If
        Else
            MyBase.WndProc(m)
        End If
    End Sub

    Shadows Function Top() As Rectangle
        If Me.WindowState <> FormWindowState.Maximized Then
            Return New Rectangle(0, 0, Me.ClientSize.Width, ImaginaryBorderSize)
        End If
    End Function

    Shadows Function Left() As Rectangle
        If Me.WindowState <> FormWindowState.Maximized Then
            Return New Rectangle(0, 0, ImaginaryBorderSize, Me.ClientSize.Height)
        End If
    End Function

    Shadows Function Bottom() As Rectangle
        If Me.WindowState <> FormWindowState.Maximized Then
            Return New Rectangle(0, Me.ClientSize.Height - ImaginaryBorderSize, Me.ClientSize.Width, ImaginaryBorderSize)
        End If
    End Function

    Shadows Function Right() As Rectangle
        If Me.WindowState <> FormWindowState.Maximized Then
            Return New Rectangle(Me.ClientSize.Width - ImaginaryBorderSize, 0, ImaginaryBorderSize, Me.ClientSize.Height)
        End If
    End Function

    Shadows Function TopLeft() As Rectangle
        If Me.WindowState <> FormWindowState.Maximized Then
            Return New Rectangle(0, 0, ImaginaryBorderSize, ImaginaryBorderSize)
        End If
    End Function

    Shadows Function TopRight() As Rectangle
        If Me.WindowState <> FormWindowState.Maximized Then
            Return New Rectangle(Me.ClientSize.Width - ImaginaryBorderSize, 0, ImaginaryBorderSize, ImaginaryBorderSize)
        End If
    End Function

    Shadows Function BottomLeft() As Rectangle
        If Me.WindowState <> FormWindowState.Maximized Then
            Return New Rectangle(0, Me.ClientSize.Height - ImaginaryBorderSize, ImaginaryBorderSize, ImaginaryBorderSize)
        End If
    End Function

    Shadows Function BottomRight() As Rectangle
        If Me.WindowState <> FormWindowState.Maximized Then
            Return New Rectangle(Me.ClientSize.Width - ImaginaryBorderSize, Me.ClientSize.Height - ImaginaryBorderSize, ImaginaryBorderSize, ImaginaryBorderSize)
        End If
    End Function

    '---Functions

    Public Sub New(g_username As String, g_password As String)
        Authorisation.Username = g_username
        Authorisation.Password = g_password
        Authorisation.GenerateUID()
        InitializeComponent()
    End Sub

    'Init WinForm
    Private Sub POSSystem_Load() Handles MyBase.Load

        For Each cntrl As Control In TblTabsContainer.Controls.OfType(Of Panel) 'Init Tab System
            cntrl.Width = 0
        Next
        lblCurrentUser.Text = Authorisation.Username
        LoadUserConfig() 'Load User Data
        LoadAccounts() ' Load Passwords
        'ChangeTab(lblTabSel1, e)

    End Sub

    '---Database Functions

    'Load Passwords
    Private Sub LoadAccounts()
        Dim cmd As New OleDbCommand("SELECT [Account Name] FROM Passwords", AuthLogin.conn)
        myReader = cmd.ExecuteReader
        lbxUsernames.Items.Clear()
        While myReader.Read
            lbxUsernames.Items.Add(myReader("Account Name"))
        End While
    End Sub

    'Load User Configs
    Private Sub LoadUserConfig()
        accentColor = Color.FromArgb(database.SqlReadValue("SELECT Accent FROM UserConfig WHERE (UID=" & Authorisation.UID & ")"))
        If database.SqlReadValue("SELECT RGB FROM UserConfig WHERE (UID=" & Authorisation.UID & ")").ToString = "True" Then
            TmrRGB.Enabled = True
        Else
            TmrRGB.Enabled = False
        End If
        UpdateAccent()
    End Sub

    'Save User Config
    Private Sub SaveConfig(command As String)
        'Dim cmd As New OleDbCommand(command, AuthLogin.conn)
        'cmd.ExecuteNonQuery()
    End Sub

    '---UI Library Functions

    'Tab Changing System
    Private Sub ChangeTab(sender As Object, e As EventArgs) Handles lblTabSel1.Click, lblTabSel2.Click
        If sender.forecolor <> accentColor Then

            'Darkens all tab indicator text
            For Each panel As Control In TblTabsContainer.Controls.OfType(Of Panel)
                If panel.Tag <> "border" Then
                    panel.Padding = New Padding(0, 0, 0, 1)
                    For Each lbl As Control In panel.Controls.OfType(Of Label)
                        If sender Is lbl Then
                            lbl.ForeColor = accentColor
                            lbl.BackColor = Color.FromArgb(31, 33, 45)
                            lbl.Parent.Padding = New Padding(1, 1, 1, 0)
                        Else
                            lbl.ForeColor = Color.FromArgb(150, 150, 150)
                            lbl.BackColor = Color.FromArgb(27, 28, 39)
                        End If
                    Next
                End If
            Next

            'Hightlights selected tab with accent color
            sender.ForeColor = accentColor
            sender.BackColor = Color.FromArgb(31, 33, 45)

            'Undocks all tab panels and hides them
            For Each menuscreen As Control In Panel1.Controls.OfType(Of Panel)
                menuscreen.Dock = DockStyle.None
                menuscreen.Height = 0
            Next

            'Docks the selected tab panel and accents selected tab indicator
            If sender Is lblTabSel1 Then
                pnlMainPage.Dock = DockStyle.Fill
                pnlMainPage.BringToFront()
            ElseIf sender Is lblTabSel2 Then
                pnlSettingsPage.Dock = DockStyle.Fill
                pnlSettingsPage.BringToFront()
            End If
        End If
        btnSave.Focus() 'Remove control focus

    End Sub

    '---Change Colourisable Accents in UI

    Public Sub UpdateAccent()
        'Groupbox Topbar
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

        'Color Picker Preview
        pnlColorPicker.BackColor = accentColor
        'Main Topbar
        Panel8.BackColor = accentColor
        'RGB Toggle
        If TmrRGB.Enabled Then
            PnlRGBToggle.BackColor = accentColor
        End If
        'Tab Label Accent Updating
        lblTabSel2.ForeColor = accentColor

        ColorPicker.UpdateAccent()
        SaveConfig("UPDATE UserConfig SET Accent=" & accentColor.ToArgb() & " WHERE UID=" & Authorisation.UID)

    End Sub


    '---Titlebar Button Events'

    'Exit Button
    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        AuthLogin.conn.Close()
        Application.Exit()
    End Sub

    'Minimize Button
    Private Sub BtnMinimize_Click(sender As Object, e As EventArgs) Handles BtnMinimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    'Maximize Button
    Private Sub BtnMaximize_Click(sender As Object, e As EventArgs) Handles BtnMaximize.Click
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
            Me.ClientSize = storedClientSize
            WindowsState = "normal"
        Else
            storedClientSize = Me.ClientSize
            Me.MaximumSize = Maxscreen
            Me.WindowState = FormWindowState.Maximized
            WindowsState = "maximised"
        End If
    End Sub

    '---Notifications

    'Full screen notifications
    Private Sub Notifcation(notifcationText As String)
        lblNotifcationInfo.Text = notifcationText
        pnlNotification.Dock = DockStyle.Fill
        pnlNotification.BringToFront()
    End Sub

    'Dismiss Notification Button
    Private Sub DimissNotification(sender As Object, e As EventArgs) Handles btnContinueNotification.Click
        pnlNotification.Dock = DockStyle.None
        pnlNotification.Height = 0
    End Sub

    'UI Accent Colour Picker
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles pnlColorPicker.Click
        If Not ColorPicker.IsHandleCreated Then
            ColorPicker.Show()
        End If
        UpdateAccent()
    End Sub

    '---Application Code

    'Users Screen

    'Load selected User's Data
    Private Sub LoadSelectedUserInfo(sender As Object, e As EventArgs) Handles lbxUsernames.SelectedValueChanged
        If lbxUsernames.SelectedItem <> "" Then
            selectedUID = database.SqlReadValue("SELECT UID FROM Passwords WHERE ([Account Name]='" & lbxUsernames.SelectedItem.ToString & "')")
            TbxAccountName.Text = lbxUsernames.SelectedItem.ToString
            TbxWebsite.Text = database.SqlReadValue("SELECT Website FROM Passwords WHERE UID=" & selectedUID)
            TbxUsername.Text = database.SqlReadValue("SELECT Username FROM Passwords WHERE UID=" & selectedUID)
            Dim wrapper As New Simple3Des(localPassword)
            Dim hashedpassword As String = database.SqlReadValue("SELECT [Password] FROM Passwords WHERE UID=" & selectedUID)
            Try
                Dim plainText As String = wrapper.DecryptData(hashedpassword)
                TbxPassword.Text = plainText
            Catch ex As System.Security.Cryptography.CryptographicException
                Notifcation("Error: Passwords could not be decrypted.")
            Catch ex As FormatException
                Notifcation("Error: Password entry is corrupt.")
            End Try
        End If

    End Sub

    'Save Changes to User's Username and Password
    Private Sub UpdateUserCredentials(sender As Object, e As EventArgs) Handles btnSave.Click
        If TbxAccountName.Text <> "" And lbxUsernames.SelectedItem <> Nothing And (database.SqlReadValue("SELECT [Account Name] FROM Passwords WHERE UID=" & selectedUID) = TbxAccountName.Text.ToString Or database.SqlReadValue("SELECT UID FROM Passwords WHERE [Account Name]='" & TbxAccountName.Text.ToString & "'") = Nothing) Then
            Dim wrapper As New Simple3Des(localPassword)
            Dim cipherText As String = wrapper.EncryptData(TbxPassword.Text.ToString)
            SaveConfig("UPDATE Passwords SET [Account Name]='" & TbxAccountName.Text & "' WHERE UID=" & selectedUID)
            SaveConfig("UPDATE Passwords SET Website='" & TbxWebsite.Text & "' WHERE UID=" & selectedUID)
            SaveConfig("UPDATE Passwords SET Username='" & TbxUsername.Text & "' WHERE UID=" & selectedUID)
            SaveConfig("UPDATE Passwords SET [Password]='" & cipherText & "' WHERE UID=" & selectedUID)
            Notifcation("New User Credentials for " & TbxAccountName.Text & " have been saved successfully!")
        ElseIf TbxAccountName.Text = "" Then
            Notifcation("Error: An account name is required!")
            LoadSelectedUserInfo(sender, e)
        ElseIf database.SqlReadValue("SELECT UID FROM Passwords WHERE [Account Name]='" & TbxAccountName.Text.ToString & "'") <> Nothing Then
            Notifcation("Error: This account name is already in use!")
        Else
            Notifcation("Error: An entry must be selected.")
        End If
        LoadAccounts()
        lbxUsernames.SelectedItem = database.SqlReadValue("SELECT [Account Name] FROM [Passwords] WHERE UID=" & selectedUID)
    End Sub

    'Clears User Data Fields
    Private Sub ClearUserDataFields(sender As Object, e As EventArgs) Handles BtnClear.Click
        selectedUID = Nothing
        lbxUsernames.SelectedItem = Nothing
        TbxAccountName.Clear()
        TbxWebsite.Clear()
        TbxUsername.Clear()
        TbxPassword.Clear()
    End Sub

    'Adds New User To Database
    Private Sub AddNewUser(sender As Object, e As EventArgs) Handles BtnAddUser.Click
        If database.SqlReadValue("SELECT UID FROM Passwords WHERE ([Account Name]='" & TbxAccountName.Text.ToString & "')") = Nothing And TbxAccountName.Text.ToString <> Nothing Then
            Dim wrapper As New Simple3Des(localPassword)
            Dim cipherText As String = wrapper.EncryptData(TbxPassword.Text.ToString)
            SaveConfig("INSERT INTO Passwords ([Account Name],Website,Username,[Password]) VALUES ('" & TbxAccountName.Text.ToString & "','" & TbxWebsite.Text.ToString & "','" & TbxUsername.Text.ToString & "','" & cipherText & "')")
            Notifcation("New entry " & TbxAccountName.Text.ToString & " has been successfully added!")
            LoadAccounts()
            lbxUsernames.SelectedItem = database.SqlReadValue("SELECT [Account Name] FROM [Passwords] WHERE [Account Name]='" & TbxAccountName.Text.ToString & "'")
        ElseIf database.SqlReadValue("SELECT UID FROM Passwords WHERE [Account Name]='" & TbxAccountName.Text.ToString & "'") = Nothing Then
            Notifcation("Error: Account name is required!")
        Else
            Notifcation("Error: Entry " & TbxAccountName.Text.ToString & " already exists.")
            TbxAccountName.Clear()
        End If

    End Sub

    'Deletes Selected User
    Private Sub DeleteUser(sender As Object, e As EventArgs) Handles BtnDelete.Click, BtnContinueAction.Click, BtnCancelAction.Click
        If sender Is BtnDelete And lbxUsernames.SelectedItem <> Nothing Then
            lblConfirmationText.Text = "Are you sure that you want to delete your credentials for " & lbxUsernames.SelectedItem.ToString & "?"
            pnlConfirmation.Dock = DockStyle.Fill
            pnlConfirmation.BringToFront()
        ElseIf sender Is BtnDelete Then
            Notifcation("Error: An entry must be selected!")
        ElseIf sender Is BtnContinueAction Or sender Is BtnCancelAction Then
            pnlConfirmation.Dock = DockStyle.None
            pnlConfirmation.Height = 0
        End If
        If sender Is BtnContinueAction Then
            Dim TempAccountName As String = database.SqlReadValue("SELECT [Account Name] FROM Passwords WHERE UID=" & selectedUID)
            SaveConfig("DELETE FROM Passwords WHERE UID=" & selectedUID)
            selectedUID = Nothing
            ClearUserDataFields(sender, e)
            Notifcation("Entry " & TempAccountName & " successfully deleted!")
        End If
        LoadAccounts()
        lbxUsernames.SelectedItem = database.SqlReadValue("SELECT [Account Name] FROM [Passwords] WHERE UID=" & selectedUID)
    End Sub

    'Settings Tab 

    'User Logout Button
    Private Sub UserLogOut(sender As Object, e As EventArgs) Handles BtnLogOut.Click
        AuthLogin.conn.Close()
        Me.Close()
        AuthLogin.Show()
        AuthLogin.LoadUsernames()
    End Sub

    'Save Admin Password Button
    Private Sub SaveAdminPassword(sender As Object, e As EventArgs) Handles btnSaveAdminPass.Click
        If TbxAdminUsername.Text <> "" Then
            SaveConfig("UPDATE UserAuth SET Username='" & TbxAdminUsername.Text & "' WHERE UID=1")
        End If
        Dim cmd2 As New OleDbCommand("SELECT UID FROM [PASSWORDS]", AuthLogin.conn)
        Dim myReader2 As OleDbDataReader = cmd2.ExecuteReader

        'Setup encryption keys
        Dim wrapper As New Simple3Des(localPassword)
        Dim newWrapper As New Simple3Des(tbxAdminPassword.Text)

        If tbxAdminPassword.Text <> "" Then

            'Decrypt all passwords
            While myReader2.Read()
                Dim plainText As String = wrapper.DecryptData(database.SqlReadValue("SELECT [Password] FROM [Passwords] WHERE UID=" & myReader2("UID")))
                SaveConfig("UPDATE Passwords SET [PASSWORD]='" & plainText & "' WHERE UID=" & myReader2("UID"))
            End While

            'Reload Database
            myReader2.Close()
            myReader2 = cmd2.ExecuteReader

            'Re-encrypt all passwords
            While myReader2.Read()
                Dim NewPassword As String = newWrapper.EncryptData(database.SqlReadValue("SELECT [Password] FROM [Passwords] WHERE UID=" & myReader2("UID")))
                SaveConfig("UPDATE Passwords SET [PASSWORD]='" & NewPassword & "' WHERE UID=" & myReader2("UID"))
            End While

            'Rehash and Save Password
            localPassword = tbxAdminPassword.Text
            Dim hashedpassword As String = AuthLogin.MD5(localPassword)
            SaveConfig("UPDATE UserAuth SET PIN='" & hashedpassword & "' WHERE UID=1")

        End If

        'Notify User Of Changes Made
        If TbxAdminUsername.Text <> "" And tbxAdminPassword.Text <> "" Then
            Notifcation("New admin credentials have been updated successfully!")
        ElseIf TbxAdminUsername.Text <> "" Then
            Notifcation("New admin username has been updated successfully!")
        ElseIf tbxAdminPassword.Text <> "" Then
            Notifcation("New admin password has been updated successfully!")
        Else
            Notifcation("Error: Both fields can not be empty!")
        End If
        TbxAdminUsername.Clear()
        tbxAdminPassword.Clear()
    End Sub

    '---DebugInfo

    'Timer Tick Update
    Private Sub DebugInfoUpdateOnTick(sender As Object, e As EventArgs) Handles tmrMain.Tick
        lblTitle.Text = programName & " | " & versionNumber & " | " & currentUser & " | " & DateTime.Now.ToString("HH:mm:ss") & " | " & DateTime.Now.ToString("dd MMM. yyyy")
    End Sub


    '---RGB Window 

    'RGB Window Border Effect Update On Tick
    Private Sub TmrRGB_Tick(sender As Object, e As EventArgs) Handles TmrRGB.Tick
        If data > 360 Then
            data = 0
        Else
            data += 1
        End If
        Me.BackColor = ColorPicker.HlsToRgb(data, 0.5, 0.5)

    End Sub

    'RGB Toggle Event
    Private Sub PnlRGBToggle_Click(sender As Object, e As EventArgs) Handles PnlRGBToggle.Click
        If TmrRGB.Enabled = False Then
            TmrRGB.Enabled = True
            PnlRGBToggle.BackColor = accentColor
            SaveConfig("UPDATE UserConfig SET RGB='True'" & " WHERE UID=" & Authorisation.UID)
        Else
            TmrRGB.Enabled = False
            PnlRGBToggle.BackColor = Color.FromArgb(27, 28, 39)
            Me.BackColor = Color.FromArgb(98, 113, 165)
            data = 0
            SaveConfig("UPDATE UserConfig SET RGB='False'" & " WHERE UID=" & Authorisation.UID)
        End If
    End Sub
End Class

'---Encryption Container
'https://learn.microsoft.com/en-us/dotnet/visual-basic/programming-guide/language-features/strings/walkthrough-encrypting-and-decrypting-strings

Public NotInheritable Class Simple3Des
    Private ReadOnly TripleDes As New TripleDESCryptoServiceProvider
    Private Function TruncateHash(
    ByVal key As String,
    ByVal length As Integer) As Byte()

        Dim sha1 As New SHA1CryptoServiceProvider

        ' Hash the key.
        Dim keyBytes() As Byte =
            System.Text.Encoding.Unicode.GetBytes(key)
        Dim hash() As Byte = sha1.ComputeHash(keyBytes)

        ' Truncate or pad the hash.
        ReDim Preserve hash(length - 1)
        Return hash
    End Function
    Sub New(ByVal key As String)
        ' Initialize the crypto provider.
        TripleDes.Key = TruncateHash(key, TripleDes.KeySize \ 8)
        TripleDes.IV = TruncateHash("", TripleDes.BlockSize \ 8)
    End Sub
    Public Function EncryptData(
    ByVal plaintext As String) As String

        ' Convert the plaintext string to a byte array.
        Dim plaintextBytes() As Byte =
            System.Text.Encoding.Unicode.GetBytes(plaintext)

        ' Create the stream.
        Dim ms As New System.IO.MemoryStream
        ' Create the encoder to write to the stream.
        Dim encStream As New CryptoStream(ms,
            TripleDes.CreateEncryptor(),
            System.Security.Cryptography.CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream.
        encStream.Write(plaintextBytes, 0, plaintextBytes.Length)
        encStream.FlushFinalBlock()

        ' Convert the encrypted stream to a printable string.
        Return Convert.ToBase64String(ms.ToArray)
    End Function
    Public Function DecryptData(
    ByVal encryptedtext As String) As String

        ' Convert the encrypted text string to a byte array.
        Dim encryptedBytes() As Byte = Convert.FromBase64String(encryptedtext)

        ' Create the stream.
        Dim ms As New System.IO.MemoryStream
        ' Create the decoder to write to the stream.
        Dim decStream As New CryptoStream(ms,
        TripleDes.CreateDecryptor(),
        System.Security.Cryptography.CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream.
        decStream.Write(encryptedBytes, 0, encryptedBytes.Length)
        decStream.FlushFinalBlock()

        ' Convert the plaintext stream to a string.
        Return System.Text.Encoding.Unicode.GetString(ms.ToArray)
    End Function
End Class
