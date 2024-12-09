<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Members
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
        Me.btnRetrieve = New System.Windows.Forms.Button()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.btnDisable = New System.Windows.Forms.Button()
        Me.selectDeselect = New System.Windows.Forms.CheckBox()
        Me.boxSearchUser = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAddMember = New System.Windows.Forms.Button()
        Me.btnPayed = New System.Windows.Forms.Button()
        Me.btnPrintCard = New System.Windows.Forms.Button()
        Me.btnPlan = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnRetrieve
        '
        Me.btnRetrieve.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRetrieve.Location = New System.Drawing.Point(530, 18)
        Me.btnRetrieve.Name = "btnRetrieve"
        Me.btnRetrieve.Size = New System.Drawing.Size(75, 23)
        Me.btnRetrieve.TabIndex = 0
        Me.btnRetrieve.Text = "Refresh"
        Me.btnRetrieve.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(24, 47)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(743, 428)
        Me.ListView1.TabIndex = 1
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'btnDisable
        '
        Me.btnDisable.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDisable.Location = New System.Drawing.Point(611, 481)
        Me.btnDisable.Name = "btnDisable"
        Me.btnDisable.Size = New System.Drawing.Size(75, 23)
        Me.btnDisable.TabIndex = 2
        Me.btnDisable.Text = "Disable"
        Me.btnDisable.UseVisualStyleBackColor = True
        '
        'selectDeselect
        '
        Me.selectDeselect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.selectDeselect.AutoSize = True
        Me.selectDeselect.Location = New System.Drawing.Point(24, 481)
        Me.selectDeselect.Name = "selectDeselect"
        Me.selectDeselect.Size = New System.Drawing.Size(15, 14)
        Me.selectDeselect.TabIndex = 4
        Me.selectDeselect.UseVisualStyleBackColor = True
        '
        'boxSearchUser
        '
        Me.boxSearchUser.Location = New System.Drawing.Point(163, 16)
        Me.boxSearchUser.Name = "boxSearchUser"
        Me.boxSearchUser.Size = New System.Drawing.Size(198, 20)
        Me.boxSearchUser.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Search Member Username:"
        '
        'btnAddMember
        '
        Me.btnAddMember.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddMember.Location = New System.Drawing.Point(692, 18)
        Me.btnAddMember.Name = "btnAddMember"
        Me.btnAddMember.Size = New System.Drawing.Size(75, 23)
        Me.btnAddMember.TabIndex = 7
        Me.btnAddMember.Text = "Add Member"
        Me.btnAddMember.UseVisualStyleBackColor = True
        '
        'btnPayed
        '
        Me.btnPayed.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPayed.Location = New System.Drawing.Point(501, 481)
        Me.btnPayed.Name = "btnPayed"
        Me.btnPayed.Size = New System.Drawing.Size(104, 23)
        Me.btnPayed.TabIndex = 8
        Me.btnPayed.Text = "Process Payment"
        Me.btnPayed.UseVisualStyleBackColor = True
        '
        'btnPrintCard
        '
        Me.btnPrintCard.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrintCard.Location = New System.Drawing.Point(692, 481)
        Me.btnPrintCard.Name = "btnPrintCard"
        Me.btnPrintCard.Size = New System.Drawing.Size(75, 23)
        Me.btnPrintCard.TabIndex = 9
        Me.btnPrintCard.Text = "Print Card"
        Me.btnPrintCard.UseVisualStyleBackColor = True
        '
        'btnPlan
        '
        Me.btnPlan.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPlan.Location = New System.Drawing.Point(611, 18)
        Me.btnPlan.Name = "btnPlan"
        Me.btnPlan.Size = New System.Drawing.Size(75, 23)
        Me.btnPlan.TabIndex = 10
        Me.btnPlan.Text = "Add Plan"
        Me.btnPlan.UseVisualStyleBackColor = True
        '
        'Members
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnPlan)
        Me.Controls.Add(Me.btnPrintCard)
        Me.Controls.Add(Me.btnPayed)
        Me.Controls.Add(Me.btnAddMember)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.boxSearchUser)
        Me.Controls.Add(Me.selectDeselect)
        Me.Controls.Add(Me.btnDisable)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.btnRetrieve)
        Me.Name = "Members"
        Me.Size = New System.Drawing.Size(785, 518)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnRetrieve As Button
    Friend WithEvents ListView1 As ListView
    Friend WithEvents btnDisable As Button
    Friend WithEvents selectDeselect As CheckBox
    Friend WithEvents boxSearchUser As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnAddMember As Button
    Friend WithEvents btnPayed As Button
    Friend WithEvents btnPrintCard As Button
    Friend WithEvents btnPlan As Button
End Class
