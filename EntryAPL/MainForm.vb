Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data
Imports System.Data.SQLite
Imports System.IO
Imports SharedLibrary
Imports System.Collections.Generic

Public Class MainForm
    Inherits Form

    Private connectionString As String
    Private dataTable As DataTable
    Private dgvData As DataGridView

    Private txtSearchId As TextBox
    Private txtSearchContractorNameKanji As TextBox
    Private txtSearchContractorNameKana As TextBox
    Private txtSearchContractorAddressKanji As TextBox
    Private txtSearchContractorAddressKana As TextBox
    Private txtSearchContractorDateofBirth As TextBox
    Private txtSearchRecipientNameKanji As TextBox
    Private txtSearchRecipientNameKana As TextBox
    Private txtSearchRecipientAddressKanji As TextBox
    Private txtSearchRecipientAddressKana As TextBox
    Private txtSearchRecipientDateofBirth As TextBox
    Private txtSearchGender As TextBox
    Private txtSearchAge As TextBox
    Private txtSearchProcess As TextBox
    Private txtSearchSource As TextBox
    Private btnSearch As Button
    Private btnRefresh As Button

    Private txtIdInput As TextBox
    Private btnConfirm As Button

    Public Sub New()
        InitializeComponent()
        Me.Text = "EntryAPL - 데이터 목록"
        Me.Size = New Size(700, 500)
        Me.StartPosition = FormStartPosition.CenterScreen

        InitializeDBConnection()
        InitializeUI()
        LoadData()
    End Sub

    Private Sub InitializeDBConnection()
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
            Environment.Exit(1)
        End If

        connectionString = $"Data Source={dbPath};Version=3;"

        Try
            Using conn As New SQLiteConnection(connectionString)
                conn.Open()
                SharedUtils.EnsureTableSchema(conn)
            End Using
        Catch ex As Exception
            MessageBox.Show($"DB 스키마 초기화 오류: {ex.Message}", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub InitializeUI()
        ' Search panel
        Dim pnlTop = New Panel() With {.Dock = DockStyle.Top, .Height = 150}

        ' Row 1
        pnlTop.Controls.Add(New Label() With {.Text = "ID:", .Location = New Point(10, 15), .AutoSize = True})
        txtSearchId = New TextBox() With {.Location = New Point(30, 12), .Width = 155}
        pnlTop.Controls.Add(txtSearchId)

        pnlTop.Controls.Add(New Label() With {.Text = "계약자 명(한자):", .Location = New Point(185, 15), .AutoSize = True})
        txtSearchContractorNameKanji = New TextBox() With {.Location = New Point(270, 12), .Width = 75}
        pnlTop.Controls.Add(txtSearchContractorNameKanji)

        pnlTop.Controls.Add(New Label() With {.Text = "계약자 명(카나):", .Location = New Point(345, 15), .AutoSize = True})
        txtSearchContractorNameKana = New TextBox() With {.Location = New Point(420, 12), .Width = 65}
        pnlTop.Controls.Add(txtSearchContractorNameKana)

        ' Row 2
        pnlTop.Controls.Add(New Label() With {.Text = "계약자 주소(한자):", .Location = New Point(10, 42), .AutoSize = True})
        txtSearchContractorAddressKanji = New TextBox() With {.Location = New Point(95, 39), .Width = 90}
        pnlTop.Controls.Add(txtSearchContractorAddressKanji)

        pnlTop.Controls.Add(New Label() With {.Text = "계약자 주소(카나):", .Location = New Point(185, 42), .AutoSize = True})
        txtSearchContractorAddressKana = New TextBox() With {.Location = New Point(270, 39), .Width = 75}
        pnlTop.Controls.Add(txtSearchContractorAddressKana)

        pnlTop.Controls.Add(New Label() With {.Text = "계약자 생년월일:", .Location = New Point(345, 42), .AutoSize = True})
        txtSearchContractorDateofBirth = New TextBox() With {.Location = New Point(420, 39), .Width = 65}
        pnlTop.Controls.Add(txtSearchContractorDateofBirth)

        ' Row 3
        pnlTop.Controls.Add(New Label() With {.Text = "수취인 명(한자):", .Location = New Point(10, 69), .AutoSize = True})
        txtSearchRecipientNameKanji = New TextBox() With {.Location = New Point(95, 66), .Width = 90}
        pnlTop.Controls.Add(txtSearchRecipientNameKanji)

        pnlTop.Controls.Add(New Label() With {.Text = "수취인 명(카나):", .Location = New Point(185, 69), .AutoSize = True})
        txtSearchRecipientNameKana = New TextBox() With {.Location = New Point(270, 66), .Width = 75}
        pnlTop.Controls.Add(txtSearchRecipientNameKana)

        pnlTop.Controls.Add(New Label() With {.Text = "수취인 주소(한자):", .Location = New Point(345, 69), .AutoSize = True})
        txtSearchRecipientAddressKanji = New TextBox() With {.Location = New Point(430, 66), .Width = 55}
        pnlTop.Controls.Add(txtSearchRecipientAddressKanji)

        ' Row 4
        pnlTop.Controls.Add(New Label() With {.Text = "수취인 주소(카나):", .Location = New Point(10, 96), .AutoSize = True})
        txtSearchRecipientAddressKana = New TextBox() With {.Location = New Point(95, 93), .Width = 90}
        pnlTop.Controls.Add(txtSearchRecipientAddressKana)

        pnlTop.Controls.Add(New Label() With {.Text = "수취인 생년월일:", .Location = New Point(185, 96), .AutoSize = True})
        txtSearchRecipientDateofBirth = New TextBox() With {.Location = New Point(270, 93), .Width = 75}
        pnlTop.Controls.Add(txtSearchRecipientDateofBirth)

        pnlTop.Controls.Add(New Label() With {.Text = "성별:", .Location = New Point(345, 96), .AutoSize = True})
        txtSearchGender = New TextBox() With {.Location = New Point(375, 93), .Width = 110}
        pnlTop.Controls.Add(txtSearchGender)

        ' Row 5
        pnlTop.Controls.Add(New Label() With {.Text = "나이:", .Location = New Point(10, 124), .AutoSize = True})
        txtSearchAge = New TextBox() With {.Location = New Point(35, 121), .Width = 150}
        pnlTop.Controls.Add(txtSearchAge)

        pnlTop.Controls.Add(New Label() With {.Text = "프로세스:", .Location = New Point(185, 124), .AutoSize = True})
        txtSearchProcess = New TextBox() With {.Location = New Point(230, 121), .Width = 115}
        pnlTop.Controls.Add(txtSearchProcess)

        pnlTop.Controls.Add(New Label() With {.Text = "소스:", .Location = New Point(345, 124), .AutoSize = True})
        txtSearchSource = New TextBox() With {.Location = New Point(375, 121), .Width = 110}
        pnlTop.Controls.Add(txtSearchSource)



        btnSearch = New Button() With {.Text = "검색", .Location = New Point(495, 10), .Width = 60, .Height = 50}
        AddHandler btnSearch.Click, AddressOf btnSearch_Click
        pnlTop.Controls.Add(btnSearch)

        btnRefresh = New Button() With {.Text = "초기화", .Location = New Point(565, 10), .Width = 60, .Height = 50}
        AddHandler btnRefresh.Click, AddressOf btnRefresh_Click
        pnlTop.Controls.Add(btnRefresh)


        Me.Controls.Add(pnlTop)

        ' Grid
        dgvData = New DataGridView() With {
            .Dock = DockStyle.Bottom,
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .ReadOnly = True,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            .MultiSelect = False,
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        }
        AddHandler dgvData.CellDoubleClick, AddressOf dgvData_CellDoubleClick
        Me.Controls.Add(dgvData)

        ' Bottom panel for direct ID input
        Dim pnlBottom = New Panel() With {.Dock = DockStyle.Bottom, .Height = 50}

        pnlBottom.Controls.Add(New Label() With {.Text = "선택된 ID 또는 직접 입력:", .Location = New Point(10, 15), .AutoSize = True})
        txtIdInput = New TextBox() With {.Location = New Point(160, 12), .Width = 150}
        pnlBottom.Controls.Add(txtIdInput)

        btnConfirm = New Button() With {.Text = "확인 (상세보기)", .Location = New Point(320, 10), .Width = 120, .Height = 25}
        AddHandler btnConfirm.Click, AddressOf btnConfirm_Click
        pnlBottom.Controls.Add(btnConfirm)

        ' Update txtIdInput when grid selection changes
        AddHandler dgvData.SelectionChanged, AddressOf dgvData_SelectionChanged

        Me.Controls.Add(pnlBottom)
    End Sub

    Private Sub LoadData()
        Try
            Using conn As New SQLiteConnection(connectionString)
                conn.Open()
                Dim query As String = "SELECT * FROM UserTable"
                Using cmd As New SQLiteCommand(query, conn)
                    Using adapter As New SQLiteDataAdapter(cmd)
                        dataTable = New DataTable()
                        adapter.Fill(dataTable)
                        dgvData.DataSource = dataTable
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"데이터 로드 오류: {ex.Message}", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs)
        If dataTable Is Nothing Then Return

        Dim filters As New List(Of String)()

        If Not String.IsNullOrWhiteSpace(txtSearchId.Text) Then filters.Add($"Id LIKE '%{txtSearchId.Text.Trim().Replace("'", "''")}%'")
        If Not String.IsNullOrWhiteSpace(txtSearchContractorNameKanji.Text) Then filters.Add($"contractorNameKanji LIKE '%{txtSearchContractorNameKanji.Text.Trim().Replace("'", "''")}%'")
        If Not String.IsNullOrWhiteSpace(txtSearchContractorNameKana.Text) Then filters.Add($"contractorNameKana LIKE '%{txtSearchContractorNameKana.Text.Trim().Replace("'", "''")}%'")
        If Not String.IsNullOrWhiteSpace(txtSearchContractorAddressKanji.Text) Then filters.Add($"contractorAddressKanji LIKE '%{txtSearchContractorAddressKanji.Text.Trim().Replace("'", "''")}%'")
        If Not String.IsNullOrWhiteSpace(txtSearchContractorAddressKana.Text) Then filters.Add($"contractorAddressKana LIKE '%{txtSearchContractorAddressKana.Text.Trim().Replace("'", "''")}%'")
        If Not String.IsNullOrWhiteSpace(txtSearchContractorDateofBirth.Text) Then filters.Add($"contractorDateofBirth LIKE '%{txtSearchContractorDateofBirth.Text.Trim().Replace("'", "''")}%'")
        If Not String.IsNullOrWhiteSpace(txtSearchRecipientNameKanji.Text) Then filters.Add($"recipientNameKanji LIKE '%{txtSearchRecipientNameKanji.Text.Trim().Replace("'", "''")}%'")
        If Not String.IsNullOrWhiteSpace(txtSearchRecipientNameKana.Text) Then filters.Add($"recipientNameKana LIKE '%{txtSearchRecipientNameKana.Text.Trim().Replace("'", "''")}%'")
        If Not String.IsNullOrWhiteSpace(txtSearchRecipientAddressKanji.Text) Then filters.Add($"recipientAddressKanji LIKE '%{txtSearchRecipientAddressKanji.Text.Trim().Replace("'", "''")}%'")
        If Not String.IsNullOrWhiteSpace(txtSearchRecipientAddressKana.Text) Then filters.Add($"recipientAddressKana LIKE '%{txtSearchRecipientAddressKana.Text.Trim().Replace("'", "''")}%'")
        If Not String.IsNullOrWhiteSpace(txtSearchRecipientDateofBirth.Text) Then filters.Add($"recipientDateofBirth LIKE '%{txtSearchRecipientDateofBirth.Text.Trim().Replace("'", "''")}%'")

        If Not String.IsNullOrWhiteSpace(txtSearchGender.Text) Then filters.Add($"gender LIKE '%{txtSearchGender.Text.Trim().Replace("'", "''")}%'")

        If Not String.IsNullOrWhiteSpace(txtSearchAge.Text) Then
            Dim ageVal As Integer
            If Integer.TryParse(txtSearchAge.Text.Trim(), ageVal) Then
                filters.Add($"age = {ageVal}")
            Else
                filters.Add($"Convert(age, 'System.String') LIKE '%{txtSearchAge.Text.Trim().Replace("'", "''")}%'")
            End If
        End If

        If Not String.IsNullOrWhiteSpace(txtSearchProcess.Text) Then filters.Add($"current_process LIKE '%{txtSearchProcess.Text.Trim().Replace("'", "''")}%'")
        If Not String.IsNullOrWhiteSpace(txtSearchSource.Text) Then filters.Add($"InputSource LIKE '%{txtSearchSource.Text.Trim().Replace("'", "''")}%'")

        If filters.Count > 0 Then
            dataTable.DefaultView.RowFilter = String.Join(" AND ", filters)
        Else
            dataTable.DefaultView.RowFilter = ""
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs)
        txtSearchId.Clear()
        txtSearchContractorNameKanji.Clear()
        txtSearchContractorNameKana.Clear()
        txtSearchContractorAddressKanji.Clear()
        txtSearchContractorAddressKana.Clear()
        txtSearchContractorDateofBirth.Clear()
        txtSearchRecipientNameKanji.Clear()
        txtSearchRecipientNameKana.Clear()
        txtSearchRecipientAddressKanji.Clear()
        txtSearchRecipientAddressKana.Clear()
        txtSearchRecipientDateofBirth.Clear()
        txtSearchGender.Clear()
        txtSearchAge.Clear()
        txtSearchProcess.Clear()
        txtSearchSource.Clear()
        If dataTable IsNot Nothing Then
            dataTable.DefaultView.RowFilter = ""
        End If
        LoadData()
    End Sub

    Private Sub dgvData_SelectionChanged(sender As Object, e As EventArgs)
        If dgvData.CurrentRow IsNot Nothing AndAlso dgvData.CurrentRow.Index >= 0 Then
            Dim idValue = dgvData.CurrentRow.Cells("Id").Value
            If idValue IsNot Nothing AndAlso Not DBNull.Value.Equals(idValue) Then
                txtIdInput.Text = idValue.ToString()
            End If
        End If
    End Sub

    Private Sub dgvData_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            Dim idValue = dgvData.Rows(e.RowIndex).Cells("Id").Value
            If idValue IsNot Nothing AndAlso Not DBNull.Value.Equals(idValue) Then
                OpenDetailForm(idValue.ToString())
            End If
        End If
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs)
        Dim targetId As String = txtIdInput.Text.Trim()
        If String.IsNullOrEmpty(targetId) Then
            MessageBox.Show("ID를 입력하거나 목록에서 선택해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        OpenDetailForm(targetId)
    End Sub

    Private Sub OpenDetailForm(id As String)
        Dim detailForm As New DetailForm(connectionString, id)
        ' Handle window transition by capturing close event
        AddHandler detailForm.FormClosed, Sub(s, args)
                                              Me.Show()
                                              LoadData() ' Refresh data to reflect any changes
                                          End Sub
        Me.Hide()
        detailForm.Show()
    End Sub

End Class