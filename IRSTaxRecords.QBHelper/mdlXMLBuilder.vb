Imports System.Xml
Imports QBXMLRP2Lib
Imports IRSTaxRecords.QBHelper.QBWebService
'Imports IRSTaxRecords.QBHelper.QBServiceLocal

Public Module mdlXMLBuilder

    Public Const SERVICE_CODE_NAME As String = "RO-"
    Public Const SERVICE_DESC As String = "Tax Services Provided"
    Public Const YEARS_CODE_NAME As String = "DEPT-TREASURY"

    Public Const LOAN_NUMBER_FIELD As String = "LoanNumber"
    Public Const ORDERNUMBER_CUSTOMERID_FIELD As String = "OrderNo_CustomerID"
    Public Const TAX_YEARS_FIELD As String = "TaxYears"
    Public Const SSN_REQTYPE_ORDERSTATUS_FIELD As String = "SSN_ReqType_Status"


    'Public Const PER_YEAR_CHARGE As Decimal = 4.5

    Private Function MakeSimpleElem(ByVal doc As XmlDocument, ByVal tagName As String, ByVal tagVal As String) As XmlElement

        Dim elem As XmlElement
        elem = doc.CreateElement(tagName)
        elem.InnerText = tagVal
        Return elem

    End Function

    Public Function HtmlEncode(ByVal text As String) As String
        Return System.Web.HttpUtility.HtmlEncode(text)
    End Function
    Private Function EncloseQueryInHeader(ByVal query As String) As String
        Dim header As String = "<?xml version=""1.0"" encoding=""utf-8"" ?>" & vbCrLf
        header += "<?qbxml version=""6.0""?>" & vbCrLf
        header += "<QBXML>" & vbCrLf
        header += "<QBXMLMsgsRq onError = ""stopOnError"">" & vbCrLf
        header += query
        header += "</QBXMLMsgsRq>" & vbCrLf
        header += "</QBXML>"

        Return header
    End Function
    Public Function BuildQuery_CustomersList() As String
        Dim query As String = "<CustomerQueryRq requestID=""1""/>"
        Return EncloseQueryInHeader(query)
    End Function
    Public Function BuildQuery_GetCustomer(ByVal Name As String) As String
        Dim query As String = "<CustomerQueryRq requestID=""1"">"
        query += "<FullName>" & Name.EncodeWithCDATA & "</FullName>"
        query += "</CustomerQueryRq>"
        Return EncloseQueryInHeader(query)
    End Function
    Public Function BuildQuery_GetCustomerFromListID(ByVal ListID As String) As String
        Dim query As String = "<CustomerQueryRq requestID=""1"">"
        query += "<ListID>" & ListID & "</ListID>"
        query += "</CustomerQueryRq>"
        Return EncloseQueryInHeader(query)
    End Function
    Public Function BuildQuery_GetInvoices(ByVal CustomerListId As String) As String
        'Dim query As String = "<InvoiceQueryRq requestID=""1""><EntityFilter><FullNameWithChildren>" & customerName & "</FullNameWithChildren></EntityFilter><PaidStatus>NotPaidOnly</PaidStatus></InvoiceQueryRq>"
        Dim query As String = "<InvoiceQueryRq requestID=""1""><EntityFilter><ListID>" & CustomerListId & "</ListID></EntityFilter><PaidStatus>NotPaidOnly</PaidStatus></InvoiceQueryRq>"
        'query += "<CreditMemoQueryRq requestID=""2""><EntityFilter><FullNameWithChildren>" & customerName & "</FullNameWithChildren></EntityFilter><IncludeLinkedTxns>true</IncludeLinkedTxns></CreditMemoQueryRq>"

        Return EncloseQueryInHeader(query)
    End Function
    Public Function BuildQuery_GetInvoice(ByVal InvoiceNumber As String) As String

        Dim query As String = "<InvoiceQueryRq requestID=""1"">"
        'query += "<EntityFilter><ListID>" & CustomerListId & "</ListID></EntityFilter>"
        'query += "<PaidStatus>NotPaidOnly</PaidStatus>"
        query += "<RefNumber>" & InvoiceNumber & "</RefNumber>"
        query += "<IncludeLineItems>true</IncludeLineItems>"
        'query += "<DataExt>"
        query += "<OwnerID>0</OwnerID>"
        'query += "<DataExtName>" & LOAN_NUMBER_FIELD & "</DataExtName>"
        'query += "</DataExt>"
        query += "</InvoiceQueryRq>"

        Return EncloseQueryInHeader(query)
    End Function
    Public Function BuildQuery_InvoicePayment(ByVal customerListId As String, ByVal paidOn As DateTime, ByVal refNo As String, ByVal amount As Decimal, _
                                              ByVal paymentMethod As String, ByVal invoiceId As String) As String

        Dim query As String = "<ReceivePaymentAddRq requestID=""1"">"
        query += "<ReceivePaymentAdd>"
        query += "<CustomerRef>"
        query += "<ListID>" & customerListId & "</ListID>"
        query += "</CustomerRef>"

        Dim paid As String = paidOn.Year & "-" & Format(paidOn.Month, "0#").ToString & "-" & Format(paidOn.Day, "0#").ToString

        query += "<TxnDate>" & paid & "</TxnDate>"

        query += "<RefNumber>" & refNo & "</RefNumber>"

        query += "<TotalAmount>" & amount & "</TotalAmount>"

        query += "<PaymentMethodRef><FullName>" & paymentMethod & "</FullName></PaymentMethodRef>"
        query += "<AppliedToTxnAdd>"
        query += "<TxnID>" & invoiceId & "</TxnID>"
        query += "<PaymentAmount>" & amount & "</PaymentAmount>"
        query += "</AppliedToTxnAdd>"

        query += "</ReceivePaymentAdd>"
        query += "</ReceivePaymentAddRq>"
        Return EncloseQueryInHeader(query)
    End Function


    Public Function BuildCustomerAddRq(ByVal c As Customer) As String

        Dim query As String = "<CustomerAddRq requestID=""1"">"
        query += "<CustomerAdd>"
        query += "<Name>" & c.CompanyName.EncodeWithCDATA & "</Name>"
        query += "<CompanyName>" & c.CompanyName.EncodeWithCDATA & "</CompanyName>"

        query += "<BillAddress>"
        query += "<Addr1>" & c.CompanyName.EncodeWithCDATA & "</Addr1>"
        query += "<Addr2>" & c.Address.EncodeWithCDATA & "</Addr2>"
        query += "<Addr3>" & c.Address1.EncodeWithCDATA & "</Addr3>"
        query += "<City>" & c.City.EncodeWithCDATA & "</City>"
        query += "<State>" & c.State.EncodeWithCDATA & "</State>"
        query += "<PostalCode>" & c.Zip.EncodeWithCDATA & "</PostalCode>"
        query += "<Country>USA</Country>"
        query += "</BillAddress>"


        If c.Telephone.Trim <> "" AndAlso c.Telephone.Length < 10 Then query += "<Phone>" & c.Telephone & "</Phone>"
        'If c.FaxNumber.Trim <> "" Then query += "<Fax>" & c.FaxNumber & "</Fax>"
        If c.Email.Trim <> "" Then query += "<Email>" & c.Email & "</Email>"
        If c.Name.Trim <> "" Then
            query += "<Contact>" & c.Name.EncodeWithCDATA & "</Contact>"
        End If


        'query += "<Phone>" & c.Telephone & "</Phone>"
        'query += "<Phone>" & c.Telephone & "</Phone>"


        query += "</CustomerAdd>"
        query += "</CustomerAddRq>"
        Return EncloseQueryInHeader(query)

    End Function

    Public Function BuildInvoiceServiceAddRq(ByVal Name As String, ByVal Desc As String, ByVal Price As Decimal, ByVal AccountName As String) As String

        Dim query As String = "<ItemServiceAddRq requestID=""1"">"
        query += "<ItemServiceAdd>"
        query += "<Name>" & Name & "</Name>"

        query += "<SalesOrPurchase>"
        query += "<Desc>" & Desc & "</Desc>"
        query += "<Price>" & Price & "</Price>"
        query += "<AccountRef><FullName>" & AccountName & "</FullName></AccountRef>"
        query += "</SalesOrPurchase>"

        query += "</ItemServiceAdd>"
        query += "</ItemServiceAddRq>"
        Return EncloseQueryInHeader(query)

    End Function
    

    
    Public Function BuildInvoiceServiceQuery(ByVal Name As String) As String

        Dim query As String = "<ItemServiceQueryRq requestID=""1"">"
        query += "<FullName>" & Name & "</FullName>"

        query += "</ItemServiceQueryRq>"
        Return EncloseQueryInHeader(query)

    End Function
    Public Function BuildInvoiceUpdateRequest(ByVal ListID As String, ByVal EditSequence As String, ByVal IsToPrinted As Boolean, ByVal isToEmail As Boolean) As String
        Dim query As String = "<InvoiceModRq requestID=""1"">"
        query += "<InvoiceMod>"
        query += "<TxnID>" & ListID & "</TxnID>"
        query += "<EditSequence>" & EditSequence & "</EditSequence>"

        If IsToPrinted Then
            query += "<IsToBePrinted>1</IsToBePrinted>"
        Else
            query += "<IsToBePrinted>0</IsToBePrinted>"
        End If

        If isToEmail Then
            query += "<IsToBeEmailed>1</IsToBeEmailed>"
        Else
            query += "<IsToBeEmailed>0</IsToBeEmailed>"
        End If
        
        query += "</InvoiceMod>"
        query += "</InvoiceModRq>"

        Return EncloseQueryInHeader(query)
    End Function
    Public Function BuildGetInvoiceRequest(ByVal ListID As String) As String
        Dim query As String = "<InvoiceQueryRq requestID=""1"">"
        query += "<TxnID>" & ListID & "</TxnID>"
        query += "</InvoiceQueryRq>"

        Return EncloseQueryInHeader(query)
    End Function
    Public Function BuildInvoiceRequest(ByVal invNo As String, ByVal c As Customer, ByVal invoices() As Integer, ByVal toBePrinted As Boolean, ByVal tobeEmailed As Boolean, Optional ByVal templateName As String = "IRSTAXRECORDS") As String
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("<InvoiceAddRq requestID=""1"">")
        sb.AppendLine("<InvoiceAdd>")
        sb.AppendLine("<CustomerRef><ListID>" & c.ListId & "</ListID>")
        'query += "<FullName>" & HtmlEncode(c.CompanyName) & "</FullName>"
        sb.AppendLine("</CustomerRef>")

        sb.AppendLine("<RefNumber>" & invNo & "</RefNumber>")
        If toBePrinted = False Then sb.AppendLine("<IsToBePrinted>0</IsToBePrinted>")
        If tobeEmailed = True Then sb.AppendLine("<IsToBeEmailed>1</IsToBeEmailed>")



        Dim arrList As New ArrayList

        For temp As Integer = 0 To invoices.Length - 1
            Dim inv As Invoice = Nothing
            Try
                inv = WebServiceHelper.Service.GetInvoice(invoices(temp))
            Catch ex As Exception
                Trace.WriteLine($"Failed to get invoice with id {invoices(temp)}. {ex.Message}{vbCrLf}{ex.StackTrace}")
                Throw New Exception($"Failed to get invoice with id {invoices(temp)}. {ex.Message}{vbCrLf}{ex.StackTrace}")
            End Try

            sb.AppendLine(GetInvoiceLine(c, inv, True))
            Dim chargedSecond As Boolean = False
            If inv.typeOfForm <> TypeOfForm.S_SSN Then
                If c.ChargeSecondTaxPayer AndAlso inv.TaxPayer2.Trim <> "" Then
                    sb.AppendLine(GetInvoiceLine(c, inv, False))
                    chargedSecond = True
                End If
                Try
                    sb.AppendLine(GetSecondLineItem(inv, chargedSecond, c.IRSFee))
                Catch ex As Exception
                    Trace.WriteLine("Failed to add second line for Order# " & inv.OrderNumber & ", Customer=" & c.CustomerID & ", " & ex.Message)
                    Throw New Exception("Failed to add second line for Order# " & inv.OrderNumber & ", Customer=" & c.CustomerID & ", " & ex.Message)
                End Try

            End If
        Next



        sb.AppendLine("</InvoiceAdd>")
        sb.AppendLine("</InvoiceAddRq>")
        Return EncloseQueryInHeader(sb.ToString)
    End Function
    Private Function GetInvoiceLine(ByVal c As Customer, ByVal inv As Invoice, ByVal isFirst As Boolean) As String
        Dim query As String = ""
        query += "<InvoiceLineAdd>"
        query += "<ItemRef><FullName>" & SERVICE_CODE_NAME & "</FullName></ItemRef>"
        Select Case inv.typeOfForm
            Case TypeOfForm.S_1040
                query += "<Desc>1040 Request</Desc>"
            Case TypeOfForm.S_1065
                query += "<Desc>1065 Request</Desc>"
            Case TypeOfForm.S_1099
                query += "<Desc>1099 Request</Desc>"
            Case TypeOfForm.S_1120
                query += "<Desc>1120 Request</Desc>"
            Case TypeOfForm.S_SSN
                query += "<Desc>SSN Request</Desc>"
            Case TypeOfForm.S_W2
                query += "<Desc>W2 Request</Desc>"
            Case Else
                query += "<Desc>Unknown Request</Desc>"
        End Select

        If inv.typeOfForm = TypeOfForm.S_SSN Then
            query += "<Rate>" & c.SSN_Fee & "</Rate>"
        Else
            query += "<Rate>" & c.rushRate & "</Rate>"
        End If
    
        'If inv.DeliveryDate > New DateTime(2000, 1, 1) Then
        '    query += "<ServiceDate>" & Format(inv.DeliveryDate, "yyyy-MM-dd") & "</ServiceDate>"
        'Else
        '    query += "<ServiceDate>" & Format(DateTime.Now, "yyyy-MM-dd") & "</ServiceDate>"
        'End If
        If inv.Orderdate > New DateTime(2000, 1, 1) Then
            query += "<ServiceDate>" & Format(inv.Orderdate, "yyyy-MM-dd") & "</ServiceDate>"
        Else
            query += "<ServiceDate>" & Format(DateTime.Now, "yyyy-MM-dd") & "</ServiceDate>"
        End If

        If isFirst Then
            query += "<Other1>" & inv.TaxPayer1.Trim.LeftXCharacters(28).EncodeWithCDATA & "</Other1>"
        Else
            query += "<Other1>" & inv.TaxPayer2.Trim.LeftXCharacters(28).EncodeWithCDATA & "</Other1>"
        End If

        query += "<Other2>" & inv.Processor.Trim.LeftXCharacters(15).EncodeWithCDATA & "</Other2>"


        query += GetCustomDataXML(inv)

        query += "</InvoiceLineAdd>"
        Return query
    End Function
    Private Function GetSecondLineItem(ByVal inv As Invoice, ByVal chargedSecond As Boolean, ByVal IRSFee As Decimal) As String

        If IRSFee <= 0 Then
            IRSFee = 99.99        'Default Value
        End If

        Dim totalYears As Integer = 0
        If inv.TaxYear2000 Then totalYears += 1
        If inv.TaxYear2001 Then totalYears += 1
        If inv.TaxYear2002 Then totalYears += 1
        If inv.TaxYear2003 Then totalYears += 1
        If inv.Taxyear2004 Then totalYears += 1

        If inv.Taxyear2005 Then totalYears += 1
        If inv.Taxyear2006 Then totalYears += 1
        If inv.Taxyear2007 Then totalYears += 1
        If inv.Taxyear2008 Then totalYears += 1
        If inv.Taxyear2009 Then totalYears += 1
        If inv.Taxyear2010 Then totalYears += 1
        If inv.Taxyear2011 Then totalYears += 1
        If inv.Taxyear2012 Then totalYears += 1
        If inv.Taxyear2013 Then totalYears += 1
        If inv.Taxyear2014 Then totalYears += 1
        If inv.Taxyear2015 Then totalYears += 1
        If inv.Taxyear2016 Then totalYears += 1
        If inv.Taxyear2017 Then totalYears += 1
        If inv.Taxyear2018 Then totalYears += 1
        If inv.Taxyear2019 Then totalYears += 1
        If inv.Taxyear2020 Then totalYears += 1
        If inv.Taxyear2021 Then totalYears += 1
        If inv.Taxyear2022 Then totalYears += 1
        If inv.Taxyear2023 Then totalYears += 1
        If inv.Taxyear2024 Then totalYears += 1
        If inv.Taxyear2025 Then totalYears += 1

        Dim query As String = ""


        query += "<InvoiceLineAdd>"
        If totalYears > 0 Then
            'Debug.Write(totalYears)
        Else
            Throw New Exception("Zero years found for Order# " & inv.OrderNumber)
        End If

        'Dim PER_YEAR_CHARGE As Decimal = 0

        query += "<ItemRef><FullName>" & YEARS_CODE_NAME & "</FullName></ItemRef>"
        query += "<Desc>Dept of Treasury Fee " & totalYears & " Years @" & FormatCurrency(IRSFee, 2).ToString & "</Desc>"
        query += "<Rate>" & IRSFee * totalYears & "</Rate>"
        'If inv.DeliveryDate > New DateTime(2000, 1, 1) Then
        '    query += "<ServiceDate>" & Format(inv.DeliveryDate, "yyyy-MM-dd") & "</ServiceDate>"
        'Else
        '    query += "<ServiceDate>" & Format(DateTime.Now, "yyyy-MM-dd") & "</ServiceDate>"
        'End If
        If inv.Orderdate > New DateTime(2000, 1, 1) Then
            query += "<ServiceDate>" & Format(inv.Orderdate, "yyyy-MM-dd") & "</ServiceDate>"
        Else
            query += "<ServiceDate>" & Format(DateTime.Now, "yyyy-MM-dd") & "</ServiceDate>"
        End If

        'Show Full name, if typeOfRequest= 1120, 1065
        'Show Last Name if typeofRequest=  1040, 1099, W2
        'If chargedSecond Then
        Select Case inv.typeOfForm
            Case TypeOfForm.S_1120, TypeOfForm.S_1065
                query += "<Other1>" & inv.TaxPayer1.Trim.LeftXCharacters(28).EncodeWithCDATA & "</Other1>"
            Case Else
                Dim names() As String = Split(inv.TaxPayer1.Trim, " ")
                If names.Length > 1 Then
                    'Add the type of form at the end of first name
                    query += "<Other1>" & (names(names.Length - 1).Trim.LeftXCharacters(28) & " " & TypeOfFormToString(inv.typeOfForm)).EncodeWithCDATA & "</Other1>"
                Else
                    'User might have only one name
                    query += "<Other1>" & (inv.TaxPayer1.Trim.LeftXCharacters(28) & " " & TypeOfFormToString(inv.typeOfForm)).EncodeWithCDATA & "</Other1>"
                End If
        End Select
        'Else
        'query += "<Other1>" & Left(HtmlEncode(inv.TaxPayer1.Trim), 28) & "</Other1>"
        'End If

        query += "<Other2>" & Left(inv.Processor.Trim, 28) & "</Other2>"

        query += GetCustomDataXML(inv)

        query += "</InvoiceLineAdd>"

        Return query
    End Function
    Private Function GetCustomDataXML(ByVal inv As Invoice) As String
        Dim query As String = ""

        query += "<DataExt>"
        query += "<OwnerID>0</OwnerID>"
        query += "<DataExtName>" & LOAN_NUMBER_FIELD & "</DataExtName>"
        query += "<DataExtValue>" & Left(HtmlEncode(inv.fldLoanNumber), 250) & "</DataExtValue>"
        query += "</DataExt>"

        query += "<DataExt>"
        query += "<OwnerID>0</OwnerID>"
        query += "<DataExtName>" & ORDERNUMBER_CUSTOMERID_FIELD & "</DataExtName>"
        query += "<DataExtValue>" & inv.OrderNumber & "," & inv.CustomeriD & "</DataExtValue>"
        query += "</DataExt>"

        query += "<DataExt>"
        query += "<OwnerID>0</OwnerID>"
        query += "<DataExtName>" & SSN_REQTYPE_ORDERSTATUS_FIELD & "</DataExtName>"
        Dim value As String = inv.SSNNo & "," & TypeOfFormToString(inv.typeOfForm) & "," & GetStatusString(inv.Status)

        query += "<DataExtValue>" & value & "</DataExtValue>"
        query += "</DataExt>"


        query += "<DataExt>"
        query += "<OwnerID>0</OwnerID>"
        query += "<DataExtName>" & TAX_YEARS_FIELD & "</DataExtName>"
        query += "<DataExtValue>" & GetYearsAsString(inv) & "</DataExtValue>"
        query += "</DataExt>"


        Return query
    End Function
    Private Function GetStatusString(ByVal statusCode As String) As String
        Select Case statusCode.Trim.ToLower
            Case "a" : Return "Completed"
            Case "c" : Return "Cancelled"
            Case "d" : Return "Completed"
            Case "e" : Return "Completed"
            Case "i" : Return "Completed"
            Case "m" : Return "Completed"
            Case "n" : Return "Completed"
            Case "p" : Return "Pending"
            Case "r" : Return "Completed"
            Case "s" : Return "Completed"
            Case "u" : Return "Updated"
            Case Else
                Return statusCode
        End Select
    End Function
    Private Function GetYearsAsString(ByVal inv As Invoice) As String
        Dim outStr As String = ""
        If inv.TaxYear2000 Then outStr += "2000-"
        If inv.TaxYear2001 Then outStr += "2001-"
        If inv.TaxYear2002 Then outStr += "2002-"
        If inv.TaxYear2003 Then outStr += "2003-"
        If inv.Taxyear2004 Then outStr += "2004-"
        If inv.Taxyear2005 Then outStr += "2005-"

        If inv.Taxyear2006 Then outStr += "2006-"
        If inv.Taxyear2007 Then outStr += "2007-"
        If inv.Taxyear2008 Then outStr += "2008-"
        If inv.Taxyear2009 Then outStr += "2009-"
        If inv.Taxyear2010 Then outStr += "2010-"
        If inv.Taxyear2011 Then outStr += "2011-"
        If inv.Taxyear2012 Then outStr += "2012-"
        If inv.Taxyear2013 Then outStr += "2013-"
        If inv.Taxyear2014 Then outStr += "2014-"
        If inv.Taxyear2015 Then outStr += "2015-"
        If inv.Taxyear2016 Then outStr += "2016-"
        If inv.Taxyear2017 Then outStr += "2017-"
        If inv.Taxyear2018 Then outStr += "2018-"
        If inv.Taxyear2019 Then outStr += "2019-"
        If inv.Taxyear2020 Then outStr += "2020-"
        If inv.Taxyear2021 Then outStr += "2021-"
        If inv.Taxyear2022 Then outStr += "2022-"
        If inv.Taxyear2023 Then outStr += "2023-"
        If inv.Taxyear2024 Then outStr += "2024-"
        If inv.Taxyear2025 Then outStr += "2025-"

        If outStr.EndsWith("-") Then outStr = Mid(outStr, 1, outStr.Length - 1)

        Return outStr
    End Function
    Private Function TypeOfFormToString(ByVal t As TypeOfForm) As String
        Select Case t
            Case TypeOfForm.S_1040 : Return "1040"
            Case TypeOfForm.S_1065 : Return "1065"
            Case TypeOfForm.S_1099 : Return "1099"
            Case TypeOfForm.S_1120 : Return "1120"
            Case TypeOfForm.S_SSN : Return "SSN"
            Case TypeOfForm.S_W2 : Return "W2"
            Case TypeOfForm.S_None : Return "None"
            Case Else
                Return "Unknown"
        End Select
    End Function


#Region "Data Ext Functions"
    Public Function BuildDataExtDefGetRequest(ByVal Name As String) As String
        Dim query As String = "<DataExtDefQueryRq requestID=""1"">"
        query += "<OwnerID>0</OwnerID>"
        'query += "<IncludeRetElement>" & Name & "</IncludeRetElement>"
        query += "</DataExtDefQueryRq>"
        Return EncloseQueryInHeader(query)
    End Function
    Public Function BuildDataExtDefAddRq(ByVal Name As String) As String
        Dim query As String = "<DataExtDefAddRq requestID=""1"">"
        query += "<DataExtDefAdd>"
        query += "<OwnerID>0</OwnerID>"
        query += "<DataExtName>" & Name & "</DataExtName>"
        query += "<DataExtType>STR255TYPE</DataExtType>"
        query += " <AssignToObject>Item</AssignToObject>"
        query += "</DataExtDefAdd>"
        query += "</DataExtDefAddRq>"
        Return EncloseQueryInHeader(query)
    End Function
#End Region
End Module
