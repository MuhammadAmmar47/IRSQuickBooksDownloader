Imports QBXMLRP2Lib
Imports System.Xml
Imports System.Windows.Forms
Imports IRSTaxRecords.QBHelper.QBWebService
'Imports IRSTaxRecords.QBHelper.QBServiceLocal

Public Class QuickBooksProcessor

    Private objQBProcessor As QBXMLRP2Lib.RequestProcessor2 = Nothing
    Private Const Application_Name As String = "IRS Tax Records Application"
    Private Const Application_ID As String = "IRS Tax Records Application"

    Private _Isconnected As Boolean
    Private _QBTicket As String = ""
    Private _LastError As String

    Public ReadOnly Property LastError() As String
        Get
            Return _LastError
        End Get
    End Property
    Public ReadOnly Property IsConnected() As Boolean
        Get
            Return _Isconnected
        End Get
    End Property
    Private ReadOnly Property QBTicket() As String
        Get
            Return _QBTicket
        End Get
    End Property


    Private Sub ClearErrors()
        _LastError = ""
    End Sub
    Public Function Connect() As Boolean
        If IsConnected Then Return True
        _LastError = ""
        Dim connPref As QBXMLRP2Lib.QBXMLRPConnectionType = QBXMLRPConnectionType.localQBD
        objQBProcessor = New RequestProcessor2

        objQBProcessor.OpenConnection2(Application_ID, Application_Name, connPref)

        Dim auth As AuthPreferences = objQBProcessor.AuthPreferences
        Dim authFlags As Integer
        authFlags = authFlags Or &H1&
        authFlags = authFlags Or &H2&
        authFlags = authFlags Or &H4&
        authFlags = authFlags Or &H8&

        auth.PutAuthFlags(authFlags)
        Try
            _QBTicket = objQBProcessor.BeginSession("", QBFileMode.qbFileOpenDoNotCare)
        Catch ex As Exception
            _LastError = ex.Message
            Return False
        End Try

        _Isconnected = True
        Return True
    End Function
    Public Sub Disconnect()
        If _Isconnected Then
            objQBProcessor.EndSession(QBTicket)
            objQBProcessor.CloseConnection()
            _Isconnected = False
        End If

    End Sub
    Private Function IsErrorResponse(ByVal respNode As XmlNode) As Boolean
        If respNode Is Nothing Then Return True
        If respNode.Attributes("statusCode").Value = "0" AndAlso respNode.Attributes("statusMessage").Value = "Status OK" Then Return False
        'Otherwise, Error
        Return True
    End Function
    Public Function GetAllCustomers() As ArrayList
        If Not IsConnected Then Connect()
        If Not IsConnected Then Return Nothing
        ClearErrors()
        Application.DoEvents()

        Dim customerRequest As String = mdlXMLBuilder.BuildQuery_CustomersList
        Dim resp As String = ""
        Try
            Application.DoEvents()
            resp = objQBProcessor.ProcessRequest(QBTicket, customerRequest)
            Application.DoEvents()
            If resp Is Nothing OrElse resp.Trim = "" Then Throw New Exception("Trash response from quick books")
            Application.DoEvents()
        Catch ex As Exception
            _LastError = ex.Message
            Return Nothing
        End Try

        Dim xmlDoc As New XmlDocument
        xmlDoc.LoadXml(resp)
        Dim node As XmlNode = Nothing
        Try
            Application.DoEvents()
            node = xmlDoc.GetElementsByTagName("CustomerQueryRs").Item(0)
            Application.DoEvents()
        Catch ex As Exception
            _LastError = "Failed to load root node. " & ex.Message
            Return Nothing
        End Try
        'Check if there was any Error
        If IsErrorResponse(node) Then Return Nothing
        Dim arr As New ArrayList
        For Each childNode As XmlNode In node.ChildNodes
            Application.DoEvents()
            arr.Add(GetCustomerFromNode(childNode))
            Application.DoEvents()
        Next
        Return arr
    End Function
    Public Function GetCustomer(ByVal Name As String) As Customer
        If Not IsConnected Then Connect()
        If Not IsConnected Then Return Nothing
        ClearErrors()
        Application.DoEvents()

        Dim customerRequest As String = mdlXMLBuilder.BuildQuery_GetCustomer(Name)
        Dim resp As String = ""
        Try
            Application.DoEvents()
            resp = objQBProcessor.ProcessRequest(QBTicket, customerRequest)
            Application.DoEvents()
            If resp Is Nothing OrElse resp.Trim = "" Then Throw New Exception("Trash response from quick books")
            Application.DoEvents()
        Catch ex As Exception
            _LastError = ex.Message
            Return Nothing
        End Try

        Dim xmlDoc As New XmlDocument
        xmlDoc.LoadXml(resp)
        Dim node As XmlNode = Nothing
        Try
            Application.DoEvents()
            node = xmlDoc.GetElementsByTagName("CustomerQueryRs").Item(0)
            Application.DoEvents()
        Catch ex As Exception
            _LastError = "Failed to load root node. " & ex.Message
            Return Nothing
        End Try
        'Check if there was any Error
        If IsErrorResponse(node) Then Return Nothing
        Dim arr As New ArrayList
        For Each childNode As XmlNode In node.ChildNodes
            Application.DoEvents()
            arr.Add(GetCustomerFromNode(childNode))
            Application.DoEvents()
        Next
        If arr.Count = 0 Then Return Nothing
        Return CType(arr.Item(0), Customer)

    End Function
    Public Function GetCustomerFromListID(ByVal ListID As String) As Customer
        If Not IsConnected Then Connect()
        If Not IsConnected Then Return Nothing
        ClearErrors()
        Application.DoEvents()

        Dim customerRequest As String = mdlXMLBuilder.BuildQuery_GetCustomerFromListID(ListID)
        Dim resp As String = ""
        Try
            Application.DoEvents()
            resp = objQBProcessor.ProcessRequest(QBTicket, customerRequest)
            Application.DoEvents()
            If resp Is Nothing OrElse resp.Trim = "" Then Throw New Exception("Trash response from quick books")
            Application.DoEvents()
        Catch ex As Exception
            _LastError = ex.Message
            Return Nothing
        End Try

        Dim xmlDoc As New XmlDocument
        xmlDoc.LoadXml(resp)
        Dim node As XmlNode = Nothing
        Try
            Application.DoEvents()
            node = xmlDoc.GetElementsByTagName("CustomerQueryRs").Item(0)
            Application.DoEvents()
        Catch ex As Exception
            _LastError = "Failed to load root node. " & ex.Message
            Return Nothing
        End Try
        'Check if there was any Error
        If IsErrorResponse(node) Then Return Nothing
        Dim arr As New ArrayList
        For Each childNode As XmlNode In node.ChildNodes
            Application.DoEvents()
            arr.Add(GetCustomerFromNode(childNode))
            Application.DoEvents()
        Next
        If arr.Count = 0 Then Return Nothing
        Return CType(arr.Item(0), Customer)

    End Function
    'Public Function GetAllInvoices(ByVal CustomerListId As String) As ArrayList
    '    If Not IsConnected Then Connect()
    '    If Not IsConnected Then Return Nothing
    '    ClearErrors()

    '    Dim invoiceRequest As String = mdlXMLBuilder.BuildQuery_GetInvoices(CustomerListId)
    '    Dim resp As String = ""
    '    Try
    '        Application.DoEvents()
    '        resp = objQBProcessor.ProcessRequest(QBTicket, invoiceRequest)
    '        Application.DoEvents()
    '        If resp Is Nothing OrElse resp.Trim = "" Then Throw New Exception("Trash response from quick books")
    '    Catch ex As Exception
    '        _LastError = ex.Message
    '        Return Nothing
    '    End Try

    '    Dim xmlDoc As New XmlDocument
    '    Application.DoEvents()
    '    xmlDoc.LoadXml(resp)
    '    Application.DoEvents()
    '    Dim node As XmlNode = Nothing
    '    Try
    '        Application.DoEvents()
    '        node = xmlDoc.GetElementsByTagName("InvoiceQueryRs").Item(0)
    '        Application.DoEvents()
    '    Catch ex As Exception
    '        _LastError = "Failed to load root node. " & ex.Message
    '        Return Nothing
    '    End Try

    '    'Check if there was any error
    '    Application.DoEvents()
    '    If IsErrorResponse(node) Then Return Nothing
    '    Application.DoEvents()

    '    Dim arr As New ArrayList
    '    For Each childNode As XmlNode In node.ChildNodes
    '        Application.DoEvents()
    '        'arr.Add(GetInvoiceFromNode(childNode))
    '        Application.DoEvents()
    '    Next
    '    Return arr
    'End Function

    Public Function PayInvoice(ByVal customerListId As String, ByVal paidOn As DateTime, ByVal refNo As String, ByVal amount As Decimal, ByVal paymentMethod As String, ByVal invoiceId As String) As Boolean

        If Not IsConnected Then Connect()
        If Not IsConnected Then Return Nothing
        ClearErrors()

        Application.DoEvents()
        Dim query As String = mdlXMLBuilder.BuildQuery_InvoicePayment(customerListId, paidOn, refNo, amount, paymentMethod, invoiceId)
        Application.DoEvents()

        Dim resp As String = ""
        Try
            Application.DoEvents()
            resp = objQBProcessor.ProcessRequest(QBTicket, query)
            Application.DoEvents()
            If resp Is Nothing OrElse resp.Trim = "" Then Throw New Exception("Trash response from quick books")
        Catch ex As Exception
            _LastError = ex.Message
            Return False
        End Try

        Dim xmlDoc As New XmlDocument
        Application.DoEvents()
        xmlDoc.LoadXml(resp)
        Application.DoEvents()
        Dim node As XmlNode = Nothing
        Try
            Application.DoEvents()
            node = xmlDoc.GetElementsByTagName("ReceivePaymentAddRs").Item(0)
            Application.DoEvents()
        Catch ex As Exception
            _LastError = "Failed to load root node. " & ex.Message
            Return False
        End Try

        'Check if there was any error
        Application.DoEvents()
        If IsErrorResponse(node) Then Return False
        Application.DoEvents()

        Return True
    End Function

    Private Function GetCustomerName(ByVal name As String, ByVal tryNo As Integer) As String
        name = Left(name, 40)
        Dim tempC As Customer = Nothing
        Dim newName As String = HtmlEncode(name)
        If tryNo = 0 Then
            'Try with original name first.
            tempC = GetCustomer(name)
        Else
            newName = name & " " & tryNo
            tempC = GetCustomer(name & " " & tryNo)
        End If
        If tempC Is Nothing Then
            'Customer with this name already exists, 
            Return newName
            'Customer Name found...
        End If
        'Customre already exists with specified name, so try agian with new suffix
        Return GetCustomerName(name, tryNo + 1)
    End Function
    Public Function AddCustomer(ByVal c As Customer, ByRef ListId As String) As Boolean

        If Not IsConnected Then Connect()
        If Not IsConnected Then Return Nothing
        ClearErrors()
        Application.DoEvents()

        c.CompanyName = GetCustomerName(c.CompanyName.Trim, 0)

        Dim customerRequest As String = mdlXMLBuilder.BuildCustomerAddRq(c)
        Dim resp As String = ""
        Try
            Application.DoEvents()
            resp = objQBProcessor.ProcessRequest(QBTicket, customerRequest)
            Application.DoEvents()
            If resp Is Nothing OrElse resp.Trim = "" Then Throw New Exception("Trash response from quick books")
            Application.DoEvents()
        Catch ex As Exception
            _LastError = ex.Message
            Return False
        End Try

        Dim xmlDoc As New XmlDocument
        xmlDoc.LoadXml(resp)
        Dim node As XmlNode = Nothing
        Try
            Application.DoEvents()
            node = xmlDoc.GetElementsByTagName("CustomerAddRs").Item(0)
            Application.DoEvents()
        Catch ex As Exception
            _LastError = "Failed to load root node. " & ex.Message
            Return False
        End Try
        'Check if there was any Error
        If IsErrorResponse(node) Then
            Me._LastError = GetErrorMessage(node)
            Return False
        End If
        Dim arr As New ArrayList
        ListId = node.ChildNodes(0).ChildNodes(0).InnerText
        Return True
    End Function
    Private Function GetErrorMessage(ByVal node As XmlNode) As String
        Return node.Attributes("statusMessage").Value
    End Function

    Public Function GetInvoice(ByVal InvoiceNumber As String) As String
        If Not IsConnected Then Connect()
        If Not IsConnected Then Throw New Exception("Failed to connect to quickbooks. Please make sure quick books software is running and you allow the connection to quickbooks.")
        ClearErrors()
        Application.DoEvents()

        Dim invoiceServiceReq As String = mdlXMLBuilder.BuildQuery_GetInvoice(InvoiceNumber)
        Dim resp As String = ""
        Try
            Application.DoEvents()
            resp = objQBProcessor.ProcessRequest(QBTicket, invoiceServiceReq)
            Application.DoEvents()
            If resp Is Nothing OrElse resp.Trim = "" Then Throw New Exception("Trash response from quick books")
            Application.DoEvents()
        Catch ex As Exception
            _LastError = ex.Message
            Return ""
        End Try

        Dim xmlDoc As New XmlDocument
        xmlDoc.LoadXml(resp)
        Dim node As XmlNode = Nothing
        Try
            Application.DoEvents()
            node = xmlDoc.GetElementsByTagName("InvoiceQueryRs").Item(0)
            Application.DoEvents()
        Catch ex As Exception
            _LastError = "Failed to load root node. " & ex.Message
            Return ""
        End Try
        'Check if there was any Error
        If IsErrorResponse(node) Then
            Me._LastError = GetErrorMessage(node)
            Return ""
        End If
        Dim ListId As String = node.ChildNodes(0).ChildNodes(0).InnerText
        Return resp
    End Function
    Public Function AddInvoiceService(ByVal Name As String, ByVal Desc As String, ByVal Price As Decimal, ByVal accountName As String) As String
        If Not IsConnected Then Connect()
        If Not IsConnected Then Return Nothing
        ClearErrors()
        Application.DoEvents()

        Dim invoiceServiceReq As String = mdlXMLBuilder.BuildInvoiceServiceAddRq(Name, Desc, Price, accountName)
        Dim resp As String = ""
        Try
            Application.DoEvents()
            resp = objQBProcessor.ProcessRequest(QBTicket, invoiceServiceReq)
            Application.DoEvents()
            If resp Is Nothing OrElse resp.Trim = "" Then Throw New Exception("Trash response from quick books")
            Application.DoEvents()
        Catch ex As Exception
            _LastError = ex.Message
            Return ""
        End Try

        Dim xmlDoc As New XmlDocument
        xmlDoc.LoadXml(resp)
        Dim node As XmlNode = Nothing
        Try
            Application.DoEvents()
            node = xmlDoc.GetElementsByTagName("ItemServiceAddRs").Item(0)
            Application.DoEvents()
        Catch ex As Exception
            _LastError = "Failed to load root node. " & ex.Message
            Return ""
        End Try
        'Check if there was any Error
        If IsErrorResponse(node) Then
            Me._LastError = GetErrorMessage(node)
            Return ""
        End If
        Dim ListId As String = node.ChildNodes(0).ChildNodes(0).InnerText
        Return ListId
    End Function

