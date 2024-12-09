Imports System.IO
Imports System.Text
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Windows.Media.Media3D
Imports MySql.Data.MySqlClient

Public Class Dashboard
    Private originalListViewItems As List(Of ListViewItem)
    Private scannerFocusTimer As New Timer()
    Dim messageBoxShown As Boolean = False
    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigureChart()

        FetchAndBindAttendanceData()

        LoadUsers()

        originalListViewItems = ListView1.Items.Cast(Of ListViewItem).ToList()

        With ListView1
            .View = View.Details
            .Columns.Add("Attendance ID", 90, HorizontalAlignment.Center)
            .Columns.Add("Complete Name", 200, HorizontalAlignment.Left)
            .Columns.Add("Sex", 70, HorizontalAlignment.Center)
            .Columns.Add("Username", 200, HorizontalAlignment.Left)
            .Columns.Add("Phone Number", 150, HorizontalAlignment.Left)
            .Columns.Add("In", 150, HorizontalAlignment.Left)
            .Columns.Add("Out", 150, HorizontalAlignment.Left)
            .Columns.Add("Date", 170, HorizontalAlignment.Left)
            .Columns.Add("Days Left", 100, HorizontalAlignment.Center)

            .FullRowSelect = True
            .OwnerDraw = True
        End With

        AddHandler ListView1.DrawSubItem, AddressOf Me.ListView1_DrawSubItem
        AddHandler ListView1.DrawColumnHeader, AddressOf ListView1_DrawColumnHeader
        AddHandler ListView1.Resize, AddressOf ListView1_Resize
    End Sub

    Private Sub Dashboard_Enter(sender As Object, e As EventArgs) Handles MyBase.Enter
        txtScannerInput.Focus()
    End Sub

    Private Sub Dashboard_GotFocus(sender As Object, e As EventArgs) Handles MyBase.GotFocus
        txtScannerInput.Focus()
    End Sub
    Private Sub EnsureScannerFocus(sender As Object, e As EventArgs)
        If Me.ParentForm Is Nothing Then Return

        If Me.ParentForm.ActiveControl Is Me OrElse Me.ContainsFocus Then
            If Not txtScannerInput.Focused Then
                txtScannerInput.Focus()
            End If
        End If
    End Sub
    Public Sub FocusScannerInput()
        If txtScannerInput IsNot Nothing AndAlso txtScannerInput.Visible Then
            txtScannerInput.Focus()
        End If
    End Sub


    Private Sub Dashboard_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        scannerFocusTimer.Stop()
        scannerFocusTimer.Dispose()
    End Sub

    Private Sub ListView1_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs)
        If e.Item.Selected Then
            e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds)
        Else
            If (e.ItemIndex Mod 2) = 0 Then
                e.Graphics.FillRectangle(Brushes.LightGray, e.Bounds)
            Else
                e.Graphics.FillRectangle(Brushes.White, e.Bounds)
            End If
        End If

        Dim alignment As TextFormatFlags = TextFormatFlags.Left
        Select Case e.Header.TextAlign
            Case HorizontalAlignment.Left
                alignment = TextFormatFlags.Left
            Case HorizontalAlignment.Center
                alignment = TextFormatFlags.HorizontalCenter
            Case HorizontalAlignment.Right
                alignment = TextFormatFlags.Right
        End Select

        TextRenderer.DrawText(e.Graphics, e.SubItem.Text, ListView1.Font, e.Bounds, ListView1.ForeColor, alignment)
    End Sub

    Private Sub ConfigureChart()
        Chart1.Series.Clear()

        Dim series As New Series("Attendance")
        series.ChartType = SeriesChartType.Bar
        series.XValueType = ChartValueType.Date
        series.IsValueShownAsLabel = True
        series.Color = Color.Blue

        Chart1.Series.Add(series)

        Dim chartArea As New ChartArea()
        chartArea.AxisX.LabelStyle.Format = "dd-MM"
        chartArea.AxisX.Interval = 1
        chartArea.AxisX.MajorGrid.LineColor = Color.LightGray
        chartArea.AxisY.MajorGrid.LineColor = Color.LightGray
        Chart1.ChartAreas.Clear()
        Chart1.ChartAreas.Add(chartArea)

        Chart1.Titles.Clear()
    End Sub

    Private Sub FetchAndBindAttendanceData()
        Dim attendanceData As New Dictionary(Of Date, Integer)

        Try
            dbDisconn()
            dbConn()

            Dim query As String = "SELECT DATE(`Date`) AS `Date`, COUNT(*) AS attendance_count " &
                              "FROM `attendance logs` " &
                              "WHERE MONTH(`Date`) = MONTH(CURRENT_DATE()) " &
                              "AND YEAR(`Date`) = YEAR(CURRENT_DATE()) " &
                              "GROUP BY DATE(`Date`);"

            Using cmd As New MySqlCommand(query, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim attendanceDate As Date = reader.GetDateTime("Date")
                        Dim attendanceCount As Integer = reader.GetInt32("attendance_count")
                        If Not attendanceData.ContainsKey(attendanceDate) Then
                            attendanceData.Add(attendanceDate, attendanceCount)
                        End If
                    End While
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching attendance data: " & ex.Message)
        Finally
            dbDisconn()
        End Try

        For Each kvp In attendanceData
            Console.WriteLine("Date: " & kvp.Key.ToString("dd-MM-yyyy") & ", Count: " & kvp.Value)
        Next

        Chart1.Series("Attendance").Points.Clear()
        For Each kvp In attendanceData
            Chart1.Series("Attendance").Points.AddXY(kvp.Key, kvp.Value)
        Next

        Chart1.Invalidate()
    End Sub


    Private Sub LoadUsers()
        FetchAndBindAttendanceData()

        Try
            dbDisconn()
            dbConn()
            Dim cmd As New MySqlCommand("
            SELECT
                users.UID,
                users.lastname,
                users.firstname,
                users.sex,
                users.uname,
                users.phone_no,
                attendance.AID,
                attendance.`In`,
                attendance.`Out`,
                attendance.`Date`,
                membership.days_left
            FROM
                users
            JOIN
                `attendance logs` AS attendance
            ON 
                attendance.UID = users.UID
            JOIN
                `membership countdown` AS membership
            ON
                membership.UID = users.UID
            WHERE
                DATE(attendance.Date) = CURDATE()
            ORDER BY attendance.AID ASC", conn)


            Using cmd
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    ListView1.Items.Clear()

                    Dim dataFound As Boolean = False

                    While reader.Read()
                        dataFound = True

                        Dim userInfo As New ListViewItem(reader("AID").ToString())
                        Dim completename As String = reader("lastname").ToString() & ", " & reader("firstname").ToString()
                        userInfo.SubItems.Add(completename)

                        Dim sexID As Integer = reader("sex")
                        Dim sex As String = If(sexID = 1, "Male", "Female")
                        userInfo.SubItems.Add(sex)

                        userInfo.SubItems.Add(reader("uname").ToString())
                        userInfo.SubItems.Add(reader("phone_no").ToString())
                        userInfo.SubItems.Add(reader("In").ToString())
                        userInfo.SubItems.Add(reader("Out").ToString())

                        Dim attendanceDate As DateTime = reader.GetDateTime("Date")
                        userInfo.SubItems.Add(attendanceDate.ToString("dd-MM-yyyy"))

                        userInfo.SubItems.Add(reader("days_left").ToString())

                        ListView1.Items.Add(userInfo)
                    End While
                    'If Not dataFound AndAlso Not messageBoxShown Then
                    '    MessageBox.Show("No data found for today's date.", "Information")
                    '    messageBoxShown = True ' Prevent further message box pop-ups
                    'End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading users: " & ex.Message, "Error")
        Finally
            dbDisconn()
        End Try
    End Sub


    Private Sub boxSearchUser_TextChanged(sender As Object, e As EventArgs) Handles boxSearchUser.TextChanged
        Dim searchText As String = boxSearchUser.Text.ToLower()
        FilterListView(searchText)
    End Sub


    Private Sub FilterListView(searchText As String)
        If String.IsNullOrEmpty(searchText) Then
            ListView1.Items.Clear()
            ListView1.Items.AddRange(originalListViewItems.ToArray())
        Else
            ListView1.Items.Clear()
            For Each item As ListViewItem In originalListViewItems
                If item.SubItems.Cast(Of ListViewItem.ListViewSubItem).Any(Function(subItem) subItem.Text.ToLower().Contains(searchText)) Then
                    ListView1.Items.Add(item)
                End If
            Next
        End If
    End Sub
    Private Sub txtScannerInput_KeyDown(sender As Object, e As KeyEventArgs) Handles txtScannerInput.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim scannedData As String = txtScannerInput.Text.Trim()

            If Not String.IsNullOrEmpty(scannedData) Then
                ProcessAttendance(scannedData)
            Else
                MessageBox.Show("Invalid scan data.", "Error")
            End If

            txtScannerInput.Clear()
        End If
    End Sub
    Private Sub ProcessAttendance(uid As String)
        Try
            Using conn
                dbConn()

                Dim todayDate As String = Date.Today.ToString("yyyy-MM-dd")

                Dim queryCheck As String = "SELECT al.AID, al.`In`, al.`Out`, u.status, u.lastname, u.firstname, u.image " &
                                           "FROM `attendance logs` al " &
                                           "JOIN `users` u ON al.UID = u.UID " &
                                           "WHERE al.UID = @UID AND DATE(al.`Date`) = @Today " &
                                           "ORDER BY al.`In` DESC LIMIT 1;"
                Dim existingId As Integer = 0
                Dim hasInTime As Boolean = False
                Dim hasOutTime As Boolean = False
                Dim userStatus As Integer = 0
                Dim recordFound As Boolean = False
                Dim completename As String = String.Empty
                Dim profileImage() As Byte = Nothing

                Using cmdCheck As New MySqlCommand(queryCheck, conn)
                    cmdCheck.Parameters.AddWithValue("@UID", uid)
                    cmdCheck.Parameters.AddWithValue("@Today", todayDate)

                    Using reader As MySqlDataReader = cmdCheck.ExecuteReader()
                        If reader.Read() Then
                            existingId = reader.GetInt32("AID")
                            hasInTime = Not IsDBNull(reader("In"))
                            hasOutTime = Not IsDBNull(reader("Out"))
                            completename = reader("lastname").ToString & ", " & reader("firstname").ToString
                            userStatus = reader.GetInt32("status")
                            profileImage = If(IsDBNull(reader("image")), Nothing, CType(reader("image"), Byte()))
                            recordFound = True
                        End If
                    End Using
                End Using

                If Not recordFound Then
                    Dim queryStatus As String = "SELECT status, lastname, firstname, image FROM `users` WHERE UID = @UID;"
                    Using cmdStatus As New MySqlCommand(queryStatus, conn)
                        cmdStatus.Parameters.AddWithValue("@UID", uid)
                        Using reader As MySqlDataReader = cmdStatus.ExecuteReader()
                            If reader.Read() Then
                                userStatus = reader.GetInt32("status")
                                completename = reader("lastname").ToString & ", " & reader("firstname").ToString
                                profileImage = If(IsDBNull(reader("image")), Nothing, CType(reader("image"), Byte()))
                            End If
                        End Using
                    End Using
                End If

                If userStatus = 0 Then

                    Dim autoCloseMessageBox As New AutoCloseMessageBox("Attendance is rejected. User status is disabled.", "Error", 5000)
                    autoCloseMessageBox.ShowDialog()

                    Return
                End If

                If existingId > 0 Then
                    If hasInTime And Not hasOutTime Then
                        Dim queryUpdateOut As String = "UPDATE `attendance logs` SET `Out` = CURTIME() WHERE AID = @ID;"
                        Using cmdUpdateOut As New MySqlCommand(queryUpdateOut, conn)
                            cmdUpdateOut.Parameters.AddWithValue("@ID", existingId)
                            cmdUpdateOut.ExecuteNonQuery()
                        End Using
                        Dim autoCloseMessageBox As New AutoCloseMessageBox("Out time logged for: " & completename, "Success", 5000)
                        autoCloseMessageBox.ShowDialog()
                    Else
                        Dim queryInsertNewVisit As String = "INSERT INTO `attendance logs` (UID, `In`) VALUES (@UID, CURTIME());"
                        Using cmdInsertNewVisit As New MySqlCommand(queryInsertNewVisit, conn)
                            cmdInsertNewVisit.Parameters.AddWithValue("@UID", uid)
                            cmdInsertNewVisit.ExecuteNonQuery()
                        End Using
                        Dim autoCloseMessageBox As New AutoCloseMessageBox("New attendance record created with In time for: " & completename, "Success", 5000)
                        autoCloseMessageBox.ShowDialog()
                    End If
                Else
                    Dim queryInsert As String = "INSERT INTO `attendance logs` (UID, `In`) VALUES (@UID, CURTIME());"
                    Using cmdInsert As New MySqlCommand(queryInsert, conn)
                        cmdInsert.Parameters.AddWithValue("@UID", uid)
                        cmdInsert.ExecuteNonQuery()
                    End Using
                    Dim autoCloseMessageBox As New AutoCloseMessageBox("New attendance record created with In time for: " & completename, "Success", 5000)
                    autoCloseMessageBox.ShowDialog()
                End If

                LoadUserDetails(existingId, completename, profileImage)

                LoadUsers()
            End Using
        Catch ex As Exception
            MessageBox.Show("Error processing attendance: " & ex.Message, "Error")
        End Try
    End Sub
    Private Sub LoadUserDetails(attendanceID As Integer, completeName As String, profileImage() As Byte)
        lblAID.Text = attendanceID.ToString()
        lblCName.Text = completeName

        If profileImage IsNot Nothing Then
            Using ms As New MemoryStream(profileImage)
                imgProfile.Image = Image.FromStream(ms)
            End Using
        Else
            imgProfile.Image = Nothing
        End If
    End Sub

End Class
