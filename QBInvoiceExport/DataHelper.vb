Public Class DataHelper

    'Public Shared ReadOnly Property ConnectionString() As String
    '    Get
    '        Return System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
    '    End Get
    'End Property

    'Public Shared Function ExecuteQuery(ByVal strQ As String) As DataTable
    '    Dim con As New SqlClient.SqlConnection(ConnectionString)


    '    Try
    '        con.Open()

    '        Dim cmd As New SqlClient.SqlCommand(strQ, con)
    '        Dim ad As New SqlClient.SqlDataAdapter(cmd)
    '        Dim dt As New DataTable
    '        ad.Fill(dt)
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    Finally
    '        If con.State <> ConnectionState.Closed Then con.Close()
    '    End Try
    'End Function
End Class
