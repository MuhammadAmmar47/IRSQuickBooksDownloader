Imports System.Xml

Public Class Customer


    Public Shared Property TopParentCompanyID() As Integer
        Get
            Return 9459
        End Get
        Set(ByVal value As Integer)
        End Set
    End Property

#Region "Constructor"
    Public Sub New()
    End Sub
    Public Sub New(ByVal node As XmlNode)
        'Get List Id
        'If Not node.SelectSingleNode("ListID") Is Nothing Then ListId = node.SelectSingleNode("ListID").InnerText
        'If Not node.SelectSingleNode("TimeCreated") Is Nothing Then CreatedOn = node.SelectSingleNode("TimeCreated").InnerText
        'If Not node.SelectSingleNode("TimeModified") Is Nothing Then ModifiedOn = node.SelectSingleNode("TimeModified").InnerText

        'If Not node.SelectSingleNode("FirstName") Is Nothing Then FirstName = node.SelectSingleNode("FirstName").InnerText
        'If Not node.SelectSingleNode("LastName") Is Nothing Then LastName = node.SelectSingleNode("LastName").InnerText

        'If Not node.SelectSingleNode("BillAddress/Addr1") Is Nothing Then Address1 = node.SelectSingleNode("BillAddress/Addr1").InnerText
        'If Not node.SelectSingleNode("BillAddress/Addr2") Is Nothing Then Address2 = node.SelectSingleNode("BillAddress/Addr2").InnerText
        'If Not node.SelectSingleNode("BillAddress/City") Is Nothing Then City = node.SelectSingleNode("BillAddress/City").InnerText
        'If Not node.SelectSingleNode("BillAddress/State") Is Nothing Then State = node.SelectSingleNode("BillAddress/State").InnerText
        'If Not node.SelectSingleNode("BillAddress/PostalCode") Is Nothing Then ZipCode = node.SelectSingleNode("BillAddress/PostalCode").InnerText

        'If Not node.SelectSingleNode("Email") Is Nothing Then UserName = node.SelectSingleNode("Email").InnerText
        'Password = Utilities.GeneratePassword(10)
    End Sub
