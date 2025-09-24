Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports IRSTaxRecords.Core

<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class IRSTaxServices
    Inherits System.Web.Services.WebService

    Public Authentication As AuthHeader = Nothing
    Private _CurrentUserID As Integer = 0

    Private Sub ValidateAuthentication(ByVal auth As AuthHeader)
        
        Dim userName As String = System.Configuration.ConfigurationManager.AppSettings("WebServiceUserName")
        Dim Password As String = System.Configuration.ConfigurationManager.AppSettings("WebServicePassword")

        If userName.ToLower <> auth.UserName.ToLower Then Throw New Exception("Invalid username or password")
        If Password <> auth.Password Then Throw New Exception("Invalid username or password")
        
    End Sub

    Private Sub ValidateAuthenticationForCustomer(ByVal auth As AuthHeader)
        Dim c As Customer = DataServices.GetCustomer(auth.UserName)
        If c Is Nothing Then
            Throw New Exception("Invalid username and/or password")
        Else
            If c.Password <> auth.Password Then Throw New Exception("Invalid username and/or password")
        End If

        _CurrentUserID = c.CustomerID
    End Sub


#Region "Customer Methods"
    <WebMethod(), SoapHeader("Authentication")> _
        Public Function Login() As Customer
        ValidateAuthenticationForCustomer(Authentication)
        Return DataServices.GetCustomer(Authentication.UserName)
    End Function
    <WebMethod(), SoapHeader("Authentication")> _
    Public Function UploadFile(ByVal fileName As String, ByVal fileContent() As Byte) As Integer
        ValidateAuthenticationForCustomer(Authentication)

        'SAve file to the disk
        Dim diskPath As String = AppSettings.OrderPadFilesDiskFolderPath(_CurrentUserID) & Guid.NewGuid.ToString & System.IO.Path.GetExtension(fileName)
        System.IO.File.WriteAllBytes(diskPath, fileContent)
        Dim file As New Core.Content.OrderPadFile
        With file
            .FileName = System.IO.Path.GetFileName(diskPath)
            .FileNameReal = System.IO.Path.GetFileName(fileName)
            .UploadedOn = Now
            .UserID = _CurrentUserID
        End With
        If OrderPadServices.AddNew(file) Then
            'Send email to admin...
            Try
                If Email.MailSender.SendOrderFilePadUploadedEmail(file) Then
                    Diagnostics.Trace.WriteLine($"Added new OrderPad entry with record# {file.ID}, UserID={file.UserID}, FileName={file.FileName}, RealFileName={file.FileNameReal}, Email Sent Successfully")
                    file.ErrorMessage = "Email Sent"
                    OrderPadServices.Update(file)
                Else
                    Diagnostics.Trace.WriteLine($"Added new OrderPad entry with record# {file.ID}, UserID={file.UserID}, FileName={file.FileName}, RealFileName={file.FileNameReal}, Email NOT Sent. {Email.MailSender.LastError}")
                    file.ErrorMessage = "Email not Sent"
                    OrderPadServices.Update(file)
                End If
            Catch ex As Exception
                Diagnostics.Trace.WriteLine("Failed to send new file uploaded email. " & ex.Message)
                file.ErrorMessage = "Failed to send new file uploaded email " & ex.Message
                OrderPadServices.Update(file)
            End Try
            Return file.ID
        Else
            Diagnostics.Trace.WriteLine($"Failed to save file. {DataHelper.LastErrorMessage}")
            Throw New Exception("Failed to save file. " & DataHelper.LastErrorMessage)
        End If
    End Function
#End Region

