Imports System
Imports System.IO
Imports System.Data.SQLite
Imports SharedLibrary
Imports System.Threading

Module Program
    Sub Main(args As String())
        Dim logFile As String = "ConsoleBatch.log"
        SharedUtils.LogMessage(logFile, "[ConsoleBatch] 데이터 인서트 배치를 시작합니다.")

        ' 처리 후 ../moved_data 폴더로 이동할 CSV 파일 경로
        Dim sourceCsvPath As String = "../input_data/input.csv"
        Dim targetFolder As String = "../moved_data"
        Dim targetCsvPath As String = Path.Combine(targetFolder, "input.csv")
        ' 로컬 테스트를 위한 SQLite DB 파일 경로
        Dim connectionString As String = "Data Source=../local.db;Version=3;"

        Dim scanIntervalMs As Integer = 5000 ' 스캔 주기 (기본 5초)
        Dim diagnose As Boolean = False
        Dim configPath As String = "config.ini"

        If File.Exists(configPath) Then
            Dim configLines As String() = File.ReadAllLines(configPath)
            For Each line As String In configLines
                If String.IsNullOrWhiteSpace(line) OrElse line.StartsWith(";") OrElse line.StartsWith("#") Then Continue For

                Dim parts As String() = line.Split(New Char() {"="c}, 2)
                If parts.Length = 2 Then
                    Dim key As String = parts(0).Trim().ToLower()
                    Dim val As String = parts(1).Trim().ToLower()

                    If key = "scan_interval_seconds" Then
                        Dim sec As Integer
                        If Integer.TryParse(val, sec) Then scanIntervalMs = sec * 1000
                    ElseIf key = "diagnose" Then
                        Boolean.TryParse(val, diagnose)
                    End If
                End If
            Next
        Else
            File.WriteAllText(configPath, $"scan_interval_seconds=5{Environment.NewLine}diagnose=false{Environment.NewLine}")
        End If

        SharedUtils.LogMessage(logFile, $"[ConsoleBatch] 폴더 스캔을 시작합니다. (주기: {scanIntervalMs / 1000}초)")

        While True
            If Not File.Exists(sourceCsvPath) Then
                If diagnose Then
                    SharedUtils.LogMessage(logFile, $"[Diagnose] 대상 데이터 파일이 없어 처리를 건너뜁니다: {sourceCsvPath}")
                End If
                Thread.Sleep(scanIntervalMs)
                Continue While
            End If

            SharedUtils.LogMessage(logFile, $"[Info] 파일을 발견하여 처리를 시작합니다: {sourceCsvPath}")

        Try
            Using connection As New SQLiteConnection(connectionString)
                connection.Open()

                ' 공통 테이블 생성 및 갱신 로직
                SharedUtils.EnsureTableSchema(connection)

                    ' 1. CSV 파일 읽기
                    Dim lines As String() = File.ReadAllLines(sourceCsvPath, Encoding.GetEncoding("Shift_JIS"))

                    ' 첫 번째 줄이 헤더(Header)라고 가정하고 두 번째 줄부터 순회
                    For i As Integer = 1 To lines.Length - 1
                    Dim line As String = lines(i)
                    If String.IsNullOrWhiteSpace(line) Then Continue For

                    Dim columns As String() = line.Split(","c)
                If columns.Length >= 4 Then
                            Dim contractorNameKanji As String = columns(0).Trim()
                            Dim contractorNameKana As String = columns(1).Trim()
                            Dim contractorAddressKanji As String = columns(2).Trim()
                            Dim contractorAddressKana As String = columns(3).Trim()
                            Dim contractorDateofBirth As String = columns(4).Trim()
                            Dim recipientNameKanji As String = columns(5).Trim()
                            Dim recipientNameKana As String = columns(6).Trim()
                            Dim recipientAddressKanji As String = columns(7).Trim()
                            Dim recipientAddressKana As String = columns(8).Trim()
                            Dim recipientDateofBirth As String = columns(9).Trim()
                            Dim id As String = columns(10).Trim()
                            Dim gender As String = columns(11).Trim()
                            Dim age As String = AgeCalculator(DateOfBirthValidation(contractorDateofBirth))

                            ' 2. ID 중복 체크
                            Dim checkQuery As String = "SELECT COUNT(1) FROM UserTable WHERE Id = @Id"
                        Using checkCommand As New SQLiteCommand(checkQuery, connection)
                            checkCommand.Parameters.AddWithValue("@Id", id)
                            Dim count As Integer = Convert.ToInt32(checkCommand.ExecuteScalar())

                            If count = 0 Then
                                    ' 3. 중복되지 않는 데이터만 'wait' 상태로 Insert
                                    Dim insertQuery As String = "INSERT INTO UserTable (Id, contractorNameKanji, contractorNameKana, contractorAddressKanji, contractorAddressKana, contractorDateofBirth, recipientNameKanji, recipientNameKana, recipientAddressKanji, recipientAddressKana, recipientDateofBirth, gender, age, current_process, InputSource) VALUES (@Id, @contractorNameKanji, @contractorNameKana, @contractorAddressKanji, @contractorAddressKana, @contractorDateofBirth, @recipientNameKanji, @recipientNameKana, @recipientAddressKanji, @recipientAddressKana, @recipientDateofBirth, @Gender, @Age, 'wait', 'Batch')"
                                    Using insertCommand As New SQLiteCommand(insertQuery, connection)
                                        insertCommand.Parameters.AddWithValue("@Id", id)
                                        insertCommand.Parameters.AddWithValue("@contractorNameKanji", contractorNameKanji)
                                        insertCommand.Parameters.AddWithValue("@contractorNameKana", contractorNameKana)
                                        insertCommand.Parameters.AddWithValue("@contractorAddressKanji", contractorAddressKanji)
                                        insertCommand.Parameters.AddWithValue("@contractorAddressKana", contractorAddressKana)
                                        insertCommand.Parameters.AddWithValue("@contractorDateofBirth", contractorDateofBirth)
                                        insertCommand.Parameters.AddWithValue("@recipientNameKanji", recipientNameKanji)
                                        insertCommand.Parameters.AddWithValue("@recipientNameKana", recipientNameKana)
                                        insertCommand.Parameters.AddWithValue("@recipientAddressKanji", recipientAddressKanji)
                                        insertCommand.Parameters.AddWithValue("@recipientAddressKana", recipientAddressKana)
                                        insertCommand.Parameters.AddWithValue("@recipientDateofBirth", recipientDateofBirth)
                                        insertCommand.Parameters.AddWithValue("@Gender", gender)
                                insertCommand.Parameters.AddWithValue("@Age", age)
                                    insertCommand.ExecuteNonQuery()
                                    SharedUtils.LogMessage(logFile, $"[Success] ID: {id} - 데이터가 wait 상태로 Insert 되었습니다.")
                                End Using
                            Else
                                SharedUtils.LogMessage(logFile, $"[Skipped] ID: {id} - 이미 존재하는 데이터이므로 Insert를 건너뛰었습니다.")
                            End If
                        End Using
                    End If
                Next
            End Using

            ' 4. 파일 이동 로직 (UpdateBatch가 처리할 수 있는 폴더로 이동)
            If Not Directory.Exists(targetFolder) Then Directory.CreateDirectory(targetFolder)
            If File.Exists(targetCsvPath) Then File.Delete(targetCsvPath) ' 이미 이동된 파일이 있으면 덮어쓰기 위해 삭제
            File.Move(sourceCsvPath, targetCsvPath)
            SharedUtils.LogMessage(logFile, $"[Info] 파일 처리가 끝나 대상 폴더로 이동되었습니다: {targetCsvPath}")
        Catch ex As Exception
            SharedUtils.LogMessage(logFile, $"[Error] 배치 실행 중 오류가 발생했습니다: {ex.Message}")
        End Try

            SharedUtils.LogMessage(logFile, "[ConsoleBatch] 다음 스캔을 대기합니다...")
            Thread.Sleep(scanIntervalMs)
        End While
    End Sub
End Module