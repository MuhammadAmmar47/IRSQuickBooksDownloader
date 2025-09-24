Namespace Content
    Public Class Analysis
        Dim _id As Integer
        Dim _FirstName As String = ""
        Dim _LastName As String = ""
        Dim _Email As String = ""
        Dim _PhoneNumber As String = ""
        Dim _ServicesRequested As String = ""
        Dim _LearnMoreRequested As String = ""
        Dim _RequestedOn As DateTime
        Dim _bProcessed As Boolean

        Private _ZipCode As String = ""
        Private _Address As String = ""
        Private _City As String = ""
        Private _State As String = ""

        Public Property id() As Integer
            Get
                Return _id
            End Get
            Set(ByVal Value As Integer)
                _id = value
            End Set
        End Property

        Public Property FirstName() As String
            Get
                Return _FirstName
            End Get
            Set(ByVal Value As String)
                _FirstName = value
            End Set
        End Property

        Public Property LastName() As String
            Get
                Return _LastName
            End Get
            Set(ByVal Value As String)
                _LastName = value
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

        Public Property PhoneNumber() As String
            Get
                Return _PhoneNumber
            End Get
            Set(ByVal Value As String)
                _PhoneNumber = value
            End Set
        End Property

        Public Property ServicesRequested() As String
            Get
                Return _ServicesRequested
            End Get
            Set(ByVal Value As String)
                _ServicesRequested = value
            End Set
        End Property

        Public Property LearnMoreRequested() As String
            Get
                Return _LearnMoreRequested
            End Get
            Set(ByVal Value As String)
                _LearnMoreRequested = value
            End Set
        End Property

        Public Property RequestedOn() As DateTime
            Get
                Return _RequestedOn
            End Get
            Set(ByVal Value As DateTime)
                _RequestedOn = value
            End Set
        End Property

        Public Property IsProcessed() As Boolean
            Get
                Return _bProcessed
            End Get
            Set(ByVal Value As Boolean)
                _bProcessed = Value
            End Set
        End Property


        Public Property Address() As String
            Get
                Return _Address
            End Get
            Set(ByVal value As String)
                _Address = value
            End Set
        End Property
        Public Property City() As String
            Get
                Return _City
            End Get
            Set(ByVal value As String)
                _City = value
            End Set
        End Property
        Public Property State() As String
            Get
                Return _State
            End Get
            Set(ByVal value As String)
                _State = value
            End Set
        End Property
        Public Property ZipCode() As String
            Get
                Return _ZipCode
            End Get
            Set(ByVal value As String)
                _ZipCode = value
            End Set
        End Property
    End Class
End Namespace