Public Partial Class SearchCustomer
    Inherits System.Web.UI.Page

    Public Property SearchFor() As String
        Get
            Return txtSearchFor.Text.Trim
        End Get
        Set(ByVal value As String)
            txtSearchFor.Text = value.Trim
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        msg.Clear()

        If Not Page.IsPostBack Then


            If Request.QueryString("SearchFor") IsNot Nothing Then SearchFor = Request.QueryString("SearchFor")
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Response.Redirect("SearchCustomer.aspx?SearchFor=" & SearchFor)
    End Sub

    Private Sub grdMain_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
        If SearchFor.Trim = "" Then
            Me.grdMain.Visible = False
            Return
        End If

        Dim strQ As String = "SELECT * FROM Customer "
        strQ += " WHERE UserID='" & SearchFor.Replace("'", "''") & "'"
        If IsNumeric(SearchFor) Then strQ += " OR CustomerID=" & CInt(SearchFor)
        strQ += " OR CompanyName = '" & SearchFor.Replace("'", "''") & "'"

        Me.grdMain.DataSource = DataHelper.ExecuteQuery(strQ)
    End Sub
End Class