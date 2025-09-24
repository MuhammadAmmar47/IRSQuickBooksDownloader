Imports System.Reflection

Namespace Exceptions
    Public Class ErrorLogging
        Public Shared Sub AddToLog(ByVal msgType As ERROR_TYPE, ByVal msg As String, Optional ByVal ex As Exception = Nothing)
            Trace.WriteLine(Now.ToString & " [" & msgType.ToString & "] " & msg)
            If ex IsNot Nothing Then
                Trace.WriteLine(ex.Message & vbCrLf & ex.StackTrace)
            End If

            Return
            'Dim fileName As String = ""
            'Dim funcName As String = ""
            'Dim lineNo As Integer = -1

            'Dim sTrace As StackTrace = New StackTrace(True)
            'Dim sFrame As StackFrame = sTrace.GetFrame(1)   'Get reference of previous function
            'Dim mBase As MethodInfo = sFrame.GetMethod

            'funcName = mBase.Name
            'lineNo = sFrame.GetFileLineNumber
            'fileName = sFrame.GetFileName
            'If funcName Is Nothing Then funcName = ""
            'If fileName Is Nothing Then fileName = ""
            'If fileName <> "" Then
            '    'Extract file name only, instead of complete path
            '    Try
            '        fileName = New System.IO.FileInfo(fileName).Name
            '    Catch ex1 As Exception
            '    End Try
            'End If
            'Dim httpURL As String = ""
            'If Not System.Web.HttpContext.Current Is Nothing Then
            '    Try
            '        httpURL = System.Web.HttpContext.Current.Request.Url.AbsoluteUri
            '    Catch
            '        httpURL = ""
            '    End Try

            'End If
            'If Not ex Is Nothing Then
            '    msg += " [" & ex.Message & "]"
            'End If

            'If ex Is Nothing Then
            '    DataServices.AddToLog(msgType, msg, "", fileName, lineNo, funcName, httpURL)
            'Else
            '    DataServices.AddToLog(msgType, msg, ex.StackTrace, fileName, lineNo, funcName, httpURL)
            'End If
        End Sub
        Public Shared Function GetLog() As DataTable
            Dim strQ As String = "Select * From QB_ErrorLog Order By OccuredOn DESC"

            Dim req As New DataRequest
            req.Command = strQ
            req.CommandType = CommandType.Text

            Return New SQLServerDataHelper(AppSettings.ConnectionString).ExecuteQuery(req)
        End Function
        Public Shared Function ClearLog() As Boolean
            Dim strQ As String = "DELETE FROM QB_ErrorLog"

            Dim req As New DataRequest
            req.Command = strQ
            req.CommandType = CommandType.Text

            If New SQLServerDataHelper(AppSettings.ConnectionString).ExecuteNonQuery(req) > 0 Then
                Return True
            Else
                Return False
            End If

        End Function
    End Class

    Public Enum ERROR_TYPE
        INFORMATION = 0
        [ERROR] = 1
        EXCEPTION = 2
    End Enum
End Namespace