#Region "custom Fields/DataExtionsion management"
    Public Function ConfirmCustomFields() As Boolean

        Dim result As Boolean = False
        result = CustomFieldAvailable(mdlXMLBuilder.LOAN_NUMBER_FIELD)
        If Not result Then result = CustomFieldAdd(mdlXMLBuilder.LOAN_NUMBER_FIELD)
        If Not result Then Return False

        result = CustomFieldAvailable(mdlXMLBuilder.ORDERNUMBER_CUSTOMERID_FIELD)
        If Not result Then result = CustomFieldAdd(mdlXMLBuilder.ORDERNUMBER_CUSTOMERID_FIELD)
        If Not result Then Return False

        result = CustomFieldAvailable(mdlXMLBuilder.SSN_REQTYPE_ORDERSTATUS_FIELD)
        If Not result Then result = CustomFieldAdd(mdlXMLBuilder.SSN_REQTYPE_ORDERSTATUS_FIELD)
        If Not result Then Return False

        result = CustomFieldAvailable(mdlXMLBuilder.TAX_YEARS_FIELD)
        If Not result Then result = CustomFieldAdd(mdlXMLBuilder.TAX_YEARS_FIELD)
        If Not result Then Return False

        Return True
    End Function
    Public Function CustomFieldAvailable(ByVal CustomFieldName As String) As Boolean
        If Not IsConnected Then Connect()
        If Not IsConnected Then Return Nothing
        ClearErrors()
        Application.DoEvents()

        Dim invoiceServiceReq As String = mdlXMLBuilder.BuildDataExtDefGetRequest(CustomFieldName)
        Dim resp As String = ""
        Try

            Application.DoEvents()
            resp = objQBProcessor.ProcessRequest(QBTicket, invoiceServiceReq)
            Application.DoEvents()
            If resp Is Nothing OrElse resp.Trim = "" Then Throw New Exception("Trash response from quick books")
            Application.DoEvents()
        Catch ex As Exception
            _LastError = ex.Message
            Return ""
        End Try

        Dim xmlDoc As New XmlDocument
        xmlDoc.LoadXml(resp)
        Dim node As XmlNode = Nothing

        Try
            Application.DoEvents()
            node = xmlDoc.GetElementsByTagName("DataExtDefQueryRs").Item(0)
            Application.DoEvents()
        Catch ex As Exception
            _LastError = "Failed to load root node. " & ex.Message
            Return False
        End Try
        'Check if there was any Error
        If IsErrorResponse(node) Then
            Me._LastError = GetErrorMessage(node)
            Return False
        End If

        For Each node In xmlDoc.GetElementsByTagName("DataExtName")
            If node.InnerText = CustomFieldName Then
                Return True
            End If
        Next

        Return False
    End Function
    Public Function CustomFieldAdd(ByVal CustomFieldName As String) As Boolean
        If Not IsConnected Then Connect()
        If Not IsConnected Then Return Nothing
        ClearErrors()
        Application.DoEvents()

        Dim invoiceServiceReq As String = mdlXMLBuilder.BuildDataExtDefAddRq(CustomFieldName)
        Dim resp As String = ""
        Try

            Application.DoEvents()
            resp = objQBProcessor.ProcessRequest(QBTicket, invoiceServiceReq)
            Application.DoEvents()
            If resp Is Nothing OrElse resp.Trim = "" Then Throw New Exception("Trash response from quick books")
            Application.DoEvents()
        Catch ex As Exception
            _LastError = ex.Message
            Return ""
        End Try

        Dim xmlDoc As New XmlDocument
        xmlDoc.LoadXml(resp)
        Dim node As XmlNode = Nothing

        Try
            Application.DoEvents()
            node = xmlDoc.GetElementsByTagName("DataExtDefAddRs").Item(0)
            Application.DoEvents()
        Catch ex As Exception
            _LastError = "Failed to load root node. " & ex.Message
            Return False
        End Try
        'Check if there was any Error
        If IsErrorResponse(node) Then
            Me._LastError = GetErrorMessage(node)
            Return False
        End If

        Return True
    End Function
