Imports System.CodeDom.Compiler
Imports System.Data.OleDb
Imports System.Security.Cryptography.X509Certificates

Public Class MainProgram

    '---Init'

    'Client Info Config
    Public Shared ReadOnly programName As String = "Password Manager"
    Public Shared ReadOnly versionNumber As String = "[Dev Build]"

    'Client Info Variables
    Public Shared currentUser As String = "[USER]"
    Dim UID As Integer

    'Variables Init'
    Public Shared accentColor As Color = Color.FromArgb(255, 255, 255)
    ReadOnly cDialog As New ColorDialog()
    Dim selectedUID As New Integer

    'Variables Init'
    Dim r As Integer
    Dim b As Integer
    Dim g As Integer
    Dim data As Integer

    'Database Variables Init
    Dim myReader As OleDbDataReader
    ReadOnly conn As New OleDbConnection(AuthLogin.UserDataConnectionString)

    '---Winforms Init' 

    'Winforms Variable Init'
    Private Property MoveForm As Boolean
    Private Property MoveForm_MousePositiion As Point

    'Winforms Dragging Events
    Private Sub WindowDragging_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlTitleIcons.MouseDown, lblTitle.MouseDown
        If e.Button = MouseButtons.Left And Me.WindowState <> FormWindowState.Maximized Then
            MoveForm = True
            Me.Cursor = Cursors.Default
            MoveForm_MousePositiion = e.Location
        End If
    End Sub

    Private Sub WindowDragging_MouseUp(sender As Object, e As MouseEventArgs) Handles pnlTitleIcons.MouseUp, lblTitle.MouseUp
        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub WindowDragging_MouseMove(sender As Object, e As MouseEventArgs) Handles pnlTitleIcons.MouseMove, lblTitle.MouseMove
        If MoveForm Then
            Me.Location += (e.Location - MoveForm_MousePositiion)
        End If
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
        MyBase.WndProc(m)
        If m.Msg = &H84 Then
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

    'Functions

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
            hue -= 360
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

    'Init WinForm
    Private Sub POSSystem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Connect Database
        conn.Open()
        'Init Tab System
        For Each cntrl As Control In TblTabsContainer.Controls.OfType(Of Panel)
            cntrl.Width = 0
        Next
        lblCurrentUser.Text = currentUser
        LoadUserConfig() 'Load User Data
        LoadPasswords() ' Load Passwords
        ChangeTab(lblTabSel1, e)
    End Sub

    '---Database Functions

    'Read Value from Database
    Public Function SqlReadVAlue(command As String)
        Dim cmd As New OleDbCommand(command, conn)
        myReader = cmd.ExecuteReader
        While myReader.Read()
            Return myReader.GetValue(0)
        End While
        Return Nothing
    End Function

    'Load Passwords
    Private Sub LoadPasswords()
        Dim cmd As New OleDbCommand("SELECT [Account Name] FROM Passwords", conn)
        myReader = cmd.ExecuteReader
        lbxUsernames.Items.Clear()
        While myReader.Read
            lbxUsernames.Items.Add(myReader("Account Name"))
        End While
    End Sub

    'Load User Configs
    Private Sub LoadUserConfig()
        UID = CInt(SqlReadVAlue("SELECT UID FROM UserAuth WHERE (Username='" & currentUser & "')"))
        accentColor = Color.FromArgb(SqlReadVAlue("SELECT Accent FROM UserConfig WHERE (UID=" & UID & ")"))
        UpdateAccent()
    End Sub

    'Save User Config
    Private Sub SaveConfig(command As String)
        Dim cmd As New OleDbCommand(command, conn)
        cmd.ExecuteNonQuery()
    End Sub

    '---UI Library Functions

    'Tab Changing System
    Private Sub ChangeTab(sender As Object, e As EventArgs) Handles lblTabSel1.Click, lblTabSel2.Click

        'Hides selected tab indicator
        'For Each cntrl As Control In TblTabsContainer.Controls.OfType(Of Panel)
        'cntrl.Visible = False
        'Next

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

        btnSave.Focus()
    End Sub

    '---Change Colourisable Accents in UI

    Public Sub UpdateAccent()
        'Groupbox Topbar Color Updating
        Panel8.BackColor = accentColor
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

        'Update Color Picker UI Preview
        pnlColorPicker.BackColor = accentColor

        'Tab Label Accent Updating
        lblTabSel2.ForeColor = accentColor

        If TmrRGB.Enabled Then
            PnlRGBToggle.BackColor = accentColor
        End If

        ColorPicker.updateAccent()

    End Sub


    '---Titlebar Button Events'

    'Exit Button
    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        conn.Close()
        AuthLogin.Close()
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
        ColorPicker.colorpickerlocation = pnlColorPicker.PointToScreen(Point.Empty)
        ColorPicker.Show()

        'If (cDialog.ShowDialog() = DialogResult.OK) Then
        'accentColor = cDialog.Color ' update with user selected color.
        'End If
        UpdateAccent()
        SaveConfig("UPDATE UserConfig SET Accent=" & accentColor.ToArgb() & " WHERE UID=" & UID)
    End Sub

    '---Application Code

    'Users Screen

    'Load selected User's Data
    Private Sub LoadSelectedUserInfo(sender As Object, e As EventArgs) Handles lbxUsernames.SelectedValueChanged
        If lbxUsernames.SelectedItem <> "" Then
            selectedUID = SqlReadVAlue("SELECT UID FROM Passwords WHERE ([Account Name]='" & lbxUsernames.SelectedItem.ToString & "')")
            TbxAccountName.Text = lbxUsernames.SelectedItem.ToString
            TbxWebsite.Text = SqlReadVAlue("SELECT Website FROM Passwords WHERE UID=" & selectedUID)
            TbxUsername.Text = SqlReadVAlue("SELECT Username FROM Passwords WHERE UID=" & selectedUID)
            TbxPassword.Text = SqlReadVAlue("SELECT [Password] FROM Passwords WHERE UID=" & selectedUID)
        End If
    End Sub

    Private Sub TmrRGB_Tick(sender As Object, e As EventArgs) Handles TmrRGB.Tick
        If Data > 360 Then
            Data = 0
        Else
            Data += 1
        End If
        HlsToRgb(Data, 0.5, 0.5)
        Me.BackColor = Color.FromArgb(r, g, b)

    End Sub

    Private Sub PnlRGBToggle_Click(sender As Object, e As EventArgs) Handles PnlRGBToggle.Click
        If TmrRGB.Enabled = False Then
            TmrRGB.Enabled = True
            PnlRGBToggle.BackColor = accentColor
        Else
            TmrRGB.Enabled = False
            PnlRGBToggle.BackColor = Color.FromArgb(27, 28, 39)
            Me.BackColor = Color.FromArgb(98, 113, 165)
            data = 0
        End If
    End Sub

    'Save Changes to User's Username and Password
    Private Sub UpdateUserCredentials(sender As Object, e As EventArgs) Handles btnSave.Click
        If TbxAccountName.Text <> "" And lbxUsernames.SelectedItem <> Nothing And (SqlReadVAlue("SELECT [Account Name] FROM Passwords WHERE UID=" & selectedUID) = TbxAccountName.Text.ToString Or SqlReadVAlue("SELECT UID FROM Passwords WHERE [Account Name]='" & TbxAccountName.Text.ToString & "'") = Nothing) Then
            SaveConfig("UPDATE Passwords SET [Account Name]='" & TbxAccountName.Text & "' WHERE UID=" & selectedUID)
            SaveConfig("UPDATE Passwords SET Website='" & TbxWebsite.Text & "' WHERE UID=" & selectedUID)
            SaveConfig("UPDATE Passwords SET Username='" & TbxUsername.Text & "' WHERE UID=" & selectedUID)
            SaveConfig("UPDATE Passwords SET [Password]='" & TbxPassword.Text & "' WHERE UID=" & selectedUID)
            Notifcation("New User Credentials for " & TbxAccountName.Text & " have been saved successfully!")
        ElseIf TbxAccountName.Text = "" Then
            Notifcation("Error: An account name is required!")
            LoadSelectedUserInfo(sender, e)
        ElseIf SqlReadVAlue("SELECT UID FROM Passwords WHERE [Account Name]='" & TbxAccountName.Text.ToString & "'") <> Nothing Then
            Notifcation("Error: This account name is already in use!")
        Else
            Notifcation("Error: An entry must be selected.")
        End If
        LoadPasswords()
        lbxUsernames.SelectedItem = SqlReadVAlue("SELECT [Account Name] FROM [Passwords] WHERE UID=" & selectedUID)
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
        If SqlReadVAlue("SELECT UID FROM Passwords WHERE ([Account Name]='" & TbxAccountName.Text.ToString & "')") = Nothing And TbxAccountName.Text.ToString <> Nothing Then
            SaveConfig("INSERT INTO Passwords ([Account Name],Website,Username,[Password]) VALUES ('" & TbxAccountName.Text.ToString & "','" & TbxWebsite.Text.ToString & "','" & TbxUsername.Text.ToString & "','" & TbxPassword.Text.ToString & "')")
            Notifcation("New entry " & TbxAccountName.Text.ToString & " has been successfully added!")
            LoadPasswords()
        ElseIf SqlReadVAlue("SELECT UID FROM Passwords WHERE [Account Name]='" & TbxAccountName.Text.ToString & "'") = Nothing Then
            Notifcation("Error: Account name is required!")
        Else
            Notifcation("Error: Entry " & TbxAccountName.Text.ToString & " already exists.")
            TbxAccountName.Clear()
        End If
    End Sub

    'Deletes Selected User
    Private Sub DeleteUser(sender As Object, e As EventArgs) Handles BtnDelete.Click, BtnContinueAction.Click, BtnCancelAction.Click
        If sender Is BtnDelete And lbxUsernames.SelectedItem <> Nothing Then
            lblConfirmationText.Text = "Are you sure that you want to delete you credentials for " & lbxUsernames.SelectedItem.ToString & "?"
            pnlConfirmation.Dock = DockStyle.Fill
            pnlConfirmation.BringToFront()
        ElseIf sender Is BtnDelete Then
            Notifcation("Error: An entry must be selected!")
        ElseIf sender Is BtnContinueAction Or sender Is BtnCancelAction Then
            pnlConfirmation.Dock = DockStyle.None
            pnlConfirmation.Height = 0
        End If
        If sender Is BtnContinueAction Then
            Dim TempAccountName As String = SqlReadVAlue("SELECT [Account Name] FROM Passwords WHERE UID=" & selectedUID)
            SaveConfig("DELETE FROM Passwords WHERE UID=" & selectedUID)
            selectedUID = Nothing
            ClearUserDataFields(sender, e)
            Notifcation("Entry " & TempAccountName & " Successfully Deleted!")
        End If
        LoadPasswords()
        lbxUsernames.SelectedItem = SqlReadVAlue("SELECT [Account Name] FROM [Passwords] WHERE UID=" & selectedUID)
    End Sub

    'Settings Tab 

    'User Logout Button
    Private Sub UserLogOut(sender As Object, e As EventArgs) Handles BtnLogOut.Click
        conn.Close()
        Me.Close()
        AuthLogin.Show()
        AuthLogin.LoadUsernames()
    End Sub

    'Save Admin Password Button
    Private Sub SaveAdminPassword(sender As Object, e As EventArgs) Handles btnSaveAdminPass.Click
        If TbxAdminUsername.Text <> "" Then
            SaveConfig("UPDATE UserAuth SET Username='" & TbxAdminUsername.Text & "' WHERE UID=1")
        End If
        If tbxAdminPassword.Text <> "" Then
            SaveConfig("UPDATE UserAuth SET PIN='" & tbxAdminPassword.Text & "' WHERE UID=1")
        End If
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

    '---Watermark

    'Timer Tick Update
    Private Sub TmrMain_Tick(sender As Object, e As EventArgs) Handles tmrMain.Tick
        lblTitle.Text = programName & " | " & versionNumber & " | " & currentUser & " | " & DateTime.Now.ToString("HH:mm:ss") & " | " & DateTime.Now.ToString("dd MMM. yyyy")
    End Sub

End Class
