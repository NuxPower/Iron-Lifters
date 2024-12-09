Imports MySql.Data.MySqlClient

Public Class PaymentLogs
    Private originalListViewItems As List(Of ListViewItem)
    Private selectedDate As Date

    Private Sub PaymentLogs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        selectedDate = DateTimePicker1.Value.Date
        InitializeListView()
        LoadPayments()
    End Sub

    Private Sub InitializeListView()
        originalListViewItems = New List(Of ListViewItem)()
        With ListView1
            .View = View.Details
            .Columns.Add("Payment ID", 90, HorizontalAlignment.Center)
            .Columns.Add("User ID", 90, HorizontalAlignment.Center)
            .Columns.Add("Complete Name", 200, HorizontalAlignment.Left)
            .Columns.Add("Username", 200, HorizontalAlignment.Left)
            .Columns.Add("Payment Date", 170, HorizontalAlignment.Left)
            .Columns.Add("Membership Plan", 150, HorizontalAlignment.Left)
            .Columns.Add("Amount", 100, HorizontalAlignment.Center)
            '.Columns.Add("Status", 100, HorizontalAlignment.Center)
            .FullRowSelect = True
            .OwnerDraw = True
        End With

        AddHandler ListView1.DrawSubItem, AddressOf ListView1_DrawSubItem
        AddHandler ListView1.DrawColumnHeader, AddressOf ListView1_DrawColumnHeader
        AddHandler ListView1.Resize, AddressOf ListView1_Resize
    End Sub

    Private Sub LoadPayments()
        Try
            dbDisconn()
            dbConn()

            Dim formattedDate As String = selectedDate.ToString("yyyy-MM-dd")
            Dim cmd As New MySqlCommand("
                SELECT 
                    payment.PID, 
                    payment.UID, 
                    users.uname, 
                    users.firstname,
                    users.lastname,
                    payment.date_time_payed, 
                    plan.price, 
                    plan.membership_plan_name, 
                    users.status 
                FROM 
                    `payment logs` AS payment
                JOIN 
                    users ON users.UID = payment.UID
                JOIN 
                    `membership plans` AS plan ON plan.MPID = payment.MPID
                WHERE 
                    DATE(payment.date_time_payed) = '" & formattedDate & "'", conn)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            ListView1.Items.Clear()

            originalListViewItems.Clear()

            While reader.Read()
                Dim paymentInfo As New ListViewItem(reader("PID").ToString())
                paymentInfo.SubItems.Add(reader("UID").ToString())
                paymentInfo.SubItems.Add(reader("lastname").ToString & ", " & reader("firstname").ToString)
                paymentInfo.SubItems.Add(reader("uname").ToString())
                paymentInfo.SubItems.Add(Convert.ToDateTime(reader("date_time_payed")).ToString("dd-MM-yyyy HH:mm:ss"))
                paymentInfo.SubItems.Add(reader("membership_plan_name").ToString())
                paymentInfo.SubItems.Add(Convert.ToDecimal(reader("price")).ToString("₱#,##0.00"))
                'paymentInfo.SubItems.Add(If(reader("status").ToString() = "1", "Paid", "Pending"))

                ListView1.Items.Add(paymentInfo)
                originalListViewItems.Add(paymentInfo)
            End While
            reader.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dbDisconn()
        End Try
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        selectedDate = DateTimePicker1.Value.Date
        LoadPayments()
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

    Private Sub boxSearchUser_TextChanged(sender As Object, e As EventArgs) Handles boxSearchUser.TextChanged
        Dim searchText As String = boxSearchUser.Text.ToLower()
        FilterListView(searchText)
    End Sub

    Private Sub FilterListView(searchText As String)
        ListView1.Items.Clear()

        For Each item As ListViewItem In originalListViewItems
            If item.SubItems.Cast(Of ListViewItem.ListViewSubItem).Any(Function(subItem) subItem.Text.ToLower().Contains(searchText)) Then
                ListView1.Items.Add(item)
            End If
        Next
    End Sub
End Class
