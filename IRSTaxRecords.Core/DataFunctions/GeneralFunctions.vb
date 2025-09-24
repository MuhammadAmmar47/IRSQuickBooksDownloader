'Public Class GeneralFunctions
'    Private data As SQLServerDataHelper = Nothing
'    Public Sub New()
'        data = New SQLServerDataHelper(AppSettings.ConnectionString)
'    End Sub

'    '#Region "Locally used"
'    '    Protected Function SelectDataSetByID(ByVal id As Integer, ByVal storedProcName As String) As DataSet
'    '        Dim request As New DataRequest
'    '        request.Command = storedProcName
'    '        request.CommandType = CommandType.StoredProcedure
'    '        request.AddParameter("@ID", id)
'    '        request.Transactional = False
'    '        Dim result As DataSet
'    '        result = data.ExecuteDataSet(request)
'    '        Return result
'    '    End Function
'    '    Protected Function SelectByID(ByVal id As Integer, ByVal storedProcName As String) As DataTable
'    '        Dim dtResult As New DataTable("Results")
'    '        Dim tempDS As DataSet = SelectDataSetByID(id, storedProcName)
'    '        If Not tempDS Is Nothing Then
'    '            If tempDS.Tables.Count() > 0 Then
'    '                dtResult = tempDS.Tables(0)
'    '            End If
'    '        End If
'    '        tempDS = Nothing
'    '        Return dtResult
'    '    End Function
'    '#End Region

'    '#Region "Customers/Invoiecs"
'    '    Public Function GetCustomer(ByVal id As Integer) As Customer
'    '        Dim strQ As String = "Select * From QB_Customers Where ID=@ID"
'    '        Dim req As New DataRequest
'    '        req.Command = strQ
'    '        req.CommandType = CommandType.Text
'    '        req.AddParameter("@ID", id)
'    '        Dim dt As DataTable = data.ExecuteQuery(req)

'    '        If dt.Rows.Count = 0 Then Return Nothing

'    '        'Prepare the user object
'    '        Return ConvertCustomerTableToCustomerObject(dt)
'    '    End Function
'    '    Public Function GetCustomer(ByVal UserID As String) As Customer
'    '        Dim strQ As String = "Select * From QB_Customers Where UserId=@UserId"
'    '        Dim req As New DataRequest
'    '        req.Command = strQ
'    '        req.CommandType = CommandType.Text
'    '        req.AddParameter("@UserID", UserID)

'    '        Dim dt As DataTable = data.ExecuteQuery(req)

'    '        If dt.Rows.Count = 0 Then Return Nothing

'    '        'Prepare the user object
'    '        Return ConvertCustomerTableToCustomerObject(dt)
'    '    End Function
'    '    Private Function ConvertCustomerTableToCustomerObject(ByVal dt As DataTable) As Customer
'    '        Dim c As New Customer
'    '        With c
'    '            .ID = dt.Rows(0)("ID")
'    '            .Address1 = dt.Rows(0)("Address1")
'    '            .Address2 = dt.Rows(0)("Address2")
'    '            .City = dt.Rows(0)("City")
'    '            .CreatedOn = dt.Rows(0)("CreatedOn")
'    '            .FirstName = dt.Rows(0)("FirstName")
'    '            .LastName = dt.Rows(0)("LastName")
'    '            .ListId = dt.Rows(0)("ListID")
'    '            .ModifiedOn = dt.Rows(0)("ModifiedOn")
'    '            .Password = dt.Rows(0)("Password")
'    '            .State = dt.Rows(0)("State")
'    '            .UserName = dt.Rows(0)("UserId")
'    '            .ZipCode = dt.Rows(0)("ZipCode")
'    '            If Not dt.Rows(0)("PaymentMethod") Is DBNull.Value Then .PaymentMethod = dt.Rows(0)("PaymentMethod")
'    '            If Not dt.Rows(0)("bAdmin") Is DBNull.Value Then .IsAdmin = dt.Rows(0)("bAdmin")
'    '        End With

'    '        Return c
'    '    End Function
'    '    Public Function GetcustomerInvoices(ByVal customerId As Integer) As DataTable
'    '        Dim strQ As String = "Select * From QB_Invoices Where CustomerID=@CustomerID"
'    '        strQ += " Order By CreatedOn"

'    '        Dim req As New DataRequest
'    '        req.Command = strQ

