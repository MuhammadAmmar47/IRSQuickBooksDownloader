Namespace Orders
    Public Class Order
        Private _fldordernumber As Integer
        Private _fldListid As Decimal
        Private _fldlisttype As Decimal
        Private _fldCompanyID As Integer
        Private _fldcustomeriD As Integer
        Private _fldrequestname As String = ""
        Private _fldsecondname As String = ""
        Private _fldssnno As String = ""
        Private _fldtaxyear2003 As Boolean
        Private _fldtaxyear2002 As Boolean
        Private _fldtaxyear2001 As Boolean
        Private _fldtaxyear2000 As Boolean
        Private _fldtypeofform As String = ""
        Private _fldordertype As String = ""
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
        Private _fldTaxyear2004 As Boolean
        Private _fldSpecialFlag As String = ""
        Private _fldTaxyear2005 As Boolean
        Private _fldTaxyear2006 As Boolean
        Private _ListID As String = ""
        Private _bUpdatedInQB As Boolean
        Private _fldTaxyear2007 As Boolean
        Private _fldTaxyear2008 As Boolean
        Private _fldTaxyear2009 As Boolean
        Private _fldTaxyear2010 As Boolean
        Private _fldLoanNumber As String = ""
        Private _QBBatchNumber As Integer
        Private _UpdatedInQBOn As DateTime
        Private _fldTaxyear2011 As Boolean
        Private _fldTaxyear2012 As Boolean
        Private _IsRejected As Boolean
        Private _RejectCode As RejectCodeType
        Private _NoticeReason As String = ""
        Private _IsDismissedForRejection As Boolean
        Private _fldTaxyear2013 As Boolean
        Private _fldTaxyear2014 As Boolean
        Private _fldTaxyear2015 As Boolean

        Public Property fldTaxyear2016 As Boolean
        Public Property fldTaxyear2017 As Boolean
        Public Property fldTaxyear2018 As Boolean
        Public Property fldTaxyear2019 As Boolean
        Public Property fldTaxyear2020 As Boolean
        Public Property fldTaxyear2021 As Boolean
        Public Property fldTaxyear2022 As Boolean
        Public Property fldTaxyear2023 As Boolean
        Public Property fldTaxyear2024 As Boolean
        Public Property fldTaxyear2025 As Boolean


        Public Property fldTaxyear2015() As Boolean
            Get
                Return _fldTaxyear2015
            End Get
            Set(ByVal value As Boolean)
                _fldTaxyear2015 = value
            End Set
        End Property
        Public Property fldTaxyear2014() As Boolean
            Get
                Return _fldTaxyear2014
            End Get
            Set(ByVal value As Boolean)
                _fldTaxyear2014 = value
            End Set
        End Property
        Public Property fldTaxyear2013() As Boolean
            Get
                Return _fldTaxyear2013
            End Get
            Set(ByVal value As Boolean)
                _fldTaxyear2013 = value
            End Set
        End Property
        Public Property fldordernumber() As Integer
            Get
                Return _fldordernumber
            End Get
            Set(ByVal Value As Integer)
                _fldordernumber = value
            End Set
        End Property

        Public Property fldListid() As Decimal
            Get
                Return _fldListid
            End Get
            Set(ByVal Value As Decimal)
                _fldListid = value
            End Set
        End Property

        Public Property fldlisttype() As Decimal
            Get
                Return _fldlisttype
            End Get
            Set(ByVal Value As Decimal)
                _fldlisttype = value
            End Set
        End Property

        Public Property fldCompanyID() As Integer
            Get
                Return _fldCompanyID
            End Get
            Set(ByVal Value As Integer)
                _fldCompanyID = value
            End Set
        End Property

        Public Property fldcustomeriD() As Integer
            Get
                Return _fldcustomeriD
            End Get
            Set(ByVal Value As Integer)
                _fldcustomeriD = value
            End Set
        End Property

        Public Property fldrequestname() As String
            Get
                Return _fldrequestname
            End Get
            Set(ByVal Value As String)
                _fldrequestname = value
            End Set
        End Property

        Public Property fldsecondname() As String
            Get
                Return _fldsecondname
            End Get
            Set(ByVal Value As String)
                _fldsecondname = value
            End Set
        End Property

        Public Property fldssnno() As String
            Get
                Return _fldssnno
            End Get
            Set(ByVal Value As String)
                _fldssnno = value
            End Set
        End Property

        Public Property fldtaxyear2003() As Boolean
            Get
                Return _fldtaxyear2003
            End Get
            Set(ByVal Value As Boolean)
                _fldtaxyear2003 = value
            End Set
        End Property

        Public Property fldtaxyear2002() As Boolean
            Get
                Return _fldtaxyear2002
            End Get
            Set(ByVal Value As Boolean)
                _fldtaxyear2002 = value
            End Set
        End Property

        Public Property fldtaxyear2001() As Boolean
            Get
                Return _fldtaxyear2001
            End Get
            Set(ByVal Value As Boolean)
                _fldtaxyear2001 = value
            End Set
        End Property

        Public Property fldtaxyear2000() As Boolean
            Get
                Return _fldtaxyear2000
            End Get
            Set(ByVal Value As Boolean)
                _fldtaxyear2000 = value
            End Set
        End Property

        Public Property FormType() As FormTypeCodeType
            Get
                Return Val(_fldtypeofform)
            End Get
            Set(ByVal value As FormTypeCodeType)
                _fldtypeofform = CInt(value)
            End Set
        End Property
        Public Property fldtypeofform() As String
            Get
                Return _fldtypeofform
            End Get
            Set(ByVal Value As String)
                _fldtypeofform = Value
            End Set
        End Property
        Public Property fldordertype() As String
            Get
                Return _fldordertype
            End Get
            Set(ByVal Value As String)
                _fldordertype = Value
            End Set
        End Property

        Public Property fldemail() As String
            Get
                Return _fldemail
            End Get
            Set(ByVal Value As String)
                _fldemail = value
            End Set
        End Property

        Public Property fldfax() As String
            Get
                Return _fldfax
            End Get
            Set(ByVal Value As String)
                _fldfax = value
            End Set
        End Property

        Public Property fldfaxno() As String
            Get
                Return _fldfaxno
            End Get
            Set(ByVal Value As String)
                _fldfaxno = value
            End Set
        End Property

        Public Property fldstatus() As String
            Get
                Return _fldstatus
            End Get
            Set(ByVal Value As String)
                _fldstatus = value
            End Set
        End Property

        Public Property fldDOB() As String
            Get
                Return _fldDOB
            End Get
            Set(ByVal Value As String)
                _fldDOB = value
            End Set
        End Property

        Public Property fldSex() As String
            Get
                Return _fldSex
            End Get
            Set(ByVal Value As String)
                _fldSex = value
            End Set
        End Property

        Public Property fldbillingstatus() As Boolean
            Get
                Return _fldbillingstatus
            End Get
            Set(ByVal Value As Boolean)
                _fldbillingstatus = value
            End Set
        End Property

        Public Property flddeliverydate() As DateTime
            Get
                Return _flddeliverydate
            End Get
            Set(ByVal Value As DateTime)
                _flddeliverydate = value
            End Set
        End Property

        Public Property fldPdf() As String
            Get
                Return _fldPdf
            End Get
            Set(ByVal Value As String)
                _fldPdf = value
            End Set
        End Property

        Public Property fldOrderdate() As DateTime
            Get
                Return _fldOrderdate
            End Get
            Set(ByVal Value As DateTime)
                _fldOrderdate = value
            End Set
        End Property

        Public Property fldTaxyear2004() As Boolean
            Get
                Return _fldTaxyear2004
            End Get
            Set(ByVal Value As Boolean)
                _fldTaxyear2004 = value
            End Set
        End Property

        Public Property fldSpecialFlag() As String
            Get
                Return _fldSpecialFlag
            End Get
            Set(ByVal Value As String)
                _fldSpecialFlag = value
            End Set
        End Property

        Public Property fldTaxyear2005() As Boolean
            Get
                Return _fldTaxyear2005
            End Get
            Set(ByVal Value As Boolean)
                _fldTaxyear2005 = value
            End Set
        End Property

        Public Property fldTaxyear2006() As Boolean
            Get
                Return _fldTaxyear2006
            End Get
            Set(ByVal Value As Boolean)
                _fldTaxyear2006 = value
            End Set
        End Property

        Public Property ListID() As String
            Get
                Return _ListID
            End Get
            Set(ByVal Value As String)
                _ListID = value
            End Set
        End Property

        Public Property bUpdatedInQB() As Boolean
            Get
                Return _bUpdatedInQB
            End Get
            Set(ByVal Value As Boolean)
                _bUpdatedInQB = value
            End Set
        End Property

        Public Property fldTaxyear2007() As Boolean
            Get
                Return _fldTaxyear2007
            End Get
            Set(ByVal Value As Boolean)
                _fldTaxyear2007 = value
            End Set
        End Property

        Public Property fldTaxyear2008() As Boolean
            Get
                Return _fldTaxyear2008
            End Get
            Set(ByVal Value As Boolean)
                _fldTaxyear2008 = value
            End Set
        End Property

        Public Property fldTaxyear2009() As Boolean
            Get
                Return _fldTaxyear2009
            End Get
            Set(ByVal Value As Boolean)
                _fldTaxyear2009 = value
            End Set
        End Property

        Public Property fldTaxyear2010() As Boolean
            Get
                Return _fldTaxyear2010
            End Get
            Set(ByVal Value As Boolean)
                _fldTaxyear2010 = value
            End Set
        End Property

        Public Property fldLoanNumber() As String
            Get
                Return _fldLoanNumber
            End Get
            Set(ByVal Value As String)
                _fldLoanNumber = value
            End Set
        End Property

        Public Property QBBatchNumber() As Integer
            Get
                Return _QBBatchNumber
            End Get
            Set(ByVal Value As Integer)
                _QBBatchNumber = value
            End Set
        End Property

        Public Property UpdatedInQBOn() As DateTime
            Get
                Return _UpdatedInQBOn
            End Get
            Set(ByVal Value As DateTime)
                _UpdatedInQBOn = value
            End Set
        End Property

        Public Property fldTaxyear2011() As Boolean
            Get
                Return _fldTaxyear2011
            End Get
            Set(ByVal Value As Boolean)
                _fldTaxyear2011 = value
            End Set
        End Property

        Public Property fldTaxyear2012() As Boolean
            Get
                Return _fldTaxyear2012
            End Get
            Set(ByVal Value As Boolean)
                _fldTaxyear2012 = value
            End Set
        End Property

        Public Property IsRejected() As Boolean
            Get
                Return _IsRejected
            End Get
            Set(ByVal Value As Boolean)
                _IsRejected = value
            End Set
        End Property

        Public Property RejectCode() As RejectCodeType
            Get
                Return _RejectCode
            End Get
            Set(ByVal Value As RejectCodeType)
                _RejectCode = Value
            End Set
        End Property

        'Public Property NoticeReason() As String
        '    Get
        '        Return _NoticeReason
        '    End Get
        '    Set(ByVal Value As String)
        '        _NoticeReason = value
        '    End Set
        'End Property

        Public Property IsDismissedForRejection() As Boolean
            Get
                Return _IsDismissedForRejection
            End Get
            Set(ByVal Value As Boolean)
                _IsDismissedForRejection = value
            End Set
        End Property
    End Class
End Namespace