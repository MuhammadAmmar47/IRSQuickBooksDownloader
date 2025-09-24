'Public Class DownloadFunctions
'    Private data As SQLServerDataHelper = Nothing
'    Public Sub New()
'        data = New SQLServerDataHelper(AppSettings.ConnectionString)
'    End Sub

'    Public Function GetPaidInvoices() As DataTable
'        Dim req As New DataRequest
'        req.CommandType = CommandType.StoredProcedure
'        req.Command = "usp_PaidInvoices"

'        Return data.ExecuteDataSet(req).Tables(0)
'    End Function

'    Public Function MarkInvoiceUpdated(ByVal invoiceId As Integer) As Boolean
'        Dim req As New DataRequest
'        req.CommandType = CommandType.Text
'        req.Command = "UPDATE QB_Invoices SET bUpdatedInQB=1 Where ID=" & invoiceId

'        If data.ExecuteNonQuery(req) > 0 Then
'            Return True
'        Else
'            Return False
'        End If
'    End Function

'    Public Function GetInvoice(ByVal id As Integer) As Invoice
'        Dim req As New DataRequest
'        req.CommandType = CommandType.Text
'        req.Command = "SELECT dbo.QB_Invoices.*, dbo.QB_Payments.PaidOn AS PaidOn, dbo.QB_Payments.PaymentMethod AS PaymentMethod "
'        req.Command += " FROM dbo.QB_Invoices LEFT OUTER JOIN dbo.QB_Payments ON dbo.QB_Invoices.PaymentId = dbo.QB_Payments.id "
'        req.Command += " WHERE dbo.QB_Invoices.id = " & id

'        Dim dt As DataTable = data.ExecuteQuery(req)

'        Return DataTableToInvoice(dt)
'    End Function

'    Private Function DataTableToInvoice(ByVal dt As DataTable) As Invoice
'        If dt.Rows.Count = 0 Then Return Nothing

'        Dim inv As New Invoice
'        With inv

'            .AccountRef = dt.Rows(0)("AccountRef")
'            .AppliedAmount = dt.Rows(0)("AppliedAmount")
'            .BalanceRemaining = dt.Rows(0)("BalanceRemaining")
'            .ClassRefName = dt.Rows(0)("ClassRefName")
'            .CreatedOn = dt.Rows(0)("CreatedOn")
'            .CustomerId = dt.Rows(0)("CustomerID")
'            .DueDate = dt.Rows(0)("DueDate")
'            .Id = dt.Rows(0)("ID")
'            .InvoiceNumber = dt.Rows(0)("InvoiceNumber")
'            .IsPaid = dt.Rows(0)("IsPaid")
'            .ModifiedOn = dt.Rows(0)("ModifiedOn")
'            .SalesTaxTotal = dt.Rows(0)("SalesTaxTotal")
'            .SubTotal = dt.Rows(0)("SubTotal")
'            .TransactionDate = dt.Rows(0)("TransactionDate")
'            .TransactionID = dt.Rows(0)("TransactionId")
'            .TransactionNo = dt.Rows(0)("TransactionNo")

'            .PaymentTypeString = ""
'            If Not dt.Rows(0)("ReferenceNumber") Is DBNull.Value Then .PaymentReferenceNumber = dt.Rows(0)("ReferenceNumber")
'            If Not dt.Rows(0)("bUpdatedInQB") Is DBNull.Value Then .IsUpdatedInQuickBooks = dt.Rows(0)("bUpdatedInQB")
'            If Not dt.Rows(0)("PaymentId") Is DBNull.Value Then .PaymentID = dt.Rows(0)("PaymentId")

'            If Not dt.Rows(0)("PaidOn") Is DBNull.Value Then .PaidOn = dt.Rows(0)("PaidOn")

'            .CustomerListID = DataServices.GeneralFunctions.GetCustomer(.CustomerId).ListId

'            If Not dt.Rows(0)("PaymentMethod") Is DBNull.Value Then
'                If CType(dt.Rows(0)("PaymentMethod"), Payments.PaymentType) = Payments.PaymentType.eCheck Then
'                    .PaymentTypeString = "Cash"
'                Else
'                    'Get credit carad type
'                    .PaymentTypeString = DataServices.GeneralFunctions.CreditCard_GetByContactId(.CustomerId).CardType.ToString.Replace("_", " ")
'                End If
'            Else
'                .PaymentTypeString = ""
'            End If
'            '.AccountRef = dt.Rows(0)("AccountRef")
'            '.AppliedAmount = dt.Rows(0)("AppliedAmount")
'            '.BalanceRemaining = dt.Rows(0)("BalanceRemaining")
'            '.ClassRefName = dt.Rows(0)("ClassRefName")
'            '.CreatedOn = dt.Rows(0)("CreatedOn")
'            '.CustomerId = dt.Rows(0)("CustomerID")
'            '.DueDate = dt.Rows(0)("DueDate")
'            '.Id = dt.Rows(0)("ID")
'            '.InvoiceNumber = dt.Rows(0)("InvoiceNumber")
'            '.IsPaid = dt.Rows(0)("IsPaid")
'            '.ModifiedOn = dt.Rows(0)("ModifiedOn")
'            '.SalesTaxTotal = dt.Rows(0)("SalesTaxTotal")
'            '.SubTotal = dt.Rows(0)("SubTotal")
'            '.TransactionDate = dt.Rows(0)("TransactionDate")
'            '.TransactionID = dt.Rows(0)("TransactionId")
'            '.TransactionNo = dt.Rows(0)("TransactionNo")

'            '.PaymentTypeString = ""
'            'If Not dt.Rows(0)("ReferenceNumber") Is DBNull.Value Then .PaymentReferenceNumber = dt.Rows(0)("ReferenceNumber")
'            'If Not dt.Rows(0)("bUpdatedInQB") Is DBNull.Value Then .IsUpdatedInQuickBooks = dt.Rows(0)("bUpdatedInQB")
'            'If Not dt.Rows(0)("PaymentId") Is DBNull.Value Then .PaymentID = dt.Rows(0)("PaymentId")
'        End With

'        Return inv
'    End Function
'End Class