'    '        req.AddParameter("@CustomerID", customerId)

'    '        Return data.ExecuteQuery(req)
'    '    End Function
'    '    Public Function GetTotalBalance(ByVal customerId As Integer) As Decimal
'    '        Dim strQ As String = "Select Sum(BalanceRemaining) From QB_Invoices Where CustomerID=@CustomerID"
'    '        strQ += " AND (IsPaid IS NULL or IsPaid=0)"

'    '        Dim req As New DataRequest
'    '        req.Command = strQ

'    '        req.AddParameter("@CustomerID", customerId)

'    '        Dim dt As DataTable = data.ExecuteQuery(req)
'    '        If dt.Rows.Count = 0 Then Return 0
'    '        If dt.Rows(0)(0) Is DBNull.Value Then Return 0
'    '        Dim total As Decimal = dt.Rows(0)(0)
'    '        Return total
'    '    End Function
'    '    Public Function GetPaidInvoices(ByVal paymentID As Integer) As DataTable
'    '        Dim strQ As String = "Select * From QB_Invoices Where PaymentId=@PaymentId"
'    '        strQ += " Order By CreatedOn"

'    '        Dim req As New DataRequest
'    '        req.Command = strQ

'    '        req.AddParameter("@PaymentId", paymentID)

'    '        Return data.ExecuteQuery(req)

'    '    End Function
'    '    'Public Function MarkInvoicesPaid(ByVal customerId As Integer) As Boolean
'    '    '    Dim strQ As String = "UPDATE QB_Invoices SET IsPaid=1 Where CustomerID=@CustomerID"
'    '    '    Dim req As New DataRequest
'    '    '    req.Command = strQ

'    '    '    req.AddParameter("@CustomerID", customerId)

'    '    '    If data.ExecuteNonQuery(req) > 0 Then
'    '    '        Return True
'    '    '    Else
'    '    '        Return False
'    '    '    End If
'    '    'End Function
'    '#End Region

'    '#Region "Bank Account Functions"
'    '    Public Function UpdateBankAccountInfo(ByVal ba As Payments.BankAccount) As Boolean
'    '        Dim oldAccount As Payments.BankAccount = Nothing

'    '        If ba.ID > 0 Then oldAccount = GetBankAccount(ba.ID)
'    '        If oldAccount Is Nothing Then
'    '            oldAccount = GetBankAccount_ByContactId(ba.ContactID)
'    '        End If

'    '        ba.AccountNumber = Encryption.Encrypt(ba.AccountNumber)

'    '        Dim result As Boolean = False
'    '        If oldAccount Is Nothing Then
'    '            'User Never had any account, Save New
'    '            result = BankAccount_Add(ba)
'    '        Else
'    '            'User Alrady have account, update that.
'    '            ba.ID = oldAccount.ID
'    '            result = BankAccount_Update(ba)
'    '        End If

'    '        Return result
'    '    End Function
'    '    Public Function BankAccount_Update(ByVal acc As Payments.BankAccount) As Boolean
'    '        Dim req As New DataRequest
'    '        req.Command = "usp_BakAccount_u"
'    '        req.CommandType = CommandType.StoredProcedure


'    '        req.AddParameter("@ID", acc.ID)
'    '        req.AddParameter("@ContactId", acc.ContactID)
'    '        req.AddParameter("@AccountTitle", acc.AccountTitle)
'    '        req.AddParameter("@AccountNumber", acc.AccountNumber)
'    '        req.AddParameter("@AccountType", acc.AccountType)
'    '        req.AddParameter("@BankName", acc.BankName)
'    '        req.AddParameter("@RoutingNumber", acc.RoutingNumber)

'    '        If data.ExecuteNonQuery(req) > 0 Then
'    '            Return True
'    '        Else
'    '            Return False
'    '        End If

'    '    End Function
'    '    Public Function BankAccount_Add(ByVal acc As Payments.BankAccount) As Boolean
'    '        Dim req As New DataRequest
'    '        req.Command = "usp_BakAccount_i"
'    '        req.CommandType = CommandType.StoredProcedure

'    '        req.AddParameter("@ContactId", acc.ContactID)
'    '        req.AddParameter("@AccountTitle", acc.AccountTitle)
'    '        req.AddParameter("@AccountNumber", acc.AccountNumber)
'    '        req.AddParameter("@AccountType", acc.AccountType)
'    '        req.AddParameter("@BankName", acc.BankName)
'    '        req.AddParameter("@RoutingNumber", acc.RoutingNumber)

