Imports System.IO
Namespace Exceptions
    Public Class WebPageTraceListener
        Inherits TextWriterTraceListener

        Public Property FilePath As String

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal stream As Stream)
            MyBase.New(stream)
        End Sub

        Public Sub New(ByVal fileName As String)
            MyBase.New(fileName)
            Dim server = System.Web.HttpContext.Current?.Server
            If fileName.StartsWith("/") Then
                If server Is Nothing Then
                    Dim fileNameOnly = System.IO.Path.GetFileName(fileName)
                    FilePath = $"C:\traces\{fileNameOnly}"
                Else
                    FilePath = server.MapPath(fileName)
                End If
            Else
                FilePath = fileName
            End If


            Dim fInfo As New FileInfo(FilePath)
            If Not Directory.Exists(fInfo.Directory.FullName) Then Directory.CreateDirectory(fInfo.Directory.FullName)
        End Sub

        Public Sub New(ByVal writer As TextWriter)
            MyBase.New(writer)
        End Sub

        Public Sub New(ByVal stream As Stream, ByVal name As String)
            MyBase.New(stream, name)
        End Sub

        Public Sub New(ByVal path As String, ByVal name As String)
            MyBase.New(path, name)
        End Sub

        Public Sub New(ByVal writer As TextWriter, ByVal name As String)
            MyBase.New(writer, name)
        End Sub

        Public Overloads Overrides Sub WriteLine(ByVal message As String)
            SyncLock Me
                Try
                    message = message.Replace(vbCrLf, "<br />")
                    message = message.Replace(vbTab, " ")

                    If FilePath = "" Then Return
                    If Not File.Exists(FilePath) Then
                        File.WriteAllText(FilePath, "TimeOfLog" & vbTab & "UserID" & vbTab & "PageName" & vbTab & "Message" & vbCrLf)
                    End If


                    File.AppendAllText(FilePath, DateTime.Now & vbTab & message & vbCrLf)

                Catch ex As Exception
                    'Failed to write into file....
                End Try

                ' We must *not* attempt to access the database/SQL if we are logging such an issue in the first place
                ' Otherwise we will end up in a stack overflow state/infinite recusion
                TrimLogFile()

            End SyncLock
        End Sub
        Public Overrides Sub Write(ByVal message As String)
            If FilePath.Trim = "" Then Return
            SyncLock Me
                Try
                    Using mWriter As New System.IO.StreamWriter(FilePath, True)
                        mWriter.Write(message)
                        mWriter.Close()
                    End Using

                Catch ex As Exception
                End Try
            End SyncLock
        End Sub
        Public Overrides Sub WriteLine(ByVal message As String, ByVal category As String)
            If category Is Nothing Then
                WriteLine(message)
            Else
                WriteLine("[" & category.ToString & "] " & message)
            End If
        End Sub

        Public Sub TrimLogFile()
            If FilePath Is Nothing OrElse FilePath.Trim = "" Then Return
            'If Not File.Exists(FilePath) Then Return 'Lets make sure file exists...

            Dim fInfo As New FileInfo(FilePath)
            If MAX_LOG_FILE_SIZE = 0 Then
                Try
                    MAX_LOG_FILE_SIZE = 4 * 2048        'From KB to Bytes
                Catch ex As Exception
                End Try

            End If
            If Not Directory.Exists(fInfo.Directory.FullName) Then
                'Directory didn't existed (first write maybe). Create directory and return.
                Directory.CreateDirectory(fInfo.Directory.FullName)
                Return
            End If

            If fInfo IsNot Nothing AndAlso fInfo.Length > MAX_LOG_FILE_SIZE Then
                Dim name As String = Path.GetFileNameWithoutExtension(fInfo.FullName)
                Dim ext As String = Path.GetExtension(fInfo.FullName)
                Dim newFileName As String = fInfo.Name & "-" & Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & "-" & Now.Hour.ToString & "-" & Now.Minute.ToString & "-" & Now.Second.ToString
                newFileName += ext

                Dim newPath As String = fInfo.Directory.FullName
                If Not newPath.EndsWith("\") Then newPath += "\"
                newPath += newFileName
                Try
                    fInfo.MoveTo(newPath)
                Catch ex As Exception
                End Try
                Try
                    DeleteOldFiles()
                Catch ex As Exception
                End Try
            End If
        End Sub
        Private Sub DeleteOldFiles()

            Dim fInfo As New FileInfo(FilePath)
            Dim filterName As String = Path.GetFileNameWithoutExtension(fInfo.FullName)
            Dim files() As String = Directory.GetFiles(fInfo.Directory.FullName, "*" & filterName & "*")
            For Each File As String In files
                fInfo = New FileInfo(File)
                If fInfo.Extension.ToLower = ".txt" Then
                    If fInfo.LastWriteTime.AddDays(30) < Now Then
                        'Delete this file
                        Try
                            System.IO.File.Delete(fInfo.FullName)
                        Catch ex As Exception

                        End Try
                    End If
                End If
            Next
        End Sub
        Private MAX_LOG_FILE_SIZE As Long = 0
    End Class
End Namespace