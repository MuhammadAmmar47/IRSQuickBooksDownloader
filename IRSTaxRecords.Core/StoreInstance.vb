Imports System.Web.Security
Imports System.Security.Principal
Imports System.Web

Public Class StoreInstance

    Public Shared Function LoginUser(ByVal email As String, ByVal pwd As String, ByVal bRemember As Boolean) As Boolean
        Dim c As Customer = DataServices.GetCustomer(email)

        If c Is Nothing Then Throw New Exception("Invalid user id")

        If c.Password = pwd Then
            Dim userData As String = AuthUser(c, bRemember)

            'OrderServices.AssignSessionCartToUser(c.CustomerID)

            Dim redirectUrl As String
            redirectUrl = FormsAuthentication.GetRedirectUrl(c.UserID, bRemember)
            If Not (String.IsNullOrEmpty(redirectUrl)) Then redirectUrl = "/Welcome.aspx"
            'If c.IsAdmin Then
            '    'redirectUrl = "~/Admin/"
            '    redirectUrl = "~/Admin"
            'Else
            '    redirectUrl = "~/MyAccount/"
            'End If

            HttpContext.Current.Session("LoginId") = 100001

                'FormsAuthentication.SetAuthCookie(c.ID, False)
                HttpContext.Current.Response.Redirect(redirectUrl)

                If Not HttpContext.Current.Request.QueryString("ReturnUrl") Is Nothing Then
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.QueryString("ReturnUrl"))
                Else
                    HttpContext.Current.Response.Redirect("MyInvoices.aspx")
                End If

                Return True
            Else
                Throw New Exception("Password doesn't match")
        End If
    End Function
    Public Shared Function LoginUser(ByVal email As String, ByVal pwd As String) As Boolean
        Return LoginUser(email, pwd, False)
    End Function
    Public Shared Function SignOff() As Boolean
        FormsAuthentication.SignOut()
        HttpContext.Current.Response.Cookies.Clear()
        HttpContext.Current.Request.Cookies.Clear()
        HttpContext.Current.Session.Clear()
        HttpContext.Current.Session.Abandon()
        If HttpContext.Current.Request.IsAuthenticated Then
            FormsAuthentication.SignOut()
        End If
    End Function
    Public Shared Function AuthUser(ByVal user As Customer, ByVal remember As Boolean) As String
        Dim userData As String = CStr(user.CustomerID) + Constant.DefaultSeparetor
        Dim roles() As String = Nothing
        'If user.IsAdmin Then
        '    ReDim roles(1)
        '    roles(0) = "Admin"
        '    roles(1) = "User"
        'Else
        '    ReDim roles(0)
        '    roles(0) = "User"
        'End If

        If Not roles Is Nothing Then
            For Each r As String In roles
                userData = userData + r + Constant.DefaultSeparetor
            Next
        End If

        Dim ticket As New FormsAuthenticationTicket(1, user.UserID, DateTime.Now, DateTime.Today.AddMinutes(9999999), remember, userData)

        Dim encTicket As String = FormsAuthentication.Encrypt(ticket)
        Dim cookie As HttpCookie = New HttpCookie(FormsAuthentication.FormsCookieName, encTicket)

        If ticket.IsPersistent Then
            cookie.Expires = ticket.Expiration
        End If

        Dim rolesArr As String() = userData.Split(Constant.DefaultSeparetor)
        Dim id As FormsIdentity = New FormsIdentity(ticket)
        HttpContext.Current.User = New GenericPrincipal(id, rolesArr)

        HttpContext.Current.Response.Cookies.Add(cookie)
        Return userData
    End Function
    Public Shared ReadOnly Property CurrentUserId() As Integer
        Get
            Dim context As HttpContext = HttpContext.Current
            If context Is Nothing Then
                Return -1
            End If

            If context.User.Identity.IsAuthenticated Then
                Return CType(context.User.Identity, System.Web.Security.FormsIdentity).Ticket.UserData.Split(Constant.DefaultSeparetor)(0)
            End If

            Return -1
        End Get
    End Property
    Public Shared ReadOnly Property IsUserLoggedIn() As Boolean
        Get
            Return HttpContext.Current.User.Identity.IsAuthenticated
        End Get
    End Property
    Public Shared ReadOnly Property CurrentUser() As Customer
        Get
            If IsUserLoggedIn Then Return DataServices.GetCustomer(CurrentUserId)
            Return Nothing
        End Get
    End Property
    Public Shared ReadOnly Property GetCustomerId() As Integer
        Get
            Dim authCookie As HttpCookie = HttpContext.Current.Request.Cookies(".ASPXAUTH")
            If authCookie IsNot Nothing AndAlso Not String.IsNullOrEmpty(authCookie.Value) Then
                Try
                    Dim ticket As FormsAuthenticationTicket = FormsAuthentication.Decrypt(authCookie.Value)
                    If ticket IsNot Nothing AndAlso Not String.IsNullOrEmpty(ticket.UserData) Then
                        Dim userData As String = ticket.UserData.TrimEnd(";"c)
                        Dim parsedId As Integer
                        If Integer.TryParse(userData, parsedId) Then
                            Return parsedId
                        End If
                    End If
                Catch ex As Exception
                    Return -1
                End Try
            End If
            Return -1
        End Get
    End Property
End Class
