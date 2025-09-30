Imports System.Data.SqlClient
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
        Dim dr1() As DataRow = dtAll.Select("OrderType = '1'")
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
        Dim dr2() As DataRow = dtAll.Select("OrderType = '7'")
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
        Dim dr3() As DataRow = dtAll.Select("OrderType = 'SSV'")
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
            Return DirectCast([Enum].ToObject(GetType(TypeOfForm), intVal), TypeOfForm).ToString()
        Else
            Return "Unknown"
        End If
    End Function
End Class
