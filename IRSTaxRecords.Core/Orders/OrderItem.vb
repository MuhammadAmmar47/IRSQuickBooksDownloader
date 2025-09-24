Namespace Orders
    Public Class OrderItem
        Dim _id As Decimal
        Dim _OrderId As Integer
        Dim _Qty As Integer
        Dim _DisplayName As String = ""
        Dim _DisplayDescription As String = ""
        Dim _SitePrice As Decimal
        Dim _SiteCost As Decimal
        Dim _TaxTotal As Decimal
        Dim _ProductType As ProductType
        Dim _ProductId As Integer


        Public Property id() As Decimal
            Get
                Return _id
            End Get
            Set(ByVal Value As Decimal)
                _id = value
            End Set
        End Property

        Public Property OrderId() As Integer
            Get
                Return _OrderId
            End Get
            Set(ByVal Value As Integer)
                _OrderId = value
            End Set
        End Property

        Public Property Qty() As Integer
            Get
                Return _Qty
            End Get
            Set(ByVal Value As Integer)
                _Qty = value
            End Set
        End Property

        Public Property DisplayName() As String
            Get
                Return _DisplayName
            End Get
            Set(ByVal Value As String)
                _DisplayName = value
            End Set
        End Property

        Public Property DisplayDescription() As String
            Get
                Return _DisplayDescription
            End Get
            Set(ByVal Value As String)
                _DisplayDescription = value
            End Set
        End Property

        Public Property SitePrice() As Decimal
            Get
                Return _SitePrice
            End Get
            Set(ByVal Value As Decimal)
                _SitePrice = value
            End Set
        End Property

        Public Property SiteCost() As Decimal
            Get
                Return _SiteCost
            End Get
            Set(ByVal Value As Decimal)
                _SiteCost = value
            End Set
        End Property

        Public Property TaxTotal() As Decimal
            Get
                Return _TaxTotal
            End Get
            Set(ByVal Value As Decimal)
                _TaxTotal = value
            End Set
        End Property

        Public Property ProductType() As ProductType
            Get
                Return _ProductType
            End Get
            Set(ByVal Value As ProductType)
                _ProductType = Value
            End Set
        End Property

        Public Property ProductId() As Integer
            Get
                Return _ProductId
            End Get
            Set(ByVal Value As Integer)
                _ProductId = value
            End Set
        End Property


    End Class
End Namespace
