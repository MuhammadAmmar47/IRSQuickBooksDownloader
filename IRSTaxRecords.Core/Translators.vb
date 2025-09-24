Imports System
Imports System.Data

Namespace Utilities
    Public Class Translators

        Private Sub New()
        End Sub


        Public Shared Function GetBoolValue(ByVal value As String) As Boolean
            If value.ToUpper.Trim = "TRUE" Then Return True
            If value.ToUpper.Trim = "1" Then Return True
            If value.ToUpper.Trim = "Y" Then Return True
            If value.ToUpper.Trim = "YES" Then Return True
            Return False
        End Function
        Public Shared Function AmazonTimeToLocalTime(ByVal AmazonTime As String) As DateTime
            'If Split(AmazonTime, "-").Length = 4 Then
            '    'eg 2012-03-13T14:04:27-07:00
            '    Return CDate(Mid(AmazonTime, 1, AmazonTime.LastIndexOf("-")))
            'ElseIf AmazonTime.Contains("+") Then
            '    Return CDate(Mid(AmazonTime, 1, AmazonTime.LastIndexOf("+")))
            'End If
            Return CDate(AmazonTime)
        End Function

        ''' <summary>
        ''' Converts a single column DataTable into an Array of Strings
        ''' </summary>
        ''' <param name="dt"></param>
        ''' <returns></returns>
        Public Shared Function DataTableToArray(ByVal dt As DataTable) As String()
            Dim rowCnt As Integer = dt.Rows.Count
            Dim arr(rowCnt - 1) As String

            If dt.Columns.Count > 0 Then
                Dim irow As Integer
                For irow = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(irow)
                    Dim str As String = String.Empty
                    str = dr(0).ToString()
                    arr(irow) = str
                Next irow
            End If

            Return arr
        End Function
        Public Shared Function ExcelFileToDataTable(ByVal filePath As String) As DataTable
            Dim conn As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & filePath & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";")
            Try

                conn.Open()

                Dim cmd As New OleDb.OleDbCommand("Select * FROM [Sheet1$]", conn)
                Dim ad As New OleDb.OleDbDataAdapter(cmd)
                Dim dt As New DataTable

                ad.Fill(dt)

                conn.Close()
                cmd.Dispose()
                Return dt
            Catch ex As Exception
                If conn.State = ConnectionState.Open Then conn.Close()
                Throw
            End Try
            Return Nothing

        End Function
        Public Shared Function GetEnumValueFromName(ByVal enumObject As Type, ByVal name As String) As Int32
            For Each iEnumItem As Object In [Enum].GetValues(enumObject)
                If iEnumItem.ToString.ToLower = name.ToLower Then
                    Return CType(iEnumItem, Int32)
                End If
            Next
            Return Int32.MinValue
        End Function

        Public Shared Function EnumToDataTable(ByVal enumObject As Type, ByVal KeyField As String, ByVal DisplayField As String, Optional ByVal convertToProperCase As Boolean = False, Optional ByVal ExactName As Boolean = False) As DataTable

            Dim oData As DataTable = Nothing
            Dim oRow As DataRow = Nothing

            '-------------------------------------------------------------
            ' Sanity check
            If KeyField.Trim() = String.Empty Then
                KeyField = "KEY"
            End If

            If DisplayField.Trim() = String.Empty Then
                DisplayField = "VALUE"
            End If
            '-------------------------------------------------------------

            '-------------------------------------------------------------
            ' Create the DataTable
            oData = New DataTable

            oData.Columns.Add(KeyField, GetType(System.Int32))

            oData.Columns.Add(DisplayField)
            '-------------------------------------------------------------

            '-------------------------------------------------------------
            ' Add the enum items to the datatable
            For Each iEnumItem As Object In [Enum].GetValues(enumObject)
                oRow = oData.NewRow()
                oRow(KeyField) = CType(iEnumItem, Int32)
                If convertToProperCase Then
                    If ExactName Then
                        oRow(DisplayField) = StrConv(iEnumItem.ToString(), VbStrConv.ProperCase)
                    Else
                        oRow(DisplayField) = StrConv(Replace(iEnumItem.ToString(), "_", " "), VbStrConv.ProperCase)
                    End If

                Else
                    If ExactName Then
                        oRow(DisplayField) = iEnumItem.ToString()
                    Else
                        oRow(DisplayField) = Replace(iEnumItem.ToString(), "_", " ")
                    End If

                End If

                oData.Rows.Add(oRow)
            Next
            '-------------------------------------------------------------

            Return oData

        End Function

        Public Shared Function EnumValueToString(ByVal enumObject As Type, ByVal enumValue As Integer) As String
            For Each iEnumItem As Object In [Enum].GetValues(enumObject)
                If iEnumItem = enumValue Then
                    Return iEnumItem.ToString
                End If
            Next
            Return ""
        End Function
        Public Shared Function DataTableToCSVFile(ByVal dt As DataTable, ByVal seperator As CSVFileSeperator, ByVal headerOnly As Boolean, ByVal addQuotesToValues As Boolean, ByVal excludeHeader As Boolean) As String
            Dim outStr As New System.Text.StringBuilder

            Dim delimator As String = ""
            Select Case seperator
                Case CSVFileSeperator.Comma : delimator = ","
                Case CSVFileSeperator.TAB : delimator = vbTab
                Case CSVFileSeperator.Pipe : delimator = "|"
            End Select
            If Not excludeHeader Then
                For temp As Integer = 0 To dt.Columns.Count - 1
                    outStr.Append(dt.Columns(temp).ColumnName)
                    If temp < dt.Columns.Count - 1 Then
                        outStr.Append(delimator)
                    End If
                Next
                If headerOnly Then
                    Return outStr.ToString
                End If

                outStr.Append(vbCrLf)

            End If

            For rowCount As Integer = 0 To dt.Rows.Count - 1
                Dim thisRow As String = ""
                For colCount As Integer = 0 To dt.Columns.Count - 1
                    If dt.Rows(rowCount)(colCount) Is DBNull.Value Then
                        If addQuotesToValues Then
                            thisRow += Chr(34) & Chr(34)
                        Else
                            thisRow += ""
                        End If

                    Else
                        If addQuotesToValues Then
                            thisRow += Chr(34) & dt.Rows(rowCount)(colCount).ToString.Replace(Chr(34), "") & Chr(34)
                        Else
                            If dt.Rows(rowCount)(colCount).ToString.Contains(delimator) Then
                                thisRow += Chr(34) & dt.Rows(rowCount)(colCount).ToString & Chr(34)
                            Else
                                thisRow += dt.Rows(rowCount)(colCount).ToString
                            End If

                        End If

                    End If

                    If colCount < dt.Columns.Count - 1 Then
                        thisRow += delimator
                    End If
                Next


                outStr.Append(thisRow & vbCrLf)

            Next

            Return outStr.ToString
        End Function
        Public Shared Function CSVFileToDataTable(ByVal filePath As String, ByVal seperator As CSVFileSeperator, ByVal SkipRows As Integer) As DataTable
            'Dim wholeContent As String = System.IO.File.ReadAllText(filePath)
            'If wholeContent.Contains(ChrW(&H1A)) Then
            '    Return CSVFileToDataTable(filePath, seperator, SkipRows, System.Text.Encoding.UTF7)
            'Else
            '    Return CSVFileToDataTable(filePath, seperator, SkipRows, System.Text.Encoding.Default)
            'End If
            Return CSVFileToDataTable(filePath, seperator, SkipRows, Nothing)
        End Function
        Public Shared Function CSVFileToDataTable(ByVal filePath As String, ByVal seperator As CSVFileSeperator, ByVal SkipRows As Integer, ByVal useEncoding As System.Text.Encoding) As DataTable
            'Dim reader As New IO.StreamReader(filePath, System.Text.Encoding.Default, True)
            Dim reader As IO.StreamReader = Nothing  'href.Utils.EncodingTools.OpenTextFile(filePath)
            If useEncoding Is Nothing Then
                reader = New System.IO.StreamReader(filePath)
            Else
                reader = New System.IO.StreamReader(filePath, useEncoding)
            End If
            Dim dt As New DataTable
            Dim delimator As String = ""
            Select Case seperator
                Case CSVFileSeperator.Comma : delimator = ","
                Case CSVFileSeperator.TAB : delimator = vbTab
            End Select
            Try
                Dim colstoRemove As New Generic.List(Of String)
                For temp As Integer = 1 To SkipRows
                    Dim wasteLine As String = reader.ReadLine()
                    Trace.WriteLine("Skipping line with text " & wasteLine)
                Next
                Dim headerLine As String = reader.ReadLine
                If headerLine.StartsWith("##") Then headerLine = reader.ReadLine()

                While headerLine.Trim = ""
                    headerLine = reader.ReadLine
                    If reader.EndOfStream Then Exit While
                End While

                For Each str As String In Split(headerLine, delimator)
                    Dim colName As String = RemoveQuotesFromValue(str.Trim)

                    'Column already existed.
                    Dim colRenamecount As String = ""
                    While dt.Columns.Contains(colName & colRenamecount)
                        colRenamecount = Val(colRenamecount) + 1
                    End While
                    If colRenamecount <> "" Then colstoRemove.Add(colName & colRenamecount)
                    dt.Columns.Add(colName & colRenamecount)
                Next
                Dim lineText As String = reader.ReadLine
                If dt.Columns.Count = 1 Then
                    Dim dr As DataRow = dt.NewRow
                    If lineText Is Nothing Then
                        dr(0) = ""
                    Else
                        Dim values() As String = Split(lineText.Trim, delimator)
                        dr(0) = values(0).Trim
                    End If
                    dt.Rows.Add(dr)
                Else
                    While lineText Is Nothing = False
                        If lineText.Trim <> "" Then

                            Dim dr As DataRow = dt.NewRow
                            Dim values() As String = Split(lineText, delimator)
                            If values.Length <> dt.Columns.Count Then
                                'There is some issue with the "," or somethng like that. Check the Quotes and then parse it...
                                Dim startIndex As Integer = 0
                                Dim endIndex As Integer = lineText.IndexOf(delimator)
                                Dim colCounter As Integer = 0
                                While endIndex > 0

                                    Dim content As String = Mid(lineText, startIndex + 1, endIndex - startIndex)
                                    If content.StartsWith("""") Then
                                        endIndex = lineText.IndexOf("""", startIndex + 2) + 1
                                        content = Mid(lineText, startIndex + 1, endIndex - startIndex)
                                        content = RemoveQuotesFromValue(content)
                                    End If
                                    dr(colCounter) = content
                                    colCounter += 1
                                    startIndex = endIndex + 1

                                    endIndex = lineText.IndexOf(delimator, startIndex)
                                    If colCounter >= dt.Columns.Count - 1 Then Exit While
                                End While
                            Else
                                For temp As Integer = 0 To values.Length - 1
                                    dr(temp) = RemoveQuotesFromValue(values(temp).Trim)
                                    If temp = dt.Columns.Count - 1 Then Exit For
                                Next
                            End If


                            dt.Rows.Add(dr)
                        End If
                        lineText = reader.ReadLine
                    End While
                End If

                For Each colName As String In colstoRemove
                    dt.Columns.Remove(colName)
                Next
            Catch ex As Exception
                Throw ex
            Finally
                reader.Close()
            End Try

            Return dt
        End Function
        'Public Shared Function CSVFileToDataTableUsingJet(ByVal filePath As String, ByVal seperator As CSVFileSeperator) As DataTable
        '    Dim connString As String = ""
        '    Select Case seperator
        '        Case CSVFileSeperator.Comma
        '            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & System.IO.Path.GetDirectoryName(filePath) & ";Extended Properties=""text;HDR=YES;FMT=Delimited"""
        '        Case CSVFileSeperator.TAB
        '            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & System.IO.Path.GetDirectoryName(filePath) & ";Extended Properties=""text;HDR=YES;FMT=TabDelimited"""
        '        Case Else
        '            Throw New Exception("This format is not yet supported")
        '    End Select
        '    Dim conn As New System.Data.OleDb.OleDbConnection(connString)
        '    conn.Open()
        '    Dim strQ As String = "SELECT * from [" & System.IO.Path.GetFileName(filePath) & "]"
        '    Dim ad As New OleDb.OleDbDataAdapter(strQ, conn)
        '    Dim dt As New DataTable
        '    Try
        '        ad.Fill(dt)
        '    Catch ex As Exception
        '        Throw
        '    End Try
        '    conn.Close()

        '    Return dt
        'End Function
        Private Shared Function RemoveQuotesFromValue(ByVal value As String) As String
            If value.StartsWith(Chr(34)) AndAlso value.EndsWith(Chr(34)) Then
                value = Mid(value, 2)
                If value.Length = 0 Then Return value
                value = Mid(value, 1, value.Length - 1)
            End If
            Return value
        End Function
        Public Shared Function CSVFileToDataTable(ByVal filePath As String, ByVal seperator As CSVFileSeperator) As DataTable
            Return CSVFileToDataTable(filePath, seperator, 0)
        End Function

        Public Enum CSVFileSeperator
            Comma = 0
            TAB = 1
            Pipe = 2
        End Enum

        Public Shared Function CloneObject(ByVal obj As Object) As Object
            If obj Is Nothing Then Return obj

            Dim serialized As String = ""
            Try
                If TypeOf obj Is DataTable AndAlso CType(obj, DataTable).TableName = "" Then Return obj 'Can't serializer datatable without datatable name
                serialized = Serialize(obj)
                Return DeSerializeFromXML(serialized, obj.GetType)
            Catch ex As Exception
                Trace.WriteLine("Failed to Serialize/Deserialize Cache object " & obj.ToString & vbCrLf & ex.Message & vbCrLf & "SErialized String was: " & serialized & vbCrLf & ex.StackTrace)
                Return obj
            End Try

            'Using memStream As System.IO.MemoryStream = New System.IO.MemoryStream()
            '    Dim binaryFormatter As Runtime.Serialization.Formatters.Binary.BinaryFormatter = New Runtime.Serialization.Formatters.Binary.BinaryFormatter(Nothing, New Runtime.Serialization.StreamingContext(Runtime.Serialization.StreamingContextStates.Clone))
            '    binaryFormatter.Serialize(memStream, obj)
            '    memStream.Seek(0, IO.SeekOrigin.Begin)
            '    Return binaryFormatter.Deserialize(memStream)
            'End Using
        End Function
        Public Shared Function Serialize(ByVal obj As Object) As String
            Dim x As New System.Xml.Serialization.XmlSerializer(obj.GetType)
            Dim sb As New System.Text.StringBuilder
            Dim t As New IO.StringWriter(sb)
            x.Serialize(t, obj)

            Return sb.ToString
        End Function
        Public Shared Function DeSerializeFromXML(ByVal xml As String, ByVal type As Type) As Object
            Dim s As New IO.StringReader(xml)
            Dim x As New System.Xml.Serialization.XmlSerializer(type)
            Dim ls As Object = x.Deserialize(s)
            Return ls
        End Function
        Public Shared Function GetNextDayOfWeekDate(ByVal dayOfWeekRequired As DayOfWeek) As Date
            Dim today As DateTime = Now
            While True
                If today.DayOfWeek = dayOfWeekRequired Then Return today
                today = today.AddDays(1)
            End While
        End Function


        Public Shared Function GenericArrayToString(ByVal array As Generic.List(Of Integer)) As String
            Dim outSTr As String = ""
            For Each item As Integer In array
                outSTr += item & ","
            Next
            If outSTr.EndsWith(",") Then outSTr = Mid(outSTr, 1, outSTr.Length - 1)
            Return outSTr
        End Function
        Public Shared Function GenericArrayToString(ByVal array As Generic.List(Of String)) As String
            Dim outSTr As String = ""
            For Each item As String In array
                outSTr += item & ","
            Next
            If outSTr.EndsWith(",") Then outSTr = Mid(outSTr, 1, outSTr.Length - 1)
            Return outSTr
        End Function

        Public Shared Function StringToGenericArray(ByVal str As String) As Generic.List(Of String)
            Dim list As New Generic.List(Of String)
            For Each item As String In Split(str, ",")
                If item.Trim <> "" Then list.Add(item)
            Next
            Return list
        End Function
        Public Shared Function StringToGenericArrayInt(ByVal str As String) As Generic.List(Of Integer)
            Dim list As New Generic.List(Of Integer)
            For Each item As String In Split(str, ",")
                If item.Trim <> "" Then list.Add(item)
            Next
            Return list
        End Function
        Public Shared Function CloneDataRow(ByVal row As DataRow) As DataRow
            Dim newRow As DataRow = row.Table.NewRow
            For Each col As DataColumn In row.Table.Columns
                newRow(col.ColumnName) = row(col.ColumnName)
            Next
            Return newRow
        End Function

        Public Structure NameType
            Public Firstname As String
            Public LastName As String
            Public Sub New(ByVal name As String)
                Firstname = ""
                LastName = ""

                If name.Trim = "" Then Return
                Dim values() As String = Split(name.Trim, " ")

                Dim noSet As Integer = 0
                For temp As Integer = 0 To values.Length - 1
                    If values(temp).Trim <> "" Then
                        Select Case noSet
                            Case 0
                                Firstname = values(temp).Trim
                            Case Else
                                LastName += values(temp).Trim & " "
                        End Select
                        noSet += 1
                    End If
                Next
                LastName = LastName.Trim
            End Sub
        End Structure
    End Class
End Namespace

