Imports MySql.Data.MySqlClient

Public Class FacultyManagement
    Private originalListViewItems As List(Of ListViewItem)
    Private checkedItemIDs As New HashSet(Of String)

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles selectDeselect.CheckedChanged
        If selectDeselect.Checked Then
            For Each item As ListViewItem In ListView1.Items
                item.Checked = True
                checkedItemIDs.Add(item.Text)
            Next
        Else
            For Each item As ListViewItem In ListView1.Items
                item.Checked = False
            Next
            checkedItemIDs.Clear()
        End If
    End Sub

    Private Sub LoadUsers()
        Try
            dbDisconn()
            dbConn()

            Dim cmd As New MySqlCommand("
                SELECT 
                    faculty.FID, 
                    faculty.created_at, 
                    faculty.lastname, 
                    faculty.firstname, 
                    faculty.username, 
                    faculty.phone_number, 
                    flevel.level_name, 
                    sex.`Sex Name` 
                FROM faculty
                JOIN flevel ON flevel.FIDLevel = faculty.FIDLevel 
                JOIN sex ON sex.SID = faculty.sex", conn)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            ListView1.Items.Clear()
            Dim tempItems As New List(Of ListViewItem)

            While reader.Read()
                Dim userInfo As New ListViewItem(reader("FID").ToString())
                userInfo.SubItems.Add($"{reader("lastname")}, {reader("firstname")}")
                userInfo.SubItems.Add(reader("Sex Name").ToString())
                userInfo.SubItems.Add(reader("username").ToString())
                userInfo.SubItems.Add(reader("created_at").ToString())
                userInfo.SubItems.Add(reader("phone_number").ToString())
                userInfo.SubItems.Add(reader("level_name").ToString())

                userInfo.Checked = checkedItemIDs.Contains(userInfo.Text)

                ListView1.Items.Add(userInfo)
                tempItems.Add(CType(userInfo.Clone(), ListViewItem))
            End While

            originalListViewItems = tempItems
            reader.Close()

        Catch ex As Exception
            MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dbDisconn()
        End Try
    End Sub

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

    Private Sub ListView1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles ListView1.ItemCheck
        Dim itemID As String = ListView1.Items(e.Index).Text

        If e.NewValue = CheckState.Checked Then
            checkedItemIDs.Add(itemID)
        Else
            checkedItemIDs.Remove(itemID)
        End If
    End Sub

    Private Sub FacultyManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeImageList(ListView1)

        With ListView1
            .View = View.Details
            .Columns.Add("ID", 50, HorizontalAlignment.Right)
            .Columns.Add("Complete Name", 250, HorizontalAlignment.Left)
            .Columns.Add("Sex", 70, HorizontalAlignment.Center)
            .Columns.Add("Username", 150, HorizontalAlignment.Left)
            .Columns.Add("Date Registered", 250, HorizontalAlignment.Left)
            .Columns.Add("Phone Number", 200, HorizontalAlignment.Left)
            .Columns.Add("Faculty Access Level", 100, HorizontalAlignment.Center)
            .CheckBoxes = True
            .FullRowSelect = True
            .OwnerDraw = True
        End With

        AddHandler ListView1.DrawSubItem, AddressOf ListView1_DrawSubItem
        AddHandler ListView1.DrawColumnHeader, AddressOf ListView1_DrawColumnHeader
        AddHandler ListView1.Resize, AddressOf ListView1_Resize

        LoadUsers()
    End Sub
    Private Sub ListView1_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs)
        Dim bounds = e.Bounds

        If e.Item.Selected Then
            e.Graphics.FillRectangle(Brushes.LightBlue, bounds)
        Else
            e.Graphics.FillRectangle(If(e.ItemIndex Mod 2 = 0, Brushes.LightGray, Brushes.White), bounds)
        End If

        If e.ColumnIndex = 0 Then
            Dim checkBoxBounds As New Rectangle(bounds.X + 3, bounds.Y + (bounds.Height - 16) \ 2, 16, 16)
            ControlPaint.DrawCheckBox(e.Graphics, checkBoxBounds, If(e.Item.Checked, ButtonState.Checked, ButtonState.Normal))
        End If

        e.DrawText()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        dbConn()

        Dim count As Integer = 0
        Dim superAdminCmd As New MySqlCommand("SELECT COUNT(*) FROM faculty WHERE FIDLevel = 2", conn)
        count = Convert.ToInt32(superAdminCmd.ExecuteScalar())
        If ListView1.CheckedItems.Count = 0 Then
            MessageBox.Show("Select a user first!!")
            conn.Close()
            Return
        End If

        Dim superAdminCount As Integer = 0
        For Each item As ListViewItem In ListView1.CheckedItems
            If item.SubItems(2).Text = "Super Admin" Then
                superAdminCount += 1
            End If
        Next

        Dim LoggedInFID As Integer = AdminLoggedModule.LoggedInFID

        If MessageBox.Show("Are you sure you want to delete the selected faculty?", "Confirm Disable", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                For Each item As ListViewItem In ListView1.CheckedItems
                    Dim FID As Integer = Integer.Parse(item.SubItems(0).Text)

                    If FID = LoggedInFID Then
                        MessageBox.Show("You cannot delete your own account!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If

                    Dim deleteCmd As New MySqlCommand("DELETE FROM faculty WHERE FID = @FID", conn)
                    deleteCmd.Parameters.AddWithValue("@FID", FID)
                    deleteCmd.ExecuteNonQuery()
                Next

                MessageBox.Show("Selected users have been deleted successfully.")
                LoadUsers()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            Finally
                dbDisconn()
            End Try
        Else
            dbDisconn()
        End If

        originalListViewItems = ListView1.Items.Cast(Of ListViewItem).ToList()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadUsers()
    End Sub

    Private Sub btnAddFaculty_Click(sender As Object, e As EventArgs) Handles btnAddFaculty.Click
        AddFaculty.ShowDialog()
        LoadUsers()
    End Sub
End Class
