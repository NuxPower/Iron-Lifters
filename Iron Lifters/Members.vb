Imports System.IO
Imports MySql.Data.MySqlClient
Imports System.Drawing.Imaging
Imports System.Drawing
Imports QRCoder

Public Class Members
    Private originalListViewItems As List(Of ListViewItem)
    Private checkedItemIDs As New HashSet(Of String)
    Private Sub LoadUsers()
        Try
            dbDisconn()
            dbConn()

            Dim cmd As New MySqlCommand("SELECT users.UID, users.created_at, users.lastname, users.firstname, users.sex, users.uname, users.status, users.MPID, membership.MCID, membership.days_left, sex.`Sex Name`, mp.membership_plan_name 
                                      FROM users 
                                      JOIN `membership countdown` AS membership ON membership.UID = users.UID 
                                      JOIN sex ON sex.SID = users.sex 
                                      JOIN `membership plans` AS mp ON mp.MPID = users.MPID
                                      ORDER BY users.UID ASC", conn)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            ListView1.Items.Clear()
            Dim tempItems As New List(Of ListViewItem)

            While reader.Read()
                Dim userInfo As New ListViewItem(reader("UID").ToString())
                Dim completename As String = $"{reader("lastname")}, {reader("firstname")}"
                userInfo.SubItems.Add(completename)
                userInfo.SubItems.Add(reader("Sex Name").ToString)

                If reader("status") = 1 Then
                    userInfo.SubItems.Add("")
                    userInfo.SubItems(3).Tag = "active"
                Else
                    userInfo.SubItems.Add("")
                    userInfo.SubItems(3).Tag = "disabled"
                End If

                userInfo.SubItems.Add(reader("uname").ToString())
                userInfo.SubItems.Add(reader("created_at").ToString())
                userInfo.SubItems.Add(reader("membership_plan_name").ToString())
                userInfo.SubItems.Add(reader("days_left").ToString())

                ' Retain checked state for previously checked items
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

    Private Sub Members_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeImageList(ListView1)
        With ListView1
            .View = View.Details
            .Columns.Add("UID", 50, HorizontalAlignment.Right)
            .Columns.Add("Complete Name", 250, HorizontalAlignment.Left)
            .Columns.Add("Sex", 70, HorizontalAlignment.Center)
            .Columns.Add("Status", 50, HorizontalAlignment.Center)
            .Columns.Add("Username", 150, HorizontalAlignment.Left)
            .Columns.Add("Date Registered", 250, HorizontalAlignment.Left)
            .Columns.Add("Membership Plan", 200, HorizontalAlignment.Left)
            .Columns.Add("Days Left", 100, HorizontalAlignment.Center)
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
        If e.ColumnIndex <> 3 Then
            e.DrawText()
        Else
            Dim imageKey As String = e.Item.SubItems(e.ColumnIndex).Tag.ToString()
            Dim image As Image = imageList.Images(imageKey)

            Dim xPos As Integer = e.Bounds.Left + (e.Bounds.Width - image.Width) \ 2
            Dim yPos As Integer = e.Bounds.Top + (e.Bounds.Height - image.Height) \ 2

            e.Graphics.DrawImage(image, xPos, yPos)
        End If
    End Sub
    Private Sub btnRetrieve_Click(sender As Object, e As EventArgs) Handles btnRetrieve.Click
        LoadUsers()
    End Sub

    Private Sub disableButton_Click(sender As Object, e As EventArgs) Handles btnDisable.Click
        dbConn()
        If ListView1.CheckedItems.Count = 0 Then
            MessageBox.Show("Select a user first!!")
            dbDisconn()
            Return
        End If

        If MessageBox.Show("Are you sure you want to disable the selected users?", "Confirm Disable", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                For Each item As ListViewItem In ListView1.CheckedItems
                    Dim uid As Integer = Integer.Parse(item.SubItems(0).Text)
                    Dim disableCmd As New MySqlCommand("UPDATE users SET status = 0 WHERE UID = '" & uid & "'", conn)
                    disableCmd.ExecuteNonQuery()
                Next

                MessageBox.Show("Selected users have been disabled successfully.")
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

    Private Sub btnAddMember_Click(sender As Object, e As EventArgs) Handles btnAddMember.Click
        AddMember.Show()
        LoadUsers()
    End Sub

    Private Sub btnPayed_Click(sender As Object, e As EventArgs) Handles btnPayed.Click
        PaymentProcess.ShowDialog()
        LoadUsers()
    End Sub

    Private Sub btnPrintCard_Click(sender As Object, e As EventArgs) Handles btnPrintCard.Click
        If ListView1.CheckedItems.Count = 0 Then
            MessageBox.Show("Please check a member to print the card.")
            Return
        End If

        For Each selectedItem As ListViewItem In ListView1.CheckedItems
            Dim uid As String = selectedItem.SubItems(0).Text
            Dim fullName As String = selectedItem.SubItems(1).Text

            Try
                ' Query QR code from the database
                Dim qrCodeImage As Bitmap = GetQRCodeFromDatabase(uid)

                ' Create card image with QR code, full name, and UID
                Dim cardImage As Bitmap = CreateCardImage(qrCodeImage, fullName, uid)

                ' Save the card image as a JPEG file
                SaveCardImage(cardImage, uid)

                MessageBox.Show("Card has been printed and saved successfully for " & fullName)
            Catch ex As Exception
                MessageBox.Show("Error printing card for " & fullName & ": " & ex.Message)
            End Try
        Next
    End Sub



    Private Function GetQRCodeFromDatabase(uid As String) As Bitmap
        Dim qrCodeImage As Bitmap = Nothing

        Try
            dbConn()
            Dim cmd As New MySqlCommand("SELECT qr FROM users WHERE UID = @UID", conn)
            cmd.Parameters.AddWithValue("@UID", uid)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.Read() AndAlso Not IsDBNull(reader("qr")) Then
                Dim qrCodeBytes As Byte() = DirectCast(reader("qr"), Byte())
                Using ms As New MemoryStream(qrCodeBytes)
                    qrCodeImage = New Bitmap(ms)
                End Using
            End If
            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Error retrieving QR code from database: " & ex.Message)
        Finally
            dbDisconn()
        End Try

        If qrCodeImage Is Nothing Then
            Throw New Exception("QR code not found for UID: " & uid)
        End If

        Return qrCodeImage
    End Function

    Private Sub btnPlan_Click(sender As Object, e As EventArgs) Handles btnPlan.Click
        AddMembershipPlan.ShowDialog()
        LoadUsers()
    End Sub


    'Private Function CreateCardImage(qrCodeImage As Bitmap, fullName As String, uid As String) As Bitmap
    '    Dim cardWidth As Integer = 300
    '    Dim cardHeight As Integer = 200
    '    Dim cardImage As New Bitmap(cardWidth, cardHeight)

    '    Using g As Graphics = Graphics.FromImage(cardImage)
    '        g.Clear(Color.White)

    '        ' Draw QR code
    '        Dim qrCodeX As Integer = 10
    '        Dim qrCodeY As Integer = 15
    '        Dim qrCodeWidth As Integer = 170
    '        Dim qrCodeHeight As Integer = 170
    '        g.DrawImage(qrCodeImage, qrCodeX, qrCodeY, qrCodeWidth, qrCodeHeight)

    '        ' Draw Full Name
    '        Dim fullNameFont As New Font("Arial", 12, FontStyle.Bold)
    '        Dim fullNameX As Integer = 170
    '        Dim fullNameY As Integer = 40
    '        g.DrawString(fullName, fullNameFont, Brushes.Black, fullNameX, fullNameY)

    '        ' Draw UID
    '        Dim uidFont As New Font("Arial", 10)
    '        Dim uidX As Integer = 170
    '        Dim uidY As Integer = 70
    '        g.DrawString("UID: " & uid, uidFont, Brushes.Black, uidX, uidY)
    '    End Using

    '    Return cardImage
    'End Function


    'Private Sub SaveCardImage(cardImage As Bitmap, uid As String)
    '    Dim customPath As String = "E:\Iron Lifters Cards"

    '    If Not Directory.Exists(customPath) Then
    '        Directory.CreateDirectory(customPath)
    '    End If

    '    Dim fileName As String = Path.Combine(customPath, "Card_" & uid & ".jpeg")
    '    cardImage.Save(fileName, Imaging.ImageFormat.Jpeg)
    'End Sub

End Class
