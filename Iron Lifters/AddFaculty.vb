Imports MySql.Data.MySqlClient

Imports System.IO

Public Class AddFaculty
    Private Sub a_Click(sender As Object, e As EventArgs) Handles a.Click
        Try
            dbConn()
            Dim sexid As Integer = ComboBox2.SelectedIndex
            Dim accid As Integer = ComboBox1.SelectedIndex + 1
            Dim phn As Integer = Integer.Parse(phBox.Text)

            Dim cmd As New MySqlCommand("INSERT INTO faculty (lastname, firstname, sex, email, username, password, FIDLevel, phone_number) VALUES ('" & lBox.Text & "', '" & fBox.Text & "', '" & sexid & "', '" & eBox.Text & "', '" & uBox.Text & "', '" & pBox.Text & "', '" & accid & "', " & phn & ")", conn)

            cmd.ExecuteNonQuery()

            MessageBox.Show("Faculty added successfully.")
            Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dbDisconn()
        End Try
    End Sub

    Private Sub cancelButton_Click(sender As Object, e As EventArgs) Handles cancelButton.Click
        dbDisconn()
        Close()
    End Sub

    Private Sub AddUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            dbConn()
            Dim cmd As New MySqlCommand("SELECT level_name FROM flevel", conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            While reader.Read()
                ComboBox1.Items.Add(reader("level_name").ToString())
            End While
            cmd.Dispose()
            reader.Close()
            cmd = New MySqlCommand("SELECT `Sex Name` FROM sex", conn)
            reader = cmd.ExecuteReader
            While reader.Read
                ComboBox2.Items.Add(reader("Sex Name").ToString())
            End While
            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Error loading membership plans: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub
End Class