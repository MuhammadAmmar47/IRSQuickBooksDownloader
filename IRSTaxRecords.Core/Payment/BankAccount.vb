Namespace Payments

    Public Class BankAccount

        Private _ID As Integer
        Private _ContactID As Integer

        Private _AccountTitle As String = ""
        Private _AccountNumber As String = ""
        Private _AccountType As BankAccountType
        Private _BankName As String = ""
        Private _RoutingNumber As String = ""

        Public Property ID() As Integer
            Get
                Return _ID
            End Get
            Set(ByVal value As Integer)
                _ID = value
            End Set

        End Property
        Public Property ContactID() As Integer
            Get
                Return _ContactID
            End Get
            Set(ByVal value As Integer)
                _ContactID = value
            End Set

        End Property
        Public Property AccountTitle() As String
            Get
                Return _AccountTitle
            End Get
            Set(ByVal value As String)
                _AccountTitle = value
            End Set

        End Property
        Public Property AccountNumber() As String
            Get
                Return _AccountNumber
            End Get
            Set(ByVal value As String)
                _AccountNumber = value
            End Set

        End Property
        Public Property AccountType() As BankAccountType
            Get
                Return _AccountType
            End Get
            Set(ByVal value As BankAccountType)
                _AccountType = value
            End Set

        End Property
        Public Property BankName() As String
            Get
                Return _BankName
            End Get
            Set(ByVal value As String)
                _BankName = value
            End Set

        End Property
        Public Property RoutingNumber() As String
            Get
                Return _RoutingNumber
            End Get
            Set(ByVal value As String)
                _RoutingNumber = value
            End Set

        End Property

    End Class
End Namespace