Public Class ApplicationSettings
    Public Shared ReadOnly Property WebServiceURL() As String
        Get
            Dim setting As String = System.Configuration.ConfigurationManager.AppSettings("WebserviceURL")
            If setting Is Nothing OrElse setting = "" Then setting = "https://www.irstaxrecords.com/IRSTaxServices.asmx"
            Return setting
        End Get
    End Property
End Class
