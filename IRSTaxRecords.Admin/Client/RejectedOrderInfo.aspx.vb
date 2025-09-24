Public Class RejectedOrderInfo
    Inherits System.Web.UI.Page
    Private ReadOnly Property OrderId As Integer
        Get
            Return Val(Request.QueryString("OrderID"))
        End Get
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        msg.Clear()

        If Not Page.IsPostBack Then
            Dim strQ As String = $"SELECT     tblOrder.*
            FROM dbo.ClientUser 
            INNER JOIN dbo.tblorder ON dbo.ClientUser.UserID = dbo.tblorder.fldcustomeriD
            WHERE ClientUser.ClientID = {Globals.CALoginClientID }
            AND fldOrderNumber= {Me.OrderId}"

            Dim dt As DataTable = DataHelper.ExecuteQuery(strQ)
            If dt.Rows.Count = 0 Then
                Response.Redirect("SearchResult.aspx")
            End If

            If Request.QueryString("reason") IsNot Nothing Then
                Select Case Request.QueryString("reason").ToString.ToLower
                    Case "address"
                        lblInfoType.Text = "Enter correct address"
                    Case "ssn"
                        lblInfoType.Text = "Enter correct name or SSN"
                    Case Else
                        lblInfoType.Text = "Enter correct info"
                End Select
            End If

        End If
    End Sub

    Private Sub btnSubmitInfo_Click(sender As Object, e As EventArgs) Handles btnSubmitInfo.Click
        If txtComments.Text.Trim = "" Then
            msg.ShowError("Please enter information.")
            Return
        End If

        Dim strQ As String = $"Insert into tblComments(OrderId,fldComments,fldCommentDate)  Values ({Me.OrderId},'{txtComments.Text.Replace("'", "''")}','{Now.ToString}')"
        DataHelper.ExecuteNonQuery(strQ)

        Try
            Core.Email.MailSender.SendCommentsUpdatedEmail(Me.OrderId, txtComments.Text)
            msg.ShowInformation($"Information saved successfully and emailed admin.")
        Catch ex As Exception
            msg.ShowInformation($"Information saved successfully. But failed to email admin. {ex.Message}, {ex.StackTrace}")
        End Try


    End Sub
End Class