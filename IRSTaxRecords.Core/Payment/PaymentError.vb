Namespace Payments
    Public Class PaymentError
        Private _ErrorCode As String = ""
        Private _ErrorDetail As String = ""
        Public Sub New()
        End Sub
        Public Sub New(ByVal code As String, ByVal detail As String)
            _ErrorCode = code
            _ErrorDetail = detail
        End Sub
        Public Property ErrorCode() As String
            Get
                Return _ErrorCode
            End Get
            Set(ByVal value As String)
                _ErrorCode = value
            End Set
        End Property
        Public Property ErrorDetail() As String
            Get
                Return _ErrorDetail
            End Get
            Set(ByVal value As String)
                _ErrorDetail = value
            End Set
        End Property
    End Class
End Namespace