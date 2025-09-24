Public Partial Class Statistics
    Inherits System.Web.UI.Page


    Public Property FromDate() As DateTime
        Get
            If Me.dtFromDate.SelectedDate.HasValue Then Return Me.dtFromDate.SelectedDate
            Return DateTime.MinValue
        End Get
        Set(ByVal value As DateTime)
            If value.Equals(DateTime.MinValue) Then Return
            dtFromDate.SelectedDate = value
        End Set
    End Property
    Public Property ToDate() As DateTime
        Get
            If Me.dtToDate.SelectedDate.HasValue Then Return Me.dtToDate.SelectedDate
            Return DateTime.MinValue
        End Get
        Set(ByVal value As DateTime)
            If value.Equals(DateTime.MinValue) Then Return
            dtToDate.SelectedDate = value
        End Set
    End Property

    Public ClientName As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Globals.CALoginClientID < 1 Then Response.Redirect("Login.aspx") : Return
        Dim c As Core.Contacts.Client = ContactServices.Clients.GetClientByID(Globals.CALoginClientID)
        If c Is Nothing Then
            msg.ShowError("Client with id " & Globals.CALoginClientID & " was not found")
            Return
        End If
        ClientName = c.ClientName


        If Not Page.IsPostBack Then

            Me.dtToDate.SelectedDate = Now.ToShortDateString
            Me.dtFromDate.SelectedDate = Now.AddMonths(-1).ToShortDateString

            If Request.QueryString("FromDate") IsNot Nothing AndAlso IsDate(Request.QueryString("FromDate")) Then FromDate = Request.QueryString("FromDate")
            If Request.QueryString("ToDate") IsNot Nothing AndAlso IsDate(Request.QueryString("ToDate")) Then ToDate = Request.QueryString("ToDate")

            ShowStatistics()
        End If
    End Sub

    Private Sub btnShowResults_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShowResults.Click
        If Me.dtFromDate.SelectedDate.HasValue = False OrElse Me.dtToDate.SelectedDate.HasValue = False Then
            msg.ShowError("Please select start and end dates")
            Return
        End If

        Dim url As String = "Statistics.aspx?ToDate=" & ToDate.ToShortDateString & "&FromDate=" & FromDate.ToShortDateString
        Response.Redirect(url)
    End Sub

    Private Sub ShowStatistics()
        If FromDate.Equals(DateTime.MinValue) OrElse ToDate.Equals(DateTime.MinValue) Then
            Me.pnlSearchREsults.Visible = False
            Return
        End If

        Dim strQ As String = "SELECT     Count(dbo.tblorder.fldordernumber)"
        strQ += " FROM         dbo.ClientUser "
        strQ += " INNER JOIN dbo.tblorder ON dbo.ClientUser.UserID = dbo.tblorder.fldcustomeriD"
        strQ += " WHERE ClientUser.ClientID = " & Globals.CALoginClientID
        lblTotalOrders.Text = DataHelper.ExecuteAndReadFirstValueTrimNull(strQ)

        strQ = "SELECT   dbo.tblorder.fldOrderdate, dbo.tblorder.flddeliverydate"
        strQ += " FROM         dbo.ClientUser "
        strQ += " INNER JOIN dbo.tblorder ON dbo.ClientUser.UserID = dbo.tblorder.fldcustomeriD"
        strQ += " WHERE ClientUser.ClientID = " & Globals.CALoginClientID
        strQ += " AND fldOrderDate IS NOT NULL"
        strQ += " AND  fldDeliveryDate IS NOT NULL"
        strQ += " AND DATEDIFF(hour, dbo.tblorder.fldOrderdate, dbo.tblorder.flddeliverydate) > 0"
        Dim dt As DataTable = DataHelper.ExecuteQuery(strQ)
        dt.Columns.Add("ResponseTime", GetType(System.Int32))
        For Each row As DataRow In dt.Rows
            row("ResponseTime") = 0
            If row("fldOrderdate") Is DBNull.Value Then Continue For
            If row("flddeliverydate") Is DBNull.Value Then Continue For

            Dim fromDate As DateTime = row("fldOrderdate")
            Dim toDate As DateTime = row("flddeliverydate")

            row("ResponseTime") = Globals.GetTimeDifferenceInHours(fromDate, toDate)

        Next

        lblAvgTurnTimeForAllOrders.Text = Compute(dt, "Avg(ResponseTime)", Nothing)
        lblFastestTurnTime.Text = Compute(dt, "Min(ResponseTime)", Nothing)
    End Sub
    Private Function Compute(ByVal dt As DataTable, ByVal expression As String, ByVal filter As String) As Integer
        Dim value As Object = dt.Compute(expression, filter)
        If value Is DBNull.Value Then Return 0
        Return value
    End Function
    Private _dtAll As DataTable = Nothing

    Private Sub grdMain_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdMain.ItemDataBound
        Try
            Dim lblNumberOfOrders As Label = CType(e.Item.FindControl("lblNumberOfOrders"), Label)
            Dim lblPercentOrdered As Label = CType(e.Item.FindControl("lblPercentOrdered"), Label)
            Dim lblNumberOfRejcts As Label = CType(e.Item.FindControl("lblNumberOfRejcts"), Label)

            Dim dr As DataRowView = CType(e.Item.DataItem, DataRowView)

            Dim CustomerId As Integer = dr("CustomerID")

            Dim totalOrders As Integer = _dtAll.Select("CustomerID=" & CustomerId).Length
            Dim totalRejects As Integer = _dtAll.Select("CustomerID=" & CustomerId.ToString & " AND fldStatus = 'r'").Length

            lblTotalOrders.Text = totalOrders
            lblPercentOrdered.Text = Math.Round(((totalOrders / _dtAll.Rows.Count) * 100), 2).ToString & "%"

            Dim rejectPercent As Decimal = Math.Round(((totalRejects / _dtAll.Rows.Count) * 100), 2)
            lblNumberOfRejcts.Text = totalRejects & " = " & rejectPercent & " % reject rate"

            lblNumberOfOrders.Text = totalOrders

        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdMain_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
        If _dtAll Is Nothing Then
            Dim strQ As String = "SELECT     dbo.tblorder.fldordernumber, dbo.tblorder.fldOrderdate, dbo.tblorder.flddeliverydate, fldStatus, RejectCode"
            strQ += " , dbo.Customer.Name as ProcessorName, Customer.CustomerID"
            strQ += " FROM dbo.ClientUser "
            strQ += " INNER JOIN dbo.tblorder ON dbo.ClientUser.UserID = dbo.tblorder.fldcustomeriD "
            strQ += " INNER JOIN dbo.Customer ON dbo.ClientUser.UserID = dbo.Customer.CustomerID"
            strQ += " WHERE ClientUser.ClientID=" & Globals.CALoginClientID
            strQ += " AND fldOrderdate>='" & FromDate.ToShortDateString & "'"
            strQ += " AND fldOrderdate<='" & ToDate.ToShortDateString & " 11:59:59 PM'"
            _dtAll = DataHelper.ExecuteQuery(strQ)
        End If

        Me.grdMain.DataSource = _dtAll.DefaultView.ToTable(True, "ProcessorName", "CustomerID")


        Dim totalRejects As Integer = _dtAll.Select("fldStatus='r'").Length

        lblTotalNumberOfRejcts.Text = totalRejects
        lblRejectReason_InvalidAddress.Text = GetRejectStats(RejectCodeType.Address, totalRejects)
        lblRejectReason_SSN.Text = GetRejectStats(RejectCodeType.Social_Security_Number, totalRejects)
        lblRejectReason_NameNotMatch.Text = GetRejectStats(RejectCodeType.Name_does_not_match_record, totalRejects)
        lblRejectReason_UnProcessable.Text = GetRejectStats(RejectCodeType.Unprocessable, totalRejects)
        lblRejectReason_Illegible.Text = GetRejectStats(RejectCodeType.Illegible, totalRejects)
        lblRejectReason_Altered.Text = GetRejectStats(RejectCodeType.Altered, totalRejects)
        lblRejectReason_InvalidDate.Text = GetRejectStats(RejectCodeType.Old_Signature_date, totalRejects)

    End Sub

    Private Function GetRejectStats(ByVal code As RejectCodeType, ByVal totalRejects As Integer) As String
        Dim total As Integer = _dtAll.Select("RejectCode=" & CInt(code)).Length

        If totalRejects = 0 Then
            Return total & " (0%)"
        Else
            Dim percent As Decimal = Math.Round(total / totalRejects * 100, 2)
            Return total & "(" & percent & " %)"
        End If
    End Function
    Public Enum RejectCodeType
        None = 0
        Address = 9
        Unprocessable = 10
        Social_Security_Number = 3
        Illegible = 1
        Name_does_not_match_record = 11
        Old_Signature_date = 4

        Altered = 2
        Invalid_signature = 5
        Incomplete_form = 6
        Invalid_form_request = 12
        Missing_line_5 = 13
        Did_not_file_jointly = 14
        Invalid_product_request = 15

        MISSING_4506 = 90
        Name_differs_on_coversheet = 91
        Years_differ_on_4506T = 92

        T4506_Request_spouse = 93
        Duplicate_Name_on_coversheet = 94
        Batch_Processed_within_last_3_days = 95

        Request_on_4506_differ = 96
        Page_cutoff_during_transmission = 97
        Reference_number_missing = 98
    End Enum
End Class