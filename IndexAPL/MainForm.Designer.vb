<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.txtId = New System.Windows.Forms.TextBox()
		Me.txtName = New System.Windows.Forms.TextBox()
		Me.txtAge = New System.Windows.Forms.TextBox()
		Me.btnRegister = New System.Windows.Forms.Button()
		Me.cmbGender = New System.Windows.Forms.ComboBox()
		Me.SuspendLayout()
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(20, 20)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(48, 12)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "ID (선택):"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(20, 60)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(25, 12)
		Me.Label2.TabIndex = 1
		Me.Label2.Text = "이름:"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(20, 100)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(25, 12)
		Me.Label3.TabIndex = 2
		Me.Label3.Text = "성별:"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(20, 140)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(25, 12)
		Me.Label4.TabIndex = 3
		Me.Label4.Text = "나이:"
		'
		'txtId
		'
		Me.txtId.Location = New System.Drawing.Point(100, 20)
		Me.txtId.Name = "txtId"
		Me.txtId.Size = New System.Drawing.Size(150, 19)
		Me.txtId.TabIndex = 4
		'
		'txtName
		'
		Me.txtName.Location = New System.Drawing.Point(100, 60)
		Me.txtName.Name = "txtName"
		Me.txtName.Size = New System.Drawing.Size(150, 19)
		Me.txtName.TabIndex = 5
		'
		'txtAge
		'
		Me.txtAge.Location = New System.Drawing.Point(100, 140)
		Me.txtAge.Name = "txtAge"
		Me.txtAge.Size = New System.Drawing.Size(150, 19)
		Me.txtAge.TabIndex = 6
		'
		'btnRegister
		'
		Me.btnRegister.Location = New System.Drawing.Point(100, 180)
		Me.btnRegister.Name = "btnRegister"
		Me.btnRegister.Size = New System.Drawing.Size(150, 30)
		Me.btnRegister.TabIndex = 7
		Me.btnRegister.Text = "등록"
		Me.btnRegister.UseVisualStyleBackColor = True
		'
		'cmbGender
		'
		Me.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmbGender.FormattingEnabled = True
		Me.cmbGender.Items.AddRange(New Object() {"M", "F"})
		Me.cmbGender.Location = New System.Drawing.Point(100, 100)
		Me.cmbGender.Name = "cmbGender"
		Me.cmbGender.Size = New System.Drawing.Size(150, 20)
		Me.cmbGender.TabIndex = 8
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(304, 221)
		Me.Controls.Add(Me.cmbGender)
		Me.Controls.Add(Me.btnRegister)
		Me.Controls.Add(Me.txtAge)
		Me.Controls.Add(Me.txtName)
		Me.Controls.Add(Me.txtId)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.Name = "MainForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "IndexAPL - 회원 등록"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
	Friend WithEvents txtId As TextBox
	Friend WithEvents txtName As TextBox
	Friend WithEvents txtAge As TextBox
	Friend WithEvents btnRegister As Button
	Friend WithEvents cmbGender As ComboBox
End Class