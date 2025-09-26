Public Class DataServices
    Private Shared _data As SQLServerDataHelper = Nothing

    Private Shared ReadOnly Property Data() As SQLServerDataHelper
        Get
            If _data Is Nothing Then _data = New SQLServerDataHelper(AppSettings.ConnectionString)
            Return _data
        End Get
    End Property
    
#Region "Locally used"
    Protected Shared Function SelectDataSetByID(ByVal id As Integer, ByVal storedProcName As String) As DataSet
        Dim request As New DataRequest
        request.Command = storedProcName
        request.CommandType = CommandType.StoredProcedure
        request.AddParameter("@ID", id)
        request.Transactional = False
        Dim result As DataSet
        result = Data.ExecuteDataSet(request)
        Return result
    End Function
    Protected Shared Function SelectByID(ByVal id As Integer, ByVal storedProcName As String) As DataTable
        Dim dtResult As New DataTable("Results")
        Dim tempDS As DataSet = SelectDataSetByID(id, storedProcName)
        If Not tempDS Is Nothing Then
            If tempDS.Tables.Count() > 0 Then
                dtResult = tempDS.Tables(0)
            End If
        End If
        tempDS = Nothing
        Return dtResult
    End Function
#End Region

#Region "Content functions"
    Public Shared Function GetAllStates() As DataTable
        Dim strQ As String = "Select StateName, StateCode From QB_Region Order By StateName"
        Return Data.ExecuteQuery(strQ)
    End Function
#End Region

#Region "Contact US"
    Public Shared Function ContactUs_Insert(ByVal c As ContactUs) As Boolean
        Dim strQ As String = "INSERT INTO tblAGLContactUs(SenderName, Email, InterestedService, Message, SentOn, IPAddress, Phone) values("
        strQ += " @SenderName, @Email, @InterestedService, @Message, @SentOn, @IPAddress, @Phone)"

        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text

        If c.SentOn.Equals(DateTime.MinValue) Then c.SentOn = DateTime.Now

        req.AddParameter("@SenderName", c.SenderName)
        req.AddParameter("@Email", c.Email)
        req.AddParameter("@InterestedService", c.InterestedService)
        req.AddParameter("@Message", c.Message)
        req.AddParameter("@SentOn", c.SentOn)
        req.AddParameter("@IPAddress", c.IPAddress)
        req.AddParameter("@Phone", c.Phone)

        If Data.ExecuteNonQuery(req) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
#End Region

    Public Shared Function ClearAllErrorsBeforeDownloading() As Boolean
        Dim strQ As String = "UPDATE Customer SET bError=0"
        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text

        If Data.ExecuteNonQuery(strQ) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
#Region "Customers"
    Public Shared Function GetCustomersFromBillToID(ByVal BillToID As Integer) As List(Of Integer)
        Dim strQ As String = "Select * From Customer Where BillToID=@BillToID"
        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text
        req.AddParameter("@BillToID", BillToID)
        Dim dt As DataTable = Data.ExecuteQuery(req)

        Dim list As New List(Of Integer)

        For Each row As DataRow In dt.Rows
            list.Add(row("CustomerID"))
        Next
        Return list
    End Function
    Public Shared Function GetCustomer(ByVal id As Integer) As Customer
        Dim strQ As String = "Select * From Customer Where CustomerID=@ID"
        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text
        req.AddParameter("@ID", id)
        Dim dt As DataTable = Data.ExecuteQuery(req)

        If dt.Rows.Count = 0 Then Return Nothing

        'Prepare the user object
        Return ConvertCustomerTableToCustomerObject(dt)
    End Function
    Public Shared Function GetNextNewCustomer() As Customer
        Dim strQ As String = "Select TOP 1 * From Customer Where (LISTID IS NULL OR ListId='') AND bError=0 AND (PARENTID <1 OR PARENTID=" & Customer.TopParentCompanyID & ")"
        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text
        Dim dt As DataTable = Data.ExecuteQuery(req)

        If dt.Rows.Count = 0 Then Return Nothing

        'Prepare the user object
        Return ConvertCustomerTableToCustomerObject(dt)
    End Function
    Public Shared Function GetTotalNewCustomers() As Integer
        Dim strQ As String = "Select COUNT (*) as TotalNew From Customer Where (LISTID IS NULL OR ListId='') AND (PARENTID <1 OR PARENTID=" & Customer.TopParentCompanyID & ") "
        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text
        Dim dt As DataTable = Data.ExecuteQuery(req)

        If dt.Rows.Count = 0 Then Return 0

        'Prepare the user object
        If dt.Rows(0)("TotalNew") Is DBNull.Value Then Return 0
        Return CInt(dt.Rows(0)("TotalNew"))
    End Function
    Public Shared Function GetCustomer(ByVal UserID As String) As Customer
        Dim strQ As String = "Select * From Customer Where UserID=@UserID"
        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text
        req.AddParameter("@UserID", UserID)

        Dim dt As DataTable = Data.ExecuteQuery(req)

        If dt.Rows.Count = 0 Then Return Nothing

        'Prepare the user object
        Return ConvertCustomerTableToCustomerObject(dt)
    End Function
    Private Shared Function ConvertCustomerTableToCustomerObject(ByVal dt As DataTable) As Customer
        Dim c As New Customer
        With c
            If Not dt.Rows(0)("CustomerID") Is DBNull.Value Then .CustomerID = dt.Rows(0)("CustomerID")
            If Not dt.Rows(0)("CompanyName") Is DBNull.Value Then .CompanyName = dt.Rows(0)("CompanyName")
            If Not dt.Rows(0)("Name") Is DBNull.Value Then .Name = dt.Rows(0)("Name")
            If Not dt.Rows(0)("Telephone") Is DBNull.Value Then .Telephone = dt.Rows(0)("Telephone")
            If Not dt.Rows(0)("FaxNumber") Is DBNull.Value Then .FaxNumber = dt.Rows(0)("FaxNumber")
            If Not dt.Rows(0)("Email") Is DBNull.Value Then .Email = dt.Rows(0)("Email")
            If Not dt.Rows(0)("BilltoName") Is DBNull.Value Then .BilltoName = dt.Rows(0)("BilltoName")
            If Not dt.Rows(0)("Address") Is DBNull.Value Then .Address = dt.Rows(0)("Address")
            If Not dt.Rows(0)("Address1") Is DBNull.Value Then .Address1 = dt.Rows(0)("Address1")
            If Not dt.Rows(0)("City") Is DBNull.Value Then .City = dt.Rows(0)("City")
            If Not dt.Rows(0)("State") Is DBNull.Value Then .State = dt.Rows(0)("State")
            If Not dt.Rows(0)("Zip") Is DBNull.Value Then .Zip = dt.Rows(0)("Zip")
            If Not dt.Rows(0)("Referal") Is DBNull.Value Then .Referal = dt.Rows(0)("Referal")
            If Not dt.Rows(0)("UserID") Is DBNull.Value Then .UserID = dt.Rows(0)("UserID")
            If Not dt.Rows(0)("Password") Is DBNull.Value Then .Password = dt.Rows(0)("Password")
            If Not dt.Rows(0)("Approved") Is DBNull.Value Then .Approved = dt.Rows(0)("Approved")
            If Not dt.Rows(0)("confDate") Is DBNull.Value Then .confDate = dt.Rows(0)("confDate")
            If Not dt.Rows(0)("confTime") Is DBNull.Value Then .confTime = dt.Rows(0)("confTime")
            If Not dt.Rows(0)("BranchID") Is DBNull.Value Then .BranchID = dt.Rows(0)("BranchID")
            If Not dt.Rows(0)("Status") Is DBNull.Value Then .Status = dt.Rows(0)("Status")
            If Not dt.Rows(0)("standardRate") Is DBNull.Value Then .standardRate = dt.Rows(0)("standardRate")
            If Not dt.Rows(0)("rushRate") Is DBNull.Value Then .rushRate = dt.Rows(0)("rushRate")
            If Not dt.Rows(0)("showgrant") Is DBNull.Value Then .showgrant = dt.Rows(0)("showgrant")
            If Not dt.Rows(0)("fld_associate_id") Is DBNull.Value Then .Associate_id = dt.Rows(0)("fld_associate_id")
            If Not dt.Rows(0)("fld_bill4506") Is DBNull.Value Then .Bill4506 = dt.Rows(0)("fld_bill4506")
            If Not dt.Rows(0)("fld_billssn") Is DBNull.Value Then .BillSSN = dt.Rows(0)("fld_billssn")
            If Not dt.Rows(0)("fld_commission_amount") Is DBNull.Value Then .Commission_Amount = dt.Rows(0)("fld_commission_amount")
            If Not dt.Rows(0)("fld_comm_4506") Is DBNull.Value Then .Comm_4506 = dt.Rows(0)("fld_comm_4506")
            If Not dt.Rows(0)("fld_comm_SSN") Is DBNull.Value Then .Comm_SSN = dt.Rows(0)("fld_comm_SSN")
            If Not dt.Rows(0)("ListId") Is DBNull.Value Then .ListId = dt.Rows(0)("ListId")
            If Not dt.Rows(0)("ChargeSecondTaxPayer") Is DBNull.Value Then .ChargeSecondTaxPayer = dt.Rows(0)("ChargeSecondTaxPayer")

            If Not dt.Rows(0)("SSN_Fee") Is DBNull.Value Then .SSN_Fee = dt.Rows(0)("SSN_Fee")

            If Not dt.Rows(0)("ParentID") Is DBNull.Value Then .ParentID = dt.Rows(0)("ParentID")
            If Not dt.Rows(0)("CreditCardActive") Is DBNull.Value Then .CreditCardActive = dt.Rows(0)("CreditCardActive")
            If Not dt.Rows(0)("irs_fee") Is DBNull.Value Then .IRSFee = dt.Rows(0)("irs_fee")
            If Not dt.Rows(0)("Addloannumber") Is DBNull.Value Then .Addloannumber = dt.Rows(0)("Addloannumber")
            If Not dt.Rows(0)("BillToID") Is DBNull.Value Then .BillToID = dt.Rows(0)("BillToID")
            If .rushRate = 0 Then .rushRate = 19
        End With

        Return c
    End Function
    

    Public Shared Function GetCustomerFromListId(ByVal listID As String) As Integer
        Dim strQ As String = "Select Id From Customer Where ListID=@ListID"
        Dim req As New DataRequest
        req.CommandType = CommandType.Text
        req.Command = strQ
        req.AddParameter("@ListID", listID)

        Dim dt As DataTable = Data.ExecuteQuery(req)
        If dt.Rows.Count = 0 Then Return -1
        Return dt.Rows(0)("ID")
    End Function
    
    Public Shared Function InsertCustomer(ByVal c As Customer) As Integer
        Dim id As Integer = GetCustomerFromListId(c.ListId)
        If id > 1 Then
            Return id
        End If

        Dim strQ As String = ""
        'New Customer
        strQ = "INSERT INTO Customer(CompanyName, Name, Telephone, FaxNumber, Email, BilltoName, Address, Address1, City, State, Zip, Referal, UserID, Password, Approved, confDate, confTime, BranchID, Status, standardRate, rushRate, showgrant, fld_associate_id, fld_bill4506, fld_billssn, fld_commission_amount, fld_comm_4506, fld_comm_SSN, ListId, ChargeSecondTaxPayer) Values "
        strQ += " (@CompanyName, @Name, @Telephone, @FaxNumber, @Email, @BilltoName, @Address, @Address1, @City, @State, @Zip, @Referal, @UserID, @Password, @Approved, @confDate, @confTime, @BranchID, @Status, @standardRate, @rushRate, @showgrant, @fld_associate_id, @fld_bill4506, @fld_billssn, @fld_commission_amount, @fld_comm_4506, @fld_comm_SSN, @ListId, @ChargeSecondTaxPayer); "
        strQ += "Select @@IDENTITY as NewId"

        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text

        req.AddParameter("@CompanyName", c.CompanyName)
        req.AddParameter("@Name", c.Name)
        req.AddParameter("@Telephone", c.Telephone)
        req.AddParameter("@FaxNumber", c.FaxNumber)
        req.AddParameter("@Email", c.Email)
        req.AddParameter("@BilltoName", c.BilltoName)
        req.AddParameter("@Address", c.Address)
        req.AddParameter("@Address1", c.Address1)
        req.AddParameter("@City", c.City)
        req.AddParameter("@State", c.State)
        req.AddParameter("@Zip", c.Zip)
        req.AddParameter("@Referal", c.Referal)
        req.AddParameter("@UserID", c.UserID)
        req.AddParameter("@Password", c.Password)
        req.AddParameter("@Approved", c.Approved)
        req.AddParameter("@confDate", c.confDate)
        req.AddParameter("@confTime", c.confTime)
        req.AddParameter("@BranchID", c.BranchID)
        req.AddParameter("@Status", c.Status)
        req.AddParameter("@standardRate", c.standardRate)
        req.AddParameter("@rushRate", c.rushRate)
        req.AddParameter("@showgrant", c.showgrant)
        req.AddParameter("@fld_associate_id", c.Associate_id)
        req.AddParameter("@fld_bill4506", c.Bill4506)
        req.AddParameter("@fld_billssn", c.BillSSN)
        req.AddParameter("@fld_commission_amount", c.Commission_Amount)
        req.AddParameter("@fld_comm_4506", c.Comm_4506)
        req.AddParameter("@fld_comm_SSN", c.Comm_SSN)
        req.AddParameter("@ListId", c.ListId)
        req.AddParameter("@ChargeSecondTaxPayer", c.ChargeSecondTaxPayer)


        id = Data.ExecuteAndReadInteger(req, "NewId")


        Return id

    End Function

    Public Shared Function UpdateCustomer(ByVal c As Customer) As Integer

        Dim strQ As String = ""
        'New Customer
        strQ = "UPDATE Customer SET "
        strQ += " ListId = @ListId, bError=@bError "
        strQ += " WHERE CustomerID = @CustomerID"

        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text

        req.AddParameter("@CustomerID", c.CustomerID)

        req.AddParameter("@ListId", c.ListId)
        req.AddParameter("@bError", c.IsError)

        Return Data.ExecuteNonQuery(req)




    End Function

