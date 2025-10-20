
Public Class Global_asax
    Inherits HttpApplication

    Sub Application_Start(sender As Object, e As EventArgs)
        ' Fires when the application is started
    End Sub

    Protected Sub Application_AcquireRequestState(sender As Object, e As EventArgs)
        'Try
        '    Dim context = HttpContext.Current
        '    If context Is Nothing OrElse context.Request Is Nothing Then Return

        '    ' Current path
        '    Dim path As String = context.Request.AppRelativeCurrentExecutionFilePath.ToLower()

        '    If path = "~/" OrElse path = "~\" Then
        '        context.Response.Redirect("~/Default.aspx")
        '        Return
        '    End If

        '    ' Always allow Default.aspx and Login.aspx
        '    If path.Contains("default.aspx") Then
        '        ' If cookie exists, clear it and redirect to login
        '        Dim Cookie = context.Request.Cookies(".ASPXAUTH")
        '        If Cookie IsNot Nothing AndAlso Not String.IsNullOrEmpty(Cookie.Value) Then
        '            Dim expiredCookie As New HttpCookie(".ASPXAUTH")
        '            expiredCookie.Expires = DateTime.Now.AddDays(-1)
        '            context.Response.Cookies.Add(expiredCookie)

        '            context.Response.Redirect("~/login.aspx")
        '        End If

        '        Return
        '    End If

        '    ' Allow login.aspx and static resources
        '    If path.Contains("login.aspx") OrElse
        '       path.Contains("css") OrElse
        '       path.Contains("js") OrElse
        '       path.Contains("images") OrElse
        '       path.EndsWith("asmx") Then
        '        Return
        '    End If

        '    ' Check cookie
        '    Dim authCookie = context.Request.Cookies(".ASPXAUTH")
        '    If authCookie Is Nothing OrElse String.IsNullOrEmpty(authCookie.Value) Then
        '        context.Response.Redirect("~/login.aspx")
        '    End If

        'Catch ex As Exception
        '    ' Optional: log error
        'End Try
    End Sub

    Protected Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        If Request.Cookies(".ASPXAUTH") IsNot Nothing Then
            Dim cookie As New HttpCookie(".ASPXAUTH")
            cookie.Expires = DateTime.Now.AddDays(-1)
            Response.Cookies.Add(cookie)
        End If
    End Sub

End Class