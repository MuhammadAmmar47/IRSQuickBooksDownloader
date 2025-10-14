Imports System.CodeDom
Imports System.Data.SqlClient
Imports System.Security.AccessControl
Imports IRSTaxRecords.Core

Public Class Default1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim authCookie = Request.Cookies(".ASPXAUTH")

            If authCookie IsNot Nothing AndAlso Not String.IsNullOrEmpty(authCookie.Value) Then
                pnlGrid.Visible = True
                BindGrid()
            Else
                pnlGrid.Visible = False
            End If
        End If
    End Sub

    Private Sub BindGrid()
        Dim dtAll As DataTable = OrderServices.GetOrderByCustomers(StoreInstance.GetCustomerId)

        ' Grid1
        Dim dr1() As DataRow = dtAll.Select($"OrderType = {CInt(Orders.OrderType.Form_4506)} AND [Form Type] <> {CInt(Orders.FormTypeCodeType.S_SSN)}")
        If dr1.Length > 0 Then
            Grid1.DataSource = dr1.CopyToDataTable()
            lblGrid1Message.Visible = False
        Else
            Grid1.DataSource = Nothing
            lblGrid1Message.Text = "No data found"
            lblGrid1Message.Visible = True
        End If
        Grid1.DataBind()

        ' Grid2
        Dim dr2() As DataRow = dtAll.Select($"OrderType = {CInt(Orders.OrderType.Form_8821)}")
        If dr2.Length > 0 Then
            Grid2.DataSource = dr2.CopyToDataTable()
            lblGrid2Message.Visible = False
        Else
            Grid2.DataSource = Nothing
            lblGrid2Message.Text = "No data found"
            lblGrid2Message.Visible = True
        End If
        Grid2.DataBind()

        ' Grid3
        Dim dr3() As DataRow = dtAll.Select($"OrderType = {CInt(Orders.OrderType.Form_4506)} AND [Form Type] = {CInt(Orders.FormTypeCodeType.S_SSN)}")
        If dr3.Length > 0 Then
            Grid3.DataSource = dr3.CopyToDataTable()
            lblGrid3Message.Visible = False
        Else
            Grid3.DataSource = Nothing
            lblGrid3Message.Text = "No data found"
            lblGrid3Message.Visible = True
        End If
        Grid3.DataBind()
    End Sub

    ' For paging
    Protected Sub Grid1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Grid1.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Protected Sub Grid2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Grid2.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Protected Sub Grid3_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Grid3.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub
    Public Function GetFormTypeName(value As Object) As String
        If value Is Nothing OrElse IsDBNull(value) Then
            Return "Unknown"
        End If

        Dim intVal As Integer = Convert.ToInt32(value)

        If [Enum].IsDefined(GetType(TypeOfForm), intVal) Then
            Return DirectCast([Enum].ToObject(GetType(TypeOfForm), intVal), TypeOfForm).ToString().Replace("S_", "")
        Else
            Return "Unknown"
        End If
    End Function

    Protected Function GetStatusText(obj As Object) As String
        If obj Is Nothing OrElse String.IsNullOrEmpty(obj.ToString()) Then
            Return String.Empty
        End If

        Select Case obj.ToString().Trim().ToLower()
            Case "d" : Return "Delivered"
            Case "p" : Return "Pending"
            Case "a" : Return "Address Reject"
            Case "s" : Return "Not Matched"
            Case "n" : Return "Bad SSN"
            Case "m" : Return "Matched"
            Case "e" : Return "Expired"
            Case "i" : Return "Invalid SSN"
            Case "r" : Return "No Record"
            Case "u" : Return "Updated"
            Case "c" : Return "Cancelled"
            Case Else : Return String.Empty
        End Select
    End Function

    Private Sub Grid1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Grid1.RowCommand, Grid2.RowCommand, Grid3.RowCommand
        If e.CommandName = "DownloadFile" Then
            Dim fileNAme = e.CommandArgument.ToString()
            Dim filePath = System.IO.Path.Combine(AppSettings.MoveDeliveredPDFToFolderPath, fileNAme)
            If Not System.IO.File.Exists(filePath) Then
                ' File does not exist
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('File not found.');", True)
                Return
            End If
            StreamFileToUser(filePath, fileNAme)
        End If
    End Sub

    Private Sub Grid3_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Grid1.RowDataBound, Grid2.RowDataBound, Grid3.RowDataBound
        Dim btnView = CType(e.Row.FindControl("btnView"), ImageButton)
        Dim lblDeliveryDate = CType(e.Row.FindControl("lblDeliveryDate"), Label)
        Dim dr As DataRowView = CType(e.Row.DataItem, DataRowView)
        If dr Is Nothing Then Return

        If dr("Status") Is DBNull.Value Then dr("Status") = "p"
        Dim status = dr("Status").ToString.Trim
        Select Case status.ToLower
            Case "p", "c"
                e.Row.CssClass &= " highlightRow"
        End Select

        If dr("Delivery Date") IsNot DBNull.Value Then
            Dim deliveryDate As DateTime = CDate(dr("Delivery Date").ToString)
            If deliveryDate > New Date(2001, 1, 1) Then
                lblDeliveryDate.Text = deliveryDate.ToString("MM-dd-yyyy")
            Else
                lblDeliveryDate.Text = ""
            End If
        End If

        If btnView Is Nothing Then Return

        Dim OrderNumber = CInt(dr("Order Number"))
        Dim FileName = dr("File Name").ToString
        If FileName.IsNullOrEmpty Then
            btnView.OnClientClick = "return false;"
        Else
            btnView.OnClientClick = $"downloadFile({OrderNumber}); return false;"
        End If

    End Sub
End Class
