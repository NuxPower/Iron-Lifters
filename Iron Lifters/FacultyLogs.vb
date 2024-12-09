Imports MySql.Data.MySqlClient

Public Class FacultyLogs
    Private originalListViewItems As List(Of ListViewItem)
    Private selectedDate As Date

    Private Sub AttendanceLogs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        selectedDate = DateTimePicker1.Value.Date
        InitializeListView()
        LoadUsers()
    End Sub

    Private Sub InitializeListView()
        originalListViewItems = New List(Of ListViewItem)()
        With ListView1
            .View = View.Details
            .Columns.Add("Attendance ID", 90, HorizontalAlignment.Center)
            .Columns.Add("Complete Name", 200, HorizontalAlignment.Left)
            .Columns.Add("Sex", 70, HorizontalAlignment.Center)
            .Columns.Add("Username", 200, HorizontalAlignment.Left)
            .Columns.Add("In", 150, HorizontalAlignment.Left)
            .Columns.Add("Out", 150, HorizontalAlignment.Left)
            .FullRowSelect = True
            .OwnerDraw = True
        End With

        AddHandler ListView1.DrawSubItem, AddressOf ListView1_DrawSubItem
        AddHandler ListView1.DrawColumnHeader, AddressOf ListView1_DrawColumnHeader
        AddHandler ListView1.Resize, AddressOf ListView1_Resize
    End Sub

    Private Sub LoadUsers()
        Try
            dbDisconn()
            dbConn()

            Dim formattedDate As String = selectedDate.ToString("yyyy-MM-dd")
            Dim cmd As New MySqlCommand("
                SELECT 
                    faculty.FID, 
                    faculty.lastname, 
                    faculty.firstname, 
                    faculty.sex, 
                    faculty.username, 
                    falogs.FALID,
                    falogs.in_datetime, 
                    falogs.out_datetime
                FROM 
                    faculty 
                JOIN 
                    falogs 
                ON 
                    falogs.FID = faculty.FID 
                WHERE 
                    DATE(falogs.in_datetime) = '" & formattedDate & "'
                ORDER BY falogs.FALID ASC", conn)


            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            ListView1.Items.Clear()

            originalListViewItems.Clear()

            While reader.Read()
                Dim userInfo As New ListViewItem(reader("FALID").ToString())
                Dim completename As String = reader("lastname").ToString() & ", " & reader("firstname").ToString()
                userInfo.SubItems.Add(completename)

                Dim sexID As Integer = reader("sex")
                Dim sex As String = If(sexID = 1, "Male", "Female")
                userInfo.SubItems.Add(sex)
                userInfo.SubItems.Add(reader("username").ToString())
                userInfo.SubItems.Add(reader("in_datetime").ToString())
                userInfo.SubItems.Add(reader("out_datetime").ToString())
                ListView1.Items.Add(userInfo)

                originalListViewItems.Add(userInfo)
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
        LoadUsers()
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
