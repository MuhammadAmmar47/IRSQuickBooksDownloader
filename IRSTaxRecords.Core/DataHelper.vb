
Public Class DataHelper
    Private Shared LastError As Generic.List(Of Exception) = Nothing
    Public Shared ReadOnly Property LastErrorList() As Generic.List(Of Exception)
        Get
            Return LastError
        End Get
    End Property
    Public Shared ReadOnly Property LastErrorMessage() As String
        Get
            Dim outStr As String = ""

            For Each err As Exception In LastErrorList
                outStr += err.Message & vbCrLf
            Next
            Return outStr
        End Get
    End Property
    Public Shared Function ExecuteQuery(ByVal strQ As String, ByVal ConnectionString As String) As DataTable

        Dim _Helper As SQLServerDataHelper = New SQLServerDataHelper
        _Helper.ConnectionString = ConnectionString

        Dim dt As DataTable = _Helper.ExecuteQuery(strQ)
        LastError = SQLServerDataHelper.ErrorList
        _Helper = Nothing

        Return dt
    End Function
    Public Shared Function ExecuteQuery(ByVal strQ As String) As DataTable
        Return ExecuteQuery(strQ, AppSettings.ConnectionString)
    End Function
    Public Shared Function ExecuteNonQuery(ByVal strQ As String, ByVal ConnectionString As String) As Integer
        If LastError IsNot Nothing Then LastError.Clear()

        Dim _Helper As SQLServerDataHelper = New SQLServerDataHelper
        _Helper.ConnectionString = ConnectionString
        Dim result As Integer = _Helper.ExecuteNonQuery(strQ)
        LastError = SQLServerDataHelper.ErrorList
        Return result
    End Function
    Public Shared Function ExecuteNonQuery(ByVal strQ As String) As Integer
        Return ExecuteNonQuery(strQ, AppSettings.ConnectionString)
    End Function
    Public Shared Function ExecuteAndReadFirstValue(ByVal strQ As String) As Object
        Dim dt As DataTable = ExecuteQuery(strQ)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return dt.Rows(0)(0)
        Else
            Return Nothing
        End If
    End Function
    Public Shared Function ExecuteAndReadFirstValueTrimNull(ByVal strQ As String) As Object
        Dim dt As DataTable = ExecuteQuery(strQ)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            If dt.Rows(0)(0) Is DBNull.Value Then Return ""
            Return dt.Rows(0)(0)
        Else
            Return ""
        End If
    End Function
    Public Shared Function ExecuteDataSet(ByVal strQ As String) As DataSet
        Dim _Helper As SQLServerDataHelper = New SQLServerDataHelper
        _Helper.ConnectionString = AppSettings.ConnectionString
        Dim _data As New DataRequest()
        _data.Command = strQ
        _data.CommandType = CommandType.Text

        _data.Transactional = False
        Dim ds As DataSet = _Helper.ExecuteDataSet(_data)
        LastError = SQLServerDataHelper.ErrorList
        Return ds
    End Function
    Public Shared Function ExecuteAndReadFirstValue(ByVal strQ As String, ByVal ConnectionString As String) As Object
        Dim dt As DataTable = ExecuteQuery(strQ, ConnectionString)
        If dt.Rows.Count = 0 Then Throw New Exception("No record found from Query. can't read first value. " & vbCrLf & strQ)
        Return dt.Rows(0)(0)
    End Function
End Class
