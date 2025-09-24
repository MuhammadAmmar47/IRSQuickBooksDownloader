Public Class ListServices
    Public Shared Function GetList(ByVal ListID As Integer) As Content.ListType

        Dim dt As DataTable = DataServices.List_GetList(ListID)
        If dt.Rows.Count = 0 Then Return Nothing
        Return DataRowToList(dt.Rows(0))
    End Function
    Public Shared Function UpdateList(ByVal list As Content.ListType) As Boolean
        Return DataServices.List_UpdateList(list.fldlistid, list.fldlisttype, list.fldListname, list.fldCurrentdate, list.fldDateCheck, list.IRSBatchNumber)
    End Function
    Public Shared Function AddNewList(ByVal lst As Content.ListType) As Boolean
        lst.fldlistid = DataServices.List_AddNewList(lst.fldlisttype, lst.fldListname, lst.fldCurrentdate, lst.fldDateCheck)
    End Function
    Public Shared Function GetCurrentListID(ByVal listType As ListTypeCodeType) As Integer
        Dim strQ As String = "select max(fldListid) as chkListid  from tbllist where fldListType=" & CInt(listType) & " and fldDateCheck='" & Now.ToShortDateString & "'"
        Return Val(DataHelper.ExecuteAndReadFirstValueTrimNull(strQ))
    End Function
    Public Shared Function GetListTypeFromFormType(ByVal formType As TypeOfForm) As ListTypeCodeType
        Select Case formType
            Case TypeOfForm.S_1040, TypeOfForm.S_1120, TypeOfForm.S_1065
                Return ListTypeCodeType.C_1040
            Case TypeOfForm.S_W2, TypeOfForm.S_1099
                Return ListTypeCodeType.W2
            Case Else
                Return ListTypeCodeType.SSN
        End Select
    End Function
    Private Shared Function DataRowToList(ByVal dr As DataRow) As Content.ListType
        Dim list As New Content.ListType
        With list
            If Not dr("fldlistid") Is DBNull.Value Then .fldlistid = dr("fldlistid")
            If Not dr("fldlisttype") Is DBNull.Value Then .fldlisttype = dr("fldlisttype")
            If Not dr("fldListname") Is DBNull.Value Then .fldListname = dr("fldListname")
            If Not dr("fldCurrentdate") Is DBNull.Value Then .fldCurrentdate = dr("fldCurrentdate")
            If Not dr("fldDateCheck") Is DBNull.Value Then .fldDateCheck = dr("fldDateCheck")
            If Not dr("IRSBatchNumber") Is DBNull.Value Then .IRSBatchNumber = dr("IRSBatchNumber")
        End With
        Return list
    End Function
End Class
