Public Class Site1
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim authCookie = Request.Cookies(".ASPXAUTH")

            ' Hide button on Login.aspx
            If Request.Url.AbsolutePath.ToLower().Contains("login.aspx") Then
                lnkLoginLogout.Visible = False
                Return
            End If

            ' If cookie exists → show Logout
            If authCookie IsNot Nothing AndAlso Not String.IsNullOrEmpty(authCookie.Value) Then
                lnkLoginLogout.Text = "Logout"
            Else
                lnkLoginLogout.Text = "Account Login"
            End If
        End If
    End Sub
    Protected Sub lnkLoginLogout_Click(sender As Object, e As EventArgs)
        If lnkLoginLogout.Text = "Logout" Then
            Session.Clear()
            Session.Abandon()
            ' Clear cookie
            If Request.Cookies(".ASPXAUTH") IsNot Nothing Then
                Dim cookie As New HttpCookie(".ASPXAUTH")
                cookie.Expires = DateTime.Now.AddDays(-1)
                Response.Cookies.Add(cookie)
            End If

            Response.Redirect("~/Login.aspx")
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub

End Class