'    '        If data.ExecuteNonQuery(req) > 0 Then
'    '            Return True
'    '        Else
'    '            Return False
'    '        End If

'    '    End Function
'    '    Public Function GetBankAccount(ByVal accountID As Integer) As Payments.BankAccount
'    '        Return DataTableToBankAccount(SelectByID(accountID, "usp_BankAccount_SelectByID"))
'    '    End Function
'    '    Public Function GetBankAccount_ByContactId(ByVal contactID As Integer) As Payments.BankAccount
'    '        Dim req As New DataRequest
'    '        req.Command = "usp_BankAccount_by_ContactID_s"
'    '        req.CommandType = CommandType.StoredProcedure
'    '        req.AddParameter("@ContactId", contactID)

'    '        Dim dt As DataTable = data.ExecuteDataSet(req).Tables(0)
'    '        Return DataTableToBankAccount(dt)
'    '    End Function
'    '    Private Shared Function DataTableToBankAccount(ByVal dt As DataTable) As Payments.BankAccount
'    '        If dt.Rows.Count = 0 Then Return Nothing

'    '        Dim acc As New Payments.BankAccount
'    '        With acc
'    '            .AccountNumber = Encryption.Decrypt(dt.Rows(0)("AccountNumber"))
'    '            .AccountTitle = dt.Rows(0)("AccountTitle")
'    '            .AccountType = dt.Rows(0)("AccountType")
'    '            .BankName = dt.Rows(0)("BankName")
'    '            .ContactID = dt.Rows(0)("ContactId")
'    '            .ID = dt.Rows(0)("ID")
'    '            .RoutingNumber = dt.Rows(0)("RoutingNumber")
'    '        End With

'    '        Return acc
'    '    End Function
'    '#End Region

'    '#Region "Credit Card"
'    '    Public Function UpdateUserCreditCardInfo(ByVal cc As Payments.CreditCard) As Boolean
'    '        Dim oldCard As Payments.CreditCard = Nothing

'    '        If cc.ID > 0 Then oldCard = CreditCard_Get(cc.ID)
'    '        If oldCard Is Nothing Then
'    '            oldCard = CreditCard_GetByContactId(cc.ContactID)
'    '        End If

'    '        cc.CardNumber = Encryption.Encrypt(cc.CardNumber)

'    '        Dim result As Boolean = False
'    '        If oldCard Is Nothing Then
'    '            'User Never had any card, Save New
'    '            result = CreditCard_Add(cc)
'    '        Else
'    '            'User Alrady have card, update that.
'    '            cc.ID = oldCard.ID
'    '            result = CreditCard_Update(cc)
'    '        End If

'    '        Return result
'    '    End Function
'    '    Public Function CreditCard_Update(ByVal cc As Payments.CreditCard) As Boolean
'    '        Dim req As New DataRequest
'    '        req.Command = "usp_CreditCard_u"
'    '        req.CommandType = CommandType.StoredProcedure

'    '        req.AddParameter("@ID", cc.ID)
'    '        req.AddParameter("@ContactId", cc.ContactID)
'    '        req.AddParameter("@CardType", cc.CardType)
'    '        req.AddParameter("@CardNumber", cc.CardNumber)
'    '        req.AddParameter("@ExpirationMonth", cc.ExpirationMonth)
'    '        req.AddParameter("@ExpirationYear", cc.ExpirationYear)
'    '        req.AddParameter("@SecurityCode", cc.SecurityCode)
'    '        req.AddParameter("@CardholdersName", cc.CardholdersName)

'    '        If data.ExecuteNonQuery(req) > 0 Then
'    '            Return True
'    '        Else
'    '            Return False
'    '        End If

'    '    End Function
'    '    Public Function CreditCard_Add(ByVal cc As Payments.CreditCard) As Boolean
'    '        Dim req As New DataRequest
'    '        req.Command = "usp_CreditCard_i"
'    '        req.CommandType = CommandType.StoredProcedure

