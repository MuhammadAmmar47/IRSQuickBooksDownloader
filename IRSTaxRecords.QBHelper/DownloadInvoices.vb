Imports IRSTaxRecords.QBHelper.QBWebService
'Imports IRSTaxRecords.QBHelper.QBServiceLocal
Imports System.Windows.Forms


Public Class DownloadInvoices
    Public Event DownloadStatus(ByVal total As Integer, ByVal totalDone As Integer)
    Public Event DownloadFinished(ByVal totalSuccess As Integer)
    Public Event Information(ByVal msg As String)
    Public Event NewCustomerProcessing(ByVal CustomerName As String, ByVal totalOrders As Integer)
    Public Event [Error](errorMsg As String, ex As Exception)
    Private _Cancel As Boolean = False

    Public Function GetLastBatchNumber() As Integer
        Try
            Return WebServiceHelper.Service.GetLastBatchNumber()
        Catch ex As Exception
            RaiseEvent Error("Failed to get batch number. " & ex.Message, ex)
            Return -1
        End Try
    End Function
    Public Function RevertLastBatchNumber() As Integer
        Try
            Return WebServiceHelper.Service.RevertLastBatchNumber()
        Catch ex As Exception
            RaiseEvent Error("Failed to get revert last batch. " & ex.Message, ex)
            Return -1
        End Try
    End Function

    Public Sub TESTMe()
        Dim inv As IRSTaxRecords.QBHelper.QBWebService.Invoice = WebServiceHelper.Service.GetInvoice(928957)
        Trace.WriteLine(inv.Taxyear2016)
    End Sub
    Public Sub StartDownloading(ByVal fromDate As DateTime, ByVal ToDate As DateTime, ByVal CurrentBatchNumber As Integer)
        Dim customers() As Integer
        Try
            WebServiceHelper.Service.ClearAllErrorsBeforeDownloading()
        Catch ex As Exception
            RaiseEvent Error(ex.Message, ex)
            Return
        End Try

        RaiseEvent Information("Starting download from " & fromDate.ToShortDateString & " to " & ToDate.ToShortDateString)


        Try
            RaiseEvent Information("Finding customers within date range " & fromDate.ToString & " to " & ToDate.ToString)
            Application.DoEvents()
            customers = WebServiceHelper.Service.GetCompaniesWithPendingInvoices(fromDate, ToDate)
            Application.DoEvents()
            RaiseEvent Information(customers.Length & " customers found with date range " & fromDate.ToShortDateString & " to " & ToDate.ToString)
        Catch ex As Exception
            RaiseEvent Error(ex.Message, ex)
            Return
        End Try

        If customers Is Nothing OrElse customers.Length = 0 OrElse (customers.Length = 1 AndAlso customers(0) = 0) Then
            Application.DoEvents()
            RaiseEvent [Error]("No new customer with pending invoices found", Nothing)
            RaiseEvent DownloadFinished(0)
            Application.DoEvents()
            Return
        End If



        RaiseEvent Information(customers.Length & " invoices to build (customer records. Proceding...")
        Application.DoEvents()
        Dim totalSuccess As Integer = 0
        Dim totalDone As Integer = 0
        Dim qb As New QuickBooksProcessor()
        Try
            If Not qb.Connect Then
                RaiseEvent Error("Failed to connect to QuickBooks: " & qb.LastError, Nothing)
                Return
            End If
        Catch ex As Exception
            RaiseEvent Error(ex.Message, ex)
            Return
        End Try
        _Cancel = False

        'Make sure that the invoice item is available for pricing
        Dim InvListID As String = qb.GetInvoiceService(mdlXMLBuilder.SERVICE_CODE_NAME)
        If InvListID.Length < 7 Then
            qb.Disconnect()
            Throw New Exception(mdlXMLBuilder.SERVICE_CODE_NAME & " must be added as service item list in quick books. ")
        End If

        'Also check if the invoice item for YEARs is added in Quick Books.
        InvListID = qb.GetInvoiceService(mdlXMLBuilder.YEARS_CODE_NAME)
        If InvListID.Length < 7 Then
            qb.Disconnect()
            Throw New Exception(mdlXMLBuilder.YEARS_CODE_NAME & " must be added as service item list in quick books. ")
        End If

        If Not qb.ConfirmCustomFields() Then
            qb.Disconnect()
            Throw New Exception("Not all custom fields present and failed to add them automatically.")
        End If

        Dim customersProcessed As New Dictionary(Of Integer, Integer)


        For temp As Integer = 0 To customers.Length - 1
            RaiseEvent Information("Processing " & temp + 1 & " of " & customers.Length)
            Try
                Application.DoEvents()
                RaiseEvent DownloadStatus(customers.Length, temp + 1)

                'Dim thisCustomerID As Integer = customers(temp)
                'RaiseEvent Information($"Processing Customer with id  {thisCustomerID}")
                'If customersProcessed.ContainsKey(thisCustomerID) Then
                '    RaiseEvent Information($"Customer with id  {thisCustomerID} is already processed with customer ID {customersProcessed(thisCustomerID)}")
                '    Continue For
                'End If

                Application.DoEvents()
                Dim c As Customer = WebServiceHelper.Service.GetCustomer(customers(temp))
                Application.DoEvents()
                If Not c Is Nothing Then
                    RaiseEvent Information($"Processing Customer {c.CustomerID}, ParentID={c.ParentID}, CustomerName={c.Name}, CompanyName={c.CompanyName}")
                    If c.BillToID > 0 AndAlso customersProcessed.ContainsKey(c.BillToID) Then
                        RaiseEvent Information($"Customer {c.CustomerID}, ParentID={c.ParentID}, CustomerName={c.Name}, CompanyName={c.CompanyName} is already processed with Bill to CompanyID {c.BillToID}")
                        Continue For
                    End If
                    If c.BillToID > 0 Then customersProcessed.Add(c.BillToID, c.CustomerID)
                    'Check, for which customer it should add bill
                    Dim originalCustomerId As Integer = c.CustomerID
                    c = GetCompanyToBillTo(c, 1)

                    If c.ListId = "" OrElse qb.GetCustomerFromListID(c.ListId) Is Nothing Then
                        'Customer is NOT Already added, add now
                        RaiseEvent [Error]("Customer " & c.Name & " was not already added. Adding to Quick Books now", Nothing)
                        Dim listID As String = ""
                        Application.DoEvents()
                        If qb.AddCustomer(c, listID) Then
                            c.ListId = listID
                            Application.DoEvents()
                            WebServiceHelper.Service.UpdateCustomer(c)
                            Application.DoEvents()
                        Else
                            RaiseEvent Error("Failed to add customer " & c.Name & ". Will not process its invoices. Error " & qb.LastError, Nothing)
                            Continue For
                        End If
                    End If
                    If Not c Is Nothing Then
                        Dim invoices() As Integer = Nothing
                        If c.BillToID > 0 Then
                            invoices = WebServiceHelper.Service.GetPendingInvoiceOfCompany(fromDate, ToDate, c.BillToID)
                        Else
                            invoices = WebServiceHelper.Service.GetPendingInvoiceOfCompany(fromDate, ToDate, originalCustomerId)
                        End If


                        Application.DoEvents()
                        If invoices.Length > 0 Then
                            Application.DoEvents()
                            RaiseEvent NewCustomerProcessing(c.Name, invoices.Length)
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
                                RaiseEvent [Error]("Failed to create invoice for customer " & c.Name & vbCrLf & qb.LastError, Nothing)
                                Application.DoEvents()
                            Else
                                totalSuccess += 1


                                If c.CreditCardActive Then
                                    Dim editSequence As String = ""
                                    Dim totalPaid As Decimal = qb.GetInvoiceBalanceRemaining(listID, editSequence)
                                    Try
                                        If qb.PayInvoice(c.ListId, Now, PaymentReferenceName, totalPaid, PaymentMethodName, listID) Then
                                            'Already marked as printed/emailed in add invoice request
                                            'totalPaid = qb.GetInvoiceBalanceRemaining(listID, editSequence)
                                            'If Not qb.UpdateInvoice(listID, editSequence, False, True) Then
                                            '    RaiseEvent Error("Failed to update invoice for printing and email. ListID=" & listID)
                                            'End If
                                        Else
                                            Debug.Write("Invoice NOT marked as paid")
                                        End If
                                    Catch ex As Exception
                                        RaiseEvent Error("Failed to mark invoice " & listID & " as printed", Nothing)
                                    End Try

                                End If


                                For i As Integer = 0 To invoices.Length - 1
                                    Application.DoEvents()
                                    Dim result As Boolean = WebServiceHelper.Service.MarkInvoiceUpdatedInQB(invoices(i), listID, CurrentBatchNumber)
                                    If result = False Then
                                        RaiseEvent Error("failed to update invoice for order number  " & invoices(i).ToString, Nothing)
                                    End If
                                    Application.DoEvents()
                                Next
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                RaiseEvent [Error](ex.Message, ex)
            End Try

            Application.DoEvents()
            If _Cancel Then Exit For
        Next


        RaiseEvent DownloadFinished(totalSuccess)

        qb.Disconnect()
        qb = Nothing

    End Sub
    Private Function GetCompanyToBillTo(ByVal c As Customer, callNumber As Integer) As Customer
        If c.BillToID > 0 Then
            RaiseEvent Information($"GetCompanyToBillTo: Customer={c.Name}, Company={c.CompanyName}, ID={c.CustomerID}, BillToID={c.BillToID}. Returning Customer {c.BillToID}")
            Return WebServiceHelper.Service.GetCustomer(c.BillToID)
        End If

        'If c.ParentID < 1 OrElse c.ParentID = 9459 Then
        RaiseEvent Information($"GetCompanyToBillTo: Customer={c.Name}, Company={c.CompanyName}, ID={c.CustomerID}, ParentID={c.ParentID}")
        If c.ParentID < 1 OrElse c.ParentID = 9545 OrElse c.CompanyName = "IRS Tax Records Customer List" Then
            Return c
        End If
        If callNumber > 10 Then Throw New Exception($"Max tries reach to GetCompanyToBillTo: Customer={c.Name}, Company={c.CompanyName}, ID={c.CustomerID}, ParentID={c.ParentID}")
        'Get the parent of the customer and bill to him
        c = WebServiceHelper.Service.GetCustomer(c.ParentID)
        Return GetCompanyToBillTo(c, callNumber + 1)
    End Function
    Public Sub StopDownloading()
        _Cancel = True
    End Sub
End Class
