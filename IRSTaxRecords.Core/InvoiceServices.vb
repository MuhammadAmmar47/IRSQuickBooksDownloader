Public Class InvoiceServices


    Public Shared Function GetPendingInvoicesOfCompany(ByVal dateFrom As DateTime, ByVal dateTo As DateTime, ByVal CompanyId As Integer) As Integer()
        'Return DataServices.Invoics_GetCompanyInvoices(dateFrom, dateTo, CompanyId)
        Dim dt As DataTable = DataServices.Invoics_GetCompanyInvoices(dateFrom, dateTo, CompanyId)
        Dim arrList As New ArrayList()
        For temp As Integer = 0 To dt.Rows.Count - 1
            arrList.Add(dt.Rows(temp)("fldordernumber"))
        Next

        Return DirectCast(arrList.ToArray(GetType(Integer)), Integer())
    End Function

    Public Shared Function GetCompaniesWithPendingInvoices(ByVal dateFrom As DateTime, ByVal dateTo As DateTime) As Integer()

        Dim dt As DataTable = DataServices.Invoices_GetCompaniesWithPendingInvoices(dateFrom, dateTo)
        Dim arrList As New ArrayList()
        For temp As Integer = 0 To dt.Rows.Count - 1
            arrList.Add(dt.Rows(temp)("companyid"))
        Next

        Return DirectCast(arrList.ToArray(GetType(Integer)), Integer())
    End Function
    Public Shared Function GetInvoice(ByVal OrderNumber As Integer) As Invoice
        Return DataServices.Invoices_GetInvoice(OrderNumber)
    End Function
    Public Shared Function GetInvoicesByListID(ByVal ListID As String) As Generic.List(Of Invoice)
        Return DataServices.Invoices_GetInvoicesByListID(ListID)
    End Function

    Public Shared Function GetInvoicesByCustomerName(ByVal CustomerName As String, ByVal orderDateFrom As DateTime, ByVal orderDateTo As DateTime) As Generic.List(Of Invoice)
        Return DataServices.Invoices_GetInvoicesByCustomerName(CustomerName, orderDateFrom, orderDateTo)
    End Function


    Public Shared Function MarkInvoiceUpdatedInQB(ByVal OrderNumber As Integer, ByVal ListID As String, ByVal QBBatchNumber As Integer) As Boolean
        Return DataServices.Invoices_MarkInvoiceUpdated(OrderNumber, ListID, QBBatchNumber)
    End Function

    Public Shared Function GetLastBatchNumber() As Integer
        Return DataServices.Invoices_GetLastBatchNumber()
    End Function
    Public Shared Function RevertLastBatchNumber() As Integer
        Dim lastBatchNumber As Integer = DataServices.Invoices_GetLastBatchNumber()
        Return DataServices.Invoices_RevertBatchNumber(lastBatchNumber)
    End Function
End Class