#End Region
#Region "Invoices"

#End Region
#Region "Bank Account Functions"
    Public Shared Function UpdateBankAccountInfo(ByVal ba As Payments.BankAccount) As Boolean
        Dim oldAccount As Payments.BankAccount = Nothing

        If ba.ID > 0 Then oldAccount = GetBankAccount(ba.ID)
        If oldAccount Is Nothing Then
            oldAccount = GetBankAccount_ByContactId(ba.ContactID)
        End If

        ba.AccountNumber = Encryption.Encrypt(ba.AccountNumber)

        Dim result As Boolean = False
        If oldAccount Is Nothing Then
            'User Never had any account, Save New
            result = BankAccount_Add(ba)
        Else
            'User Alrady have account, update that.
            ba.ID = oldAccount.ID
            result = BankAccount_Update(ba)
        End If

        Return result
    End Function
    Public Shared Function BankAccount_Update(ByVal acc As Payments.BankAccount) As Boolean
        Dim req As New DataRequest
        req.Command = "usp_BakAccount_u"
        req.CommandType = CommandType.StoredProcedure


        req.AddParameter("@ID", acc.ID)
        req.AddParameter("@ContactId", acc.ContactID)
        req.AddParameter("@AccountTitle", acc.AccountTitle)
        req.AddParameter("@AccountNumber", acc.AccountNumber)
        req.AddParameter("@AccountType", acc.AccountType)
        req.AddParameter("@BankName", acc.BankName)
        req.AddParameter("@RoutingNumber", acc.RoutingNumber)

        If Data.ExecuteNonQuery(req) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Shared Function BankAccount_Add(ByVal acc As Payments.BankAccount) As Boolean
        Dim req As New DataRequest
        req.Command = "usp_BakAccount_i"
        req.CommandType = CommandType.StoredProcedure

        req.AddParameter("@ContactId", acc.ContactID)
        req.AddParameter("@AccountTitle", acc.AccountTitle)
        req.AddParameter("@AccountNumber", acc.AccountNumber)
        req.AddParameter("@AccountType", acc.AccountType)
        req.AddParameter("@BankName", acc.BankName)
        req.AddParameter("@RoutingNumber", acc.RoutingNumber)

        If Data.ExecuteNonQuery(req) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Shared Function GetBankAccount(ByVal accountID As Integer) As Payments.BankAccount
        Return DataTableToBankAccount(SelectByID(accountID, "usp_BankAccount_SelectByID"))
    End Function
    Public Shared Function GetBankAccount_ByContactId(ByVal contactID As Integer) As Payments.BankAccount
        Dim req As New DataRequest
        req.Command = "usp_BankAccount_by_ContactID_s"
        req.CommandType = CommandType.StoredProcedure
        req.AddParameter("@ContactId", contactID)

        Dim dt As DataTable = Data.ExecuteDataSet(req).Tables(0)
        Return DataTableToBankAccount(dt)
    End Function
    Private Shared Function DataTableToBankAccount(ByVal dt As DataTable) As Payments.BankAccount
        If dt.Rows.Count = 0 Then Return Nothing

        Dim acc As New Payments.BankAccount
        With acc
            .AccountNumber = Encryption.Decrypt(dt.Rows(0)("AccountNumber"))
            .AccountTitle = dt.Rows(0)("AccountTitle")
            .AccountType = dt.Rows(0)("AccountType")
            .BankName = dt.Rows(0)("BankName")
            .ContactID = dt.Rows(0)("ContactId")
            .ID = dt.Rows(0)("ID")
            .RoutingNumber = dt.Rows(0)("RoutingNumber")
        End With

        Return acc
    End Function
#End Region

#Region "Credit Card"
    Public Shared Function UpdateUserCreditCardInfo(ByVal cc As Payments.CreditCard) As Boolean
        Dim oldCard As Payments.CreditCard = Nothing

        If cc.ID > 0 Then oldCard = CreditCard_Get(cc.ID)
        If oldCard Is Nothing Then
            oldCard = CreditCard_GetByContactId(cc.ContactID)
        End If

        cc.CardNumber = Encryption.Encrypt(cc.CardNumber)

        Dim result As Boolean = False
        If oldCard Is Nothing Then
            'User Never had any card, Save New
            result = CreditCard_Add(cc)
        Else
            'User Alrady have card, update that.
            cc.ID = oldCard.ID
            result = CreditCard_Update(cc)
        End If

        Return result
    End Function
    Public Shared Function CreditCard_Update(ByVal cc As Payments.CreditCard) As Boolean
        Dim req As New DataRequest
        req.Command = "usp_CreditCard_u"
        req.CommandType = CommandType.StoredProcedure

        req.AddParameter("@ID", cc.ID)
        req.AddParameter("@ContactId", cc.ContactID)
        req.AddParameter("@CardType", cc.CardType)
        req.AddParameter("@CardNumber", cc.CardNumber)
        req.AddParameter("@ExpirationMonth", cc.ExpirationMonth)
        req.AddParameter("@ExpirationYear", cc.ExpirationYear)
        req.AddParameter("@SecurityCode", cc.SecurityCode)
        req.AddParameter("@CardholdersName", cc.CardholdersName)

        If Data.ExecuteNonQuery(req) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Shared Function CreditCard_Add(ByVal cc As Payments.CreditCard) As Boolean
        Dim req As New DataRequest
        req.Command = "usp_CreditCard_i"
        req.CommandType = CommandType.StoredProcedure

        req.AddParameter("@ContactId", cc.ContactID)
        req.AddParameter("@CardType", cc.CardType)
        req.AddParameter("@CardNumber", cc.CardNumber)
        req.AddParameter("@ExpirationMonth", cc.ExpirationMonth)
        req.AddParameter("@ExpirationYear", cc.ExpirationYear)
        req.AddParameter("@SecurityCode", cc.SecurityCode)
        req.AddParameter("@CardholdersName", cc.CardholdersName)

        If Data.ExecuteNonQuery(req) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Shared Function CreditCard_GetByContactId(ByVal contactId As Integer) As Payments.CreditCard
        Dim req As New DataRequest
        req.Command = "usp_CreditCard_By_Contact_Id_s"
        req.CommandType = CommandType.StoredProcedure
        req.AddParameter("@ContactId", contactId)

        Dim dt As DataTable = Data.ExecuteDataSet(req).Tables(0)
        Return DataTableToCreditCard(dt)
    End Function
    Public Shared Function CreditCard_Get(ByVal cardId As Integer) As Payments.CreditCard
        Return DataTableToCreditCard(SelectByID(cardId, "usp_CreditCard_SelectByID"))
    End Function
    Private Shared Function DataTableToCreditCard(ByVal dt As DataTable) As Payments.CreditCard
        If dt.Rows.Count = 0 Then Return Nothing
        Dim cc As New Payments.CreditCard
        With cc
            .CardholdersName = dt.Rows(0)("CardholdersName")
            .CardNumber = Encryption.Decrypt(dt.Rows(0)("CardNumber"))
            .CardType = dt.Rows(0)("CardType")
            .ContactID = dt.Rows(0)("ContactId")
            .ExpirationMonth = dt.Rows(0)("ExpirationMonth")
            .ExpirationYear = dt.Rows(0)("ExpirationYear")
            .ID = dt.Rows(0)("ID")
            .SecurityCode = dt.Rows(0)("SecurityCode")
        End With
        Return cc
    End Function
#End Region