#End Region


    Private _CustomerID As Integer
    Private _CompanyName As String = ""
    Private _Name As String = ""
    Private _Telephone As String = ""
    Private _FaxNumber As String = ""
    Private _Email As String = ""
    Private _BilltoName As String = ""
    Private _Address As String = ""
    Private _Address1 As String = ""
    Private _City As String = ""
    Private _State As String = ""
    Private _Zip As String = ""
    Private _Referal As String = ""
    Private _UserID As String = ""
    Private _Password As String = ""
    Private _Approved As Integer
    Private _confDate As DateTime
    Private _confTime As String = ""
    Private _BranchID As Integer
    Private _Status As Integer
    Private _standardRate As Integer
    Private _rushRate As Decimal
    Private _showgrant As Integer
    Private _fld_associate_id As Integer
    Private _fld_bill4506 As Decimal
    Private _fld_billssn As Decimal
    Private _fld_commission_amount As Decimal
    Private _fld_comm_4506 As Decimal
    Private _fld_comm_SSN As Decimal
    Private _ListId As String = ""
    Private _IsError As Boolean = False
    Private _ChargeSecondTaxPayer As Boolean
    Private _ParentID As Integer = 0
    Private _SSN_Fee As Decimal
    Private _CreditCardActive As Boolean
    Private _IRSFee As Decimal
    Private _Addloannumber As Integer
    Public Property BillToID As Integer
    Public Property Addloannumber() As Integer
        Get
            Return _Addloannumber
        End Get
        Set(ByVal value As Integer)
            _Addloannumber = value
        End Set
    End Property
    Public Property IRSFee() As Decimal
        Get
            Return _IRSFee
        End Get
        Set(ByVal value As Decimal)
            _IRSFee = value
        End Set
    End Property
    Public Property CreditCardActive() As Boolean
        Get
            Return _CreditCardActive
        End Get
        Set(ByVal value As Boolean)
            _CreditCardActive = value
        End Set
    End Property
    Public Property SSN_Fee() As Decimal
        Get
            Return _SSN_Fee
        End Get
        Set(ByVal value As Decimal)
            _SSN_Fee = value
        End Set
    End Property
    Public Property ParentID() As Integer
        Get
            Return _ParentID
        End Get
        Set(ByVal value As Integer)
            _ParentID = value
        End Set
    End Property
    Public Property ChargeSecondTaxPayer() As Boolean
        Get
            Return _ChargeSecondTaxPayer
        End Get
        Set(ByVal value As Boolean)
            _ChargeSecondTaxPayer = value
        End Set
    End Property
    Public Property IsError() As Boolean
        Get
            Return _IsError
        End Get
        Set(ByVal value As Boolean)
            _IsError = value
        End Set
    End Property
    Public Property ListId() As String
        Get
            Return _ListId
        End Get
        Set(ByVal Value As String)
            _ListId = value
        End Set
    End Property

    Public Property CustomerID() As Integer
        Get
            Return _CustomerID
        End Get
        Set(ByVal Value As Integer)
            _CustomerID = Value
        End Set
    End Property

    Public Property CompanyName() As String
        Get
            Return _CompanyName
        End Get
        Set(ByVal Value As String)
            _CompanyName = value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal Value As String)
            _Name = value
        End Set
    End Property

    Public Property Telephone() As String
        Get
            Return _Telephone
        End Get
        Set(ByVal Value As String)
            _Telephone = value
        End Set
    End Property

    Public Property FaxNumber() As String
        Get
            Return _FaxNumber
        End Get
        Set(ByVal Value As String)
            _FaxNumber = value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal Value As String)
            _Email = value
        End Set
    End Property

    Public Property BilltoName() As String
        Get
            Return _BilltoName
        End Get
        Set(ByVal Value As String)
            _BilltoName = value
        End Set
    End Property

    Public Property Address() As String
        Get
            Return _Address
        End Get
        Set(ByVal Value As String)
            _Address = value
        End Set
    End Property

    Public Property Address1() As String
        Get
            Return _Address1
        End Get
        Set(ByVal Value As String)
            _Address1 = value
        End Set
    End Property

    Public Property City() As String
        Get
            Return _City
        End Get
        Set(ByVal Value As String)
            _City = value
        End Set
    End Property

    Public Property State() As String
        Get
            Return _State
        End Get
        Set(ByVal Value As String)
            _State = value
        End Set
    End Property

    Public Property Zip() As String
        Get
            Return _Zip
        End Get
        Set(ByVal Value As String)
            _Zip = value
        End Set
    End Property

    Public Property Referal() As String
        Get
            Return _Referal
        End Get
        Set(ByVal Value As String)
            _Referal = value
        End Set
    End Property

    Public Property UserID() As String
        Get
            Return _UserID
        End Get
        Set(ByVal Value As String)
            _UserID = value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal Value As String)
            _Password = value
        End Set
    End Property

    Public Property Approved() As Integer
        Get
            Return _Approved
        End Get
        Set(ByVal Value As Integer)
            _Approved = value
        End Set
    End Property

    Public Property confDate() As DateTime
        Get
            Return _confDate
        End Get
        Set(ByVal Value As DateTime)
            _confDate = value
        End Set
    End Property

    Public Property confTime() As String
        Get
            Return _confTime
        End Get
        Set(ByVal Value As String)
            _confTime = value
        End Set
    End Property

    Public Property BranchID() As Integer
        Get
            Return _BranchID
        End Get
        Set(ByVal Value As Integer)
            _BranchID = value
        End Set
    End Property

    Public Property Status() As Integer
        Get
            Return _Status
        End Get
        Set(ByVal Value As Integer)
            _Status = value
        End Set
    End Property

    Public Property standardRate() As Integer
        Get
            Return _standardRate
        End Get
        Set(ByVal Value As Integer)
            _standardRate = value
        End Set
    End Property

    Public Property rushRate() As Decimal
        Get
            Return _rushRate
        End Get
        Set(ByVal Value As Decimal)
            _rushRate = Value
        End Set
    End Property

    Public Property showgrant() As Integer
        Get
            Return _showgrant
        End Get
        Set(ByVal Value As Integer)
            _showgrant = value
        End Set
    End Property

    Public Property Associate_id() As Integer
        Get
            Return _fld_associate_id
        End Get
        Set(ByVal Value As Integer)
            _fld_associate_id = Value
        End Set
    End Property

    Public Property Bill4506() As Decimal
        Get
            Return _fld_bill4506
        End Get
        Set(ByVal Value As Decimal)
            _fld_bill4506 = Value
        End Set
    End Property

    Public Property BillSSN() As Decimal
        Get
            Return _fld_billssn
        End Get
        Set(ByVal Value As Decimal)
            _fld_billssn = Value
        End Set
    End Property

    Public Property Commission_Amount() As Decimal
        Get
            Return _fld_commission_amount
        End Get
        Set(ByVal Value As Decimal)
            _fld_commission_amount = Value
        End Set
    End Property

    Public Property Comm_4506() As Decimal
        Get
            Return _fld_comm_4506
        End Get
        Set(ByVal Value As Decimal)
            _fld_comm_4506 = Value
        End Set
    End Property

    Public Property Comm_SSN() As Decimal
        Get
            Return _fld_comm_SSN
        End Get
        Set(ByVal Value As Decimal)
            _fld_comm_SSN = Value
        End Set
    End Property



End Class
