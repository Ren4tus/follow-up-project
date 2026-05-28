<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DetailForm
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtAge = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtProcess = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtSource = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.cmbGender = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(112, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ID (수정불가):"
        '
        'txtId
        '
        Me.txtId.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.txtId.Location = New System.Drawing.Point(190, 40)
        Me.txtId.Name = "txtId"
        Me.txtId.ReadOnly = True
        Me.txtId.Size = New System.Drawing.Size(154, 19)
        Me.txtId.TabIndex = 6
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(190, 86)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(154, 19)
        Me.txtName.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(112, 89)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(25, 12)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "이름:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(112, 138)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 12)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "성별:"
        '
        'txtAge
        '
        Me.txtAge.Location = New System.Drawing.Point(190, 188)
        Me.txtAge.Name = "txtAge"
        Me.txtAge.Size = New System.Drawing.Size(154, 19)
        Me.txtAge.TabIndex = 12
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(112, 191)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(25, 12)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "나이:"
        '
        'txtProcess
        '
        Me.txtProcess.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.txtProcess.Location = New System.Drawing.Point(190, 242)
        Me.txtProcess.Name = "txtProcess"
        Me.txtProcess.ReadOnly = True
        Me.txtProcess.Size = New System.Drawing.Size(154, 19)
        Me.txtProcess.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(112, 245)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 12)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "프로세스:"
        '
        'txtSource
        '
        Me.txtSource.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.txtSource.Location = New System.Drawing.Point(190, 298)
        Me.txtSource.Name = "txtSource"
        Me.txtSource.ReadOnly = True
        Me.txtSource.Size = New System.Drawing.Size(154, 19)
        Me.txtSource.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(112, 301)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 12)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "입력소스:"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(123, 364)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 17
        Me.btnSave.Text = "확인(수정)"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(244, 364)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 18
        Me.btnCancel.Text = "뒤로가기"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'cmbGender
        '
        Me.cmbGender.FormattingEnabled = True
        Me.cmbGender.Items.AddRange(New Object() {"M", "F"})
        Me.cmbGender.Location = New System.Drawing.Point(190, 135)
        Me.cmbGender.Name = "cmbGender"
        Me.cmbGender.Size = New System.Drawing.Size(154, 20)
        Me.cmbGender.TabIndex = 19
        '
        'DetailForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(458, 433)
        Me.Controls.Add(Me.cmbGender)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtSource)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtProcess)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtAge)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtId)
        Me.Controls.Add(Me.Label1)
        Me.Name = "DetailForm"
        Me.Text = "DetailForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtId As TextBox
    Friend WithEvents txtName As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtAge As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtProcess As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtSource As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents cmbGender As ComboBox
End Class