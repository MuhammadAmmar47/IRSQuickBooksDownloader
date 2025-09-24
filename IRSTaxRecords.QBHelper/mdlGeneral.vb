Imports IRSTaxRecords.QBHelper.QBWebService
'Imports IRSTaxRecords.QBHelper.QBServiceLocal

Imports System.Xml

Public Module mdlGeneral
    Public qbObject As New QuickBooksProcessor


    Public ReadOnly Property PaymentReferenceName() As String
        Get
            Dim value As String = System.Configuration.ConfigurationManager.AppSettings("PaymentReferenceName")
            If value Is Nothing OrElse value.Trim = "" Then
                value = "Online Payment"
            End If
            Return value
        End Get
    End Property
    Public ReadOnly Property PaymentMethodName() As String
        Get
            Dim value As String = System.Configuration.ConfigurationManager.AppSettings("PaymentMethodName")
            If value Is Nothing OrElse value.Trim = "" Then
                value = "Cash"
            End If
            Return value
        End Get
    End Property
    Public ReadOnly Property WebServiceURL() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("WebServiceURL")
        End Get
    End Property

    

    Public Property CurrentInvoiceNumber() As Integer
        Get
            Return My.Settings.CurrentInvoiceNo
        End Get
        Set(ByVal value As Integer)
            My.Settings.CurrentInvoiceNo = value
            My.Settings.Save()
            My.Settings.Reload()
        End Set
    End Property
#Region "XML Parsing"
    Public Function GetCustomerFromNode(ByVal node As System.Xml.XmlNode) As Customer
        'Get List Id
        Dim c As Customer = New Customer
        If Not node.SelectSingleNode("ListID") Is Nothing Then c.ListId = node.SelectSingleNode("ListID").InnerText
        'If Not node.SelectSingleNode("TimeCreated") Is Nothing Then c.CreatedOn = node.SelectSingleNode("TimeCreated").InnerText
        'If Not node.SelectSingleNode("TimeModified") Is Nothing Then c.ModifiedOn = node.SelectSingleNode("TimeModified").InnerText

        'If Not node.SelectSingleNode("FirstName") Is Nothing Then c.FirstName = node.SelectSingleNode("FirstName").InnerText
        'If Not node.SelectSingleNode("LastName") Is Nothing Then c.LastName = node.SelectSingleNode("LastName").InnerText

        If Not node.SelectSingleNode("BillAddress/Addr1") Is Nothing Then c.Address = node.SelectSingleNode("BillAddress/Addr1").InnerText
        If Not node.SelectSingleNode("BillAddress/Addr2") Is Nothing Then c.Address1 = node.SelectSingleNode("BillAddress/Addr2").InnerText
        If Not node.SelectSingleNode("BillAddress/City") Is Nothing Then c.City = node.SelectSingleNode("BillAddress/City").InnerText
        If Not node.SelectSingleNode("BillAddress/State") Is Nothing Then c.State = node.SelectSingleNode("BillAddress/State").InnerText
        If Not node.SelectSingleNode("BillAddress/PostalCode") Is Nothing Then c.Zip = node.SelectSingleNode("BillAddress/PostalCode").InnerText

        If Not node.SelectSingleNode("Email") Is Nothing Then c.Email = node.SelectSingleNode("Email").InnerText
        c.Password = GeneratePassword(10)

        Return c
    End Function
    'Public Function GetInvoiceFromNode(ByVal node As System.Xml.XmlNode) As Invoice
    '    Dim i As New Invoice
    '    If Not node.SelectSingleNode("TxnID") Is Nothing Then i.TransactionID = node.SelectSingleNode("TxnID").InnerText
    '    If Not node.SelectSingleNode("TimeCreated") Is Nothing Then i.CreatedOn = node.SelectSingleNode("TimeCreated").InnerText
    '    If Not node.SelectSingleNode("TimeModified") Is Nothing Then i.ModifiedOn = node.SelectSingleNode("TimeModified").InnerText
    '    If Not node.SelectSingleNode("TxnNumber") Is Nothing Then i.TransactionNo = node.SelectSingleNode("TxnNumber").InnerText
    '    If Not node.SelectSingleNode("ClassRef/FullName") Is Nothing Then i.ClassRefName = node.SelectSingleNode("ClassRef/FullName").InnerText
    '    If Not node.SelectSingleNode("ARAccountRef/FullName") Is Nothing Then i.AccountRef = node.SelectSingleNode("ARAccountRef/FullName").InnerText
    '    If Not node.SelectSingleNode("TxnDate") Is Nothing Then i.TransactionDate = node.SelectSingleNode("TxnDate").InnerText
    '    If Not node.SelectSingleNode("RefNumber") Is Nothing Then i.InvoiceNumber = node.SelectSingleNode("RefNumber").InnerText
    '    If Not node.SelectSingleNode("DueDate") Is Nothing Then i.DueDate = node.SelectSingleNode("DueDate").InnerText

    '    If Not node.SelectSingleNode("Subtotal") Is Nothing Then i.SubTotal = node.SelectSingleNode("Subtotal").InnerText
    '    If Not node.SelectSingleNode("AppliedAmount") Is Nothing Then i.AppliedAmount = node.SelectSingleNode("AppliedAmount").InnerText
    '    If Not node.SelectSingleNode("BalanceRemaining") Is Nothing Then i.BalanceRemaining = node.SelectSingleNode("BalanceRemaining").InnerText
    '    If Not node.SelectSingleNode("IsPaid") Is Nothing Then
    '        Dim value As String = node.SelectSingleNode("IsPaid").InnerText
    '        If value.Trim.ToLower = "false" Then
    '            i.IsPaid = False
    '        Else
    '            i.IsPaid = True
    '        End If
    '    End If

    '    'Make applied amount +ve
    '    If i.AppliedAmount < 0 Then
    '        i.AppliedAmount = i.AppliedAmount * -1
    '    End If

    '    Return i
    'End Function
#End Region


#Region "Password generation"

    Public Function GeneratePassword(ByVal length As Integer) As String
        Dim result As String = ""

        For i As Integer = 0 To length - 1
            Randomize()
            If i = 0 Then
                result += GetRandomPrintableLetter()
            Else
                result += GetRandomPrintableCharacter()
            End If
        Next

        Return result
    End Function
    Private Function GetRandomPrintableLetter() As String
        Const passwordCharacters As String = "abcdefghijkmnopqrstuvwxyz"
        Return Mid(passwordCharacters, Int(Len(passwordCharacters) * Rnd()) + 1, 1)
    End Function
    Private Function GetRandomPrintableCharacter() As String
        Const passwordCharacters As String = "abcdefghijkmnopqrstuvwxyz23456789"
        Return Mid(passwordCharacters, Int(Len(passwordCharacters) * Rnd()) + 1, 1)
    End Function
#End Region
End Module
