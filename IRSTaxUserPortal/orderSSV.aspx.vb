Imports IRSTaxRecords.Core

Public Class orderSSV
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        'If Not ValidateForm() Then Return
        Dim savedFilePath As String = Nothing
        If ssa89File.HasFile Then
            savedFilePath = order_4506.UploadFile(ssa89File.PostedFile, "~/Uploads/")
        End If

        Dim resultOrderIDs As New Generic.List(Of Integer)
        Dim gender As String = ""
        If rbMale.Checked Then
            gender = rbMale.Value
        ElseIf rbFemale.Checked Then
            gender = rbFemale.Value
        End If
        Dim o As New Orders.Order
        With o
            .fldCompanyID = StoreInstance.GetCustomerId()
            .fldcustomeriD = StoreInstance.GetCustomerId()
            .fldemail = "OFF"
            .fldfax = "OFF"
            .fldfaxno = ""
            .fldListid = 0
            .fldlisttype = 1
            .fldLoanNumber = Me.txtLoanNumber.Text.Trim
            .fldOrderdate = Now.ToShortDateString
            .fldrequestname = txtTaxPayerName.Text.Trim()
            .fldssnno = txtSocialSecurityNumber.Text.Trim()
            .fldstatus = "p"
            .fldPdf = ssa89File.PostedFile.FileName
            .fldTaxyear2020 = Nothing
            .fldTaxyear2021 = Nothing
            .fldTaxyear2022 = Nothing
            .fldTaxyear2023 = Nothing
            .fldTaxyear2024 = Nothing
            .fldListid = 0
            .fldlisttype = 0
            .FormType = 0
            .fldordernumber = 0
            .fldDOB = If(String.IsNullOrWhiteSpace(txtDob.Text), Nothing, txtDob.Text.Trim())
            .fldSex = gender

            OrderServices.CreateNewOrder(o)
                If o.fldordernumber < 1 Then
                    'msg.ShowError("Failed to save order. " & DataHelper.LastErrorMessage)
                End If
            resultOrderIDs.Add(o.fldordernumber)
        End With

        Response.Redirect("~/Confirmation.aspx")
        'msg.ShowInformation(resultOrderIDs.Count & " Orders created. Order numbers are " & Utilities.Translators.GenericArrayToString(resultOrderIDs))
    End Sub

End Class