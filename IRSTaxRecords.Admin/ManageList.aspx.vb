Imports IRSTaxRecords.Core

Partial Public Class ManageList
    Inherits System.Web.UI.Page

    Private Property ListType() As Integer
        Get
            If lstListType.SelectedItem Is Nothing Then Return -1
            Return lstListType.SelectedValue
        End Get
        Set(ByVal value As Integer)
            If lstListType.Items.FindByValue(value) IsNot Nothing Then
                lstListType.ClearSelection()
                lstListType.Items.FindByValue(value).Selected = True
            End If
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        msg.Clear()

        If Not Page.IsPostBack Then
            If Request.QueryString("ListType") IsNot Nothing Then ListType = Request.QueryString("ListType")


            If ListType < 1 Then Response.Redirect("ManageList.aspx?ListType=1")

            If Session("actionMsg") IsNot Nothing Then
                msg.ShowInformation(Session("actionMsg"))
                Session("actionMsg") = Nothing
            End If
        End If

    End Sub

    Private Sub grdMain_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
        Dim strQ As String = "SELECT TOP 10 * "
        strQ += " , TotalPending=(select  count(fldordernumber) as countnumber from tblorder where fldstatus='p' and fldListID=tbllist.fldlistid)"
        strQ += " from tblList "
        strQ += " WHERE fldlisttype = " & ListType
        strQ += " Order By fldDateCheck desc"

        Me.grdMain.DataSource = DataHelper.ExecuteQuery(strQ)
    End Sub

    Private Sub lstListType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstListType.SelectedIndexChanged
        Response.Redirect("ManageList.aspx?ListType=" & ListType)
    End Sub

    Private Sub btnProceed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProceed.Click
        Response.Redirect("CreateNewList.aspx?ListType=" & ListType)
    End Sub
End Class