'Public Class Utilities
'    Public Shared Function ValidateEMail(ByVal email As String) As Boolean
'        If email.Trim = "" Then Return False
'        Dim mExp As New System.Text.RegularExpressions.Regex("\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
'        Dim result As System.Text.RegularExpressions.Match = mExp.Match(email)
'        If result.Success Then Return True
'        Return False
'    End Function
'    Public Shared Function ValidateCardNumber(ByVal cardNo As String) As Boolean
'        If cardNo.Length < 14 Then Return False
'        Return True
'    End Function

'    Public Shared Function GeneratePassword(ByVal length As Integer) As String
'        Dim result As String = ""

'        For i As Integer = 0 To length - 1
'            Randomize()
'            If i = 0 Then
'                result += GetRandomPrintableLetter()
'            Else
'                result += GetRandomPrintableCharacter()
'            End If
'        Next

'        Return result
'    End Function
'    Private Shared Function GetRandomPrintableLetter() As String
'        Const passwordCharacters As String = "abcdefghijkmnopqrstuvwxyz"
'        Return Mid(passwordCharacters, Int(Len(passwordCharacters) * Rnd()) + 1, 1)
'    End Function
'    Private Shared Function GetRandomPrintableCharacter() As String
'        Const passwordCharacters As String = "abcdefghijkmnopqrstuvwxyz23456789"
'        Return Mid(passwordCharacters, Int(Len(passwordCharacters) * Rnd()) + 1, 1)
'    End Function

'End Class