#Region "Payment Transactions"
    'Public Shared Function LogTransactionResponse(ByVal terminal As Payments.PaymentTerminal, ByVal paymentMethod As Payments.PaymentType, ByVal success As Boolean) As Boolean
    '    Dim strQ As String = "INSERT INTO QB_TransactionLog (CustomerId, TransactionDate, FullRequest, FullResponse, ResponseCode, ResponseMessage, ResponseTransId, PaymentMethod, amount, bSuccess) values("
    '    strQ += "@CustomerId, @TransactionDate, @FullRequest, @FullResponse, @ResponseCode, @ResponseMessage, @ResponseTransId, @PaymentMethod, @amount, @bSuccess)"

    '    Dim req As New DataRequest
    '    req.Command = strQ
    '    req.AddParameter("@CustomerId", terminal.CustomerId)
    '    req.AddParameter("@TransactionDate", DateTime.Now.ToString)
    '    req.AddParameter("@FullRequest", terminal.FullRequest)
    '    req.AddParameter("@FullResponse", terminal.FullResponse)
    '    req.AddParameter("@ResponseCode", terminal.ResponseCode)
    '    req.AddParameter("@ResponseMessage", terminal.ResponseMessage)
    '    req.AddParameter("@ResponseTransId", terminal.ResponseTransactionID)

    '    req.AddParameter("@PaymentMethod", paymentMethod)
    '    req.AddParameter("@amount", terminal.Amount)
    '    req.AddParameter("@bSuccess", success)

    '    If Data.ExecuteNonQuery(req) > 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function
    'Public Shared Function LogPaymentReceived(ByVal terminal As Payments.PaymentTerminal, ByVal paymentMethod As Payments.PaymentType, ByVal orderId As Integer) As Integer
    '    Dim strQ As String = "INSERT INTO QB_Payments (CustomerId, OrderId, PaidOn, ResponseCode, ResponseMessage, ResponseTransactionID, PaymentMethod, amount) values ("
    '    strQ += "@CustomerId, @OrderId, @PaidOn, @ResponseCode, @ResponseMessage, @ResponseTransactionID, @PaymentMethod, @amount);"
    '    strQ += "Select @@IDENTITY as NewId"

    '    Dim req As New DataRequest
    '    req.Command = strQ
    '    req.AddParameter("@CustomerId", terminal.CustomerId)
    '    req.AddParameter("@OrderId", orderId)
    '    req.AddParameter("@PaidOn", DateTime.Now.ToString)
    '    req.AddParameter("@ResponseCode", terminal.ResponseCode)
    '    req.AddParameter("@ResponseMessage", terminal.ResponseMessage)
    '    req.AddParameter("@ResponseTransactionID", terminal.ResponseTransactionID)
    '    req.AddParameter("@PaymentMethod", paymentMethod)
    '    req.AddParameter("@amount", terminal.Amount)

    '    Dim id As Integer = Data.ExecuteAndReadInteger(req, "NewId")

    '    If id > 0 Then
    '        UpdateInvoicesPaid(terminal.CustomerId, id, terminal.ResponseTransactionID)
    '        Return id
    '    Else
    '        Return -1
    '    End If
    'End Function
    'Public Shared Function UpdateInvoicesPaid(ByVal customerId As Integer, ByVal paymentId As Integer, ByVal ReferenceNumber As String) As Boolean
    '    Dim o As Orders.Order = OrderServices.CurrentShoppingCart

    '    For Each item As Orders.OrderItem In o.Items
    '        If item.ProductType = Orders.ProductType.Quick_Books_Invoice Then
    '            Dim strQ As String = "UPDATE QB_Invoices SET PaymentId=@PaymentID, IsPaid=1, ReferenceNumber=@ReferenceNumber Where "
    '            strQ += " ID=@ID"
    '            'strQ += " CustomerID=@CustomerID AND (PaymentId IS NULL OR PaymentId=0)"

    '            Dim req As New DataRequest
    '            req.Command = strQ
    '            req.AddParameter("@ID", item.ProductId)
    '            req.AddParameter("@PaymentId", paymentId)
    '            req.AddParameter("@CustomerID", customerId)
    '            req.AddParameter("@ReferenceNumber", ReferenceNumber)
    '            If Data.ExecuteNonQuery(req) > 0 Then
    '                'Return True
    '            Else
    '                'Return False
    '            End If
    '        End If
    '    Next

    'End Function
#End Region

#Region "Search Users"
    Public Shared Function SearchUser(ByVal firstName As String, ByVal lastName As String, ByVal email As String) As DataTable
        Dim strQ As String = "Select * From Customer Where 1=1 "
        If firstName.Trim <> "" Then strQ += " AND FirstName LIKE @FirstName "
        If lastName.Trim <> "" Then strQ += " AND LastName LIKE @LastName "
        If email.Trim <> "" Then strQ += " AND UserID LiKE @UserId"
        strQ += " Order By FirstName, LastName "

        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text
        req.AddParameter("@FirstName", "%" & firstName & "%")
        req.AddParameter("@LastName", "%" & lastName & "%")
        req.AddParameter("@UserID", "%" & email & "%")
        Dim dt As DataTable = Data.ExecuteQuery(req)

        Return dt

    End Function
#End Region

#Region "Exception Logging functions"
    Public Shared Function AddToLog(ByVal logType As Integer, ByVal msg As String, ByVal stackTrace As String, ByVal sourcefile As String, ByVal sourceLine As Integer, ByVal sourceFunction As String, ByVal HttpUrl As String) As Boolean
        Dim request As New DataRequest
        request.Command = "usp_ErrorLog_i"
        request.CommandType = CommandType.StoredProcedure
        request.Transactional = False

        request.AddParameter("@OccuredOn", DateTime.Now.ToString)
        request.AddParameter("@Type", logType)
        request.AddParameter("@Message", msg)

        request.AddParameter("@StackTrace", stackTrace)
        request.AddParameter("@SourceFile", sourcefile)
        request.AddParameter("@SourceLine", sourceLine)
        request.AddParameter("@SourceFunction", sourceFunction)
        request.AddParameter("@HttpURL", HttpUrl)

        Dim result As Integer = Data.ExecuteNonQuery(request)

        If result = Nothing Then
            Return False
        Else
            If result = 1 Then
                Return True
            Else
                Return False
            End If
        End If
    End Function
#End Region

#Region "Services List"
    Public Shared Function Services_ListAll(ByVal activeOnly As Boolean) As DataTable
        Dim strQ As String = "Select * From tblAGLServices "
        If activeOnly Then strQ += " Where ServiceStatus=1"
        Return Data.ExecuteQuery(strQ)
    End Function
    Public Shared Function Services_ListAllParent(ByVal activeOnly As Boolean) As DataTable
        Dim strQ As String = "Select * From tblAGLServices Where 1=1 "
        If activeOnly Then strQ += " AND ServiceStatus=1"
        strQ += " AND (ParentServiceID IS NULL OR ParentServiceID<1)"
        Return Data.ExecuteQuery(strQ)
    End Function

    Public Shared Function Services_Get(ByVal id As Integer) As DataTable
        Dim strQ As String = "Select * From tblAGLServices Where ServiceId=" & id
        Return Data.ExecuteQuery(strQ)
    End Function
    Public Shared Function Service_Delete(ByVal id As Integer) As Boolean
        Dim strQ As String = "Delete From tblAGLServices Where ServiceId=" & id
        If Data.ExecuteNonQuery(strQ) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function Service_Update(ByVal id As Integer, ByVal name As String, ByVal desc As String, ByVal price As Decimal, ByVal isActive As Boolean, ByVal ParentId As Integer) As Boolean
        Dim req As New DataRequest
        Dim strQ As String = "UPDATE tblAGLServices SET ServiceName=@ServiceName, ServiceDesscription=@ServiceDesscription, ServicePrice=@ServicePrice, ServiceStatus=@ServiceStatus, ParentServiceID=@ParentServiceID "
        strQ += " Where ServiceId=@ServiceId"

        req.Command = strQ
        req.CommandType = CommandType.Text

        req.AddParameter("@ServiceId", id)
        req.AddParameter("@ServiceName", name)
        req.AddParameter("@ServiceDesscription", desc)
        req.AddParameter("@ServicePrice", price)
        req.AddParameter("@ServiceStatus", isActive)
        req.AddParameter("@ParentServiceID", ParentId)


        If Data.ExecuteNonQuery(req) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Shared Function Service_add(ByVal name As String, ByVal desc As String, ByVal price As Decimal, ByVal isActive As Boolean, ByVal ParentID As Integer) As Boolean
        Dim req As New DataRequest
        Dim strQ As String = "INSERT INTO tblAGLServices (ServiceName, ServiceDesscription, ServicePrice, ServiceStatus, ParentServiceID) values("
        strQ += " @ServiceName, @ServiceDesscription, @ServicePrice, @ServiceStatus, @ParentServiceID)"


        req.Command = strQ
        req.CommandType = CommandType.Text

        req.AddParameter("@ServiceName", name)
        req.AddParameter("@ServiceDesscription", desc)
        req.AddParameter("@ServicePrice", price)
        req.AddParameter("@ServiceStatus", isActive)
        req.AddParameter("@ParentServiceID", ParentID)

        If Data.ExecuteNonQuery(req) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Shared Function Service_GetAllChild(ByVal serviceID As Integer, ByVal activeOnly As Boolean) As DataTable
        Dim strQ As String = "Select * From tblAGLServices Where ParentServiceID=" & serviceID
        strQ += " Order By ServiceName"
        Return Data.ExecuteQuery(strQ)
    End Function
    Public Shared Function Service_GetParent(ByVal serviceId As Integer) As Integer
        Dim strQ As String = "Select ParentServiceID From tblAGLServices Where ServiceId=" & serviceId
        Dim dt As DataTable = Data.ExecuteQuery(strQ)
        If dt.Rows.Count = 0 Then Return -1
        If dt.Rows(0)("ParentServiceID") Is DBNull.Value Then Return -1
        Return dt.Rows(0)("ParentServiceID")
    End Function
#End Region

