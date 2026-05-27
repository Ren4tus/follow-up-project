Imports System.Data.SQLite

Public Class DetailForm
    Inherits Form

    Private connectionString As String
    Private currentId As String

    Public Sub New(connString As String, id As String)
        InitializeComponent()
        Me.connectionString = connString
        Me.currentId = id
        InitializeUI()
        LoadData()
    End Sub

    Private Sub InitializeUI()

        AddHandler btnCancel.Click, Sub(s, e) Me.Close()
        Me.Controls.Add(btnCancel)
    End Sub

    Private Sub LoadData()
        Try
            Using conn As New SQLiteConnection(connectionString)
                conn.Open()
                Dim query As String = "SELECT Id, name, gender, age, current_process, InputSource FROM UserTable WHERE Id = @Id"
                Using cmd As New SQLiteCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Id", currentId)
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            txtId.Text = reader("Id").ToString()
                            txtName.Text = reader("name").ToString()
                            
                            Dim gen As String = reader("gender").ToString()
                            If cmbGender.Items.Contains(gen) Then
                                cmbGender.SelectedItem = gen
                            End If
                            
                            txtAge.Text = reader("age").ToString()
                            txtProcess.Text = reader("current_process").ToString()
                            txtSource.Text = reader("InputSource").ToString()
                        Else
                            MessageBox.Show("데이터를 찾을 수 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Me.Close()
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"데이터 로드 오류: {ex.Message}", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim name As String = txtName.Text.Trim()
        Dim age As String = txtAge.Text.Trim()
        Dim gender As String = If(cmbGender.SelectedItem IsNot Nothing, cmbGender.SelectedItem.ToString(), "")

        If String.IsNullOrEmpty(name) Then
            MessageBox.Show("이름을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            Using conn As New SQLiteConnection(connectionString)
                conn.Open()
                Dim updateQuery As String = "UPDATE UserTable SET name = @Name, gender = @Gender, age = @Age WHERE Id = @Id"
                Using cmd As New SQLiteCommand(updateQuery, conn)
                    cmd.Parameters.AddWithValue("@Name", name)
                    cmd.Parameters.AddWithValue("@Gender", gender)
                    cmd.Parameters.AddWithValue("@Age", age)
                    cmd.Parameters.AddWithValue("@Id", currentId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            MessageBox.Show("수정되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        Catch ex As Exception
            MessageBox.Show($"업데이트 오류: {ex.Message}", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class