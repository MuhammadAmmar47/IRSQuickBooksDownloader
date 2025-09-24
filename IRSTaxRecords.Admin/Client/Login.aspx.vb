Public Partial Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        If Me.txtPassword.Text.Trim = "" OrElse txtUserName.Text.Trim = "" Then
            msg.ShowError("Please enter username and password")
            Return
        End If

        Dim strQ As String = "SELECT * FROM Client WHERE LoginUserName='" & txtUserName.Text.Trim.Replace("'", "''") & "'"
        Dim dt As DataTable = DataHelper.ExecuteQuery(strQ)
        If dt.Rows.Count = 0 Then
            msg.ShowError("Invalid username.")
            Return
        End If

        If dt.Rows(0)("LoginPassword") <> Me.txtPassword.Text Then
            msg.ShowError("Invalid password")
            Return
        End If

        Globals.CALoginClientID = dt.Rows(0)("ID")
        Response.Redirect("SearchResult.aspx")
    End Sub
End Class