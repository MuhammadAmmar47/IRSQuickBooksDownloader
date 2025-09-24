Public Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        msg.Clear()

        If Not Page.IsPostBack Then

        End If
    End Sub

    Private Sub grdMain_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
        Dim strq As String = "SELECT * FROM Client ORder By ClientName"
        Me.grdMain.DataSource = DataHelper.ExecuteQuery(strq)
    End Sub
End Class