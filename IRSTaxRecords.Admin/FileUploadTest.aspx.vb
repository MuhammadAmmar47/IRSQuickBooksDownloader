Public Partial Class FileUploadTest
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        System.Threading.Thread.Sleep(3000)
        FileUpload1.SaveAs(Server.MapPath("~/" & FileUpload1.FileName))
    End Sub
End Class