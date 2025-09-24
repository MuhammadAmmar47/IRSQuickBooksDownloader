Namespace Content
    Public Class ListType
        Private _fldlistid As Integer
        Private _fldlisttype As Decimal
        Private _fldListname As String = ""
        Private _fldCurrentdate As String = ""
        Private _fldDateCheck As DateTime
        Private _IRSBatchNumber As String = ""


        Public Property fldlistid() As Integer
            Get
                Return _fldlistid
            End Get
            Set(ByVal Value As Integer)
                _fldlistid = value
            End Set
        End Property

        Public Property fldlisttype() As ListTypeCodeType
            Get
                Return _fldlisttype
            End Get
            Set(ByVal Value As ListTypeCodeType)
                _fldlisttype = Value
            End Set
        End Property

        Public Property fldListname() As String
            Get
                Return _fldListname
            End Get
            Set(ByVal Value As String)
                _fldListname = value
            End Set
        End Property

        Public Property fldCurrentdate() As String
            Get
                Return _fldCurrentdate
            End Get
            Set(ByVal Value As String)
                _fldCurrentdate = value
            End Set
        End Property

        Public Property fldDateCheck() As DateTime
            Get
                Return _fldDateCheck
            End Get
            Set(ByVal Value As DateTime)
                _fldDateCheck = value
            End Set
        End Property

        Public Property IRSBatchNumber() As String
            Get
                Return _IRSBatchNumber
            End Get
            Set(ByVal Value As String)
                _IRSBatchNumber = value
            End Set
        End Property
    End Class
End Namespace