Imports System.IO

Namespace Email
    Public Class MailSender
#Region "Send Email Methods"
        Public Shared Function SendMail(ByVal toName As String, ByVal toEmail As String, ByVal template As EmailTemplate) As Boolean
            Return SendMail(toName, toEmail, template)
        End Function
        Public Shared Function SendMail(ByVal toName As String, ByVal toEmail As String, ByVal template As EmailTemplate, ByVal attachments As Generic.List(Of String)) As Boolean
            Try
                If Utilities.Validations.ValidateEMail(template.SenderEmail) = False Then
                    Throw New Exception("Invalid sender email address")
                End If

                Dim msg As New System.Net.Mail.MailMessage
                With msg
                    .From = New Net.Mail.MailAddress(template.SenderEmail, template.SenderName)
                    .To.Add(New Net.Mail.MailAddress(toEmail, toName))
                    .Subject = template.Subject
                    .IsBodyHtml = True
                    .Body = template.Body
                End With

                If attachments IsNot Nothing AndAlso attachments.Count > 0 Then
                    For Each attach As String In attachments
                        msg.Attachments.Add(New System.Net.Mail.Attachment(attach))
                    Next
                End If

                Dim smtp As New Net.Mail.SmtpClient(AppSettings.EMailServer)
                With smtp
                    .Host = AppSettings.EMailServer
                    If AppSettings.EMailUserName <> "" AndAlso AppSettings.EMailPassword <> "" Then
                        .Credentials = New System.Net.NetworkCredential(AppSettings.EMailUserName, AppSettings.EMailPassword)
                    End If
                End With

                Try
                    smtp.Send(msg)
                Catch ex As Exception
                    Throw
                Finally
                    msg.Dispose()
                    msg = Nothing
                    smtp = Nothing
                End Try
                Return True

            Catch ex As Exception
                Trace.WriteLine("Failed to send email. " & ex.Message)
                Exceptions.ErrorLogging.AddToLog(Exceptions.ERROR_TYPE.EXCEPTION, "Failed to send email: " & ex.Message, ex)
                Throw
            End Try
        End Function

        Public Shared LastError As String = ""
        Public Shared Function Send(ByVal [from] As String, ByVal [to] As String, ByVal cc As String, ByVal subject As String, ByVal Body As String, ByVal attachments As Generic.List(Of String)) As Boolean
            Try
                Dim SenderAddress As String = [from]
                Dim SenderName As String = AppSettings.EmailFromName
                Dim replyToemail As String = [from]

                If cc Is Nothing OrElse cc = "" Then
                    cc = AppSettings.CCEmailAddress
                End If

                Dim siteRoot As String = ""

                Dim mMsg As New System.Net.Mail.MailMessage
                With mMsg
                    If replyToemail <> "" Then .ReplyTo = New System.Net.Mail.MailAddress(replyToemail, replyToemail)

                    .From = New System.Net.Mail.MailAddress(SenderAddress, SenderName)

                    If [to].Contains(";") Then
                        For Each mTo As String In Split([to], ";")
                            If mTo.Trim <> "" Then .To.Add(New System.Net.Mail.MailAddress(mTo.Trim))
                        Next
                    Else
                        .To.Add(New System.Net.Mail.MailAddress([to], [to]))
                    End If


                    If cc IsNot Nothing AndAlso cc <> "" Then
                        .CC.Add(New System.Net.Mail.MailAddress(cc, cc))
                    End If


                    .Attachments.Clear()

                    If Not attachments Is Nothing Then
                        Dim temp As Integer
                        For temp = 0 To attachments.Count - 1
                            Dim attach As New System.Net.Mail.Attachment(attachments(temp))
                            .Attachments.Add(attach)
                        Next
                    End If

                    .Subject = subject

                    .Body = Body
                    .IsBodyHtml = True

                    .Headers.Add("X-Sender", SenderAddress)
                    .Headers.Add("X-Receiver", [to])
                End With

                Return Send(mMsg)
            Catch ex As Exception
                LastError = ex.Message
                Trace.WriteLine("Failed to send email: " & ex.Message)
                Throw
            End Try
        End Function
        Public Shared Function Send(ByVal mMsg As System.Net.Mail.MailMessage) As Boolean
            Try
                LastError = ""
                Try
                    Dim mSMTP As New System.Net.Mail.SmtpClient

                    With mSMTP
                        .Host = AppSettings.EMailServer  'WebAppSettings.MailServer(ClientID)
                        .Port = AppSettings.EmailPort 'WebAppSettings.MailServerPort(ClientID)

                        If AppSettings.EmailRequiresAuthentication Then  'WebAppSettings.MailServerRequiresAuth(ClientID) Then
                            .Credentials = New System.Net.NetworkCredential(AppSettings.EmailUserName, AppSettings.EmailPassword)
                        End If

                        .EnableSsl = AppSettings.EmailUseSSL
                    End With
                    'End If

                    mSMTP.Send(mMsg)
                    Return True
                Catch ex As Exception
                    Throw
                Finally
                    If Not mMsg Is Nothing Then
                        mMsg.Dispose()
                    End If
                End Try
            Catch ex As Exception
                LastError = ex.Message
                Trace.WriteLine("Exception in sending email: " & ex.Message)
                Throw
            End Try
        End Function

