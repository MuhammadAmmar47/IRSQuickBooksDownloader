Imports IRSTaxRecords.QBHelper
Imports System.Xml

Public Class frmMain

    Private ReadOnly Property InvoiceDateDifferenceInDays() As Integer
        Get
            Dim value As Integer = Val(System.Configuration.ConfigurationManager.AppSettings("InvoiceDateDifferenceInDays"))
            If value < 1 Then value = 30
            Return value
        End Get
    End Property
    Private ReadOnly Property WebserviceURL() As String
        Get
            Dim value As String = System.Configuration.ConfigurationManager.AppSettings("WebserviceURL")
            If value Is Nothing OrElse value.Trim = "" Then
                value = "https://www.irstaxrecords.com/IRSTaxServices.asmx"
            End If
            Return value
        End Get
    End Property

    Dim headerRowStyle As String = "align='center' bgcolor='#C0C0C0'"
    Dim invoices As New Generic.List(Of Invoice)


    Private Sub btnQueryInvoices_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles btnQueryInvoices.LinkClicked
        lblInfo.Text = ""
        Me.ProgressBar1.Value = 0

        If Not Login() Then Return

        If txtRefNo.Text.Trim = "" Then
            MsgBox("Please enter invoice reference number.", MsgBoxStyle.Critical)
            Return
        End If
        invoices.Clear()
        Me.lstInvoices.Items.Clear()
        Me.txtInvoiceDetails.Text = ""
        lblInvoiceExorted.Visible = False
        Dim qb As New QuickBooksProcessor
        Try
            Dim responseXML As String = qb.GetInvoice(Me.txtRefNo.Text.Trim)
            If Me.chkShowXMLOnly.Checked Then
                Me.txtInvoiceDetails.Text = responseXML
                Return
            End If
            Dim xmlDoc As New XmlDocument
            xmlDoc.LoadXml(responseXML)

            Dim nodes As XmlNodeList = xmlDoc.SelectNodes("/QBXML/QBXMLMsgsRs/InvoiceQueryRs/InvoiceRet")



            For Each invoiceNode As XmlNode In nodes
                invoices.Add(Invoice.ParseFromXML(invoiceNode))
                Me.lstInvoices.Items.Add(invoices(invoices.Count - 1))
            Next
        Catch ex As Exception
            MsgBox("Failed to get invoices. " & ex.Message, MsgBoxStyle.Critical)
        Finally
            qb.Disconnect()
        End Try

        If Me.lstInvoices.Items.Count = 1 Then
            lstInvoices.SelectedIndex = 0
            btnExportInvoice.PerformClick()
        End If


    End Sub
    Private Sub ExportToExcel(ByVal invoice As Invoice, ByVal filePath As String)
        _InvoicesList = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("<html>")
        sb.AppendLine("<head><title>Invoice " & invoice.ToString & "</title></head>")
        sb.AppendLine("<body>")

        sb.AppendLine("<table border=""1"" cellpadding=""0"" cellspacing=""0"" cellpadding=""10"">")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td " & headerRowStyle & "><b>Customer ID</b></td>")
        sb.AppendLine("<td " & headerRowStyle & "><b>Customer Name</b></td>")
        sb.AppendLine("<td " & headerRowStyle & "><b>Order Name</b></td>")
        'html += "<td " & headerRowStyle & "><b>Order Number</b></td>"
        sb.AppendLine("<td " & headerRowStyle & "><b>Loan Number</b></td>")
        sb.AppendLine("<td " & headerRowStyle & "><b>Request</b></td>")
        sb.AppendLine("<td " & headerRowStyle & "><b>Tax Years</b></td>")
        'html += "<td " & headerRowStyle & "><b>SSN</b></td>"
        'html += "<td " & headerRowStyle & "><b>Status</b></td>"
        sb.AppendLine("<td " & headerRowStyle & "><b>Order Date</b></td>")
        sb.AppendLine("<td " & headerRowStyle & "><b>Price</b></td>")
        sb.AppendLine("<td " & headerRowStyle & "><b>SSN</b></td>")
        sb.AppendLine("</tr>")

        Dim rateTotal As Decimal = 0
        Dim alreadyIncludedList As New Generic.List(Of InvoiceLine)

        Dim temp As Integer = 0
        Dim totalRecords As Integer = invoice.InvoiceLines.Count

        'We are going to check each pair for duplication. If found any duplicate, that means its next item is duplicate too...
        While True
            Dim invoiceLine As InvoiceLine = invoice.InvoiceLines(temp)
            Try
                AddMissingInformation(invoice.TxnID, invoiceLine, invoice.InvoiceLines(temp + 1))
            Catch ex As Exception

            End Try


            invoiceLine.IsDuplicate = Me.IsDuplicate(invoiceLine, alreadyIncludedList)

            temp += 1

            'If the main item is duplicate, then its next item is duplicate too (the service fee etc)
            If invoiceLine.IsDuplicate AndAlso invoice.InvoiceLines.Count > temp Then
                invoice.InvoiceLines(temp).IsDuplicate = True
            End If

            temp += 1

            Application.DoEvents()
            Me.ProgressBar1.Value = (temp / totalRecords) * 100
            lblInfo.Text = "Processing " & temp & " of " & totalRecords
            Application.DoEvents()

            alreadyIncludedList.Add(invoiceLine)
            If temp >= invoice.InvoiceLines.Count Then Exit While
        End While
        temp = 0
        Me.ProgressBar1.Value = 0
        totalRecords = invoice.InvoiceLines.Count
        For Each invoiceLine As InvoiceLine In invoice.InvoiceLines
            Dim thisStyle As String = ""
            If invoiceLine.IsDuplicate Then thisStyle = " bgcolor='pink'"
            sb.AppendLine("<tr>")
            sb.AppendLine("<td " & headerRowStyle & ">" & invoiceLine.CustomerID & "</td>")
            sb.AppendLine("<td " & thisStyle & ">" & invoiceLine.Other2 & "</td>")
            sb.AppendLine("<td " & thisStyle & ">" & invoiceLine.Other1 & "</td>")
            'html += "<td>" & invoiceLine.OrderNumber & "</td>"
            sb.AppendLine("<td " & thisStyle & ">" & invoiceLine.LoanNumber & "</td>")
            sb.AppendLine("<td " & thisStyle & ">" & invoiceLine.Request & "</td>")
            sb.AppendLine("<td " & thisStyle & ">" & invoiceLine.TaxYears & "</td>")
            'html += "<td>" & invoiceLine.SSN & "</td>"
            'html += "<td>" & invoiceLine.Status & "</td>"
            sb.AppendLine("<td " & thisStyle & ">" & invoiceLine.ServiceDate & "</td>")
            If invoiceLine.IsDuplicate Then
                sb.AppendLine("<td " & thisStyle & ">" & FormatCurrency(0, 2) & "</td>")
            Else
                sb.AppendLine("<td " & thisStyle & ">" & FormatCurrency(invoiceLine.Rate, 2) & "</td>")
            End If
            sb.AppendLine("<td " & thisStyle & ">" & invoiceLine.SSN & "</td>")

            sb.AppendLine("</tr>")

            If Not invoiceLine.IsDuplicate Then rateTotal += invoiceLine.Rate
            temp += 1
            Application.DoEvents()
            Me.ProgressBar1.Value = (temp / totalRecords) * 100
            lblInfo.Text = "Creating Excel entries " & temp & " of " & totalRecords
            Application.DoEvents()
        Next
        'For Each invoiceLine As InvoiceLine In invoice.InvoiceLines
        '    Dim isDuplicate As Boolean = Me.IsDuplicate(invoiceLine, alreadyIncludedList)
        '    Dim thisStyle As String = ""
        '    If isDuplicate Then thisStyle = " bgcolor='pink'"

        '    AddMissingInformation(invoice.TxnID, invoiceLine)

        '    html += "<tr>"
        '    html += "<td " & headerRowStyle & ">" & invoiceLine.CustomerID & "</td>"
        '    html += "<td " & thisStyle & ">" & invoiceLine.Other2 & "</td>"
        '    html += "<td " & thisStyle & ">" & invoiceLine.Other1 & "</td>"
        '    'html += "<td>" & invoiceLine.OrderNumber & "</td>"
        '    html += "<td " & thisStyle & ">" & invoiceLine.LoanNumber & "</td>"
        '    html += "<td " & thisStyle & ">" & invoiceLine.Request & "</td>"
        '    html += "<td " & thisStyle & ">" & invoiceLine.TaxYears & "</td>"
        '    'html += "<td>" & invoiceLine.SSN & "</td>"
        '    'html += "<td>" & invoiceLine.Status & "</td>"
        '    html += "<td " & thisStyle & ">" & invoiceLine.ServiceDate & "</td>"
        '    If isDuplicate Then
        '        html += "<td " & thisStyle & ">" & FormatCurrency(0, 2) & "</td>"
        '    Else
        '        html += "<td " & thisStyle & ">" & FormatCurrency(invoiceLine.Rate, 2) & "</td>"
        '    End If
        '    html += "<td " & thisStyle & ">" & invoiceLine.SSN & "</td>"

        '    html += "</tr>"

        '    alreadyIncludedList.Add(invoiceLine)
        '    If Not isDuplicate Then rateTotal += invoiceLine.Rate
        'Next

        'Finally, add the row for the Total
        sb.AppendLine("<tr>")
        sb.AppendLine("<td colspan=""7"">&nbsp;</td>")
        sb.AppendLine("<td>" & FormatCurrency(rateTotal, 2) & "</td>")
        sb.AppendLine("</tr>")

        sb.AppendLine("</table>")

        sb.AppendLine("</body>")
        sb.AppendLine("</html>")


        System.IO.File.WriteAllText(filePath, sb.ToString)
    End Sub
    Private Function IsDuplicate(ByVal thisLine As InvoiceLine, ByVal alreadyList As Generic.List(Of InvoiceLine)) As Boolean
        If chkIgnoreDuplicateInvoices.Checked Then Return False
        For Each item As InvoiceLine In alreadyList

            If thisLine.Request <> item.Request Then Continue For 'Not same
            If thisLine.TaxYears <> item.TaxYears Then Continue For 'Not same
            If thisLine.ItemRef_FullName <> item.ItemRef_FullName Then Continue For 'Not same
            If chkConsiderDuplicatebySSN.Checked Then
                If thisLine.SSN.Replace("-", "").Replace(" ", "").Trim <> item.SSN.Replace("-", "").Replace(" ", "").Trim Then Continue For 'Not same
            End If


            If thisLine.Other1 <> item.Other1 Then Continue For
            'Seems matched, its a duplicate
            Return True
            'If Me.chkConsiderDuplicatebySSN.Checked Then
            '    If thisLine.Request = item.Request AndAlso thisLine.TaxYears = thisLine.TaxYears AndAlso thisLine.SSN.Replace("-", "").Replace(" ", "").Trim = item.SSN.Replace("-", "").Replace(" ", "").Trim And item.ItemRef_FullName = thisLine.ItemRef_FullName Then
            '        Return True
            '    End If
            'Else
            '    If thisLine.Request = item.Request AndAlso thisLine.TaxYears = thisLine.TaxYears AndAlso thisLine.Other1 = item.Other1 AndAlso item.ItemRef_FullName = thisLine.ItemRef_FullName Then
            '        Return True
            '    End If
            'End If

        Next
        Return False
    End Function
    Private Sub SaveHtmlToExcel(ByVal htmlFilePath As String, ByVal excelFilePath As String)

    End Sub
    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtPassword.Text = System.Configuration.ConfigurationManager.AppSettings("WebServicePassword")
        Me.txtUserName.Text = System.Configuration.ConfigurationManager.AppSettings("WebServiceUserName")


        lblInvoiceExorted.Visible = False
        Me.txtRefNo.Text = ""
        lblInfo.Text = ""
    End Sub


    Private Sub lstInvoices_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstInvoices.SelectedIndexChanged
        Me.txtInvoiceDetails.Text = ""
        If lstInvoices.SelectedItem Is Nothing Then Return

        Dim invoice As Invoice = CType(lstInvoices.SelectedItem, Invoice)
        Me.txtInvoiceDetails.Text = "Customer Name: " & vbTab & invoice.CustomerRef_FullName & vbCrLf
        Me.txtInvoiceDetails.Text += "Sub Total: " & vbTab & invoice.Subtotal & vbCrLf
        Me.txtInvoiceDetails.Text += "Ref. Number: " & vbTab & invoice.RefNumber & vbCrLf
        Me.txtInvoiceDetails.Text += "Time Created: " & vbTab & invoice.TimeCreated & vbCrLf
        Me.txtInvoiceDetails.Text += "Txn ID: " & vbTab & vbTab & invoice.TxnID & vbCrLf
        Me.txtInvoiceDetails.Text += "Txn Number: " & vbTab & invoice.TxnNumber & vbCrLf

        Me.txtInvoiceDetails.Text += vbCrLf
        Me.txtInvoiceDetails.Text += "Item Count: " & vbTab & invoice.InvoiceLines.Count & vbCrLf

        Dim count As Integer = 0
        For Each item As InvoiceLine In invoice.InvoiceLines
            Me.txtInvoiceDetails.Text += "Loan Number: " & vbTab & item.LoanNumber & vbCrLf

            count += 1
            If count > 50 Then Exit For
        Next

        lblInvoiceExorted.Visible = False
    End Sub

    Private Sub btnExportInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportInvoice.Click
        lblInvoiceExorted.Visible = False
        If lstInvoices.SelectedItem Is Nothing Then MsgBox("Please select an invoice from the list.") : Return
        Dim invoice As Invoice = CType(lstInvoices.SelectedItem, Invoice)
        With dlgSave
            .FileName = invoice.CustomerRef_FullName & ".xls"
            .Filter = "Excel Files|*.xls"
        End With
        If dlgSave.ShowDialog = Windows.Forms.DialogResult.Cancel Then Return
        ExportToExcel(invoice, dlgSave.FileName)

        lblInvoiceExorted.Visible = True
    End Sub


    Dim _InvoicesList() As IRSTaxServices.Invoice = Nothing
    Private Sub AddMissingInformation(ByVal ListId As String, ByVal line As InvoiceLine, ByVal nextLine As InvoiceLine)
        If line.SSN <> "" AndAlso line.Request <> "" Then
            'We already have all information, we dont need to get anything from db...
            Return
        End If


        If nextLine.Other1.EndsWith("W2") OrElse line.Other1.EndsWith("W2") Then
            line.Request = "W2"
            nextLine.Request = "W2"
        ElseIf nextLine.Other1.EndsWith("1040") OrElse line.Other1.EndsWith("1040") Then
            line.Request = "1040"
            nextLine.Request = "1040"
        ElseIf nextLine.Other1.EndsWith("1120") OrElse line.Other1.EndsWith("1120") Then
            line.Request = "1120"
            nextLine.Request = "1120"
        ElseIf nextLine.Other1.EndsWith("1065") OrElse line.Other1.EndsWith("1065") Then
            line.Request = "1065"
            nextLine.Request = "1065"
        ElseIf nextLine.Other1.EndsWith("1099") OrElse line.Other1.EndsWith("1099") Then
            line.Request = "1099"
            nextLine.Request = "1099"
        ElseIf nextLine.Other1.EndsWith("SSN") OrElse line.Other1.EndsWith("SSN") Then
            line.Request = "SSN"
            nextLine.Request = "SSN"
        End If

        If line.SSN <> "" AndAlso line.Request <> "" Then
            'We already have all information, we dont need to get anything from db...
            Return
        End If
        Dim clearInvoiceList As Boolean = False
        If _InvoicesList Is Nothing Then
            Try
                Dim service As New IRSTaxServices.IRSTaxServices()
                service.AuthHeaderValue = New IRSTaxServices.AuthHeader
                service.AuthHeaderValue.UserName = txtUserName.Text.Trim
                service.AuthHeaderValue.Password = txtPassword.Text

                service.Url = WebserviceURL
                _InvoicesList = service.GetInvoicesByListID(ListId)
            Catch ex As Exception
                MsgBox("Failed to get invoice details from server. " & ex.Message, MsgBoxStyle.Critical)
                Return
            End Try
        End If
        If _InvoicesList.Length = 0 Then
            'Find using the name...
            Try
                Dim service As New IRSTaxServices.IRSTaxServices()
                service.AuthHeaderValue = New IRSTaxServices.AuthHeader
                service.AuthHeaderValue.UserName = txtUserName.Text.Trim
                service.AuthHeaderValue.Password = txtPassword.Text

                service.Url = WebserviceURL
                _InvoicesList = service.GetInvoicesByCustomerName(line.Other1, line.ServiceDate.AddDays(-InvoiceDateDifferenceInDays), line.ServiceDate.AddDays(InvoiceDateDifferenceInDays))
                clearInvoiceList = True
            Catch ex As Exception
                MsgBox("Failed to get invoice details from server. " & ex.Message, MsgBoxStyle.Critical)
                Return
            End Try

        End If
        If _InvoicesList IsNot Nothing AndAlso _InvoicesList.Length > 0 Then
            For Each invoice As IRSTaxServices.Invoice In _InvoicesList

                If invoice.RequestName.ToUpper.Trim = line.Other1.ToUpper.Trim OrElse invoice.RequestName.ToUpper.Trim = nextLine.Other1.ToUpper.Trim Then
                    'Matched
                    Dim itemToCopyTo As InvoiceLine = line
                    If invoice.RequestName.ToUpper.Trim = nextLine.Other1.ToUpper.Trim Then
                        itemToCopyTo = nextLine
                    End If

                    If itemToCopyTo.SSN.Trim = "" Then itemToCopyTo.SSN = invoice.SSNNo
                    If itemToCopyTo.Request.Trim = "" Then
                        Select Case invoice.typeOfForm
                            Case IRSTaxServices.TypeOfForm.S_1040
                                itemToCopyTo.Request = "1040"
                            Case IRSTaxServices.TypeOfForm.S_1065
                                itemToCopyTo.Request = "1065"
                            Case IRSTaxServices.TypeOfForm.S_1099
                                itemToCopyTo.Request = "1099"
                            Case IRSTaxServices.TypeOfForm.S_1120
                                itemToCopyTo.Request = "1120"
                            Case IRSTaxServices.TypeOfForm.S_SSN
                                itemToCopyTo.Request = "SSN"
                            Case IRSTaxServices.TypeOfForm.S_W2
                                itemToCopyTo.Request = "W2"
                        End Select
                    End If
                End If
                If (line.SSN.Trim <> "" AndAlso line.Request.Trim <> "") Or (nextLine.SSN.Trim <> "" AndAlso nextLine.Request.Trim <> "") Then Exit For
            Next
        End If
        If clearInvoiceList Then
            _InvoicesList = New IRSTaxServices.Invoice() {}
        End If
        'If _dt Is Nothing Then
        '    Dim strQ As String = "Select * FROM tblOrder WHERE ListID = '" & ListId & "' " ' AND fldRequestName LIKE  '%" & RequestName.Replace("'", "''") & "%'"
        '    _dt = DataHelper.ExecuteQuery(strQ)
        'End If

        'Dim rows() As DataRow = _dt.Select("fldRequestName='" & line.Other1.Replace("'", "''") & "'")
        'If rows.Length > 0 Then
        '    If line.SSN.Trim = "" Then line.SSN = rows(0)("fldSSNNo").ToString.Trim
        '    'If line.Request.Trim = "" Then
        '    '    Select Case rows(0)("fldtypeofform").ToString.Trim
        '    '        Case "1" : line.Request = "1040"
        '    '        Case "2" : line.Request = "1120"
        '    '        Case "3" : line.Request = "1065"
        '    '        Case "4" : line.Request = "W2"
        '    '        Case "5" : line.Request = "1099"
        '    '        Case "6" : line.Request = "SSN"
        '    '    End Select
        '    'End If
        'End If

    End Sub

    Private Sub btnTestAuth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestAuth.Click
        If Not Login() Then Return
        MsgBox("Login successful", MsgBoxStyle.Information)
    End Sub
    Private Function Login() As Boolean
        If txtUserName.Text.Trim = "" Then MsgBox("Please enter user name.") : Return False
        If txtPassword.Text.Trim = "" Then MsgBox("Please enter password.") : Return False

        Try
            Dim service As New IRSTaxServices.IRSTaxServices()

            service.AuthHeaderValue = New IRSTaxServices.AuthHeader
            service.AuthHeaderValue.UserName = txtUserName.Text.Trim
            service.AuthHeaderValue.Password = txtPassword.Text

            service.Url = WebserviceURL

            service.Authenticate()

            Return True
        Catch ex1 As Web.Services.Protocols.SoapException
            MsgBox("Failed to authenticate. Please check your username and/or password." & ex1.Message)
        Catch ex As Exception
            MsgBox("Failed to authenticate. Please check your username and/or password.")
        End Try
    End Function
End Class