'    '        req.AddParameter("@ContactId", cc.ContactID)
'    '        req.AddParameter("@CardType", cc.CardType)
'    '        req.AddParameter("@CardNumber", cc.CardNumber)
'    '        req.AddParameter("@ExpirationMonth", cc.ExpirationMonth)
'    '        req.AddParameter("@ExpirationYear", cc.ExpirationYear)
'    '        req.AddParameter("@SecurityCode", cc.SecurityCode)
'    '        req.AddParameter("@CardholdersName", cc.CardholdersName)

'    '        If data.ExecuteNonQuery(req) > 0 Then
'    '            Return True
'    '        Else
'    '            Return False
'    '        End If

'    '    End Function
'    '    Public Function CreditCard_GetByContactId(ByVal contactId As Integer) As Payments.CreditCard
'    '        Dim req As New DataRequest
'    '        req.Command = "usp_CreditCard_By_Contact_Id_s"
'    '        req.CommandType = CommandType.StoredProcedure
'    '        req.AddParameter("@ContactId", contactId)

'    '        Dim dt As DataTable = data.ExecuteDataSet(req).Tables(0)
'    '        Return DataTableToCreditCard(dt)
'    '    End Function
'    '    Public Function CreditCard_Get(ByVal cardId As Integer) As Payments.CreditCard
'    '        Return DataTableToCreditCard(SelectByID(cardId, "usp_CreditCard_SelectByID"))
'    '    End Function
'    '    Private Shared Function DataTableToCreditCard(ByVal dt As DataTable) As Payments.CreditCard
'    '        If dt.Rows.Count = 0 Then Return Nothing
'    '        Dim cc As New Payments.CreditCard
'    '        With cc
'    '            .CardholdersName = dt.Rows(0)("CardholdersName")
'    '            .CardNumber = Encryption.Decrypt(dt.Rows(0)("CardNumber"))
'    '            .CardType = dt.Rows(0)("CardType")
'    '            .ContactID = dt.Rows(0)("ContactId")
'    '            .ExpirationMonth = dt.Rows(0)("ExpirationMonth")
'    '            .ExpirationYear = dt.Rows(0)("ExpirationYear")
'    '            .ID = dt.Rows(0)("ID")
'    '            .SecurityCode = dt.Rows(0)("SecurityCode")
'    '        End With
'    '        Return cc
'    '    End Function
'    '#End Region

'    '#Region "Payment Transactions"
'    '    Public Function LogTransactionResponse(ByVal terminal As Payments.PaymentTerminal, ByVal paymentMethod As Payments.PaymentType, ByVal success As Boolean) As Boolean
'    '        Dim strQ As String = "INSERT INTO QB_TransactionLog (CustomerId, TransactionDate, FullRequest, FullResponse, ResponseCode, ResponseMessage, ResponseTransId, PaymentMethod, amount, bSuccess) values("
'    '        strQ += "@CustomerId, @TransactionDate, @FullRequest, @FullResponse, @ResponseCode, @ResponseMessage, @ResponseTransId, @PaymentMethod, @amount, @bSuccess)"

'    '        Dim req As New DataRequest
'    '        req.Command = strQ
'    '        req.AddParameter("@CustomerId", terminal.CustomerId)
'    '        req.AddParameter("@TransactionDate", DateTime.Now.ToString)
'    '        req.AddParameter("@FullRequest", terminal.FullRequest)
'    '        req.AddParameter("@FullResponse", terminal.FullResponse)
'    '        req.AddParameter("@ResponseCode", terminal.ResponseCode)
'    '        req.AddParameter("@ResponseMessage", terminal.ResponseMessage)
'    '        req.AddParameter("@ResponseTransId", terminal.ResponseTransactionID)

'    '        req.AddParameter("@PaymentMethod", paymentMethod)
'    '        req.AddParameter("@amount", terminal.Amount)
'    '        req.AddParameter("@bSuccess", success)

'    '        If data.ExecuteNonQuery(req) > 0 Then
'    '            Return True
'    '        Else
'    '            Return False
'    '        End If
'    '    End Function
'    '    Public Function LogPaymentReceived(ByVal terminal As Payments.PaymentTerminal, ByVal paymentMethod As Payments.PaymentType) As Integer
'    '        Dim strQ As String = "INSERT INTO QB_Payments (CustomerId, PaidOn, ResponseCode, ResponseMessage, ResponseTransactionID, PaymentMethod, amount) values ("
'    '        strQ += "@CustomerId, @PaidOn, @ResponseCode, @ResponseMessage, @ResponseTransactionID, @PaymentMethod, @amount);"
'    '        strQ += "Select @@IDENTITY as NewId"

