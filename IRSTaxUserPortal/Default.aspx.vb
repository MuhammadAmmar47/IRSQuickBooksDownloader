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
        Dim dt As DataTable = OrderServices.GetOrderByCustomers(StoreInstance.GetCustomerId)
        Grid1.DataSource = dt
        Grid1.DataBind()
    End Sub

    ' For paging
    Protected Sub Grid1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Grid1.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub
End Class