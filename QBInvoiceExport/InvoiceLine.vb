Imports System.Xml

Public Class InvoiceLine
    Private _TxnLineID As String = ""
    Private _ItemRef_ListID As String = ""
    Private _ItemRef_FullName As String = ""
    Private _ItemDesc As String = ""
    Private _Rate As Decimal
    Private _Amount As Decimal
    Private _ServiceDate As DateTime
    Private _Other1 As String = ""  'Its Processor Name
    Private _Other2 As String = "" 'Its Borrower Name
    Private _LoanNumber As String = ""


    Private _Request As String = ""
    Private _OrderNumber As String = ""
    Private _SSN As String = ""
    Private _Status As String = ""
    Private _TaxYears As String = ""
    Private _CustomerID As String = ""

    Private _IsDuplicate As Boolean

#Region "Properties"
    Public Property IsDuplicate() As Boolean
        Get
            Return _IsDuplicate
        End Get
        Set(ByVal value As Boolean)
            _IsDuplicate = value
        End Set
    End Property
    Public Property CustomerID() As String
        Get
            Return _CustomerID
        End Get
        Set(ByVal value As String)
            _CustomerID = value
        End Set
    End Property
    Public Property TaxYears() As String
        Get
            Return _TaxYears
        End Get
        Set(ByVal Value As String)
            _TaxYears = Value
        End Set
    End Property
    Public Property Status() As String
        Get
            Return _Status
        End Get
        Set(ByVal Value As String)
            _Status = Value
        End Set
    End Property
    Public Property SSN() As String
        Get
            Return _SSN
        End Get
        Set(ByVal Value As String)
            _SSN = Value
        End Set
    End Property
    Public Property OrderNumber() As String
        Get
            Return _OrderNumber
        End Get
        Set(ByVal Value As String)
            _OrderNumber = Value
        End Set
    End Property
    Public Property Request() As String
        Get
            Return _Request
        End Get
        Set(ByVal Value As String)
            _Request = Value
        End Set
    End Property
    Public Property TxnLineID() As String
        Get
            Return _TxnLineID
        End Get
        Set(ByVal Value As String)
            _TxnLineID = Value
        End Set
    End Property
    Public Property ItemRef_ListID() As String
        Get
            Return _ItemRef_ListID
        End Get
        Set(ByVal Value As String)
            _ItemRef_ListID = Value
        End Set
    End Property
    Public Property ItemRef_FullName() As String
        Get
            Return _ItemRef_FullName
        End Get
        Set(ByVal Value As String)
            _ItemRef_FullName = Value
        End Set
    End Property
    Public Property ItemDesc() As String
        Get
            Return _ItemDesc
        End Get
        Set(ByVal Value As String)
            _ItemDesc = Value
        End Set
    End Property
    Public Property Rate() As Decimal
        Get
            Return _Rate
        End Get
        Set(ByVal Value As Decimal)
            _Rate = Value
        End Set
    End Property
    Public Property Amount() As Decimal
        Get
            Return _Amount
        End Get
        Set(ByVal Value As Decimal)
            _Amount = Value
        End Set
    End Property
    Public Property ServiceDate() As DateTime
        Get
            Return _ServiceDate
        End Get
        Set(ByVal Value As DateTime)
            _ServiceDate = Value
        End Set
    End Property
    Public Property Other1() As String
        Get
            Return _Other1
        End Get
        Set(ByVal Value As String)
            _Other1 = Value
        End Set
    End Property
    Public Property Other2() As String
        Get
            Return _Other2
        End Get
        Set(ByVal Value As String)
            _Other2 = Value
        End Set
    End Property
    Public Property LoanNumber() As String
        Get
            Return _LoanNumber
        End Get
        Set(ByVal Value As String)
            _LoanNumber = Value
        End Set
    End Property
#End Region



    Public Shared Function ParseFromXML(ByVal invoiceNode As XmlNode) As InvoiceLine
        Dim line As New InvoiceLine
        With line
            .TxnLineID = GetNodeText(invoiceNode, "TxnLineID")
            .ItemDesc = GetNodeText(invoiceNode, "Desc")
            .Rate = GetNodeText(invoiceNode, "Rate")
            .Amount = GetNodeText(invoiceNode, "Amount")
            .ServiceDate = GetNodeText(invoiceNode, "ServiceDate")
            .Other1 = GetNodeText(invoiceNode, "Other1")
            .Other2 = GetNodeText(invoiceNode, "Other2")

            .ItemRef_FullName = GetNodeText(invoiceNode, "ItemRef/FullName")
            .ItemRef_ListID = GetNodeText(invoiceNode, "ItemRef/ListID")

            For Each node As XmlNode In invoiceNode.SelectNodes("DataExtRet")
                Select Case GetNodeText(node, "DataExtName")
                    Case IRSTaxRecords.QBHelper.mdlXMLBuilder.LOAN_NUMBER_FIELD
                        .LoanNumber = GetNodeText(node, "DataExtValue")
                    Case IRSTaxRecords.QBHelper.mdlXMLBuilder.ORDERNUMBER_CUSTOMERID_FIELD
                        Dim values() As String = Split(GetNodeText(node, "DataExtValue"), ",")
                        .OrderNumber = values(0)
                        If values.Length > 1 Then .CustomerID = values(1)
                    Case IRSTaxRecords.QBHelper.mdlXMLBuilder.TAX_YEARS_FIELD
                        .TaxYears = GetNodeText(node, "DataExtValue")
                    Case IRSTaxRecords.QBHelper.mdlXMLBuilder.SSN_REQTYPE_ORDERSTATUS_FIELD
                        Dim values() As String = Split(GetNodeText(node, "DataExtValue"), ",")
                        .SSN = values(0)
                        If values.Length > 1 Then .Request = values(1)
                        If values.Length > 2 Then .Status = values(2)
                    Case "Other1"
                        .Other1 = GetNodeText(node, "DataExtValue")
                    Case "Other2"
                        .Other2 = GetNodeText(node, "DataExtValue")
                End Select
            Next
        End With
        Return line
    End Function

End Class
