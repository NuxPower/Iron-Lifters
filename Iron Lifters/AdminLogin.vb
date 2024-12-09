Imports MySql.Data.MySqlClient

Public Class AdminLogin
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            dbConn()
            Dim username As String = uBox.Text
            Dim password As String = pBox.Text

            Using cmd As New MySqlCommand("SELECT FID, username, password, FIDLevel FROM faculty WHERE username = @username AND password = @password", conn)
                cmd.Parameters.AddWithValue("@username", username)
                cmd.Parameters.AddWithValue("@password", password)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.HasRows Then
                        reader.Read()

                        ' Retrieve FID and set module attributes for logged-in user
                        AdminLoggedModule.LoggedInFID = reader("FID")
                        AdminLoggedModule.LoggedInAccessLevel = reader("FIDLevel")
                        AdminLoggedModule.LoginInDateTime = DateTime.Now
                        AdminLoggedModule.LoginOutDateTime = DateTime.MinValue

                        ' Close the reader before logging the login time
                        reader.Close()

                        ' Log the login time to the database
                        LogAdminLogin(AdminLoggedModule.LoggedInFID, AdminLoggedModule.LoginInDateTime)

                        My.Application.ChangeMainForm(Main)
                        Main.Show()
                        Me.Close()
                        dbDisconn()
                    Else
                        MessageBox.Show("Unauthorized login detected!!")
                        reader.Close()
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error processing app: " & ex.Message, "Error")
        Finally
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                dbDisconn()
            End If
        End Try
    End Sub

    Private Sub LogAdminLogin(fid As Integer, loginTime As DateTime)
        Try
            Using cmd As New MySqlCommand("INSERT INTO falogs (FID, in_datetime) VALUES (@FID, @in_datetime)", conn)
                cmd.Parameters.AddWithValue("@FID", fid)
                cmd.Parameters.AddWithValue("@in_datetime", loginTime)
                cmd.ExecuteNonQuery()
            End Using

            ' Retrieve the last inserted FALID
            Dim falID As Integer
            Using cmdGetID As New MySqlCommand("SELECT LAST_INSERT_ID()", conn)
                falID = Convert.ToInt32(cmdGetID.ExecuteScalar())
            End Using

            ' Set the LoggedInID in LoggedInModule
            AdminLoggedModule.LoggedInFALID = falID
        Catch ex As Exception
            MessageBox.Show("Error logging admin login: " & ex.Message, "Error")
        End Try
    End Sub

End Class
