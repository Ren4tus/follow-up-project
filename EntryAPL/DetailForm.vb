Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data.SQLite

Public Class DetailForm
    Inherits Form

    Private connectionString As String
    Private currentId As String

    'Private txtId As TextBox
    'Private txtName As TextBox
    'Private cmbGender As ComboBox
    'Private txtAge As TextBox
    'Private txtProcess As TextBox
    'Private txtSource As TextBox
    'Private btnSave As Button
    'Private btnCancel As Button

    Public Sub New(connString As String, id As String)
        InitializeComponent()
        Me.connectionString = connString
        Me.currentId = id

        'InitializeUI()
        LoadData()
    End Sub

    'Private Sub InitializeUI()
    '    Me.Text = "데이터 상세 및 수정"
    '    Me.Size = New Size(320, 320)
    '    Me.StartPosition = FormStartPosition.CenterScreen
    '    Me.FormBorderStyle = FormBorderStyle.FixedSingle
    '    Me.MaximizeBox = False
    '    Me.MinimizeBox = False

    '    Dim yPos As Integer = 20
    '    Dim spacing As Integer = 35

    '    ' 1. Id (Read-only)
    '    Me.Controls.Add(New Label() With { .Text = "ID (수정불가):", .Location = New Point(20, yPos), .AutoSize = True })
    '    txtId = New TextBox() With { .Location = New Point(120, yPos), .Width = 150, .ReadOnly = True, .BackColor = Color.LightGray }
    '    Me.Controls.Add(txtId)
    '    yPos += spacing

    '    ' 2. Name
    '    Me.Controls.Add(New Label() With { .Text = "이름:", .Location = New Point(20, yPos), .AutoSize = True })
    '    txtName = New TextBox() With { .Location = New Point(120, yPos), .Width = 150 }
    '    Me.Controls.Add(txtName)
    '    yPos += spacing

    '    ' 3. Gender
    '    Me.Controls.Add(New Label() With { .Text = "성별:", .Location = New Point(20, yPos), .AutoSize = True })
    '    cmbGender = New ComboBox() With { .Location = New Point(120, yPos), .Width = 150, .DropDownStyle = ComboBoxStyle.DropDownList }
    '    cmbGender.Items.AddRange(New String() {"M", "F"})
    '    Me.Controls.Add(cmbGender)
    '    yPos += spacing

    '    ' 4. Age
    '    Me.Controls.Add(New Label() With { .Text = "나이:", .Location = New Point(20, yPos), .AutoSize = True })
    '    txtAge = New TextBox() With { .Location = New Point(120, yPos), .Width = 150 }
    '    Me.Controls.Add(txtAge)
    '    yPos += spacing

    '    ' 5. current_process (Read-only)
    '    Me.Controls.Add(New Label() With { .Text = "프로세스:", .Location = New Point(20, yPos), .AutoSize = True })
    '    txtProcess = New TextBox() With { .Location = New Point(120, yPos), .Width = 150, .ReadOnly = True, .BackColor = Color.LightGray }
    '    Me.Controls.Add(txtProcess)
    '    yPos += spacing

    '    ' 6. InputSource (Read-only)
    '    Me.Controls.Add(New Label() With { .Text = "입력소스:", .Location = New Point(20, yPos), .AutoSize = True })
    '    txtSource = New TextBox() With { .Location = New Point(120, yPos), .Width = 150, .ReadOnly = True, .BackColor = Color.LightGray }
    '    Me.Controls.Add(txtSource)
    '    yPos += spacing

    '    ' Buttons
    '    btnSave = New Button() With { .Text = "확인(수정)", .Location = New Point(50, yPos), .Width = 100, .Height = 30 }
    '    AddHandler btnSave.Click, AddressOf btnSave_Click
    '    Me.Controls.Add(btnSave)

    '    btnCancel = New Button() With { .Text = "뒤로가기", .Location = New Point(160, yPos), .Width = 100, .Height = 30 }
    '    AddHandler btnCancel.Click, Sub(s, e) Me.Close()
    '    Me.Controls.Add(btnCancel)
    'End Sub

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

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

End Class