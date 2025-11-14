Imports IRSTaxRecords.Core

Public Class Default1
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True

        If Not IsPostBack Then
            If StoreInstance.IsUserLoggedIn Then
                pnlGrid.Visible = True
                BindGrid()

                lblGreeting.Text = $"Welcome: {StoreInstance.CurrentUser.Name}"
            Else
                pnlGrid.Visible = False
            End If

            If Session("PasswordChanged") IsNot Nothing AndAlso CBool(Session("PasswordChanged")) = True Then
                Session("PasswordChanged") = False
                lblPasswordChanged.Visible = True
            Else
                lblPasswordChanged.Visible = False
            End If
        Else
        End If
    End Sub

    Private Sub BindGrid()
        Dim dtAll As DataTable = OrderServices.GetOrderByCustomers(StoreInstance.GetCustomerId)

        ' Grid1
        Dim dr1() As DataRow = dtAll.Select($"OrderType = {CInt(Orders.OrderType.Form_4506)} AND FormType <> {CInt(Orders.FormTypeCodeType.S_SSN)}")
        If dr1.Length > 0 Then
            Dim dtFirst As DataTable = dr1.CopyToDataTable()
            dtFirst.DefaultView.Sort = "OrderNumber DESC"
            Grid1.DataSource = dtFirst.DefaultView
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
            Dim dtFirst As DataTable = dr2.CopyToDataTable()
            dtFirst.DefaultView.Sort = "OrderNumber DESC"
            Grid2.DataSource = dtFirst.DefaultView
            lblGrid2Message.Visible = False
        Else
            Grid2.DataSource = Nothing
            lblGrid2Message.Text = "No data found"
            lblGrid2Message.Visible = True
        End If
        Grid2.DataBind()

        ' Grid3
        Dim dr3() As DataRow = dtAll.Select($"OrderType = {CInt(Orders.OrderType.Form_4506)} AND FormType = {CInt(Orders.FormTypeCodeType.S_SSN)}")
        If dr3.Length > 0 Then
            Dim dtFirst As DataTable = dr3.CopyToDataTable()
            dtFirst.DefaultView.Sort = "OrderNumber DESC"
            Grid3.DataSource = dtFirst.DefaultView
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
            Case "h" : Return "Unprocessable"
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

        If dr("DeliveryDate") IsNot DBNull.Value Then
            Dim deliveryDate As DateTime = CDate(dr("DeliveryDate").ToString)
            If deliveryDate > New Date(2001, 1, 1) Then
                lblDeliveryDate.Text = deliveryDate.ToString("MM-dd-yyyy")
            Else
                lblDeliveryDate.Text = ""
            End If
        End If


        Select Case status.ToLower
            Case "p"
                e.Row.CssClass &= " highlightRow"
                btnView.ImageUrl = "/img/spaceclear.gif"

                Dim isSSNGrid As Boolean = False
                If TypeOf sender Is GridView Then
                    If CType(sender, GridView).ID.ToLower.Equals("grid3") Then
                        isSSNGrid = True
                    End If
                End If
                Dim orderDate As DateTime = CDate(dr("OrderDate").ToString)
                If dr("OrderType") Is DBNull.Value Then dr("OrderType") = CInt(Orders.OrderType.Form_4506)
                orderDate = GetDeliveryDate(orderDate, dr("OrderType"), isSSNGrid)
                lblDeliveryDate.Text = orderDate.ToString("MM-dd-yyyy")
            Case "c"
                btnView.ImageUrl = "/img/spaceclear.gif"
        End Select

        If btnView Is Nothing Then Return

        Dim OrderNumber = CInt(dr("OrderNumber"))
        Dim FileName = dr("File Name").ToString
        If FileName.IsNullOrEmpty Then
            btnView.ImageUrl = "/img/spaceclear.gif"
            btnView.OnClientClick = "return false;"
        Else
            btnView.OnClientClick = $"downloadFile({OrderNumber}); return false;"
        End If

    End Sub
    Private Shared Function GetDeliveryDate(actualDate As DateTime, orderType As Orders.OrderType, isSSNGrid As Boolean) As DateTime
        If isSSNGrid Then
            If actualDate.DayOfWeek = DayOfWeek.Saturday Then
                actualDate = actualDate.AddDays(2)
            ElseIf actualDate.DayOfWeek = DayOfWeek.Sunday Then
                actualDate = actualDate.AddDays(1)
            End If
            Return actualDate
        End If
        Select Case orderType
            Case Orders.OrderType.Form_4506
                Return AddDaysConsideringHolidays(actualDate, 3)
            Case Orders.OrderType.Form_8821
                Return AddDaysConsideringHolidays(actualDate, 1)
        End Select
        Return actualDate
    End Function
    Private Shared Function AddDaysConsideringHolidays(theDate As DateTime, daysToAdd As Integer) As DateTime
        For temp As Integer = 1 To daysToAdd
            theDate = theDate.AddDays(1)
            If theDate.DayOfWeek = DayOfWeek.Saturday Then
                theDate = theDate.AddDays(1)
            End If
            If theDate.DayOfWeek = DayOfWeek.Sunday Then
                theDate = theDate.AddDays(1)
            End If
        Next
        Return theDate
    End Function

End Class
