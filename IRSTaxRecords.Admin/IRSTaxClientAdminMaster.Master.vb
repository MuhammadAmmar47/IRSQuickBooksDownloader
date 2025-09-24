Public Partial Class IRSTaxClientAdminMaster
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.Url.AbsoluteUri.ToLower.Contains("login.aspx") Then
            Me.btnLogout.Visible = False
        ElseIf Not Request.Url.AbsoluteUri.ToLower.Contains("/client/") Then
            Me.btnLogout.Visible = False
        End If
    End Sub

    Private Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Globals.CALoginClientID = 0
        Response.Redirect("Login.aspx")
    End Sub
End Class