Imports OrderPad.IRSTaxServices


Public Class frmLogin
    Private ReadOnly Property TemporaryFolderPath() As String
        Get
            Dim tempFolderPath As String = System.IO.Path.GetTempPath
            If Not tempFolderPath.EndsWith("\") Then tempFolderPath += "\"
            tempFolderPath += "OrderPad\"
            If Not System.IO.Directory.Exists(tempFolderPath) Then System.IO.Directory.CreateDirectory(tempFolderPath)
            Return tempFolderPath
        End Get
    End Property
    Private Sub SelfDestruct(ByVal fileToCopy As String)

        Dim UpdatedFilePath = """" & fileToCopy & """"

        Dim CurrentExecutableFilePathWithoutQuotes As String = Application.ExecutablePath
        Dim CurrentExecutableFilePathWithQuotes As String = """" & CurrentExecutableFilePathWithoutQuotes & """"
        Dim ProcessName As String = System.IO.Path.GetFileName(CurrentExecutableFilePathWithoutQuotes)



        Dim BatchFilePath As String = TemporaryFolderPath & DateTime.Now.Ticks.ToString() + ".bat"

        Dim sWriter As System.IO.StreamWriter = Nothing
        sWriter = New System.IO.StreamWriter(BatchFilePath)

        sWriter.WriteLine("taskkill /im " + ProcessName)

        sWriter.WriteLine("echo f | xcopy /f /y " + UpdatedFilePath + " " + CurrentExecutableFilePathWithQuotes)

        Dim objFileInfo As System.IO.FileInfo = Nothing
        objFileInfo = New System.IO.FileInfo(System.IO.Path.GetDirectoryName(Application.ExecutablePath))

        Dim DirPath As String = """" + objFileInfo.FullName + """"

        sWriter.WriteLine("start " + DirPath + "  " + ProcessName)



        sWriter.Close()

        System.Diagnostics.Process.Start(BatchFilePath)
        Me.Close()
        End


    End Sub
    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Dim objITaxServices As New OrderPad.IRSTaxServices.IRSTaxServices()
            objITaxServices.Url = ApplicationSettings.WebServiceURL
            Dim currentVersion As String = objITaxServices.OrderPadCurrentVersion()
            Dim version As String = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString
            If version <> currentVersion Then
                If MsgBox("There is a newer (" & currentVersion & ") version available. Do you want to upgrade?", MsgBoxStyle.YesNo, "OrderPad - Newer Version available") = MsgBoxResult.Yes Then
                    Trace.WriteLine("Upgrading to newer version of " & version)

                    Dim bytes() As Byte = objITaxServices.OrderPadGetLatestVersion


                    Dim savePath As String = TemporaryFolderPath & "OrderPad.exe"
                    System.IO.File.WriteAllBytes(savePath, bytes)
                    SelfDestruct(savePath)
                    Me.Close()
                    End
                    Return
                End If
            End If
        Catch ex As Exception
            Trace.WriteLine("Failed to check version. " & ex.Message & vbCrLf & ex.StackTrace)
        End Try
        Try
            If My.Settings.Username <> String.Empty Then
                txtUsername.Text = My.Settings.Username
                txtPassword.Text = My.Settings.Password
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub lblExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblExit.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
            Application.Exit()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lblLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblLogin.Click
        Try

            If txtUsername.Text.Trim() = String.Empty Then
                MessageBox.Show("Please provide username.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtUsername.Focus()
                Return
            End If

            If txtPassword.Text.Trim() = String.Empty Then
                MessageBox.Show("Please provide password.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtPassword.Focus()
                Return
            End If


            Cursor = Cursors.WaitCursor
            Dim objAuthHeader As New OrderPad.IRSTaxServices.AuthHeader
            objAuthHeader.UserName = txtUsername.Text
            objAuthHeader.Password = txtPassword.Text



            Dim objITaxServices As New OrderPad.IRSTaxServices.IRSTaxServices()
            objITaxServices.Url = ApplicationSettings.WebServiceURL
            objITaxServices.AuthHeaderValue = objAuthHeader
            Dim objCustomer As Customer = objITaxServices.Login()



            My.Settings.Username = txtUsername.Text
            My.Settings.Password = txtPassword.Text
            My.Settings.Save()

            MessageBox.Show("Credentials have been Validated successfully! You are now ready to upload files.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception

            MessageBox.Show("Credentials failed to validate." & System.Environment.NewLine & "Reason:" & ex.Message, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Dim str As String = ex.Message
        End Try

        Cursor = Cursors.Default
    End Sub
End Class