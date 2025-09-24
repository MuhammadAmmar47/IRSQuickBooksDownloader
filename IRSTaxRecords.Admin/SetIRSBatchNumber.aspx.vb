Imports IRSTaxRecords.Core

Partial Public Class SetIRSBatchNumber
    Inherits System.Web.UI.Page

    Private Property BatchNumberFrom() As Integer
        Get
            Return Val(Me.txtBatchNumberFrom.Text)
        End Get
        Set(ByVal value As Integer)
            Me.txtBatchNumberFrom.Text = value
        End Set
    End Property
    Private Property BatchNumberTo() As Integer
        Get
            Return Val(Me.txtBatchNumberTo.Text)
        End Get
        Set(ByVal value As Integer)
            Me.txtBatchNumberTo.Text = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        msg.Clear()

        If Not Page.IsPostBack Then

            If Request.QueryString("BatchNumberFrom") IsNot Nothing Then BatchNumberFrom = Val(Request.QueryString("BatchNumberFrom"))
            If Request.QueryString("BatchNumberTo") IsNot Nothing Then BatchNumberTo = Val(Request.QueryString("BatchNumberTo"))

            If Session("actionMsg") IsNot Nothing Then
                msg.ShowInformation(Session("actionMsg"))
                Session("actionMsg") = Nothing
            End If
        End If

    End Sub

    Private Sub grdMain_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
        If BatchNumberFrom < 1 OrElse BatchNumberTo < 1 Then
            msg.ShowError("Please enter batch # range to search for.")
            Me.btnUpdateBatchNumbers.Visible = False
            Return
        End If
        Dim strQ As String = "SELECT TOP 200 * FROM tbllist WHERE fldListID>=" & BatchNumberFrom & " AND fldListID<=" & BatchNumberTo
        Me.grdMain.DataSource = DataHelper.ExecuteQuery(strQ)
    End Sub

    Private Sub btnSearchBatch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchBatch.Click
        Dim url As String = "SetIRSBatchNumber.aspx?BatchNumberFrom=" & BatchNumberFrom & "&BatchNumberTo=" & BatchNumberTo
        Response.Redirect(url)
    End Sub

    Private Sub btnUpdateBatchNumbers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateBatchNumbers.Click
        For Each item As Telerik.Web.UI.GridDataItem In Me.grdMain.Items
            Dim lblLocalBatchNumber As Label = CType(item.FindControl("lblLocalBatchNumber"), Label)
            Dim txtIRSBatchNumber As TextBox = CType(item.FindControl("txtIRSBatchNumber"), TextBox)

            Dim list As Content.ListType = ListServices.GetList(lblLocalBatchNumber.Text)
            list.IRSBatchNumber = txtIRSBatchNumber.Text.Trim
            If Not list.IRSBatchNumber.ToUpper.StartsWith("GO") Then list.IRSBatchNumber = "GO" & list.IRSBatchNumber
            ListServices.UpdateList(list)
        Next

        Session("actionMsg") = "IRS Batch numbers saved successfully"
        Response.Redirect(Request.Url.PathAndQuery)
    End Sub

    Private Sub btnDownloadsample_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownloadsample.Click
        Dim strQ As String = "SELECT fldListID as ListID, IRSBatchNumber from tbllist WHERE IRSBatchNumber IS NULL OR IRSBatchNumber = '' Order By fldListID DESC"
        Dim dt As DataTable = DataHelper.ExecuteQuery(strQ)
        Dim tempFile As String = Server.MapPath("temp")
        If Not System.IO.Directory.Exists(tempFile) Then System.IO.Directory.CreateDirectory(tempFile)
        If Not tempFile.EndsWith("\") Then tempFile += "\"
        tempFile += Guid.NewGuid.ToString & ".xls"
        Utilities.ExcelHelper.GenerateExcelFile(dt, tempFile)
        StreamFileToUser(tempFile, "IRSBatchNumbers.xls")
    End Sub

    Private Sub btnUploadfile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadfile.Click
        If Me.flUpload.PostedFile Is Nothing OrElse Me.flUpload.PostedFile.FileName = "" Then
            msg.ShowError("Please select file to upload")
            Return
        End If

        Dim tempFile As String = Server.MapPath("~/temp/")
        If Not System.IO.Directory.Exists(tempFile) Then System.IO.Directory.CreateDirectory(tempFile)
        If Not tempFile.EndsWith("\") Then tempFile += "\"
        tempFile += Guid.NewGuid.ToString & System.IO.Path.GetExtension(Me.flUpload.PostedFile.FileName)
        flUpload.PostedFile.SaveAs(tempFile)

        Dim dt As DataTable = Nothing
        Try
            dt = Utilities.ExcelHelper.GetAsDataTable(tempFile, "")
        Catch ex As Exception
            msg.ShowError("Failed to read excel file. " & ex.Message)
            Return
        End Try

        Dim totalUpdated As Integer = 0
        For Each row As DataRow In dt.Rows
            If row("ListID") Is DBNull.Value Then Continue For
            If row("IRSBatchNumber") Is DBNull.Value Then Continue For

            Dim strQ As String = "UPDATE tbllist SET IRSBatchNumber='" & row("IRSBatchNumber").ToString.Replace("'", "''") & "' WHERE fldListID=" & Val(row("ListID"))
            If DataHelper.ExecuteNonQuery(strQ) > 0 Then
                totalUpdated += 1
            End If
        Next

        msg.ShowInformation(totalUpdated & " rows updated")


    End Sub
End Class