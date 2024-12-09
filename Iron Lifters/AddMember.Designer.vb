<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddMember
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
        Me.addType = New System.Windows.Forms.Button()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.cancelButton = New System.Windows.Forms.Button()
        Me.a = New System.Windows.Forms.Button()
        Me.sBox = New System.Windows.Forms.CheckBox()
        Me.pBox = New System.Windows.Forms.TextBox()
        Me.uBox = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lBox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.fBox = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.eBox = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.btnUploadProfile = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.phBox = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'addType
        '
        Me.addType.Location = New System.Drawing.Point(501, 58)
        Me.addType.Name = "addType"
        Me.addType.Size = New System.Drawing.Size(24, 21)
        Me.addType.TabIndex = 46
        Me.addType.Text = "..."
        Me.addType.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(329, 58)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(166, 21)
        Me.ComboBox1.TabIndex = 4
        '
        'cancelButton
        '
        Me.cancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cancelButton.Location = New System.Drawing.Point(369, 219)
        Me.cancelButton.Name = "cancelButton"
        Me.cancelButton.Size = New System.Drawing.Size(75, 23)
        Me.cancelButton.TabIndex = 11
        Me.cancelButton.Text = "Cancel"
        Me.cancelButton.UseVisualStyleBackColor = True
        '
        'a
        '
        Me.a.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.a.Location = New System.Drawing.Point(450, 219)
        Me.a.Name = "a"
        Me.a.Size = New System.Drawing.Size(75, 23)
        Me.a.TabIndex = 12
        Me.a.Text = "Add"
        Me.a.UseVisualStyleBackColor = True
        '
        'sBox
        '
        Me.sBox.AutoSize = True
        Me.sBox.Location = New System.Drawing.Point(100, 162)
        Me.sBox.Name = "sBox"
        Me.sBox.Size = New System.Drawing.Size(15, 14)
        Me.sBox.TabIndex = 8
        Me.sBox.UseVisualStyleBackColor = True
        '
        'pBox
        '
        Me.pBox.Location = New System.Drawing.Point(329, 123)
        Me.pBox.Name = "pBox"
        Me.pBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.pBox.Size = New System.Drawing.Size(196, 20)
        Me.pBox.TabIndex = 7
        '
        'uBox
        '
        Me.uBox.Location = New System.Drawing.Point(99, 123)
        Me.uBox.Name = "uBox"
        Me.uBox.Size = New System.Drawing.Size(130, 20)
        Me.uBox.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 162)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(46, 13)
        Me.Label6.TabIndex = 39
        Me.Label6.Text = "Activate"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(235, 66)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 13)
        Me.Label5.TabIndex = 38
        Me.Label5.Text = "Membership Plan"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(235, 130)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 13)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "Password"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 130)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 36
        Me.Label3.Text = "Username"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "Lastname"
        '
        'lBox
        '
        Me.lBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower
        Me.lBox.Location = New System.Drawing.Point(99, 23)
        Me.lBox.Name = "lBox"
        Me.lBox.Size = New System.Drawing.Size(130, 20)
        Me.lBox.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(235, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 47
        Me.Label2.Text = "Firstname"
        '
        'fBox
        '
        Me.fBox.Location = New System.Drawing.Point(329, 23)
        Me.fBox.Name = "fBox"
        Me.fBox.Size = New System.Drawing.Size(196, 20)
        Me.fBox.TabIndex = 2
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(11, 66)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(25, 13)
        Me.Label7.TabIndex = 49
        Me.Label7.Text = "Sex"
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(99, 58)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(130, 21)
        Me.ComboBox2.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(11, 98)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(32, 13)
        Me.Label8.TabIndex = 50
        Me.Label8.Text = "Email"
        '
        'eBox
        '
        Me.eBox.Location = New System.Drawing.Point(99, 91)
        Me.eBox.Name = "eBox"
        Me.eBox.Size = New System.Drawing.Size(426, 20)
        Me.eBox.TabIndex = 5
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(235, 163)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(36, 13)
        Me.Label9.TabIndex = 51
        Me.Label9.Text = "Profile"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'btnUploadProfile
        '
        Me.btnUploadProfile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUploadProfile.Location = New System.Drawing.Point(329, 151)
        Me.btnUploadProfile.Name = "btnUploadProfile"
        Me.btnUploadProfile.Size = New System.Drawing.Size(196, 23)
        Me.btnUploadProfile.TabIndex = 9
        Me.btnUploadProfile.Text = "Upload Image..."
        Me.btnUploadProfile.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 194)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(78, 13)
        Me.Label10.TabIndex = 53
        Me.Label10.Text = "Phone Number"
        '
        'phBox
        '
        Me.phBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.phBox.Location = New System.Drawing.Point(99, 191)
        Me.phBox.Name = "phBox"
        Me.phBox.Size = New System.Drawing.Size(130, 20)
        Me.phBox.TabIndex = 10
        '
        'AddMember
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(537, 254)
        Me.Controls.Add(Me.phBox)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.btnUploadProfile)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.eBox)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.fBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.addType)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.cancelButton)
        Me.Controls.Add(Me.a)
        Me.Controls.Add(Me.sBox)
        Me.Controls.Add(Me.pBox)
        Me.Controls.Add(Me.uBox)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lBox)
        Me.Name = "AddMember"
        Me.Text = "Add Member"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents addType As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents cancelButton As Button
    Friend WithEvents a As Button
    Friend WithEvents sBox As CheckBox
    Friend WithEvents pBox As TextBox
    Friend WithEvents uBox As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lBox As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents fBox As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents eBox As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents btnUploadProfile As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents phBox As TextBox
End Class
