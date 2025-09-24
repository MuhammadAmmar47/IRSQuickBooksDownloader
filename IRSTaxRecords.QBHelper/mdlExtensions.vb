Imports System.Runtime.CompilerServices
Imports System.Linq
Public Module mdlExtensions
    <Extension>
    Public Function ToSqlList(Of T)(list As IEnumerable(Of T)) As String
        Dim strings As String() = list _
            .Select(Function(entry) entry?.ToString()) _
            .ToArray()

        Return String.Join(", ", strings)
    End Function
    <Extension>
    Public Function EncodeWithCDATA(text As String) As String
        Return "<![CDATA[" & text & "]]>"
    End Function
    <Extension>
    Public Function LeftXCharacters(text As String, ByVal maxCharacters As Integer) As String
        Return Left(text.Trim, maxCharacters)
    End Function
End Module
