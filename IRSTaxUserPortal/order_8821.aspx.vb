Imports System.IO

Public Class order_8821
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSDubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If fuform8821.HasFile Then
            ' Save file & get full path
            Dim savedFilePath As String = UploadFile(fuform8821.PostedFile, "~/Uploads/")

            If savedFilePath IsNot Nothing Then
                ' lblMessage.Text = "File uploaded successfully. Saved at: " & savedFilePath
            Else
                'lblMessage.Text = "File upload failed."
            End If
        End If
    End Sub


    Public Shared Function UploadFile(file As HttpPostedFile, uploadFolderPath As String) As String
        If file Is Nothing OrElse file.ContentLength = 0 Then
            Return Nothing
        End If

        Try
            Dim serverPath As String = HttpContext.Current.Server.MapPath(uploadFolderPath)
            If Not Directory.Exists(serverPath) Then
                Directory.CreateDirectory(serverPath)
            End If

            Dim extension As String = Path.GetExtension(file.FileName)
            Dim newFileName As String = DateTime.Now.ToString("yyyyMMddHHmmss") & "_" & Guid.NewGuid().ToString() & extension


            Dim filePath As String = Path.Combine(serverPath, newFileName)
            file.SaveAs(filePath)

            Return filePath
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class