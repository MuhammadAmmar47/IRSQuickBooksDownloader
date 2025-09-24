Imports System.Xml

Public Class Invoice

#Region "Private Members"
    Private _fldordernumber As Integer
    Private _fldListid As Decimal
    Private _fldlisttype As Decimal
    Private _fldCompanyID As Integer
    Private _fldcustomeriD As Integer
    Private _fldrequestname As String = ""
    Private _fldsecondname As String = ""
    Private _fldssnno As String = ""
    Private _fldtypeofform As TypeOfForm = Core.TypeOfForm.S_None
    Private _fldemail As String = ""
    Private _fldfax As String = ""
    Private _fldfaxno As String = ""
    Private _fldstatus As String = ""
    Private _fldDOB As String = ""
    Private _fldSex As String = ""
    Private _fldbillingstatus As Boolean
    Private _flddeliverydate As DateTime
    Private _fldPdf As String = ""
    Private _fldOrderdate As DateTime
    Private _fldSpecialFlag As String = ""
    Private _Processor As String = ""
    Private _ListID As String = ""


    Private _fldtaxyear2003 As Boolean
    Private _fldtaxyear2002 As Boolean
    Private _fldtaxyear2001 As Boolean
    Private _fldtaxyear2000 As Boolean
    Private _fldTaxyear2004 As Boolean
    Private _fldTaxyear2005 As Boolean
    Private _fldTaxyear2006 As Boolean
    Private _fldTaxyear2007 As Boolean
    Private _fldTaxyear2008 As Boolean
    Private _fldTaxyear2009 As Boolean
    Private _fldTaxyear2010 As Boolean
    Private _fldTaxyear2011 As Boolean
    Private _fldTaxyear2012 As Boolean
    Private _fldLoanNumber As String = ""
    Private _fldTaxYear2013 As Boolean
    Private _fldTaxYear2014 As Boolean
    Private _fldTaxYear2015 As Boolean
    Public Property Taxyear2016 As Boolean
    Public Property Taxyear2017 As Boolean
    Public Property Taxyear2018 As Boolean
    Public Property Taxyear2019 As Boolean
    Public Property Taxyear2020 As Boolean
    Public Property Taxyear2021 As Boolean
    Public Property Taxyear2022 As Boolean
    Public Property Taxyear2023 As Boolean
    Public Property Taxyear2024 As Boolean
    Public Property Taxyear2025 As Boolean
#End Region

#Region "Constructors"
    Public Sub New()
    End Sub
    'Public Sub New(ByVal node As XmlNode)
    '    'If Not node.SelectSingleNode("TxnID") Is Nothing Then TransactionID = node.SelectSingleNode("TxnID").InnerText
    '    'If Not node.SelectSingleNode("TimeCreated") Is Nothing Then CreatedOn = node.SelectSingleNode("TimeCreated").InnerText
    '    'If Not node.SelectSingleNode("TimeModified") Is Nothing Then ModifiedOn = node.SelectSingleNode("TimeModified").InnerText
    '    'If Not node.SelectSingleNode("TxnNumber") Is Nothing Then TransactionNo = node.SelectSingleNode("TxnNumber").InnerText
    '    'If Not node.SelectSingleNode("ClassRef/FullName") Is Nothing Then ClassRefName = node.SelectSingleNode("ClassRef/FullName").InnerText
    '    'If Not node.SelectSingleNode("ARAccountRef/FullName") Is Nothing Then AccountRef = node.SelectSingleNode("ARAccountRef/FullName").InnerText
    '    'If Not node.SelectSingleNode("TxnDate") Is Nothing Then TransactionDate = node.SelectSingleNode("TxnDate").InnerText
    '    'If Not node.SelectSingleNode("RefNumber") Is Nothing Then InvoiceNumber = node.SelectSingleNode("RefNumber").InnerText
    '    'If Not node.SelectSingleNode("DueDate") Is Nothing Then DueDate = node.SelectSingleNode("DueDate").InnerText

    '    'If Not node.SelectSingleNode("Subtotal") Is Nothing Then SubTotal = node.SelectSingleNode("Subtotal").InnerText
    '    'If Not node.SelectSingleNode("AppliedAmount") Is Nothing Then AppliedAmount = node.SelectSingleNode("AppliedAmount").InnerText
    '    'If Not node.SelectSingleNode("BalanceRemaining") Is Nothing Then BalanceRemaining = node.SelectSingleNode("BalanceRemaining").InnerText
    '    'If Not node.SelectSingleNode("IsPaid") Is Nothing Then
    '    '    Dim value As String = node.SelectSingleNode("IsPaid").InnerText
    '    '    If value.Trim.ToLower = "false" Then
    '    '        IsPaid = False
    '    '    Else
    '    '        IsPaid = True
    '    '    End If
    '    'End If

    '    ''Make applied amount +ve
    '    'If AppliedAmount < 0 Then
    '    '    AppliedAmount = AppliedAmount * -1
    '    'End If
    'End Sub
