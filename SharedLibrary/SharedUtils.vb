Imports System
Imports System.IO
Imports System.Data.SQLite
Imports System.Collections.Generic

Public Module SharedUtils
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
            {"contractor_name_kanji", "Text"},
            {"contractor_name_kana", "Text"},
            {"contractor_address_kanji", "Text"},
            {"contractor_address_kana", "Text"},
            {"contractor_dateofbirth", "Text"},
            {"recipient_name_kanji", "Text"},
            {"recipient_name_kana", "Text"},
            {"recipient_address_kanji", "Text"},
            {"recipient_address_kana", "Text"},
            {"recipient_dateofbirth", "TEXT"},
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
End Module
