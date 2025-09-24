Public Partial Class _Default1
    Inherits System.Web.UI.Page


    Public ClientName As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Globals.CALoginClientID < 1 Then Response.Redirect("Login.aspx") : Return
        Dim c As Core.Contacts.Client = ContactServices.Clients.GetClientByID(Globals.CALoginClientID)
        If c Is Nothing Then
            msg.ShowError("Client with id " & Globals.CALoginClientID & " was not found")
            Return
        End If
        ClientName = c.ClientName
        trStats.Visible = c.AddStatistics
    End Sub

    Private Sub grdMain_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdMain.ItemDataBound
        Try
            Dim btnLoginto As LinkButton = CType(e.Item.FindControl("btnLoginto"), LinkButton)

            Dim dr As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim UserID As String = dr("UserID")
            Dim pwd As String = dr("Password")

            btnLoginto.OnClientClick = "fnSubmit2('" & UserID & "','" & pwd & "'); return false;"
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdMain_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
        Dim strQ As String = "SELECT     dbo.Customer.CustomerID, dbo.Customer.CompanyName, dbo.Customer.Name, dbo.Customer.Email, dbo.Customer.UserID, dbo.Customer.Password"
        strQ += " FROM dbo.ClientUser INNER JOIN dbo.Customer ON dbo.ClientUser.UserID = dbo.Customer.CustomerID "
        strQ += " WHERE ClientUser.ClientID=" & Globals.CALoginClientID
        strQ += " Order By Name"
        Me.grdMain.DataSource = DataHelper.ExecuteQuery(strQ)
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Response.Redirect("SearchResult.aspx?Keywords=" & Me.txtFindRecords.Text.Trim)
    End Sub
End Class