Imports System.Data.SqlClient
Imports System.IO

Public Class order_4506
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim savedFilePath As String = Nothing
        If fuform4506C.HasFile Then
            savedFilePath = UploadFile(fuform4506C.PostedFile, "~/Uploads/")
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

        ' Example hard-coded values (replace with actual logic)
        Dim newListId As Integer = 1
        Dim listType As Integer = 1
        Dim customerId As Integer = 1
        Dim typeOfForm As String = "4506-C"
        Dim email As String = ""
        Dim fax As String = ""
        Dim faxNo As String = ""
        Dim orderDate As DateTime = DateTime.Now
        Dim companyId As Integer = 1

        ' Insert into DB
        InsertOrder(newListId, listType, customerId, requestName, ssnNumber,
                taxYear2024, taxYear2023, taxYear2022, taxYear2021, taxYear2020,
                typeOfForm, email, fax, faxNo, orderDate, companyId, loanNumber)
    End Sub


    Public Sub InsertOrder(
        ByVal newListId As Integer,
        ByVal listType As Integer,
        ByVal customerId As Integer,
        ByVal requestName As String,
        ByVal ssnNumber As String,
        ByVal taxYear2024 As String,
        ByVal taxYear2023 As String,
        ByVal taxYear2022 As String,
        ByVal taxYear2021 As String,
        ByVal taxYear2020 As String,
        ByVal typeOfForm As String,
        ByVal email As String,
        ByVal fax As String,
        ByVal faxNo As String,
        ByVal orderDate As DateTime,
        ByVal companyId As Integer,
        ByVal loanNumber As String)

        Dim query As String = "
            INSERT INTO tblorder
            (
                fldListid,
                fldlisttype,
                fldCustomerID,
                fldRequestName,
                fldssnno,
                fldTaxyear2024,
                fldTaxyear2023,
                fldTaxyear2022,
                fldTaxYear2021,
                fldTaxYear2020,
                fldtypeofform,
                fldemail,
                fldfax,
                fldfaxno,
                fldStatus,
                fldBillingStatus,
                fldOrderDate,
                fldCompanyID,
                fldLoanNumber
            )
            VALUES
            (
                @NewListId,
                @ListType,
                @CustomerID,
                @RequestName,
                @SSNNumber,
                @TaxYear2024,
                @TaxYear2023,
                @TaxYear2022,
                @TaxYear2021,
                @TaxYear2020,
                @TypeOfForm,
                @Email,
                @Fax,
                @FaxNo,
                'p',
                0,
                @OrderDate,
                @CompanyID,
                @LoanNumber
            )"
        Dim connStr As String = ConfigurationManager.ConnectionStrings("IRSConnection").ConnectionString
        Using connection As New SqlConnection(connStr)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@NewListId", newListId)
                command.Parameters.AddWithValue("@ListType", listType)
                command.Parameters.AddWithValue("@CustomerID", customerId)
                command.Parameters.AddWithValue("@RequestName", requestName)
                command.Parameters.AddWithValue("@SSNNumber", ssnNumber)
                command.Parameters.AddWithValue("@TaxYear2024", If(taxYear2024, DBNull.Value))
                command.Parameters.AddWithValue("@TaxYear2023", If(taxYear2023, DBNull.Value))
                command.Parameters.AddWithValue("@TaxYear2022", If(taxYear2022, DBNull.Value))
                command.Parameters.AddWithValue("@TaxYear2021", If(taxYear2021, DBNull.Value))
                command.Parameters.AddWithValue("@TaxYear2020", If(taxYear2020, DBNull.Value))
                command.Parameters.AddWithValue("@TypeOfForm", typeOfForm)
                command.Parameters.AddWithValue("@Email", email)
                command.Parameters.AddWithValue("@Fax", fax)
                command.Parameters.AddWithValue("@FaxNo", faxNo)
                command.Parameters.AddWithValue("@OrderDate", orderDate)
                command.Parameters.AddWithValue("@CompanyID", companyId)
                command.Parameters.AddWithValue("@LoanNumber", loanNumber)

                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
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