Imports System.Reflection
Imports System.Globalization

Module ServiceLogger

#Region "Logging Functions"
    Private _FileName As String = ""
    Private ReadOnly Property EnableLogging() As Boolean
        Get
            Return True
        End Get
    End Property
    Private ReadOnly Property LogFileName() As String
        Get
            If _FileName = "" Then
                'Get the log file name
                Dim fInfo As New IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)
                If fInfo.Directory.FullName.EndsWith("\") Then
                    _FileName = fInfo.Directory.FullName & "InvoiceDownloadLog.txt"
                Else
                    _FileName = fInfo.Directory.FullName & "\InvoiceDownloadLog.txt"
                End If
            End If
            Return _FileName
        End Get
    End Property
    Public Sub DeleteLog()
        If System.IO.File.Exists(LogFileName) Then
            Try
                System.IO.File.Delete(LogFileName)
            Catch ex As Exception
            End Try
        End If
    End Sub
    Public Sub AddToLog(ByVal entryType As LOG_TYPE, ByVal logText As String, ByVal ex As System.Exception)
        Trace.WriteLine($"{logText} [{entryType.ToString}]")
        If ex IsNot Nothing Then
            Trace.WriteLine($"{ex.MessageWithInnerExceptionDetails}")
        End If
        If EnableLogging = False Then Return

        Dim fileName, funcName As String
        Dim lineNo As Integer = -1
        fileName = ""
        funcName = ""
        'If entryType <> LOG_TYPE.INFORMATION Then
        'Default EntryType will be information
        Try
            Dim sTrace As StackTrace = New StackTrace(True)
            Dim sFrame As StackFrame = sTrace.GetFrame(1)   'Get reference of previous function
            Dim mBase As MethodInfo = sFrame.GetMethod

            funcName = mBase.Name
            lineNo = sFrame.GetFileLineNumber
            fileName = sFrame.GetFileName
        Catch ex2 As Exception
        End Try

        If funcName Is Nothing Then funcName = ""
        If fileName Is Nothing Then fileName = ""
        If Not ex Is Nothing Then
            logText += vbCrLf & " {" & ex.Message & "}"
            If Not ex.InnerException Is Nothing Then
                logText += vbCrLf & " {" & ex.InnerException.Message & "}" & vbCrLf
            End If
            logText += vbCrLf & ex.StackTrace & vbCrLf
        End If

        WriteLogToFile(logText & " [" & funcName & "][" & entryType.ToString & "]")
    End Sub
    Public Sub AddToLog(ByVal entryType As LOG_TYPE, ByVal logText As String)
        AddToLog(entryType, logText, Nothing)
    End Sub
    Public Sub AddToLog(ByVal logText As String)
        AddToLog(LOG_TYPE.INFORMATION, logText, Nothing)
    End Sub
    Private Sub WriteLogToFile(ByVal logText As String)
        Dim mWriter As New IO.StreamWriter(LogFileName, True)
        mWriter.WriteLine(DateTime.Now & " : " & logText)
        mWriter.Close()
    End Sub
#End Region
End Module

Public Enum LOG_TYPE
    [ERROR]
    INFORMATION
End Enum