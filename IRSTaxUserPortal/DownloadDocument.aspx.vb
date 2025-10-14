Imports IRSTaxRecords.Core

Partial Public Class DownloadDocument '181061
    Inherits System.Web.UI.Page
    Private ReadOnly Property OrderId As Integer
        Get
            Return Val(Request.QueryString("OrderID"))
        End Get
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            DownloadDocument()
        End If
    End Sub
    Private Sub DownloadDocument()
        Dim order = OrderServices.GetOrder(Me.OrderId)
        If order Is Nothing Then
            Response.Write("File doesn't exists")
            Return
        End If
        If order.fldcustomeriD <> StoreInstance.GetCustomerId() Then
            Response.Write("You are not authorized to download this file.")
            Return
        End If
        If order.fldPdf.IsNullOrEmpty Then
            Response.Write("file is not ready yet.")
            Return
        End If

        Dim fullPath As String = AppSettings.MoveDeliveredPDFToFolderPath & order.fldPdf
        If Not System.IO.File.Exists(fullPath) Then
            Response.Write($"File {fullPath} doesn't exists on disk anymore.")
            Return
        End If

        StreamFileToUser(fullPath)
    End Sub
    Private Sub StreamFileToUser(outfilePath As String)
        Dim FileBuffer = System.IO.File.ReadAllBytes(outfilePath)
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-length", FileBuffer.Length.ToString())
        Response.BinaryWrite(FileBuffer)
    End Sub

End Class