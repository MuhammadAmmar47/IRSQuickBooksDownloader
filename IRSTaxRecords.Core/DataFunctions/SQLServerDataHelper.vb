Imports System.Data
Imports System.Data.SqlClient

Public Class SQLServerDataHelper

    Private _connectionString As String

    Private Shared _LastError As Generic.List(Of Exception) = Nothing

    Public Sub New()
    End Sub
    Public Sub New(ByVal connString As String)
        _connectionString = connString
    End Sub

    Public Shared Property ErrorList() As Generic.List(Of Exception)
        Get
            If _LastError Is Nothing Then _LastError = New Generic.List(Of Exception)
            'Return _ErrorList
            Return _LastError
        End Get
        Set(ByVal Value As Generic.List(Of Exception))
            _LastError = Value
            '_ErrorList = Value
        End Set
    End Property
    Public Shared ReadOnly Property LastErrorMessage() As String
        Get
            Dim outStr As String = ""

            For Each err As Exception In ErrorList
                outStr += err.Message & vbCrLf
            Next
            Return outStr
        End Get
    End Property

    Public Property ConnectionString() As String
        Get
            Return _connectionString
        End Get
        Set(ByVal Value As String)
            _connectionString = Value
        End Set
    End Property

    Public Function ExecuteNonQuery(ByVal strQ As String) As Integer
        Dim req As New DataRequest
        req.CommandType = CommandType.Text
        req.Command = strQ
        req.Transactional = False
        Return ExecuteNonQuery(req)
    End Function
    Public Function ExecuteNonQuery(ByVal request As DataRequest) As Integer

        Dim connection As New SqlConnection(ConnectionString)
        Dim command As New SqlCommand
        Dim _parameter As SqlParameter = Nothing
        Dim _Param As DataRequest.Parameter = Nothing
        Dim iRows As Integer
        Try
            ' attempt to open sql connection and exec command
            connection.Open()
            command.Connection = connection
            command.CommandText = request.Command
            command.CommandType = request.CommandType

            ' add parameters if they exist
            If request.Parameters.Count > 0 Then
                For Each _Param In request.Parameters
                    _parameter = command.Parameters.AddWithValue(_Param.ParamName, _Param.ParamValue)
                Next
            End If

            iRows = command.ExecuteNonQuery()
        Catch exData As SqlClient.SqlException
            Debug.WriteLine(exData.Message)
            request.Exception = exData
            Exceptions.ErrorLogging.AddToLog(Exceptions.ERROR_TYPE.EXCEPTION, exData.Message & GetParametersList(request), exData)
            ErrorList.Add(exData)
            iRows = -1
            Throw
        Catch ex As Exception
            ErrorList.Add(ex)
            Debug.WriteLine(ex.Message)
            request.Exception = ex
            Exceptions.ErrorLogging.AddToLog(Exceptions.ERROR_TYPE.EXCEPTION, ex.Message & GetParametersList(request), ex)
            iRows = -1
            Throw
        Finally
            command.Dispose()
            connection.Dispose()
        End Try

        Return iRows
    End Function
    Public Function ExecuteQuery(ByVal request As DataRequest) As DataTable
        Return ExecuteDataSet(request).Tables(0)
    End Function
    Public Function ExecuteQuery(ByVal strQ As String) As DataTable
        Dim request As New DataRequest
        request.Command = strQ
        request.CommandType = CommandType.Text
        request.Transactional = False

        Dim data As New SQLServerDataHelper(AppSettings.ConnectionString)
        Return data.ExecuteDataSet(request).Tables(0)
    End Function
    Public Function ExecuteDataSet(ByVal request As DataRequest) As DataSet
        Dim connection As New SqlConnection(_connectionString)
        Dim command As New SqlCommand
        Dim SQLParameter As SqlParameter = Nothing
        Dim param As DataRequest.Parameter = Nothing
        Dim adapter As SqlDataAdapter = Nothing
        Dim outputDataSet As New DataSet

        Try
            connection.Open()
            command.Connection = connection
            command.CommandText = request.Command
            command.CommandType = request.CommandType

            ' add parameters if they exist
            If request.Parameters.Count > 0 Then
                For Each param In request.Parameters
                    SQLParameter = command.Parameters.AddWithValue(param.ParamName, param.ParamValue)
                Next
            End If

            adapter = New SqlDataAdapter(command)

            ' allow generic naming - NewDataSet
            adapter.Fill(outputDataSet)

        Catch exSQL As SqlClient.SqlException
            ErrorList.Add(exSQL)
            Debug.WriteLine(exSQL.Message)
            Exceptions.ErrorLogging.AddToLog(Exceptions.ERROR_TYPE.EXCEPTION, exSQL.Message & GetParametersList(request), exSQL)
            request.Exception = exSQL
            Throw
        Catch ex As Exception
            ErrorList.Add(ex)
            Debug.WriteLine(ex.Message)
            Exceptions.ErrorLogging.AddToLog(Exceptions.ERROR_TYPE.EXCEPTION, ex.Message & GetParametersList(request), ex)
            request.Exception = ex
            Throw
        Finally
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If

            command.Dispose()
            adapter.Dispose()
            connection.Dispose()
        End Try

        Return outputDataSet
    End Function
    Public Function ExecuteAndReadInteger(ByVal request As DataRequest, ByVal fieldName As String) As Integer
        Dim result As Integer = -1
        Dim connection As New SqlConnection(ConnectionString)
        Dim command As New SqlCommand
        Dim _parameter As SqlParameter = Nothing
        Dim _param As DataRequest.Parameter = Nothing
        Dim reader As SqlDataReader = Nothing

        Try
            ' open connection, and begin to set properties of command
            connection.Open()
            command.Connection = connection
            command.CommandText = request.Command
            command.CommandType = request.CommandType

            ' add parameters if they exist
            If request.Parameters.Count > 0 Then
                For Each _param In request.Parameters
                    If Convert.ToString(_param.ParamValue) = Nothing Then
                        _parameter = command.Parameters.AddWithValue(_param.ParamName, String.Empty)
                    Else
                        _parameter = command.Parameters.AddWithValue(_param.ParamName, _param.ParamValue)
                    End If

                Next
            End If

            reader = command.ExecuteReader()
            If reader.Read() Then
                If Not reader(fieldName) Is DBNull.Value Then
                    result = Convert.ToInt32(reader(fieldName))
                End If
            End If
            reader.Close()

        Catch exSQL As SqlException
            ErrorList.Add(exSQL)
            Debug.WriteLine(exSQL.Message)
            request.Exception = exSQL
            Exceptions.ErrorLogging.AddToLog(Exceptions.ERROR_TYPE.EXCEPTION, exSQL.Message & GetParametersList(request), exSQL)
            If connection.State <> ConnectionState.Closed Then
                connection.Close()
            End If
            command.Dispose()
            connection.Dispose()
            Throw
        Catch ex As Exception
            ErrorList.Add(ex)
            Debug.WriteLine(ex.Message)
            request.Exception = ex
            Exceptions.ErrorLogging.AddToLog(Exceptions.ERROR_TYPE.EXCEPTION, ex.Message & GetParametersList(request), ex)
            If connection.State <> ConnectionState.Closed Then
                connection.Close()
            End If
            command.Dispose()
            connection.Dispose()
            Throw
        Finally
            If connection.State <> ConnectionState.Closed Then
                connection.Close()
            End If
            command.Dispose()
            connection.Dispose()
            reader = Nothing
        End Try

        Return result
    End Function

    Private Function GetParametersList(ByVal req As DataRequest) As String
        Dim outStr As String = vbCrLf & "Parameters List: " & vbCrLf
        For Each param As DataRequest.Parameter In req.Parameters
            outStr += param.ParamName & " : "
            If param.ParamValue Is DBNull.Value OrElse param.ParamValue Is Nothing Then
                outStr += " NULL"
            Else
                outStr += param.ParamValue.ToString
            End If
            outStr += vbCrLf

        Next
        Return outStr
    End Function

End Class
