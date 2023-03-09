Imports System.Data.OleDb
Imports System.Web.UI.WebControls

Public Class AdminPanel

    '---Init'

    'Client Info Variables
    Public Shared ReadOnly businessName As String = ""
    Public Shared ReadOnly versionNumber As String = "[Dev Build]"
    ReadOnly currentUser As String = "Admin"
    Dim UID As Integer

    'Variables Init'
    Public Shared accentColor As Color = Color.FromArgb(255, 255, 255)
    ReadOnly cDialog As New ColorDialog()
    Dim selectedUID As New Integer

    '---Winforms Init' 

    'Load UID
    Private Sub setUID(ByVal username As String)
        Dim temp As String = ""
        Dim cmdInput As String = "SELECT UID FROM UserAuth WHERE (Username='" & username & "')"
        Dim cmd As New OleDbCommand(cmdInput, conn)
        Dim myReader As OleDbDataReader = cmd.ExecuteReader
        While myReader.Read()
            temp = myReader("UID")
        End While
        UID = CInt(temp)

    End Sub

    'Database Variables Init
    Dim myReader As OleDbDataReader
    Dim conn As New OleDbConnection(AuthLogin.UserDataConnectionString)

    '---Winforms Init' 

    'Read From Database
    Public Function SqlReadVAlue(command As String)
        Dim cmd As New OleDbCommand(command, conn)
        myReader = cmd.ExecuteReader
        While myReader.Read()
            Return myReader.GetValue(0)
        End While
    End Function

    'Load Usernames

    Private Sub loadUsernames()
        Dim cmd As New OleDbCommand("SELECT Username FROM UserAuth", conn)
        myReader = cmd.ExecuteReader
        lbxUsernames.Items.Clear()
        While myReader.Read
            If myReader("Username") <> "admin" Then
                lbxUsernames.Items.Add(myReader("Username"))
            End If
        End While
    End Sub

    'Load User Configs
    Private Sub loadUserConfig()
        UID = CInt(SqlReadVAlue("SELECT UID FROM UserAuth WHERE (Username='" & currentUser & "')"))
        accentColor = Color.FromArgb(SqlReadVAlue("SELECT Accent FROM UserConfig WHERE (UID=" & UID & ")"))
        UpdateAccent()
    End Sub

    'Save User Config
    Private Sub saveConfig(command As String)
        Dim cmd As New OleDbCommand(command, conn)
        cmd.ExecuteNonQuery()
    End Sub

    'Init tab system and load accent color
    Private Sub POSSystem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        conn.Open()
        For Each cntrl As Control In TblTabsContainer.Controls.OfType(Of Panel)
            cntrl.Width = 0
        Next
        lblCurrentUser.Text = currentUser
        setUID(currentUser)
        loadUserConfig()
        ChangeTab(lblTabSel1, e)
        loadUsernames()
    End Sub


    '---Tab Changing System

    Private Sub ChangeTab(sender As Object, e As EventArgs) Handles lblTabSel1.Click, lblTabSel2.Click, lblTabSel3.Click, lblTabSel4.Click

        'Hides selected tab indicator
        For Each cntrl As Control In TblTabsContainer.Controls.OfType(Of Panel)
            cntrl.Visible = False
        Next

        'Darkens all tab indicator text
        For Each lbl As Control In TblTabsContainer.Controls.OfType(Of Label)
            lbl.ForeColor = Color.FromArgb(150, 150, 150)
        Next

        'Hightlights selected tab with accent color
        sender.ForeColor = accentColor

        'Undocks all tab panels and hides them
        For Each menuscreen As Control In Panel1.Controls.OfType(Of Panel)
            menuscreen.Dock = DockStyle.None
            menuscreen.Height = 0
        Next

        'Docks the selected tab panel and accents selected tab indicator
        If sender Is lblTabSel1 Then
            pnlMainPage.Dock = DockStyle.Fill
            pnlTabHighlight1.Visible = True
        ElseIf sender Is lblTabSel2 Then
            pnlMenuPage.Dock = DockStyle.Fill
            pnlTabHighlight2.Visible = True
        ElseIf sender Is lblTabSel3 Then
            pnlPerformancePage.Dock = DockStyle.Fill
            pnlTabHighlight3.Visible = True
        ElseIf sender Is lblTabSel4 Then
            pnlSettingsPage.Dock = DockStyle.Fill
            pnlTabHighlight4.Visible = True
        End If
    End Sub

    '---Change Colourisable Accents in UI

    Private Sub UpdateAccent()
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

        'Tab Highlight Accent Updating
        For Each cntrl As Control In TblTabsContainer.Controls.OfType(Of Panel)
            cntrl.BackColor = accentColor
        Next

        'Tab Label Accent Updating
        lblTabSel4.ForeColor = accentColor

    End Sub

    '---Application Code

    'Settings Tab 

    'UI Accent Colour Picker
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles pnlColorPicker.Click
        If (cDialog.ShowDialog() = DialogResult.OK) Then
            accentColor = cDialog.Color ' update with user selected color.
        End If
        UpdateAccent()
        saveConfig("UPDATE UserConfig SET Accent=" & accentColor.ToArgb() & " WHERE UID=" & UID)
    End Sub

    'User Logout Button
    Private Sub UserLogOut(sender As Object, e As EventArgs) Handles BtnLogOut.Click
        Me.Close()
        AuthLogin.Show()
    End Sub

    '---Watermark

    'Timer Tick Update
    Private Sub TmrMain_Tick(sender As Object, e As EventArgs) Handles tmrMain.Tick
        lblTitle.Text = "POS SYSTEM | " & versionNumber & " | " & currentUser & " | " & DateTime.Now.ToString("HH:mm:ss") & " | " & DateTime.Now.ToString("dd MMM. yyyy")
    End Sub

    Private Sub LoadSelectedUserInfo(sender As Object, e As EventArgs) Handles lbxUsernames.SelectedValueChanged, btnReload.Click
        If lbxUsernames.SelectedItem <> "" Then
            selectedUID = SqlReadVAlue("SELECT UID FROM UserAuth WHERE (Username='" & lbxUsernames.SelectedItem.ToString & "')")
            TbxUsername.Text = lbxUsernames.SelectedItem
            TbxPassword.Text = SqlReadVAlue("SELECT PIN FROM UserAuth WHERE (Username='" & lbxUsernames.SelectedItem.ToString & "')")
        End If

    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        saveConfig("UPDATE UserAuth SET Username='" & TbxUsername.Text & "' WHERE UID=" & selectedUID)
        saveConfig("UPDATE UserAuth SET PIN='" & TbxPassword.Text & "' WHERE UID=" & selectedUID)
        loadUsernames()
        AuthLogin.loadUsernames()
        lbxUsernames.SelectedItem = SqlReadVAlue("SELECT Username FROM UserAuth WHERE (UID=" & selectedUID & ")")
    End Sub
End Class
