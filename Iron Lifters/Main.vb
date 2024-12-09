Imports MySql.Data.MySqlClient

Public Class Main
    Dim topbar As String = "Close"

    Sub Clear()
        btnDashboard.Text = ""
        btnDashboard.ImageAlign = ContentAlignment.MiddleLeft
        btnMembers.Text = ""
        btnMembers.ImageAlign = ContentAlignment.MiddleLeft
        btnPaymentLogs.Text = ""
        btnPaymentLogs.ImageAlign = ContentAlignment.MiddleLeft
        btnAttendanceLogs.Text = ""
        btnAttendanceLogs.ImageAlign = ContentAlignment.MiddleLeft
        btnFacultyManagement.Text = ""
        btnFacultyManagement.ImageAlign = ContentAlignment.MiddleLeft
        btnFacultyLogs.Text = ""
        btnFacultyLogs.ImageAlign = ContentAlignment.MiddleLeft
        btnLogout.Text = ""
        btnLogout.ImageAlign = ContentAlignment.MiddleLeft
    End Sub

    Sub SetName()
        btnDashboard.Text = "Dashboard"
        btnDashboard.ImageAlign = ContentAlignment.MiddleRight
        btnMembers.Text = "Members"
        btnMembers.ImageAlign = ContentAlignment.MiddleRight
        btnPaymentLogs.Text = "Payment Logs"
        btnPaymentLogs.ImageAlign = ContentAlignment.MiddleRight
        btnAttendanceLogs.Text = "Attendance Logs"
        btnAttendanceLogs.ImageAlign = ContentAlignment.MiddleRight
        btnFacultyManagement.Text = "Faculty Management"
        btnFacultyManagement.ImageAlign = ContentAlignment.MiddleRight
        btnFacultyLogs.Text = "Faculty Logs"
        btnFacultyLogs.ImageAlign = ContentAlignment.MiddleRight
        btnLogout.Text = "Logout"
        btnLogout.ImageAlign = ContentAlignment.MiddleRight
    End Sub
    Sub menuLoad()
        If topbar = "Open" Then
            sidebar.Width += 150
            imagepanel.Width += 25
            imagepanel.Height += 25
            If sidebar.Width >= 200 Then
                imagepanel.Visible = True
                SetName()
                topbar = "Close"
                Timer1.Stop()
            End If
        Else
            sidebar.Width -= 150
            imagepanel.Width -= 25
            imagepanel.Height -= 25
            If sidebar.Width <= 50 Then
                imagepanel.Visible = False
                Clear()
                topbar = "Open"
                Timer1.Stop()
            End If
        End If
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        menuLoad()
    End Sub


    Private Sub MainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AccessLevelCheck()
        dashboardLoad()
        menuLoad()
        CheckAndUpdateDaysLeft()
    End Sub
    Private Sub LoadUserControl(uc As UserControl)
        If TypeOf uc Is Dashboard Then
            fragment.Controls.Clear()

            Dim scrollablePanel As New Panel With {
            .Dock = DockStyle.Fill,
            .AutoScroll = True
        }

            uc.Dock = DockStyle.Top
            scrollablePanel.Controls.Add(uc)

            fragment.Controls.Add(scrollablePanel)

            Dim dashboardControl As Dashboard = DirectCast(uc, Dashboard)
            dashboardControl.FocusScannerInput()
        Else
            fragment.Controls.Clear()

            uc.Dock = DockStyle.Fill
            fragment.Controls.Add(uc)
        End If
    End Sub



    Private Sub CheckAndUpdateDaysLeft()
        Dim lastCheckedDate As DateTime = My.Settings.LastCheckedDate
        Dim currentDate As DateTime = DateTime.Now.Date

        If lastCheckedDate < currentDate Then
            Dim daysDifference As Integer = (currentDate - lastCheckedDate).Days
            UpdateDaysLeft(daysDifference)
            My.Settings.LastCheckedDate = currentDate
            My.Settings.Save()

        End If
        CheckForRenewalAlerts()
    End Sub
    Private Sub CheckForRenewalAlerts()
        Try
            dbDisconn()
            dbConn()
            Dim cmd As New MySqlCommand("
            SELECT u.firstname, u.lastname, u.uname, u.email, mc.days_left, mc.UID 
            FROM users u 
            JOIN `membership countdown` mc ON u.UID = mc.UID 
            WHERE mc.days_left <= 3 AND mc.emails_sent = 0", conn)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            Dim usersToNotify As New List(Of Tuple(Of String, String, String, Integer, Integer))

            While reader.Read()
                Dim completeName As String = reader("firstname").ToString() & " " & reader("lastname").ToString()
                Dim username As String = reader("uname").ToString()
                Dim email As String = reader("email").ToString()
                Dim daysLeft As Integer = Convert.ToInt32(reader("days_left"))
                Dim uid As Integer = Convert.ToInt32(reader("UID"))

                usersToNotify.Add(Tuple.Create(email, completeName, username, daysLeft, uid))
            End While

            reader.Close()

            For Each user In usersToNotify
                Dim email = user.Item1
                Dim completeName = user.Item2
                Dim username = user.Item3
                Dim daysLeft = user.Item4
                Dim uid = user.Item5

                SendRenewalEmail(email, completeName, username, daysLeft)
                MarkEmailAsSent(uid)
            Next

            If usersToNotify.Count > 0 Then
                MessageBox.Show("Successfully sent renewal email notification to user/s!")
            End If

        Catch ex As Exception
            MessageBox.Show("Error checking for renewal alerts: " & ex.Message)
        Finally
            dbDisconn()
        End Try
    End Sub

    Private Sub MarkEmailAsSent(uid As Integer)
        Try
            dbConn() ' Ensure the connection is open before executing the command
            Dim updateCmd As New MySqlCommand("UPDATE `membership countdown` SET emails_sent = 1 WHERE UID = @UID", conn)
            updateCmd.Parameters.AddWithValue("@UID", uid)
            updateCmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Error updating email sent status: " & ex.Message)
        End Try
    End Sub


    Private Sub UpdateDaysLeft(daysDifference As Integer)
        Try
            dbConn()

            Dim trans As MySqlTransaction = conn.BeginTransaction()

            Dim updateCmd As New MySqlCommand("UPDATE `membership countdown` SET days_left = CASE 
            WHEN days_left - @daysDifference < 0 THEN 0 
            ELSE days_left - @daysDifference 
            END", conn, trans)
            updateCmd.Parameters.AddWithValue("@daysDifference", daysDifference)
            updateCmd.ExecuteNonQuery()

            Dim updateStatusCmd As New MySqlCommand("UPDATE users u JOIN `membership countdown` mc ON u.UID = mc.UID SET u.status = 0 WHERE mc.days_left = 0", conn, trans)
            updateStatusCmd.ExecuteNonQuery()

            trans.Commit()
        Catch ex As Exception
            MessageBox.Show("Error updating days left: " & ex.Message)
        Finally
            dbDisconn()
        End Try
    End Sub
    Private Sub dashboardLoad()
        FragmentTitle.Text = "Dashboard"
        Dim fragment1 As New Dashboard()
        LoadUserControl(fragment1)

        fragment1.FocusScannerInput()
    End Sub

    Private Sub menuButton_Click(sender As Object, e As EventArgs) Handles menuButton.Click
        Timer1.Start()
    End Sub

    Private Sub btnMembers_Click_1(sender As Object, e As EventArgs) Handles btnMembers.Click
        FragmentTitle.Text = "Members"
        Dim fragment1 As New Members()
        LoadUserControl(fragment1)
    End Sub

    Private Sub btnDashboard_Click_1(sender As Object, e As EventArgs) Handles btnDashboard.Click
        dashboardLoad()
    End Sub

    Private Sub btnAttendanceLogs_Click_1(sender As Object, e As EventArgs) Handles btnAttendanceLogs.Click
        FragmentTitle.Text = "Attendance Logs"
        Dim fragment1 As New AttendanceLogs()
        LoadUserControl(fragment1)
    End Sub

    Private Sub btnPaymentLogs_Click(sender As Object, e As EventArgs) Handles btnPaymentLogs.Click
        FragmentTitle.Text = "Payment Logs"
        Dim fragment1 As New PaymentLogs()
        LoadUserControl(fragment1)
    End Sub

    Private Sub btnFacultyManagement_Click(sender As Object, e As EventArgs) Handles btnFacultyManagement.Click
        FragmentTitle.Text = "Faculty Management"
        Dim fragment1 As New FacultyManagement()
        LoadUserControl(fragment1)
    End Sub
    Private Sub btnFacultyLogs_Click(sender As Object, e As EventArgs) Handles btnFacultyLogs.Click
        FragmentTitle.Text = "Faculty Logs"
        Dim fragment1 As New FacultyLogs()
        LoadUserControl(fragment1)
    End Sub
    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        If MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            AdminLoggedModule.SetLogoutTime()

            Try
                dbConn()
                Using cmd As New MySqlCommand("UPDATE falogs SET out_datetime = @out_datetime WHERE FID = @fid AND FALID = " & AdminLoggedModule.LoggedInFALID & "", conn)
                    cmd.Parameters.AddWithValue("@out_datetime", AdminLoggedModule.LoginOutDateTime)
                    cmd.Parameters.AddWithValue("@fid", AdminLoggedModule.LoggedInFID)
                    cmd.ExecuteNonQuery()

                    My.Application.ChangeMainForm(AdminLogin)

                End Using
            Catch ex As Exception
                MessageBox.Show("Error logging out: " & ex.Message, "Error")
            Finally
                If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                    dbDisconn()
                End If
            End Try

            Me.Close()
            AdminLogin.Show()
        Else
            dbDisconn()
        End If
    End Sub

    Private Sub AccessLevelCheck()

        If Not AdminLoggedModule.LoggedInAccessLevel = 2 Then
            btnFacultyManagement.Enabled = False
            btnFacultyManagement.Visible = False
            btnFacultyLogs.Enabled = False
            btnFacultyLogs.Visible = False
        Else

            btnFacultyManagement.Enabled = True
            btnFacultyManagement.Visible = True
            btnFacultyLogs.Enabled = True
            btnFacultyLogs.Visible = True
        End If
    End Sub


End Class
