Public Partial Class ClientDetail
    Inherits System.Web.UI.Page

    Private ReadOnly Property CurrentClientID() As Integer
        Get
            Return Val(Request.QueryString("ClientID"))
        End Get
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        msg.Clear()

        If Not Page.IsPostBack Then
            LoadClientDetail()

            Me.pnlUsers.Visible = CurrentClientID > 0

            If Session("ActionMsg") IsNot Nothing Then
                msg.ShowInformation(Session("ActionMsg"))
                Session("ActionMsg") = Nothing
            End If
        End If
    End Sub
    Private Sub LoadClientDetail()
        If CurrentClientID < 1 Then Return
        Dim c As Core.Contacts.Client = ContactServices.Clients.GetClientByID(CurrentClientID)
        If c Is Nothing Then
            msg.ShowError("Client with id " & CurrentClientID & " was not found")
            Return
        End If
        Me.txtNameOfCompany.Text = c.ClientName
        Me.txtUserID.Text = c.LoginUserName
        txtPassword.Text = c.LoginPassword
        Me.chkAddStatistics.Checked = c.AddStatistics
    End Sub

    Private Sub btnSaveClient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveClient.Click
        Dim c As Core.Contacts.Client = Nothing
        If CurrentClientID > 0 Then
            c = Core.ContactServices.Clients.GetClientByID(CurrentClientID)
        Else
            c = New Core.Contacts.Client
        End If
        With c
            .AddStatistics = Me.chkAddStatistics.Checked
            .ClientName = Me.txtNameOfCompany.Text.Trim
            .LoginPassword = Me.txtPassword.Text
            .LoginUserName = Me.txtUserID.Text.Trim
        End With
        If c.ID > 0 Then
            ContactServices.Clients.Update(c)
        Else
            ContactServices.Clients.AddNew(c)
        End If

        Session("ActionMsg") = "Client saved successfully"
        Response.Redirect("ClientDetail.aspx?ClientID=" & c.ID)
    End Sub

    Private Sub grdMain_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
        Dim strQ As String = "SELECT     dbo.Customer.CompanyName, dbo.Customer.CustomerID, dbo.Customer.Name, dbo.Customer.UserID, dbo.Customer.Password"
        strQ += " FROM dbo.ClientUser INNER JOIN dbo.Customer ON dbo.ClientUser.UserID = dbo.Customer.CustomerID"
        strQ += " WHERE ClientUser.clientID=" & CurrentClientID
        Me.grdMain.DataSource = DataHelper.ExecuteQuery(strQ)
    End Sub

    Private Sub btnSaveUsers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveUsers.Click
        If Me.txtUserId1.Text.Trim = "" Then
            msg.ShowError("Please enter username to add to client")
            Return
        End If

        Dim customers() As String = Split(Me.txtUserId1.Text.Trim, vbCrLf)
        Dim errorMsg As String = ""
        For Each Customer As String In customers
            Try
                AddUser(Customer, CurrentClientID)
            Catch ex As Exception
                errorMsg += "UserName " & Customer & ", " & ex.Message & "<br>"
            End Try
        Next


        Session("ActionMsg") = "User associated with client successfully"
        If errorMsg <> "" Then Session("ActionMsg") += "<br>But failed to add one or more userids.<br>" & errorMsg
        Response.Redirect(Request.Url.PathAndQuery)
    End Sub
    Private Shared Sub AddUser(ByVal UserNAme As String, ByVal CurrentClientId As Integer)
        Dim strQ As String = "SELECT CustomerID, UserID from Customer WHERE UserId='" & UserNAme.Replace("'", "''").Trim & "'"
        Dim dt As DataTable = DataHelper.ExecuteQuery(strQ)
        If dt.Rows.Count = 0 Then
            Throw New Exception("No user found from user id " & UserNAme)
            Return
        End If

        Dim customerId As Integer = dt.Rows(0)("CustomerID")
        Dim dt1 As DataTable = DataHelper.ExecuteQuery("SElect ClientID From ClientUser WHERE UserID=" & customerId)
        If dt1.Rows.Count > 0 AndAlso dt1.Rows(0)("ClientID") <> CurrentClientId Then
            Throw New Exception("User " & UserNAme & " is already associated with ClientID " & dt1.Rows(0)("ClientID"))
            Return
        End If

        ContactServices.Clients.ClientUser_add(CurrentClientId, customerId)
    End Sub

    Private Sub btnDoAction_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDoAction.Click
        Select Case ddlDoAction.SelectedValue
            Case ""
                msg.ShowError("Please select an action")
            Case "RemoveUserAssociation"
                For Each item As Telerik.Web.UI.GridDataItem In Me.grdMain.Items
                    If Not item.Selected Then Continue For
                    Dim lblCustomerID As Label = CType(item.FindControl("lblCustomerID"), Label)

                    ContactServices.Clients.ClientUser_delete(CurrentClientID, lblCustomerID.Text)
                Next
                Session("ActionMsg") = "Selected users association removed successfully"
                Response.Redirect(Request.Url.PathAndQuery)
        End Select
    End Sub
End Class