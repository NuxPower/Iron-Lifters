<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.FragmentTitle = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.fragment = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.imagepanel = New System.Windows.Forms.Panel()
        Me.sidebar = New System.Windows.Forms.Panel()
        Me.btnFacultyLogs = New System.Windows.Forms.Button()
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.btnFacultyManagement = New System.Windows.Forms.Button()
        Me.btnAttendanceLogs = New System.Windows.Forms.Button()
        Me.btnPaymentLogs = New System.Windows.Forms.Button()
        Me.btnMembers = New System.Windows.Forms.Button()
        Me.btnDashboard = New System.Windows.Forms.Button()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.menuButton = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.imagepanel.SuspendLayout()
        Me.sidebar.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(191, Byte), Integer), CType(CType(191, Byte), Integer))
        Me.Panel1.Controls.Add(Me.FragmentTitle)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(600, 46)
        Me.Panel1.TabIndex = 0
        '
        'FragmentTitle
        '
        Me.FragmentTitle.AutoSize = True
        Me.FragmentTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FragmentTitle.Location = New System.Drawing.Point(12, 12)
        Me.FragmentTitle.Name = "FragmentTitle"
        Me.FragmentTitle.Size = New System.Drawing.Size(0, 25)
        Me.FragmentTitle.TabIndex = 0
        '
        'Timer1
        '
        Me.Timer1.Interval = 10
        '
        'fragment
        '
        Me.fragment.BackColor = System.Drawing.Color.White
        Me.fragment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fragment.Location = New System.Drawing.Point(0, 46)
        Me.fragment.Name = "fragment"
        Me.fragment.Size = New System.Drawing.Size(600, 404)
        Me.fragment.TabIndex = 2
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.menuButton)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(200, 46)
        Me.Panel2.TabIndex = 0
        '
        'imagepanel
        '
        Me.imagepanel.Controls.Add(Me.PictureBox2)
        Me.imagepanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.imagepanel.Location = New System.Drawing.Point(0, 46)
        Me.imagepanel.Name = "imagepanel"
        Me.imagepanel.Size = New System.Drawing.Size(200, 135)
        Me.imagepanel.TabIndex = 1
        '
        'sidebar
        '
        Me.sidebar.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.sidebar.Controls.Add(Me.btnFacultyLogs)
        Me.sidebar.Controls.Add(Me.btnLogout)
        Me.sidebar.Controls.Add(Me.btnFacultyManagement)
        Me.sidebar.Controls.Add(Me.btnAttendanceLogs)
        Me.sidebar.Controls.Add(Me.btnPaymentLogs)
        Me.sidebar.Controls.Add(Me.btnMembers)
        Me.sidebar.Controls.Add(Me.btnDashboard)
        Me.sidebar.Controls.Add(Me.imagepanel)
        Me.sidebar.Controls.Add(Me.Panel2)
        Me.sidebar.Dock = System.Windows.Forms.DockStyle.Right
        Me.sidebar.Location = New System.Drawing.Point(600, 0)
        Me.sidebar.Name = "sidebar"
        Me.sidebar.Size = New System.Drawing.Size(200, 450)
        Me.sidebar.TabIndex = 1
        '
        'btnFacultyLogs
        '
        Me.btnFacultyLogs.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnFacultyLogs.FlatAppearance.BorderSize = 0
        Me.btnFacultyLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFacultyLogs.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFacultyLogs.Image = Global.Iron_Lifters.My.Resources.Resources.Checked_User_Male
        Me.btnFacultyLogs.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnFacultyLogs.Location = New System.Drawing.Point(0, 371)
        Me.btnFacultyLogs.Name = "btnFacultyLogs"
        Me.btnFacultyLogs.Size = New System.Drawing.Size(200, 38)
        Me.btnFacultyLogs.TabIndex = 17
        Me.btnFacultyLogs.Text = "Faculty Logs"
        Me.btnFacultyLogs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFacultyLogs.UseVisualStyleBackColor = True
        '
        'btnLogout
        '
        Me.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnLogout.FlatAppearance.BorderSize = 0
        Me.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogout.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogout.Image = Global.Iron_Lifters.My.Resources.Resources.Logout_Rounded
        Me.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnLogout.Location = New System.Drawing.Point(0, 412)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Size = New System.Drawing.Size(200, 38)
        Me.btnLogout.TabIndex = 16
        Me.btnLogout.Text = "Logout"
        Me.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLogout.UseVisualStyleBackColor = True
        '
        'btnFacultyManagement
        '
        Me.btnFacultyManagement.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnFacultyManagement.FlatAppearance.BorderSize = 0
        Me.btnFacultyManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFacultyManagement.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFacultyManagement.Image = Global.Iron_Lifters.My.Resources.Resources.Management
        Me.btnFacultyManagement.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnFacultyManagement.Location = New System.Drawing.Point(0, 333)
        Me.btnFacultyManagement.Name = "btnFacultyManagement"
        Me.btnFacultyManagement.Size = New System.Drawing.Size(200, 38)
        Me.btnFacultyManagement.TabIndex = 15
        Me.btnFacultyManagement.Text = "Faculty Management"
        Me.btnFacultyManagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFacultyManagement.UseVisualStyleBackColor = True
        '
        'btnAttendanceLogs
        '
        Me.btnAttendanceLogs.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnAttendanceLogs.FlatAppearance.BorderSize = 0
        Me.btnAttendanceLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAttendanceLogs.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAttendanceLogs.Image = Global.Iron_Lifters.My.Resources.Resources.Attendance
        Me.btnAttendanceLogs.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAttendanceLogs.Location = New System.Drawing.Point(0, 295)
        Me.btnAttendanceLogs.Name = "btnAttendanceLogs"
        Me.btnAttendanceLogs.Size = New System.Drawing.Size(200, 38)
        Me.btnAttendanceLogs.TabIndex = 14
        Me.btnAttendanceLogs.Text = "Attendance Logs"
        Me.btnAttendanceLogs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAttendanceLogs.UseVisualStyleBackColor = True
        '
        'btnPaymentLogs
        '
        Me.btnPaymentLogs.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnPaymentLogs.FlatAppearance.BorderSize = 0
        Me.btnPaymentLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPaymentLogs.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPaymentLogs.Image = Global.Iron_Lifters.My.Resources.Resources.paymentlogs
        Me.btnPaymentLogs.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPaymentLogs.Location = New System.Drawing.Point(0, 257)
        Me.btnPaymentLogs.Name = "btnPaymentLogs"
        Me.btnPaymentLogs.Size = New System.Drawing.Size(200, 38)
        Me.btnPaymentLogs.TabIndex = 13
        Me.btnPaymentLogs.Text = "Payment Logs"
        Me.btnPaymentLogs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPaymentLogs.UseVisualStyleBackColor = True
        '
        'btnMembers
        '
        Me.btnMembers.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnMembers.FlatAppearance.BorderSize = 0
        Me.btnMembers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMembers.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMembers.Image = Global.Iron_Lifters.My.Resources.Resources.members
        Me.btnMembers.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMembers.Location = New System.Drawing.Point(0, 219)
        Me.btnMembers.Name = "btnMembers"
        Me.btnMembers.Size = New System.Drawing.Size(200, 38)
        Me.btnMembers.TabIndex = 12
        Me.btnMembers.Text = "Members"
        Me.btnMembers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMembers.UseVisualStyleBackColor = True
        '
        'btnDashboard
        '
        Me.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnDashboard.FlatAppearance.BorderSize = 0
        Me.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDashboard.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDashboard.Image = Global.Iron_Lifters.My.Resources.Resources.Control_Panel
        Me.btnDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDashboard.Location = New System.Drawing.Point(0, 181)
        Me.btnDashboard.Name = "btnDashboard"
        Me.btnDashboard.Size = New System.Drawing.Size(200, 38)
        Me.btnDashboard.TabIndex = 11
        Me.btnDashboard.Text = "Dashboard"
        Me.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDashboard.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PictureBox2.Image = Global.Iron_Lifters.My.Resources.Resources._302545659_481884507279435_1553771727610696154_n_removebg_preview_1
        Me.PictureBox2.Location = New System.Drawing.Point(27, 21)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(151, 90)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 0
        Me.PictureBox2.TabStop = False
        '
        'menuButton
        '
        Me.menuButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.menuButton.FlatAppearance.BorderSize = 0
        Me.menuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.menuButton.Image = Global.Iron_Lifters.My.Resources.Resources.Menu
        Me.menuButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.menuButton.Location = New System.Drawing.Point(0, 0)
        Me.menuButton.Name = "menuButton"
        Me.menuButton.Size = New System.Drawing.Size(53, 46)
        Me.menuButton.TabIndex = 0
        Me.menuButton.UseVisualStyleBackColor = True
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.fragment)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.sidebar)
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Iron Lifters"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.imagepanel.ResumeLayout(False)
        Me.sidebar.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Timer1 As Timer
    Friend WithEvents fragment As Panel
    Public WithEvents FragmentTitle As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents imagepanel As Panel
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents sidebar As Panel
    Friend WithEvents btnFacultyManagement As Button
    Friend WithEvents btnAttendanceLogs As Button
    Friend WithEvents btnPaymentLogs As Button
    Friend WithEvents btnMembers As Button
    Friend WithEvents btnDashboard As Button
    Friend WithEvents menuButton As Button
    Friend WithEvents btnLogout As Button
    Friend WithEvents btnFacultyLogs As Button
End Class