#End Region

        'Public Shared Function SendPasswordReminderEmail(ByVal u As Customer) As Boolean

        '    Dim template As EmailTemplate = EmailTemplate.GetTemplate(EMAIL_TYPE.PASSWORD_REMINDER)

        '    If u.Password Is Nothing OrElse u.Password.Trim = "" Then
        '        u.Password = Utilities.GeneratePassword(7)
        '        DataServices.UpdateCustomer(u)
        '    End If

        '    u = DataServices.GetCustomer(u.ID)

        '    Dim mapper As New Email.PropertyMapper(u)
        '    template.Body = mapper.MapContent(template.Body)

        '    Return SendMail(u.FirstName & " " & u.LastName, u.UserName, template)
        'End Function
        'Public Shared Function SendWelcomeEmail(ByVal u As Customer) As Boolean
        '    Dim template As EmailTemplate = EmailTemplate.GetTemplate(EMAIL_TYPE.WELCOME)

        '    Dim mapper As New Email.PropertyMapper(u)
        '    template.Body = mapper.MapContent(template.Body)

        '    Return SendMail(u.FirstName & " " & u.LastName, u.UserName, template)
        'End Function
#Region "Email methods"
        Public Shared Function SendContactUsEmail(ByVal c As ContactUs) As Boolean
            Dim template As EmailTemplate = EmailTemplate.GetTemplate(EMAIL_TYPE.CONTACT_US)

            Dim mapper As New Email.PropertyMapper(c)
            template.Body = mapper.MapContent(template.Body)

            Dim result As Boolean = SendMail(AppSettings.EmailFromName, AppSettings.EmailFromEmail, template)

            'SEnd CC to customer support

            Return result
        End Function
        Public Shared Function SendReferredEmail(ByVal r As ReferAFriend) As Boolean
            Dim template As EmailTemplate = EmailTemplate.GetTemplate(EMAIL_TYPE.REFER_A_FRIEND)

            Dim mapper As New Email.PropertyMapper(r)
            template.Body = mapper.MapContent(template.Body)

            Dim result As Boolean = SendMail(AppSettings.CustomerSupportName, AppSettings.CustomerSupportEmail, template)

            Return result
        End Function
        Public Shared Function SendOnlineAnalysisEmail(ByVal a As Content.Analysis) As Boolean
            Dim template As EmailTemplate = EmailTemplate.GetTemplate(EMAIL_TYPE.ONLINE_ANALYSIS)

            Dim mapper As New Email.PropertyMapper(a)
            template.Body = mapper.MapContent(template.Body)

            Dim result As Boolean = SendMail(a.FirstName & " " & a.LastName, a.Email, template)

            'SEnd CC to customer support
            SendMail(AppSettings.CustomerSupportName, AppSettings.CustomerSupportEmail, template)
            Return result
        End Function

        Public Shared Function SendCommentsUpdatedEmail(OrderID As Integer, comments As String) As Boolean
            Dim content As String = $"New comments entered for Order# {OrderID}.<br>
                        Comments are <br>{comments}"



            Dim template As New EmailTemplate
            With template
                .Body = content
                .IsHtml = True
                .Name = "OrderComments"
                .SenderEmail = AppSettings.CustomerSupportEmail
                .SenderName = AppSettings.CustomerSupportName
                .Subject = $"New Comments for Order# {OrderID}"
            End With



            Return SendMail(AppSettings.CustomerSupportEmail, AppSettings.CustomerSupportEmail, template, Nothing)
        End Function
        Public Shared Function SendOrderFilePadUploadedEmail(ByVal o As Core.Content.OrderPadFile) As Boolean
            Dim u As Customer = DataServices.GetCustomer(o.UserID)
            Dim content As String = "New File Uploaded.<br>"
            content += "Uplloaded by " & u.UserID & " (" & u.CustomerID & ")<br>"
            content += "File Name: " & o.FileNameReal & "<br>"
            content += "File Name (on disk): " & o.FileName & "<br>"


            Dim template As New EmailTemplate
            With template
                .Body = content
                .IsHtml = True
                .Name = "OrderFileUploaded"
                .SenderEmail = AppSettings.CustomerSupportEmail
                .SenderName = AppSettings.CustomerSupportName
                .Subject = "New file " & o.FileNameReal & " uploaded by UserID " & o.UserID
            End With


            Dim attachments As New Generic.List(Of String)
            attachments.Add(AppSettings.OrderPadFilesDiskFolderPath(o.UserID) & o.FileName)

            Return SendMail(AppSettings.CustomerSupportEmail, AppSettings.CustomerSupportEmail, template, attachments)
        End Function

        Public Shared Function SendOrderCreatedEmail(customerName As String, emailTo As String, OrderName As String, FormName As String, OrderNumbers As String) As Boolean
            Dim content As String = DataHelper.ExecuteQuery("select fldmessage from tblEmail where fldid=1").Rows(0)("fldmessage")
            content = content.Replace("<$CustomerName$>", customerName)
            content = content.Replace("<$ordername$>", OrderName)
            content = content.Replace("<$frmname$>", FormName)
            content = content.Replace("<$ordernumber$>", OrderNumbers)
            Dim t As New Email.EmailTemplate With {
            .Body = content,
            .IsHtml = True,
            .Name = "Order Received",
            .SenderEmail = AppSettings.CustomerSupportEmail,
            .SenderName = AppSettings.CustomerSupportName,
            .Subject = "Order Received"
            }
            Try
                If Email.MailSender.Send(t.SenderEmail, emailTo, "", t.Subject, t.Body, Nothing) Then
                    Diagnostics.Trace.WriteLine($"Email sent successfully for Order#s {OrderNumbers}")
                    Return True
                Else
                    Diagnostics.Trace.WriteLine($"Email sent FAILED for Order#s {OrderNumbers}. {Email.MailSender.LastError}")
                    Return False
                End If
            Catch ex As Exception
                Diagnostics.Trace.WriteLine($"Email sent FAILED for Order#s {OrderNumbers}. {ex.MessageWithInnerExceptionDetails}")
            End Try
            Return False
        End Function
        Public Shared Function SendOrderCreatedEmailToAdmin(customerName As String, UserID As String, OrderName As String, FormName As String, OrderNumbers As String, LoanNumber As String, pdfFilePath As String) As Boolean
            Dim content As String = $"Dear Admin,<br />  
   Customer name ({customerName}) with User ID ({UserID}), has ordered ({FormName}) for Taxpayer Name ({OrderName}). <br />
   The order form is attached. This order was entered at {Now.ToString()}. The loan number for this order is ({LoanNumber}).
"
            Dim t As New Email.EmailTemplate With {
            .Body = content,
            .IsHtml = True,
            .Name = "New Order Received",
            .SenderEmail = AppSettings.CustomerSupportEmail,
            .SenderName = AppSettings.CustomerSupportName,
            .Subject = "New Order Received"
            }
            Dim attachments As New List(Of String)
            If pdfFilePath.IsNotNullOrEmpty AndAlso System.IO.File.Exists(pdfFilePath) Then
                attachments.Add(pdfFilePath)
            End If
            Try

                If Email.MailSender.Send(t.SenderEmail, AppSettings.CustomerSupportEmail, "", t.Subject, t.Body, attachments) Then
                    Diagnostics.Trace.WriteLine($"Email sent successfully for Order#s {OrderNumbers}")
                    Return True
                Else
                    Diagnostics.Trace.WriteLine($"Email sent FAILED for Order#s {OrderNumbers}. {Email.MailSender.LastError}")
                    Return False
                End If
            Catch ex As Exception
                Diagnostics.Trace.WriteLine($"Email sent FAILED for Order#s {OrderNumbers}. {ex.MessageWithInnerExceptionDetails}")
            End Try
            Return False
        End Function
#End Region
    End Class
End Namespace