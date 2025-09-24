Public Class ChangePassword
    Inherits System.Web.UI.Page

    Private ReadOnly Property CustomerID() As String
        Get
            If Request.QueryString("UserID") Is Nothing Then Return ""
            Dim value As String = Request.QueryString("UserID")
            If IsNumeric(value) Then Return value
            Dim UserNameFound As String = base64_decode(value)
            Dim strQ As String = "SELECT CustomerId, UserId,  password FROM Customer WHERE UserId='" & UserNameFound & "'"
            Dim dt As DataTable = DataHelper.ExecuteQuery(strQ)
            If dt.Rows.Count = 0 Then
                If IsNumeric(UserNameFound) Then
                    strQ = "SELECT CustomerId, UserId,  password FROM Customer WHERE CustomerId='" & UserNameFound & "'"
                    dt = DataHelper.ExecuteQuery(strQ)
                End If
            End If
            If dt.Rows.Count > 0 Then Return dt.Rows(0)("CustomerId")
            Return UserNameFound
        End Get
    End Property
    Private ReadOnly Property UserName As String
        Get
            Dim strQ As String = "SELECT UserID FROM customer WHERE CustomerID = " & CustomerID
            Dim dt As DataTable = DataHelper.ExecuteQuery(strQ)
            If dt.Rows.Count = 0 Then Return ""
            Return dt.Rows(0)("UserID")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblUserID.Text = CustomerID


        Me.txtNewPassword.Attributes.Add("value", Me.txtNewPassword.Text)
        Me.txtNewPassword2.Attributes.Add("value", Me.txtNewPassword2.Text)
        Me.txtOldPassword.Attributes.Add("value", Me.txtOldPassword.Text)

        If Not Page.IsPostBack Then
            If Val(CustomerID) < 1 Then
                msg.ShowError("Invalid User Id")
                Me.btnChangePassword.Enabled = False
                Return
            End If
        End If


    End Sub

    Dim loweCharacters As String = "abcdefghijklmpnoqrstuvwxyz"
    Dim upperCharacters As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    Dim numbers As String = "0123456789"
    Dim specialCharacters As String = "~!@#$%^&*()+[]{}<>/?;:"

    Private Function ValidatePassword() As Boolean
        Dim passwordError As String = "Password must be 8 characters long, must have one upper, one lower case, one number and one special character."
        Dim password As String = txtNewPassword.Text
        If password.Length < 8 Then msg.ShowError(passwordError) : Return False
        Dim isFound As Boolean = False
        For Each character As String In loweCharacters
            If password.Contains(character) Then isFound = True
        Next
        If Not isFound Then msg.ShowError(passwordError) : Return False

        isFound = False
        For Each character As String In upperCharacters
            If password.Contains(character) Then isFound = True
        Next
        If Not isFound Then msg.ShowError(passwordError) : Return False

        isFound = False
        For Each character As String In numbers
            If password.Contains(character) Then isFound = True
        Next
        If Not isFound Then msg.ShowError(passwordError) : Return False

        isFound = False
        For Each character As String In specialCharacters
            If password.Contains(character) Then isFound = True
        Next
        If Not isFound Then msg.ShowError(passwordError) : Return False

        Return True
    End Function
    Private Sub btnChangePassword_Click(sender As Object, e As EventArgs) Handles btnChangePassword.Click
        If Me.txtOldPassword.Text.Trim = "" Then
            msg.ShowError("Please enter old password")
            Return
        End If
        If Me.txtNewPassword.Text.Trim = "" Then
            msg.ShowError("Please enter new password")
            Return
        End If
        If Me.txtNewPassword.Text <> Me.txtNewPassword2.Text Then
            msg.ShowError("New password doesn't match.")
            Return
        End If

        If Not ValidatePassword() Then Return

        Dim strQ As String = "SELECT CustomerId, UserId,  password FROM Customer WHERE CustomerId=" & CustomerID
        Dim dt As DataTable = DataHelper.ExecuteQuery(strQ)
        If dt.Rows.Count = 0 Then
            msg.ShowError("No user found with CustomerID " & CustomerID)
            Return
        End If
        If dt.Rows(0)("password") <> txtOldPassword.Text Then
            msg.ShowError("Invalid old password")
            Return
        End If

        strQ = "UPDATE Customer SET password='" & txtNewPassword.Text.Replace("'", "''") & "', PasswordChangeRequired=0, PasswordChangedOn=GetDate() WHERE CustomerId=" & CustomerID
        DataHelper.ExecuteNonQuery(strQ)
        msg.ShowInformation("Password changed successfully. <a href=""newwelcomeframe.asp"">Click here to continue to your account</a>")
        'pnlPasswordChanged.Visible = True
        Me.pnlChangePassword.Visible = False
    End Sub
End Class