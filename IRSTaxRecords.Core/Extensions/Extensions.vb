Imports System.Runtime.CompilerServices
Imports System.Linq

Module Extensions
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
End Module
