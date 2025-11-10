Imports System.Configuration

Public Class AppSettings

    Public Shared ReadOnly Property ConnectionString() As String
        Get
            Return ConfigurationManager.AppSettings("ConnectionString")
        End Get
    End Property

#Region "Authorize.NET related settings"
    Public Shared ReadOnly Property AuthorizeLoginID() As String
        Get
            Return GetSetting("Authorize_LoginID")
        End Get
    End Property
    Public Shared ReadOnly Property AuthorizeEmailCustomer() As String
        Get
            Return GetSetting("Authorize_Email_Customer")
        End Get
    End Property
    Public Shared ReadOnly Property Authorize_Trans_Key() As String
        Get
            Return GetSetting("Authorize_Trans_Key")
        End Get
    End Property
    Public Shared ReadOnly Property Authorize_TestMode() As Boolean
        Get
            If GetSetting("Authorize_TestMode").ToLower.Trim = "true" Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property
    Public Shared ReadOnly Property Authorize_TransURL() As String
        Get
            Return GetSetting("Authorize_TransURL")
        End Get
    End Property
#End Region

    'Public Shared ReadOnly Property CustomerSupportName() As String
    '    Get
    '        Return GetSetting("CustomerSupportName")
    '    End Get
    'End Property
    'Public Shared ReadOnly Property CustomerSupportEmail() As String
    '    Get
    '        Return GetSetting("CustomerSupportEmail")
    '    End Get
    'End Property
    'Public Shared ReadOnly Property EMailServer() As String
    '    Get
    '        Return GetSetting("EMailServer")
    '    End Get
    'End Property
    'Public Shared ReadOnly Property EMailUserName() As String
    '    Get
    '        Return GetSetting("EMailUserName")
    '    End Get
    'End Property
    'Public Shared ReadOnly Property EMailPassword() As String
    '    Get
    '        Return GetSetting("EMailPassword")
    '    End Get
    'End Property

    Public Shared ReadOnly Property WebApplicationName() As String
        Get
            Return GetSetting("WebApplicationName")
        End Get
    End Property
    Public Shared ReadOnly Property AddDeliveryDays() As Integer
        Get
            Dim _addDeliveryDays As String = GetSetting("AddDeliveryDays")
            If (_addDeliveryDays.IsNullOrEmpty) Then
                Return 3
            End If
            Return GetSetting("AddDeliveryDays")
        End Get
    End Property

#Region "Email Settings"
    Public Shared ReadOnly Property CustomerSupportName() As String
        Get
            Return EmailFromName
        End Get
    End Property
    Public Shared ReadOnly Property CustomerSupportEmail() As String
        Get
            Return EmailFromEmail
        End Get
    End Property
    Public Shared ReadOnly Property CCEmailAddress() As String
        Get
            Return GetSetting("CCEmailAddress")
        End Get
    End Property

    Public Shared ReadOnly Property EmailFromName() As String
        Get
            Return GetSetting("EmailFromName")
        End Get
    End Property
    Public Shared ReadOnly Property EmailFromEmail() As String
        Get
            Return GetSetting("EmailFromEmail")
        End Get
    End Property
    Public Shared ReadOnly Property EmailServer() As String
        Get
            Return GetSetting("EmailServer")
        End Get
    End Property
    Public Shared ReadOnly Property EmailPort() As Integer
        Get
            Dim port As Integer = Val(GetSetting("EmailPort"))
            If port = 0 Then port = 25
            Return port
        End Get
    End Property
    Public Shared ReadOnly Property EmailRequiresAuthentication() As Boolean
        Get
            Dim value As String = GetSetting("EmailRequiresAuthentication")
            If value Is Nothing Then value = ""
            If value.Trim.ToLower = "true" OrElse value = "1" Then Return True
            Return False
        End Get
    End Property
    Public Shared ReadOnly Property EmailUseSSL() As Boolean
        Get
            Dim value As String = GetSetting("EmailUseSSL")
            If value Is Nothing Then value = ""
            If value.ToLower.Trim = "true" OrElse value = "1" Then Return True
            Return False
        End Get
    End Property
    Public Shared ReadOnly Property EmailUserName() As String
        Get
            Return GetSetting("EmailUserName")
        End Get
    End Property
    Public Shared ReadOnly Property EmailPassword() As String
        Get
            Return GetSetting("EmailPassword")
        End Get
    End Property
#End Region


    Public Shared ReadOnly Property IsAppInTestMode() As Boolean
        Get
            If GetSetting("IsAppInTestMode").ToLower.Trim = "true" Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property


    Public Shared ReadOnly Property SiteStandardRoot() As String
        Get
            Return GetSetting("SiteStandardRoot")
        End Get
    End Property
    Public Shared ReadOnly Property SiteSecureRoot() As String
        Get
            Return GetSetting("SiteSecureRoot")
        End Get
    End Property

    Public Shared ReadOnly Property OrderPadFilesDiskFolderPath(ByVal CustomerID As Integer) As String
        Get
            Dim value As String = GetSettingFromDB("OrderPadFilesDiskFolderPath")
            If value Is Nothing OrElse value = "" Then value = "C:\temp\OrderPadFiles\"
            If Not value.EndsWith("\") Then value += "\"
            value += CustomerID.ToString & "\"
            If Not System.IO.Directory.Exists(value) Then System.IO.Directory.CreateDirectory(value)
            Return value
        End Get
    End Property
    Public Shared ReadOnly Property MoveDeliveredPDFToFolderPath As String
        Get
            Dim path As String = GetSetting("MoveDeliveredPDFToFolderPath")
            If String.IsNullOrEmpty(path) Then
                Throw New Exception("MoveDeliveredPDFToFolderPath is not defined.")
            End If
            If Not path.EndsWith("\") Then path += "\"
            If Not System.IO.Directory.Exists(path) Then System.IO.Directory.CreateDirectory(path)
            Return path
        End Get
    End Property
    Public Shared ReadOnly Property PDFSavePath() As String
        Get
            Dim path As String = System.Configuration.ConfigurationManager.AppSettings("PDFSavePath")
            If String.IsNullOrEmpty(path) Then
                Throw New Exception("PDFSavePath is not defined in config.")
            End If
            If Not path.EndsWith("\") Then path += "\"
            If Not System.IO.Directory.Exists(path) Then System.IO.Directory.CreateDirectory(path)
            Return path
        End Get
    End Property

#Region "SEtting REader/Writer"
    Private Shared Function GetSetting(ByVal SettingName As String) As String
        Dim value As String = ConfigurationManager.AppSettings(SettingName)
        If value Is Nothing OrElse value = "" Then
            value = GetSettingFromDB(SettingName)
        End If
        Return value
    End Function
    
    Public Shared Function DeleteSetting(ByVal SettingName As String) As Boolean
        Return DataServices.ApplicationSetting_Delete(SettingName)
    End Function
    Public Shared Function GetSettingFromDB(ByVal SettingName As String) As String
        Return DataServices.ApplicationSetting_Get(SettingName)
    End Function
    Public Shared Function UpdateSettingInDB(ByVal SettingName As String, ByVal SettingValue As String) As Boolean
        Return DataServices.ApplicationSetting_Update(SettingName, SettingValue)
    End Function
    Public Shared Function ListAllSettings() As DataTable
        Return DataServices.ApplicationSetting_ListAll
    End Function
#End Region
End Class
