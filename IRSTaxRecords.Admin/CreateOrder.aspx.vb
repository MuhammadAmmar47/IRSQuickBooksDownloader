Public Partial Class CreateOrder
    Inherits System.Web.UI.Page

    Private ReadOnly Property UserId() As Integer
        Get
            Return Val(Request.QueryString("UserID"))
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        msg.Clear()

        If Not Page.IsPostBack Then
            If UserId > 0 Then
                Dim u As Customer = DataServices.GetCustomer(UserId)
                If u Is Nothing Then
                    msg.ShowError("Customer with id " & UserId & " doesn't exists")
                    Me.btnCreateOrder.Enabled = False
                    Return
                End If

                Me.lblOrderSummary.Text = ""
                Me.lblUserId.Text = u.UserID
                Me.lblUserName.Text = u.Name & " of " & u.CompanyName
                If u.Addloannumber = 2 Then
                    trLoanNumber.Visible = True
                Else
                    trLoanNumber.Visible = False
                End If
                If u.Approved = 2 Then
                    msg.ShowError("This user/company is suspended. Can't create order")
                    Me.btnCreateOrder.Enabled = False
                End If
            End If
            
        End If
    End Sub
    Private Function SelectedIDs(ByVal chk As CheckBoxList) As Generic.List(Of Integer)
        Dim list As New Generic.List(Of Integer)
        For Each item As ListItem In chk.Items
            If item.Selected Then list.Add(item.Value)
        Next
        Return list
    End Function
    Private Function ValidateForm() As Boolean
        Dim msg As String = ""
        If UserId < 1 Then msg += "No user selected. Use textbox to search user to create order for.<br>"
        If Me.txtLoanNumber.Visible AndAlso txtLoanNumber.Text.Trim = "" Then msg += "Please enter loan number.<br>"
        If txtName.Text.Trim = "" Then msg += "Please enter request name.<br>"
        If txtSSN.Text.Trim = "" Then msg += "Please enter SSN.<br>"
        If SelectedIDs(Me.chkTaxyears).Count = 0 Then msg += "Please select at least one year to order.<br>"
        If Me.rdoTaxForms.SelectedItem Is Nothing Then msg += "Please select at least one form type.<br>"

        Dim typeOfForms As Generic.List(Of TypeOfForm) = GetTypeOfFormsSelected()
        For Each formtype As TypeOfForm In typeOfForms
            Dim listType As ListTypeCodeType = ListServices.GetListTypeFromFormType(formtype)
            Dim listID As Integer = ListServices.GetCurrentListID(listType)
            If listID < 1 Then
                msg += "There is no current list for form type " & formtype.ToString & ", ListType=" & listType.ToString & "<br>"
            End If
        Next


        If msg = "" Then Return True
        Me.msg.ShowError(msg)
        Return False
    End Function
    Private Function GetTypeOfFormsSelected() As Generic.List(Of TypeOfForm)
        Dim typeOfForms As New Generic.List(Of TypeOfForm)
        Select Case Me.rdoTaxForms.SelectedValue
            Case "1040"
                typeOfForms.Add(TypeOfForm.S_1040)
            Case "1040/W2"
                typeOfForms.Add(TypeOfForm.S_1040)
                typeOfForms.Add(TypeOfForm.S_W2)
            Case "1120"
                typeOfForms.Add(TypeOfForm.S_1120)
            Case "1065"
                typeOfForms.Add(TypeOfForm.S_1065)
            Case "W-2"
                typeOfForms.Add(TypeOfForm.S_W2)
            Case "1099"
                typeOfForms.Add(TypeOfForm.S_1099)
            Case Else
                Throw New Exception("Form type " & rdoTaxForms.SelectedValue & " is not a valid form type.")
        End Select
        Return typeOfForms
    End Function
    Private Sub btnCreateOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateOrder.Click
        If Not ValidateForm() Then Return

        Dim years As Generic.List(Of Integer) = SelectedIDs(chkTaxyears)


        Dim u As Core.Customer = DataServices.GetCustomer(UserId)

        Dim resultOrderIDs As New Generic.List(Of Integer)
        Dim typeOfForms As Generic.List(Of TypeOfForm) = GetTypeOfFormsSelected()
        Dim o As New Orders.Order
        With o
            .fldCompanyID = u.CustomerID
            .fldcustomeriD = u.CustomerID
            If Me.chkEmail.Checked Then
                .fldemail = "ON"
            Else
                .fldemail = "OFF"
            End If

            If Me.chkFax.Checked Then
                .fldfax = "ON"
            Else
                .fldfax = "OFF"
            End If

            .fldfaxno = ""
            .fldListid = 0
            .fldlisttype = 1
            .fldLoanNumber = Me.txtLoanNumber.Text.Trim
            .fldOrderdate = Now.ToShortDateString
            .fldrequestname = Me.txtName.Text.Trim
            .fldssnno = txtSSN.Text.Trim
            .fldstatus = "p"
            For Each Year As Integer In years
                Select Case Year
                    Case 2000 : .fldtaxyear2000 = True
                    Case 2001 : .fldtaxyear2001 = True
                    Case 2002 : .fldtaxyear2002 = True
                    Case 2003 : .fldtaxyear2003 = True
                    Case 2004 : .fldTaxyear2004 = True
                    Case 2005 : .fldTaxyear2005 = True
                    Case 2006 : .fldTaxyear2006 = True
                    Case 2007 : .fldTaxyear2007 = True
                    Case 2008 : .fldTaxyear2008 = True
                    Case 2009 : .fldTaxyear2009 = True
                    Case 2010 : .fldTaxyear2010 = True
                    Case 2011 : .fldTaxyear2011 = True
                    Case 2012 : .fldTaxyear2012 = True
                    Case 2013 : .fldTaxyear2013 = True
                    Case 2014 : .fldTaxyear2014 = True
                End Select
            Next
            For Each formType As TypeOfForm In typeOfForms
                Dim listType As ListTypeCodeType = ListServices.GetListTypeFromFormType(formType)

                Dim listID As Integer = ListServices.GetCurrentListID(listType)

                .fldListid = listID
                .fldlisttype = CInt(listType)
                .FormType = formType
                .fldordernumber = 0

                OrderServices.CreateNewOrder(o)
                If o.fldordernumber < 1 Then
                    msg.ShowError("Failed to save order. " & DataHelper.LastErrorMessage)
                End If
                resultOrderIDs.Add(o.fldordernumber)
            Next
        End With


        msg.ShowInformation(resultOrderIDs.Count & " Orders created. Order numbers are " & Utilities.Translators.GenericArrayToString(resultOrderIDs))
    End Sub

    Private Sub btnSearchUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchUser.Click
        Dim strQ As String = "SELECT CustomerID FROM Customer "
        strQ += " WHERE UserID='" & txtUserName.Text.Replace("'", "''") & "'"
        If IsNumeric(txtUserName.Text) Then strQ += " OR CustomerID=" & CInt(txtUserName.Text)
        strQ += " OR CompanyName = '" & txtUserName.Text.Replace("'", "''") & "'"
        Dim dt As DataTable = DataHelper.ExecuteQuery(strQ)
        If dt.Rows.Count = 1 Then
            Response.Redirect("CreateOrder.aspx?UserID=" & dt.Rows(0)("CustomerID"))
        Else
            Response.Redirect("SearchCustomer.aspx?SearchFor=" & txtUserName.Text.Trim)
        End If
    End Sub
End Class