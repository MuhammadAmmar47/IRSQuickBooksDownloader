Public Partial Class PDFUpload
    Inherits System.Web.UI.Page

#Region "Private properties"
    Private ReadOnly Property PDFSavePath() As String
        Get
            Dim path As String = System.Configuration.ConfigurationManager.AppSettings("PDFSavePath")
            If String.IsNullOrEmpty(path) Then
                msg.ShowError("PDF save path is not defined.")
                Return ""
            End If
            If Not path.EndsWith("\") Then path += "\"
            If Not System.IO.Directory.Exists(path) Then System.IO.Directory.CreateDirectory(path)
            Return path
        End Get
    End Property
    Public ReadOnly Property UserID() As String
        Get
            If Request.QueryString("UserID") Is Nothing Then Return ""
            Dim value As String = Request.QueryString("UserID")
            If IsNumeric(value) Then Return value

            'Return System.Text.Encoding.Default.GetString(System.Convert.FromBase64String(value))
            Return base64_decode(value)
        End Get
    End Property


    Private ReadOnly Property EmailUserName() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("EmailUserName")
        End Get
    End Property
    Private ReadOnly Property EmailPassword() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("EmailPassword")
        End Get
    End Property
    Private ReadOnly Property EmailServer() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("EmailServer")
        End Get
    End Property
    Private ReadOnly Property EmailFrom() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("EmailFrom")
        End Get
    End Property
    Private ReadOnly Property EmailTo() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("EmailTo")
        End Get
    End Property
    Private ReadOnly Property EmailSubject() As String
        Get
            Dim value As String = System.Configuration.ConfigurationManager.AppSettings("EmailSubject")
            If value = "" Then Return "new pdf file uploaded"
            Return value
        End Get
    End Property
#End Region

#Region "Private functions"
    Dim Base64Chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"

    Public Function base64_decode(ByVal strIn)
        Dim w1, w2, w3, w4, n, strOut
        strOut = ""
        For n = 1 To Len(strIn) Step 4
            w1 = mimedecode(Mid(strIn, n, 1))
            w2 = mimedecode(Mid(strIn, n + 1, 1))
            w3 = mimedecode(Mid(strIn, n + 2, 1))
            w4 = mimedecode(Mid(strIn, n + 3, 1))
            If w2 >= 0 Then _
             strOut = strOut + _
              Chr(((w1 * 4 + Int(w2 / 16)) And 255))
            If w3 >= 0 Then _
             strOut = strOut + _
              Chr(((w2 * 16 + Int(w3 / 4)) And 255))
            If w4 >= 0 Then _
             strOut = strOut + _
              Chr(((w3 * 64 + w4) And 255))
        Next
        base64_decode = strOut
    End Function
    Private Function mimedecode(ByVal strIn)
        If Len(strIn) = 0 Then
            mimedecode = -1 : Exit Function
        Else
            mimedecode = InStr(Base64Chars, strIn) - 1
        End If
    End Function
#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        msg.Clear()
    End Sub

    Private Sub btnUploadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFile.Click
        If UserID = "" Then
            msg.ShowError("UserID not found")
            Return
        End If
        If fileUpload.UploadedFiles Is Nothing OrElse fileUpload.UploadedFiles.Count = 0 Then
            msg.ShowError("Please select a file to upload.")
            Return
        End If
        If Not fileUpload.UploadedFiles(0).FileName.ToLower.EndsWith(".pdf") Then
            msg.ShowError("Only PDF files are accepted.")
            Return
        End If

        'File uploaded, save to server
        Dim newFileName As String = UserID & "-" & Guid.NewGuid.ToString & ".pdf"
        fileUpload.UploadedFiles(0).SaveAs(PDFSavePath & newFileName)

        Dim origFileName As String = System.IO.Path.GetFileName(fileUpload.UploadedFiles(0).FileName)

        'Create entry to database!
        Dim strQ As String = "INSERT INTO PDFFileUpload(UserID, PDFFileName, OriginalFileName, UploadedOn) "
        strQ += " Values('" & UserID.Replace("'", "''") & "', '" & newFileName.Replace("'", "''") & "', '" & origFileName.Replace("'", "''") & "', GetDate())"
        Try
            If DataHelper.ExecuteNonQuery(strQ) < 1 Then
                Return
            End If
        Catch ex As Exception
            msg.ShowError("Failed to save record. " & ex.Message)
            Return
        End Try
        msg.ShowInformation("File uploaded successfully.")

        'Finally, send an email...
        Dim msg1 As New System.Net.Mail.MailMessage()
        With msg1
            .To.Add(New System.Net.Mail.MailAddress(EmailTo))
            .From = New System.Net.Mail.MailAddress(EmailFrom)
            .Subject = EmailSubject
            .Attachments.Add(New System.Net.Mail.Attachment(PDFSavePath & newFileName))
            .Body = "New file uploaded from user " & UserID & ", filename=" & newFileName & ", original filename " & origFileName
        End With

        Dim mSMTP As New System.Net.Mail.SmtpClient
        With mSMTP
            .Credentials = New System.Net.NetworkCredential(EmailUserName, EmailPassword)
            .Host = EmailServer
        End With
        Try
            mSMTP.Send(msg1)
            msg.ShowInformation(origFileName & " uploaded and saved successfully")
        Catch ex As Exception
            msg.ShowError("Failed to send email. " & ex.Message)
        End Try
    End Sub

End Class