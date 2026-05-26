Public Class MainForm
    Private _userId As String

    Public Sub New(userId As String)
        InitializeComponent()
        _userId = userId
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblWelcome.Text = _userId & "님 환영합니다."
        UpdateCounterDisplay()
    End Sub

    Private Sub UpdateCounterDisplay()
        Dim value As Integer = DatabaseHelper.GetCounterValue(_userId)
        lblCounter.Text = value.ToString()
    End Sub

    Private Sub btnPlusOne_Click(sender As Object, e As EventArgs) Handles btnPlusOne.Click
        DatabaseHelper.IncrementCounter(_userId)
        UpdateCounterDisplay()
    End Sub

End Class