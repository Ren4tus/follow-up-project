<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SignUpForm
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblNewID = New System.Windows.Forms.Label()
        Me.txtNewID = New System.Windows.Forms.TextBox()
        Me.lblNewPassword = New System.Windows.Forms.Label()
        Me.txtNewPassword = New System.Windows.Forms.TextBox()
        Me.btnRegister = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblNewID
        '
        Me.lblNewID.AutoSize = True
        Me.lblNewID.Location = New System.Drawing.Point(30, 30)
        Me.lblNewID.Name = "lblNewID"
        Me.lblNewID.Size = New System.Drawing.Size(53, 12)
        Me.lblNewID.TabIndex = 0
        Me.lblNewID.Text = "아이디:"
        '
        'txtNewID
        '
        Me.txtNewID.Location = New System.Drawing.Point(90, 27)
        Me.txtNewID.Name = "txtNewID"
        Me.txtNewID.Size = New System.Drawing.Size(150, 21)
        Me.txtNewID.TabIndex = 1
        '
        'lblNewPassword
        '
        Me.lblNewPassword.AutoSize = True
        Me.lblNewPassword.Location = New System.Drawing.Point(18, 65)
        Me.lblNewPassword.Name = "lblNewPassword"
        Me.lblNewPassword.Size = New System.Drawing.Size(65, 12)
        Me.lblNewPassword.TabIndex = 2
        Me.lblNewPassword.Text = "비밀번호:"
        '
        'txtNewPassword
        '
        Me.txtNewPassword.Location = New System.Drawing.Point(90, 62)
        Me.txtNewPassword.Name = "txtNewPassword"
        Me.txtNewPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtNewPassword.Size = New System.Drawing.Size(150, 21)
        Me.txtNewPassword.TabIndex = 3
        '
        'btnRegister
        '
        Me.btnRegister.Location = New System.Drawing.Point(90, 100)
        Me.btnRegister.Name = "btnRegister"
        Me.btnRegister.Size = New System.Drawing.Size(150, 30)
        Me.btnRegister.TabIndex = 4
        Me.btnRegister.Text = "가입하기"
        Me.btnRegister.UseVisualStyleBackColor = True
        '
        'SignUpForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(280, 160)
        Me.Controls.Add(Me.btnRegister)
        Me.Controls.Add(Me.txtNewPassword)
        Me.Controls.Add(Me.lblNewPassword)
        Me.Controls.Add(Me.txtNewID)
        Me.Controls.Add(Me.lblNewID)
        Me.Name = "SignUpForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "회원가입"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblNewID As System.Windows.Forms.Label
    Friend WithEvents txtNewID As System.Windows.Forms.TextBox
    Friend WithEvents lblNewPassword As System.Windows.Forms.Label
    Friend WithEvents txtNewPassword As System.Windows.Forms.TextBox
    Friend WithEvents btnRegister As System.Windows.Forms.Button
End Class