<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FacultyManagement
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnAddFaculty = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.boxSearchUser = New System.Windows.Forms.TextBox()
        Me.selectDeselect = New System.Windows.Forms.CheckBox()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnAddFaculty
        '
        Me.btnAddFaculty.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddFaculty.Location = New System.Drawing.Point(695, 7)
        Me.btnAddFaculty.Name = "btnAddFaculty"
        Me.btnAddFaculty.Size = New System.Drawing.Size(75, 23)
        Me.btnAddFaculty.TabIndex = 16
        Me.btnAddFaculty.Text = "Add Faculty"
        Me.btnAddFaculty.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Search Faculty:"
        '
        'boxSearchUser
        '
        Me.boxSearchUser.Location = New System.Drawing.Point(98, 10)
        Me.boxSearchUser.Name = "boxSearchUser"
        Me.boxSearchUser.Size = New System.Drawing.Size(198, 20)
        Me.boxSearchUser.TabIndex = 14
        '
        'selectDeselect
        '
        Me.selectDeselect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.selectDeselect.AutoSize = True
        Me.selectDeselect.Location = New System.Drawing.Point(12, 385)
        Me.selectDeselect.Name = "selectDeselect"
        Me.selectDeselect.Size = New System.Drawing.Size(15, 14)
        Me.selectDeselect.TabIndex = 13
        Me.selectDeselect.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(614, 385)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 12
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(12, 35)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(758, 344)
        Me.ListView1.TabIndex = 11
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(695, 385)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnRefresh.TabIndex = 10
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'FacultyManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnAddFaculty)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.boxSearchUser)
        Me.Controls.Add(Me.selectDeselect)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.btnRefresh)
        Me.Name = "FacultyManagement"
        Me.Size = New System.Drawing.Size(787, 421)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnAddFaculty As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents boxSearchUser As TextBox
    Friend WithEvents selectDeselect As CheckBox
    Friend WithEvents btnDelete As Button
    Friend WithEvents ListView1 As ListView
    Friend WithEvents btnRefresh As Button
End Class
