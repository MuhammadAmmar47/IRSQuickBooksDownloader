Public Class ChangePassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
        End If
    End Sub
    Protected Sub btnChangePassword_Click(sender As Object, e As EventArgs) Handles btnChangePassword.Click
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

        ' Example validation: Replace with actual user verification logic
        Dim isCurrentPasswordValid As Boolean = (currentPwd = "oldpassword") ' demo check

        If Not isCurrentPasswordValid Then
            lblMessage.Text = "Your current password is incorrect."
            lblMessage.CssClass = "text-danger fw-semibold d-block mb-3"
            Exit Sub
        End If



        ' TODO: Implement actual password update logic (e.g., update in database)
        lblMessage.Text = "Your password has been successfully changed!"
        lblMessage.CssClass = "text-success fw-semibold d-block mb-3"
    End Sub
End Class