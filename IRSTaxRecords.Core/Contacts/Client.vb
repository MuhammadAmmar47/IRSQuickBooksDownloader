Namespace Contacts
    Public Class Client
        Private _ID As Integer
        Private _ClientName As String = ""
        Private _LoginUserName As String = ""
        Private _LoginPassword As String = ""
        Private _AddStatistics As Boolean


        Public Property ID() As Integer
            Get
                Return _ID
            End Get
            Set(ByVal Value As Integer)
                _ID = value
            End Set
        End Property

        Public Property ClientName() As String
            Get
                Return _ClientName
            End Get
            Set(ByVal Value As String)
                _ClientName = value
            End Set
        End Property

        Public Property LoginUserName() As String
            Get
                Return _LoginUserName
            End Get
            Set(ByVal Value As String)
                _LoginUserName = value
            End Set
        End Property

        Public Property LoginPassword() As String
            Get
                Return _LoginPassword
            End Get
            Set(ByVal Value As String)
                _LoginPassword = value
            End Set
        End Property

        Public Property AddStatistics() As Boolean
            Get
                Return _AddStatistics
            End Get
            Set(ByVal Value As Boolean)
                _AddStatistics = value
            End Set
        End Property

    End Class
End Namespace