#Region "QB related methods"
    <WebMethod(), SoapHeader("Authentication")> _
    Public Function Authenticate() As Boolean
        ValidateAuthentication(Authentication)
        Return True
    End Function

    <WebMethod(), SoapHeader("Authentication")>
    Public Function GetCustomerFromBillToID(ByVal BillToID As Integer) As List(Of Integer)
        ValidateAuthentication(Authentication)
        Return DataServices.GetCustomersFromBillToID(BillToID)
    End Function

    <WebMethod(), SoapHeader("Authentication")> _
    Public Function GetCustomer(ByVal id As Integer) As Customer
        ValidateAuthentication(Authentication)
        Return DataServices.GetCustomer(id)
    End Function
    <WebMethod(), SoapHeader("Authentication")> _
   Public Function ClearAllErrorsBeforeDownloading() As Boolean
        ValidateAuthentication(Authentication)
        Return DataServices.ClearAllErrorsBeforeDownloading()
    End Function
    <WebMethod(), SoapHeader("Authentication")> _
    Public Function GetCustomerFromListId(ByVal listID As String) As Customer
        ValidateAuthentication(Authentication)
        Dim id As Integer = DataServices.GetCustomerFromListId(listID)
        If id > 0 Then Return DataServices.GetCustomer(id)
        Return Nothing
    End Function
    <WebMethod(), SoapHeader("Authentication")> _
       Public Function GetTotalNewCustomers() As Integer
        ValidateAuthentication(Authentication)
        Return DataServices.GetTotalNewCustomers()
    End Function
    <WebMethod(), SoapHeader("Authentication")> _
   Public Function GetNextNewCustomer() As Customer
        ValidateAuthentication(Authentication)
        Return DataServices.GetNextNewCustomer()
    End Function
    <WebMethod(), SoapHeader("Authentication")> _
    Public Function UpdateCustomer(ByVal c As Customer) As Boolean
        ValidateAuthentication(Authentication)
        Return DataServices.UpdateCustomer(c)
    End Function

    <WebMethod(), SoapHeader("Authentication")> _
    Public Function GetCompaniesWithPendingInvoices(ByVal dateFrom As DateTime, ByVal dateTo As DateTime) As Integer()
        ValidateAuthentication(Authentication)
        Return InvoiceServices.GetCompaniesWithPendingInvoices(dateFrom, dateTo)
    End Function
    <WebMethod(), SoapHeader("Authentication")> _
        Public Function MarkInvoiceUpdatedInQB(ByVal OrderNumber As Integer, ByVal ListID As String, ByVal QBBatchNumber As Integer) As Boolean
        ValidateAuthentication(Authentication)
        Return InvoiceServices.MarkInvoiceUpdatedInQB(OrderNumber, ListID, QBBatchNumber)
    End Function


    <WebMethod(), SoapHeader("Authentication")> _
    Public Function GetPendingInvoiceOfCompany(ByVal dateFrom As DateTime, ByVal dateTo As DateTime, ByVal CompanyID As Integer) As Integer()
        ValidateAuthentication(Authentication)
        Return InvoiceServices.GetPendingInvoicesOfCompany(dateFrom, dateTo, CompanyID)
    End Function
    <WebMethod(), SoapHeader("Authentication")> _
    Public Function GetInvoice(ByVal OrderNumber As Integer) As Invoice
        ValidateAuthentication(Authentication)
        Return InvoiceServices.GetInvoice(OrderNumber)
    End Function
    <WebMethod(), SoapHeader("Authentication")> _
  Public Function GetLastBatchNumber() As Integer
        ValidateAuthentication(Authentication)
        Return InvoiceServices.GetLastBatchNumber
    End Function

    <WebMethod(), SoapHeader("Authentication")> _
    Public Function GetInvoicesByListID(ByVal ListID As String) As Generic.List(Of Invoice)
        ValidateAuthentication(Authentication)
        Return InvoiceServices.GetInvoicesByListID(ListID)
    End Function
    <WebMethod(), SoapHeader("Authentication")> _
        Public Function GetInvoicesByCustomerName(ByVal CustomerName As String, ByVal orderDateFrom As DateTime, ByVal orderDateTo As DateTime) As Generic.List(Of Invoice)
        ValidateAuthentication(Authentication)
        Return InvoiceServices.GetInvoicesByCustomerName(CustomerName, orderDateFrom, orderDateTo)
    End Function

    'Will return number of records reverted
    <WebMethod(), SoapHeader("Authentication")> _
    Public Function RevertLastBatchNumber() As Integer
        ValidateAuthentication(Authentication)
        Return InvoiceServices.RevertLastBatchNumber
    End Function

    
#End Region


#Region "OrderPAD New API"
    Private Const ORDER_PAD_FILE_PATH As String = "~/bin/OrderPad.exe"

    <WebMethod(), SoapHeader("Authentication")> _
    Public Function OrderPadCurrentVersion() As String
        Dim assemblyPath As String = Server.MapPath(ORDER_PAD_FILE_PATH)
        If Not System.IO.File.Exists(assemblyPath) Then Throw New Exception("File " & assemblyPath & " doesn't exists")

        Dim ass As System.Reflection.Assembly = System.Reflection.Assembly.LoadFile(assemblyPath)
        Return ass.GetName().Version.ToString
        'Dim currentVersion As String = System.Configuration.ConfigurationManager.AppSettings("OrderPadCurrentVersion")
        'If currentVersion Is Nothing OrElse currentVersion = "" Then currentVersion = "1.0.0.0"
        'Return currentVersion
    End Function
    <WebMethod(), SoapHeader("Authentication")> _
    Public Function OrderPadGetLatestVersion() As Byte()
        Dim assemblyPath As String = Server.MapPath(ORDER_PAD_FILE_PATH)
        Dim bytes() As Byte = System.IO.File.ReadAllBytes(assemblyPath)
        Return bytes
    End Function
#End Region
End Class