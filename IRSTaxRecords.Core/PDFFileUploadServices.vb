Public Class PDFFileUploadServices
    Public Shared Function SavePDFFileUploaded(file As Content.PDFFileUpload) As Boolean
        Dim _helper As New DataServices()
        Dim result As Integer = _helper.PDFFileUpload_AddNew(file.UserID, file.PDFFileName, file.OriginalFileName, file.UploadedOn, file.LoanNumber)
        Return result > 0

    End Function

End Class
