Imports System.Xml

Public Class Invoice
#Region "Private Fields"
    Private _TxnID As String = ""
    Private _TimeCreated As DateTime
    Private _TimeModified As DateTime
    Private _EditSequence As String = ""
    Private _TxnNumber As Integer
    Private _CustomerRef_ListID As String = ""
    Private _CustomerRef_FullName As String = ""
    Private _TxnDate As DateTime
    Private _RefNumber As String = ""
    Private _IsPending As Boolean
    Private _IsFinanceCharge As Boolean
    Private _DueDate As DateTime
    Private _ShipDate As DateTime
    Private _Subtotal As Decimal
    Private _SalesTaxPercentage As Decimal
    Private _SalesTaxTotal As Decimal
    Private _AppliedAmount As Decimal
    Private _BalanceRemaining As Decimal
    Private _IsPaid As Boolean
    Private _IsToBePrinted As Boolean
    Private _IsToBeEmailed As Boolean
    Private _BillAddress_Addr1 As String = ""
    Private _BillAddress_Addr2 As String = ""
    Private _BillAddress_City As String = ""
    Private _BillAddress_State As String = ""
    Private _BillAddress_PostalCode As String = ""
    Private _InvoiceLine As New Generic.List(Of InvoiceLine)
#End Region


#Region "Properties"
    Public Property InvoiceLines() As Generic.List(Of InvoiceLine)
        Get
            Return _InvoiceLine
        End Get
        Set(ByVal value As Generic.List(Of InvoiceLine))
            _InvoiceLine = value
        End Set
    End Property
    Public Property TxnID() As String
        Get
            Return _TxnID
        End Get
        Set(ByVal Value As String)
            _TxnID = Value
        End Set
    End Property
    Public Property TimeCreated() As DateTime
        Get
            Return _TimeCreated
        End Get
        Set(ByVal Value As DateTime)
            _TimeCreated = Value
        End Set
    End Property
    Public Property TimeModified() As DateTime
        Get
            Return _TimeModified
        End Get
        Set(ByVal Value As DateTime)
            _TimeModified = Value
        End Set
    End Property
    Public Property EditSequence() As String
        Get
            Return _EditSequence
        End Get
        Set(ByVal Value As String)
            _EditSequence = Value
        End Set
    End Property
    Public Property TxnNumber() As Integer
        Get
            Return _TxnNumber
        End Get
        Set(ByVal Value As Integer)
            _TxnNumber = Value
        End Set
    End Property
    Public Property CustomerRef_ListID() As String
        Get
            Return _CustomerRef_ListID
        End Get
        Set(ByVal Value As String)
            _CustomerRef_ListID = Value
        End Set
    End Property
    Public Property CustomerRef_FullName() As String
        Get
            Return _CustomerRef_FullName
        End Get
        Set(ByVal Value As String)
            _CustomerRef_FullName = Value
        End Set
    End Property
    Public Property TxnDate() As DateTime
        Get
            Return _TxnDate
        End Get
        Set(ByVal Value As DateTime)
            _TxnDate = Value
        End Set
    End Property
    Public Property RefNumber() As String
        Get
            Return _RefNumber
        End Get
        Set(ByVal Value As String)
            _RefNumber = Value
        End Set
    End Property
    Public Property IsPending() As Boolean
        Get
            Return _IsPending
        End Get
        Set(ByVal Value As Boolean)
            _IsPending = Value
        End Set
    End Property
    Public Property IsFinanceCharge() As Boolean
        Get
            Return _IsFinanceCharge
        End Get
        Set(ByVal Value As Boolean)
            _IsFinanceCharge = Value
        End Set
    End Property
    Public Property DueDate() As DateTime
        Get
            Return _DueDate
        End Get
        Set(ByVal Value As DateTime)
            _DueDate = Value
        End Set
    End Property
    Public Property ShipDate() As DateTime
        Get
            Return _ShipDate
        End Get
        Set(ByVal Value As DateTime)
            _ShipDate = Value
        End Set
    End Property
    Public Property Subtotal() As Decimal
        Get
            Return _Subtotal
        End Get
        Set(ByVal Value As Decimal)
            _Subtotal = Value
        End Set
    End Property
    Public Property SalesTaxPercentage() As Decimal
        Get
            Return _SalesTaxPercentage
        End Get
        Set(ByVal Value As Decimal)
            _SalesTaxPercentage = Value
        End Set
    End Property
    Public Property SalesTaxTotal() As Decimal
        Get
            Return _SalesTaxTotal
        End Get
        Set(ByVal Value As Decimal)
            _SalesTaxTotal = Value
        End Set
    End Property
    Public Property AppliedAmount() As Decimal
        Get
            Return _AppliedAmount
        End Get
        Set(ByVal Value As Decimal)
            _AppliedAmount = Value
        End Set
    End Property
    Public Property BalanceRemaining() As Decimal
        Get
            Return _BalanceRemaining
        End Get
        Set(ByVal Value As Decimal)
            _BalanceRemaining = Value
        End Set
    End Property
    Public Property IsPaid() As Boolean
        Get
            Return _IsPaid
        End Get
        Set(ByVal Value As Boolean)
            _IsPaid = Value
        End Set
    End Property
    Public Property IsToBePrinted() As Boolean
        Get
            Return _IsToBePrinted
        End Get
        Set(ByVal Value As Boolean)
            _IsToBePrinted = Value
        End Set
    End Property
    Public Property IsToBeEmailed() As Boolean
        Get
            Return _IsToBeEmailed
        End Get
        Set(ByVal Value As Boolean)
            _IsToBeEmailed = Value
        End Set
    End Property
    Public Property BillAddress_Addr1() As String
        Get
            Return _BillAddress_Addr1
        End Get
        Set(ByVal Value As String)
            _BillAddress_Addr1 = Value
        End Set
    End Property
    Public Property BillAddress_Addr2() As String
        Get
            Return _BillAddress_Addr2
        End Get
        Set(ByVal Value As String)
            _BillAddress_Addr2 = Value
        End Set
    End Property
    Public Property BillAddress_City() As String
        Get
            Return _BillAddress_City
        End Get
        Set(ByVal Value As String)
            _BillAddress_City = Value
        End Set
    End Property
    Public Property BillAddress_State() As String
        Get
            Return _BillAddress_State
        End Get
        Set(ByVal Value As String)
            _BillAddress_State = Value
        End Set
    End Property
    Public Property BillAddress_PostalCode() As String
        Get
            Return _BillAddress_PostalCode
        End Get
        Set(ByVal Value As String)
            _BillAddress_PostalCode = Value
        End Set
    End Property
