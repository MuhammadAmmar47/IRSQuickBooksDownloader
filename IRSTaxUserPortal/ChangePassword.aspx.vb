Imports IRSTaxRecords
Imports IRSTaxRecords.Core

Public Class ChangePassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If StoreInstance.IsUserLoggedIn = False Then
                Response.Redirect("~/Login.aspx?ReturnUrl=" & Server.UrlEncode(Request.RawUrl))
            End If
        End If
    End Sub
    Protected Sub btnChangePassword_Click(sender As Object, e As EventArgs) Handles btnChangePassword.Click
        If Not ValidateStrongPassword() Then Return

        Dim currentPwd As String = txtCurrentPassword.Text.Trim()
        Dim newPwd As String = txtNewPassword.Text.Trim()
        Dim confirmPwd As String = txtConfirmPassword.Text.Trim()

        ' Validate required fields
        If String.IsNullOrEmpty(currentPwd) OrElse String.IsNullOrEmpty(newPwd) OrElse String.IsNullOrEmpty(confirmPwd) Then
            lblMessage.Text = "All fields are required."
            lblMessage.CssClass = "text-danger fw-semibold d-block mb-3"
            Exit Sub
        End If

        ' Check new passwords match
        If newPwd <> confirmPwd Then
            lblMessage.Text = "New password and confirmation do not match."
            lblMessage.CssClass = "text-danger fw-semibold d-block mb-3"
            Exit Sub
        End If

        Dim currentUser = StoreInstance.CurrentUser
        Dim isCurrentPasswordValid As Boolean = (currentUser.Password.Equals(currentPwd))

        If Not isCurrentPasswordValid Then
            lblMessage.Text = "Your current password is incorrect."
            lblMessage.CssClass = "text-danger fw-semibold d-block mb-3"
            Exit Sub
        End If

        currentUser.Password = txtNewPassword.Text
        DataServices.UpdateCustomerPassword(currentUser)


        ' TODO: Implement actual password update logic (e.g., update in database)
        lblMessage.Text = "Your password has been successfully changed!"
        lblMessage.CssClass = "text-success fw-semibold d-block mb-3"

        Session("PasswordChanged") = True
        Response.Redirect("Welcome.aspx")
    End Sub

    Const StrongPasswordsMinLength As Integer = 8
    Private Function ValidateStrongPassword() As Boolean
        Dim Password As String = txtNewPassword.Text
        Dim UserID = StoreInstance.CurrentUserId

        Dim ErrorMsg As String = ""
        If Password.Length < StrongPasswordsMinLength Then
            ErrorMsg += $"Invalid Password length. Provided length={Password.Length}, Required={StrongPasswordsMinLength}." & vbCrLf
        End If

        If Not System.Text.RegularExpressions.Regex.IsMatch(Password, ".*[A-Z]+.*") Then
            ErrorMsg += $"Password do not contains upper/lower case characters. User ID {UserID}" & vbCrLf
        End If

        If Not System.Text.RegularExpressions.Regex.IsMatch(Password, ".*[0-9]+.*") Then
            ErrorMsg += $"Password do not contains numeric characters. User ID {UserID}" & vbCrLf
        End If

        If ErrorMsg <> "" Then
            lblMessage.Text = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number and a special character."
            Diagnostics.Trace.WriteLine(ErrorMsg)
            Return False
        End If
        Return True
    End Function
End Class