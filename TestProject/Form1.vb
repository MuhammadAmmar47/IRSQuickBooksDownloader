Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim service As New IRS.IRSTaxServices
        service.Url = "http://localhost:1143/IRSTaxServices.asmx"

        service.AuthHeaderValue = New IRS.AuthHeader
        service.AuthHeaderValue.UserName = "arthur"
        service.AuthHeaderValue.Password = "12345"

        Try
            service.UploadFile("434 (3).pdf", System.IO.File.ReadAllBytes("C:\Users\Sameers\Desktop\OrderPad\bin\Release\434 (3).pdf"))
        Catch ex As Exception

        End Try
    End Sub
End Class
