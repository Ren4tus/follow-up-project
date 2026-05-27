Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data.SQLite
Imports System.IO
Imports SharedLibrary

Public Class MainForm
    Inherits Form

    Private txtId As TextBox
    Private txtName As TextBox
    Private cmbGender As ComboBox
    Private txtAge As TextBox
    Private btnRegister As Button

    Public Sub New()
        InitializeComponent()
        ' Initialize Component manually
        Me.Text = "IndexAPL - 회원 등록"
        Me.Size = New Size(320, 260)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False

        Dim lblId = New Label() With { .Text = "ID (선택):", .Location = New Point(20, 20), .AutoSize = True }
        txtId = New TextBox() With { .Location = New Point(100, 20), .Width = 150 }

        Dim lblName = New Label() With { .Text = "이름:", .Location = New Point(20, 60), .AutoSize = True }
        txtName = New TextBox() With { .Location = New Point(100, 60), .Width = 150 }

        Dim lblGender = New Label() With { .Text = "성별:", .Location = New Point(20, 100), .AutoSize = True }
        cmbGender = New ComboBox() With { .Location = New Point(100, 100), .Width = 150, .DropDownStyle = ComboBoxStyle.DropDownList }
        cmbGender.Items.AddRange(New String() {"M", "F"})
        cmbGender.SelectedIndex = 0

        Dim lblAge = New Label() With { .Text = "나이:", .Location = New Point(20, 140), .AutoSize = True }
        txtAge = New TextBox() With { .Location = New Point(100, 140), .Width = 150 }

        btnRegister = New Button() With { .Text = "등록", .Location = New Point(100, 180), .Width = 150, .Height = 30 }
        AddHandler btnRegister.Click, AddressOf btnRegister_Click

        Me.Controls.Add(lblId)
        Me.Controls.Add(txtId)
        Me.Controls.Add(lblName)
        Me.Controls.Add(txtName)
        Me.Controls.Add(lblGender)
        Me.Controls.Add(cmbGender)
        Me.Controls.Add(lblAge)
        Me.Controls.Add(txtAge)
        Me.Controls.Add(btnRegister)
    End Sub

    Private Sub btnRegister_Click(sender As Object, e As EventArgs)
        Dim id As String = txtId.Text.Trim()
        Dim name As String = txtName.Text.Trim()
        Dim gender As String = cmbGender.SelectedItem.ToString()
        Dim age As String = txtAge.Text.Trim()

        If String.IsNullOrEmpty(name) Then
            MessageBox.Show("이름을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' 3. ID 비워둔 경우 자동 생성
        If String.IsNullOrEmpty(id) Then
            id = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()
            txtId.Text = id
        End If

        ' DB Connection string resolving
        Dim currentDir As String = AppDomain.CurrentDomain.BaseDirectory
        Dim dbPath As String = ""
        While currentDir IsNot Nothing AndAlso currentDir.Length > 3
            If File.Exists(Path.Combine(currentDir, "local.db")) Then
                dbPath = Path.Combine(currentDir, "local.db")
                Exit While
            End If
            currentDir = Directory.GetParent(currentDir)?.FullName
        End While
        
        If String.IsNullOrEmpty(dbPath) Then
            MessageBox.Show("local.db 파일을 찾을 수 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim connectionString As String = $"Data Source={dbPath};Version=3;"

        Try
            Using conn As New SQLiteConnection(connectionString)
                conn.Open()
                ' 공통 DB 스키마 검사 (SharedUtils)
                SharedUtils.EnsureTableSchema(conn) 

                ' 4. ID, Name 중복 검사
                Dim checkQuery As String = "SELECT COUNT(1) FROM UserTable WHERE Id = @Id OR name = @Name"
                Using checkCmd As New SQLiteCommand(checkQuery, conn)
                    checkCmd.Parameters.AddWithValue("@Id", id)
                    checkCmd.Parameters.AddWithValue("@Name", name)
                    
                    Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                    If count > 0 Then
                        MessageBox.Show("이미 존재하는 ID 또는 이름입니다. (중복 등록 불가)", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                End Using

                ' 5. 등록 (current_process="complete", InputSource="APL")
                Dim insertQuery As String = "INSERT INTO UserTable (Id, name, gender, age, current_process, InputSource) VALUES (@Id, @Name, @Gender, @Age, 'complete', 'APL')"
                Using insertCmd As New SQLiteCommand(insertQuery, conn)
                    insertCmd.Parameters.AddWithValue("@Id", id)
                    insertCmd.Parameters.AddWithValue("@Name", name)
                    insertCmd.Parameters.AddWithValue("@Gender", gender)
                    insertCmd.Parameters.AddWithValue("@Age", age)
                    
                    insertCmd.ExecuteNonQuery()
                End Using

                MessageBox.Show($"정상적으로 등록되었습니다." & vbCrLf & $"ID: {id}" & vbCrLf & $"이름: {name}", "등록 완료", MessageBoxButtons.OK, MessageBoxIcon.Information)
                
                ' Clear fields after success
                txtId.Clear()
                txtName.Clear()
                txtAge.Clear()
                cmbGender.SelectedIndex = 0
            End Using
        Catch ex As Exception
            MessageBox.Show($"오류 발생: {ex.Message}", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class