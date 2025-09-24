Public Class AuthHeader
    Inherits System.Web.Services.Protocols.SoapHeader

    Private _UserName As String = ""
    Private _Password As String = ""

    Public Property UserName() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property
    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal value As String)
            _Password = value
        End Set
    End Property
End Class