#End Region
#Region "Properties"
    Public Property fldLoanNumber() As String
        Get
            Return _fldLoanNumber
        End Get
        Set(ByVal value As String)
            _fldLoanNumber = value
        End Set
    End Property

    Public Property Taxyear2013() As Boolean
        Get
            Return _fldTaxYear2013
        End Get
        Set(ByVal Value As Boolean)
            _fldTaxYear2013 = Value
        End Set
    End Property
    Public Property Taxyear2015() As Boolean
        Get
            Return _fldTaxYear2015
        End Get
        Set(ByVal Value As Boolean)
            _fldTaxYear2015 = Value
        End Set
    End Property
    Public Property Taxyear2014() As Boolean
        Get
            Return _fldTaxYear2014
        End Get
        Set(ByVal Value As Boolean)
            _fldTaxYear2014 = Value
        End Set
    End Property

    Public Property Taxyear2005() As Boolean
        Get
            Return _fldTaxyear2005
        End Get
        Set(ByVal Value As Boolean)
            _fldTaxyear2005 = Value
        End Set
    End Property

    Public Property Taxyear2006() As Boolean
        Get
            Return _fldTaxyear2006
        End Get
        Set(ByVal Value As Boolean)
            _fldTaxyear2006 = Value
        End Set
    End Property

    Public Property Taxyear2007() As Boolean
        Get
            Return _fldTaxyear2007
        End Get
        Set(ByVal Value As Boolean)
            _fldTaxyear2007 = Value
        End Set
    End Property

    Public Property Taxyear2008() As Boolean
        Get
            Return _fldTaxyear2008
        End Get
        Set(ByVal Value As Boolean)
            _fldTaxyear2008 = Value
        End Set
    End Property

    Public Property Taxyear2009() As Boolean
        Get
            Return _fldTaxyear2009
        End Get
        Set(ByVal Value As Boolean)
            _fldTaxyear2009 = Value
        End Set
    End Property

    Public Property Taxyear2011() As Boolean
        Get
            Return _fldTaxyear2011
        End Get
        Set(ByVal Value As Boolean)
            _fldTaxyear2011 = Value
        End Set
    End Property
    Public Property Taxyear2012() As Boolean
        Get
            Return _fldTaxyear2012
        End Get
        Set(ByVal Value As Boolean)
            _fldTaxyear2012 = Value
        End Set
    End Property
    Public Property Taxyear2010() As Boolean
        Get
            Return _fldTaxyear2010
        End Get
        Set(ByVal Value As Boolean)
            _fldTaxyear2010 = Value
        End Set
    End Property




    Public Property ListIDQuickBooks() As String
        Get
            Return _ListID
        End Get
        Set(ByVal value As String)
            _ListID = value
        End Set
    End Property

    Public Property OrderNumber() As Integer
        Get
            Return _fldordernumber
        End Get
        Set(ByVal Value As Integer)
            _fldordernumber = Value
        End Set
    End Property

    Public Property ListId() As Decimal
        Get
            Return _fldListid
        End Get
        Set(ByVal Value As Decimal)
            _fldListid = Value
        End Set
    End Property

    Public Property ListType() As Decimal
        Get
            Return _fldlisttype
        End Get
        Set(ByVal Value As Decimal)
            _fldlisttype = Value
        End Set
    End Property

    Public Property CompanyID() As Integer
        Get
            Return _fldCompanyID
        End Get
        Set(ByVal Value As Integer)
            _fldCompanyID = Value
        End Set
    End Property

    Public Property CustomeriD() As Integer
        Get
            Return _fldcustomeriD
        End Get
        Set(ByVal Value As Integer)
            _fldcustomeriD = Value
        End Set
    End Property

    Public Property RequestName() As String
        Get
            Return _fldrequestname
        End Get
        Set(ByVal Value As String)
            _fldrequestname = Value
        End Set
    End Property

    Public Property SecondName() As String
        Get
            Return _fldsecondname
        End Get
        Set(ByVal Value As String)
            _fldsecondname = Value
        End Set
    End Property

    Public Property SSNNo() As String
        Get
            Return _fldssnno
        End Get
        Set(ByVal Value As String)
            _fldssnno = Value
        End Set
    End Property

    Public Property TaxYear2003() As Boolean
        Get
            Return _fldtaxyear2003
        End Get
        Set(ByVal Value As Boolean)
            _fldtaxyear2003 = Value
        End Set
    End Property

    Public Property TaxYear2002() As Boolean
        Get
            Return _fldtaxyear2002
        End Get
        Set(ByVal Value As Boolean)
            _fldtaxyear2002 = Value
        End Set
    End Property

    Public Property TaxYear2001() As Boolean
        Get
            Return _fldtaxyear2001
        End Get
        Set(ByVal Value As Boolean)
            _fldtaxyear2001 = Value
        End Set
    End Property

    Public Property TaxYear2000() As Boolean
        Get
            Return _fldtaxyear2000
        End Get
        Set(ByVal Value As Boolean)
            _fldtaxyear2000 = Value
        End Set
    End Property

    Public Property typeOfForm() As TypeOfForm
        Get
            Return _fldtypeofform
        End Get
        Set(ByVal Value As TypeOfForm)
            _fldtypeofform = Value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return _fldemail
        End Get
        Set(ByVal Value As String)
            _fldemail = Value
        End Set
    End Property

    Public Property Fax() As String
        Get
            Return _fldfax
        End Get
        Set(ByVal Value As String)
            _fldfax = Value
        End Set
    End Property

    Public Property Faxno() As String
        Get
            Return _fldfaxno
        End Get
        Set(ByVal Value As String)
            _fldfaxno = Value
        End Set
    End Property

    Public Property Status() As String
        Get
            Return _fldstatus
        End Get
        Set(ByVal Value As String)
            _fldstatus = Value
        End Set
    End Property

    Public Property DOB() As String
        Get
            Return _fldDOB
        End Get
        Set(ByVal Value As String)
            _fldDOB = Value
        End Set
    End Property

    Public Property Sex() As String
        Get
            Return _fldSex
        End Get
        Set(ByVal Value As String)
            _fldSex = Value
        End Set
    End Property

    Public Property BillingStatus() As Boolean
        Get
            Return _fldbillingstatus
        End Get
        Set(ByVal Value As Boolean)
            _fldbillingstatus = Value
        End Set
    End Property

    Public Property DeliveryDate() As DateTime
        Get
            Return _flddeliverydate
        End Get
        Set(ByVal Value As DateTime)
            _flddeliverydate = Value
        End Set
    End Property

    Public Property PDF() As String
        Get
            Return _fldPdf
        End Get
        Set(ByVal Value As String)
            _fldPdf = Value
        End Set
    End Property

    Public Property Orderdate() As DateTime
        Get
            Return _fldOrderdate
        End Get
        Set(ByVal Value As DateTime)
            _fldOrderdate = Value
        End Set
    End Property

    Public Property Taxyear2004() As Boolean
        Get
            Return _fldTaxyear2004
        End Get
        Set(ByVal Value As Boolean)
            _fldTaxyear2004 = Value
        End Set
    End Property

    Public Property SpecialFlag() As String
        Get
            Return _fldSpecialFlag
        End Get
        Set(ByVal Value As String)
            _fldSpecialFlag = Value
        End Set
    End Property

    Public Property TaxPayer1() As String
        Get
            Return _fldrequestname
        End Get
        Set(ByVal value As String)
            _fldrequestname = value
        End Set
    End Property
    Public Property TaxPayer2() As String
        Get
            Return _fldsecondname
        End Get
        Set(ByVal value As String)
            _fldsecondname = value
        End Set
    End Property
    Public Property Processor() As String
        Get
            Return _Processor
        End Get
        Set(ByVal value As String)
            _Processor = value
        End Set
    End Property
#End Region
End Class
