Public Class ContactServices
    Public NotInheritable Class Clients
        Public Shared Function AddNew(ByVal c As Core.Contacts.Client) As Boolean
            c.ID = DataServices.Client_AddNew(c.ClientName, c.LoginUserName, c.LoginPassword, c.AddStatistics)
            Return c.ID > 0
        End Function
        Public Shared Function Update(ByVal c As Core.Contacts.Client) As Boolean
            Return DataServices.Client_Update(c.ID, c.ClientName, c.LoginUserName, c.LoginPassword, c.AddStatistics)
        End Function
        Public Shared Function Delete(ByVal ID As Integer) As Boolean
            Dim strQ As String = "DELETE FROM ClientUser WHERE ClientID=" & ID
            DataHelper.ExecuteNonQuery(strQ)

            strQ = "DELETE FROM Client WHERE ID=" & ID
            DataHelper.ExecuteNonQuery(strQ)
            Return True
        End Function
        Public Shared Function GetClientByID(ByVal ID As Integer) As Contacts.Client
            Dim strQ As String = "SELECT * FROM Client WHERE ID = " & ID
            Dim dt As DataTable = DataHelper.ExecuteQuery(strQ)
            If dt.Rows.Count = 0 Then Return Nothing
            Return DataRowToClient(dt.Rows(0))
        End Function
        Private Shared Function DataRowToClient(ByVal dr As DataRow) As Contacts.Client
            Dim c As New Core.Contacts.Client
            With c
                If Not dr("ID") Is DBNull.Value Then .ID = dr("ID")
                If Not dr("ClientName") Is DBNull.Value Then .ClientName = dr("ClientName")
                If Not dr("LoginUserName") Is DBNull.Value Then .LoginUserName = dr("LoginUserName")
                If Not dr("LoginPassword") Is DBNull.Value Then .LoginPassword = dr("LoginPassword")
                If Not dr("AddStatistics") Is DBNull.Value Then .AddStatistics = dr("AddStatistics")

            End With
            Return c
        End Function

        Public Shared Function ClientUser_add(ByVal ClientID As Integer, ByVal UserID As Integer) As Boolean
            ClientUser_delete(ClientID, UserID)

            Dim strQ As String = "INSERT INTO ClientUser(ClientID, UserID)"
            strQ += "Values (" & ClientID & ", " & UserID & ")"
            Return DataHelper.ExecuteNonQuery(strQ) > 0
        End Function
        Public Shared Function ClientUser_delete(ByVal ClientID As Integer, ByVal UserID As Integer) As Boolean
            Dim strQ As String = "DELETE FROM ClientUser WHERE ClientID=" & ClientID & " AND UserID=" & UserID
            Return DataHelper.ExecuteNonQuery(strQ) > 0
        End Function
        
    End Class
End Class