'    '        Dim req As New DataRequest
'    '        req.Command = strQ
'    '        req.AddParameter("@CustomerId", terminal.CustomerId)
'    '        req.AddParameter("@PaidOn", DateTime.Now.ToString)
'    '        req.AddParameter("@ResponseCode", terminal.ResponseCode)
'    '        req.AddParameter("@ResponseMessage", terminal.ResponseMessage)
'    '        req.AddParameter("@ResponseTransactionID", terminal.ResponseTransactionID)
'    '        req.AddParameter("@PaymentMethod", paymentMethod)
'    '        req.AddParameter("@amount", terminal.Amount)

'    '        Dim id As Integer = data.ExecuteAndReadInteger(req, "NewId")

'    '        If id > 0 Then
'    '            UpdateInvoicesPaid(terminal.CustomerId, id, terminal.ResponseTransactionID)
'    '            Return id
'    '        Else
'    '            Return -1
'    '        End If
'    '    End Function
'    '    Public Function UpdateInvoicesPaid(ByVal customerId As Integer, ByVal paymentId As Integer, ByVal ReferenceNumber As String) As Boolean
'    '        Dim strQ As String = "UPDATE QB_Invoices SET PaymentId=@PaymentID, IsPaid=1, ReferenceNumber=@ReferenceNumber Where "
'    '        strQ += " CustomerID=@CustomerID AND (PaymentId IS NULL OR PaymentId=0)"

'    '        Dim req As New DataRequest
'    '        req.Command = strQ
'    '        req.AddParameter("@PaymentId", paymentId)
'    '        req.AddParameter("@CustomerID", customerId)
'    '        req.AddParameter("@ReferenceNumber", ReferenceNumber)
'    '        If data.ExecuteNonQuery(req) > 0 Then
'    '            Return True
'    '        Else
'    '            Return False
'    '        End If
'    '    End Function
'    '#End Region

'    '#Region "Search Users"
'    '    Public Function SearchUser(ByVal firstName As String, ByVal lastName As String, ByVal email As String) As DataTable
'    '        Dim strQ As String = "Select * From QB_Customers Where 1=1 "
'    '        If firstName.Trim <> "" Then strQ += " AND FirstName LIKE @FirstName "
'    '        If lastName.Trim <> "" Then strQ += " AND LastName LIKE @LastName "
'    '        If email.Trim <> "" Then strQ += " AND UserID LiKE @UserId"
'    '        strQ += " Order By FirstName, LastName "

'    '        Dim req As New DataRequest
'    '        req.Command = strQ
'    '        req.CommandType = CommandType.Text
'    '        req.AddParameter("@FirstName", "%" & firstName & "%")
'    '        req.AddParameter("@LastName", "%" & lastName & "%")
'    '        req.AddParameter("@UserID", "%" & email & "%")
'    '        Dim dt As DataTable = data.ExecuteQuery(req)

'    '        Return dt

'    '    End Function
'    '#End Region

'    '#Region "Exception Logging functions"
'    '    Public Function AddToLog(ByVal logType As Integer, ByVal msg As String, ByVal stackTrace As String, ByVal sourcefile As String, ByVal sourceLine As Integer, ByVal sourceFunction As String, ByVal HttpUrl As String) As Boolean
'    '        Dim request As New DataRequest
'    '        request.Command = "usp_ErrorLog_i"
'    '        request.CommandType = CommandType.StoredProcedure
'    '        request.Transactional = False

'    '        request.AddParameter("@OccuredOn", DateTime.Now.ToString)
'    '        request.AddParameter("@Type", logType)
'    '        request.AddParameter("@Message", msg)

'    '        request.AddParameter("@StackTrace", stackTrace)
'    '        request.AddParameter("@SourceFile", sourcefile)
'    '        request.AddParameter("@SourceLine", sourceLine)
'    '        request.AddParameter("@SourceFunction", sourceFunction)
'    '        request.AddParameter("@HttpURL", HttpURL)

'    '        Dim result As Integer = data.ExecuteNonQuery(request)

'    '        If result = Nothing Then
'    '            Return False
'    '        Else
'    '            If result = 1 Then
'    '                Return True
'    '            Else
'    '                Return False
'    '            End If
'    '        End If
'    '    End Function
'    '#End Region
'End Class
