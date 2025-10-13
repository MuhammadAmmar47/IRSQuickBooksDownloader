Imports System.Runtime.CompilerServices
Imports System.Linq
Imports System.Web

Public Module Extensions
    <Extension()>
    Public Function MessageWithInnerExceptionDetails(ByVal e As Exception) As String
        If e Is Nothing Then Return ""
        Dim ErrorDetails = $"{e.Message}{vbCrLf}{e.StackTrace}"
        While True
            e = e.InnerException
            If e Is Nothing Then Exit While
            ErrorDetails += $"{vbCrLf}InnerException: {e.Message}{vbCrLf}{e.StackTrace}"
        End While
        Return ErrorDetails
    End Function
    <Extension>
    Public Function ToSqlList(Of T)(list As IEnumerable(Of T)) As String
        Dim strings As String() = list _
            .Select(Function(entry) entry?.ToString()) _
            .ToArray()

        Return String.Join(", ", strings)
    End Function
    <Extension>
    Public Function IsNullOrEmpty(value As String) As Boolean
        If value Is Nothing Then Return True
        If value.Trim() = "" Then Return True
        Return False
    End Function
    <Extension>
    Public Function IsNotNullOrEmpty(value As String) As Boolean
        Return Not value.IsNullOrEmpty()
    End Function

    <Extension>
    Public Function StreamFileToUser(fileToStream As String, DisplayName As String) As Boolean
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.ContentType = "application/pdf"
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" & DisplayName)
        HttpContext.Current.Response.WriteFile(fileToStream)
        HttpContext.Current.Response.Flush()
        HttpContext.Current.Response.End()
    End Function

End Module
