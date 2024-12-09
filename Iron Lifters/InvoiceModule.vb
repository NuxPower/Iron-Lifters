Imports System.Net
Imports System.Net.Mail
Imports System.Windows.Forms

Module InvoiceModule
    Public Sub SendInvoiceEmail(userId As String, userName As String, totalAmount As Decimal, recipientEmail As String)
        Try

            Dim smtpClient As New SmtpClient("smtp.gmail.com") With {
            .Port = 587,
            .EnableSsl = True,
            .Credentials = New System.Net.NetworkCredential("hanyunikul@gmail.com", "lswp ubje txhj znuf")
        }

            Dim mailMessage As New MailMessage() With {
            .From = New MailAddress("hanyunikul@gmail.com"),
            .Subject = "Invoice for Payment - " & userName,
            .Body = $"Hello {userName},{Environment.NewLine}{Environment.NewLine}" &
                    $"Thank you for your payment. The total amount is {totalAmount:C}.{Environment.NewLine}{Environment.NewLine}" &
                    $"Best Regards,{Environment.NewLine}Iron Lifters",
            .IsBodyHtml = False
        }


            mailMessage.To.Add(recipientEmail)


            smtpClient.Send(mailMessage)

            MessageBox.Show("Invoice sent successfully.", "Email Status", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Failed to send email: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub SendRenewalEmail(email As String, fullName As String, username As String, days As Integer)
        Try

            Dim smtpClient As New Net.Mail.SmtpClient("smtp.gmail.com")
            smtpClient.Port = 587
            smtpClient.EnableSsl = True
            smtpClient.Credentials = New Net.NetworkCredential("hanyunikul@gmail.com", "lswp ubje txhj znuf")
            Dim mailMessage As New Net.Mail.MailMessage()
            mailMessage.From = New Net.Mail.MailAddress("hanyunikul@gmail.com")
            mailMessage.To.Add(email)
            If days > 0 Then
                mailMessage.Subject = "Renewal Reminder: Your Membership is About to Expire"
                mailMessage.Body = $"Hello {fullName},{Environment.NewLine}{Environment.NewLine}" &
                               $"This is a reminder that your membership is about to expire in {days} days." &
                               $"{Environment.NewLine}{Environment.NewLine}" &
                               "Please renew your membership to continue enjoying the benefits." &
                               $"{Environment.NewLine}{Environment.NewLine}" &
                               $"Username: {username}" &
                               $"{Environment.NewLine}{Environment.NewLine}" &
                               "Thank you for being a member!"
            ElseIf days = 0 Then
                mailMessage.Subject = "Renewal Reminder: Your Membership has Expired!"
                mailMessage.Body = $"Hello {fullName},{Environment.NewLine}{Environment.NewLine}" &
                               $"Your membership has expired." &
                               $"{Environment.NewLine}{Environment.NewLine}" &
                               "Please renew your membership to continue enjoying the benefits." &
                               $"{Environment.NewLine}{Environment.NewLine}" &
                               $"Username: {username}" &
                               $"{Environment.NewLine}{Environment.NewLine}" &
                               "Thank you for being a member!"
            End If

            smtpClient.Send(mailMessage)

        Catch ex As Exception
            MessageBox.Show("Error sending renewal email: " & ex.Message)
        End Try
    End Sub

    Sub SendEmail(toAddress As String, subject As String, body As String)
        Try
            Dim smtpClient As New SmtpClient("smtp.gmail.com") ' Use your SMTP server details
            smtpClient.Port = 587 ' Use the appropriate port
            smtpClient.Credentials = New Net.NetworkCredential("hanyunikul@gmail.com", "lswp ubje txhj znuf") ' Use your email and password
            smtpClient.EnableSsl = True

            Dim mailMessage As New MailMessage()
            mailMessage.From = New MailAddress("hanyunikul@gmail.com")
            mailMessage.To.Add(toAddress)
            mailMessage.Subject = subject
            mailMessage.Body = body

            smtpClient.Send(mailMessage)
        Catch ex As Exception
            MessageBox.Show("Error sending email: " & ex.Message)
        End Try
    End Sub

End Module
