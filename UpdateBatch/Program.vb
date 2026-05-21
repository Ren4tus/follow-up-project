Imports System
Imports System.IO
Imports System.Data.SQLite
Imports System.Threading

Module Program
    Sub Main(args As String())
        Console.WriteLine("[UpdateBatch] 데이터 업데이트 배치를 시작합니다.")

        Dim movedCsvPath As String = "../moved_data/input.csv"
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

        Console.WriteLine($"[UpdateBatch] 폴더 스캔을 시작합니다. (주기: {scanIntervalMs / 1000}초)")

        While True
            If Not File.Exists(movedCsvPath) Then
                If diagnose Then
                    Console.WriteLine($"[Diagnose] 대상 데이터 파일이 없어 업데이트를 건너뜁니다: {movedCsvPath}")
                End If
                Thread.Sleep(scanIntervalMs)
                Continue While
            End If

            Console.WriteLine($"[Info] 파일을 발견하여 업데이트를 시작합니다: {movedCsvPath}")

        Try
            Using connection As New SQLiteConnection(connectionString)
                connection.Open()

                ' 테이블이 없으면 자동 생성 (로컬 테스트용)
                Dim createTableQuery As String = "CREATE TABLE IF NOT EXISTS UserTable (Id TEXT PRIMARY KEY, Name TEXT, Gender TEXT, Age TEXT, current_process TEXT, From TEXT);"
                Using createCmd As New SQLiteCommand(createTableQuery, connection)
                    createCmd.ExecuteNonQuery()
                End Using

                ' CSV 파일 읽기
                Dim lines As String() = File.ReadAllLines(movedCsvPath)

                ' 첫 번째 줄이 헤더라고 가정하고 두 번째 줄부터 순회
                For i As Integer = 1 To lines.Length - 1
                    Dim line As String = lines(i)
                    If String.IsNullOrWhiteSpace(line) Then Continue For

                    Dim columns As String() = line.Split(","c)
                If columns.Length >= 4 Then
                    Dim name As String = columns(0).Trim()
                    Dim gender As String = columns(1).Trim()
                    Dim age As String = columns(2).Trim()
                    Dim id As String = columns(3).Trim()

                        ' current_process가 'wait'인 경우에만 유저 정보를 업데이트하고 'complete'로 갱신
                    Dim updateQuery As String = "UPDATE UserTable SET Name = @Name, Gender = @Gender, Age = @Age, current_process = 'complete' WHERE Id = @Id AND current_process = 'wait'"

                        Using command As New SQLiteCommand(updateQuery, connection)
                            command.Parameters.AddWithValue("@Id", id)
                            command.Parameters.AddWithValue("@Name", name)
                            command.Parameters.AddWithValue("@Gender", gender)
                            command.Parameters.AddWithValue("@Age", age)

                            Dim rowsAffected As Integer = command.ExecuteNonQuery()

                            If rowsAffected > 0 Then
                                Console.WriteLine($"[Success] ID: {id} - 정보 업데이트 및 complete 상태로 갱신되었습니다.")
                            Else
                                Console.WriteLine($"[Skipped] ID: {id} - 조건에 맞지 않아(예: 이미 complete 상태) 건너뛰었습니다.")
                            End If
                        End Using
                    End If
                Next
            End Using

            ' 반복 처리 방지를 위해 처리가 완료된 파일은 삭제
            File.Delete(movedCsvPath)
            Console.WriteLine($"[Info] 데이터 업데이트가 완료되어 파일이 삭제되었습니다: {movedCsvPath}")

        Catch ex As Exception
            Console.WriteLine($"[Error] 배치 실행 중 오류가 발생했습니다: {ex.Message}")
        End Try

            Console.WriteLine("[UpdateBatch] 다음 스캔을 대기합니다...")
            Thread.Sleep(scanIntervalMs)
        End While
    End Sub
End Module
