Namespace Utilities
    Public Class Validations
        Public Const invalidChar As String = "~!@#$%^&*()+|?/\'""=-`:<>;"
        'Public Const ValidCharacters As String = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-_.#"
        Public Const ValidCharacters As String = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-_."

        Public Shared Function ValidateURL(ByVal url As String) As Boolean
            If url.Trim = "" Then Return False
            If url.ToLower.StartsWith("http://") = False AndAlso url.ToLower.StartsWith("https://") = False Then
                url = "http://" & url
            End If
            Dim mExp As New System.Text.RegularExpressions.Regex("http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?")
            Dim result As System.Text.RegularExpressions.Match = mExp.Match(url)
            If result.Success Then Return True
            Return False
        End Function

        Public Shared Function ValidateEMail(ByVal email As String) As Boolean
            If email.Trim = "" Then Return False
            Dim mExp As New System.Text.RegularExpressions.Regex("\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
            Dim result As System.Text.RegularExpressions.Match = mExp.Match(email)
            If result.Success Then Return True
            Return False
        End Function
        Public Shared Function ValidateUSAZipCode(ByVal zipCode As String) As Boolean
            If zipCode.Trim = "" Then Return False
            Dim mExp As New System.Text.RegularExpressions.Regex("^\d{5}(-\d{4})?$")
            Dim result As System.Text.RegularExpressions.Match = mExp.Match(zipCode)
            If result.Success Then Return True
            Return False
        End Function
        Public Shared Function ValidateCanadaZipCode(ByVal zipCode As String) As Boolean
            If zipCode.Trim = "" Then Return False
            Dim mExp As New System.Text.RegularExpressions.Regex("^[ABCEGHJKLMNPRSTVXY]{1}\d{1}[A-Z]{1} *\d{1}[A-Z]{1}\d{1}$")
            Dim result As System.Text.RegularExpressions.Match = mExp.Match(zipCode)
            If result.Success Then Return True
            Return False
        End Function

        Public Shared Function ValidateCardNumber(ByVal cardNo As String) As Boolean
            If cardNo.Length < 14 Then Return False
            Return True
        End Function
        Public Shared Function ValidateSettingName(ByVal name As String) As Boolean
            For temp As Integer = 0 To invalidChar.Length - 1
                If name.IndexOf(invalidChar.Chars(temp).ToString) >= 0 Then
                    Return False
                End If
            Next
            Return True
        End Function
        Public Shared Function ValidateColumnName(ByVal name As String) As Boolean
            If name.IndexOf(" ") >= 0 Then Return False
            For temp As Integer = 0 To invalidChar.Length - 1
                If name.IndexOf(invalidChar.Chars(temp).ToString) >= 0 Then
                    Return False
                End If
            Next
            Return True
        End Function
        Public Shared Function MakeValidFileName(ByVal name As String) As String
            Dim invalidChars As String = System.Text.RegularExpressions.Regex.Escape(New String(System.IO.Path.GetInvalidFileNameChars()))
            Dim invalidRegStr As String = String.Format("([{0}]*\.+$)|([{0}]+)", invalidChars)
            Return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "_")
        End Function
        Public Shared Function StripInvalidCharacters(ByVal name As String) As String
            Dim outName As String = name
            For temp As Integer = invalidChar.Length - 1 To 0 Step -1
                If name.IndexOf(invalidChar.Chars(temp).ToString) >= 0 Then
                    outName = outName.Replace(invalidChar.Chars(temp).ToString, "")
                End If
            Next
            Return outName
        End Function
        Public Shared Function StripSpecialCharacters(ByVal name As String, Optional ByVal replaceWith As String = "?") As String
            Dim outName As String = ""

            For temp As Integer = 0 To name.Length - 1
                'Check if this is the valid character
                If ValidCharacters.IndexOf(name.Chars(temp).ToString) >= 0 Then
                    'It found in the original characters list
                    outName += name.Chars(temp).ToString
                Else
                    'Its NOT Found in the allowed characters list
                    outName += replaceWith
                End If
            Next

            'For temp As Integer = ValidCharacters.Length - 1 To 0 Step -1
            '    If name.IndexOf(ValidCharacters.Chars(temp).ToString) >= 0 Then
            '    Else
            '        outName = outName.Replace(ValidCharacters.Chars(temp).ToString, "?")
            '    End If
            'Next
            Return outName
        End Function

        Public Shared Function ContentContainEmail(ByVal content As String) As String
            Dim regEx As New System.Text.RegularExpressions.Regex("\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b", Text.RegularExpressions.RegexOptions.Singleline Or Text.RegularExpressions.RegexOptions.IgnoreCase)
            Dim m As System.Text.RegularExpressions.Match = regEx.Match(content.ToLower)
            If m.Success Then
                Return True
            End If
            Return False
        End Function

        Public Shared Function ValidateSearsSKU(ByVal sku As String) As Boolean
            If sku.Trim = "" Then Return False
            If sku.Length > 50 Then Return False
            Dim allowedString As String = "abcdefghijklmnopqrstuvwxyz0123456789-_"

            For temp As Integer = 0 To sku.Length - 1
                Dim thisChar As String = sku.Chars(temp).ToString.ToLower
                If allowedString.ToLower.IndexOf(thisChar) < 0 Then
                    Return False
                End If
            Next
            Return True
            'Dim result2 As Boolean = System.Text.RegularExpressions.Regex.IsMatch(sku, "[\w\s-]{1,20}")
            'Trace.WriteLine(result2)
            ''Dim expression As String = "^[a-zA-Z0-9_]+$"
            'Dim expression As String = "[\w\s-]{1,20}"
            'Dim mExp As New System.Text.RegularExpressions.Regex(expression)
            'Dim result As System.Text.RegularExpressions.Match = mExp.Match(sku)
            'If result.Success Then Return True
            'Return False
        End Function
    End Class

End Namespace