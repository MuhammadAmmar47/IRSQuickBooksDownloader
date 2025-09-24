Namespace Payments

    Public Class CreditCard

        Private _CardID As Integer = 0
        Private _CardType As CreditCardType
        Private _CardNumber As String = ""
        Private _ExpirationMonth As Integer
        Private _ExpirationYear As Integer
        Private _SecurityCode As String = ""
        Private _CardholdersName As String = ""

        Private _ContactID As Integer = 0


        Public Property ID() As Integer
            Get
                Return _CardID
            End Get
            Set(ByVal value As Integer)
                _CardID = value
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
        Public Property CardType() As CreditCardType
            Get
                Return _CardType
            End Get
            Set(ByVal value As CreditCardType)
                _CardType = value
            End Set
        End Property
        Public Property CardNumber() As String
            Get
                Return _CardNumber
            End Get
            Set(ByVal value As String)
                _CardNumber = value
            End Set
        End Property
        Public Property ExpirationMonth() As Integer
            Get
                Return _ExpirationMonth
            End Get
            Set(ByVal value As Integer)
                _ExpirationMonth = value
            End Set
        End Property
        Public Property ExpirationYear() As Integer
            Get
                Return _ExpirationYear
            End Get
            Set(ByVal value As Integer)
                _ExpirationYear = value
            End Set
        End Property
        Public Property SecurityCode() As String
            Get
                Return _SecurityCode
            End Get
            Set(ByVal value As String)
                _SecurityCode = value
            End Set
        End Property
        Public Property CardholdersName() As String
            Get
                Return _CardholdersName
            End Get
            Set(ByVal value As String)
                _CardholdersName = value
            End Set
        End Property
    End Class
End Namespace