#Region "Orders"
    '    Public Shared Function InsertOrder(ByVal o As Orders.Order) As Boolean
    '        Dim strQ As String = "INSERT INTO QB_Orders(UserId,CreatedOn,BillToNameFirst,BillToNameLast,BillToAddress,BillToCity,BillToState,BillToZipCode,OrderStatus,PaymentStatus,SubTotal,ShippingTotal,TaxTotal,GrandTotal,PaidOn,PaymentId,bCompleted) "
    '        strQ += " Values (@UserId,@CreatedOn,@BillToNameFirst,@BillToNameLast,@BillToAddress,@BillToCity,@BillToState,@BillToZipCode,@OrderStatus,@PaymentStatus,@SubTotal,@ShippingTotal,@TaxTotal,@GrandTotal,@PaidOn,@PaymentId,@bCompleted); "
    '        strQ += " Select @@IDENTITY as New_value"
    '        o.Calculate()

    '        Dim req As New DataRequest
    '        req.Command = strQ
    '        req.CommandType = CommandType.Text


    '        req.AddParameter("@UserId", o.UserId)
    '        req.AddParameter("@CreatedOn", o.CreatedOn)
    '        req.AddParameter("@BillToNameFirst", o.BillToNameFirst)
    '        req.AddParameter("@BillToNameLast", o.BillToNameLast)
    '        req.AddParameter("@BillToAddress", o.BillToAddress)
    '        req.AddParameter("@BillToCity", o.BillToCity)
    '        req.AddParameter("@BillToState", o.BillToState)
    '        req.AddParameter("@BillToZipCode", o.BillToZipCode)
    '        req.AddParameter("@OrderStatus", o.OrderStatus)
    '        req.AddParameter("@PaymentStatus", o.PaymentStatus)
    '        req.AddParameter("@SubTotal", o.SubTotal)
    '        req.AddParameter("@ShippingTotal", o.ShippingTotal)
    '        req.AddParameter("@TaxTotal", o.TaxTotal)
    '        req.AddParameter("@GrandTotal", o.GrandTotal)
    '        req.AddParameter("@PaidOn", o.PaidOn)
    '        req.AddParameter("@PaymentId", o.PaymentId)
    '        req.AddParameter("@bCompleted", o.IsCompleted)

    '        Dim result As Integer = Data.ExecuteAndReadInteger(req, "New_value")
    '        o.id = result
    '        If result > 0 Then
    '            SaveOrderItems(o)
    '        End If
    '        Return True
    '    End Function
    '    Public Shared Function UpdateOrder(ByVal o As Orders.Order) As Boolean
    '        If o.id < 1 Then Return InsertOrder(o)

    '        Dim strQ As String = "UPDATE QB_Orders SET UserId = @UserId,CreatedOn = @CreatedOn,BillToNameFirst = @BillToNameFirst,BillToNameLast = @BillToNameLast,BillToAddress = @BillToAddress,BillToCity = @BillToCity,BillToState = @BillToState,BillToZipCode = @BillToZipCode,OrderStatus = @OrderStatus,PaymentStatus = @PaymentStatus,SubTotal = @SubTotal,ShippingTotal = @ShippingTotal,TaxTotal = @TaxTotal,GrandTotal = @GrandTotal,PaidOn = @PaidOn,PaymentId = @PaymentId,bCompleted = @bCompleted "
    '        strQ += " WHERE id = @id"

    '        o.Calculate()

    '        Dim req As New DataRequest
    '        req.Command = strQ
    '        req.CommandType = CommandType.Text

    '        req.AddParameter("@ID", o.id)
    '        req.AddParameter("@UserId", o.UserId)
    '        req.AddParameter("@CreatedOn", o.CreatedOn)
    '        req.AddParameter("@BillToNameFirst", o.BillToNameFirst)
    '        req.AddParameter("@BillToNameLast", o.BillToNameLast)
    '        req.AddParameter("@BillToAddress", o.BillToAddress)
    '        req.AddParameter("@BillToCity", o.BillToCity)
    '        req.AddParameter("@BillToState", o.BillToState)
    '        req.AddParameter("@BillToZipCode", o.BillToZipCode)
    '        req.AddParameter("@OrderStatus", o.OrderStatus)
    '        req.AddParameter("@PaymentStatus", o.PaymentStatus)
    '        req.AddParameter("@SubTotal", o.SubTotal)
    '        req.AddParameter("@ShippingTotal", o.ShippingTotal)
    '        req.AddParameter("@TaxTotal", o.TaxTotal)
    '        req.AddParameter("@GrandTotal", o.GrandTotal)
    '        req.AddParameter("@PaidOn", o.PaidOn)
    '        req.AddParameter("@PaymentId", o.PaymentId)
    '        req.AddParameter("@bCompleted", o.IsCompleted)

    '        Dim result As Integer = Data.ExecuteNonQuery(req)
    '        If result > 0 Then
    '            SaveOrderItems(o)
    '            Return True
    '        End If
    '        Return False
    '    End Function
    '    Public Shared Function GetOrder(ByVal id As Integer) As Orders.Order
    '        Dim strQ As String = "Select * From QB_Orders Where ID=" & id
    '        Dim dt As DataTable = Data.ExecuteQuery(strQ)
    '        Return ConvertDataTableToOrder(dt)
    '    End Function
    '    Public Shared Function GetAllOrdersOfUser(ByVal UserId As Integer, ByVal completed As Boolean) As DataTable
    '        Dim strQ As String = "Select * From QB_Orders Where UserId=" & UserId & " AND "
    '        If completed Then
    '            strQ += " bCompleted=1"
    '        Else
    '            strQ += " bCompleted=0 OR bCompleted IS NULL "
    '        End If
    '        Dim dt As DataTable = Data.ExecuteQuery(strQ)
    '        Return dt
    '        'Return ConvertDataTableToOrder(dt)
    '    End Function
    '    Private Shared Function ConvertDataTableToOrder(ByVal dt As DataTable) As Orders.Order
    '        If dt.Rows.Count = 0 Then Return Nothing
    '        Dim o As New Orders.Order
    '        With o
    '            If Not dt.Rows(0)("id") Is DBNull.Value Then .id = dt.Rows(0)("id")
    '            If Not dt.Rows(0)("UserId") Is DBNull.Value Then .UserId = dt.Rows(0)("UserId")
    '            If Not dt.Rows(0)("CreatedOn") Is DBNull.Value Then .CreatedOn = dt.Rows(0)("CreatedOn")
    '            If Not dt.Rows(0)("BillToNameFirst") Is DBNull.Value Then .BillToNameFirst = dt.Rows(0)("BillToNameFirst")
    '            If Not dt.Rows(0)("BillToNameLast") Is DBNull.Value Then .BillToNameLast = dt.Rows(0)("BillToNameLast")
    '            If Not dt.Rows(0)("BillToAddress") Is DBNull.Value Then .BillToAddress = dt.Rows(0)("BillToAddress")
    '            If Not dt.Rows(0)("BillToCity") Is DBNull.Value Then .BillToCity = dt.Rows(0)("BillToCity")
    '            If Not dt.Rows(0)("BillToState") Is DBNull.Value Then .BillToState = dt.Rows(0)("BillToState")
    '            If Not dt.Rows(0)("BillToZipCode") Is DBNull.Value Then .BillToZipCode = dt.Rows(0)("BillToZipCode")
    '            If Not dt.Rows(0)("OrderStatus") Is DBNull.Value Then .OrderStatus = dt.Rows(0)("OrderStatus")
    '            If Not dt.Rows(0)("PaymentStatus") Is DBNull.Value Then .PaymentStatus = dt.Rows(0)("PaymentStatus")
    '            If Not dt.Rows(0)("SubTotal") Is DBNull.Value Then .SubTotal = dt.Rows(0)("SubTotal")
    '            If Not dt.Rows(0)("ShippingTotal") Is DBNull.Value Then .ShippingTotal = dt.Rows(0)("ShippingTotal")
    '            If Not dt.Rows(0)("TaxTotal") Is DBNull.Value Then .TaxTotal = dt.Rows(0)("TaxTotal")
    '            If Not dt.Rows(0)("GrandTotal") Is DBNull.Value Then .GrandTotal = dt.Rows(0)("GrandTotal")
    '            If Not dt.Rows(0)("PaidOn") Is DBNull.Value Then .PaidOn = dt.Rows(0)("PaidOn")
    '            If Not dt.Rows(0)("PaymentId") Is DBNull.Value Then .PaymentId = dt.Rows(0)("PaymentId")
    '            If Not dt.Rows(0)("bCompleted") Is DBNull.Value Then .IsCompleted = dt.Rows(0)("bCompleted")
    '        End With

    '        'Also get the Order Items
    '        dt = GetOrderItems(o.id)
    '        For temp As Integer = 0 To dt.Rows.Count - 1
    '            o.Items.Add(ConvertDataRowToOrderItem(dt.Rows(temp)))
    '        Next
    '        o.Calculate()
    '        Return o
    '    End Function
    '    Private Shared Function ConvertDataRowToOrderItem(ByVal dr As DataRow) As Orders.OrderItem
    '        Dim oItem As New Orders.OrderItem
    '        With oItem
    '            If Not dr("id") Is DBNull.Value Then .id = dr("id")
    '            If Not dr("OrderId") Is DBNull.Value Then .OrderId = dr("OrderId")
    '            If Not dr("Qty") Is DBNull.Value Then .Qty = dr("Qty")
    '            If Not dr("DisplayName") Is DBNull.Value Then .DisplayName = dr("DisplayName")
    '            If Not dr("DisplayDescription") Is DBNull.Value Then .DisplayDescription = dr("DisplayDescription")
    '            If Not dr("SitePrice") Is DBNull.Value Then .SitePrice = dr("SitePrice")
    '            If Not dr("SiteCost") Is DBNull.Value Then .SiteCost = dr("SiteCost")
    '            If Not dr("TaxTotal") Is DBNull.Value Then .TaxTotal = dr("TaxTotal")
    '            If Not dr("ProductType") Is DBNull.Value Then .ProductType = dr("ProductType")
    '            If Not dr("ProductId") Is DBNull.Value Then .ProductId = dr("ProductId")
    '        End With
    '        Return oItem
    '    End Function
    '    Public Shared Function GetLastOrderOfUser(ByVal userID As Integer) As Orders.Order
    '        Dim strQ As String = "Select * From QB_Orders Where PaymentStatus<>" & Orders.PaymentStatus.Charged
    '        strQ += " AND PaymentStatus<>" & Orders.PaymentStatus.Authorized
    '        strQ += " AND UserId=" & userID & " AND bCompleted=0"

    '        Dim dt As DataTable = Data.ExecuteQuery(strQ)
    '        Return ConvertDataTableToOrder(dt)
    '    End Function
    '    Public Shared Function GetOrderItems(ByVal orderId As Integer) As DataTable
    '        Dim strQ As String = "Select * From QB_OrderItems Where OrderId=" & orderId
    '        Return Data.ExecuteQuery(strQ)
    '    End Function
    '    Public Shared Sub SaveOrderItems(ByVal o As Orders.Order)
    '        ClearOrderItems(o.id)

    '        For Each item As Orders.OrderItem In o.Items
    '            Dim strQ As String = "INSERT INTO QB_OrderItems(OrderId,Qty,DisplayName,DisplayDescription,SitePrice,SiteCost,TaxTotal,ProductType,ProductId) Values (@OrderId,@Qty,@DisplayName,@DisplayDescription,@SitePrice,@SiteCost,@TaxTotal,@ProductType,@ProductId); "
    '            strQ += " SELECT @@IDENTITY as New_ID"

    '            Dim req As New DataRequest
    '            req.Command = strQ
    '            req.CommandType = CommandType.Text

    '            req.AddParameter("@id", item.id)
    '            req.AddParameter("@OrderId", o.id)
    '            req.AddParameter("@Qty", item.Qty)
    '            req.AddParameter("@DisplayName", item.DisplayName)
    '            req.AddParameter("@DisplayDescription", item.DisplayDescription)
    '            req.AddParameter("@SitePrice", item.SitePrice)
    '            req.AddParameter("@SiteCost", item.SiteCost)
    '            req.AddParameter("@TaxTotal", item.TaxTotal)
    '            req.AddParameter("@ProductType", item.ProductType)
    '            req.AddParameter("@ProductId", item.ProductId)

    '            item.id = Data.ExecuteAndReadInteger(req, "New_ID")

    '        Next

    '    End Sub
    '    Public Shared Sub ClearOrderItems(ByVal orderId As Integer)
    '        Dim strQ As String = "DELETE FROM QB_OrderItems Where OrderId=" & orderId

    '        Data.ExecuteNonQuery(strQ)
    '    End Sub
    Public Function Orders_AddNew(ByVal fldListid As Decimal, ByVal fldlisttype As Decimal, ByVal fldCompanyID As Integer, ByVal fldcustomeriD As Integer, ByVal fldrequestname As String, ByVal fldsecondname As String, ByVal fldssnno As String, ByVal fldtaxyear2003 As Boolean, ByVal fldtaxyear2002 As Boolean, ByVal fldtaxyear2001 As Boolean, ByVal fldtaxyear2000 As Boolean, ByVal fldtypeofform As String, ByVal fldemail As String, ByVal fldfax As String, ByVal fldfaxno As String, ByVal fldstatus As String, ByVal fldDOB As String, ByVal fldSex As String, ByVal fldbillingstatus As Boolean, ByVal flddeliverydate As DateTime, ByVal fldPdf As String, ByVal fldOrderdate As DateTime, ByVal fldTaxyear2004 As Boolean, ByVal fldSpecialFlag As String, ByVal fldTaxyear2005 As Boolean, ByVal fldTaxyear2006 As Boolean, ByVal ListID As String, ByVal bUpdatedInQB As Boolean, ByVal fldTaxyear2007 As Boolean, ByVal fldTaxyear2008 As Boolean, ByVal fldTaxyear2009 As Boolean, ByVal fldTaxyear2010 As Boolean, ByVal fldLoanNumber As String, ByVal QBBatchNumber As Integer, ByVal UpdatedInQBOn As DateTime, ByVal fldTaxyear2011 As Boolean, ByVal fldTaxyear2012 As Boolean, ByVal IsRejected As Boolean, ByVal RejectCode As Integer, ByVal NoticeReason As String, ByVal IsDismissedForRejection As Boolean, ByVal fldTaxYear2013 As Boolean, ByVal fldTaxYear2014 As Boolean, ByVal fldTaxYear2015 As Boolean, fldTaxYear2016 As Boolean, fldTaxYear2017 As Boolean, fldTaxYear2018 As Boolean, fldTaxYear2019 As Boolean, fldTaxYear2020 As Boolean, fldTaxYear2021 As Boolean, fldTaxYear2022 As Boolean, fldTaxYear2023 As Boolean, fldTaxYear2024 As Boolean, fldTaxYear2025 As Boolean, Optional ByVal fldordertype As String = Nothing) As Integer
        Dim req As New DataRequest
        req.Command = "INSERT INTO tblorder(fldordertype,fldListid, fldlisttype, fldCompanyID, fldcustomeriD, fldrequestname, fldsecondname, fldssnno, fldtaxyear2003, fldtaxyear2002, fldtaxyear2001, fldtaxyear2000, fldtypeofform, fldemail, fldfax, fldfaxno, fldstatus, fldDOB, fldSex, fldbillingstatus, flddeliverydate, fldPdf, fldOrderdate, fldTaxyear2004, fldSpecialFlag, fldTaxyear2005, fldTaxyear2006, ListID, bUpdatedInQB, fldTaxyear2007, fldTaxyear2008, fldTaxyear2009, fldTaxyear2010, fldLoanNumber, QBBatchNumber, UpdatedInQBOn, fldTaxyear2011, fldTaxyear2012, IsRejected, RejectCode, NoticeReason, IsDismissedForRejection, fldTaxYear2013, fldTaxYear2014, fldTaxYear2015, fldTaxYear2016, fldTaxYear2017, fldTaxYear2018, fldTaxYear2019, fldTaxYear2020, fldTaxYear2021, fldTaxYear2022, fldTaxYear2023, fldTaxYear2024, fldTaxYear2025) " & vbCrLf
        req.Command += " Values (@fldordertype,@fldListid, @fldlisttype, @fldCompanyID, @fldcustomeriD, @fldrequestname, @fldsecondname, @fldssnno, @fldtaxyear2003, @fldtaxyear2002, @fldtaxyear2001, @fldtaxyear2000, @fldtypeofform, @fldemail, @fldfax, @fldfaxno, @fldstatus, @fldDOB, @fldSex, @fldbillingstatus, @flddeliverydate, @fldPdf, @fldOrderdate, @fldTaxyear2004, @fldSpecialFlag, @fldTaxyear2005, @fldTaxyear2006, @ListID, @bUpdatedInQB, @fldTaxyear2007, @fldTaxyear2008, @fldTaxyear2009, @fldTaxyear2010, @fldLoanNumber, @QBBatchNumber, @UpdatedInQBOn, @fldTaxyear2011, @fldTaxyear2012, @IsRejected, @RejectCode, @NoticeReason, @IsDismissedForRejection, @fldTaxYear2013, @fldTaxYear2014, @fldTaxYear2015, @fldTaxYear2016, @fldTaxYear2017, @fldTaxYear2018, @fldTaxYear2019, @fldTaxYear2020, @fldTaxYear2021, @fldTaxYear2022, @fldTaxYear2023, @fldTaxYear2024, @fldTaxYear2025)" & vbCrLf
        req.Command += "SELECT SCOPE_IDENTITY() AS NEW_ID"
        req.CommandType = CommandType.Text


        req.AddParameter("@fldListid", fldListid)
        req.AddParameter("@fldlisttype", fldlisttype)
        req.AddParameter("@fldCompanyID", fldCompanyID)
        req.AddParameter("@fldcustomeriD", fldcustomeriD)
        req.AddParameter("@fldrequestname", fldrequestname)
        req.AddParameter("@fldsecondname", fldsecondname)
        req.AddParameter("@fldssnno", fldssnno)
        req.AddParameter("@fldtaxyear2003", fldtaxyear2003)
        req.AddParameter("@fldtaxyear2002", fldtaxyear2002)
        req.AddParameter("@fldtaxyear2001", fldtaxyear2001)
        req.AddParameter("@fldtaxyear2000", fldtaxyear2000)
        req.AddParameter("@fldtypeofform", fldtypeofform)
        req.AddParameter("@fldordertype", fldordertype)
        req.AddParameter("@fldemail", fldemail)
        req.AddParameter("@fldfax", fldfax)
        req.AddParameter("@fldfaxno", fldfaxno)
        req.AddParameter("@fldstatus", fldstatus)
        req.AddParameter("@fldDOB", fldDOB)
        req.AddParameter("@fldSex", fldSex)
        req.AddParameter("@fldbillingstatus", fldbillingstatus)
        req.AddParameter("@flddeliverydate", flddeliverydate)
        req.AddParameter("@fldPdf", fldPdf)
        req.AddParameter("@fldOrderdate", fldOrderdate)
        req.AddParameter("@fldTaxyear2004", fldTaxyear2004)
        req.AddParameter("@fldSpecialFlag", fldSpecialFlag)
        req.AddParameter("@fldTaxyear2005", fldTaxyear2005)
        req.AddParameter("@fldTaxyear2006", fldTaxyear2006)
        req.AddParameter("@ListID", ListID)
        req.AddParameter("@bUpdatedInQB", bUpdatedInQB)
        req.AddParameter("@fldTaxyear2007", fldTaxyear2007)
        req.AddParameter("@fldTaxyear2008", fldTaxyear2008)
        req.AddParameter("@fldTaxyear2009", fldTaxyear2009)
        req.AddParameter("@fldTaxyear2010", fldTaxyear2010)
        req.AddParameter("@fldLoanNumber", fldLoanNumber)
        req.AddParameter("@QBBatchNumber", QBBatchNumber)
        req.AddParameter("@UpdatedInQBOn", UpdatedInQBOn)
        req.AddParameter("@fldTaxyear2011", fldTaxyear2011)
        req.AddParameter("@fldTaxyear2012", fldTaxyear2012)
        req.AddParameter("@IsRejected", IsRejected)
        req.AddParameter("@RejectCode", RejectCode)
        req.AddParameter("@NoticeReason", NoticeReason)
        req.AddParameter("@IsDismissedForRejection", IsDismissedForRejection)
        req.AddParameter("@fldTaxYear2013", fldTaxYear2013)
        req.AddParameter("@fldTaxYear2014", fldTaxYear2014)
        req.AddParameter("@fldTaxYear2015", fldTaxYear2015)
        req.AddParameter("@fldTaxYear2016", fldTaxYear2016)
        req.AddParameter("@fldTaxYear2017", fldTaxYear2017)
        req.AddParameter("@fldTaxYear2018", fldTaxYear2018)
        req.AddParameter("@fldTaxYear2019", fldTaxYear2019)
        req.AddParameter("@fldTaxYear2020", fldTaxYear2020)
        req.AddParameter("@fldTaxYear2021", fldTaxYear2021)
        req.AddParameter("@fldTaxYear2022", fldTaxYear2022)
        req.AddParameter("@fldTaxYear2023", fldTaxYear2023)
        req.AddParameter("@fldTaxYear2024", fldTaxYear2024)
        req.AddParameter("@fldTaxYear2025", fldTaxYear2025)

        Return Data.ExecuteAndReadInteger(req, "NEW_ID")
    End Function
