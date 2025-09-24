Public Class ReferAFriend
    Private _id As Integer
    Private _Email As String = ""
    Private _FirstName As String = ""
    Private _LastName As String = ""
    Private _Address As String = ""
    Private _Address2 As String = ""
    Private _City As String = ""
    Private _State As String = ""
    Private _Zip As String = ""
    Private _Phone As String = ""
    Private _ReferredByID As Integer
    Private _ReferredOn As DateTime

    Public Property ReferredByID() As Integer
        Get
            Return _ReferredByID
        End Get
        Set(ByVal Value As Integer)
            _ReferredByID = Value
        End Set
    End Property

    Public Property ReferredOn() As DateTime
        Get
            Return _ReferredOn
        End Get
        Set(ByVal Value As DateTime)
            _ReferredOn = value
        End Set
    End Property


    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal Value As Integer)
            _id = value
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

    Public Property Address() As String
        Get
            Return _Address
        End Get
        Set(ByVal Value As String)
            _Address = value
        End Set
    End Property

    Public Property Address2() As String
        Get
            Return _Address2
        End Get
        Set(ByVal Value As String)
            _Address2 = value
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

    Public Property Phone() As String
        Get
            Return _Phone
        End Get
        Set(ByVal Value As String)
            _Phone = value
        End Set
    End Property
End Class
