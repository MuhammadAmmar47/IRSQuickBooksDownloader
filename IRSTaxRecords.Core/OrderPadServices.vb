Public Class OrderPadServices
    Public Shared Function AddNew(ByVal file As Core.Content.OrderPadFile) As Boolean
        file.ID = DataServices.OrderPadFile_AddNew(file.UserID, file.UploadedOn, file.FileName, file.FileNameReal, file.ErrorMessage)
        Return file.ID > 0
    End Function
    Public Shared Function Update(ByVal file As Core.Content.OrderPadFile) As Boolean
        Return DataServices.OrderPadFile_Update(file.ID, file.UserID, file.UploadedOn, file.FileName, file.FileNameReal, file.ErrorMessage)
    End Function
    Public Shared Function GetByID(ByVal ID As Integer) As Core.Content.OrderPadFile
        Dim dt As DataTable = DataServices.OrderPadFile_GetByID(ID)
        If dt.Rows.Count = 0 Then Return Nothing
        Return DAtaRowToFile(dt.Rows(0))
    End Function

    Private Shared Function DAtaRowToFile(ByVal dr As DataRow) As Core.Content.OrderPadFile
        Dim file As New Core.Content.OrderPadFile
        With file
            If Not dr("ID") Is DBNull.Value Then .ID = dr("ID")
            If Not dr("UserID") Is DBNull.Value Then .UserID = dr("UserID")
            If Not dr("UploadedOn") Is DBNull.Value Then .UploadedOn = dr("UploadedOn")
            If Not dr("FileName") Is DBNull.Value Then .FileName = dr("FileName")
            If Not dr("FileNameReal") Is DBNull.Value Then .FileNameReal = dr("FileNameReal")
            If Not dr("ErrorMessage") Is DBNull.Value Then .ErrorMessage = dr("ErrorMessage")
        End With
        Return file
    End Function
End Class
