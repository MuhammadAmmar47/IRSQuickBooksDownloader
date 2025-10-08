Imports IRSTaxRecords
Imports IRSTaxRecords.Core
Imports IRSTaxRecords.Core.Content

Public Class orderSSV
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If StoreInstance.IsUserLoggedIn = False Then
            Response.Redirect("~/Login.aspx?ReturnUrl=" & Server.UrlEncode(Request.RawUrl))
        End If
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If Not ValidateForm() Then Return
        Dim savedFilePath As String = Nothing
        If ssa89File.HasFile Then
            savedFilePath = order_4506.UploadFile(ssa89File.PostedFile, AppSettings.PDFSavePath)
            Dim file As New Core.Content.PDFFileUpload With {
                .UserID = StoreInstance.CurrentUserId,
                .PDFFileName = System.IO.Path.GetFileName(savedFilePath),
                .OriginalFileName = ssa89File.FileName,
                .UploadedOn = Now,
                .LoanNumber = Me.txtLoanNumber.Text.Trim()
            }
            PDFFileUploadServices.SavePDFFileUploaded(file)
        End If

        Dim resultOrderIDs As New Generic.List(Of Integer)
        Dim gender As String = ""
        If rbMale.Checked Then
            gender = rbMale.Value
        ElseIf rbFemale.Checked Then
            gender = rbFemale.Value
        End If
        Dim lst As New Core.Content.ListType

        ' Addition of ListType for OrderService SSV
        With lst
            .fldCurrentdate = Now.ToLongDateString
            .fldDateCheck = Now.ToShortDateString
            .fldListname = DateTime.Now.ToString("dddd, MMMM dd, yyyy")
            .fldlisttype = ListTypeCodeType.SSN
        End With
        ListServices.AddNewList(lst)

        Dim currentUser = StoreInstance.CurrentUser
        Dim o As New Orders.Order
        With o
            .fldCompanyID = StoreInstance.GetCustomerId()
            .fldcustomeriD = StoreInstance.GetCustomerId()
            .fldemail = "OFF"
            .fldfax = "OFF"
            .fldfaxno = ""
            .fldListid = lst.fldlistid
            .fldlisttype = ListTypeCodeType.SSN
            .fldLoanNumber = Me.txtLoanNumber.Text.Trim
            .fldOrderdate = Now.ToShortDateString
            .fldrequestname = txtTaxPayerName.Text.Trim()
            .fldsecondname = txtTaxPayerName.Text.Trim()
            .fldssnno = txtSocialSecurityNumber.Text.Trim()
            .fldstatus = "p"
            .fldPdf = ""
            .fldTaxyear2020 = Nothing
            .fldTaxyear2021 = Nothing
            .fldTaxyear2022 = Nothing
            .fldTaxyear2023 = Nothing
            .fldTaxyear2024 = Nothing
            .FormType = 0
            .fldordernumber = 0
            .fldDOB = If(String.IsNullOrEmpty(txtDob.Text), Nothing, txtDob.Text.Trim())
            .fldSex = gender
            .fldordertype = Orders.OrderType.Form_SSN
            .fldtypeofform = TypeOfForm.S_SSN
            OrderServices.CreateNewOrder(o)
            If o.fldordernumber < 1 Then
                lblMessage.Text = "Failed to save order. " & DataHelper.LastErrorMessage
            End If
            resultOrderIDs.Add(o.fldordernumber)
        End With

        Email.MailSender.SendOrderCreatedEmail(currentUser.Name, currentUser.Email, o.fldrequestname, "SSN", resultOrderIDs.ToSqlList)
        Response.Redirect("~/Confirmation.aspx")
    End Sub
    Private Function ValidateForm() As Boolean
        Dim msg As String = ""

        If txtTaxPayerName.Text.Trim = "" Then msg &= "Please enter Tax Payer Name.<br>"
        If txtSocialSecurityNumber.Text.Trim = "" Then msg &= "Please enter SSN.<br>"
        If txtDob.Text.Trim = "" Then msg &= "Please enter the DOB.<br>"
        If Me.txtLoanNumber.Visible AndAlso txtLoanNumber.Text.Trim = "" Then msg &= "Please enter Loan Number.<br>"
        If Not ssa89File.HasFile Then msg &= "Please attach a PDF file.<br>"

        If msg = "" Then
            Return True
        Else
            lblMessage.Text = msg
            Return False
        End If
    End Function

End Class