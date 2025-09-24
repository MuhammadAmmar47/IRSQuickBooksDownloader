Imports System.IO
Imports Microsoft.Win32
Imports System.Reflection
Imports OrderPad.IRSTaxServices

Public Class frmProcessing

    Dim CommandLineArgs As System.Collections.ObjectModel.ReadOnlyCollection(Of String) = My.Application.CommandLineArgs
    Dim objSoapClient As New OrderPad.IRSTaxServices.IRSTaxServices()
    Dim objAuthHeader As New AuthHeader()
    Dim BalloonOpen As Boolean
    Dim UploadingThread As New Threading.Thread(AddressOf UploadFile)
    Dim InProcess As Boolean = False

    Private Function GetIconPath()

        Dim FilePath As String = Path.GetDirectoryName(Application.ExecutablePath) & "\OrderPad.ico"

        Try
            Try
                If System.IO.File.Exists(FilePath) Then
                    System.IO.File.Delete(FilePath)
                End If
            Catch InnerExp As Exception

            End Try

            Dim IconResource As System.IO.Stream = GetType(frmProcessing).Assembly.GetManifestResourceStream("OrderPad.OrderPad.ico")
            Dim IconStream As Stream = System.IO.File.OpenWrite(FilePath)
            Dim index As Integer = 0
            For index = 0 To IconResource.Length - 1
                IconStream.WriteByte(IconResource.ReadByte())
            Next

            IconStream.Close()

            Return FilePath


        Catch ex As Exception
            Return String.Empty
        End Try

    End Function

    Private Sub CreateShortcut() ' Commented as we want a single executable without any DLL.
        'Try
        '    Dim WshShell As WshShellClass = New WshShellClass
        '    Dim OrderPadShortcut As IWshRuntimeLibrary.IWshShortcut = Nothing
        '    Dim SendToFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.SendTo)

        '    Dim OrderPadLnk As String = SendToFolder & "\OrderPad.lnk"

        '    If System.IO.File.Exists(OrderPadLnk) Then
        '        Return
        '    End If

        '    OrderPadShortcut = CType(WshShell.CreateShortcut(OrderPadLnk), IWshRuntimeLibrary.IWshShortcut)
        '    OrderPadShortcut.TargetPath = Path.GetDirectoryName(Application.ExecutablePath) & "\OrderPad.exe"
        '    OrderPadShortcut.IconLocation = GetIconPath()


        '    OrderPadShortcut.Save()

        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub UploadFile(Optional FileName As String = "")
        Try

            InProcess = True
            Dim TotalFiles As Integer = 0
            Dim Successful As Integer = 0
            Dim Failed As Integer = 0
            Dim Skipped As Integer = 0

            Dim file As FileInfo
            'Dim encoding As New System.Text.UTF8Encoding()
            Dim failureFiles As New Generic.List(Of String)
            Dim successFiles As New Generic.List(Of String)
            Dim skippedfiles As New Generic.List(Of String)
            Dim theItemsToUpload As New List(Of String)
            If FileName IsNot Nothing AndAlso FileName.Trim <> "" Then
                theItemsToUpload.Add(FileName)
            Else
                For temp As Integer = 0 To CommandLineArgs.Count - 1
                    theItemsToUpload.Add(CommandLineArgs(temp))
                Next
            End If
            For i As Integer = 0 To theItemsToUpload.Count - 1

                file = New FileInfo(theItemsToUpload(i))
                Try
                    TotalFiles = TotalFiles + 1

                    If file.Extension = ".pdf" Then

                        lblProcessing.Text = $"Upload file {file.Name} ({i + 1} of {theItemsToUpload.Count})"

                        UploadingNotify.BalloonTipText = "Uploading file: " & file.Name & " (" & i + 1 & " of " & theItemsToUpload.Count & ")"
                        CancelUploadingToolStripMenuItem.Enabled = True
                        UploadingNotify.ShowBalloonTip(500)
                        Threading.Thread.Sleep(5000)
                        'Dim result As Integer = objSoapClient.UploadFile(theItemsToUpload(i), encoding.GetBytes(file.Length))
                        objSoapClient.AuthHeaderValue = New AuthHeader With {.Password = My.Settings.Password, .UserName = My.Settings.Username}
                        Dim result As Integer = objSoapClient.UploadFile(theItemsToUpload(i), System.IO.File.ReadAllBytes(file.FullName))
                        Successful = Successful + 1
                        successFiles.Add(file.Name)
                    Else
                        Skipped = Skipped + 1
                        skippedfiles.Add(file.Name)
                    End If
                Catch ex As Exception
                    Failed = Failed + 1
                    failureFiles.Add(file.Name)
                End Try
                Me.ProgressBar1.Value = (i + 1) / theItemsToUpload.Count * 100
            Next


            InProcess = False
            Dim Message As String = $"Uploaded {Successful} out of {TotalFiles}{vbCrLf}"
            If successFiles.Count > 0 Then Message += $"Successfully uploaded {GetFileNames(successFiles)}"
            If failureFiles.Count > 0 Then Message += $"{vbCrLf}Failed files are {GetFileNames(failureFiles)}"
            If skippedfiles.Count > 0 Then Message += $"{vbCrLf}Skipped files are {GetFileNames(skippedfiles)}"

            'Dim Message As String = "Uploading process has been completed." + System.Environment.NewLine + System.Environment.NewLine
            'Message = Message + "Total Files: " + TotalFiles.ToString() + System.Environment.NewLine

            'Message = Message + "Successful: " + Successful.ToString() + System.Environment.NewLine
            'If Successful > 0 Then Message += "Successful files are " & System.Environment.NewLine & GetFileNames(successFiles)

            'Message = Message + "Failure: " + Failed.ToString() + System.Environment.NewLine
            'If Failed > 0 Then Message += "Failure files are " & System.Environment.NewLine & GetFileNames(failureFiles)

            'Message = Message + "Skipped: " + Skipped.ToString()
            'If Skipped > 0 Then Message += "Skipped files are " & System.Environment.NewLine & GetFileNames(skippedfiles)

            Me.txtLog.Text = Message
            MsgBox(Message, Title:="OrderPad Progress")
            CancelUploadingToolStripMenuItem.Text = "Exit"

            'UploadingNotify.BalloonTipText = Message
            'UploadingNotify.ShowBalloonTip(1000)
            'Threading.Thread.Sleep(500)
            Me.Close()
            Application.Exit()
        Catch ex As Exception

        End Try
    End Sub
    Private Function GetFileNames(ByVal list As Generic.List(Of String)) As String
        Dim outMsg As String = ""
        For Each fileName As String In list
            outMsg += vbTab & fileName & System.Environment.NewLine
        Next
        Return outMsg
    End Function
    Private Sub CancelUploadingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelUploadingToolStripMenuItem.Click

        Try
            If InProcess Then
                Dim Result As DialogResult = MessageBox.Show("Are you sure to cancel upload process?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If Result = Windows.Forms.DialogResult.No Then
                    Return
                End If

            End If
            UploadingThread.Abort()
        Catch ex As Exception

        End Try
        Try
            'objSoapClient.Close()
            Application.Exit()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub UploadingNotify_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles UploadingNotify.MouseMove
        Try
            If Not BalloonOpen Then
                UploadingNotify.ShowBalloonTip(5000)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub UploadingNotify_BalloonTipShown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UploadingNotify.BalloonTipShown
        BalloonOpen = True
    End Sub

    Private Sub UploadingNotify_BalloonTipClosed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UploadingNotify.BalloonTipClosed
        BalloonOpen = False
    End Sub

    Private Function ValidateFiles() As Boolean

        Try

            For index As Integer = 0 To CommandLineArgs.Count - 1

                Try
                    Dim objFileInfo As FileInfo = New FileInfo(CommandLineArgs(index))

                    If objFileInfo.Extension = ".pdf" Then
                        Return True
                    End If
                Catch ex As Exception

                End Try
            Next
            Catch ex As Exception

        End Try

        Return False
    End Function

    Private Sub frmProcessing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim CommandLineArgs As New List(Of String)
        'CommandLineArgs.Add("C:\Users\Sameers\Desktop\IMAG0193.jpg")
        'CommandLineArgs.Add("C:\Users\Sameers\Desktop\18124860353.pdf")
        'Me.CommandLineArgs = New System.Collections.ObjectModel.ReadOnlyCollection(Of String)(CommandLineArgs)
        Try
            objSoapClient.Url = ApplicationSettings.WebServiceURL
            'For testing, uncomment below line!
            'UploadFile("C:\Users\Sameers\Desktop\Rental Agreement 401.pdf")

            CreateShortcut()

            Me.Hide()
            Me.NotifyMenu.Visible = False

            If My.Settings.Username = String.Empty Or CommandLineArgs.Count = 0 Then
                Dim objLogin As New frmLogin
                Dim result As DialogResult = objLogin.ShowDialog()
                If result = Windows.Forms.DialogResult.Cancel Then
                    Me.Close()
                    End
                    Return
                End If
            End If

            If CommandLineArgs.Count > 0 Then
                If Not ValidateFiles() Then
                    MessageBox.Show("Invalid File Type. Only 'PDF' files are allowed to upload. Please verify.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Application.Exit()
                    Return
                End If

                objAuthHeader.UserName = My.Settings.Username
                objAuthHeader.Password = My.Settings.Password

                UploadingNotify.BalloonTipText = "Starting files uploading process...."

                Try
                    objSoapClient.AuthHeaderValue = objAuthHeader
                    objSoapClient.Url = ApplicationSettings.WebServiceURL
                    objSoapClient.Login()

                    UploadingThread.Start()
                Catch ex As Exception
                    MessageBox.Show("Unable to upload files. Reason:" + ex.Message + System.Environment.NewLine + "Please try again later.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Application.Exit()
                End Try
            Else
                Application.Exit()
                End
                Return
            End If

        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub

End Class

