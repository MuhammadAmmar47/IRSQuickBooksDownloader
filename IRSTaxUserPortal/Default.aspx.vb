Public Class Default1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim authCookie = Request.Cookies(".ASPXAUTH")

            If authCookie IsNot Nothing AndAlso Not String.IsNullOrEmpty(authCookie.Value) Then
                pnlGrid.Visible = True
                BindGrid()
            Else
                pnlGrid.Visible = False
            End If
        End If
    End Sub
    Private Sub BindGrid()
        ' Example data (you can replace with database query)
        Dim dt As New DataTable()
        dt.Columns.Add("ID")
        dt.Columns.Add("Name")
        dt.Columns.Add("Email")

        dt.Rows.Add("1", "John Doe", "john@example.com")
        dt.Rows.Add("2", "Jane Smith", "jane@example.com")
        dt.Rows.Add("3", "Ali Khan", "ali@example.com")
        dt.Rows.Add("4", "John Doe", "john@example.com")
        dt.Rows.Add("5", "Jane Smith", "jane@example.com")
        dt.Rows.Add("6", "Ali Khan", "ali@example.com")

        Grid1.DataSource = dt
        Grid1.DataBind()
    End Sub

    ' For paging
    Protected Sub Grid1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Grid1.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub
End Class