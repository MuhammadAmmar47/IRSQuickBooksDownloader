'Public Class UploadFunctions
'    Private data As SQLServerDataHelper = Nothing
'    Public Sub New()
'        data = New SQLServerDataHelper(AppSettings.ConnectionString)
'    End Sub
'    'Public Function GetCustomerFromListId(ByVal listID As String) As Integer
'    '    Dim strQ As String = "Select Id From QB_Customers Where ListID=@ListID"
'    '    Dim req As New DataRequest
'    '    req.CommandType = CommandType.Text
'    '    req.Command = strQ
'    '    req.AddParameter("@ListID", listID)

'    '    Dim dt As DataTable = data.ExecuteQuery(req)
'    '    If dt.Rows.Count = 0 Then Return -1
'    '    Return dt.Rows(0)("ID")
'    'End Function

'    'Public Function InsertCustomer(ByVal c As Customer) As Integer
'    '    Dim id As Integer = GetCustomerFromListId(c.ListId)
'    '    If id > 1 Then
'    '        Return id
'    '    End If

'    '    Dim strQ As String = ""
'    '    'New Customer
'    '    strQ = "INSERT INTO QB_Customers(ListID, UserId, Password, CreatedOn, ModifiedOn, FirstName, LastName, Address1, Address2, City, State, ZipCode, PaymentMethod, bAdmin) Values"
'    '    strQ += " (@ListID, @UserId, @Password, @CreatedOn, @ModifiedOn, @FirstName, @LastName, @Address1, @Address2, @City, @State, @ZipCode, @PaymentMethod, @bAdmin); "
'    '    strQ += "Select @@IDENTITY as NewId"

'    '    Dim req As New DataRequest
'    '    req.Command = strQ
'    '    req.CommandType = CommandType.Text

'    '    req.AddParameter("@ListID", c.ListId)
'    '    req.AddParameter("@UserId", c.UserName)
'    '    req.AddParameter("@Password", c.Password)
'    '    req.AddParameter("@CreatedOn", c.CreatedOn)
'    '    req.AddParameter("@ModifiedOn", c.ModifiedOn)
'    '    req.AddParameter("@FirstName", c.FirstName)
'    '    req.AddParameter("@LastName", c.LastName)
'    '    req.AddParameter("@Address1", c.Address1)
'    '    req.AddParameter("@Address2", c.Address2)
'    '    req.AddParameter("@City", c.City)
'    '    req.AddParameter("@State", c.State)
'    '    req.AddParameter("@ZipCode", c.ZipCode)
'    '    req.AddParameter("@PaymentMethod", c.PaymentMethod)
'    '    req.AddParameter("@bAdmin", c.IsAdmin)


'    '    id = data.ExecuteAndReadInteger(req, "NewId")


'    '    Return id

'    'End Function

'    'Public Function UpdateCustomer(ByVal c As Customer) As Integer

'    '    Dim strQ As String = ""
'    '    'New Customer
'    '    strQ = "UPDATE QB_Customers SET "
'    '    strQ += " UserId=@UserId, Password=@Password, FirstName=@FirstName, LastName=@LastName, Address1=@Address1, Address2=@Address2, City=@City, State=@State, ZipCode=@ZipCode, PaymentMethod=@PaymentMethod, bAdmin=@bAdmin "
'    '    strQ += " WHERE ID=@ID"

'    '    Dim req As New DataRequest
'    '    req.Command = strQ
'    '    req.CommandType = CommandType.Text

'    '    req.AddParameter("@UserId", c.UserName)
'    '    req.AddParameter("@Password", c.Password)
'    '    req.AddParameter("@FirstName", c.FirstName)
'    '    req.AddParameter("@LastName", c.LastName)
'    '    req.AddParameter("@Address1", c.Address1)
'    '    req.AddParameter("@Address2", c.Address2)
'    '    req.AddParameter("@City", c.City)
'    '    req.AddParameter("@State", c.State)
'    '    req.AddParameter("@ZipCode", c.ZipCode)
'    '    req.AddParameter("@PaymentMethod", c.PaymentMethod)

'    '    req.AddParameter("@ID", c.ID)
'    '    req.AddParameter("@bAdmin", c.IsAdmin)


'    '    Return data.ExecuteNonQuery(req)




'    'End Function

'    'Public Function GetInvoiceByTransactionId(ByVal transId As String, ByVal customerId As Integer) As Integer
'    '    Dim strQ As String = "Select ID From QB_Invoices Where TransactionId = @TransactionId AND CustomerID=@CustomerID"

'    '    Dim req As New DataRequest
'    '    req.CommandType = CommandType.Text
'    '    req.Command = strQ
'    '    req.AddParameter("@TransactionId", transId)
'    '    req.AddParameter("@CustomerID", customerId)

'    '    Dim dt As DataTable = data.ExecuteQuery(req)
'    '    If dt.Rows.Count = 0 Then Return -1
'    '    Return dt.Rows(0)("ID")

'    'End Function
'    'Public Function InsertInvoice(ByVal i As Invoice) As Boolean
'    '    Dim id As Integer = GetInvoiceByTransactionId(i.TransactionID, i.CustomerId)
'    '    If id > 0 Then
'    '        Return True 'Invoice already added
'    '    End If

'    '    Dim strQ As String = "INSERT INTO QB_Invoices (CustomerID, TransactionId, CreatedOn, ModifiedOn, TransactionNo, InvoiceNumber, ClassRefName, AccountRef, TransactionDate, DueDate, SubTotal, SalesTaxTotal, AppliedAmount, BalanceRemaining, IsPaid) values ("
'    '    strQ += "@CustomerID, @TransactionId, @CreatedOn, @ModifiedOn, @TransactionNo, @InvoiceNumber, @ClassRefName, @AccountRef, @TransactionDate, @DueDate, @SubTotal, @SalesTaxTotal, @AppliedAmount, @BalanceRemaining, @IsPaid)"


'    '    Dim req As New DataRequest
'    '    req.Command = strQ
'    '    req.CommandType = CommandType.Text

'    '    req.AddParameter("@CustomerID", i.CustomerId)
'    '    req.AddParameter("@TransactionId", i.TransactionID)
'    '    req.AddParameter("@CreatedOn", i.CreatedOn)
'    '    req.AddParameter("@ModifiedOn", i.ModifiedOn)
'    '    req.AddParameter("@TransactionNo", i.TransactionNo)
'    '    req.AddParameter("@InvoiceNumber", i.InvoiceNumber)
'    '    req.AddParameter("@ClassRefName", i.ClassRefName)
'    '    req.AddParameter("@AccountRef", i.AccountRef)
'    '    req.AddParameter("@TransactionDate", i.TransactionDate)
'    '    req.AddParameter("@DueDate", i.DueDate)
'    '    req.AddParameter("@SubTotal", i.SubTotal)
'    '    req.AddParameter("@SalesTaxTotal", i.SalesTaxTotal)
'    '    req.AddParameter("@AppliedAmount", i.AppliedAmount)
'    '    req.AddParameter("@BalanceRemaining", i.BalanceRemaining)
'    '    req.AddParameter("@IsPaid", i.IsPaid)



'    '    If data.ExecuteNonQuery(req) > 0 Then
'    '        Return True
'    '    Else
'    '        Return False
'    '    End If
'    'End Function
'End Class
