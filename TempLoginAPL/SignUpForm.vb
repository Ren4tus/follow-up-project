Public Class SignUpForm

    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Dim id As String = txtNewID.Text.Trim()
        Dim pw As String = txtNewPassword.Text.Trim()

        If String.IsNullOrEmpty(id) OrElse String.IsNullOrEmpty(pw) Then
            MessageBox.Show("아이디와 비밀번호를 모두 입력해주세요.")
            Return
        End If

        If DatabaseHelper.RegisterUser(id, pw) Then
            MessageBox.Show("회원가입이 완료되었습니다.")
            Me.Close()
        End If
    End Sub

End Class