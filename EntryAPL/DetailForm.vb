Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data.SQLite

Public Class DetailForm
    Inherits Form

    Private connectionString As String
    Private currentId As String
    Private pattern As String
    Private input As String


    Private txtId As TextBox
    Private txtContractorNameKanji As TextBox
    Private txtContractorNameKana As TextBox
    Private txtContractorAddressKanji As TextBox
    Private txtContractorAddressKana As TextBox
    Private txtContractorDateofBirth As TextBox
    Private txtRecipientNameKanji As TextBox
    Private txtRecipientNameKana As TextBox
    Private txtRecipientAddressKanji As TextBox
    Private txtRecipientAddressKana As TextBox
    Private txtRecipientDateofBirth As TextBox
    Private cmbGender As ComboBox
    Private txtAge As TextBox
    Private txtProcess As TextBox
    Private txtSource As TextBox
    Private btnSave As Button
    Private btnCopy As Button
    Private btnAgeInput As Button
    Private btnCancel As Button

    Function KanjiValidation(input As String) As Boolean
        pattern = "^[\u30A0-\u30FF\u4E00-\u9FFF\s]+$"
        Return Regex.IsMatch(input, pattern)
    End Function
    Function KanaValidation(input As String) As Boolean
        pattern = "^[\u30A0-\u30FF\s]+$"
        Return Regex.IsMatch(input, pattern)
    End Function
    Function AddressValidation(input As String) As Boolean
        pattern = "^[0-9A-Za-z\u0370-\u03FF\u3040-\u30FF\u31F0-\u31FF\u3400-\u4DBF\u4E00-\u9FFF\uFF10-\uFF19\uFF21-\uFF3A\uFF41-\uFF5A\uFF65-\uFF9F、。・ー「」『』【】\s]+$"
        Return Regex.IsMatch(input, pattern)
    End Function
    Function DateOfBirthValidation(input As String) As (Valid As Boolean, DateOfBirth As DateTime)
        Dim dobPattern As New Regex(
        "^(?:" &
        "(\d{4})[-/.](0?[1-9]|1[0-2])[-/.](0?[1-9]|[12][0-9]|3[01])|" &
        "(\d{4})年(0?[1-9]|1[0-2])月(0?[1-9]|[12][0-9]|3[01])日|" &
        "(明治|大正|昭和|平成|令和)(\d{1,2})年(0?[1-9]|1[0-2])月(0?[1-9]|[12][0-9]|3[01])日" &
        ")$",
        RegexOptions.Compiled
    )
        Dim eraRanges As New Dictionary(Of String, (Integer, Integer, Integer, Integer, Integer, Integer)) From {
        {"明治", (1868, 9, 8, 1912, 7, 29)},
        {"大正", (1912, 7, 30, 1926, 12, 24)},
        {"昭和", (1926, 12, 25, 1989, 1, 7)},
        {"平成", (1989, 1, 8, 2019, 4, 30)},
        {"令和", (2019, 5, 1, 9999, 12, 31)}
    }
        Dim m As Match = dobPattern.Match(input.Trim())
        Dim year As Integer, month As Integer, day As Integer

        If m.Groups(1).Success Then
            year = CInt(m.Groups(1).Value)
            month = CInt(m.Groups(2).Value)
            day = CInt(m.Groups(3).Value)

        ElseIf m.Groups(4).Success Then
            year = CInt(m.Groups(4).Value)
            month = CInt(m.Groups(5).Value)
            day = CInt(m.Groups(6).Value)

        ElseIf m.Groups(7).Success Then
            Dim eraName = m.Groups(7).Value
            Dim eraYear = CInt(m.Groups(8).Value)
            month = CInt(m.Groups(9).Value)
            day = CInt(m.Groups(10).Value)

            Dim era = eraRanges(eraName)
            year = era.Item1 + eraYear - 1

            Dim dobEra As Date
            Try
                dobEra = New Date(year, month, day)
            Catch ex As ArgumentOutOfRangeException
                Return (False, DateTime.MinValue)
            End Try

            Dim startEra As New Date(era.Item1, era.Item2, era.Item3)
            Dim endEra As New Date(era.Item4, era.Item5, era.Item6)
            If dobEra < startEra OrElse dobEra > endEra Then
                Return (False, DateTime.MinValue)
            End If
        End If

        Try
            Dim dob As New DateTime(year, month, day)
            Return (True, dob)
        Catch ex As ArgumentOutOfRangeException
            Return (False, DateTime.MinValue)
        End Try
    End Function
    Function AgeCalculator(DateOfBirth As DateTime) As Integer
        Dim today As DateTime = Date.Today
        Dim age As Integer
        If DateOfBirth > Date.Today Then
            Return 0
        End If
        age = today.Year - DateOfBirth.Year
        If (today.Month < DateOfBirth.Month) OrElse (today.Month = DateOfBirth.Month AndAlso today.Day < DateOfBirth.Day) Then
            age -= 1
        End If
        Return age
    End Function

    Public Sub New(connString As String, id As String)
        InitializeComponent()
        Me.connectionString = connString
        Me.currentId = id

        InitializeUI()
        LoadData()
    End Sub

    Private Sub InitializeUI()
        Me.Text = "데이터 상세 및 수정"
        Me.Size = New Size(320, 320)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        Dim yPos As Integer = 20
        Dim spacing As Integer = 35

        ' 1. Id (Read-only)
        Me.Controls.Add(New Label() With {.Text = "ID (수정불가):", .Location = New Point(20, yPos), .AutoSize = True})
        txtId = New TextBox() With {.Location = New Point(120, yPos), .Width = 300, .ReadOnly = True, .BackColor = Color.LightGray}
        Me.Controls.Add(txtId)
        yPos += spacing

        ' 2. ContractorNameKanji
        Me.Controls.Add(New Label() With {.Text = "계약자 명(한자):", .Location = New Point(20, yPos), .AutoSize = True})
        txtContractorNameKanji = New TextBox() With {.Location = New Point(120, yPos), .Width = 300}
        Me.Controls.Add(txtContractorNameKanji)
        yPos += spacing

        ' 3. ContractorNameKana
        Me.Controls.Add(New Label() With {.Text = "계약자 명(카나):", .Location = New Point(20, yPos), .AutoSize = True})
        txtContractorNameKana = New TextBox() With {.Location = New Point(120, yPos), .Width = 300}
        Me.Controls.Add(txtContractorNameKana)
        yPos += spacing

        ' 4. ContractorAddressKanji
        Me.Controls.Add(New Label() With {.Text = "계약자 주소(한자):", .Location = New Point(20, yPos), .AutoSize = True})
        txtContractorAddressKanji = New TextBox() With {.Location = New Point(120, yPos), .Width = 300}
        Me.Controls.Add(txtContractorAddressKanji)
        yPos += spacing

        ' 5. ContractorAddressKana
        Me.Controls.Add(New Label() With {.Text = "계약자 주소(카나):", .Location = New Point(20, yPos), .AutoSize = True})
        txtContractorAddressKana = New TextBox() With {.Location = New Point(120, yPos), .Width = 300}
        Me.Controls.Add(txtContractorAddressKana)
        yPos += spacing

        ' 6. ContractorDateofBirth
        Me.Controls.Add(New Label() With {.Text = "계약자 생년월일:", .Location = New Point(20, yPos), .AutoSize = True})
        txtContractorDateofBirth = New TextBox() With {.Location = New Point(120, yPos), .Width = 300}
        Me.Controls.Add(txtContractorDateofBirth)
        yPos += spacing

        ' 7. RecipientNameKanji
        Me.Controls.Add(New Label() With {.Text = "수취인 명(한자):", .Location = New Point(20, yPos), .AutoSize = True})
        txtRecipientNameKanji = New TextBox() With {.Location = New Point(120, yPos), .Width = 300}
        Me.Controls.Add(txtRecipientNameKanji)
        yPos += spacing

        ' 8. RecipientNameKana
        Me.Controls.Add(New Label() With {.Text = "수취인 명(카나):", .Location = New Point(20, yPos), .AutoSize = True})
        txtRecipientNameKana = New TextBox() With {.Location = New Point(120, yPos), .Width = 300}
        Me.Controls.Add(txtRecipientNameKana)
        yPos += spacing

        ' 9. RecipientAddressKanji
        Me.Controls.Add(New Label() With {.Text = "수취인 주소(한자):", .Location = New Point(20, yPos), .AutoSize = True})
        txtRecipientAddressKanji = New TextBox() With {.Location = New Point(120, yPos), .Width = 300}
        Me.Controls.Add(txtRecipientAddressKanji)
        yPos += spacing

        ' 10. RecipientAddressKana
        Me.Controls.Add(New Label() With {.Text = "수취인 주소(카나):", .Location = New Point(20, yPos), .AutoSize = True})
        txtRecipientAddressKana = New TextBox() With {.Location = New Point(120, yPos), .Width = 300}
        Me.Controls.Add(txtRecipientAddressKana)
        yPos += spacing

        ' 11. RecipientDateofBirth
        Me.Controls.Add(New Label() With {.Text = "수취인 생년월일:", .Location = New Point(20, yPos), .AutoSize = True})
        txtRecipientDateofBirth = New TextBox() With {.Location = New Point(120, yPos), .Width = 300}
        Me.Controls.Add(txtRecipientDateofBirth)
        yPos += spacing

        ' 12. Gender
        Me.Controls.Add(New Label() With {.Text = "성별:", .Location = New Point(20, yPos), .AutoSize = True})
        cmbGender = New ComboBox() With {.Location = New Point(120, yPos), .Width = 300, .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbGender.Items.AddRange(New String() {"M", "F"})
        Me.Controls.Add(cmbGender)
        yPos += spacing

        ' 13. Age
        Me.Controls.Add(New Label() With {.Text = "나이:", .Location = New Point(20, yPos), .AutoSize = True})
        txtAge = New TextBox() With {.Location = New Point(120, yPos), .Width = 300}
        Me.Controls.Add(txtAge)
        yPos += spacing

        ' 14. current_process (Read-only)
        Me.Controls.Add(New Label() With {.Text = "프로세스:", .Location = New Point(20, yPos), .AutoSize = True})
        txtProcess = New TextBox() With {.Location = New Point(120, yPos), .Width = 300, .ReadOnly = True, .BackColor = Color.LightGray}
        Me.Controls.Add(txtProcess)
        yPos += spacing

        ' 15. InputSource (Read-only)
        Me.Controls.Add(New Label() With {.Text = "입력소스:", .Location = New Point(20, yPos), .AutoSize = True})
        txtSource = New TextBox() With {.Location = New Point(120, yPos), .Width = 300, .ReadOnly = True, .BackColor = Color.LightGray}
        Me.Controls.Add(txtSource)
        yPos += spacing

        ' Buttons
        btnSave = New Button() With {.Text = "확인(수정)", .Location = New Point(5, yPos), .Width = 100, .Height = 30}
        AddHandler btnSave.Click, AddressOf btnSave_Click
        Me.Controls.Add(btnSave)

        btnCopy = New Button() With {.Text = "계약자 정보 복사", .Location = New Point(110, yPos), .Width = 100, .Height = 30}
        AddHandler btnCopy.Click, AddressOf btnCopy_Click
        Me.Controls.Add(btnCopy)

        btnAgeInput = New Button() With {.Text = "나이 자동 입력", .Location = New Point(215, yPos), .Width = 100, .Height = 30}
        AddHandler btnAgeInput.Click, AddressOf btnAgeInput_Click
        Me.Controls.Add(btnAgeInput)

        btnCancel = New Button() With {.Text = "뒤로가기", .Location = New Point(320, yPos), .Width = 100, .Height = 30}
        AddHandler btnCancel.Click, Sub(s, e) Me.Close()
        Me.Controls.Add(btnCancel)
    End Sub

    Private Sub LoadData()
        Try
            Using conn As New SQLiteConnection(connectionString)
                conn.Open()
                Dim query As String = "SELECT Id, contractorNameKanji, contractorNameKana, contractorAddressKanji, contractorAddressKana, contractorDateofBirth, recipientNameKanji, recipientNameKana, recipientAddressKanji, recipientAddressKana, recipientDateofBirth, gender, age, current_process, InputSource FROM UserTable WHERE Id = @Id"
                Using cmd As New SQLiteCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Id", currentId)
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            txtId.Text = reader("Id").ToString()
                            txtContractorNameKanji.Text = reader("contractorNameKanji").ToString()
                            txtContractorNameKana.Text = reader("contractorNameKana").ToString()
                            txtContractorAddressKanji.Text = reader("contractorAddressKanji").ToString()
                            txtContractorAddressKana.Text = reader("contractorAddressKana").ToString()
                            txtContractorDateofBirth.Text = reader("contractorDateofBirth").ToString()
                            txtRecipientNameKanji.Text = reader("recipientNameKanji").ToString()
                            txtRecipientNameKana.Text = reader("recipientNameKana").ToString()
                            txtRecipientAddressKanji.Text = reader("recipientAddressKanji").ToString()
                            txtRecipientAddressKana.Text = reader("recipientAddressKana").ToString()
                            txtRecipientDateofBirth.Text = reader("recipientDateofBirth").ToString()


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

    Private Sub btnSave_Click(sender As Object, e As EventArgs)

        Dim contractorNameKanji As String = txtContractorNameKanji.Text.Trim()
        Dim contractorNameKana As String = txtContractorNameKana.Text.Trim()
        Dim contractorAddressKanji As String = txtContractorAddressKanji.Text.Trim()
        Dim contractorAddressKana As String = txtContractorAddressKana.Text.Trim()
        Dim contractorDateofBirth As String = txtContractorDateofBirth.Text.Trim()
        Dim recipientNameKanji As String = txtRecipientNameKanji.Text.Trim()
        Dim recipientNameKana As String = txtRecipientNameKana.Text.Trim()
        Dim recipientAddressKanji As String = txtRecipientAddressKanji.Text.Trim()
        Dim recipientAddressKana As String = txtRecipientAddressKana.Text.Trim()
        Dim recipientDateofBirth As String = txtRecipientDateofBirth.Text.Trim()
        Dim age As String = txtAge.Text.Trim()
        Dim gender As String = If(cmbGender.SelectedItem IsNot Nothing, cmbGender.SelectedItem.ToString(), "")

        If String.IsNullOrEmpty(contractorNameKanji) Then
            MessageBox.Show("계약자 명을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            Dim Flag As Boolean = KanjiValidation(contractorNameKanji)
            If Flag = False Then
                MessageBox.Show("계약자 명을 한자로 입력해주세요. 상용한자에 없는 경우에는 카타카나로 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        End If

        If String.IsNullOrEmpty(contractorNameKana) Then
            MessageBox.Show("계약자 명을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            Dim Flag As Boolean = KanaValidation(contractorNameKana)
            If Flag = False Then
                MessageBox.Show("계약자 명을 카타카나로 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        End If

        If String.IsNullOrEmpty(contractorAddressKanji) Then
            MessageBox.Show("계약자 주소를 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            Dim Flag As Boolean = AddressValidation(contractorAddressKanji)
            If Flag = False Then
                MessageBox.Show("계약자 주소를 한자로 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        End If

        If String.IsNullOrEmpty(contractorAddressKana) Then
            MessageBox.Show("계약자 주소를 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            Dim Flag As Boolean = AddressValidation(contractorAddressKana)
            If Flag = False Then
                MessageBox.Show("계약자 주소를 카타카나로 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        End If

        If String.IsNullOrEmpty(contractorDateofBirth) Then
            MessageBox.Show("계약자 생년월일을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            Dim Flag As Boolean = DateOfBirthValidation(contractorDateofBirth).Valid
            If Flag = False Then
                MessageBox.Show("계약자 생년월일을 올바르게 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        End If

        If String.IsNullOrEmpty(recipientNameKanji) Then
            MessageBox.Show("수취인 명을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            Dim Flag As Boolean = KanjiValidation(recipientNameKanji)
            If Flag = False Then
                MessageBox.Show("수취인 명을 한자로 입력해주세요. 상용한자에 없는 경우에는 카타카나로 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        End If

        If String.IsNullOrEmpty(recipientNameKana) Then
            MessageBox.Show("수취인 명을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            Dim Flag As Boolean = KanaValidation(recipientNameKana)
            If Flag = False Then
                MessageBox.Show("수취인 명을 카타카나로 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        If String.IsNullOrEmpty(recipientAddressKanji) Then
            MessageBox.Show("수취인 주소를 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            Dim Flag As Boolean = AddressValidation(recipientAddressKanji)
            If Flag = False Then
                MessageBox.Show("수취인 주소를 한자로 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        If String.IsNullOrEmpty(recipientAddressKana) Then
            MessageBox.Show("수취인 주소를 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            Dim Flag As Boolean = AddressValidation(recipientAddressKana)
            If Flag = False Then
                MessageBox.Show("수취인 주소를 카타카나로 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        If String.IsNullOrEmpty(recipientDateofBirth) Then
            MessageBox.Show("수취인 생년월일을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            Dim Flag As Boolean = DateOfBirthValidation(recipientDateofBirth).Valid
            If Flag = False Then
                MessageBox.Show("수취인 생년월일을 올바르게 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        Try
            Using conn As New SQLiteConnection(connectionString)
                conn.Open()
                Dim updateQuery As String = "UPDATE UserTable SET contractorNameKanji = @contractorNameKanji, contractorNameKana = @contractorNameKana, contractorAddressKanji = @contractorAddressKanji, contractorAddressKana = @contractorAddressKana, contractorDateofBirth = @contractorDateofBirth, recipientNameKanji = @recipientNameKanji, recipientNameKana = @recipientNameKana, recipientAddressKanji = @recipientAddressKanji, recipientAddressKana = @recipientAddressKana, recipientDateofBirth = @recipientDateofBirth, gender = @Gender, age = @Age WHERE Id = @Id"
                Using cmd As New SQLiteCommand(updateQuery, conn)
                    cmd.Parameters.AddWithValue("@contractorNameKanji", contractorNameKanji)
                    cmd.Parameters.AddWithValue("@contractorNameKana", contractorNameKana)
                    cmd.Parameters.AddWithValue("@contractorAddressKanji", contractorAddressKanji)
                    cmd.Parameters.AddWithValue("@contractorAddressKana", contractorAddressKana)
                    cmd.Parameters.AddWithValue("@contractorDateofBirth", contractorDateofBirth)
                    cmd.Parameters.AddWithValue("@recipientNameKanji", recipientNameKanji)
                    cmd.Parameters.AddWithValue("@recipientNameKana", recipientNameKana)
                    cmd.Parameters.AddWithValue("@recipientAddressKanji", recipientAddressKanji)
                    cmd.Parameters.AddWithValue("@recipientAddressKana", recipientAddressKana)
                    cmd.Parameters.AddWithValue("@recipientDateofBirth", recipientDateofBirth)
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
    Private Sub btnCopy_Click(sender As Object, e As EventArgs)
        Dim contractorNameKanji As String = txtContractorNameKanji.Text
        Dim contractorNameKana As String = txtContractorNameKana.Text
        Dim contractorAddressKanji As String = txtContractorAddressKanji.Text
        Dim contractorAddressKana As String = txtContractorAddressKana.Text
        Dim contractorDateofBirth As String = txtContractorDateofBirth.Text

        If String.IsNullOrEmpty(contractorNameKanji) Then
            MessageBox.Show("계약자 명을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            Dim Flag As Boolean = KanjiValidation(contractorNameKanji)
            If Flag = False Then
                MessageBox.Show("계약자 명을 한자로 입력해주세요. 상용한자에 없는 경우에는 카타카나로 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        If String.IsNullOrEmpty(contractorNameKana) Then
            MessageBox.Show("계약자 명을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            Dim Flag As Boolean = KanaValidation(contractorNameKana)
            If Flag = False Then
                MessageBox.Show("계약자 명을 카타카나로 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        If String.IsNullOrEmpty(contractorAddressKanji) Then
            MessageBox.Show("계약자 주소를 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            Dim Flag As Boolean = AddressValidation(contractorAddressKanji)
            If Flag = False Then
                MessageBox.Show("계약자 주소를 한자로 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        If String.IsNullOrEmpty(contractorAddressKana) Then
            MessageBox.Show("계약자 주소를 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            Dim Flag As Boolean = AddressValidation(contractorAddressKana)
            If Flag = False Then
                MessageBox.Show("계약자 주소를 카타카나로 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        If String.IsNullOrEmpty(contractorDateofBirth) Then
            MessageBox.Show("계약자 생년월일을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            Dim Flag As Boolean = DateOfBirthValidation(contractorDateofBirth).Valid
            If Flag = False Then
                MessageBox.Show("계약자 생년월일을 올바르게 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        txtRecipientNameKanji.Text = contractorNameKanji
        txtRecipientNameKana.Text = contractorNameKana
        txtRecipientAddressKanji.Text = contractorAddressKanji
        txtRecipientAddressKana.Text = contractorAddressKana
        txtRecipientDateofBirth.Text = contractorDateofBirth
    End Sub
    Private Sub btnAgeInput_Click(sender As Object, e As EventArgs)
        Dim contractorDateofBirth As String = txtContractorDateofBirth.Text

        If String.IsNullOrEmpty(contractorDateofBirth) Then
            MessageBox.Show("계약자 생년월일을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            Dim Flag As Boolean = DateOfBirthValidation(contractorDateofBirth).Valid
            If Flag = False Then
                MessageBox.Show("계약자 생년월일을 올바르게 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
        Dim DateOfBirth As DateTime
        DateOfBirth = DateOfBirthValidation(contractorDateofBirth).DateOfBirth
        txtAge.Text = AgeCalculator(DateOfBirth).ToString
    End Sub
End Class