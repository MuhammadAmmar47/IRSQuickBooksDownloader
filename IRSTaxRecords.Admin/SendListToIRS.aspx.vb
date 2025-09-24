Public Partial Class SendListToIRS
    Inherits System.Web.UI.Page

    Private ReadOnly Property BatchNumber() As Integer
        Get
            Return Val(Request.QueryString("ListID"))
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        msg.Clear()
        If Not Page.IsPostBack Then
            Dim filePath As String = Server.MapPath("/EmailTemplates/ListRejectQuery.html")
            If Not System.IO.File.Exists(filePath) Then
                Me.txtEditor.Content = "File " & filePath & " couldn't be loaded"
            Else
                Dim content As String = System.IO.File.ReadAllText(filePath)
                Dim list As Core.Content.ListType = ListServices.GetList(BatchNumber)
                If list Is Nothing Then
                    msg.ShowError("List with ID " & BatchNumber & " was not found")
                Else
                    content = content.Replace("#?IRSBatchNumber?#", list.IRSBatchNumber)
                    content = content.Replace("#?OrdersList?#", GetOrdersGrid)

                    txtSubject.Text = "Please send Reject Reason for Batch# " & list.IRSBatchNumber
                End If
                Me.txtEditor.Content = content
            End If

        End If
    End Sub
    Private Function GetOrdersGrid() As String
        Dim sb As New StringBuilder
        sb.AppendLine("<table>")
        Dim dt As DataTable = DataHelper.ExecuteQuery("Select * from tblOrder WHERE fldStatus='p' AND fldListID=" & BatchNumber)
        For temp As Integer = 0 To dt.Rows.Count - 1
            Dim OrdeRId As Integer = dt.Rows(temp)("fldOrderNumber")
            Dim o As Orders.Order = OrderServices.GetOrder(OrdeRId)
            sb.AppendLine("<tr>")
            sb.AppendLine("<td>" & temp + 1 & "</td>")
            sb.AppendLine("<td>" & o.fldrequestname & "</td>")
            sb.AppendLine("<td>" & o.fldssnno & "</td>")
            sb.AppendLine("<td>" & GenericArrayToString(BuildTaxYearListOrdered(o)).Replace(",", "-") & "</td>")
            sb.AppendLine("<td>" & o.fldordernumber & "</td>")
            sb.AppendLine("</tr>")
        Next
        
        sb.AppendLine("</table>")
        Return sb.ToString
    End Function
    Private Function ValidateForm() As Boolean
        Dim msg As String = ""
        If Not Utilities.Validations.ValidateEMail(Me.txtEmailFrom.Text.Trim) Then msg += "Please enter valid from email<br>"
        If Not Utilities.Validations.ValidateEMail(Me.txtEmailto.Text.Trim) Then msg += "Please enter valid to email<br>"
        If Me.txtSubject.Text.Trim = "" Then msg += "Please enter subject.<br>"
        If Me.txtEditor.Content.Trim = "" Then msg += "Please enter email message.<br>"

        If msg = "" Then Return True
        Me.msg.ShowError(msg)
        Return False
    End Function
    Private Sub btnSendEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSendEmail.Click
        If Not ValidateForm() Then Return

        Try
            If Core.Email.MailSender.Send(Me.txtEmailFrom.Text, Me.txtEmailto.Text, "", Me.txtSubject.Text, Me.txtEditor.Content, Nothing) Then
                msg.ShowInformation("Email sent successfully")
            Else
                msg.ShowError("Failed to send email. " & Core.Email.MailSender.LastError)
            End If
        Catch ex As Exception
            msg.ShowError("ERROR: " & ex.Message)
        End Try
    End Sub
End Class