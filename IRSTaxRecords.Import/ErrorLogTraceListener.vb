Imports System.IO

Public Class ErrorLogTraceListener
    Inherits TextWriterTraceListener


    Public Property FilePath() As String = ""

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal stream As Stream)
        MyBase.New(stream)
    End Sub

    Public Sub New(ByVal path As String)
        MyBase.New(path)
        FilePath = path
    End Sub

    Public Sub New(ByVal writer As TextWriter)
        MyBase.New(writer)
    End Sub

    Public Sub New(ByVal stream As Stream, ByVal name As String)
        MyBase.New(stream, name)
    End Sub

    Public Sub New(ByVal path As String, ByVal name As String)
        MyBase.New(path, name)
    End Sub

    Public Sub New(ByVal writer As TextWriter, ByVal name As String)
        MyBase.New(writer, name)
    End Sub

    Private Function FormatDateForLog(ByVal dt As DateTime) As String
        Return $"{dt.ToString("yyyy/MM/dd HH:mm:ss.fff")}"
    End Function
    Public Overloads Overrides Sub WriteLine(ByVal message As String)
        SyncLock Me
            Try
                If FilePath = "" Then Return
                Dim mWriter As New IO.StreamWriter(FilePath, True)
                mWriter.WriteLine(FormatDateForLog(Now) & " : " & message)
                mWriter.Close()
            Catch ex As Exception
                'Failed to write into file....
            End Try
            TrimLogFile()
        End SyncLock
    End Sub
    Public Overrides Sub Write(ByVal message As String)
        If FilePath.Trim = "" Then Return
        SyncLock Me
            Try
                Dim mWriter As New IO.StreamWriter(FilePath, True)
                mWriter.Write(FormatDateForLog(Now) & " : " & message)
                mWriter.Close()
            Catch ex As Exception
            End Try
        End SyncLock
    End Sub

    Public Overrides Sub WriteLine(ByVal message As String, ByVal category As String)
        If category Is Nothing Then
            WriteLine(message)
        Else
            WriteLine("[" & category.ToString & "] " & message)
        End If
    End Sub

    Public Shared Function AddTraceListener(ByVal ListenerName As String, ByVal FilePath As String) As Boolean
        Try
            If FilePath.Trim = "" Then Throw New Exception("File path must not be empty")
            Dim _Listener As New ErrorLogTraceListener
            _Listener.FilePath = FilePath
            _Listener.Name = ListenerName

            Dim id As Integer = Trace.Listeners.Add(_Listener)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function RemoveTraceListener(ByVal ListenerName As String) As Boolean
        Try
            Trace.Listeners.Remove(ListenerName)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub TrimLogFile()
        If FilePath Is Nothing OrElse FilePath.Trim = "" Then Return
        'If Not System.IO.File.Exists(_filePath) Then Return 'Lets make sure file exists...

        Dim fInfo As New FileInfo(FilePath)
        If fInfo IsNot Nothing AndAlso fInfo.Length > MAX_LOG_FILE_SIZE Then
            Dim ext As String = Path.GetExtension(fInfo.FullName)
            Dim newFileName As String = fInfo.Name & "-" & Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & "-" & Now.Hour.ToString & "-" & Now.Minute.ToString & "-" & Now.Second.ToString
            newFileName += ext

            Dim newPath As String = fInfo.Directory.FullName
            If Not newPath.EndsWith("\") Then newPath += "\"
            newPath += newFileName
            Try
                fInfo.MoveTo(newPath)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private MAX_LOG_FILE_SIZE As Long = Integer.MaxValue
End Class