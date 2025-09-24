Public Class ContactUs
    Dim _id As Integer
    Dim _SenderName As String = ""
    Dim _Email As String = ""
    Dim _InterestedService As String = ""
    Dim _Message As String = ""
    Dim _SentOn As DateTime
    Dim _IPAddress As String = ""
    Dim _Phone As String = ""


    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal Value As Integer)
            _id = value
        End Set
    End Property

    Public Property SenderName() As String
        Get
            Return _SenderName
        End Get
        Set(ByVal Value As String)
            _SenderName = value
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

    Public Property InterestedService() As String
        Get
            Return _InterestedService
        End Get
        Set(ByVal Value As String)
            _InterestedService = value
        End Set
    End Property

    Public Property Message() As String
        Get
            Return _Message
        End Get
        Set(ByVal Value As String)
            _Message = value
        End Set
    End Property

    Public Property SentOn() As DateTime
        Get
            Return _SentOn
        End Get
        Set(ByVal Value As DateTime)
            _SentOn = value
        End Set
    End Property

    Public Property IPAddress() As String
        Get
            Return _IPAddress
        End Get
        Set(ByVal Value As String)
            _IPAddress = value
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
