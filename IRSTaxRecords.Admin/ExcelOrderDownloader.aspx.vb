Public Partial Class ExcelOrderDownloader
    Inherits System.Web.UI.Page

    Dim headerRowStyle As String = "align='center' bgcolor='#C0C0C0'"

    Private ReadOnly Property UserID() As String
        Get
            If Request.QueryString("UserID") Is Nothing Then Return ""
            Dim value As String = Request.QueryString("UserID")
            If IsNumeric(value) Then Return value
            Return base64_decode(value)
        End Get
    End Property

#Region "Private functions"
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblError.Text = ""
        If Not Page.IsPostBack Then
            Me.txtFrom.Text = Now.AddDays(-7).ToShortDateString
            Me.txtto.Text = Now.ToShortDateString
        End If
    End Sub
    Private Function Validateform() As Boolean
        Dim msg As String = ""
        If Not IsDate(Me.txtFrom.Text) Then lblError.Text += "Please enter valid from date.<br>"
        If Not IsDate(Me.txtto.Text) Then lblError.Text += "Please enter valid to date.<br>"

        If IsDate(txtFrom.Text) AndAlso IsDate(txtto.Text) Then
            If CDate(txtFrom.Text) > CDate(txtto.Text) Then
                msg += "From date can't exceed to date.<br>"
            End If
        End If
        If msg = "" Then Return True
        Me.lblError.Text = msg
        Return False
    End Function
    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If Not Validateform() Then Return

        Dim strQ As String = "SELECT tblOrder.*, tblupdate.fldupdateddate "
        strQ += " FROM         dbo.tblorder LEFT OUTER JOIN dbo.tblupdate ON dbo.tblorder.fldordernumber = dbo.tblupdate.fldordernumber "
        strQ += " INNER JOIN dbo.Customer ON dbo.tblorder.fldCustomerID= dbo.customer.CustomerID"
        strQ += " where fldtypeofform < 6 "
        
        'strQ += " and UserID = '" & UserID & "'"
        strQ += " and Customer.UserID = '" & UserID.Replace("'", "''") & "'"


        strQ += " AND fldOrderdate>='" & CDate(Me.txtFrom.Text).ToShortDateString & " 12:00 AM'"
        strQ += " AND fldOrderdate<='" & CDate(Me.txtto.Text).ToShortDateString & " 11:59 PM'"
        strQ += "  order by tblOrder.fldordernumber desc"

        Dim dt As DataTable = DataHelper.ExecuteQuery(strQ)

        Dim dtExcel As New DataTable
        dtExcel.Columns.Add("Order Date")
        dtExcel.Columns.Add("Order Name")
        dtExcel.Columns.Add("Request")
        dtExcel.Columns.Add("Tax Years")
        dtExcel.Columns.Add("Status")
        dtExcel.Columns.Add("Delivery Date")
        dtExcel.Columns.Add("Loan Number")

        For temp As Integer = 0 To dt.Rows.Count - 1
            Dim orderNo As Integer = dt.Rows(temp)("fldordernumber")
            'Dim updateDate As String = ""
            Dim formType As String = ""
            Dim delivDate As DateTime
            Dim fldorderdate As DateTime = dt.Rows(temp)("fldorderdate")

            'If dt.Rows(temp)("fldupdateddate") IsNot DBNull.Value Then updateDate = dt.Rows(temp)("fldupdateddate")
            Select Case dt.Rows(temp)("fldTypeofForm").ToString.Trim
                Case "1" : formType = "1040"
                Case "2" : formType = "1120"
                Case "3" : formType = "1065"
                Case "4" : formType = "W-2"
                Case "5" : formType = "1099"
            End Select

            If dt.Rows(temp)("flddeliverydate") Is DBNull.Value Then

                Select Case formType
                    Case "1040", "1120", "1065"
                        Select Case fldorderdate.DayOfWeek
                            Case DayOfWeek.Friday
                                dt.Rows(temp)("flddeliverydate") = fldorderdate.AddDays(4)
                            Case DayOfWeek.Saturday
                                dt.Rows(temp)("flddeliverydate") = fldorderdate.AddDays(3)
                            Case DayOfWeek.Sunday
                                dt.Rows(temp)("flddeliverydate") = fldorderdate.AddDays(2)
                            Case Else
                                dt.Rows(temp)("flddeliverydate") = fldorderdate.AddDays(1)
                        End Select
                    Case "W2", "1099", "W-2"
                        Select Case fldorderdate.DayOfWeek
                            Case DayOfWeek.Friday
                                dt.Rows(temp)("flddeliverydate") = fldorderdate.AddDays(5)
                            Case DayOfWeek.Saturday
                                dt.Rows(temp)("flddeliverydate") = fldorderdate.AddDays(4)
                            Case DayOfWeek.Sunday
                                dt.Rows(temp)("flddeliverydate") = fldorderdate.AddDays(3)
                            Case Else
                                dt.Rows(temp)("flddeliverydate") = fldorderdate.AddDays(2)
                        End Select
                End Select
            End If
            If dt.Rows(temp)("flddeliverydate") Is DBNull.Value Then dt.Rows(temp)("flddeliverydate") = fldorderdate.AddDays(1)
            delivDate = dt.Rows(temp)("flddeliverydate")

            Dim taxYears As New Generic.List(Of String)
            For yearCount As Integer = 2001 To 2010
                Dim colName As String = "fldTaxYear" & yearCount.ToString
                If dt.Columns.Contains(colName) AndAlso dt.Rows(temp)(colName) IsNot DBNull.Value AndAlso dt.Rows(temp)(colName) = True Then
                    taxYears.Add(yearCount.ToString)
                End If
            Next

            Dim status As String = ""
            Select Case dt.Rows(temp)("fldstatus").ToString.ToLower.Trim
                Case "p"
                    status = "Pending"
                    If dt.Rows(temp)("fldpdf") Is DBNull.Value OrElse dt.Rows(temp)("fldpdf").ToString.Trim = "" Then
                    Else
                        status = "Updated"
                    End If
                Case "s"
                    status = "Not Matched"
                Case "n"
                    status = "Bad SSN"
                Case "m"
                    status = "Matched"
                Case "e"
                    status = "Expired"
                Case "i"
                    status = "Invalid SSN"
                Case "a"
                    status = "Invalid Address"
                Case "d"
                    status = "Delivered"
                Case "r"
                    status = "No Records"
                Case "u"
                    status = "Updated"
                Case "c"
                    status = "Cancelled"
                Case Else
                    status = dt.Rows(temp)("fldstatus").ToString
            End Select
            Dim requestName As String = dt.Rows(temp)("fldRequestName")

            Dim row As DataRow = dtExcel.NewRow
            row("Order Date") = fldorderdate.ToShortDateString()
            row("Order Name") = requestName
            row("Request") = formType
            row("Tax Years") = TaxYeastoString(taxYears)
            row("Status") = status
            row("Delivery Date") = delivDate.ToString("MM-dd-yyyy")
            If dt.Rows(temp)("fldLoanNumber") Is DBNull.Value Then dt.Rows(temp)("fldLoanNumber") = ""
            row("Loan Number") = dt.Rows(temp)("fldLoanNumber")

            dtExcel.Rows.Add(row)
        Next
        Dim content As String = ExportToExcel(dtExcel)
        Response.Clear()
        Response.ContentType = "application/excel"
        Response.AddHeader("Content-Disposition", "attachment; filename=TaxList.xls")
        Response.Write(content)
        Response.Flush()
        'Response.TransmitFile("file path")
        Response.End()

        'Me.grDMain.DataSource = dtExcel
        'Me.grDMain.DataBind()
    End Sub
    Private Function ExportToExcel(ByVal dt As DataTable) As String
        Dim html As String = "<html>"
        html += "<head><title>Orders</title></head>"
        html += "<body>"

        html += "<table border=""1"" cellpadding=""0"" cellspacing=""0"" cellpadding=""10"">"
        html += "<tr>"
        For Each col As DataColumn In dt.Columns
            html += "<td " & headerRowStyle & "><b>" & col.ColumnName & "</b></td>"
        Next
        html += "</tr>"

        Dim rateTotal As Decimal = 0
        For Each row As DataRow In dt.Rows
            html += "<tr>"
            For Each col As DataColumn In dt.Columns
                html += "<td style=""text-align:left"">" & row(col.ColumnName).ToString & "</td>"
            Next
            html += "</tr>"
        Next
        html += "</table>"

        html += "</body>"
        html += "</html>"


        Return html
    End Function

    Private Function TaxYeastoString(ByVal years As Generic.List(Of String)) As String
        Dim outStr As String = ""
        For Each Year As String In years
            outStr += Year & "-"
        Next
        If outStr.EndsWith("-") Then outStr = Mid(outStr, 1, outStr.Length - 1)
        Return outStr
    End Function
End Class