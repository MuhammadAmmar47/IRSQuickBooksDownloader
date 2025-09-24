Imports System.ComponentModel

Public Class Form1

    Private WithEvents Download As New QBHelper.DownloadCustomers
    Private WithEvents DownloadInvoices As New QBHelper.DownloadInvoices

    Private _FileName As String = ""
    Private ReadOnly Property LogFileName() As String
        Get
            If _FileName = "" Then
                'Get the log file name
                Dim fInfo As New IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)
                If fInfo.Directory.FullName.EndsWith("\") Then
                    _FileName = fInfo.Directory.FullName & "TraceLog.txt"
                Else
                    _FileName = fInfo.Directory.FullName & "\TraceLog.txt"
                End If
            End If
            Return _FileName
        End Get
    End Property


#Region "Customers Download"
    Private Sub btnCustomersDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomersDownload.Click
        If Not CheckLogin() Then Return
        lblProgress.Text = "Starting downloading, wait for update...."
        Download.StartDownloading()
    End Sub
    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        Download.StopDownloading()
    End Sub
#Region "Logging Functions"
    Private Sub Download_DownloadFinished(ByVal totalSuccess As Integer) Handles Download.DownloadFinished
        MsgBox("Download finished.... with " & totalSuccess & " success")
    End Sub
    Private Sub Download_DownloadStatus(ByVal total As Integer, ByVal totalDone As Integer) Handles Download.DownloadStatus
        Try
            lblProgress.Text = totalDone & " of " & total & " Processed."
            Me.ProgressBar1.Maximum = total
            Me.ProgressBar1.Value = totalDone
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub Download_Error(ByVal ex As String) Handles Download.Error
        Me.txtError.Text += ex & vbCrLf
        Me.txtError.ScrollToCaret()
    End Sub
#End Region
#End Region

#Region "Form Load"
#Region "Temp Methods"
    Private Function GetCompanyToBillTo(ByVal c As QBHelper.QBWebService.Customer) As QBHelper.QBWebService.Customer
        'If c.ParentID < 1 OrElse c.ParentID = 9459 Then
        If c.ParentID < 1 OrElse c.ParentID = 9545 OrElse c.CompanyName = "IRS Tax Records Customer List" Then
            Return c
        End If
        'Get the parent of the customer and bill to him
        c = QBHelper.WebServiceHelper.Service.GetCustomer(c.ParentID)
        Return GetCompanyToBillTo(c)
    End Function
    Public Sub StartDownloading(ByVal fromDate As DateTime, ByVal ToDate As DateTime, ByVal CurrentBatchNumber As Integer)
        Dim customers() As Integer
        Try
            QBHelper.WebServiceHelper.Service.ClearAllErrorsBeforeDownloading()
        Catch ex As Exception
            Return
        End Try



        Try
            Application.DoEvents()
            customers = QBHelper.WebServiceHelper.Service.GetCompaniesWithPendingInvoices(fromDate, ToDate)
            Application.DoEvents()
        Catch ex As Exception
            Return
        End Try

        If customers Is Nothing OrElse customers.Length = 0 OrElse (customers.Length = 1 AndAlso customers(0) = 0) Then
            Application.DoEvents()
            'RaiseEvent [Error]("No new customer with pending invoices found")
            Application.DoEvents()
            Return
        End If



        Application.DoEvents()
        Dim totalSuccess As Integer = 0
        Dim totalDone As Integer = 0



        'Make sure that the invoice item is available for pricing
        Dim qb As New QBHelper.QuickBooksProcessor

        For temp As Integer = 0 To customers.Length - 1
            Try
                Application.DoEvents()

                Application.DoEvents()
                Dim c As QBHelper.QBWebService.Customer = QBHelper.WebServiceHelper.Service.GetCustomer(customers(temp))
                Application.DoEvents()
                If Not c Is Nothing Then

                    'Check, for which customer it should add bill
                    Dim originalCustomerId As Integer = c.CustomerID
                    c = GetCompanyToBillTo(c)

                    If Not c Is Nothing Then
                        Dim invoices() As Integer = QBHelper.WebServiceHelper.Service.GetPendingInvoiceOfCompany(fromDate, ToDate, originalCustomerId)
                        Application.DoEvents()
                        If invoices.Length > 0 Then
                            Application.DoEvents()
                            Application.DoEvents()


                            Dim listID As String = ""
                            If c.CreditCardActive Then
                                listID = qb.AddInvoice(c, invoices, False, True)
                            Else
                                listID = qb.AddInvoice(c, invoices, True, False)
                            End If
                            Application.DoEvents()
                            If listID = "" Then
                                Application.DoEvents()

                                Application.DoEvents()
                            Else
                                totalSuccess += 1




                                For i As Integer = 0 To invoices.Length - 1
                                    Application.DoEvents()
                                    Dim result As Boolean = QBHelper.WebServiceHelper.Service.MarkInvoiceUpdatedInQB(invoices(i), listID, CurrentBatchNumber)
                                    If result = False Then
                                        Trace.WriteLine("failed to update invoice for order number  " & invoices(i).ToString)
                                    End If
                                    Application.DoEvents()
                                Next
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                Trace.WriteLine(ex.Message)
            End Try

            Application.DoEvents()
        Next



        qb.Disconnect()
        qb = Nothing

    End Sub
#End Region
    Private Sub doSomething()
        Try
            QBHelper.WebServiceHelper.UserName = System.Configuration.ConfigurationManager.AppSettings("WebServicePassword")
            QBHelper.WebServiceHelper.Password = System.Configuration.ConfigurationManager.AppSettings("WebServiceUserName")

            DownloadInvoices.TESTMe()
            Dim lastBatchNumber As Integer = DownloadInvoices.GetLastBatchNumber()
            lastBatchNumber += 1
            StartDownloading("4/1/2014", "4/25/2014", lastBatchNumber)

            Dim c As QBHelper.QBWebService.Customer = QBHelper.WebServiceHelper.Service.GetCustomer(12521)
            Dim list() As Integer = QBHelper.WebServiceHelper.Service.GetPendingInvoiceOfCompany("03/03/2014", "03/04/2014", 12521)
            For Each item As Integer In list
                Dim inv As QBHelper.QBWebService.Invoice = QBHelper.WebServiceHelper.Service.GetInvoice(item)
                Trace.WriteLine(inv.Taxyear2012)
            Next
        Catch ex As Exception
            Trace.WriteLine(ex.Message)
        End Try

    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If System.IO.File.Exists(Me.LogFileName) Then
            Dim newPath As String = System.IO.Path.GetDirectoryName(Me.LogFileName)
            If Not newPath.EndsWith("\") Then newPath += "\"
            newPath += System.IO.Path.GetFileNameWithoutExtension(Me.LogFileName) & "-" & Now.ToString("yyyy-MM-dd-HH-mm") & "-" & Now.Ticks.ToString & ".txt"
            System.IO.File.Move(Me.LogFileName, newPath)
        End If
        ErrorLogTraceListener.AddTraceListener("MainListener", Me.LogFileName)
        'doSomething()
        Trace.WriteLine($"Starting application....")
        Me.txtPassword.Text = System.Configuration.ConfigurationManager.AppSettings("WebServicePassword")
        Me.txtUserName.Text = System.Configuration.ConfigurationManager.AppSettings("WebServiceUserName")

        IRSTaxRecords.QBHelper.WebServiceHelper.UserName = txtUserName.Text
        IRSTaxRecords.QBHelper.WebServiceHelper.Password = txtPassword.Text

        Me.lblProgress.Text = ""
        Me.lblInvoicesInfo.Text = ""
        Me.txtStartInvoice.Text = QBHelper.mdlGeneral.CurrentInvoiceNumber

        Me.txtLastBatchNo.Text = DownloadInvoices.GetLastBatchNumber()


    End Sub
#End Region

#Region "Invoices download"
    Private Sub btnInvoicesDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInvoicesDownload.Click
        If Not CheckLogin() Then Return
        If dtFrom.Value > dtTo.Value Then
            MsgBox("From date must be less than or equal to To date.")
            Return
        End If

        If MsgBox("Do you want to delete previous log file?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question) = MsgBoxResult.Yes Then
            DeleteLog()
        End If
        AddToLog(LOG_TYPE.INFORMATION, "Starting downloading of invoices from " & Me.dtFrom.Value.ToString & " to " & Me.dtTo.Value.ToString)

        'Get Last Batch Number....
        Dim lastBatchNumber As Integer = DownloadInvoices.GetLastBatchNumber()
        If lastBatchNumber < 0 Then
            MsgBox("Failed to get last batch number. Can't continue...")
            Return
        End If

        lastBatchNumber += 1

        Try
            DownloadInvoices.StartDownloading(dtFrom.Value, dtTo.Value, lastBatchNumber)
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        End Try
        Try
            Me.txtLastBatchNo.Text = DownloadInvoices.GetLastBatchNumber()
        Catch ex As Exception
            MsgBox("Failed to refresh last batch number. " & ex.Message)
        End Try


        My.Settings.Reload()
        Me.txtStartInvoice.Text = QBHelper.mdlGeneral.CurrentInvoiceNumber
    End Sub

    Private Sub btnCancelInvoicesDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelInvoicesDownload.Click
        DownloadInvoices.StopDownloading()
    End Sub
#Region "Object Events"
    Private Sub DownloadInvoices_DownloadFinished(ByVal totalSuccess As Integer) Handles DownloadInvoices.DownloadFinished
        MsgBox("Invoices download finished with total success of " & totalSuccess)
        Me.txtStartInvoice.Text = QBHelper.mdlGeneral.CurrentInvoiceNumber
        AddToLog(LOG_TYPE.INFORMATION, "Invoice download finished with total success of " & totalSuccess)
    End Sub
    Private Sub DownloadInvoices_DownloadStatus(ByVal total As Integer, ByVal totalDone As Integer) Handles DownloadInvoices.DownloadStatus
        'Me.lblInvoicesInfo.Text = "Processed " & totalDone & " of " & total
        Me.pgInvoices.Maximum = total
        Me.pgInvoices.Value = totalDone
    End Sub
    Private Sub DownloadInvoices_Error(errorMsg As String, ex As Exception) Handles DownloadInvoices.Error
        If ex IsNot Nothing Then errorMsg += ex.Message & vbCrLf & ex.StackTrace
        Me.txtLogInvoices.Text &= errorMsg & vbCrLf
        AddToLog(LOG_TYPE.ERROR, errorMsg)
    End Sub

    Private Sub DownloadInvoices_Information(ByVal msg As String) Handles DownloadInvoices.Information
        AddToLog(LOG_TYPE.INFORMATION, msg)
    End Sub
    Private Sub DownloadInvoices_NewCustomerProcessing(ByVal CustomerName As String, ByVal totalOrders As Integer) Handles DownloadInvoices.NewCustomerProcessing
        Me.lblInvoicesInfo.Text = "Processing customer " & CustomerName & ". total ordes = " & totalOrders
        AddToLog(LOG_TYPE.INFORMATION, "Processing customer " & CustomerName & ". total ordes = " & totalOrders)
    End Sub
#End Region
#End Region

    Private Sub btnUpdateInvNo_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles btnUpdateInvNo.LinkClicked
        If Not IsNumeric(Me.txtStartInvoice.Text) Then
            MsgBox("Please enter numeric values only")
            Return
        End If

        QBHelper.mdlGeneral.CurrentInvoiceNumber = Me.txtStartInvoice.Text

        MsgBox("Invoice counter updated.", MsgBoxStyle.Information)
    End Sub

    Private Sub btnRefreshBatchNo_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles btnRefreshBatchNo.LinkClicked
        Me.txtLastBatchNo.Text = DownloadInvoices.GetLastBatchNumber()
    End Sub

    Private Sub btnRevertLastDownload_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles btnRevertLastDownload.LinkClicked
        If MsgBox("This will unmark last batch's downloaded invoices. Do you want to continue?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Return
        Dim result As Integer = DownloadInvoices.RevertLastBatchNumber
        MsgBox(result & " records reverted from last batch.", MsgBoxStyle.Information)

        Me.txtLastBatchNo.Text = DownloadInvoices.GetLastBatchNumber()
    End Sub

    Private Sub btnTestAuth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestAuth.Click
        If CheckLogin() Then MsgBox("Login Successfull", MsgBoxStyle.Information)
    End Sub
    Private Function CheckLogin() As Boolean
        If txtUserName.Text.Trim = "" Then MsgBox("Please enter username") : Return False
        If txtPassword.Text.Trim = "" Then MsgBox("Please enter Password") : Return False

        Try
            IRSTaxRecords.QBHelper.WebServiceHelper.UserName = txtUserName.Text.Trim
            IRSTaxRecords.QBHelper.WebServiceHelper.Password = txtPassword.Text

            IRSTaxRecords.QBHelper.WebServiceHelper.Login()
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Trace.WriteLine($"Application closing, removing trace listener")
        ErrorLogTraceListener.RemoveTraceListener("MainListener")
    End Sub
End Class
