Public Module Globals
    Public Property CALoginClientID() As Integer
        Get
            'If HttpContext.Current.Request.Url.AbsoluteUri.Contains("localhost") Then Return 1
            If HttpContext.Current.Session("CALoginClientID") Is Nothing Then Return 0
            Return CInt(HttpContext.Current.Session("CALoginClientID"))
        End Get
        Set(ByVal value As Integer)
            If value = 0 Then
                HttpContext.Current.Session("CALoginClientID") = Nothing
            Else
                HttpContext.Current.Session("CALoginClientID") = value
            End If
        End Set
    End Property
#Region "Base64"
    Dim Base64Chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"

    Public Function base64_decode(ByVal strIn)
        Dim w1, w2, w3, w4, n, strOut
        strOut = ""
        For n = 1 To Len(strIn) Step 4
            w1 = mimedecode(Mid(strIn, n, 1))
            w2 = mimedecode(Mid(strIn, n + 1, 1))
            w3 = mimedecode(Mid(strIn, n + 2, 1))
            w4 = mimedecode(Mid(strIn, n + 3, 1))
            If w2 >= 0 Then _
             strOut = strOut + _
              Chr(((w1 * 4 + Int(w2 / 16)) And 255))
            If w3 >= 0 Then _
             strOut = strOut + _
              Chr(((w2 * 16 + Int(w3 / 4)) And 255))
            If w4 >= 0 Then _
             strOut = strOut + _
              Chr(((w3 * 64 + w4) And 255))
        Next
        base64_decode = strOut
    End Function
    Private Function mimedecode(ByVal strIn)
        If Len(strIn) = 0 Then
            mimedecode = -1 : Exit Function
        Else
            mimedecode = InStr(Base64Chars, strIn) - 1
        End If
    End Function

