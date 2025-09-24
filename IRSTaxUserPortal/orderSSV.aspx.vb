Imports IRSTaxRecords.Core

Public Class orderSSV
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim savedFilePath As String = Nothing
        If ssa89File.HasFile Then
            savedFilePath = order_4506.UploadFile(ssa89File.PostedFile, "~/Uploads/")
        End If

        ' Collect form values
        Dim requestName As String = txtTaxPayerName.Text.Trim()
        Dim ssnNumber As String = txtSocialSecurityNumber.Text.Trim()
        Dim loanNumber As String = txtLoanNumber.Text.Trim()
        Dim dob As String = If(String.IsNullOrWhiteSpace(txtDob.Text), Nothing, txtDob.Text.Trim())
        Dim gender As String = ""
        If rbMale.Checked Then
            gender = rbMale.Value
        ElseIf rbFemale.Checked Then
            gender = rbFemale.Value
        End If
        ' Tax year checkboxes
        Dim taxYear2024 As String = Nothing
        Dim taxYear2023 As String = Nothing
        Dim taxYear2022 As String = Nothing
        Dim taxYear2021 As String = Nothing
        Dim taxYear2020 As String = Nothing

        Dim newListId As Integer = 1
        Dim listType As Integer = 1
        Dim typeOfForm As String = "SSA-89"
        Dim email As String = ""
        Dim fax As String = ""
        Dim faxNo As String = ""
        Dim orderDate As DateTime = DateTime.Now
        Dim customerId As Integer = StoreInstance.GetCustomerId()
        Dim companyId As Integer = StoreInstance.GetCustomerId()
        ' Insert into DB
        order_4506.InsertOrder(newListId, listType, customerId, requestName, ssnNumber,
                taxYear2024, taxYear2023, taxYear2022, taxYear2021, taxYear2020,
                typeOfForm, email, fax, faxNo, orderDate, companyId, loanNumber, ssa89File.PostedFile.FileName, dob, gender
                )

        Response.Redirect("~/Confirmation.aspx")
    End Sub

End Class