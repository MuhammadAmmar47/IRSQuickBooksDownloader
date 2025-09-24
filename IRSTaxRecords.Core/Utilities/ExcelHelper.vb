Imports System.Data.OleDb
Imports Syncfusion.XlsIO

'Excel specifications and limits
'http://office.microsoft.com/en-us/excel-help/excel-specifications-and-limits-HP010073849.aspx
Namespace Utilities
    Public Class ExcelHelper
        Public Const ExcelVersionForWrwiting As ExcelVersion = Syncfusion.XlsIO.ExcelVersion.Excel97to2003

        Public Shared Function UpdateRowInExcel(ByVal excelPath As String, ByVal Query As String) As Boolean
            Dim connString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & excelPath & ";Extended Properties=""Excel 8.0;HDR=YES;"""

            Dim conn As OleDb.OleDbConnection = Nothing
            Try
                conn = New OleDbConnection(connString)
                conn.Open()
            Catch ex As Exception
                Throw New Exception("Failed to open connection to file " & excelPath & ", " & ex.Message)
            End Try


            Dim totalAffected As Integer = 0

            Dim cmd As New OleDbCommand(Query, conn)


            Try
                totalAffected += cmd.ExecuteNonQuery
            Catch ex As Exception
                Trace.WriteLine("SavedataTableToExcelSheet: Failed to execute " & Query & ", " & ex.Message)
            End Try

            conn.Close()
            Return totalAffected
        End Function
        Public Shared Function GetAsDataSet(ByVal excelPath As String) As DataSet
            If Not System.IO.File.Exists(excelPath) Then Throw New Exception("Source file " & excelPath & " doesn't exists. Can't convert to data table.")
            Dim excelEngine As ExcelEngine = Nothing
            Dim application As IApplication = Nothing
            Try
                excelEngine = New ExcelEngine
                application = excelEngine.Excel

                Dim workbook As IWorkbook = Nothing
                If excelPath.ToLower.EndsWith(".xlsx") Then
                    workbook = application.Workbooks.Open(excelPath, Syncfusion.XlsIO.ExcelVersion.Excel2007)
                Else
                    workbook = application.Workbooks.Open(excelPath, Syncfusion.XlsIO.ExcelVersion.Excel97to2003)
                End If
                Dim sheetIndex As Integer = 0

                Dim ds As New DataSet
                For Each tempSheet As IWorksheet In workbook.Worksheets
                    If tempSheet.Rows.Length = 0 OrElse tempSheet.Columns.Length = 0 Then Exit For

                    Dim dt As DataTable = tempSheet.ExportDataTable(1, 1, tempSheet.Rows.Length - 1, tempSheet.Columns.Length, ExcelExportDataTableOptions.ColumnNames)
                    dt.TableName = "Table" & sheetIndex
                    ds.Tables.Add(dt)
                    sheetIndex += 1
                Next


                workbook.Close()

                Return ds
            Catch ex As Exception
                Throw
            Finally
                If application IsNot Nothing Then application = Nothing
                If excelEngine IsNot Nothing Then excelEngine.Dispose() : excelEngine = Nothing
            End Try


        End Function
        Public Shared Function GetAsDataTable(ByVal excelPath As String, ByVal SheetName As String) As DataTable
            Return GetAsDataTable(excelPath, SheetName, 0)
        End Function
        Public Shared Function GetCellValue(ByVal excelPath As String, ByVal SheetName As String, ByVal RowNumber As Integer, ByVal ColumnNumber As Integer) As String
            If Not System.IO.File.Exists(excelPath) Then Throw New Exception("Source file " & excelPath & " doesn't exists. Can't convert to data table.")

            Dim excelEngine As ExcelEngine = Nothing
            Dim application As IApplication = Nothing
            Try
                excelEngine = New ExcelEngine
                application = excelEngine.Excel

                Dim workbook As IWorkbook = Nothing
                If excelPath.ToLower.EndsWith(".xlsx") Then
                    workbook = application.Workbooks.Open(excelPath, ExcelVersion.Excel2007)
                Else
                    workbook = application.Workbooks.Open(excelPath, ExcelVersion.Excel97to2003)
                End If

                Dim sheet As IWorksheet = Nothing
                Dim sheetIndex As Integer = 0
                If SheetName Is Nothing OrElse SheetName.Trim = "" Then
                    sheet = workbook.Worksheets(0)
                Else
                    For Each tempSheet As IWorksheet In workbook.Worksheets
                        If tempSheet.Name = SheetName Then
                            sheet = tempSheet

                            Exit For
                        End If
                        sheetIndex += 1
                    Next
                End If

                If sheet Is Nothing Then
                    Throw New Exception("Failed to get sheet with name " & SheetName & " from file " & excelPath)
                End If

                Dim value As String = sheet.GetText(RowNumber, ColumnNumber)


                'workbook.ActiveSheetIndex = sheetIndex
                workbook.Close()


                Return value
            Catch ex As Exception
                Throw
            Finally
                If application IsNot Nothing Then application = Nothing
                If excelEngine IsNot Nothing Then excelEngine.Dispose() : excelEngine = Nothing
            End Try
        End Function
        Public Shared Function GetAsDataTable(ByVal excelPath As String, ByVal SheetName As String, ByVal SkipRows As Integer) As DataTable
            If Not System.IO.File.Exists(excelPath) Then Throw New Exception("Source file " & excelPath & " doesn't exists. Can't convert to data table.")

            Dim excelEngine As ExcelEngine = Nothing
            Dim application As IApplication = Nothing
            Try
                excelEngine = New ExcelEngine
                application = excelEngine.Excel

                Dim workbook As IWorkbook = Nothing
                If excelPath.ToLower.EndsWith(".xlsx") Then
                    workbook = application.Workbooks.Open(excelPath, Syncfusion.XlsIO.ExcelVersion.Excel2007)
                Else
                    workbook = application.Workbooks.Open(excelPath, Syncfusion.XlsIO.ExcelVersion.Excel97to2003)
                End If
                Dim sheet As IWorksheet = Nothing
                Dim sheetIndex As Integer = 0
                If SheetName Is Nothing OrElse SheetName.Trim = "" Then
                    sheet = workbook.Worksheets(0)
                Else
                    For Each tempSheet As IWorksheet In workbook.Worksheets
                        If tempSheet.Name = SheetName Then
                            sheet = tempSheet

                            Exit For
                        End If
                        sheetIndex += 1
                    Next
                End If

                If sheet Is Nothing Then
                    Throw New Exception("Failed to get sheet with name " & SheetName & " from file " & excelPath)
                End If

                Dim dt As DataTable = sheet.ExportDataTable(SkipRows + 1, 1, sheet.Rows.Length - 1, sheet.Columns.Length, ExcelExportDataTableOptions.ColumnNames)
                'For temp As Integer = 1 To sheet.Columns.Length
                '    If sheet(1, temp).Text = "" Then
                '        Exit For
                '    End If
                '    dt.Columns.Add(sheet(1, temp).Text)
                'Next

                'For rowCount As Integer = 2 To sheet.Rows.Length
                '    Dim dr As DataRow = dt.NewRow
                '    For temp As Integer = 1 To sheet.Columns.Length
                '        Dim colName As String = sheet(1, temp).Text
                '        Dim value As Object = sheet(rowCount, temp).Text
                '        dr(colName) = value
                '        If value Is Nothing OrElse value.ToString.Trim = "" Then
                '            Trace.WriteLine("NULL")
                '        End If
                '    Next
                '    dt.Rows.Add(dr)
                'Next



                'workbook.ActiveSheetIndex = sheetIndex
                workbook.Close()


                Return dt
            Catch ex As Exception
                Throw
            Finally
                If application IsNot Nothing Then application = Nothing
                If excelEngine IsNot Nothing Then excelEngine.Dispose() : excelEngine = Nothing
            End Try


            'Dim connString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & excelPath & ";Extended Properties=""Excel 8.0;HDR=YES;"""

            'Dim conn As OleDb.OleDbConnection = Nothing
            'Try
            '    conn = New OleDbConnection(connString)
            '    conn.Open()
            'Catch ex As Exception
            '    Throw New Exception("Failed to open connection to file " & excelPath & ", " & ex.Message)
            'End Try

            'Dim cmd As New OleDbCommand("Select * FROM [" & SheetName & "$]", conn)
            'Dim ad As New OleDbDataAdapter(cmd)
            'Dim dt As New DataTable
            'Try
            '    ad.Fill(dt)
            '    Return dt
            'Catch ex As Exception
            '    Throw
            'Finally
            '    conn.Close()
            'End Try
        End Function

        Public Shared Sub GenerateExcelFile(ByVal dt As DataTable, ByVal SavePath As String)
            GenerateExcelFile(dt, SavePath, False)
        End Sub
        Public Shared Sub GenerateExcelFile(ByVal dt As DataTable, ByVal SavePath As String, ByVal useXLSXFormat As Boolean)
            Dim excelEngine As ExcelEngine = Nothing
            Dim application As IApplication = Nothing
            Try
                excelEngine = New ExcelEngine
                application = excelEngine.Excel
                Dim workbook As IWorkbook = application.Workbooks.Create(1)
                If useXLSXFormat Then
                    workbook.Version = Syncfusion.XlsIO.ExcelVersion.Excel2007
                Else
                    workbook.Version = ExcelVersionForWrwiting
                End If

                'Copy over the data from datatable to excel sheet

                'sheet.DeleteRow(2, 10)


                Dim sheet As IWorksheet = workbook.Worksheets(0)
                sheet.ImportDataTable(dt, True, 1, 1, True)



                workbook.SaveAs(SavePath)

                workbook.Close()



            Catch ex As Exception
                Throw
            Finally
                If application IsNot Nothing Then application = Nothing
                If excelEngine IsNot Nothing Then excelEngine.Dispose() : excelEngine = Nothing
            End Try
        End Sub

        Public Shared Sub GenerateExcelFile(ByVal ds As DataSet, ByVal SavePath As String)
            GenerateExcelFile(ds, SavePath, False)
        End Sub
        Public Shared Sub GenerateExcelFile(ByVal ds As DataSet, ByVal SavePath As String, ByVal useXLSXFormat As Boolean)
            Dim excelEngine As ExcelEngine = Nothing
            Dim application As IApplication = Nothing
            Try
                excelEngine = New ExcelEngine
                application = excelEngine.Excel

                Dim workbook As IWorkbook = application.Workbooks.Create(ds.Tables.Count)
                If useXLSXFormat Then
                    workbook.Version = ExcelVersion.Excel2007
                Else
                    workbook.Version = ExcelVersion.Excel97to2003
                End If

                'Copy over the data from datatable to excel sheet

                'sheet.DeleteRow(2, 10)

                For temp As Integer = 0 To ds.Tables.Count - 1
                    Dim dt As DataTable = ds.Tables(temp)
                    Dim sheet As IWorksheet = workbook.Worksheets(temp)
                    sheet.ImportDataTable(dt, True, 1, 1, True)
                Next


                workbook.SaveAs(SavePath)

                workbook.Close()



            Catch ex As Exception
                Throw
            Finally
                If application IsNot Nothing Then application = Nothing
                If excelEngine IsNot Nothing Then excelEngine.Dispose() : excelEngine = Nothing
            End Try
        End Sub

        Public Shared Sub GenerateExcelFile(ByVal templateFile As String, ByVal dt As DataTable, ByVal WorkSheetName As String, ByVal SavePath As String, ByVal RowsToSkip As Integer)
            If Not System.IO.File.Exists(templateFile) Then Throw New Exception("Template file " & templateFile & " doesn't exists. Can't create excel.")
            Dim excelEngine As ExcelEngine = Nothing
            Dim application As IApplication = Nothing
            Try
                excelEngine = New ExcelEngine
                application = excelEngine.Excel

                Dim workbook As IWorkbook = Nothing
                If templateFile.ToLower.EndsWith(".xlsx") Then
                    workbook = application.Workbooks.Open(templateFile, Syncfusion.XlsIO.ExcelVersion.Excel2007)
                Else
                    workbook = application.Workbooks.Open(templateFile, Syncfusion.XlsIO.ExcelVersion.Excel97to2003)
                End If

                workbook.Version = ExcelVersionForWrwiting

                Dim sheet As IWorksheet = Nothing
                Dim sheetIndex As Integer = 0
                For Each tempSheet As IWorksheet In workbook.Worksheets
                    If tempSheet.Name.ToLower.Trim = WorkSheetName.ToLower.Trim Then
                        sheet = tempSheet

                        Exit For
                    End If
                    sheetIndex += 1
                Next
                If sheet Is Nothing Then
                    Throw New Exception("Failed to get sheet with name " & WorkSheetName)
                End If
                'Copy over the data from datatable to excel sheet

                'sheet.DeleteRow(2, 10)

                If RowsToSkip > 0 Then
                    For temp As Integer = sheet.Rows.Length - 1 To RowsToSkip Step -1
                        sheet.Rows(temp).Clear()
                    Next
                Else
                    sheet.ClearData()
                End If



                sheet.ImportDataTable(dt, True, RowsToSkip + 1, 1, True)
                'Dim colCount As Integer = 1
                'For Each col As DataColumn In dt.Columns
                '    sheet(1, colCount).Text = col.ColumnName
                '    colCount += 1
                'Next

                ''Now export data
                'Dim rowCount As Integer = 2
                'For Each row As DataRow In dt.Rows
                '    colCount = 1
                '    For Each col As DataColumn In dt.Columns
                '        If row(col.ColumnName) Is DBNull.Value Then
                '            sheet(rowCount, colCount).Text = ""
                '        Else
                '            sheet(rowCount, colCount).Text = row(col.ColumnName)
                '        End If

                '        colCount += 1
                '    Next
                '    rowCount += 1
                'Next
                workbook.ActiveSheetIndex = sheetIndex
                workbook.SaveAs(SavePath)

                workbook.Close()



            Catch ex As Exception
                Throw
            Finally
                If application IsNot Nothing Then application = Nothing
                If excelEngine IsNot Nothing Then excelEngine.Dispose() : excelEngine = Nothing
            End Try
        End Sub
        Public Shared Sub GenerateExcelFile(ByVal dt As DataTable, ByVal WorkSheetName As String, ByVal SavePath As String, ByVal SheetNumber As Integer)
            Try
                Dim excelEngine As ExcelEngine = New ExcelEngine
                Dim application As IApplication = excelEngine.Excel

                Dim workbook As IWorkbook = application.Workbooks.Create(2)
                workbook.Version = ExcelVersionForWrwiting

                Dim sheet As IWorksheet = workbook.Worksheets(SheetNumber)
                sheet.Name = WorkSheetName

                'Copy over the data from datatable to excel sheet
                Dim colCount As Integer = 1
                For Each col As DataColumn In dt.Columns
                    sheet(1, colCount).Text = col.ColumnName
                    colCount += 1
                Next

                'Now export data
                Dim rowCount As Integer = 2
                For Each row As DataRow In dt.Rows
                    colCount = 1
                    For Each col As DataColumn In dt.Columns
                        If row(col.ColumnName) Is DBNull.Value Then
                            sheet(rowCount, colCount).Text = ""
                        Else
                            sheet(rowCount, colCount).Text = row(col.ColumnName)
                        End If

                        colCount += 1
                    Next
                    rowCount += 1
                Next




                workbook.SaveAs(SavePath)

                workbook.Close()
                excelEngine.Dispose()

            Catch ex As Exception
                Throw New Exception("Failed to create Excel file. " & ex.Message)
            End Try

        End Sub

        Public Shared Sub GenerateExcelFileManually(ByVal dt As DataTable, ByVal SavePath As String)
            Dim excelEngine As ExcelEngine = Nothing
            Dim application As IApplication = Nothing
            Try
                excelEngine = New ExcelEngine
                application = excelEngine.Excel
                Dim workbook As IWorkbook = application.Workbooks.Create(1)
                If SavePath.ToLower.EndsWith(".xlsx") Then
                    workbook.Version = Syncfusion.XlsIO.ExcelVersion.Excel2007
                Else
                    workbook.Version = ExcelVersionForWrwiting
                End If


                Dim sheet As IWorksheet = workbook.Worksheets(0)

                For temp As Integer = 0 To dt.Columns.Count - 1
                    sheet(1, temp + 1).Text = dt.Columns(temp).ColumnName
                Next

                For rowCount As Integer = 0 To dt.Rows.Count - 1
                    For temp As Integer = 0 To dt.Columns.Count - 1
                        sheet(rowCount + 2, temp + 1).Text = dt.Rows(rowCount)(temp).ToString
                        'sheet(rowCount + 2, temp + 1).CellStyle.Font.Italic = True
                    Next
                Next
                'sheet.ImportDataTable(dt, True, 1, 1, True)
                workbook.SaveAs(SavePath)
                workbook.Close()
            Catch ex As Exception
                Throw
            Finally
                If application IsNot Nothing Then application = Nothing
                If excelEngine IsNot Nothing Then excelEngine.Dispose() : excelEngine = Nothing
            End Try
        End Sub
    End Class
End Namespace