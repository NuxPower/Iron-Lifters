Imports System.IO
Imports System.Windows.Documents
Imports MySql.Data.MySqlClient



Public Class PaymentProcess
    Private originalListViewItems As List(Of ListViewItem)
    Private checkedItemIDs As New HashSet(Of String)

    Private Sub boxSearchUser_TextChanged(sender As Object, e As EventArgs) Handles boxSearchUser.TextChanged
        Dim searchText As String = boxSearchUser.Text.Trim()

        If String.IsNullOrEmpty(searchText) Then
            LoadUsers()
        Else
            FilterListView(searchText)
        End If
    End Sub
    Private Sub FilterListView(searchText As String)
        Try
            ListView1.Items.Clear()

            For Each item As ListViewItem In originalListViewItems
                If item.SubItems.Cast(Of ListViewItem.ListViewSubItem).Any(Function(subItem) subItem.Text.ToLower().Contains(searchText.ToLower())) Then
                    Dim clonedItem = CType(item.Clone(), ListViewItem)

                    clonedItem.Checked = checkedItemIDs.Contains(clonedItem.Text)

                    ListView1.Items.Add(clonedItem)
                End If
            Next

        Catch ex As Exception
            MessageBox.Show($"Error filtering ListView: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PaymentProcess_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeImageList(ListView1)
        InitializeListView()
        LoadUsers()
        originalListViewItems = ListView1.Items.Cast(Of ListViewItem).ToList()
    End Sub

    Private Sub InitializeListView()
        If ListView1.Columns.Count = 0 Then
            With ListView1
                .View = View.Details
                .Columns.Add("UID", 50, HorizontalAlignment.Right)
                .Columns.Add("Complete Name", 150, HorizontalAlignment.Left)
                .Columns.Add("Status", 50, HorizontalAlignment.Center)
                .Columns.Add("Email", 150, HorizontalAlignment.Left)
                .Columns.Add("Date Registered", 150, HorizontalAlignment.Left)
                .Columns.Add("Membership Plan", 100, HorizontalAlignment.Left)
                .Columns.Add("Days Left", 100, HorizontalAlignment.Center)
                .Columns.Add("Price", 100, HorizontalAlignment.Center)
                .CheckBoxes = True
                .FullRowSelect = True
                .OwnerDraw = True
            End With
        End If


        AddHandler ListView1.DrawSubItem, AddressOf ListView1_DrawSubItem
        AddHandler ListView1.DrawColumnHeader, AddressOf ListView1_DrawColumnHeader
        AddHandler ListView1.Resize, AddressOf ListView1_Resize
    End Sub

    Private Sub LoadUsers()
        Try
            dbDisconn()
            dbConn()
            Dim cmd As New MySqlCommand("
            SELECT 
                users.UID, 
                users.created_at, 
                users.lastname, 
                users.firstname, 
                users.email,
                users.uname, 
                users.status, 
                mplans.MPID, 
                mplans.membership_plan_name, 
                mplans.price, 
                membership.days_left
            FROM 
                users 
            JOIN 
                `membership countdown` AS membership 
            ON 
                membership.UID = users.UID 
            JOIN 
                `membership plans` AS mplans 
            ON 
                users.MPID = mplans.MPID
            ORDER BY users.UID ASC", conn)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            ListView1.Items.Clear()
            Dim tempItems As New List(Of ListViewItem)
            While reader.Read()
                Dim userInfo As New ListViewItem(reader("UID").ToString())
                Dim completename As String = reader("lastname").ToString() & ", " & reader("firstname").ToString()
                userInfo.SubItems.Add(completename)
                If reader("status") = 1 Then
                    userInfo.SubItems.Add("")
                    userInfo.SubItems(2).Tag = "active"
                Else
                    userInfo.SubItems.Add("")
                    userInfo.SubItems(2).Tag = "disabled"
                End If
                userInfo.SubItems.Add(reader("email").ToString())
                userInfo.SubItems.Add(Convert.ToDateTime(reader("created_at")).ToString("dd-MM-yyyy"))
                'If reader("MPID") = 1 Then
                '    userInfo.SubItems.Add("Without Coach")
                'ElseIf reader("MPID") = 2 Then
                '    userInfo.SubItems.Add("With Coach")
                'End If
                userInfo.SubItems.Add(reader("membership_plan_name").ToString)
                userInfo.SubItems.Add(reader("days_left").ToString())
                userInfo.SubItems.Add(FormatCurrency(reader("price")))
                userInfo.Checked = checkedItemIDs.Contains(userInfo.Text)

                ListView1.Items.Add(userInfo)
                tempItems.Add(CType(userInfo.Clone(), ListViewItem))
            End While
            originalListViewItems = tempItems
            reader.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dbDisconn()
        End Try
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

        If e.ColumnIndex = 0 Then
            Dim checkBoxBounds As New Rectangle(e.Bounds.X + 3, e.Bounds.Y + (e.Bounds.Height - 16) / 2, 16, 16)
            Dim checkBoxChecked As Boolean = e.Item.Checked
            ControlPaint.DrawCheckBox(e.Graphics, checkBoxBounds, If(checkBoxChecked, ButtonState.Checked, ButtonState.Normal))
        End If

        If e.ColumnIndex <> 2 Then
            e.DrawText()
        Else
            Dim imageKey As String = e.Item.SubItems(e.ColumnIndex).Tag.ToString()
            Dim image As Image = imageList.Images(imageKey)
            Dim xPos As Integer = e.Bounds.Left + (e.Bounds.Width - image.Width) \ 2
            Dim yPos As Integer = e.Bounds.Top + (e.Bounds.Height - image.Height) \ 2
            e.Graphics.DrawImage(image, xPos, yPos)
        End If
    End Sub
    Private Sub ListView1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles ListView1.ItemCheck
        Dim itemID As String = ListView1.Items(e.Index).Text

        If e.NewValue = CheckState.Checked Then
            checkedItemIDs.Add(itemID)
        Else
            checkedItemIDs.Remove(itemID)
        End If
    End Sub
    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Dim paidUsers As New List(Of String)

        For Each item As ListViewItem In ListView1.CheckedItems
            Dim userId As String = item.Text
            Dim userName As String = item.SubItems(1).Text
            Dim mpid As String = item.SubItems(5).Text
            Dim mpidfinal As Integer
            Dim totalAmount As Decimal
            Dim recipientEmail As String = item.SubItems(3).Text

            Dim priceText As String = item.SubItems(7).Text.Replace("₱", "").Replace(",", "")
            If Not Decimal.TryParse(priceText, totalAmount) Then
                MessageBox.Show($"Invalid price format for user {userName}: '{item.SubItems(7).Text}'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Continue For
            End If

            Try
                dbDisconn()
                dbConn()

                Dim mpidCheckCmd As New MySqlCommand("SELECT MPID FROM `membership plans` WHERE membership_plan_name = @mpid", conn)
                mpidCheckCmd.Parameters.AddWithValue("@mpid", mpid)
                Dim mpidReader As MySqlDataReader = mpidCheckCmd.ExecuteReader()
                If mpidReader.Read() Then
                    mpidfinal = mpidReader("MPID")
                End If
                mpidReader.Close()

                Dim statusCheckCmd As New MySqlCommand("SELECT status FROM users WHERE UID = @userId", conn)
                statusCheckCmd.Parameters.AddWithValue("@userId", userId)
                Dim statusReader As MySqlDataReader = statusCheckCmd.ExecuteReader()

                If statusReader.Read() Then
                    If statusReader("status") = 0 Then
                        statusReader.Close()
                        Dim updateStatusCmd As New MySqlCommand("UPDATE users SET status = 1 WHERE UID = @userId", conn)
                        updateStatusCmd.Parameters.AddWithValue("@userId", userId)
                        updateStatusCmd.ExecuteNonQuery()
                    Else
                        statusReader.Close()
                    End If
                End If

                Dim updateCmd As New MySqlCommand("UPDATE `membership countdown` SET days_left = days_left + 30, email_sent = 0 WHERE UID = @userId", conn)
                updateCmd.Parameters.AddWithValue("@userId", userId)
                updateCmd.ExecuteNonQuery()

                Dim insertLogCmd As New MySqlCommand("INSERT INTO `payment logs` (UID, MPID) VALUES (@userId, @mpidfinal)", conn)
                insertLogCmd.Parameters.AddWithValue("@userId", userId)
                insertLogCmd.Parameters.AddWithValue("@mpidfinal", mpidfinal)
                insertLogCmd.ExecuteNonQuery()

                Dim updateCreatedCmd As New MySqlCommand("UPDATE users SET created_at = @createdAt WHERE UID = @userId", conn)
                updateCreatedCmd.Parameters.AddWithValue("@createdAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                updateCreatedCmd.Parameters.AddWithValue("@userId", userId)
                updateCreatedCmd.ExecuteNonQuery()

                paidUsers.Add(userName)

                InvoiceModule.SendInvoiceEmail(userId, userName, totalAmount, recipientEmail)

                LoadUsers()

                originalListViewItems = ListView1.Items.Cast(Of ListViewItem).ToList()

            Catch ex As Exception
                MessageBox.Show($"Error processing payment for {userName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                dbDisconn()
            End Try
        Next

        If paidUsers.Count > 0 Then
            Dim message As String = "Payment Confirmed for the following users:" & Environment.NewLine & String.Join(Environment.NewLine, paidUsers)
            MessageBox.Show(message, "Payment Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("No users selected for payment.", "Payment Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub



    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        dbDisconn()
        Close()
    End Sub

End Class
