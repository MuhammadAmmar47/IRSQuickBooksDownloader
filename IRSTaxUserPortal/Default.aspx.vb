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
        Dim connStr As String = ConfigurationManager.ConnectionStrings("IRSConnection").ConnectionString
        Using conn As New SqlConnection(connStr)
            Dim query As String = "SELECT TOP 500 fldordernumber as [Order Number], fldrequestname as [Tax Payer], fldssnno as [SSN], fldLoanNumber as [Loan Number], fldOrderdate as [Order Date], 
                        fldtypeofform as [Form Type], fldstatus as Status, fldPdf as [File Name]" &
                      "FROM tblorder WHERE fldcustomerID = " & StoreInstance.GetCustomerId() & " " &
                      "ORDER BY fldOrderdate DESC"


            Dim cmd As New SqlCommand(query, conn)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            Grid1.DataSource = dt
            Grid1.DataBind()
        End Using
    End Sub

    ' For paging
    Protected Sub Grid1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Grid1.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub
End Class