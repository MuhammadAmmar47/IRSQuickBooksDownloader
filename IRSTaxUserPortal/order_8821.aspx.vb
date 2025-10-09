Imports System.IO
Imports IRSTaxRecords
Imports IRSTaxRecords.Core

Public Class order_8821
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If StoreInstance.IsUserLoggedIn = False Then
            Response.Redirect("~/Login.aspx?ReturnUrl=" & Server.UrlEncode(Request.RawUrl))
        End If
    End Sub
    Public Shared Function UploadFile(file As HttpPostedFile, uploadFolderPath As String) As String
        If file Is Nothing OrElse file.ContentLength = 0 Then
            Return Nothing
        End If

        Try
            Dim serverPath As String = HttpContext.Current.Server.MapPath(uploadFolderPath)
            If Not Directory.Exists(serverPath) Then
                Directory.CreateDirectory(serverPath)
            End If

            Dim extension As String = Path.GetExtension(file.FileName)
            Dim newFileName As String = DateTime.Now.ToString("yyyyMMddHHmmss") & "_" & Guid.NewGuid().ToString() & extension


            Dim filePath As String = Path.Combine(serverPath, newFileName)
            file.SaveAs(filePath)

            Return filePath
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function ValidateForm() As Boolean
        Dim msg As String = ""

        If txtTaxPayerName.Text.Trim = "" Then msg &= "Please enter Tax Payer Name.<br>"
        If txtSocialSecurityNumber.Text.Trim = "" Then msg &= "Please enter SSN.<br>"
        If Me.txtLoanNumber.Visible AndAlso txtLoanNumber.Text.Trim = "" Then msg &= "Please enter Loan Number.<br>"
        If SelectedIDs(Me.chkTaxyears).Count = 0 Then msg &= "Please select at least one year to order.<br>"
        If Me.chkTaxForms.Items.Count = 0 Then msg &= "Please select at least one form type.<br>"
        If Not fuform8821.HasFile Then msg &= "Please attach a PDF file.<br>"

        If msg = "" Then
            Return True
        Else
            lblMessage.Text = msg
            Return False
        End If
    End Function

    Public Class FormsToAdd
        Public Property FormType As TypeOfForm
        Public Property RecordOfAccount As Boolean = False
        Public Property AccountTranscript As Boolean = False
        Public Sub New()
        End Sub
        Public Sub New(ftype As TypeOfForm, RA As Boolean, AT As Boolean)
            MyBase.New
            FormType = ftype
            RecordOfAccount = RA
            AccountTranscript = AT
        End Sub
    End Class
    Private Function GetTypeOfFormsSelected() As List(Of FormsToAdd)
        Dim list As New List(Of FormsToAdd)

        For Each item As ListItem In chkTaxForms.Items
            If item.Selected Then
                Dim forms As New List(Of TypeOfForm)()

                Select Case item.Value
                    Case "1040"
                        list.Add(New FormsToAdd(TypeOfForm.S_1040, False, False))
                    Case "1040R"
                        list.Add(New FormsToAdd(TypeOfForm.S_1040, False, False))
                        list.Add(New FormsToAdd(TypeOfForm.S_1040, True, False))
                    Case "AT"
                        list.Add(New FormsToAdd(TypeOfForm.S_1040, False, True))
                    Case "ROA"
                        list.Add(New FormsToAdd(TypeOfForm.S_1040, True, False))
                    Case "1040/W2"
                        list.Add(New FormsToAdd(TypeOfForm.S_1040, False, False))
                        list.Add(New FormsToAdd(TypeOfForm.S_W2, False, False))
                    Case "1120"
                        list.Add(New FormsToAdd(TypeOfForm.S_1120, False, False))
                    Case "1065"
                        list.Add(New FormsToAdd(TypeOfForm.S_1065, False, False))
                    Case "W-2"
                        list.Add(New FormsToAdd(TypeOfForm.S_W2, False, False))
                    Case "1099"
                        list.Add(New FormsToAdd(TypeOfForm.S_1099, False, False))
                    Case Else
                        Throw New Exception("Form type " & item.Value & " is not a valid form type.")
                End Select
            End If
        Next

        Return list
    End Function


    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If Not ValidateForm() Then Return
        Dim savedFilePath As String = Nothing
        If fuform8821.HasFile Then
            savedFilePath = UploadFile(fuform8821.PostedFile, AppSettings.PDFSavePath)
            Dim file As New Core.Content.PDFFileUpload With {
                .UserID = StoreInstance.CurrentUserId,
                .PDFFileName = System.IO.Path.GetFileName(savedFilePath),
                .OriginalFileName = fuform8821.FileName,
                .UploadedOn = Now,
                .LoanNumber = Me.txtLoanNumber.Text.Trim()
            }
            PDFFileUploadServices.SavePDFFileUploaded(file)
        End If
        Dim years As Generic.List(Of Integer) = SelectedIDs(chkTaxyears)

        Dim resultOrderIDs As New Generic.List(Of Integer)
        Dim typeOfForms = GetTypeOfFormsSelected()

        Dim currentUser = StoreInstance.CurrentUser

        For Each frm In typeOfForms
            Dim o As New Orders.Order
            With o
                .fldCompanyID = StoreInstance.GetCustomerId()
                .fldcustomeriD = StoreInstance.GetCustomerId()
                .fldemail = "OFF"
                .fldfax = "OFF"
                .fldfaxno = ""
                .fldLoanNumber = Me.txtLoanNumber.Text.Trim
                .fldOrderdate = Now.ToShortDateString
                .fldrequestname = txtTaxPayerName.Text.Trim()
                .fldssnno = txtSocialSecurityNumber.Text.Trim()
                .fldstatus = "p"
                .fldPdf = ""

                ' assign tax years
                For Each Year As Integer In years
                    Select Case Year
                        Case 2020 : .fldTaxyear2020 = True
                        Case 2021 : .fldTaxyear2021 = True
                        Case 2022 : .fldTaxyear2022 = True
                        Case 2023 : .fldTaxyear2023 = True
                        Case 2024 : .fldTaxyear2024 = True
                    End Select
                Next

                ' form type & list info
                Dim listType As ListTypeCodeType = ListServices.GetListTypeFromFormType(frm.FormType)
                Dim currentListID = ListServices.GetCurrentListID(listType)
                Dim lst As Core.Content.ListType = Nothing
                If currentListID > 0 Then
                    lst = ListServices.GetList(currentListID)
                Else
                    ' Addition of ListType for OrderService SSV
                    lst = New Content.ListType
                    With lst
                        .fldCurrentdate = Now.ToLongDateString
                        .fldDateCheck = Now.ToShortDateString
                        .fldListname = DateTime.Now.ToString("dddd, MMMM dd, yyyy")
                        .fldlisttype = listType
                    End With
                    ListServices.AddNewList(lst)
                End If

                .fldListid = lst.fldlistid
                .fldlisttype = CInt(listType)
                .FormType = frm.FormType
                .fldordernumber = 0
                .fldordertype = Orders.OrderType.Form_8821
                .fldtypeofform = TypeOfForm.S_8821

                If frm.AccountTranscript Then
                    .fldrequestname = $"{txtTaxPayerName.Text.Trim()} 8821 AT"
                ElseIf frm.RecordOfAccount Then
                    .fldrequestname = $"{txtTaxPayerName.Text.Trim()} 8821 ROA"
                Else
                    .fldrequestname = $"{txtTaxPayerName.Text.Trim()} 8821"
                End If

                ' save order
                OrderServices.CreateNewOrder(o)
                If o.fldordernumber < 1 Then
                    lblMessage.Text = "Failed to save order. " & DataHelper.LastErrorMessage
                End If
                resultOrderIDs.Add(o.fldordernumber)
            End With
        Next

        Email.MailSender.SendOrderCreatedEmail(currentUser.Name, currentUser.Email, txtTaxPayerName.Text, "8821", resultOrderIDs.ToSqlList)
        Email.MailSender.SendOrderCreatedEmailToAdmin(currentUser.Name, currentUser.UserID, txtTaxPayerName.Text, "8821", resultOrderIDs.ToSqlList, txtLoanNumber.Text.Trim)
        Response.Redirect("~/Confirmation.aspx?form=" & 8821)
    End Sub

    Private Function SelectedIDs(ByVal chk As CheckBoxList) As Generic.List(Of Integer)
        Dim list As New Generic.List(Of Integer)
        For Each item As ListItem In chk.Items
            If item.Selected Then list.Add(item.Value)
        Next
        Return list
    End Function

End Class