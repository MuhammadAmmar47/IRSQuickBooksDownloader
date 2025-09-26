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
        Dim dt4506 As DataTable = OrderServices.GetOrderByCustomers(StoreInstance.GetCustomerId, "1")
        Dim dt8821 As DataTable = OrderServices.GetOrderByCustomers(StoreInstance.GetCustomerId, "7")
        Dim dtSSV As DataTable = OrderServices.GetOrderByCustomers(StoreInstance.GetCustomerId, "SSV")

        Grid1.DataSource = dt4506
        Grid1.DataBind()

        Grid2.DataSource = dt8821
        Grid2.DataBind()

        Grid3.DataSource = dtSSV
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
End Class