#End Region


#Region "Lawn Analysis"
    Public Shared Function FreeAnalysis_add(ByVal a As Content.Analysis) As Boolean
        Dim strQ As String = "INSERT INTO QB_LawnAnalysis(FirstName,LastName,Email,PhoneNumber,ServicesRequested,LearnMoreRequested,RequestedOn,bProcessed, Address, City, State, ZipCode) Values (@FirstName,@LastName,@Email,@PhoneNumber,@ServicesRequested,@LearnMoreRequested,@RequestedOn,@bProcessed, @Address, @City, @State, @ZipCode)"
        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text

        If a.RequestedOn.Equals(DateTime.MinValue) Then a.RequestedOn = DateTime.Now

        req.AddParameter("@FirstName", a.FirstName)
        req.AddParameter("@LastName", a.LastName)
        req.AddParameter("@Email", a.Email)
        req.AddParameter("@PhoneNumber", a.PhoneNumber)
        req.AddParameter("@ServicesRequested", a.ServicesRequested)
        req.AddParameter("@LearnMoreRequested", a.LearnMoreRequested)
        req.AddParameter("@RequestedOn", a.RequestedOn)
        req.AddParameter("@bProcessed", a.IsProcessed)

        req.AddParameter("@Address", a.Address)
        req.AddParameter("@City", a.City)
        req.AddParameter("@State", a.State)
        req.AddParameter("@ZipCode", a.ZipCode)

        If Data.ExecuteNonQuery(req) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function FreeAnalysis_Update(ByVal a As Content.Analysis) As Boolean
        Dim strQ As String = "UPDATE QB_LawnAnalysis SET FirstName = @FirstName,LastName = @LastName,Email = @Email,PhoneNumber = @PhoneNumber,ServicesRequested = @ServicesRequested,LearnMoreRequested = @LearnMoreRequested,RequestedOn = @RequestedOn,bProcessed = @bProcessed, ZipCode=@ZipCode, State=@State, City=@City, Address=@Address "
        strQ += " WHERE id = @id"
        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text

        If a.RequestedOn.Equals(DateTime.MinValue) Then a.RequestedOn = DateTime.Now

        req.AddParameter("@id", a.id)
        req.AddParameter("@FirstName", a.FirstName)
        req.AddParameter("@LastName", a.LastName)
        req.AddParameter("@Email", a.Email)
        req.AddParameter("@PhoneNumber", a.PhoneNumber)
        req.AddParameter("@ServicesRequested", a.ServicesRequested)
        req.AddParameter("@LearnMoreRequested", a.LearnMoreRequested)
        req.AddParameter("@RequestedOn", a.RequestedOn)
        req.AddParameter("@bProcessed", a.IsProcessed)

        req.AddParameter("@Address", a.Address)
        req.AddParameter("@City", a.City)
        req.AddParameter("@State", a.State)
        req.AddParameter("@ZipCode", a.ZipCode)

        If Data.ExecuteNonQuery(req) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function FreeAnalysis_Get(ByVal id As Integer) As Content.Analysis
        Dim strQ As String = "Select * From QB_LawnAnalysis Where id=" & id

        Dim dt As DataTable = Data.ExecuteQuery(strQ)
        Return DataTableToFreeAnalysis(dt)
    End Function
    Private Shared Function DataTableToFreeAnalysis(ByVal dt As DataTable) As Content.Analysis
        If dt.Rows.Count = 0 Then Return Nothing
        Dim a As New Content.Analysis
        With a
            If Not dt.Rows(0)("id") Is DBNull.Value Then .id = dt.Rows(0)("id")
            If Not dt.Rows(0)("FirstName") Is DBNull.Value Then .FirstName = dt.Rows(0)("FirstName")
            If Not dt.Rows(0)("LastName") Is DBNull.Value Then .LastName = dt.Rows(0)("LastName")
            If Not dt.Rows(0)("Email") Is DBNull.Value Then .Email = dt.Rows(0)("Email")
            If Not dt.Rows(0)("PhoneNumber") Is DBNull.Value Then .PhoneNumber = dt.Rows(0)("PhoneNumber")
            If Not dt.Rows(0)("ServicesRequested") Is DBNull.Value Then .ServicesRequested = dt.Rows(0)("ServicesRequested")
            If Not dt.Rows(0)("LearnMoreRequested") Is DBNull.Value Then .LearnMoreRequested = dt.Rows(0)("LearnMoreRequested")
            If Not dt.Rows(0)("RequestedOn") Is DBNull.Value Then .RequestedOn = dt.Rows(0)("RequestedOn")
            If Not dt.Rows(0)("bProcessed") Is DBNull.Value Then .IsProcessed = dt.Rows(0)("bProcessed")

            If Not dt.Rows(0)("Address") Is DBNull.Value Then .Address = dt.Rows(0)("Address")
            If Not dt.Rows(0)("City") Is DBNull.Value Then .City = dt.Rows(0)("City")
            If Not dt.Rows(0)("State") Is DBNull.Value Then .State = dt.Rows(0)("State")
            If Not dt.Rows(0)("ZipCode") Is DBNull.Value Then .ZipCode = dt.Rows(0)("ZipCode")
        End With
        Return a
    End Function

    Public Shared Function FreeAnalysis_GetAll(ByVal includeProcessed As Boolean) As DataTable
        Dim strQ As String = "Select * From QB_LawnAnalysis "
        If includeProcessed = False Then strQ += " Where (bProcessed IS NULL OR bProcessed=0)"
        Return Data.ExecuteQuery(strQ)
    End Function
