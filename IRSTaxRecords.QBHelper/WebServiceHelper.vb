Imports IRSTaxRecords.QBHelper.QBWebService

Public Class WebServiceHelper
    Public Shared UserName As String = ""
    Public Shared Password As String = ""
    Private Shared _Service As IRSTaxServices = Nothing

    Public Shared Function Login() As Boolean
        If UserName.Trim = "" Then Throw New Exception("Please enter user name.") : Return False
        If Password = "" Then Throw New Exception("Please enter password.") : Return False

        Try
            Service.Authenticate()
            Return True
        Catch ex As Exception
            Throw New Exception("Login Failed. Please check username and/or password. " & ex.Message)
        End Try
    End Function

    Public Shared ReadOnly Property Service() As IRSTaxServices
        Get
            If _Service Is Nothing Then
                _Service = New IRSTaxServices
                _Service.AuthHeaderValue = New QBWebService.AuthHeader
                _Service.AuthHeaderValue.UserName = UserName
                _Service.AuthHeaderValue.Password = Password
                _Service.Url = WebServiceURL
            End If
            
            Return _Service
        End Get
    End Property
End Class
