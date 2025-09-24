Imports System.Security.Cryptography
Imports System.IO
Public Class Encryption

    Public Shared ReadOnly Property IV() As Byte()
        Get
            Dim mIV As String = "SDLLC"
            Dim thisSize As Integer = 15
            Dim thisIV(thisSize) As Byte

            Dim temp As Integer
            Dim lastBound As Integer = mIV.Length
            If lastBound > thisSize Then lastBound = thisSize
            For temp = 0 To lastBound - 1
                thisIV(temp) = Convert.ToByte(mIV.Chars(temp))
            Next
            Return thisIV
        End Get
    End Property
    Public Shared ReadOnly Property KEY() As Byte()
        Get
            Dim mKey As String = "DevelopersINN"
            Dim thisSize As Integer = 15
            Dim thisKey(thisSize) As Byte
            Dim temp As Integer
            Dim lastBound As Integer = mKey.Length
            For temp = 0 To lastBound - 1
                thisKey(temp) = Convert.ToByte(mKey.Chars(temp))
            Next
            Return thisKey
        End Get
    End Property
    Public Shared Function Encrypt(ByVal text As String) As String
        Dim myRijndael As New RijndaelManaged
        'myRijndael.GenerateKey()
        'myRijndael.GenerateIV()
        myRijndael.BlockSize = 128
        myRijndael.KeySize = 128
        Dim encryptor As ICryptoTransform = myRijndael.CreateEncryptor(KEY, IV)

        Dim msEncrypt As New MemoryStream
        Dim csEncrypt As New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)

        Dim toEncrypt() As Byte = New System.Text.ASCIIEncoding().GetBytes(text)
        csEncrypt.Write(toEncrypt, 0, toEncrypt.Length)
        csEncrypt.FlushFinalBlock()

        'Get encrypted array of bytes.
        Dim encrypted() As Byte = msEncrypt.ToArray()
        Return System.Convert.ToBase64String(encrypted)

    End Function

    Public Shared Function Decrypt(ByVal text As String) As String
        Dim myRijndael As New RijndaelManaged
        Dim decryptor As ICryptoTransform = myRijndael.CreateDecryptor(KEY, IV)

        Dim msDecrypt As New MemoryStream(System.Convert.FromBase64String(text))
        Dim csDecrypt As New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)


        Dim fromEncrypt() As Byte = New Byte(text.Length) {}

        'Read the data out of the crypto stream.
        csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length)

        'Convert the byte array back into a string.

        Dim roundtrip As String = New System.Text.ASCIIEncoding().GetString(fromEncrypt)

        'Return Mid(roundtrip, 0, InStr(roundtrip, Chr(0)))

        'Return New System.Text.UTF8Encoding().GetString(fromEncrypt)
        Dim temp As Integer
        Dim outStr As String = ""
        For temp = 0 To roundtrip.Length - 1
            Dim thisChar As String = Mid(roundtrip, temp + 1, 1)
            If thisChar = Chr(0) Then
                Return outStr
            Else
                outStr &= thisChar
            End If
        Next
        Return outStr
    End Function

End Class
