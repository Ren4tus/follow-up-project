Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 앱 시작 시 데이터베이스 초기화 시도
        DatabaseHelper.InitializeDatabase()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim id As String = txtID.Text.Trim()
        Dim pw As String = txtPassword.Text.Trim()

        If String.IsNullOrEmpty(id) OrElse String.IsNullOrEmpty(pw) Then
            MessageBox.Show("아이디와 비밀번호를 모두 입력해주세요.")
            Return
        End If

        If DatabaseHelper.LoginUser(id, pw) Then
            Dim mainForm As New MainForm(id)
            mainForm.Show()
            Me.Hide() ' 로그인 화면 숨김

            ' 메인 폼이 닫힐 때 애플리케이션 종료 처리
            AddHandler mainForm.FormClosed, Sub() Me.Close()
        Else
            MessageBox.Show("아이디 또는 비밀번호가 잘못되었습니다.")
        End If
    End Sub

    Private Sub btnSignUp_Click(sender As Object, e As EventArgs) Handles btnSignUp.Click
        Dim signUp As New SignUpForm()
        signUp.ShowDialog()
    End Sub

End Class
