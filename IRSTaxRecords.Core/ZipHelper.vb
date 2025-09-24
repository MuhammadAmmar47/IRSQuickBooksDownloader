Imports System
Imports System.IO
Imports ICSharpCode.SharpZipLib.Core
Imports ICSharpCode.SharpZipLib.Zip


Public Class ZipHelper
    Public Shared Sub UnZipFileSingle(ByVal zipPathPath As String, ByVal unZipFilePath As String)
        Dim zip As New ZipFile(zipPathPath)
        Try
            Dim enum1 As IEnumerator = zip.GetEnumerator()
            While enum1.MoveNext
                Dim entry As ZipEntry = enum1.Current
                If entry.IsFile Then

                    'System.IO.File.WriteAllBytes unZipFilePath , entry.
                    Dim stream As IO.Stream = zip.GetInputStream(entry)
                    Dim buffer(entry.Size) As Byte
                    stream.Read(buffer, 0, entry.Size)
                    stream.Close()

                    System.IO.File.WriteAllBytes(unZipFilePath, buffer)
                    Return
                End If
            End While
            Throw New Exception("No file found from the zip " & zipPathPath)
        Catch ex As Exception
            zip.Close()
            Throw
        Finally
            zip.Close()
        End Try


    End Sub
    Public Shared Sub CreateSample(ByVal outPathname As String, ByVal folderName As String)

        Dim fsOut As FileStream = File.Create(outPathname)
        Dim zipStream As New ZipOutputStream(fsOut)

        zipStream.SetLevel(3)   '0-9, 9 being the highest level of compression
        Dim files As String() = Directory.GetFiles(folderName)

        For Each filename As String In files

            Dim fi As New FileInfo(filename)

            Dim entryName As String = IO.Path.GetFileName(filename) ' Removes drive from name and fixes slash direction
            Dim newEntry As New ZipEntry(entryName)
            newEntry.DateTime = fi.LastWriteTime    ' Note the zip format stores 2 second granularity

            ' Specifying the AESKeySize triggers AES encryption. Allowable values are 0 (off), 128 or 256.
            '   newEntry.AESKeySize = 256;

            ' To permit the zip to be unpacked by built-in extractor in WinXP and Server2003, WinZip 8, Java, and other older code,
            ' you need to do one of the following: Specify UseZip64.Off, or set the Size.
            ' If the file may be bigger than 4GB, or you do not need WinXP built-in compatibility, you do not need either,
            ' but the zip will be in Zip64 format which not all utilities can understand.
            '   zipStream.UseZip64 = UseZip64.Off;
            newEntry.Size = fi.Length

            zipStream.PutNextEntry(newEntry)

            ' Zip the file in buffered chunks
            ' the "using" will close the stream even if an exception occurs
            Dim buffer As Byte() = New Byte(4095) {}
            Using streamReader As FileStream = File.OpenRead(filename)
                StreamUtils.Copy(streamReader, zipStream, buffer)
            End Using
            zipStream.CloseEntry()
        Next
        zipStream.IsStreamOwner = True  ' Makes the Close also Close the underlying stream
        zipStream.Close()
    End Sub
    Public Shared Function ZipFiles(ByVal filesList As Generic.List(Of String), ByVal outPathname As String) As Boolean

        Dim fsOut As FileStream = File.Create(outPathname)
        Dim zipStream As New ZipOutputStream(fsOut)

        zipStream.SetLevel(3)   '0-9, 9 being the highest level of compression
        For Each filename As String In filesList

            Dim fi As New FileInfo(filename)

            Dim entryName As String = IO.Path.GetFileName(filename) ' Removes drive from name and fixes slash direction
            Dim newEntry As New ZipEntry(entryName)
            newEntry.DateTime = fi.LastWriteTime    ' Note the zip format stores 2 second granularity

            ' Specifying the AESKeySize triggers AES encryption. Allowable values are 0 (off), 128 or 256.
            '   newEntry.AESKeySize = 256;

            ' To permit the zip to be unpacked by built-in extractor in WinXP and Server2003, WinZip 8, Java, and other older code,
            ' you need to do one of the following: Specify UseZip64.Off, or set the Size.
            ' If the file may be bigger than 4GB, or you do not need WinXP built-in compatibility, you do not need either,
            ' but the zip will be in Zip64 format which not all utilities can understand.
            '   zipStream.UseZip64 = UseZip64.Off;
            newEntry.Size = fi.Length

            zipStream.PutNextEntry(newEntry)

            ' Zip the file in buffered chunks
            ' the "using" will close the stream even if an exception occurs
            Dim buffer As Byte() = New Byte(4095) {}
            Using streamReader As FileStream = File.OpenRead(filename)
                StreamUtils.Copy(streamReader, zipStream, buffer)
            End Using
            zipStream.CloseEntry()
        Next
        zipStream.IsStreamOwner = True  ' Makes the Close also Close the underlying stream
        zipStream.Close()
    End Function
End Class
