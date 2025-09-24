Imports IRSTaxRecords.QBHelper.QBWebService
'Imports IRSTaxRecords.QBHelper.QBServiceLocal
Imports System.Windows.Forms


Public Class DownloadCustomers
    Public Event DownloadStatus(ByVal total As Integer, ByVal totalDone As Integer)
    Public Event DownloadFinished(ByVal totalSuccess As Integer)
    Public Event [Error](ByVal ex As String)
    Private _Cancel As Boolean = False

    Public Sub StartDownloading()

        Dim totalNew As Integer = 0
        Try
            WebServiceHelper.Service.ClearAllErrorsBeforeDownloading()
        Catch ex As Exception
            RaiseEvent Error(ex.Message)
            Return
        End Try
        Try
            Application.DoEvents()
            totalNew = WebServiceHelper.Service.GetTotalNewCustomers()
            Application.DoEvents()
        Catch ex As Exception
            RaiseEvent Error(ex.Message)
            Return
        End Try

        If totalNew = 0 Then
            Application.DoEvents()
            RaiseEvent DownloadFinished(0)
            Application.DoEvents()
            Return
        End If

        Application.DoEvents()
        Dim totalSuccess As Integer = 0
        Dim totalDone As Integer = 0
        Dim qb As New QuickBooksProcessor()
        Try
            If Not qb.Connect Then
                RaiseEvent Error((qb.LastError))
                Return
            End If
        Catch ex As Exception
            RaiseEvent Error(ex.Message)
            Return
        End Try
        _Cancel = False
        While True
            Try
                Application.DoEvents()
                Dim c As Customer = WebServiceHelper.Service.GetNextNewCustomer
                Application.DoEvents()
                If c Is Nothing Then Exit While
                Dim ListID As String = ""

                If qb.AddCustomer(c, ListID) Then
                    Application.DoEvents()
                    c.ListId = ListID
                    'Return this custom back to service and update service record.
                    totalSuccess += 1
                Else
                    c.IsError = True
                    Application.DoEvents()
                    RaiseEvent Error((qb.LastError))
                    Application.DoEvents()
                End If
                Application.DoEvents()
                WebServiceHelper.Service.UpdateCustomer(c)
                Application.DoEvents()
            Catch ex As Exception
                RaiseEvent Error(ex.Message)
            End Try
            totalDone += 1
            Application.DoEvents()
            RaiseEvent DownloadStatus(totalNew, totalDone)
            If _Cancel Then Exit While
        End While

        RaiseEvent DownloadFinished(totalSuccess)

        qb.Disconnect()
        qb = Nothing


    End Sub
    Public Sub StopDownloading()
        _Cancel = True
    End Sub
End Class
