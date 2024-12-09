Imports MySql.Data.MySqlClient

Public Class AddMembershipPlan
    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Try
            ' Validate inputs
            Dim planName As String = pbox.Text.Trim()
            Dim priceInput As String = abox.Text.Trim()


            ' Ensure fields are not empty
            If String.IsNullOrWhiteSpace(planName) OrElse String.IsNullOrWhiteSpace(priceInput) Then
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' Validate price and duration
            Dim price As Decimal

            If Not Decimal.TryParse(priceInput, price) OrElse price <= 0 Then
                MessageBox.Show("Please enter a valid price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If



            ' Confirm before adding
            Dim result As DialogResult = MessageBox.Show($"Are you sure you want to add the following membership plan?" & vbCrLf &
                                                         $"Plan Name: {planName}" & vbCrLf &
                                                         $"Price: {price:C}", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then Exit Sub

            ' Add membership plan to the database
            dbConn()
            Using cmd As New MySqlCommand("INSERT INTO `membership plans` (membership_plan_name, price) VALUES (@name, @price)", conn)
                cmd.Parameters.AddWithValue("@name", planName)
                cmd.Parameters.AddWithValue("@price", price)
                cmd.ExecuteNonQuery()
            End Using

            MessageBox.Show("Membership plan added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Clear fields after successful addition
            pbox.Clear()
            abox.Clear()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dbDisconn()
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class