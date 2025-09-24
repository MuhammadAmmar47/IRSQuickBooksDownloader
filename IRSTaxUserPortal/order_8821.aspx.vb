Imports System.IO
Imports IRSTaxRecords.Core

Public Class order_8821
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSDubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If fuform8821.HasFile Then
            Dim savedFilePath As String = Nothing
            If fuform8821.HasFile Then
                savedFilePath = UploadFile(fuform8821.PostedFile, "~/Uploads/")
            End If

            ' Collect form values
            Dim requestName As String = txtTaxPayerName.Text.Trim()
            Dim ssnNumber As String = txtSocialSecurityNumber.Text.Trim()
            Dim loanNumber As String = txtLoanNumber.Text.Trim()

            ' Tax year checkboxes
            Dim taxYear2024 As String = If(chk2024.Checked, "2024", Nothing)
            Dim taxYear2023 As String = If(chk2023.Checked, "2023", Nothing)
            Dim taxYear2022 As String = If(chk2022.Checked, "2022", Nothing)
            Dim taxYear2021 As String = If(chk2021.Checked, "2021", Nothing)
            Dim taxYear2020 As String = If(chk2020.Checked, "2020", Nothing)

            Dim newListId As Integer = 1
            Dim listType As Integer = 1
            Dim typeOfForm As String = "8821"
            Dim email As String = ""
            Dim fax As String = ""
            Dim faxNo As String = ""
            Dim orderDate As DateTime = DateTime.Now
            Dim customerId As Integer = StoreInstance.GetCustomerId()
            Dim companyId As Integer = StoreInstance.GetCustomerId()

            ' Insert into DB
            order_4506.InsertOrder(newListId, listType, customerId, requestName, ssnNumber,
                taxYear2024, taxYear2023, taxYear2022, taxYear2021, taxYear2020,
                typeOfForm, email, fax, faxNo, orderDate, companyId, loanNumber, fuform8821.PostedFile.FileName
                )

            Response.Redirect("~/Confirmation.aspx")
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