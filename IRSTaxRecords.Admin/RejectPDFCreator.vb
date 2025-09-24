Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iText = iTextSharp.text
Imports iTextSharp.text.pdf.draw

Public Class RejectPDFCreator

    Public Const ImagesfolderPath As String = "C:\Inetpub\vhosts\irstaxrecords.com\httpdocs\bin\"

    Private Const FONT_NAME As String = "Arial"

    Dim headerFont As iTextSharp.text.Font = New Font(FontFactory.GetFont(FONT_NAME, 14, Font.BOLD))
    Dim headerFontSmall As iTextSharp.text.Font = New Font(FontFactory.GetFont(FONT_NAME, 12, Font.NORMAL))
    Dim itemFont As iTextSharp.text.Font = New Font(FontFactory.GetFont(FONT_NAME, 10, Font.NORMAL))
    Dim itemFontBold As iTextSharp.text.Font = New Font(FontFactory.GetFont(FONT_NAME, 10, Font.BOLD))
    Dim itemFontBoldUnderLined As iTextSharp.text.Font = New Font(FontFactory.GetFont(FONT_NAME, 12, Font.BOLD Or Font.UNDERLINE))
    Dim itemFontBoldRED As iTextSharp.text.Font = New Font(FontFactory.GetFont(FONT_NAME, 10, Font.BOLD, New BaseColor(Drawing.Color.Red)))

    'Dim headerFontSmall As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.TIMES, 12, Font.NORMAL)
    'Dim itemFont As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.TIMES, 10, Font.NORMAL)
    'Dim itemFontBold As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.TIMES, 10, Font.BOLD)
    'Dim itemFontBoldRED As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.TIMES, 10, Font.BOLD, New BaseColor(Drawing.Color.Red))


    Public Sub GenerateToFile(ByVal OrderID As Integer, ByVal FilePath As String)
        Dim generator As New RejectPDFCreator()
        Dim m As IO.MemoryStream = generator.Generate(OrderID)
        Dim sw As System.IO.FileStream = System.IO.File.OpenWrite(FilePath)
        sw.Write(m.GetBuffer(), 0, m.GetBuffer().Length)
        sw.Close()
        m.Close()
        sw.Dispose()
        m.Dispose()
        m = Nothing
        sw = Nothing
    End Sub
    Public Function Generate(ByVal OrderID As Integer) As IO.MemoryStream

        Dim m As System.IO.MemoryStream = New System.IO.MemoryStream()
        Dim _document As iText.Document = Nothing  'New iText.Document(PageSize.A4, 50, 50, 50, 50)


        _document = New iText.Document(PageSize.A4, 25, 25, 25, 25)
        Dim writer As iText.pdf.PdfWriter = iText.pdf.PdfWriter.GetInstance(_document, m)
        _document.Open()


        Dim tbl As New PdfPTable(2)
        tbl.DefaultCell.Border = PdfPCell.NO_BORDER
        tbl.SetWidthPercentage(New Single() {300, 300}, _document.PageSize)

        tbl.AddCell(GetTableCell("IRSTAXRECORDS.com", headerFont))
        Dim cell As PdfPCell = GetTableCell("Customer Service: 1-866-850-4506", headerFontSmall)
        cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        tbl.AddCell(cell)

        _document.Add(tbl)

        DrawLine(_document)



        tbl = New PdfPTable(2)
        tbl.DefaultCell.Border = PdfPCell.NO_BORDER
        tbl.SetWidthPercentage(New Single() {300, 300}, _document.PageSize)

        tbl.AddCell(GetTableCell("VERIFY YOUR BORROWERS INCOME" & vbCrLf & "WITH U.S. GOVERNMENT TAX RECORDS", headerFontSmall))
        cell = GetTableCell("611 Pennsylvania Avenue, Suite 104" & vbCrLf & "Washington D.C. 20003", headerFontSmall)
        cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        tbl.AddCell(cell)
        _document.Add(tbl)



        Dim o As Core.Orders.Order = Core.OrderServices.GetOrder(OrderID)

        If o.RejectCode = Core.Orders.RejectCodeType.Unprocessable Then
            ProcessBodyForCode10(_document, o)
        Else
            ProcessBody(_document, o)
            tbl = New PdfPTable(1)
            tbl.DefaultCell.Border = PdfPCell.NO_BORDER
            tbl.SetWidthPercentage(New Single() {600}, _document.PageSize)

            cell = GetTableCell(vbCrLf & "If you have any questions please contact Customer Service.", itemFont)
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            tbl.AddCell(cell)
            cell = GetTableCell(vbCrLf & "1-866-850-4506 or admin@irstaxrecords.com.", itemFont)
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            tbl.AddCell(cell)

            _document.Add(tbl)
        End If




        _document.Close()
        writer.Flush()

        Return m
    End Function
    Private Sub ProcessBody(ByVal _document As Document, ByVal o As Core.Orders.Order)
        '_document.Add(New Phrase(vbCrLf & vbCrLf, itemFont))

        Dim tbl As New PdfPTable(4)
        tbl.SpacingBefore = 25
        'tbl.SpacingAfter = 25
        tbl.DefaultCell.Border = PdfPCell.NO_BORDER
        tbl.SetWidthPercentage(New Single() {50, 225, 125, 175}, _document.PageSize)

        tbl.AddCell(GetTableCell("Name: ", itemFont))
        tbl.AddCell(GetTableCell(o.fldrequestname, itemFont))
        tbl.AddCell(GetTableCell("Social Security Number: ", itemFont))
        tbl.AddCell(GetTableCell(o.fldssnno, itemFont))

        _document.Add(tbl)

        DrawLine(_document)

        Dim formType As String = ""
        Select Case o.FormType
            Case Core.Orders.FormTypeCodeType.S_1040 : formType = "1040"
            Case Core.Orders.FormTypeCodeType.S_1065 : formType = "1065"
            Case Core.Orders.FormTypeCodeType.S_1099 : formType = "1099"
            Case Core.Orders.FormTypeCodeType.S_1120 : formType = "1120"
            Case Core.Orders.FormTypeCodeType.S_SSN : formType = "SSN"
            Case Core.Orders.FormTypeCodeType.S_W2 : formType = "W2"
            Case Else
                formType = "UNKNOWN"
        End Select



        _document.Add(New Phrase(vbCrLf & vbCrLf & "We processed the above " & formType & " request today, unfortunately, it was rejected by the Internal Revenue Service for the following reason(s):", itemFont))
        _document.Add(New Phrase(vbCrLf & vbCrLf, itemFont))




        tbl = New PdfPTable(2)
        tbl.DefaultCell.Border = PdfPCell.NO_BORDER
        tbl.HorizontalAlignment = PdfPCell.ALIGN_LEFT
        tbl.SetWidthPercentage(New Single() {25, 450}, _document.PageSize)
        tbl.DefaultCell.Padding = 10

        Dim serverURL As String = ImagesfolderPath


        tbl.AddCell(GetTableCellWithImage(serverURL & "Box.jpg"))
        tbl.AddCell(GetTableCell("The Internal Revenue Service has no 1040 records for tax year:", itemFont))

        tbl.AddCell(GetTableCell(" ", itemFont))
        tbl.AddCell(GetTableCell(" ", itemFont))

        If o.RejectCode = Core.Orders.RejectCodeType.Name_does_not_match_record Then
            tbl.AddCell(GetTableCellWithImage(serverURL & "BoxChecked.jpg"))
        Else
            tbl.AddCell(GetTableCellWithImage(serverURL & "Box.jpg"))
        End If
        tbl.AddCell(GetTableCell("The Taxpayer name does not match the Social Security Number provided on request Form " & formType & ". Please verify the Social Security Number and Taxpayer name so we may obtain records.", itemFont))


        tbl.AddCell(GetTableCell(" ", itemFont))
        tbl.AddCell(GetTableCell(" ", itemFont))
        If o.RejectCode = Core.Orders.RejectCodeType.Social_Security_Number Then
            tbl.AddCell(GetTableCellWithImage(serverURL & "BoxChecked.jpg"))
        Else
            tbl.AddCell(GetTableCellWithImage(serverURL & "Box.jpg"))
        End If
        tbl.AddCell(GetTableCell("The Social Security Number/EIN you have provided on Form " & formType & " is not on file with the Internal Revenue Service. Please confirm the Social Security Number/EIN so we may obtain records.", itemFont))


        tbl.AddCell(GetTableCell(" ", itemFont))
        tbl.AddCell(GetTableCell(" ", itemFont))
        If o.RejectCode = Core.Orders.RejectCodeType.Address Then
            tbl.AddCell(GetTableCellWithImage(serverURL & "BoxChecked.jpg"))
        Else
            tbl.AddCell(GetTableCellWithImage(serverURL & "Box.jpg"))
        End If
        tbl.AddCell(GetTableCell("The Address show on line 3 and 4 of Form " & formType & " does NOT match records at the Internal Revenue Service. Please provide us with the previous address from which the taxpayer has used to file taxes. fpossible, please provide us with the address used when filing the last tax return.", itemFont))

        tbl.AddCell(GetTableCell(" ", itemFont))
        tbl.AddCell(GetTableCell(" ", itemFont))
        If o.RejectCode = Core.Orders.RejectCodeType.Old_Signature_date Then
            tbl.AddCell(GetTableCellWithImage(serverURL & "BoxChecked.jpg"))
        Else
            tbl.AddCell(GetTableCellWithImage(serverURL & "Box.jpg"))
        End If
        tbl.AddCell(GetTableCell("The taxpayers signature date has expired. Form " & formType & " must be signed and dated within 60 days of the date of request.", itemFont))


        tbl.AddCell(GetTableCell(" ", itemFont))
        tbl.AddCell(GetTableCell(" ", itemFont))
        If o.RejectCode = Core.Orders.RejectCodeType.Name_does_not_match_record Then
            tbl.AddCell(GetTableCellWithImage(serverURL & "BoxChecked.jpg"))
        Else
            tbl.AddCell(GetTableCellWithImage(serverURL & "Box.jpg"))
        End If
        tbl.AddCell(GetTableCell("We are in the process of re-verifying this request with the Internal Revenue Service. If you find that the information submitted on form " & formType & " is incorrect, please notify us as soon as possible. If this borrowers record status should change, we will notify you immediately.", itemFont))

        _document.Add(tbl)


    End Sub
    Private Sub ProcessBodyForCode10(ByVal _document As Document, ByVal o As Core.Orders.Order)
        '_document.Add(New Phrase(vbCrLf & vbCrLf, itemFont))


        Dim formType As String = ""
        Select Case o.FormType
            Case Orders.FormTypeCodeType.S_1040 : formType = "1040"
            Case Orders.FormTypeCodeType.S_1065 : formType = "1065"
            Case Orders.FormTypeCodeType.S_1099 : formType = "1099"
            Case Orders.FormTypeCodeType.S_1120 : formType = "1120"
            Case Orders.FormTypeCodeType.S_SSN : formType = "SSN"
            Case Orders.FormTypeCodeType.S_W2 : formType = "W2"
            Case Else
                formType = "UNKNOWN"
        End Select


        Dim tbl As New PdfPTable(6)
        tbl.SpacingBefore = 25
        'tbl.SpacingAfter = 25
        tbl.DefaultCell.Border = PdfPCell.NO_BORDER
        tbl.SetWidthPercentage(New Single() {50, 150, 125, 100, 125, 50}, _document.PageSize)

        tbl.AddCell(GetTableCell("Name: ", itemFont))
        tbl.AddCell(GetTableCell(o.fldrequestname, itemFont))

        tbl.AddCell(GetTableCell("Social Security Number: ", itemFont))
        tbl.AddCell(GetTableCell(o.fldssnno, itemFont))

        tbl.AddCell(GetTableCell("Form Requested: ", itemFont))
        tbl.AddCell(GetTableCell(formType, itemFont))

        _document.Add(tbl)

        DrawLine(_document)



        tbl = New PdfPTable(1)
        tbl.DefaultCell.Border = PdfPCell.NO_BORDER

        Dim td As PdfPCell = GetTableCell(vbCrLf & vbCrLf & "IDENTITY THEFT", itemFontBoldUnderLined)
        td.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        tbl.AddCell(td)

        Dim paragraph As String = vbCrLf & vbCrLf & "We received a response back from the Internal Revenue Service regarding the " & formType & " request. The IRS has rejected because they have identified the possibility of"
        paragraph += " Identity Theft on this taxpayers account."
        tbl.AddCell(GetTableCell(paragraph, itemFont))

        paragraph = vbCrLf & vbCrLf & "Internal Revenue Service requests that the taxpayer contact the Identity Protection Security Unit at "
        paragraph += " 1-800-829-0433, where they can discuss the matter and possibly release transcripts to the taxpayer."
        tbl.AddCell(GetTableCell(paragraph, itemFont))

        paragraph = vbCrLf & vbCrLf & "Below is what the IRS has released to explain this reject."
        tbl.AddCell(GetTableCell(paragraph, itemFont))

        paragraph = vbCrLf & vbCrLf & "IRS has implemented the following on April 6, 2015."
        tbl.AddCell(GetTableCell(paragraph, itemFont))

        paragraph = vbCrLf & vbCrLf & "IRS definition:"
        tbl.AddCell(GetTableCell(paragraph, itemFont))

        paragraph = vbCrLf & vbCrLf & "IRS Code 10: Identity Theft or breach of taxpayer information is used when there is possible identity theft"
        paragraph += " associated with the taxpayer's account. The taxpayer is referred to the Identity Protection Security Unit"
        paragraph += " where they may be able to receive their requested information. If they are permitted to receive their"
        paragraph += " information, the information will only be sent to the taxpayer and a not third party."
        tbl.AddCell(GetTableCell(paragraph, itemFont))


        paragraph = vbCrLf & vbCrLf & "For more information about the Internal Revenue Service and Identity Theft you may visit:"
        tbl.AddCell(GetTableCell(paragraph, itemFont))

        paragraph = vbCrLf & vbCrLf & "http://www.irs.gov/Individuals/Identity-Protection"
        tbl.AddCell(GetTableCell(paragraph, itemFont))

        paragraph = vbCrLf & vbCrLf & "Please contact us if you have any questions at 1-866-850-4506" & vbCrLf
        paragraph += "or email us at admin@irstaxrecords.com"
        tbl.AddCell(GetTableCell(paragraph, itemFont))


        paragraph = vbCrLf & vbCrLf & "Regards," & vbCrLf
        paragraph += "Tom Irwin" & vbCrLf
        paragraph += "tirwin@irstaxrecords.com" & vbCrLf
        paragraph += "1-866-850-4506" & vbCrLf
        tbl.AddCell(GetTableCell(paragraph, itemFont))

        _document.Add(tbl)
    End Sub

    Private Sub DrawLine(ByVal _document As Document)
        Dim chunk As Chunk = New Chunk(New LineSeparator(1.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, -1))
        _document.Add(chunk)
    End Sub
    Private Function GetTableCell(ByVal content As String, ByVal font As text.Font) As PdfPCell
        Dim td As PdfPCell = New PdfPCell(New Phrase(content, font))
        td.PaddingTop = 0
        td.Border = PdfPCell.NO_BORDER
        td.HorizontalAlignment = PdfPCell.ALIGN_LEFT
        Return td
    End Function
    Private Function GetTableCellWithImage(ByVal imagePath As String) As PdfPCell
        Dim jpeg As iText.Image = Image.GetInstance(imagePath)
        jpeg.ScaleToFit(16, 16)
        Dim td As New PdfPCell(jpeg)
        td.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        td.Border = PdfPCell.NO_BORDER
        Return td

    End Function

End Class
