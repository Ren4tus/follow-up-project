Imports System
Imports System.IO
Imports System.Data.SQLite
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Public Module SharedUtils
    Dim pattern As String
    ' 1. 로그를 콘솔과 지정한 로그 파일에 동시에 남기는 공통 함수
    Public Sub LogMessage(logFilePath As String, message As String)
        Dim logLine As String = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}"

        ' 콘솔 출력
        Console.WriteLine(logLine)

        ' 파일 출력
        Try
            File.AppendAllText(logFilePath, logLine & Environment.NewLine)
        Catch ex As Exception
            Console.WriteLine($"[Log Error] 로그 파일 기록 실패: {ex.Message}")
        End Try
    End Sub

    ' 2. 테이블 생성 및 컬럼 갱신(추가)을 담당하는 공통 함수
    Public Sub EnsureTableSchema(connection As SQLiteConnection)
        ' 기본 테이블 생성 (Id 컬럼을 Primary Key로 먼저 생성)
        Dim createTableQuery As String = "CREATE TABLE IF NOT EXISTS UserTable (Id TEXT PRIMARY KEY);"
        Using createCmd As New SQLiteCommand(createTableQuery, connection)
            createCmd.ExecuteNonQuery()
        End Using

        ' 기존 테이블의 컬럼 목록 조회
        Dim existingColumns As New List(Of String)()
        Using infoCmd As New SQLiteCommand("PRAGMA table_info(UserTable);", connection)
            Using reader = infoCmd.ExecuteReader()
                While reader.Read()
                    existingColumns.Add(reader("name").ToString().ToLower())
                End While
            End Using
        End Using

        ' 필요한 전체 컬럼 정의. 여기에 컬럼을 추가하면 배치 실행 시 자동으로 반영
        Dim requiredColumns As New Dictionary(Of String, String) From {
            {"contractorNameKanji", "Text"},
            {"contractorNameKana", "Text"},
            {"contractorAddressKanji", "Text"},
            {"contractorAddressKana", "Text"},
            {"contractorDateofBirth", "Text"},
            {"recipientNameKanji", "Text"},
            {"recipientNameKana", "Text"},
            {"recipientAddressKanji", "Text"},
            {"recipientAddressKana", "Text"},
            {"recipientDateofBirth", "TEXT"},
            {"gender", "TEXT"},
            {"age", "TEXT"},
            {"current_process", "TEXT"},
            {"InputSource", "TEXT"}
        }

        ' 누락된 컬럼이 있으면 ALTER TABLE 로 추가
        For Each kvp In requiredColumns
            If Not existingColumns.Contains(kvp.Key.ToLower()) Then
                ' 필드명에 SQLite 예약어(예: From)가 있을 수 있으므로 따옴표("") 처리
                Dim alterQuery As String = $"ALTER TABLE UserTable ADD COLUMN ""{kvp.Key}"" {kvp.Value};"
                Using alterCmd As New SQLiteCommand(alterQuery, connection)
                    alterCmd.ExecuteNonQuery()
                End Using
            End If
        Next
    End Sub
    Public Function KanjiValidation(input As String) As Boolean
        pattern = "^[\u30A0-\u30FF\u4E00-\u9FFF\s]+$"
        Return Regex.IsMatch(input, pattern)
    End Function
    Public Function KanaValidation(input As String) As Boolean
        pattern = "^[\u30A0-\u30FF\s]+$"
        Return Regex.IsMatch(input, pattern)
    End Function
    Public Function AddressValidation(input As String) As Boolean
        pattern = "^" &
            "[0-9０-９A-Za-zＡ-Ｚａ-ｚ\u3040-\u309F\u30A0-\u30FF\u31F0-\u31FF\u4E00-\u9FFF\uFF01-\uFF5E]+" &
            "(?:[-\u30FC\uFF0D\s]*[0-9０-９A-Za-zＡ-Ｚａ-ｚ\u3040-\u30FF\u4E00-\u9FFF]+)*" &
            "(?:丁目|番地|号|町|区|市|都|道|府)?" &
            ".*$"
        Return Regex.IsMatch(input, pattern)
    End Function
    Public Function DateOfBirthValidation(input As String) As DateTime
        Dim dobPattern As New Regex(
        "^(?:" &
        "(西暦)(\d{4})年([1-9]|1[0-2])月([1-9]|[12][0-9]|3[01])日|" &
        "(明治|大正|昭和|平成|令和)(\d{1,2})年(0?[1-9]|1[0-2])月([1-9]|[12][0-9]|3[01])日" &
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
            year = CInt(m.Groups(2).Value)
            month = CInt(m.Groups(3).Value)
            day = CInt(m.Groups(4).Value)

        ElseIf m.Groups(5).Success Then
            Dim eraName = m.Groups(5).Value
            Dim eraYear = CInt(m.Groups(6).Value)
            month = CInt(m.Groups(7).Value)
            day = CInt(m.Groups(8).Value)

            Dim era = eraRanges(eraName)
            year = era.Item1 + eraYear - 1

            Dim dobEra As Date
            Try
                dobEra = New Date(year, month, day)
            Catch ex As ArgumentOutOfRangeException
                Return DateTime.MinValue
            End Try

            Dim startEra As New Date(era.Item1, era.Item2, era.Item3)
            Dim endEra As New Date(era.Item4, era.Item5, era.Item6)
            If dobEra < startEra OrElse dobEra > endEra Then
                Return DateTime.MinValue
            End If
        End If

        Try
            Dim dob As New DateTime(year, month, day)
            Return dob
        Catch ex As ArgumentOutOfRangeException
            Return DateTime.MinValue
        End Try
    End Function
    Public Function AgeCalculator(DateOfBirth As DateTime) As Integer
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
End Module
End Module