#End Region

#Region "Survey"
    Public Shared Function Survey_add(ByVal userID As Integer, ByVal quality As Integer, ByVal qText As String, ByVal process As Integer, ByVal pText As String, ByVal satisfy As Integer, ByVal sText As String, ByVal overall As Integer, ByVal oText As String) As Boolean
        Dim strQ As String = "INSERT INTO tblAGLSurvey(UserId,Question1Option,Question1Reason,Question2Option,Question2Reason,Question3Option,Question3Reason,Question4Option,Question4Reason)"
        strQ += " Values (@UserId,@Question1Option,@Question1Reason,@Question2Option,@Question2Reason,@Question3Option,@Question3Reason,@Question4Option,@Question4Reason)"

        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text

        req.AddParameter("@UserId", userID)
        req.AddParameter("@Question1Option", quality)
        req.AddParameter("@Question1Reason", qText)
        req.AddParameter("@Question2Option", process)
        req.AddParameter("@Question2Reason", pText)
        req.AddParameter("@Question3Option", satisfy)
        req.AddParameter("@Question3Reason", sText)
        req.AddParameter("@Question4Option", overall)
        req.AddParameter("@Question4Reason", oText)


        If Data.ExecuteNonQuery(req) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "Site News"
    Public Shared Function SiteNews_Add(ByVal news As String, ByVal filePath As String, ByVal active As Boolean) As Boolean
        Dim strQ As String = "INSERT INTO QB_SiteNews(NewsDetails, ImageURL, IsActive, AddedOn) values(@NewsDetails, @ImageURL, @IsActive, @AddedOn)"

        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text

        req.AddParameter("@NewsDetails", news)
        req.AddParameter("@ImageURL", filePath)
        req.AddParameter("@IsActive", active)
        req.AddParameter("@AddedOn", Now.ToString)

        If Data.ExecuteNonQuery(req) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function SiteNews_Update(ByVal id As Integer, ByVal news As String, ByVal filePath As String, ByVal isActive As Boolean) As Boolean
        Dim strQ As String = "UPDATE QB_SiteNews SET NewsDetails=@NewsDetails, "
        If filePath <> "" Then
            strQ += "ImageURL=@ImageURL, "
        End If
        strQ += "IsActive=@IsActive WHERE id=@ID"

        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text

        req.AddParameter("@ID", id)
        req.AddParameter("@NewsDetails", news)
        If filePath <> "" Then
            req.AddParameter("@ImageURL", filePath)
        End If
        req.AddParameter("@IsActive", isActive)

        If Data.ExecuteNonQuery(req) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function SiteNews_Delete(ByVal id As Integer) As Boolean
        Dim strQ As String = "DELETE FROM QB_SiteNews WHERE id=" & id
        If Data.ExecuteNonQuery(strQ) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function SiteNews_GetAll(ByVal activeOnly As Boolean) As DataTable
        Dim strQ As String = "Select * From QB_SiteNews "
        If activeOnly Then strQ += " WHERE IsActive=1"
        strQ += " Order BY AddedOn DESC"

        Return Data.ExecuteQuery(strQ)
    End Function
    Public Shared Function SiteNews_Get(ByVal id As Integer) As DataTable
        Dim strQ As String = "Select * From QB_SiteNews WHERE id=" & id
        Return Data.ExecuteQuery(strQ)
    End Function
#End Region

#Region "Refer A Friend"
    Public Shared Function Refer_Insert(ByVal r As ReferAFriend) As Integer
        Dim strQ As String = "INSERT INTO QB_ReferAFriend(Email, FirstName, LastName, Address, Address2, City, State, Zip, Phone, ReferredByID, ReferredOn) Values (@Email, @FirstName, @LastName, @Address, @Address2, @City, @State, @Zip, @Phone, @ReferredByID, @ReferredOn); "
        strQ += " SELECT @@IDENTITY as New_Row"

        Dim req As New DataRequest

        req.Command = strQ
        req.CommandType = CommandType.Text

        req.AddParameter("@Email", r.Email)
        req.AddParameter("@FirstName", r.FirstName)
        req.AddParameter("@LastName", r.LastName)
        req.AddParameter("@Address", r.Address)
        req.AddParameter("@Address2", r.Address2)
        req.AddParameter("@City", r.City)
        req.AddParameter("@State", r.State)
        req.AddParameter("@Zip", r.Zip)
        req.AddParameter("@Phone", r.Phone)
        req.AddParameter("@ReferredOn", r.ReferredOn)
        req.AddParameter("@ReferredByID", r.ReferredByID)

        Dim newID As Integer = Data.ExecuteAndReadInteger(req, "New_Row")

        Return newID
    End Function
#End Region

