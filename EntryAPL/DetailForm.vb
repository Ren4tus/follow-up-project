Imports System
Imports System.Data.SQLite
Imports System.Drawing
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports MeCab
Imports SharedLibrary

Public Class DetailForm
    Inherits Form

    Private connectionString As String
    Private currentId As String
    Private pattern As String
    Private input As String
    Private ReadOnly mecabTagger As MeCabTagger
    Private isFormLoaded As Boolean = False


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
    Private txtProcess As TextBox
    Private txtSource As TextBox
    Private btnSave As Button
    Private btnCopy As Button
    Private btnAgeInput As Button
    Private btnCancel As Button

    Public Sub New(connString As String, id As String)
        InitializeComponent()
        Me.connectionString = connString
        Me.currentId = id

        InitializeUI()
        LoadData()
        Try
            Dim param As New MeCabParam()
            param.DicDir = "......\MeCab\dic\ipadic"
            mecabTagger = mecabTagger.Create(param)
        Catch ex As Exception
            MessageBox.Show("MeCab 초기화에 실패했습니다: " & ex.Message)
        End Try
    End Sub
    Private Function InputToKatakana(input As String) As String
        If mecabTagger Is Nothing Then
            Return "[MeCab 초기화에 실패했습니다]"
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
    Function DateOfBirthSlicer(DateOfBirth As String) As String()
        Dim sliced(2) As String
        Dim dobPattern As New Regex(
        "^(?:" &
        "(明治|大正|昭和|平成|令和)(\d{1,2})年(0?[1-9]|1[0-2])月([1-9]|[12][0-9]|3[01])日|" &
        "(西暦)(\d{4})年([1-9]|1[0-2])月([1-9]|[12][0-9]|3[01])日|" &
        ")$",
        RegexOptions.Compiled
    )
        Dim m As Match = dobPattern.Match(DateOfBirth)

        If m.Groups(1).Success Then
            sliced(0) = m.Groups(2).Value.ToString
            sliced(1) = m.Groups(3).Value.ToString
            sliced(2) = m.Groups(4).Value.ToString
        ElseIf m.Groups(5).Success Then
            sliced(0) = m.Groups(6).Value.ToString
            sliced(1) = m.Groups(7).Value.ToString
            sliced(2) = m.Groups(8).Value.ToString
        End If
        Return sliced
    End Function
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
        txtContractorNameKanji = New TextBox() With {.Location = New Point(120, yPos), .Width = 300, .MaxLength = 12}
        Me.Controls.Add(txtContractorNameKanji)
        yPos += spacing

        ' 3. ContractorNameKana
        Me.Controls.Add(New Label() With {.Text = "계약자 명(카나):", .Location = New Point(20, yPos), .AutoSize = True})
        txtContractorNameKana = New TextBox() With {.Location = New Point(120, yPos), .Width = 300, .MaxLength = 20}
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
        Me.Controls.Add(New Label() With {.Text = "年", .Location = New Point(210, yPos), .AutoSize = True})
        Me.Controls.Add(New Label() With {.Text = "月", .Location = New Point(270, yPos), .AutoSize = True})
        Me.Controls.Add(New Label() With {.Text = "日", .Location = New Point(330, yPos), .AutoSize = True})
        cmbContractorDateofBirthEra = New ComboBox() With {.Location = New Point(120, yPos), .Width = 50, .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbContractorDateofBirthEra.Items.AddRange(New String() {"明治", "大正", "昭和", "平成", "令和", "西暦"})
        txtContractorDateofBirthYear = New TextBox() With {.Location = New Point(170, yPos), .Width = 40}
        cmbContractorDateofBirthMonth = New ComboBox() With {.Location = New Point(230, yPos), .Width = 40, .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbContractorDateofBirthMonth.Items.AddRange(New String() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"})
        cmbContractorDateofBirthDay = New ComboBox() With {.Location = New Point(290, yPos), .Width = 40, .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbContractorDateofBirthDay.Items.AddRange(New String() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})
        Me.Controls.Add(cmbContractorDateofBirthEra)
        Me.Controls.Add(txtContractorDateofBirthYear)
        Me.Controls.Add(cmbContractorDateofBirthMonth)
        Me.Controls.Add(cmbContractorDateofBirthDay)
        yPos += spacing

        ' 7. RecipientNameKanji
        Me.Controls.Add(New Label() With {.Text = "수취인 명(한자):", .Location = New Point(20, yPos), .AutoSize = True})
        txtRecipientNameKanji = New TextBox() With {.Location = New Point(120, yPos), .Width = 300, .MaxLength = 12}
        Me.Controls.Add(txtRecipientNameKanji)
        yPos += spacing

        ' 8. RecipientNameKana
        Me.Controls.Add(New Label() With {.Text = "수취인 명(카나):", .Location = New Point(20, yPos), .AutoSize = True})
        txtRecipientNameKana = New TextBox() With {.Location = New Point(120, yPos), .Width = 300, .MaxLength = 20}
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
        Me.Controls.Add(New Label() With {.Text = "年:", .Location = New Point(210, yPos), .AutoSize = True})
        Me.Controls.Add(New Label() With {.Text = "月:", .Location = New Point(270, yPos), .AutoSize = True})
        Me.Controls.Add(New Label() With {.Text = "日:", .Location = New Point(330, yPos), .AutoSize = True})
        cmbRecipientDateofBirthEra = New ComboBox() With {.Location = New Point(120, yPos), .Width = 50, .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbRecipientDateofBirthEra.Items.AddRange(New String() {"明治", "大正", "昭和", "平成", "令和", "西暦"})
        txtRecipientDateofBirthYear = New TextBox() With {.Location = New Point(170, yPos), .Width = 40}
        cmbRecipientDateofBirthMonth = New ComboBox() With {.Location = New Point(230, yPos), .Width = 40, .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbRecipientDateofBirthMonth.Items.AddRange(New String() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"})
        cmbRecipientDateofBirthDay = New ComboBox() With {.Location = New Point(290, yPos), .Width = 40, .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbRecipientDateofBirthDay.Items.AddRange(New String() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})
        Me.Controls.Add(cmbRecipientDateofBirthEra)
        Me.Controls.Add(txtRecipientDateofBirthYear)
        Me.Controls.Add(cmbRecipientDateofBirthMonth)
        Me.Controls.Add(cmbRecipientDateofBirthDay)
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
                            txtContractorDateofBirthYear.Text = DateOfBirthSlicer(reader("contractorDateofBirth").ToString()).GetValue(0)
                            txtRecipientNameKanji.Text = reader("recipientNameKanji").ToString()
                            txtRecipientNameKana.Text = reader("recipientNameKana").ToString()
                            txtRecipientAddressKanji.Text = reader("recipientAddressKanji").ToString()
                            txtRecipientAddressKana.Text = reader("recipientAddressKana").ToString()
                            txtRecipientDateofBirthYear.Text = DateOfBirthSlicer(reader("recipientDateofBirth").ToString()).GetValue(0)

                            Dim gen As String = reader("gender").ToString()
                            If cmbGender.Items.Contains(gen) Then
                                cmbGender.SelectedItem = gen
                            End If
                            Dim contractorDateofBirthera As String = reader("contractorDateofBirth").ToString().Substring(0, 2)
                            If cmbContractorDateofBirthEra.Items.Contains(contractorDateofBirthera) Then
                                cmbContractorDateofBirthEra.SelectedItem = contractorDateofBirthera
                            End If
                            Dim contractorDateofBirthMonth As String = DateOfBirthSlicer(reader("contractorDateofBirth").ToString()).GetValue(1)
                            If cmbContractorDateofBirthMonth.Items.Contains(contractorDateofBirthMonth) Then
                                cmbContractorDateofBirthMonth.SelectedItem = contractorDateofBirthMonth
                            End If
                            Dim contractorDateofBirthDay As String = DateOfBirthSlicer(reader("contractorDateofBirth").ToString()).GetValue(2)
                            If cmbContractorDateofBirthDay.Items.Contains(contractorDateofBirthDay) Then
                                cmbContractorDateofBirthDay.SelectedItem = contractorDateofBirthDay
                            End If
                            Dim recipientDateofBirthera As String = reader("recipientDateofBirth").ToString().Substring(0, 2)
                            If cmbRecipientDateofBirthEra.Items.Contains(recipientDateofBirthera) Then
                                cmbRecipientDateofBirthEra.SelectedItem = recipientDateofBirthera
                            End If
                            Dim recipientDateofBirthMonth As String = DateOfBirthSlicer(reader("recipientDateofBirth").ToString()).GetValue(1)
                            If cmbRecipientDateofBirthMonth.Items.Contains(recipientDateofBirthMonth) Then
                                cmbRecipientDateofBirthMonth.SelectedItem = recipientDateofBirthMonth
                            End If
                            Dim recipientDateofBirthDay As String = DateOfBirthSlicer(reader("recipientDateofBirth").ToString()).GetValue(2)
                            If cmbRecipientDateofBirthDay.Items.Contains(recipientDateofBirthDay) Then
                                cmbRecipientDateofBirthDay.SelectedItem = recipientDateofBirthDay
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
        isFormLoaded = True
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs)
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
        Dim age As String = txtAge.Text.Trim()
        Dim gender As String = If(cmbGender.SelectedItem IsNot Nothing, cmbGender.SelectedItem.ToString(), "")

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
        Dim contractorDateofBirthEra As String = If(cmbContractorDateofBirthEra.SelectedItem IsNot Nothing, cmbContractorDateofBirthEra.SelectedItem.ToString(), "")
        Dim contractorDateofBirthYear As String = txtContractorDateofBirthYear.Text
        Dim contractorDateofBirthMonth As String = If(cmbContractorDateofBirthMonth.SelectedItem IsNot Nothing, cmbContractorDateofBirthMonth.SelectedItem.ToString(), "")
        Dim contractorDateofBirthDay As String = If(cmbContractorDateofBirthDay.SelectedItem IsNot Nothing, cmbContractorDateofBirthDay.SelectedItem.ToString(), "")

        If String.IsNullOrEmpty(contractorNameKanji) Then
            MessageBox.Show("계약자 명을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            If KanjiValidation(contractorNameKanji) = False Then
                MessageBox.Show("계약자 명을 한자로 입력해주세요. 상용한자에 없는 경우에는 카타카나로 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        If String.IsNullOrEmpty(contractorNameKana) Then
            MessageBox.Show("계약자 명을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            If KanaValidation(contractorNameKana) = False Then
                MessageBox.Show("계약자 명을 카타카나로 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        If String.IsNullOrEmpty(contractorAddressKanji) Then
            MessageBox.Show("계약자 주소를 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            If AddressValidation(contractorAddressKanji) = False Then
                MessageBox.Show("계약자 주소를 한자로 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        If String.IsNullOrEmpty(contractorAddressKana) Then
            MessageBox.Show("계약자 주소를 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            If AddressValidation(contractorAddressKana) = False Then
                MessageBox.Show("계약자 주소를 카타카나로 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

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
        If Not isFormLoaded Then Return
        input = txtContractorNameKana.Text.Trim()
        Dim Pattern As String = "^[\u30A0-\u30FF\s]*$"
        If Not Regex.IsMatch(input, Pattern) Then
            MessageBox.Show("계약자 명을 카타카나로만 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtContractorNameKana.Focus()
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
    Private Sub txtContractorDateofBirthYearTextChanged(sender As Object, e As EventArgs) Handles txtContractorDateofBirthYear.TextChanged
        input = txtContractorDateofBirthYear.Text.Trim()
        Dim Pattern As String = "^\d{0,4}$"
        If Not Regex.IsMatch(input, Pattern) Then
            MessageBox.Show("한자리에서 네자릿수의 숫자만 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtContractorDateofBirthYear.Focus()
        End If
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
        If Not isFormLoaded Then Return
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