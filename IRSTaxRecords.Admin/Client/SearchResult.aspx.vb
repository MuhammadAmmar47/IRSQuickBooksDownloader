Partial Public Class SearchResult
    Inherits System.Web.UI.Page

    Private Property Keywords() As String
        Get
            Return Me.txtFindRecords.Text.Trim
        End Get
        Set(ByVal value As String)
            Me.txtFindRecords.Text = value.Trim
        End Set
    End Property
    Private Property RecordStatus() As String
        Get
            Return Me.ddlRecordStatus.Text.Trim
        End Get
        Set(ByVal value As String)
            SelectDropDownItemByValue(ddlRecordStatus, value)
        End Set
    End Property
    Private Property FromDate() As DateTime
        Get
            If Me.dtFromDate.SelectedDate.HasValue Then Return dtFromDate.SelectedDate.Value
            Return DateTime.MinValue
        End Get
        Set(ByVal value As DateTime)
            If value.Equals(DateTime.MinValue) Then Return
            dtFromDate.SelectedDate = value
        End Set
    End Property
    Private Property ToDate() As DateTime
        Get
            If Me.dtToDate.SelectedDate.HasValue Then Return dtToDate.SelectedDate.Value
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
        trStats.Visible = c.AddStatistics

        If Not Page.IsPostBack Then
            FromDate = Now.AddMonths(-1)
            ToDate = Now


            If Request.QueryString("Keywords") IsNot Nothing Then Keywords = Request.QueryString("Keywords")
            If Request.QueryString("RecordStatus") IsNot Nothing Then RecordStatus = Request.QueryString("RecordStatus")
            If Request.QueryString("FromDate") IsNot Nothing AndAlso IsDate(Request.QueryString("FromDate")) Then FromDate = Request.QueryString("FromDate")
            If Request.QueryString("ToDate") IsNot Nothing AndAlso IsDate(Request.QueryString("ToDate")) Then ToDate = Request.QueryString("ToDate")

        End If
    End Sub

    Private Sub grdMain_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdMain.ItemCommand
        If e.CommandName = "PDF" Then
            Dim filePath As String = PDFBaseRoot & e.CommandArgument
            If Not System.IO.File.Exists(filePath) Then
                'msg.ShowError("File " & filePath & " doesn't exists anymore.")
                msg.ShowError("Record no longer exists")
                Return
            End If

            Dim outputFileName As String = e.CommandArgument
            Dim lblRequestName As Label = CType(e.Item.FindControl("lblRequestName"), Label)
            If lblRequestName IsNot Nothing AndAlso lblRequestName.Text <> "" Then
                outputFileName = lblRequestName.Text.Trim & " record.pdf"
                outputFileName = Utilities.Validations.MakeValidFileName(outputFileName)
            End If
            StreamFileToUser(filePath, outputFileName)
        ElseIf e.CommandName = "RejectFile" Then
            Dim fileName As String = e.CommandArgument
            Dim FolderPath As String = "C:\Inetpub\vhosts\irstaxrecords.com\subdomains\rejects\httpdocs\Rejection\Images\temp\" & fileName
            If Not System.IO.File.Exists(FolderPath) Then
                Dim lblOrderNumber As Label = e.Item.FindControl("lblOrderNumber")
                Dim pdf As New RejectPDFCreator
                Try
                    pdf.GenerateToFile(CInt(lblOrderNumber.Text), FolderPath)
                Catch ex As Exception
                    msg.ShowError("File " & System.IO.Path.GetFileName(FolderPath) & " doesn't exist. " & ex.Message, ex)
                    Return
                End Try
            End If
            StreamFileToUser(FolderPath, System.IO.Path.GetFileName(FolderPath))
        End If
    End Sub
    Private Shared Function GetPDFFileName(ByVal fldrequestname As String, ByVal RejectCode As Core.Orders.RejectCodeType) As String
        If RejectCode = Orders.RejectCodeType.None Then Return ""
        Dim pdfName As String = fldrequestname.Trim.Replace(" ", "_") & "-" & RejectCode.ToString.Replace(" ", "_").Trim & ".pdf"
        Return pdfName
    End Function
    Private Sub grdMain_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdMain.ItemDataBound
        Try
            Dim lblFormType As Label = CType(e.Item.FindControl("lblFormType"), Label)
            Dim lblOrderDaste As Label = CType(e.Item.FindControl("lblOrderDaste"), Label)
            Dim lblORderStatus As Label = CType(e.Item.FindControl("lblORderStatus"), Label)
            Dim lblTaxYears As Label = CType(e.Item.FindControl("lblTaxYears"), Label)
            Dim lblResponseTime As Label = CType(e.Item.FindControl("lblResponseTime"), Label)
            Dim btnRejectedFile As LinkButton = CType(e.Item.FindControl("btnRejectedFile"), LinkButton)
            Dim btnRecordFile As LinkButton = CType(e.Item.FindControl("btnRecordFile"), LinkButton)
            Dim lblFileNumber As Label = CType(e.Item.FindControl("lblFileNumber"), Label)
            Dim hypRejectReason As HyperLink = CType(e.Item.FindControl("hypRejectReason"), HyperLink)
            Dim lblRequestName As Label = CType(e.Item.FindControl("lblRequestName"), Label)

            Dim dr As DataRowView = CType(e.Item.DataItem, DataRowView)

            If dr("fldPDF") IsNot DBNull.Value AndAlso dr("fldPDF").ToString <> "" Then
                Dim recordFilePath As String = PDFBaseRoot & dr("fldPDF")
                If Not System.IO.File.Exists(recordFilePath) Then
                    btnRecordFile.ForeColor = Drawing.Color.Red
                End If
                btnRecordFile.Text = $"{lblRequestName.Text}'s record"
            End If


            Dim OrderID As Integer = dr("fldordernumber")
            Dim orderDate As DateTime = DateTime.MinValue
            Dim deliveryDate As DateTime = DateTime.MinValue

            If dr("fldOrderDate") IsNot DBNull.Value Then
                lblOrderDaste.Text = CDate(dr("fldOrderDate")).ToShortDateString
                orderDate = CDate(dr("fldOrderDate"))
            End If
            If dr("flddeliverydate") IsNot DBNull.Value Then
                deliveryDate = CDate(dr("flddeliverydate"))
            End If

            If dr("fldLoanNumber") Is DBNull.Value Then dr("fldLoanNumber") = ""
            lblFileNumber.Text = dr("fldLoanNumber")

            If orderDate.Equals(DateTime.MinValue) = False AndAlso deliveryDate.Equals(DateTime.MinValue) = False Then
                lblResponseTime.Text = Globals.GetTimeDifferenceInHours(orderDate, deliveryDate) & " hours"
            End If
            If dr("fldTypeOfForm") IsNot DBNull.Value Then
                lblFormType.Text = Globals.GetFormTypeName(dr("fldTypeOfForm").ToString.Trim)
            End If

            Dim totalYears As New Generic.List(Of Integer)
            For Each col As DataColumn In dr.DataView.Table.Columns
                If col.ColumnName.ToLower.StartsWith("fldtaxyear2") Then
                    If Not dr(col.ColumnName) Is DBNull.Value AndAlso dr(col.ColumnName) = True Then
                        totalYears.Add(col.ColumnName.ToLower.Replace("fldtaxyear", ""))
                    End If
                End If
            Next
            totalYears.Sort()
            lblTaxYears.Text = ""
            For Each Year As Integer In totalYears
                lblTaxYears.Text += Year.ToString & "-"
            Next
            If lblTaxYears.Text.EndsWith("-") Then lblTaxYears.Text = Mid(lblTaxYears.Text, 1, lblTaxYears.Text.Length - 1)


            Select Case dr("fldStatus").ToString.Trim
                Case "d" : lblORderStatus.Text = "Delivered"
                Case "p" : lblORderStatus.Text = "Pending"
                Case "a" : lblORderStatus.Text = "Address Reject"
                Case "s" : lblORderStatus.Text = "Not Matched"
                Case "n" : lblORderStatus.Text = "Bad SSN"
                Case "m" : lblORderStatus.Text = "Matched"
                Case "e" : lblORderStatus.Text = "Expired"
                Case "i" : lblORderStatus.Text = "Invalid SSN"
                Case "r" : lblORderStatus.Text = "No Record"
                Case "u" : lblORderStatus.Text = "Updated"
                Case "c" : lblORderStatus.Text = "Cancelled"
                Case Else
                    lblORderStatus.Text = dr("fldStatus").ToString.Trim
            End Select
            If dr("RejectCode") IsNot DBNull.Value Then
                Dim isRejected As Boolean = True
                Select Case dr("RejectCode").ToString.Trim
                    Case "9" : lblORderStatus.Text = "Bad Address" : hypRejectReason.Text = "Enter correct address" : hypRejectReason.NavigateUrl = $"RejectedOrderInfo.aspx?OrderID={OrderID}&reason=address"
                    Case "3" : lblORderStatus.Text = "SSN Reject" : hypRejectReason.Text = "Enter correct name or SSN" : hypRejectReason.NavigateUrl = $"RejectedOrderInfo.aspx?OrderID={OrderID}&reason=ssn"
                    Case "11" : lblORderStatus.Text = "Name-SSN Do not match" : hypRejectReason.Text = "Enter correct name or SSN" : hypRejectReason.NavigateUrl = $"RejectedOrderInfo.aspx?OrderID={OrderID}&reason=ssn"
                    Case "1" : lblORderStatus.Text = "Illegible" : hypRejectReason.Text = ""
                    Case "4" : lblORderStatus.Text = "Old Date" : hypRejectReason.Text = ""
                    Case "2" : lblORderStatus.Text = "Altered" : hypRejectReason.Text = "Please resubmit legible 4506T"
                    Case "5" : lblORderStatus.Text = "Invalid signature" : hypRejectReason.Text = ""
                    Case "6" : lblORderStatus.Text = "Incomplete form" : hypRejectReason.Text = ""
                    Case "22" : lblORderStatus.Text = "Year not available" : hypRejectReason.Text = ""
                    Case "12" : lblORderStatus.Text = "Invalid form request" : hypRejectReason.Text = ""
                    Case "10" : lblORderStatus.Text = "Unprocessable" : hypRejectReason.Text = "Transcripts not available"
                    Case "13" : lblORderStatus.Text = "Missing line 5" : hypRejectReason.Text = ""
                    Case Else
                        isRejected = False
                End Select
            End If

            Dim requstName As String = dr("fldrequestname")
            Dim RejectCode As Core.Orders.RejectCodeType = dr("RejectCode")

            btnRejectedFile.Text = GetPDFFileName(requstName, RejectCode)
            btnRejectedFile.CommandArgument = btnRejectedFile.Text


            'If btnRecordFile.Text <> "" Then btnRecordFile.Text = ""
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdMain_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
        'If Me.Keywords.Trim = "" Then
        '    Me.grdMain.Visible = False
        '    msg.ShowError("Plese enter some search term.")
        '    Return
        'End If
        Dim strQ As String = "SELECT     tblOrder.*"
        strQ += " FROM dbo.ClientUser "
        strQ += " INNER JOIN dbo.tblorder ON dbo.ClientUser.UserID = dbo.tblorder.fldcustomeriD"
        strQ += " WHERE ClientUser.ClientID = " & Globals.CALoginClientID

        If Keywords <> "" Then
            strQ += " AND ("
            strQ += " fldRequestName LIKE '%" & Keywords.Replace("'", "''") & "%'"
            strQ += " OR  REPLACE(fldSSNNo, '-', '') LIKE '%" & Keywords.Replace("'", "''").Replace("-", "") & "%' "
            strQ += " OR fldLoanNumber LIKe '%" & Keywords.Replace("'", "''") & "%'"
            strQ += ")"
        End If

        If RecordStatus = "r" Then strQ += $" AND IsNull(RejectCode, '') <> ''"
        If RecordStatus = "d" Then strQ += $" AND IsNull(fldStatus, '') =  'd'"


        If Not FromDate.Equals(DateTime.MinValue) Then strQ += " AND fldOrderDate >= '" & FromDate.ToShortDateString & "'"
        If Not ToDate.Equals(DateTime.MinValue) Then strQ += " AND fldOrderDate <= '" & ToDate.ToShortDateString & " 11:59:59 PM'"

        strQ += " Order By fldOrderDate DESC"

        If Request.QueryString("lblQuery") IsNot Nothing Then
            lblQuery.Text = strQ
        End If

        Me.grdMain.DataSource = DataHelper.ExecuteQuery(strQ)
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim url As String = "SearchResult.aspx?Keywords=" & Me.txtFindRecords.Text.Trim
        url += "&RecordStatus=" & RecordStatus
        If Not FromDate.Equals(DateTime.MinValue) Then url += "&FromDate=" & FromDate.ToShortDateString
        If Not ToDate.Equals(DateTime.MinValue) Then url += "&ToDate=" & ToDate.ToShortDateString

        Response.Redirect(url)
    End Sub

    Private Sub btnDoAction_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDoAction.Click
        Select Case Me.ddlAction.SelectedValue
            Case ""
                msg.ShowError("Please select an action")
            Case "DownloadRecords"
                Dim filesPath As New Generic.List(Of String)
                For Each item As Telerik.Web.UI.GridDataItem In Me.grdMain.Items
                    If Not item.Selected Then Continue For

                    Dim btnRecordFile As LinkButton = CType(item.FindControl("btnRecordFile"), LinkButton)
                    Dim fileName As String = btnRecordFile.CommandArgument
                    If fileName Is Nothing OrElse fileName = "" Then fileName = btnRecordFile.Text
                    If fileName.Trim <> "" Then filesPath.Add(PDFBaseRoot & fileName)
                Next

                If filesPath.Count = 0 Then
                    msg.ShowError("No record selected or available to download.")
                    Return
                End If

                Dim tempFile As String = Server.MapPath("~/temp")
                If Not System.IO.Directory.Exists(tempFile) Then System.IO.Directory.CreateDirectory(tempFile)
                tempFile = Server.MapPath("~/temp/" & Guid.NewGuid.ToString & ".zip")
                ZipHelper.ZipFiles(filesPath, tempFile)
                StreamFileToUser(tempFile, "RecordsList.zip")

        End Select
    End Sub

    Private ReadOnly Property PDFBaseRoot() As String
        Get
            Dim root As String = Server.MapPath("~/pdf/pdftest/")
            If Not root.EndsWith("\") Then root += "\"
            Return root
        End Get
    End Property
End Class