#Region "News Letters"
    Public Shared Function NewsLetter_Add(ByVal email As String) As Boolean
        Dim strQ As String = "INSERT INTO QB_NewsLetter(Email, bUnsubscribe) Values(@Email, @bUnsubscribe) "

        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text

        req.AddParameter("@Email", email)
        req.AddParameter("@bUnsubscribe", 1)

        If Data.ExecuteNonQuery(req) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function NewsLetter_Subscribed(ByVal email As String) As Boolean
        Dim strQ As String = "SElect * From QB_NewsLetter Where Email = @Email"

        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text

        req.AddParameter("@Email", email)

        Dim dt As DataTable = Data.ExecuteQuery(req)
        If dt.Rows.Count = 0 Then Return False
        Return dt.Rows(0)("bUnsubscribe")
    End Function
    Public Shared Function NewsLetter_Update(ByVal email As String, ByVal subscribe As Boolean) As Boolean
        Dim strQ As String = "UPDATE QB_NewsLetter SET bUnsubscribe=@bUnsubscribe WHERE Email=@Email "

        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text

        req.AddParameter("@Email", email)
        req.AddParameter("@bUnsubscribe", 1)

        If Data.ExecuteNonQuery(req) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "Invoice Services"
    Public Shared Function Invoices_RevertBatchNumber(ByVal BatchNumber As Integer) As Integer
        Dim strQ As String = "UPDATE tblOrder SET bUpdatedInQB=NULL, ListID=NULL, UpdatedInQBOn=NULL Where QBBatchNumber=" & BatchNumber
        Return Data.ExecuteNonQuery(strQ)
    End Function
    Public Shared Function Invoices_GetLastBatchNumber() As Integer
        Dim strQ As String = "Select IsNull(Max(QBBatchNumber), 0) as NEW_ID FROM tblOrder"
        Dim req As New DataRequest
        req.CommandType = CommandType.Text
        req.Command = strQ

        Dim result As Integer = Data.ExecuteAndReadInteger(req, "NEW_ID")
        Return result
    End Function
    Public Shared Function Invoices_MarkInvoiceUpdated(ByVal invoiceId As Integer, ByVal ListID As String, ByVal QBBatchNumber As Integer) As Boolean
        Dim req As New DataRequest
        req.CommandType = CommandType.Text
        req.Command = "UPDATE tblOrder SET bUpdatedInQB=1, ListID=@ListID, UpdatedInQBOn=@UpdatedInQBOn, QBBatchNumber=@QBBatchNumber Where fldordernumber=" & invoiceId
        req.AddParameter("@ListId", ListID)
        req.AddParameter("@UpdatedInQBOn", Now)
        req.AddParameter("@QBBatchNumber", QBBatchNumber)
        If Data.ExecuteNonQuery(req) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function Invoices_GetInvoice(ByVal id As Integer) As Invoice
        Dim req As New DataRequest
        req.CommandType = CommandType.Text
        req.Command = "Select * From tblOrder Where fldordernumber=" & id

        Dim dt As DataTable = Data.ExecuteQuery(req)
        If dt.Rows.Count = 0 Then Return Nothing
        Return DataRowToInvoice(dt.Rows(0))
    End Function
    Public Shared Function Invoices_GetInvoicesByCustomerName(ByVal CustomerName As String, ByVal orderDateFrom As DateTime, ByVal orderDateTo As DateTime) As Generic.List(Of Invoice)
        Dim req As New DataRequest
        req.CommandType = CommandType.Text
        req.Command = "Select * FROM tblOrder WHERE fldREquestName = '" & CustomerName.Replace("'", "''") & "' "
        req.Command += " AND fldOrderdate>='" & orderDateFrom.ToShortDateString & " 12:00 AM' AND fldOrderdate<='" & orderDateTo.ToShortDateString & " 11:59 PM'"

        Dim dt As DataTable = Data.ExecuteQuery(req)
        Dim list As New Generic.List(Of Invoice)
        For Each row As DataRow In dt.Rows
            list.Add(DataRowToInvoice(row))
        Next
        Return list
    End Function
    Public Shared Function Invoices_GetInvoicesByListID(ByVal ListID As String) As Generic.List(Of Invoice)
        Dim req As New DataRequest
        req.CommandType = CommandType.Text
        req.Command = "Select * FROM tblOrder WHERE ListID = '" & ListID.Trim & "' "

        Dim dt As DataTable = Data.ExecuteQuery(req)
        Dim list As New Generic.List(Of Invoice)
        For Each row As DataRow In dt.Rows
            list.Add(DataRowToInvoice(row))
        Next
        Return list
    End Function
    Private Shared Function DataRowToInvoice(ByVal dr As DataRow) As Invoice
        Dim inv As New Invoice
        With inv
            If Not dr("fldordernumber") Is DBNull.Value Then .OrderNumber = dr("fldordernumber")
            If Not dr("fldListid") Is DBNull.Value Then .ListId = dr("fldListid")
            If Not dr("fldlisttype") Is DBNull.Value Then .ListType = dr("fldlisttype")
            If Not dr("fldCompanyID") Is DBNull.Value Then .CompanyID = dr("fldCompanyID")
            If Not dr("fldcustomeriD") Is DBNull.Value Then .CustomeriD = dr("fldcustomeriD")
            If Not dr("fldrequestname") Is DBNull.Value Then .RequestName = dr("fldrequestname")
            If Not dr("fldsecondname") Is DBNull.Value Then .SecondName = dr("fldsecondname")
            If Not dr("fldssnno") Is DBNull.Value Then .SSNNo = dr("fldssnno")

            If Not dr("fldTaxyear2004") Is DBNull.Value Then .Taxyear2004 = dr("fldTaxyear2004")
            If Not dr("fldtaxyear2003") Is DBNull.Value Then .TaxYear2003 = dr("fldtaxyear2003")
            If Not dr("fldtaxyear2002") Is DBNull.Value Then .TaxYear2002 = dr("fldtaxyear2002")
            If Not dr("fldtaxyear2001") Is DBNull.Value Then .TaxYear2001 = dr("fldtaxyear2001")
            If Not dr("fldtaxyear2000") Is DBNull.Value Then .TaxYear2000 = dr("fldtaxyear2000")

            If Not dr("fldtypeofform") Is DBNull.Value Then .typeOfForm = Val(dr("fldtypeofform"))
            If Not dr("fldemail") Is DBNull.Value Then .Email = dr("fldemail")
            If Not dr("fldfax") Is DBNull.Value Then .Fax = dr("fldfax")
            If Not dr("fldfaxno") Is DBNull.Value Then .Faxno = dr("fldfaxno")
            If Not dr("fldstatus") Is DBNull.Value Then .Status = dr("fldstatus")
            If Not dr("fldDOB") Is DBNull.Value Then .DOB = dr("fldDOB")
            If Not dr("fldSex") Is DBNull.Value Then .Sex = dr("fldSex")
            If Not dr("fldbillingstatus") Is DBNull.Value Then .BillingStatus = dr("fldbillingstatus")
            If Not dr("flddeliverydate") Is DBNull.Value Then .DeliveryDate = dr("flddeliverydate")
            If Not dr("fldPdf") Is DBNull.Value Then .PDF = dr("fldPdf")
            If Not dr("fldOrderdate") Is DBNull.Value Then .Orderdate = dr("fldOrderdate")
            If Not dr("fldSpecialFlag") Is DBNull.Value Then .SpecialFlag = dr("fldSpecialFlag")
            If Not dr("ListID") Is DBNull.Value Then .ListIDQuickBooks = dr("ListID")

            If Not dr("fldLoanNumber") Is DBNull.Value Then .fldLoanNumber = dr("fldLoanNumber")

            If Not dr("fldTaxyear2005") Is DBNull.Value Then .Taxyear2005 = dr("fldTaxyear2005")
            If Not dr("fldTaxyear2006") Is DBNull.Value Then .Taxyear2006 = dr("fldTaxyear2006")
            If Not dr("fldTaxyear2007") Is DBNull.Value Then .Taxyear2007 = dr("fldTaxyear2007")
            If Not dr("fldTaxyear2008") Is DBNull.Value Then .Taxyear2008 = dr("fldTaxyear2008")
            If Not dr("fldTaxyear2009") Is DBNull.Value Then .Taxyear2009 = dr("fldTaxyear2009")
            If Not dr("fldTaxyear2010") Is DBNull.Value Then .Taxyear2010 = dr("fldTaxyear2010")
            If Not dr("fldTaxyear2011") Is DBNull.Value Then .Taxyear2011 = dr("fldTaxyear2011")
            If Not dr("fldTaxyear2012") Is DBNull.Value Then .Taxyear2012 = dr("fldTaxyear2012")
            If Not dr("fldTaxyear2013") Is DBNull.Value Then .Taxyear2013 = dr("fldTaxyear2013")
            If Not dr("fldTaxyear2014") Is DBNull.Value Then .Taxyear2014 = dr("fldTaxyear2014")
            If Not dr("fldTaxyear2015") Is DBNull.Value Then .Taxyear2015 = dr("fldTaxyear2015")
            If Not dr("fldTaxyear2016") Is DBNull.Value Then .Taxyear2016 = dr("fldTaxyear2016")
            If Not dr("fldTaxyear2017") Is DBNull.Value Then .Taxyear2017 = dr("fldTaxyear2017")
            If Not dr("fldTaxyear2018") Is DBNull.Value Then .Taxyear2018 = dr("fldTaxyear2018")

            If Not dr("fldTaxyear2019") Is DBNull.Value Then .Taxyear2019 = dr("fldTaxyear2019")
            If Not dr("fldTaxyear2020") Is DBNull.Value Then .Taxyear2020 = dr("fldTaxyear2020")
            If Not dr("fldTaxyear2021") Is DBNull.Value Then .Taxyear2021 = dr("fldTaxyear2021")
            If Not dr("fldTaxyear2022") Is DBNull.Value Then .Taxyear2022 = dr("fldTaxyear2022")
            If Not dr("fldTaxyear2023") Is DBNull.Value Then .Taxyear2023 = dr("fldTaxyear2023")
            If Not dr("fldTaxyear2024") Is DBNull.Value Then .Taxyear2024 = dr("fldTaxyear2024")
            If Not dr("fldTaxyear2025") Is DBNull.Value Then .Taxyear2025 = dr("fldTaxyear2025")

            Dim strQ As String = "Select companyname,name from customer where customerid =" & .CompanyID
            Dim dt2 As DataTable = Data.ExecuteQuery(strQ)
            If dt2.Rows.Count > 0 Then
                If dt2.Rows(0)("name") Is DBNull.Value Then
                    .Processor = ""
                Else
                    .Processor = dt2.Rows(0)("name")
                End If
            End If

        End With
        Return inv
    End Function
    'Private Shared Function DataTableToInvoice(ByVal dt As DataTable) As Invoice
    '    If dt.Rows.Count = 0 Then Return Nothing

    '    Dim inv As New Invoice
    '    With inv

    '        If Not dt.Rows(0)("fldordernumber") Is DBNull.Value Then .OrderNumber = dt.Rows(0)("fldordernumber")
    '        If Not dt.Rows(0)("fldListid") Is DBNull.Value Then .ListId = dt.Rows(0)("fldListid")
    '        If Not dt.Rows(0)("fldlisttype") Is DBNull.Value Then .ListType = dt.Rows(0)("fldlisttype")
    '        If Not dt.Rows(0)("fldCompanyID") Is DBNull.Value Then .CompanyID = dt.Rows(0)("fldCompanyID")
    '        If Not dt.Rows(0)("fldcustomeriD") Is DBNull.Value Then .CustomeriD = dt.Rows(0)("fldcustomeriD")
    '        If Not dt.Rows(0)("fldrequestname") Is DBNull.Value Then .RequestName = dt.Rows(0)("fldrequestname")
    '        If Not dt.Rows(0)("fldsecondname") Is DBNull.Value Then .SecondName = dt.Rows(0)("fldsecondname")
    '        If Not dt.Rows(0)("fldssnno") Is DBNull.Value Then .SSNNo = dt.Rows(0)("fldssnno")

    '        If Not dt.Rows(0)("fldTaxyear2004") Is DBNull.Value Then .Taxyear2004 = dt.Rows(0)("fldTaxyear2004")
    '        If Not dt.Rows(0)("fldtaxyear2003") Is DBNull.Value Then .TaxYear2003 = dt.Rows(0)("fldtaxyear2003")
    '        If Not dt.Rows(0)("fldtaxyear2002") Is DBNull.Value Then .TaxYear2002 = dt.Rows(0)("fldtaxyear2002")
    '        If Not dt.Rows(0)("fldtaxyear2001") Is DBNull.Value Then .TaxYear2001 = dt.Rows(0)("fldtaxyear2001")
    '        If Not dt.Rows(0)("fldtaxyear2000") Is DBNull.Value Then .TaxYear2000 = dt.Rows(0)("fldtaxyear2000")

    '        If Not dt.Rows(0)("fldtypeofform") Is DBNull.Value Then .typeOfForm = Val(dt.Rows(0)("fldtypeofform"))
    '        If Not dt.Rows(0)("fldemail") Is DBNull.Value Then .Email = dt.Rows(0)("fldemail")
    '        If Not dt.Rows(0)("fldfax") Is DBNull.Value Then .Fax = dt.Rows(0)("fldfax")
    '        If Not dt.Rows(0)("fldfaxno") Is DBNull.Value Then .Faxno = dt.Rows(0)("fldfaxno")
    '        If Not dt.Rows(0)("fldstatus") Is DBNull.Value Then .Status = dt.Rows(0)("fldstatus")
    '        If Not dt.Rows(0)("fldDOB") Is DBNull.Value Then .DOB = dt.Rows(0)("fldDOB")
    '        If Not dt.Rows(0)("fldSex") Is DBNull.Value Then .Sex = dt.Rows(0)("fldSex")
    '        If Not dt.Rows(0)("fldbillingstatus") Is DBNull.Value Then .BillingStatus = dt.Rows(0)("fldbillingstatus")
    '        If Not dt.Rows(0)("flddeliverydate") Is DBNull.Value Then .DeliveryDate = dt.Rows(0)("flddeliverydate")
    '        If Not dt.Rows(0)("fldPdf") Is DBNull.Value Then .PDF = dt.Rows(0)("fldPdf")
    '        If Not dt.Rows(0)("fldOrderdate") Is DBNull.Value Then .Orderdate = dt.Rows(0)("fldOrderdate")
    '        If Not dt.Rows(0)("fldSpecialFlag") Is DBNull.Value Then .SpecialFlag = dt.Rows(0)("fldSpecialFlag")
    '        If Not dt.Rows(0)("ListID") Is DBNull.Value Then .ListIDQuickBooks = dt.Rows(0)("ListID")


    '        If Not dt.Rows(0)("fldTaxyear2005") Is DBNull.Value Then .Taxyear2005 = dt.Rows(0)("fldTaxyear2005")
    '        If Not dt.Rows(0)("fldTaxyear2006") Is DBNull.Value Then .Taxyear2006 = dt.Rows(0)("fldTaxyear2006")
    '        If Not dt.Rows(0)("fldTaxyear2007") Is DBNull.Value Then .Taxyear2007 = dt.Rows(0)("fldTaxyear2007")
    '        If Not dt.Rows(0)("fldTaxyear2008") Is DBNull.Value Then .Taxyear2008 = dt.Rows(0)("fldTaxyear2008")
    '        If Not dt.Rows(0)("fldTaxyear2009") Is DBNull.Value Then .Taxyear2009 = dt.Rows(0)("fldTaxyear2009")
    '        If Not dt.Rows(0)("fldTaxyear2010") Is DBNull.Value Then .Taxyear2010 = dt.Rows(0)("fldTaxyear2010")
    '        If Not dt.Rows(0)("fldLoanNumber") Is DBNull.Value Then .fldLoanNumber = dt.Rows(0)("fldLoanNumber")


    '        Dim strQ As String = "Select companyname,name from customer where customerid =" & .CompanyID
    '        Dim dt2 As DataTable = Data.ExecuteQuery(strQ)
    '        If dt2.Rows.Count > 0 Then
    '            If dt2.Rows(0)("name") Is DBNull.Value Then
    '                .Processor = ""
    '            Else
    '                .Processor = dt2.Rows(0)("name")
    '            End If
    '        End If

    '    End With
    '    Return inv
    'End Function
    Public Shared Function Invoices_GetCompaniesWithPendingInvoices(ByVal dateFrom As DateTime, ByVal dateTo As DateTime) As DataTable
        Dim strQ As String = "select distinct(fldCompanyid) as companyid from tblorder "
        strQ += " where fldOrderdate>=@DateFrom and fldOrderdate<=@DateTo "
        strQ += " AND (ListID IS NULL OR ListID='') "

        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text

        req.AddParameter("@DateFrom", dateFrom.ToShortDateString & " 12:00 AM")
        req.AddParameter("@DateTo", dateTo.ToShortDateString & " 11:59 PM")

        Dim dt As DataTable = Data.ExecuteQuery(req)

        Return dt
    End Function
    Public Shared Function Invoics_GetCompanyInvoices(ByVal dateFrom As DateTime, ByVal dateTo As DateTime, ByVal CompanyID As Integer) As DataTable
        Dim strQ As String = "Select fldordernumber,fldcustomerid,fldOrderdate,fldDeliverydate,fldtypeofform,fldrequestname,fldsecondname,fldbillingstatus,fldcompanyid from tblorder "
        If CompanyID = 9711 Then
            strQ += " Where (fldCompanyID = @CompanyID OR fldCompanyID IN (SELECT DISTINCT CustomerID FROM Customer WHERE CustomerID= 9711 OR ParentID = 9711))"
        Else
            strQ += " Where fldCompanyID = @CompanyID "
        End If

        strQ += " and (ListID Is NULL OR ListID='')"
        strQ += " AND fldOrderdate >=@DateFrom and fldOrderdate <= @DateTo "
        'strQ += " order by fldDeliverydate"
        strQ += " Order By fldOrderDate"

        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text

        req.AddParameter("@DateFrom", dateFrom.ToShortDateString & " 12:00 AM")
        req.AddParameter("@DateTo", dateTo.ToShortDateString & " 11:59 PM")
        req.AddParameter("@CompanyID", CompanyID)

        Dim dt As DataTable = Data.ExecuteQuery(req)

        Return dt
    End Function
    Public Shared Function Invoices_GetTotalCompanyInvoices(ByVal dateFrom As DateTime, ByVal dateTo As DateTime, ByVal CompanyId As Integer) As Integer
        Dim strQ As String = "select Count(*) as TotalRecords from tblorder "
        strQ += " Where fldCompanyID = @CompanyID and (ListID IS NULL or ListID ='')"
        strQ += "  AND fldOrderdate >=@DateFrom and fldOrderdate <= @DateTo "


        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text

        req.AddParameter("@DateFrom", dateFrom.ToShortDateString & " 12:00 AM")
        req.AddParameter("@DateTo", dateTo.ToShortDateString & " 11:59 PM")
        req.AddParameter("@CompanyID", CompanyId)

        Dim dt As DataTable = Data.ExecuteQuery(req)
        If dt.Rows.Count = 0 Then Return 0
        If dt.Rows(0)("TotalRecords") Is DBNull.Value Then Return 0
        Return CInt(dt.Rows(0)("TotalRecords"))
    End Function
