Imports System
Imports System.Data.SQLite
Imports System.Drawing
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports MeCab
Imports SharedLibrary

Public Class MainForm
    Inherits Form

    Private pattern As String
    Private input As String
    Private ReadOnly mecabTagger As MeCabTagger

    Private txtId As TextBox
    Private WithEvents txtContractorNameKanji As TextBox
    Private WithEvents txtContractorNameKana As TextBox
    Private WithEvents txtContractorAddressKanji As TextBox
    Private txtContractorAddressKana As TextBox
    Private cmbContractorDateofBirthEra As ComboBox
    Private WithEvents txtContractorDateofBirthYear As TextBox
    Private cmbContractorDateofBirthMonth As ComboBox
    Private cmbContractorDateofBirthDay As ComboBox
    Private WithEvents txtRecipientNameKanji As TextBox
    Private WithEvents txtRecipientNameKana As TextBox
    Private WithEvents txtRecipientAddressKanji As TextBox
    Private txtRecipientAddressKana As TextBox
    Private cmbRecipientDateofBirthEra As ComboBox
    Private WithEvents txtRecipientDateofBirthYear As TextBox
    Private cmbRecipientDateofBirthMonth As ComboBox
    Private cmbRecipientDateofBirthDay As ComboBox
    Private cmbGender As ComboBox
    Private txtAge As TextBox
    Private btnRegister As Button
    Private btnCopy As Button
    Private btnAgeInput As Button
    Private Function InputToKatakana(input As String) As String
        If mecabTagger Is Nothing Then
            Return "[MeCab not initialized]"
        End If

        Dim node As MeCabNode = mecabTagger.ParseToNode(input)
        Dim readingResult As String = ""

        While node IsNot Nothing
            If node.Stat = MeCabNodeStat.Nor Then
                Dim features As String() = node.Feature.Split(","c)
                If features.Length >= 8 AndAlso Not String.IsNullOrEmpty(features(7)) Then
                    readingResult &= features(7)
                Else
                    readingResult &= node.Surface
                End If
            End If
            node = node.Next
        End While

        Return readingResult
    End Function
    Public Sub New()
        InitializeComponent()
        ' Initialize Component manually
        Me.Text = "IndexAPL - 회원 등록"
        Me.Size = New Size(400, 620)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False

        Dim lblId = New Label() With {.Text = "ID (선택):", .Location = New Point(20, 20), .AutoSize = True}
        txtId = New TextBox() With {.Location = New Point(70, 15), .Width = 285}

        Dim lblContractorNameKanji = New Label() With {.Text = "계약자 명(한자):", .Location = New Point(20, 60), .AutoSize = True}
        txtContractorNameKanji = New TextBox() With {.Location = New Point(95, 55), .Width = 260}

        Dim lblContractorNameKana = New Label() With {.Text = "계약자 명(카나):", .Location = New Point(20, 100), .AutoSize = True}
        txtContractorNameKana = New TextBox() With {.Location = New Point(95, 95), .Width = 260}

        Dim lblContractorAddressKanji = New Label() With {.Text = "계약자 주소(한자):", .Location = New Point(20, 140), .AutoSize = True}
        txtContractorAddressKanji = New TextBox() With {.Location = New Point(105, 135), .Width = 250}

        Dim lblContractorAddressKana = New Label() With {.Text = "계약자 주소(카나):", .Location = New Point(20, 180), .AutoSize = True}
        txtContractorAddressKana = New TextBox() With {.Location = New Point(105, 175), .Width = 250}

        Dim lblContractorDateofBirth = New Label() With {.Text = "계약자 생년월일:", .Location = New Point(20, 220), .AutoSize = True}
        cmbContractorDateofBirthEra = New ComboBox() With {.Location = New Point(100, 215), .Width = 50, .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbContractorDateofBirthEra.Items.AddRange(New String() {"明治", "大正", "昭和", "平成", "令和", "西暦"})
        cmbContractorDateofBirthEra.SelectedIndex = 0
        txtContractorDateofBirthYear = New TextBox() With {.Location = New Point(150, 215), .Width = 40}
        Dim lblContractorDateofBirthYear = New Label() With {.Text = "年", .Location = New Point(190, 220), .AutoSize = True}
        cmbContractorDateofBirthMonth = New ComboBox() With {.Location = New Point(210, 215), .Width = 40, .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbContractorDateofBirthMonth.Items.AddRange(New String() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"})
        cmbContractorDateofBirthMonth.SelectedIndex = 0
        Dim lblContractorDateofBirthMonth = New Label() With {.Text = "月", .Location = New Point(250, 220), .AutoSize = True}
        cmbContractorDateofBirthDay = New ComboBox() With {.Location = New Point(270, 215), .Width = 40, .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbContractorDateofBirthDay.Items.AddRange(New String() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})
        cmbContractorDateofBirthDay.SelectedIndex = 0
        Dim lblContractorDateofBirthDay = New Label() With {.Text = "日", .Location = New Point(310, 220), .AutoSize = True}

        Dim lblRecipientNameKanji = New Label() With {.Text = "수취인 명(한자):", .Location = New Point(20, 260), .AutoSize = True}
        txtRecipientNameKanji = New TextBox() With {.Location = New Point(95, 255), .Width = 260}

        Dim lblRecipientNameKana = New Label() With {.Text = "수취인 명(카나):", .Location = New Point(20, 300), .AutoSize = True}
        txtRecipientNameKana = New TextBox() With {.Location = New Point(95, 295), .Width = 260}

        Dim lblRecipientAddressKanji = New Label() With {.Text = "수취인 주소(한자):", .Location = New Point(20, 340), .AutoSize = True}
        txtRecipientAddressKanji = New TextBox() With {.Location = New Point(105, 335), .Width = 250}

        Dim lblRecipientAddressKana = New Label() With {.Text = "수취인 주소(카나):", .Location = New Point(20, 380), .AutoSize = True}
        txtRecipientAddressKana = New TextBox() With {.Location = New Point(105, 375), .Width = 250}

        Dim lblRecipientDateofBirth = New Label() With {.Text = "수취인 생년월일:", .Location = New Point(20, 420), .AutoSize = True}
        cmbRecipientDateofBirthEra = New ComboBox() With {.Location = New Point(100, 415), .Width = 50, .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbRecipientDateofBirthEra.Items.AddRange(New String() {"明治", "大正", "昭和", "平成", "令和", "西暦"})
        cmbRecipientDateofBirthEra.SelectedIndex = 0
        txtRecipientDateofBirthYear = New TextBox() With {.Location = New Point(150, 415), .Width = 40}
        Dim lblRecipientDateofBirthYear = New Label() With {.Text = "年", .Location = New Point(190, 420), .AutoSize = True}
        cmbRecipientDateofBirthMonth = New ComboBox() With {.Location = New Point(210, 415), .Width = 40, .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbRecipientDateofBirthMonth.Items.AddRange(New String() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"})
        cmbRecipientDateofBirthMonth.SelectedIndex = 0
        Dim lblRecipientDateofBirthMonth = New Label() With {.Text = "月", .Location = New Point(250, 420), .AutoSize = True}
        cmbRecipientDateofBirthDay = New ComboBox() With {.Location = New Point(270, 415), .Width = 40, .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbRecipientDateofBirthDay.Items.AddRange(New String() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})
        cmbRecipientDateofBirthDay.SelectedIndex = 0
        Dim lblRecipientDateofBirthDay = New Label() With {.Text = "日", .Location = New Point(310, 420), .AutoSize = True}

        Dim lblGender = New Label() With {.Text = "성별:", .Location = New Point(20, 460), .AutoSize = True}
        cmbGender = New ComboBox() With {.Location = New Point(50, 455), .Width = 305, .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbGender.Items.AddRange(New String() {"M", "F"})
        cmbGender.SelectedIndex = 0

        Dim lblAge = New Label() With {.Text = "나이:", .Location = New Point(20, 500), .AutoSize = True}
        txtAge = New TextBox() With {.Location = New Point(50, 495), .Width = 305}

        btnRegister = New Button() With {.Text = "등록", .Location = New Point(10, 540), .Width = 75, .Height = 30}
        AddHandler btnRegister.Click, AddressOf btnRegister_Click

        btnCopy = New Button() With {.Text = "계약자 정보 복사", .Location = New Point(97, 540), .Width = 85, .Height = 30}
        AddHandler btnCopy.Click, AddressOf btnCopy_Click

        btnAgeInput = New Button() With {.Text = "나이 자동 입력", .Location = New Point(198, 540), .Width = 75, .Height = 30}
        AddHandler btnAgeInput.Click, AddressOf btnAgeInput_Click

        Me.Controls.Add(lblId)
        Me.Controls.Add(txtId)
        Me.Controls.Add(lblContractorNameKanji)
        Me.Controls.Add(txtContractorNameKanji)
        Me.Controls.Add(lblContractorNameKana)
        Me.Controls.Add(txtContractorNameKana)
        Me.Controls.Add(lblContractorAddressKanji)
        Me.Controls.Add(txtContractorAddressKanji)
        Me.Controls.Add(lblContractorAddressKana)
        Me.Controls.Add(txtContractorAddressKana)
        Me.Controls.Add(lblContractorDateofBirth)
        Me.Controls.Add(cmbContractorDateofBirthEra)
        Me.Controls.Add(txtContractorDateofBirthYear)
        Me.Controls.Add(lblContractorDateofBirthYear)
        Me.Controls.Add(cmbContractorDateofBirthMonth)
        Me.Controls.Add(lblContractorDateofBirthMonth)
        Me.Controls.Add(cmbContractorDateofBirthDay)
        Me.Controls.Add(lblContractorDateofBirthDay)
        Me.Controls.Add(lblRecipientNameKanji)
        Me.Controls.Add(txtRecipientNameKanji)
        Me.Controls.Add(lblRecipientNameKana)
        Me.Controls.Add(txtRecipientNameKana)
        Me.Controls.Add(lblRecipientAddressKanji)
        Me.Controls.Add(txtRecipientAddressKanji)
        Me.Controls.Add(lblRecipientAddressKana)
        Me.Controls.Add(txtRecipientAddressKana)
        Me.Controls.Add(lblRecipientDateofBirth)
        Me.Controls.Add(cmbRecipientDateofBirthEra)
        Me.Controls.Add(txtRecipientDateofBirthYear)
        Me.Controls.Add(lblRecipientDateofBirthYear)
        Me.Controls.Add(cmbRecipientDateofBirthMonth)
        Me.Controls.Add(lblRecipientDateofBirthMonth)
        Me.Controls.Add(cmbRecipientDateofBirthDay)
        Me.Controls.Add(lblRecipientDateofBirthDay)
        Me.Controls.Add(lblGender)
        Me.Controls.Add(cmbGender)
        Me.Controls.Add(lblAge)
        Me.Controls.Add(txtAge)
        Me.Controls.Add(btnRegister)
        Me.Controls.Add(btnCopy)
        Me.Controls.Add(btnAgeInput)
        Try
            Dim param As New MeCabParam()
            param.DicDir = "......\MeCab\dic\ipadic"
            mecabTagger = mecabTagger.Create(param)
        Catch ex As Exception
            MessageBox.Show("MeCab 초기화에 실패했습니다: " & ex.Message)
        End Try
    End Sub

    Private Sub btnRegister_Click(sender As Object, e As EventArgs)
        Dim id As String = txtId.Text.Trim()
        Dim contractorNameKanji As String = txtContractorNameKanji.Text.Trim()
        Dim contractorNameKana As String = txtContractorNameKana.Text.Trim()
        Dim contractorAddressKanji As String = txtContractorAddressKanji.Text.Trim()
        Dim contractorAddressKana As String = txtContractorAddressKana.Text.Trim()
        Dim contractorDateofBirth As String = $"{If(cmbContractorDateofBirthEra.SelectedItem IsNot Nothing, cmbContractorDateofBirthEra.SelectedItem.ToString(), "")}" & $"{txtContractorDateofBirthYear.Text.Trim()}年" & $"{If(cmbContractorDateofBirthMonth.SelectedItem IsNot Nothing, cmbContractorDateofBirthMonth.SelectedItem.ToString(), "")}月" & $"{If(cmbContractorDateofBirthDay.SelectedItem IsNot Nothing, cmbContractorDateofBirthDay.SelectedItem.ToString(), "")}日"
        Dim recipientNameKanji As String = txtRecipientNameKanji.Text.Trim()
        Dim recipientNameKana As String = txtRecipientNameKana.Text.Trim()
        Dim recipientAddressKanji As String = txtRecipientAddressKanji.Text.Trim()
        Dim recipientAddressKana As String = txtRecipientAddressKana.Text.Trim()
        Dim recipientDateofBirth As String = $"{If(cmbRecipientDateofBirthEra.SelectedItem IsNot Nothing, cmbRecipientDateofBirthEra.SelectedItem.ToString(), "")}" & $"{txtRecipientDateofBirthYear.Text.Trim()}年" & $"{If(cmbRecipientDateofBirthMonth.SelectedItem IsNot Nothing, cmbRecipientDateofBirthMonth.SelectedItem.ToString(), "")}月" & $"{If(cmbRecipientDateofBirthDay.SelectedItem IsNot Nothing, cmbRecipientDateofBirthDay.SelectedItem.ToString(), "")}日"
        Dim gender As String = cmbGender.SelectedItem.ToString()
        Dim age As String = txtAge.Text.Trim()

        If String.IsNullOrEmpty(contractorNameKanji) Then
            MessageBox.Show("계약자 명을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            If KanjiValidation(contractorNameKanji) = False Then
                MessageBox.Show("계약자 명을 한자로 입력해주세요. 상용한자에 없는 경우에는 카타카나로 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        End If

        If String.IsNullOrEmpty(contractorNameKana) Then
            MessageBox.Show("계약자 명을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            If KanaValidation(contractorNameKana) = False Then
                MessageBox.Show("계약자 명을 카타카나로 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        End If

        If String.IsNullOrEmpty(contractorAddressKanji) Then
            MessageBox.Show("계약자 주소를 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            If AddressValidation(contractorAddressKanji) = False Then
                MessageBox.Show("계약자 주소를 한자로 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        End If

        If String.IsNullOrEmpty(contractorAddressKana) Then
            MessageBox.Show("계약자 주소를 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            If AddressValidation(contractorAddressKana) = False Then
                MessageBox.Show("계약자 주소를 카타카나로 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        End If

        If cmbContractorDateofBirthEra.SelectedItem Is Nothing Then
            MessageBox.Show("연호를 선택해 주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If String.IsNullOrEmpty(txtContractorDateofBirthYear.Text) Then
            MessageBox.Show("계약자 생년을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If cmbContractorDateofBirthMonth.SelectedItem Is Nothing Then
            MessageBox.Show("계약자 생월을 선택해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If cmbContractorDateofBirthDay.SelectedItem Is Nothing Then
            MessageBox.Show("계약자 생일을 선택해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If DateOfBirthValidation(recipientDateofBirth) = DateTime.MinValue Then
            MessageBox.Show("계약자 생년월일을 올바르게 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        If String.IsNullOrEmpty(recipientNameKanji) Then
            MessageBox.Show("수취인 명을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            If KanjiValidation(recipientNameKanji) = False Then
                MessageBox.Show("수취인 명을 한자로 입력해주세요. 상용한자에 없는 경우에는 카타카나로 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        End If

        If String.IsNullOrEmpty(recipientNameKana) Then
            MessageBox.Show("수취인 명을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            If KanaValidation(recipientNameKana) = False Then
                MessageBox.Show("수취인 명을 카타카나로 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        If String.IsNullOrEmpty(recipientAddressKanji) Then
            MessageBox.Show("수취인 주소를 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            If AddressValidation(recipientAddressKanji) = False Then
                MessageBox.Show("수취인 주소를 한자로 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        If String.IsNullOrEmpty(recipientAddressKana) Then
            MessageBox.Show("수취인 주소를 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            If AddressValidation(recipientAddressKana) = False Then
                MessageBox.Show("수취인 주소를 카타카나로 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        If cmbRecipientDateofBirthEra.SelectedItem Is Nothing Then
            MessageBox.Show("연호를 선택해 주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If String.IsNullOrEmpty(txtRecipientDateofBirthYear.Text) Then
            MessageBox.Show("수취인 생년을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If cmbRecipientDateofBirthMonth.SelectedItem Is Nothing Then
            MessageBox.Show("수취인 생월을 선택해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If cmbRecipientDateofBirthDay.SelectedItem Is Nothing Then
            MessageBox.Show("수취인 생일을 선택해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If DateOfBirthValidation(recipientDateofBirth) = DateTime.MinValue Then
            MessageBox.Show("수취인 생년월일을 올바르게 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

                ' 4. ID 중복 검사
                Dim checkQuery As String = "SELECT COUNT(1) FROM UserTable WHERE Id = @Id"
                Using checkCmd As New SQLiteCommand(checkQuery, conn)
                    checkCmd.Parameters.AddWithValue("@Id", id)

                    Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                    If count > 0 Then
                        MessageBox.Show("이미 존재하는 ID 입니다. (중복 등록 불가)", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                End Using

                ' 5. 등록 (current_process="complete", InputSource="APL")
                Dim insertQuery As String = "INSERT INTO UserTable (Id, contractorNameKanji, contractorNameKana, contractorAddressKanji, contractorAddressKana, contractorDateofBirth, recipientNameKanji, recipientNameKana, recipientAddressKanji, recipientAddressKana, recipientDateofBirth, gender, age, current_process, InputSource) VALUES (@Id, @contractorNameKanji, @contractorNameKana, @contractorAddressKanji, @contractorAddressKana, @contractorDateofBirth, @recipientNameKanji, @recipientNameKana, @recipientAddressKanji, @recipientAddressKana, @recipientDateofBirth, @Gender, @Age, 'complete', 'APL')"
                Using insertCmd As New SQLiteCommand(insertQuery, conn)
                    insertCmd.Parameters.AddWithValue("@Id", id)
                    insertCmd.Parameters.AddWithValue("@contractorNameKanji", contractorNameKanji)
                    insertCmd.Parameters.AddWithValue("@contractorNameKana", contractorNameKana)
                    insertCmd.Parameters.AddWithValue("@contractorAddressKanji", contractorAddressKanji)
                    insertCmd.Parameters.AddWithValue("@contractorAddressKana", contractorAddressKana)
                    insertCmd.Parameters.AddWithValue("@contractorDateofBirth", contractorDateofBirth)
                    insertCmd.Parameters.AddWithValue("@recipientNameKanji", recipientNameKanji)
                    insertCmd.Parameters.AddWithValue("@recipientNameKana", recipientNameKana)
                    insertCmd.Parameters.AddWithValue("@recipientAddressKanji", recipientAddressKanji)
                    insertCmd.Parameters.AddWithValue("@recipientAddressKana", recipientAddressKana)
                    insertCmd.Parameters.AddWithValue("@recipientDateofBirth", recipientDateofBirth)
                    insertCmd.Parameters.AddWithValue("@Gender", gender)
                    insertCmd.Parameters.AddWithValue("@Age", age)

                    insertCmd.ExecuteNonQuery()
                End Using

                MessageBox.Show($"정상적으로 등록되었습니다." & vbCrLf & $"ID: {id}" & vbCrLf & $"계약자 명(한자): {contractorNameKanji}", "등록 완료", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Clear fields after success
                txtId.Clear()
                txtContractorNameKanji.Clear()
                txtContractorNameKana.Clear()
                txtContractorAddressKanji.Clear()
                txtContractorAddressKana.Clear()
                cmbContractorDateofBirthEra.SelectedIndex = 0
                txtContractorDateofBirthYear.Clear()
                cmbContractorDateofBirthMonth.SelectedIndex = 0
                cmbContractorDateofBirthDay.SelectedIndex = 0
                txtRecipientNameKanji.Clear()
                txtRecipientNameKana.Clear()
                txtRecipientAddressKanji.Clear()
                txtRecipientAddressKana.Clear()
                cmbRecipientDateofBirthEra.SelectedIndex = 0
                txtRecipientDateofBirthYear.Clear()
                cmbRecipientDateofBirthMonth.SelectedIndex = 0
                cmbRecipientDateofBirthDay.SelectedIndex = 0
                txtAge.Clear()
                cmbGender.SelectedIndex = 0
            End Using
        Catch ex As Exception
            MessageBox.Show($"오류 발생: {ex.Message}", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btnCopy_Click(sender As Object, e As EventArgs)
        Dim contractorNameKanji As String = txtContractorNameKanji.Text
        Dim contractorNameKana As String = txtContractorNameKana.Text
        Dim contractorAddressKanji As String = txtContractorAddressKanji.Text
        Dim contractorAddressKana As String = txtContractorAddressKana.Text
        Dim contractorDateofBirthEra As String = If(cmbContractorDateofBirthEra.SelectedItem IsNot Nothing, cmbContractorDateofBirthEra.SelectedItem.ToString(), "")
        Dim contractorDateofBirthYear As String = txtContractorDateofBirthYear.Text
        Dim contractorDateofBirthMonth As String = If(cmbContractorDateofBirthMonth.SelectedItem IsNot Nothing, cmbContractorDateofBirthMonth.SelectedItem.ToString(), "")
        Dim contractorDateofBirthDay As String = If(cmbContractorDateofBirthDay.SelectedItem IsNot Nothing, cmbContractorDateofBirthDay.SelectedItem.ToString(), "")
        Dim contractorDateofBirth As String = contractorDateofBirthEra & $"{txtContractorDateofBirthYear.Text.Trim()}年" & contractorDateofBirthMonth & "月" & contractorDateofBirthDay & "日"

        If String.IsNullOrEmpty(contractorNameKanji) Then
            MessageBox.Show("계약자 명을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            If KanjiValidation(contractorNameKanji) = False Then
                MessageBox.Show("계약자 명을 한자로 입력해주세요. 상용한자에 없는 경우에는 카타카나로 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        End If

        If String.IsNullOrEmpty(contractorNameKana) Then
            MessageBox.Show("계약자 명을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            If KanaValidation(contractorNameKana) = False Then
                MessageBox.Show("계약자 명을 카타카나로 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        End If

        If String.IsNullOrEmpty(contractorAddressKanji) Then
            MessageBox.Show("계약자 주소를 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            If AddressValidation(contractorAddressKanji) = False Then
                MessageBox.Show("계약자 주소를 한자로 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        End If

        If String.IsNullOrEmpty(contractorAddressKana) Then
            MessageBox.Show("계약자 주소를 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            If AddressValidation(contractorAddressKana) = False Then
                MessageBox.Show("계약자 주소를 카타카나로 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        End If

        If cmbContractorDateofBirthEra.SelectedItem Is Nothing Then
            MessageBox.Show("연호를 선택해 주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If String.IsNullOrEmpty(txtContractorDateofBirthYear.Text) Then
            MessageBox.Show("계약자 생년을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If cmbContractorDateofBirthMonth.SelectedItem Is Nothing Then
            MessageBox.Show("계약자 생월을 선택해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If cmbContractorDateofBirthDay.SelectedItem Is Nothing Then
            MessageBox.Show("계약자 생일을 선택해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        txtRecipientNameKanji.Text = contractorNameKanji
        txtRecipientNameKana.Text = contractorNameKana
        txtRecipientAddressKanji.Text = contractorAddressKanji
        txtRecipientAddressKana.Text = contractorAddressKana
        cmbRecipientDateofBirthEra.SelectedItem = contractorDateofBirthEra
        txtRecipientDateofBirthYear.Text = contractorDateofBirthYear
        cmbRecipientDateofBirthMonth.SelectedItem = contractorDateofBirthMonth
        cmbRecipientDateofBirthDay.SelectedItem = contractorDateofBirthDay
    End Sub
    Private Sub btnAgeInput_Click(sender As Object, e As EventArgs)
        Dim contractorDateofBirthEra As String = If(cmbContractorDateofBirthEra.SelectedItem IsNot Nothing, cmbContractorDateofBirthEra.SelectedItem.ToString(), "")
        Dim contractorDateofBirthYear As String = txtContractorDateofBirthYear.Text
        Dim contractorDateofBirthMonth As String = If(cmbContractorDateofBirthMonth.SelectedItem IsNot Nothing, cmbContractorDateofBirthMonth.SelectedItem.ToString(), "")
        Dim contractorDateofBirthDay As String = If(cmbContractorDateofBirthDay.SelectedItem IsNot Nothing, cmbContractorDateofBirthDay.SelectedItem.ToString(), "")
        Dim contractorDateofBirth As String = contractorDateofBirthEra & $"{txtContractorDateofBirthYear.Text.Trim()}年" & contractorDateofBirthMonth & "月" & contractorDateofBirthDay & "日"
        If String.IsNullOrEmpty(contractorDateofBirthEra) Then
            MessageBox.Show("연호를 선택해 주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If String.IsNullOrEmpty(contractorDateofBirthYear) Then
            MessageBox.Show("계약자 생년을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If String.IsNullOrEmpty(contractorDateofBirthMonth) Then
            MessageBox.Show("계약자 생월을 선택해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If String.IsNullOrEmpty(contractorDateofBirthDay) Then
            MessageBox.Show("계약자 생일을 선택해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If DateOfBirthValidation(contractorDateofBirth) = DateTime.MinValue Then
            MessageBox.Show("계약자 생년월일을 올바르게 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        Dim DateOfBirth As DateTime
        DateOfBirth = DateOfBirthValidation(contractorDateofBirth)
        txtAge.Text = AgeCalculator(DateOfBirth).ToString
    End Sub
    Private Sub txtContractorNameKanjiTextChanged(sender As Object, e As EventArgs) Handles txtContractorNameKanji.TextChanged
        input = txtContractorNameKanji.Text.Trim()
        If String.IsNullOrWhiteSpace(input) Then
            txtContractorNameKana.Text = ""
            Return
        End If
        txtContractorNameKana.Text = InputToKatakana(input)
    End Sub
    Private Sub txtContractorNameKanaTextChanged(sender As Object, e As EventArgs) Handles txtContractorNameKana.TextChanged
        input = txtContractorNameKana.Text.Trim()
        Dim Pattern As String = "^[\u30A0-\u30FF\s]*$"
        If Not Regex.IsMatch(input, Pattern) Then
            MessageBox.Show("계약자 명을 카타카나로만 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtContractorNameKana.Focus()
        End If
    End Sub
    Private Sub txtContractorDateofBirthYearTextChanged(sender As Object, e As EventArgs) Handles txtContractorDateofBirthYear.TextChanged
        input = txtContractorDateofBirthYear.Text.Trim()
        Dim Pattern As String = "^\d{0,4}$"
        If Not Regex.IsMatch(input, Pattern) Then
            MessageBox.Show("한자리에서 네자릿수의 숫자만 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtContractorDateofBirthYear.Focus()
        End If
    End Sub
    Private Sub txtContractorAddressKanjiTextChanged(sender As Object, e As EventArgs) Handles txtContractorAddressKanji.TextChanged
        input = txtContractorAddressKanji.Text.Trim()
        If String.IsNullOrWhiteSpace(input) Then
            txtContractorAddressKana.Text = ""
            Return
        End If
        txtContractorAddressKana.Text = InputToKatakana(input)
    End Sub
    Private Sub txtRecipientNameKanjiTextChanged(sender As Object, e As EventArgs) Handles txtRecipientNameKanji.TextChanged
        input = txtRecipientNameKanji.Text.Trim()
        If String.IsNullOrWhiteSpace(input) Then
            txtRecipientNameKana.Text = ""
            Return
        End If
        txtRecipientNameKana.Text = InputToKatakana(input)
    End Sub
    Private Sub txtRecipientNameKanaTextChanged(sender As Object, e As EventArgs) Handles txtRecipientNameKana.TextChanged
        input = txtRecipientNameKana.Text.Trim()
        Dim Pattern As String = "^[\u30A0-\u30FF\s]*$"
        If Not Regex.IsMatch(input, Pattern) Then
            MessageBox.Show("수취인 명을 카타카나로만 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtRecipientNameKana.Focus()
        End If
    End Sub
    Private Sub txtRecipientAddressKanjiTextChanged(sender As Object, e As EventArgs) Handles txtRecipientAddressKanji.TextChanged
        input = txtRecipientAddressKanji.Text.Trim()
        If String.IsNullOrWhiteSpace(input) Then
            txtRecipientAddressKana.Text = ""
            Return
        End If
        txtRecipientAddressKana.Text = InputToKatakana(input)
    End Sub
    Private Sub txtRecipientDateofBirthYearTextChanged(sender As Object, e As EventArgs) Handles txtRecipientDateofBirthYear.TextChanged
        input = txtRecipientDateofBirthYear.Text.Trim()
        Dim Pattern As String = "^\d{0,4}$"
        If Not Regex.IsMatch(input, Pattern) Then
            MessageBox.Show("한자리에서 네자릿수의 숫자만 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtRecipientDateofBirthYear.Focus()
        End If
    End Sub
End Class