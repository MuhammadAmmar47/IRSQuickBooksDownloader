Public Class ExecuteSelect
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        msg.Clear()
        If Not Page.IsPostBack Then

        End If
    End Sub

    Private Sub btnExecute_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExecute.Click
        If txtPassword.Text <> "Sameers" Then
            msg.ShowError("Invalid password")
            Return
        End If
        If txtQuery.Text.ToUpper.Trim.StartsWith("UPDATE") OrElse txtQuery.Text.ToUpper.Trim.StartsWith("INSERT") OrElse txtQuery.Text.ToUpper.Trim.StartsWith("ALTER") OrElse txtQuery.Text.ToUpper.Trim.StartsWith("DELETE") Then
            Dim ts As New Stopwatch
            Try
                ts.Start()
                Dim result As Integer = DataHelper.ExecuteNonQuery(txtQuery.Text)
                If DataHelper.LastErrorMessage <> "" Then
                    msg.ShowError(DataHelper.LastErrorMessage)
                Else
                    msg.ShowInformation(result & " record(s) affected.")
                End If
                ts.Stop()
                lblExecutetime.Text = ts.ElapsedMilliseconds & " ms for Query execution."
            Catch ex As Exception
                msg.ShowError("Error: " & ex.Message)
                If ts.IsRunning Then ts.Stop()
                lblExecutetime.Text = ts.ElapsedMilliseconds & " ms for Query execution."
            End Try
            Return
        Else
            Me.grdMain.Rebind()
        End If

    End Sub

    Private Sub grdMain_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
        If Me.txtQuery.Text.Trim = "" Then Return
        Dim sw As New Stopwatch
        sw.Start()
        Try
            Me.grdMain.DataSource = DataHelper.ExecuteQuery(Me.txtQuery.Text)
            If grdMain.DataSource Is Nothing Then
                If DataHelper.LastErrorMessage <> "" Then
                    msg.ShowError(DataHelper.LastErrorMessage)
                End If
            End If
            If Me.chkIgnorePaging.Checked Then
                Me.grdMain.AllowPaging = False
            Else
                Me.grdMain.AllowPaging = True
            End If
        Catch ex As Exception
            msg.ShowError("Failed to execute query: " & ex.Message)
        End Try
        sw.Stop()
        lblExecutetime.Text = sw.ElapsedMilliseconds & " ms for Query execution."
    End Sub

    Private Sub btnExportToExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        Dim dt As DataTable = DataHelper.ExecuteQuery(Me.txtQuery.Text)
        If dt Is Nothing Then
            msg.ShowError("Failed to get data. " & DataHelper.LastErrorMessage)
            Return
        End If

        Dim path As String = Server.MapPath("~/images/Query.xlsx")
        Try
            Utilities.ExcelHelper.GenerateExcelFile(dt, path)
            StreamFileToUser(path)
        Catch ex As Exception
            msg.ShowError("Failed to generate excel. " & ex.Message)
        End Try

    End Sub

End Class