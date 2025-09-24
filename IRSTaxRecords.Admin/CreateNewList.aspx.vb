Public Partial Class CreateNewList
    Inherits System.Web.UI.Page

    Public ReadOnly Property ListType() As Integer
        Get
            Return Val(Request.QueryString("ListType"))
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        msg.Clear()

        If Not Page.IsPostBack Then
            If ListType < 1 Then
                msg.ShowError("Invalid list type.")
                Me.lblListTypeName.Text = "Invalid list type"
                Me.btnNewList.Enabled = False
                Return
            End If



            Dim dt As DataTable = DataHelper.ExecuteQuery("SELECT COUNT(*) as Total from tbllist  WHERE fldlisttype = " & ListType & " AND fldDateCheck = '" & Now.ToShortDateString & "'")
            Dim totalListsToday As Integer = dt.Rows(0)("Total")

            Dim listName As String = Now.ToLongDateString
            If totalListsToday > 0 Then listName += " " & totalListsToday + 1
            listName += " ODGEN"

            Me.txtListName.Text = listName


            Select Case CType(ListType, Core.ListTypeCodeType)
                Case ListTypeCodeType.C_1040
                    Me.lblListTypeName.Text = "1040 List Name:"
                Case ListTypeCodeType.Special
                    Me.lblListTypeName.Text = "Specials List Name:"
                Case ListTypeCodeType.SSN
                    Me.lblListTypeName.Text = "SSN List Name:"
                Case ListTypeCodeType.W2
                    Me.lblListTypeName.Text = "W2 List Name:"
                Case Else
                    msg.ShowError("Invalid list type " & ListType.ToString)
                    Me.btnNewList.Enabled = False
                    Me.lblListTypeName.Text = "Unknown list type"
            End Select

        End If
    End Sub

    Private Sub btnNewList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewList.Click
        If Me.txtListName.Text.Trim = "" Then
            msg.ShowError("Please enter list name")
            Return
        End If

        Dim lst As New Core.Content.ListType
        With lst
            .fldCurrentdate = Now.ToLongDateString
            .fldDateCheck = Now.ToShortDateString
            .fldListname = Me.txtListName.Text.Trim
            .fldlisttype = ListType
        End With
        ListServices.AddNewList(lst)
        Session("actionMsg") = "List with id " & lst.fldlistid & " was created successfully"
        Response.Redirect("ManageList.aspx?ListType=" & ListType)
    End Sub
End Class