#End Region



#Region "Application Settings"
    Public Shared Function ApplicationSetting_Delete(ByVal SettingName As String) As Boolean
        Dim req As New DataRequest
        req.Command = "DELETE FROM WebAppSettings  Where SettingName=@SettingName"
        req.CommandType = CommandType.Text

        req.AddParameter("@SettingName", SettingName)

        Return Data.ExecuteNonQuery(req) > 0
    End Function
    Public Shared Function ApplicationSetting_Get(ByVal SettingName As String) As String
        Dim req As New DataRequest
        req.Command = "SELECT * FROM WebAppSettings  Where SettingName=@SettingName"
        req.CommandType = CommandType.Text

        req.AddParameter("@SettingName", SettingName)

        Dim dt As DataTable = Data.ExecuteDataSet(req).Tables(0)
        If dt.Rows.Count = 0 Then Return String.Empty
        Return dt.Rows(0)("SettingValue").ToString
    End Function
    Public Shared Function ApplicationSetting_Update(ByVal SettingName As String, ByVal SettingValue As String) As Boolean
        Dim req As New DataRequest
        Dim oldValue As String = ApplicationSetting_Get(SettingName)
        req.Command = "DELETE FROM WebAppSettings WHERE SettingName = @SettingName; " & vbCrLf
        req.Command += "INSERT INTO WebAppSettings (SettingName, SettingValue) VALUES(@SettingName, @SettingValue)"


        req.CommandType = CommandType.Text


        req.AddParameter("@SettingName", SettingName)
        req.AddParameter("@SettingValue", SettingValue)

        If Data.ExecuteNonQuery(req) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function ApplicationSetting_ListAll() As DataTable
        Dim strQ As String = "Select * FROM WebAppSettings Order By SettingName"
        Dim req As New DataRequest
        req.CommandType = CommandType.Text
        req.Command = strQ
        Return Data.ExecuteDataSet(req).Tables(0)
    End Function
#End Region

#Region "Order PAD"
    Public Shared Function OrderPadFile_AddNew(ByVal UserID As Integer, ByVal UploadedOn As DateTime, ByVal FileName As String, ByVal FileNameReal As String, ByVal ErrorMessage As String) As Integer
        Dim strQ As String = "INSERT INTO OrderPadFile(UserID, UploadedOn, FileName, FileNameReal, ErrorMessage) "
        strQ += " Values (@UserID, @UploadedOn, @FileName, @FileNameReal, @ErrorMessage)" & vbCrLf
        strQ += " SELECT SCOPE_IDENTITY() AS NEW_ID"

        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text

        req.AddParameter("@UserID", UserID)
        req.AddParameter("@UploadedOn", UploadedOn)
        req.AddParameter("@FileName", FileName)
        req.AddParameter("@FileNameReal", FileNameReal)
        req.AddParameter("@ErrorMessage", ErrorMessage)


        Return Data.ExecuteAndReadInteger(req, "NEW_ID")
    End Function
    Public Shared Function OrderPadFile_Update(ByVal ID As Integer, ByVal UserID As Integer, ByVal UploadedOn As DateTime, ByVal FileName As String, ByVal FileNameReal As String, ByVal ErrorMessage As String) As Integer
        Dim strQ As String = "UPDATE OrderPadFile SET "
        strQ += " UserID = @UserID, "
        strQ += " UploadedOn = @UploadedOn, "
        strQ += " FileName = @FileName, "
        strQ += " FileNameReal = @FileNameReal, "
        strQ += " ErrorMessage = @ErrorMessage "
        strQ += " WHERE ID=@ID"

        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text

        req.AddParameter("@ID", ID)
        req.AddParameter("@UserID", UserID)
        req.AddParameter("@UploadedOn", UploadedOn)
        req.AddParameter("@FileName", FileName)
        req.AddParameter("@FileNameReal", FileNameReal)
        req.AddParameter("@ErrorMessage", ErrorMessage)


        If Data.ExecuteNonQuery(req) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function OrderPadFile_GetByID(ByVal Id As Integer) As DataTable
        Dim strQ As String = "SELECT * FROM OrderPadFile WHERE ID = " & Id
        Return DataHelper.ExecuteQuery(strQ)
    End Function
#End Region
#Region "List Type"
    Public Shared Function List_GetList(ByVal ID As Integer) As DataTable
        Dim strQ As String = "SELECT * from tbllist WHERE fldlistid=" & ID
        Return DataHelper.ExecuteQuery(strQ)
    End Function
    Public Shared Function List_AddNewList(ByVal fldlisttype As Integer, ByVal fldListname As String, ByVal fldCurrentdate As String, ByVal fldDateCheck As DateTime) As Integer
        Dim strQ As String = "INSERT INTO tbllist (fldlisttype, fldListname, fldCurrentdate , fldDateCheck )"
        strQ += " Values(@fldlisttype, @fldListname, @fldCurrentdate, @fldDateCheck )" & vbCrLf
        strQ += " SELECT SCOPE_IDENTITY() AS NEW_ID"

        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text

        req.AddParameter("@fldlisttype", fldlisttype)
        req.AddParameter("@fldListname", fldListname)
        req.AddParameter("@fldCurrentdate", fldCurrentdate)
        req.AddParameter("@fldDateCheck", fldDateCheck)


        Dim result As Integer = Data.ExecuteAndReadInteger(req, "NEW_ID")
        Return result
    End Function
    Public Shared Function List_UpdateList(ByVal fldlistid As Integer, ByVal fldlisttype As Integer, ByVal fldListname As String, ByVal fldCurrentdate As String, ByVal fldDateCheck As DateTime, ByVal IRSBatchNumber As String) As Boolean
        Dim strQ As String = "UPDATE tbllist SET "
        strQ += " fldlisttype = @fldlisttype, fldListname = @fldListname, fldCurrentdate = @fldCurrentdate, fldDateCheck = @fldDateCheck, IRSBatchNumber = @IRSBatchNumber"
        strQ += " WHERE fldlistid = @fldlistid"

        Dim req As New DataRequest
        req.Command = strQ
        req.CommandType = CommandType.Text

        req.AddParameter("@fldlistid", fldlistid)
        req.AddParameter("@fldlisttype", fldlisttype)
        req.AddParameter("@fldListname", fldListname)
        req.AddParameter("@fldCurrentdate", fldCurrentdate)
        req.AddParameter("@fldDateCheck", fldDateCheck)
        req.AddParameter("@IRSBatchNumber", IRSBatchNumber)



        If Data.ExecuteNonQuery(req) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "Clients"
    Public Shared Function Client_AddNew(ByVal ClientName As String, ByVal LoginUserName As String, ByVal LoginPassword As String, ByVal AddStatistics As Boolean) As Integer
        Dim req As New DataRequest
        req.Command = "Client_i"
        req.CommandType = CommandType.StoredProcedure

        req.AddParameter("@ClientName", ClientName)
        req.AddParameter("@LoginUserName", LoginUserName)
        req.AddParameter("@LoginPassword", LoginPassword)
        req.AddParameter("@AddStatistics", AddStatistics)


        Return Data.ExecuteAndReadInteger(req, "NEW_ID")
    End Function
    Public Shared Function Client_Update(ByVal ID As Integer, ByVal ClientName As String, ByVal LoginUserName As String, ByVal LoginPassword As String, ByVal AddStatistics As Boolean) As Boolean
        Dim req As New DataRequest
        req.Command = "Client_u"
        req.CommandType = CommandType.StoredProcedure

        req.AddParameter("@ID", ID)
        req.AddParameter("@ClientName", ClientName)
        req.AddParameter("@LoginUserName", LoginUserName)
        req.AddParameter("@LoginPassword", LoginPassword)
        req.AddParameter("@AddStatistics", AddStatistics)


        Return Data.ExecuteNonQuery(req) > 0
    End Function
#End Region
End Class