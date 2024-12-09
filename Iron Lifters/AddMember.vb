Imports MySql.Data.MySqlClient
Imports QRCoder
Imports System.IO

Public Class AddMember
    Dim profile As Byte()
    Private Sub a_Click(sender As Object, e As EventArgs) Handles a.Click
        Try
            dbConn()
            Dim sexid As Integer = ComboBox2.SelectedIndex
            Dim memid As Integer = ComboBox1.SelectedIndex + 1
            Dim phno As String = phBox.Text.Trim()

            If sBox.CheckState <> CheckState.Checked OrElse String.IsNullOrWhiteSpace(lBox.Text) OrElse String.IsNullOrWhiteSpace(fBox.Text) OrElse String.IsNullOrWhiteSpace(sexid) OrElse String.IsNullOrWhiteSpace(memid) OrElse String.IsNullOrWhiteSpace(eBox.Text) OrElse String.IsNullOrWhiteSpace(uBox.Text) OrElse String.IsNullOrWhiteSpace(pBox.Text) OrElse profile Is Nothing OrElse profile.Length = 0 OrElse String.IsNullOrWhiteSpace(phno) OrElse phno.Length > 20 Then
                MessageBox.Show("Please input all fields.")
                Exit Sub
            End If

            Dim status As Integer = If(sBox.Checked, 1, 0)

            Dim price As Integer
            Using cmdGetAmount As New MySqlCommand("SELECT price FROM `membership plans` WHERE MPID = @mpid", conn)
                cmdGetAmount.Parameters.AddWithValue("@mpid", memid)
                Using reader As MySqlDataReader = cmdGetAmount.ExecuteReader()
                    If reader.Read() Then
                        price = Convert.ToInt32(reader("price"))
                    Else
                        MessageBox.Show("Membership plan not found.")
                        Exit Sub
                    End If
                End Using
            End Using

            Dim paymentAmount As Decimal
            Dim paymentInput As String = InputBox("Enter payment amount:", "Payment: " & price)
            If Not Decimal.TryParse(paymentInput, paymentAmount) OrElse paymentAmount < price Then
                MessageBox.Show("Invalid payment amount.")
                Exit Sub
            End If

            Using cmd As New MySqlCommand("INSERT INTO users (lastname, firstname, sex, email, uname, pword, status, MPID, image, phone_no) VALUES (@lastname, @firstname, @sex, @eBox, @uname, @pword, @status, @MPID, @profile_picture, @phone_no)", conn)
                cmd.Parameters.AddWithValue("@lastname", lBox.Text)
                cmd.Parameters.AddWithValue("@firstname", fBox.Text)
                cmd.Parameters.AddWithValue("@sex", sexid)
                cmd.Parameters.AddWithValue("@eBox", eBox.Text)
                cmd.Parameters.AddWithValue("@uname", uBox.Text)
                cmd.Parameters.AddWithValue("@pword", pBox.Text)
                cmd.Parameters.AddWithValue("@status", status)
                cmd.Parameters.AddWithValue("@MPID", memid)
                cmd.Parameters.AddWithValue("@profile_picture", profile)
                cmd.Parameters.AddWithValue("@phone_no", phno)
                cmd.ExecuteNonQuery()
            End Using

            Dim uid As Integer
            Using cmdGetUserId As New MySqlCommand("SELECT LAST_INSERT_ID()", conn)
                uid = Convert.ToInt32(cmdGetUserId.ExecuteScalar())
            End Using

            Dim qrGenerator As New QRCodeGenerator()
            Dim qrData As QRCodeData = qrGenerator.CreateQrCode(uid.ToString(), QRCodeGenerator.ECCLevel.Q)
            Dim qrCode As New QRCode(qrData)
            Dim qrCodeImage As Bitmap = qrCode.GetGraphic(20)
            Dim qrCodeImageBytes As Byte()
            Using ms As New MemoryStream()
                qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                qrCodeImageBytes = ms.ToArray()
            End Using

            Dim completename As String = $"{lBox.Text}, {fBox.Text}"
            Dim cardimage As Bitmap = CreateCardImage(qrCodeImage, completename, uid)
            SaveCardImage(cardimage, uid)

            Dim initialDaysLeft As Integer = 30
            Using cmdInsertCountdown As New MySqlCommand("INSERT INTO `membership countdown` (UID, MPID, days_left) VALUES (@uid, @mpid, @days_left)", conn)
                cmdInsertCountdown.Parameters.AddWithValue("@uid", uid)
                cmdInsertCountdown.Parameters.AddWithValue("@mpid", memid)
                cmdInsertCountdown.Parameters.AddWithValue("@days_left", initialDaysLeft)
                cmdInsertCountdown.ExecuteNonQuery()
            End Using

            Using cmdStoreQr As New MySqlCommand("UPDATE users SET qr = @qrcode WHERE UID = @uid", conn)
                cmdStoreQr.Parameters.AddWithValue("@qrcode", qrCodeImageBytes)
                cmdStoreQr.Parameters.AddWithValue("@uid", uid)
                cmdStoreQr.ExecuteNonQuery()
            End Using

            Using cmdLog As New MySqlCommand("INSERT INTO `payment logs` (UID, MPID, date_time_payed) VALUES (@uid, @mpid, @date)", conn)
                cmdLog.Parameters.AddWithValue("@uid", uid)
                cmdLog.Parameters.AddWithValue("@mpid", memid)
                cmdLog.Parameters.AddWithValue("@date", DateTime.Now)
                cmdLog.ExecuteNonQuery()
            End Using


            SendEmail(eBox.Text, "Welcome to Our Service", $"Hello {completename}, your registration and payment of {paymentAmount:C} is successful!")

            Dim autoCloseMessageBox As New AutoCloseMessageBox("User added successfully.", "Success", 5000)
            autoCloseMessageBox.ShowDialog()

            Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            dbDisconn()
        End Try
    End Sub


    Private Sub cancelButton_Click(sender As Object, e As EventArgs) Handles cancelButton.Click
        dbDisconn()
        Close()
    End Sub
    Private Sub load()
        Try
            dbConn()
            ComboBox1.Items.Clear()
            ComboBox2.Items.Clear()

            Dim cmd As New MySqlCommand("SELECT membership_plan_name FROM `membership plans`", conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            While reader.Read()
                ComboBox1.Items.Add(reader("membership_plan_name").ToString())
            End While
            reader.Close()

            cmd.CommandText = "SELECT `Sex Name` FROM sex"
            reader = cmd.ExecuteReader()
            While reader.Read()
                ComboBox2.Items.Add(reader("Sex Name").ToString())
            End While
            reader.Close()

        Catch ex As Exception
            MessageBox.Show("Error loading membership plans: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub AddUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        load()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnUploadProfile.Click
        Dim OpenFileDialog1 As New OpenFileDialog

        OpenFileDialog1.Filter = "Picture Files (*.bmp;*.gif;*.jpg;*.png)|*.bmp;*.gif;*.jpg;*.png"
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Using ms As New MemoryStream()
                Dim selectedImage As Image = Image.FromFile(OpenFileDialog1.FileName)
                selectedImage.Save(ms, selectedImage.RawFormat)
                profile = ms.ToArray()
            End Using
            MessageBox.Show("Profile image uploaded successfully.")
        End If
    End Sub

    Private Sub addType_Click(sender As Object, e As EventArgs) Handles addType.Click
        AddMembershipPlan.ShowDialog()
        load()
    End Sub
End Class