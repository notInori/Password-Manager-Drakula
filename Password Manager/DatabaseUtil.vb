Imports System.Data.OleDb

Class DatabaseUtil
    Private conn As New OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=" & ".\UserData.accdb")

    Public Sub New()
        conn.Open()
    End Sub

    Public Function SqlReadValue(command As String)
        Dim cmd As New OleDbCommand(command, conn)
        Dim DatabaseValuesArray As New List(Of Object)
        Using myReader As OleDbDataReader = cmd.ExecuteReader()
            If myReader.FieldCount > 1 Then
                While myReader.Read()
                    DatabaseValuesArray.Add(myReader(0))
                End While
                Return DatabaseValuesArray.ToArray
            ElseIf myReader.FieldCount = 1 Then
                While myReader.Read()
                    DatabaseValuesArray.Add(myReader(0))
                End While
                Return DatabaseValuesArray.ToArray
            End If
        End Using
        Return Nothing
    End Function
End Class

Class Authorisation
    Dim database As New DatabaseUtil
    Private _username As String
    Private _password As String

    Public Property Username As String
        Get
            Return _username
        End Get
        Set(value As String)
            _username = value
        End Set
    End Property

    Public Property Password As String
        Get
            Return _password
        End Get
        Set(value As String)
            _password = value
        End Set
    End Property

    '---Init 

    Public Sub New(username As String, password As String)

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

    Private Function AuthUser(ByVal username As String, ByVal password As String)
        Dim storedPassword = database.SqlReadValue("SELECT PIN FROM UserAuth WHERE (Username='" & username & "')")
        If password = CStr(_password) And CStr(_password) <> "" Then
            Return True 'Returns true if combination of username and password is correct
        Else
            Return False 'Returns false if combination is inccorrect or fields are empty
        End If
    End Function
End Class