#End Region

    Public Overrides Function ToString() As String
        Return CustomerRef_FullName & ", Txn Number: " & TxnNumber & ", Sub Total: " & FormatCurrency(Me.Subtotal, 2)

    End Function

#Region "Parsing Invoice"
    Public Shared Function ParseFromXML(ByVal invoiceNode As XmlNode) As Invoice
        Dim invoice As New Invoice
        With invoice
            .TxnID = GetNodeText(invoiceNode, "TxnID")
            .TimeCreated = GetNodeText(invoiceNode, "TimeCreated")
            .TimeModified = GetNodeText(invoiceNode, "TimeModified")
            .EditSequence = GetNodeText(invoiceNode, "EditSequence")
            .TxnNumber = GetNodeText(invoiceNode, "TxnNumber")

            .TxnDate = GetNodeText(invoiceNode, "TxnDate")
            .RefNumber = GetNodeText(invoiceNode, "RefNumber")

            .DueDate = GetNodeText(invoiceNode, "DueDate")
            .ShipDate = GetNodeText(invoiceNode, "ShipDate")
            .Subtotal = GetNodeText(invoiceNode, "Subtotal")
            .SalesTaxPercentage = GetNodeText(invoiceNode, "SalesTaxPercentage")
            .SalesTaxTotal = GetNodeText(invoiceNode, "SalesTaxTotal")
            .AppliedAmount = GetNodeText(invoiceNode, "AppliedAmount")
            .BalanceRemaining = GetNodeText(invoiceNode, "BalanceRemaining")

            .IsPending = GetNodeText(invoiceNode, "IsPending")
            .IsFinanceCharge = GetNodeText(invoiceNode, "IsFinanceCharge")
            .IsPaid = GetNodeText(invoiceNode, "IsPaid")
            .IsToBePrinted = GetNodeText(invoiceNode, "IsToBePrinted")
            .IsToBeEmailed = GetNodeText(invoiceNode, "IsToBeEmailed")
            .CustomerRef_FullName = GetNodeText(invoiceNode, "CustomerRef/FullName")
            .CustomerRef_ListID = GetNodeText(invoiceNode, "CustomerRef/ListID")


            .BillAddress_Addr1 = GetNodeText(invoiceNode, "BillAddress/Addr1")
            .BillAddress_Addr2 = GetNodeText(invoiceNode, "BillAddress/Addr2")
            .BillAddress_City = GetNodeText(invoiceNode, "BillAddress/City")
            .BillAddress_State = GetNodeText(invoiceNode, "BillAddress/State")
            .BillAddress_PostalCode = GetNodeText(invoiceNode, "BillAddress/PostalCode")


            Dim lineNode As XmlNodeList = invoiceNode.SelectNodes("InvoiceLineRet")
            For Each node As XmlNode In lineNode
                .InvoiceLines.Add(InvoiceLine.ParseFromXML(node))
            Next

        End With

        Return invoice
    End Function
#End Region

End Class