#End Region


    Public Function GetInvoiceService(ByVal Name As String) As String
        If Not IsConnected Then Connect()
        If Not IsConnected Then Return Nothing
        ClearErrors()
        Application.DoEvents()

        Dim invoiceServiceReq As String = mdlXMLBuilder.BuildInvoiceServiceQuery(Name)
        Dim resp As String = ""
        Try
            Application.DoEvents()
            resp = objQBProcessor.ProcessRequest(QBTicket, invoiceServiceReq)
            Application.DoEvents()
            If resp Is Nothing OrElse resp.Trim = "" Then Throw New Exception("Trash response from quick books")
            Application.DoEvents()
        Catch ex As Exception
            _LastError = ex.Message
            Return ""
        End Try

        Dim xmlDoc As New XmlDocument
        xmlDoc.LoadXml(resp)
        Dim node As XmlNode = Nothing
        Try
            Application.DoEvents()
            node = xmlDoc.GetElementsByTagName("ItemServiceRet").Item(0)
            Application.DoEvents()
        Catch ex As Exception
            _LastError = "Failed to load root node. " & ex.Message
            Return ""
        End Try
        Try
            Dim ListId As String = node.ChildNodes(0).ChildNodes(0).InnerText
            Return ListId
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function GetNewInvoiceNumber() As String
        Return "07-" & My.Settings.CurrentInvoiceNo.ToString
    End Function
    Private Sub IncreaseInvoiceNo()
        My.Settings.CurrentInvoiceNo += 1
        My.Settings.Save()
    End Sub
    Public Function UpdateInvoice(ByVal ListID As String, ByVal EditSequence As String, ByVal IsTobePrinted As Boolean, ByVal IsToBeEmailed As Boolean) As Boolean
        Dim invoiceReq As String = mdlXMLBuilder.BuildInvoiceUpdateRequest(ListID, EditSequence, IsTobePrinted, IsToBeEmailed)

        Dim resp As String = ""
        Try
            Application.DoEvents()
            resp = objQBProcessor.ProcessRequest(QBTicket, invoiceReq)
            Application.DoEvents()
            If resp Is Nothing OrElse resp.Trim = "" Then Throw New Exception("Trash response from quick books")
            Application.DoEvents()
        Catch ex As Exception
            _LastError = ex.Message
            Return ""
        End Try

        Dim xmlDoc As New XmlDocument
        xmlDoc.LoadXml(resp)
        Dim node As XmlNode = Nothing
        Try
            Application.DoEvents()
            node = xmlDoc.GetElementsByTagName("InvoiceQueryRs").Item(0)
            Application.DoEvents()
        Catch ex As Exception
            _LastError = "Failed to load root node. " & ex.Message
            Return ""
        End Try
        'Check if there was any Error
        If IsErrorResponse(node) Then
            Me._LastError = GetErrorMessage(node)
            Return False
        End If
        Return True
    End Function
    Public Function GetInvoiceBalanceRemaining(ByVal ListID As String, ByRef EditSequence As String) As Decimal
        Dim invoiceReq As String = mdlXMLBuilder.BuildGetInvoiceRequest(ListID)

        Dim resp As String = ""
        Try
            Application.DoEvents()
            resp = objQBProcessor.ProcessRequest(QBTicket, invoiceReq)
            Application.DoEvents()
            If resp Is Nothing OrElse resp.Trim = "" Then Throw New Exception("Trash response from quick books")
            Application.DoEvents()
        Catch ex As Exception
            _LastError = ex.Message
            Return ""
        End Try

        Dim xmlDoc As New XmlDocument
        xmlDoc.LoadXml(resp)
        Dim node As XmlNode = Nothing
        Try
            Application.DoEvents()
            node = xmlDoc.GetElementsByTagName("InvoiceQueryRs").Item(0)
            Application.DoEvents()
        Catch ex As Exception
            _LastError = "Failed to load root node. " & ex.Message
            Return ""
        End Try
        'Check if there was any Error
        If IsErrorResponse(node) Then
            Me._LastError = GetErrorMessage(node)
            Return ""
        End If

        'Get the edit sequence
        EditSequence = node.SelectSingleNode("InvoiceRet/EditSequence").InnerText

        Return CDec(node.SelectSingleNode("InvoiceRet/BalanceRemaining").InnerText)

    End Function
    Public Function AddInvoice(ByVal c As Customer, ByVal OrderIDs() As Integer, ByVal TobePrinted As Boolean, ByVal ToBeEMailed As Boolean) As String
        If Not IsConnected Then Connect()
        If Not IsConnected Then Return Nothing
        ClearErrors()
        Application.DoEvents()

        Dim invoiceReq As String = mdlXMLBuilder.BuildInvoiceRequest(GetNewInvoiceNumber, c, OrderIDs, TobePrinted, ToBeEMailed)
        Trace.WriteLine($"Creating invoice for customer = {c.CustomerID}, Name={c.Name}, OrderIDs={OrderIDs.ToSqlList}")
        Dim resp As String = ""
        Try
            Application.DoEvents()
            resp = objQBProcessor.ProcessRequest(QBTicket, invoiceReq)
            Application.DoEvents()
            If resp Is Nothing OrElse resp.Trim = "" Then Throw New Exception("Trash response from quick books")
            Application.DoEvents()
        Catch ex As Exception
            Trace.WriteLine($"Failed to create invoice for customer = {c.CustomerID}, Name={c.Name}, OrderIDs={OrderIDs.ToSqlList}, InvoiceRequest={invoiceReq}{vbCrLf}{ex.Message}{vbCrLf}{ex.StackTrace}")
            _LastError = ex.Message
            Return ""
        End Try

        Dim xmlDoc As New XmlDocument
        xmlDoc.LoadXml(resp)
        Dim node As XmlNode = Nothing
        Try
            Application.DoEvents()
            node = xmlDoc.GetElementsByTagName("InvoiceAddRs").Item(0)
            Application.DoEvents()
        Catch ex As Exception
            Trace.WriteLine($"Filed to load root node. {ex.Message}{vbCrLf}{ex.StackTrace}")
            _LastError = "Failed to load root node. " & ex.Message
            Return ""
        End Try
        'Check if there was any Error
        If IsErrorResponse(node) Then
            Me._LastError = GetErrorMessage(node)
            Trace.WriteLine($"Filed to insert invoice. LastError = {Me._LastError}")
            Return ""
        End If
        IncreaseInvoiceNo()
        Dim ListId As String = node.ChildNodes(0).ChildNodes(0).InnerText
        Return ListId
    End Function

End Class
