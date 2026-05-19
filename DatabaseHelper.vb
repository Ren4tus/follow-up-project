Imports System.Data
Imports System.Data.SQLite
Imports System.IO

Public Module DatabaseHelper
    ' DB 파일 경로 (실행 파일과 같은 폴더인 bin/Debug 등에 생성됨)
    Private ReadOnly DbPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "local_database.sqlite")
    Private ReadOnly ConnectionString As String = $"Data Source={DbPath};Version=3;"

    Private Function GetConnection() As SQLiteConnection
        Return New SQLiteConnection(ConnectionString)
    End Function

    Public Sub InitializeDatabase()
        Try
            ' DB 파일이 없으면 자동 생성
            If Not File.Exists(DbPath) Then
                SQLiteConnection.CreateFile(DbPath)
            End If

            Using conn As SQLiteConnection = GetConnection()
                conn.Open()
                
                ' USERS 테이블 생성 (IF NOT EXISTS를 사용하여 안전하게 생성)
                Dim cmdText As String = "CREATE TABLE IF NOT EXISTS USERS (USER_ID TEXT PRIMARY KEY, PASSWORD TEXT);"
                Using cmd As New SQLiteCommand(cmdText, conn)
                    cmd.ExecuteNonQuery()
                End Using

                ' USER_DATA 테이블 생성
                cmdText = "CREATE TABLE IF NOT EXISTS USER_DATA (USER_ID TEXT PRIMARY KEY, COUNTER_VALUE INTEGER DEFAULT 0);"
                Using cmd As New SQLiteCommand(cmdText, conn)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("DB 초기화 실패: " & ex.Message)
        End Try
    End Sub

    Public Function RegisterUser(id As String, password As String) As Boolean
        Try
            Using conn As SQLiteConnection = GetConnection()
                conn.Open()

                ' 아이디 중복 확인
                Dim checkCmd As New SQLiteCommand("SELECT COUNT(*) FROM USERS WHERE USER_ID = @id", conn)
                checkCmd.Parameters.AddWithValue("@id", id)
                Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                
                If count > 0 Then
                    MessageBox.Show("이미 존재하는 아이디입니다.")
                    Return False
                End If

                ' 사용자 추가
                Dim insertCmd As New SQLiteCommand("INSERT INTO USERS (USER_ID, PASSWORD) VALUES (@id, @pw)", conn)
                insertCmd.Parameters.AddWithValue("@id", id)
                insertCmd.Parameters.AddWithValue("@pw", password)
                insertCmd.ExecuteNonQuery()

                ' 초기 카운터 데이터 추가
                Dim insertDataCmd As New SQLiteCommand("INSERT INTO USER_DATA (USER_ID, COUNTER_VALUE) VALUES (@id, 0)", conn)
                insertDataCmd.Parameters.AddWithValue("@id", id)
                insertDataCmd.ExecuteNonQuery()

                Return True
            End Using
        Catch ex As Exception
            MessageBox.Show("회원가입 오류: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function LoginUser(id As String, password As String) As Boolean
        Try
            Using conn As SQLiteConnection = GetConnection()
                conn.Open()
                Dim cmd As New SQLiteCommand("SELECT COUNT(*) FROM USERS WHERE USER_ID = @id AND PASSWORD = @pw", conn)
                cmd.Parameters.AddWithValue("@id", id)
                cmd.Parameters.AddWithValue("@pw", password)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return count > 0
            End Using
        Catch ex As Exception
            MessageBox.Show("로그인 오류: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function GetCounterValue(id As String) As Integer
        Try
            Using conn As SQLiteConnection = GetConnection()
                conn.Open()
                Dim cmd As New SQLiteCommand("SELECT COUNTER_VALUE FROM USER_DATA WHERE USER_ID = @id", conn)
                cmd.Parameters.AddWithValue("@id", id)
                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing AndAlso Not DBNull.Value.Equals(result) Then
                    Return Convert.ToInt32(result)
                End If
                Return 0
            End Using
        Catch ex As Exception
            MessageBox.Show("데이터 조회 오류: " & ex.Message)
            Return 0
        End Try
    End Function

    Public Sub IncrementCounter(id As String)
        Try
            Using conn As SQLiteConnection = GetConnection()
                conn.Open()
                Dim cmd As New SQLiteCommand("UPDATE USER_DATA SET COUNTER_VALUE = COUNTER_VALUE + 1 WHERE USER_ID = @id", conn)
                cmd.Parameters.AddWithValue("@id", id)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show("데이터 업데이트 오류: " & ex.Message)
        End Try
    End Sub

End Module