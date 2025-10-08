Imports System.IO
Imports IRSTaxRecords
Imports IRSTaxRecords.Core

Public Class order_8821
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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

    Private Function GetTypeOfFormsSelected() As Dictionary(Of String, List(Of TypeOfForm))
        Dim typeOfFormsMap As New Dictionary(Of String, List(Of TypeOfForm))()

        For Each item As ListItem In chkTaxForms.Items
            If item.Selected Then
                Dim forms As New List(Of TypeOfForm)()

                Select Case item.Value
                    Case "1040"
                        forms.Add(TypeOfForm.S_1040)
                    Case "1040R"
                        forms.Add(TypeOfForm.S_1040)
                        forms.Add(TypeOfForm.S_1040)
                    Case "AT"
                        forms.Add(TypeOfForm.S_1040)
                    Case "ROA"
                        forms.Add(TypeOfForm.S_1040)
                    Case "1040/W2"
                        forms.Add(TypeOfForm.S_1040)
                        forms.Add(TypeOfForm.S_W2)
                    Case "1120"
                        forms.Add(TypeOfForm.S_1120)
                    Case "1065"
                        forms.Add(TypeOfForm.S_1065)
                    Case "W-2"
                        forms.Add(TypeOfForm.S_W2)
                    Case "1099"
                        forms.Add(TypeOfForm.S_1099)
                    Case Else
                        Throw New Exception("Form type " & item.Value & " is not a valid form type.")
                End Select

                typeOfFormsMap.Add(item.Value, forms)
            End If
        Next

        Return typeOfFormsMap
    End Function


    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If Not ValidateForm() Then Return
        Dim savedFilePath As String = Nothing
        If fuform8821.HasFile Then
            savedFilePath = UploadFile(fuform8821.PostedFile, "~/Uploads/")
        End If
        Dim years As Generic.List(Of Integer) = SelectedIDs(chkTaxyears)

        Dim resultOrderIDs As New Generic.List(Of Integer)
        Dim typeOfForms = GetTypeOfFormsSelected()
        Dim loopIndex As Integer = 1

        For Each kvp In typeOfForms   ' kvp.Key = checkbox value, kvp.Value = List(Of TypeOfForm)
            For Each formType As TypeOfForm In kvp.Value
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
                    .fldPdf = System.IO.Path.GetFileName(savedFilePath)

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
                    Dim listType As ListTypeCodeType = ListServices.GetListTypeFromFormType(formType)
                    Dim lst As New Core.Content.ListType

                    ' Addition of ListType for OrderService SSV
                    With lst
                        .fldCurrentdate = Now.ToLongDateString
                        .fldDateCheck = Now.ToShortDateString
                        .fldListname = DateTime.Now.ToString("dddd, MMMM dd, yyyy")
                        .fldlisttype = listType
                    End With
                    ListServices.AddNewList(lst)

                    .fldListid = lst.fldlistid
                    .fldlisttype = CInt(listType)
                    .FormType = formType
                    .fldordernumber = 0
                    .fldordertype = Orders.OrderType.Form_8821
                    .fldtypeofform = TypeOfForm.S_8821
                    ' set second name based on rules
                    If formType = TypeOfForm.S_1040 AndAlso loopIndex = 2 Then
                        .fldsecondname = txtTaxPayerName.Text.Trim() & " ROA"
                    ElseIf kvp.Key = "ROA" Then
                        .fldsecondname = txtTaxPayerName.Text.Trim() & " ROA"
                    ElseIf kvp.Key = "AT" Then
                        .fldsecondname = txtTaxPayerName.Text.Trim() & " AT"
                    Else
                        .fldsecondname = txtTaxPayerName.Text.Trim()
                    End If

                    ' save order
                    OrderServices.CreateNewOrder(o)
                    If o.fldordernumber < 1 Then
                        lblMessage.Text = "Failed to save order. " & DataHelper.LastErrorMessage
                    End If
                    resultOrderIDs.Add(o.fldordernumber)
                    loopIndex += 1
                End With
            Next
        Next

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