#End Region

    Public Function TrimNull(ByVal value As Object) As String
        If value.Equals(DBNull.Value) Then Return ""
        Return value.ToString.Trim
    End Function
    Public Function TrimNullBool(ByVal value As Object) As Boolean
        If value.Equals(DBNull.Value) Then Return False
        Return CBool(value)
    End Function

    Public Function BuildTaxYearListOrdered(ByVal o As Orders.Order) As Generic.List(Of String)
        Dim list As New Generic.List(Of String)
        If o.fldtaxyear2000 Then list.Add(2000)
        If o.fldtaxyear2001 Then list.Add(2001)
        If o.fldtaxyear2002 Then list.Add(2002)
        If o.fldtaxyear2003 Then list.Add(2003)
        If o.fldTaxyear2004 Then list.Add(2004)
        If o.fldTaxyear2005 Then list.Add(2005)
        If o.fldTaxyear2006 Then list.Add(2006)
        If o.fldTaxyear2007 Then list.Add(2007)
        If o.fldTaxyear2008 Then list.Add(2008)
        If o.fldTaxyear2009 Then list.Add(2009)
        If o.fldTaxyear2010 Then list.Add(2010)
        If o.fldTaxyear2011 Then list.Add(2011)
        If o.fldTaxyear2012 Then list.Add(2012)
        If o.fldTaxyear2013 Then list.Add(2013)
        If o.fldTaxyear2014 Then list.Add(2014)
        Return list
    End Function
    Public Function GenericArrayToString(ByVal array As Generic.List(Of Integer)) As String
        Dim outSTr As String = ""
        For Each item As Integer In array
            outSTr += item & ","
        Next
        If outSTr.EndsWith(",") Then outSTr = Mid(outSTr, 1, outSTr.Length - 1)
        Return outSTr
    End Function
    Public Function GenericArrayToString(ByVal array As Generic.List(Of String)) As String
        Dim outSTr As String = ""
        For Each item As String In array
            outSTr += item & ","
        Next
        If outSTr.EndsWith(",") Then outSTr = Mid(outSTr, 1, outSTr.Length - 1)
        Return outSTr
    End Function
    Public Function StringToGenericArray(ByVal str As String) As Generic.List(Of String)
        Dim list As New Generic.List(Of String)
        For Each item As String In Split(str, ",")
            If item.Trim <> "" Then list.Add(item)
        Next
        Return list
    End Function
    Public Function StringToGenericArrayInt(ByVal str As String) As Generic.List(Of Integer)
        Dim list As New Generic.List(Of Integer)
        For Each item As String In Split(str, ",")
            If item.Trim <> "" Then list.Add(item)
        Next
        Return list
    End Function
    Public Function GetFormTypeName(ByVal tyoe As TypeOfForm) As String
        Select Case tyoe
            Case TypeOfForm.S_1040
                Return "1040"
            Case TypeOfForm.S_1065
                Return "1065"
            Case TypeOfForm.S_1099
                Return "1099"
            Case TypeOfForm.S_1120
                Return "1120"
            Case TypeOfForm.S_SSN
                Return "SSN"
            Case TypeOfForm.S_W2
                Return "W2"
            Case Else
                Return tyoe
        End Select
    End Function

    Public Function SelectDropDownItemByValue(ByVal ddl As DropDownList, ByVal value As String) As Boolean
        ddl.ClearSelection()
        If value Is Nothing Then Return False
        For Each item As ListItem In ddl.Items
            If item Is Nothing OrElse item.Value Is Nothing Then Continue For
            If item.Value.ToLower.Trim = value.ToLower.Trim Then item.Selected = True : Return True
        Next
        Return False
        'Try
        '    ddl.ClearSelection()
        '    ddl.Items.FindByValue(value).Selected = True
        '    Return True
        'Catch ex As Exception
        'End Try
        'Return False
    End Function
    Public Function SelectDropDownItemByText(ByVal ddl As DropDownList, ByVal text As String) As Boolean
        ddl.ClearSelection()
        If text Is Nothing Then Return False
        For Each item As ListItem In ddl.Items
            If item Is Nothing OrElse item.Text Is Nothing Then Continue For
            If item.Text.ToLower.Trim = text.ToLower.Trim Then item.Selected = True : Return True
        Next
        Return False

        'Try
        '    ddl.ClearSelection()
        '    ddl.Items.FindByText(text).Selected = True
        '    Return True
        'Catch ex As Exception
        'End Try
        'Return False
    End Function
    Public Sub RemoveDropDownItemByValue(ByVal ddl As DropDownList, ByVal value As String)
        For Each item As ListItem In ddl.Items
            If item.Value = value Then
                ddl.Items.Remove(item)
                Return
            End If
        Next
    End Sub
    Public Sub RemoveDropDownItemByText(ByVal ddl As DropDownList, ByVal text As String)
        For Each item As ListItem In ddl.Items
            If item.Text.ToUpper = text.ToUpper Then
                ddl.Items.Remove(item)
                Return
            End If
        Next
    End Sub

    Public Sub StreamFileToUser(ByVal outfilePath As String, ByVal displayFileName As String, ByVal deleteFile As Boolean)
        HttpContext.Current.Response.ClearContent()
        HttpContext.Current.Response.ClearHeaders()


        HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=" & displayFileName)
        HttpContext.Current.Response.ContentType = "application/binary"
        System.Threading.Thread.Sleep(1000)

        HttpContext.Current.Response.WriteFile(outfilePath)
        HttpContext.Current.Response.Flush()
        HttpContext.Current.Response.End()

    End Sub
    Public Sub StreamFileToUser(ByVal outfilePath As String)
        StreamFileToUser(outfilePath, System.IO.Path.GetFileName(outfilePath))
    End Sub
    Public Sub StreamFileToUser(ByVal outfilePath As String, ByVal displayFileName As String)
        StreamFileToUser(outfilePath, displayFileName, False)
    End Sub
    Public Sub StreamStringToUser(ByVal stringToStream As String, ByVal displayFileName As String)
        HttpContext.Current.Response.ClearContent()
        HttpContext.Current.Response.ClearHeaders()


        HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=" & displayFileName)
        HttpContext.Current.Response.ContentType = "application/binary"
        System.Threading.Thread.Sleep(1000)

        HttpContext.Current.Response.Write(stringToStream)
        HttpContext.Current.Response.Flush()
        HttpContext.Current.Response.End()

    End Sub

    Public Function GetTimeDifferenceInHours(ByVal fromDate As DateTime, ByVal toDAte As DateTime) As Integer
        Dim totalHours As Integer = toDAte.Subtract(fromDate).TotalHours
        While True
            Select Case fromDate.DayOfWeek
                Case DayOfWeek.Saturday, DayOfWeek.Sunday
                    totalHours -= 24
            End Select
            fromDate = fromDate.AddDays(1)
            If fromDate >= toDAte Then Exit While
        End While
        Return totalHours
    End Function
End Module
