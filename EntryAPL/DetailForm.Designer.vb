<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DetailForm
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()>
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
		Me.cmbGender = New System.Windows.Forms.ComboBox()
		Me.txtSource = New System.Windows.Forms.TextBox()
		Me.txtProcess = New System.Windows.Forms.TextBox()
		Me.txtName = New System.Windows.Forms.TextBox()
		Me.txtAge = New System.Windows.Forms.TextBox()
		Me.txtId = New System.Windows.Forms.TextBox()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.btnCancel = New System.Windows.Forms.Button()
		Me.btnSave = New System.Windows.Forms.Button()
		Me.SuspendLayout()
		'
		'cmbGender
		'
		Me.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmbGender.FormattingEnabled = True
		Me.cmbGender.Items.AddRange(New Object() {"M", "F"})
		Me.cmbGender.Location = New System.Drawing.Point(120, 90)
		Me.cmbGender.Name = "cmbGender"
		Me.cmbGender.Size = New System.Drawing.Size(150, 20)
		Me.cmbGender.TabIndex = 14
		'
		'txtSource
		'
		Me.txtSource.BackColor = System.Drawing.Color.LightGray
		Me.txtSource.Location = New System.Drawing.Point(120, 195)
		Me.txtSource.Name = "txtSource"
		Me.txtSource.ReadOnly = True
		Me.txtSource.Size = New System.Drawing.Size(150, 19)
		Me.txtSource.TabIndex = 13
		'
		'txtProcess
		'
		Me.txtProcess.BackColor = System.Drawing.Color.LightGray
		Me.txtProcess.Location = New System.Drawing.Point(120, 160)
		Me.txtProcess.Name = "txtProcess"
		Me.txtProcess.ReadOnly = True
		Me.txtProcess.Size = New System.Drawing.Size(150, 19)
		Me.txtProcess.TabIndex = 11
		'
		'txtName
		'
		Me.txtName.Location = New System.Drawing.Point(120, 55)
		Me.txtName.Name = "txtName"
		Me.txtName.Size = New System.Drawing.Size(150, 19)
		Me.txtName.TabIndex = 10
		'
		'txtAge
		'
		Me.txtAge.Location = New System.Drawing.Point(120, 125)
		Me.txtAge.Name = "txtAge"
		Me.txtAge.Size = New System.Drawing.Size(150, 19)
		Me.txtAge.TabIndex = 9
		'
		'txtId
		'
		Me.txtId.BackColor = System.Drawing.Color.LightGray
		Me.txtId.Location = New System.Drawing.Point(120, 20)
		Me.txtId.Name = "txtId"
		Me.txtId.ReadOnly = True
		Me.txtId.Size = New System.Drawing.Size(150, 19)
		Me.txtId.TabIndex = 2
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Location = New System.Drawing.Point(20, 198)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(43, 12)
		Me.Label6.TabIndex = 8
		Me.Label6.Text = "입력소스:"
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Location = New System.Drawing.Point(20, 93)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(25, 12)
		Me.Label5.TabIndex = 7
		Me.Label5.Text = "성별:"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(20, 163)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(43, 12)
		Me.Label4.TabIndex = 6
		Me.Label4.Text = "프로세스:"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(20, 58)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(25, 12)
		Me.Label3.TabIndex = 5
		Me.Label3.Text = "이름:"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(20, 128)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(25, 12)
		Me.Label2.TabIndex = 4
		Me.Label2.Text = "나이:"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(20, 20)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(66, 12)
		Me.Label1.TabIndex = 2
		Me.Label1.Text = "ID (수정불가):"
		'
		'btnCancel
		'
		Me.btnCancel.Location = New System.Drawing.Point(160, 230)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(100, 30)
		Me.btnCancel.TabIndex = 3
		Me.btnCancel.Text = "뒤로가기"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'btnSave
		'
		Me.btnSave.Location = New System.Drawing.Point(50, 230)
		Me.btnSave.Name = "btnSave"
		Me.btnSave.Size = New System.Drawing.Size(100, 30)
		Me.btnSave.TabIndex = 2
		Me.btnSave.Text = "확인(수정)"
		Me.btnSave.UseVisualStyleBackColor = True
		'
		'DetailForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(304, 281)
		Me.Controls.Add(Me.txtAge)
		Me.Controls.Add(Me.txtProcess)
		Me.Controls.Add(Me.txtId)
		Me.Controls.Add(Me.cmbGender)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.txtName)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.txtSource)
		Me.Controls.Add(Me.btnSave)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.Label6)
		Me.Controls.Add(Me.Label5)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "DetailForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "데이터 상세 및 수정"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents btnCancel As Button
	Friend WithEvents btnSave As Button
	Friend WithEvents txtId As TextBox
	Friend WithEvents Label6 As Label
	Friend WithEvents Label5 As Label
	Friend WithEvents Label4 As Label
	Friend WithEvents Label3 As Label
	Friend WithEvents Label2 As Label
	Friend WithEvents Label1 As Label
	Friend WithEvents txtSource As TextBox
	Friend WithEvents txtProcess As TextBox
	Friend WithEvents txtName As TextBox
	Friend WithEvents txtAge As TextBox
	Friend WithEvents cmbGender As ComboBox
End Class