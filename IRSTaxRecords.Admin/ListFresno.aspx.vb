Public Partial Class ListFresno
    Inherits System.Web.UI.Page

    Public ReadOnly Property ListID() As Integer
        Get
            Return Val(Request.QueryString("id"))
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        msg.Clear()

        If Not Page.IsPostBack Then
            LoadData()
            CType(Me.Master, IRSWebAdminMaster).HideMainMenu()

        End If
    End Sub
    Private Sub LoadData()

    End Sub

    Private Sub grdMain_CustomAggregate(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCustomAggregateEventArgs) Handles grdMain.CustomAggregate
        Dim dt As DataTable = Session("DataSource")
        Dim yearsObj As Object = dt.Compute("Sum(YearsTotal)", Nothing)
        If yearsObj IsNot DBNull.Value Then
            Dim totalYears As Integer = CInt(yearsObj)
            e.Result = "<b>Total Years: " & totalYears & "</b>"
            lblTotalTaxYears.Text = totalYears
        Else
            e.Result = "<b>Total Years: 0</b>"
            lblTotalTaxYears.Text = 0
        End If
        e.Result = ""
    End Sub

    Private Sub grdMain_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdMain.ItemDataBound
        Try


        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdMain_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
        Dim list As Core.Content.ListType = ListServices.GetList(ListID)
        If list Is Nothing Then
            msg.ShowError("List with id " & ListID & " was not found")
            Return
        End If

        lblListName.Text = list.fldListname
        lblListTypeName.Text = Globals.GetFormTypeName(list.fldlisttype)
        lblListTypeName2.Text = lblListTypeName.Text

        Dim displayName As String = ""
        Select Case lblListTypeName2.Text
            Case "W-2", "W2", "1120"
                CType(Me.Master, IRSWebAdminMaster).SetLeftBorderColor("Maroon")
                displayName = "W-2"
            Case "1040"
                CType(Me.Master, IRSWebAdminMaster).SetLeftBorderColor("#00ff00")
                displayName = "1040"
            Case "1065"
                CType(Me.Master, IRSWebAdminMaster).SetLeftBorderColor("#00ff00")
                displayName = "1040"
            Case "1099"
                CType(Me.Master, IRSWebAdminMaster).SetLeftBorderColor("#00ff00")
                displayName = "1040"
            Case "SSN"
                CType(Me.Master, IRSWebAdminMaster).SetLeftBorderColor("#00ff00")
                displayName = "Special"
        End Select
        If displayName <> "" Then
            lblListTypeName.Text = displayName
            lblListTypeName2.Text = lblListTypeName.Text
        End If

        Dim strQ As String = ""
        If list.fldlisttype = ListTypeCodeType.Special Then
            strQ = " select fldlistid,fldTypeOfForm, fldrequestname,fldssnno,fldTaxyear2007,fldtaxyear2006,fldtaxyear2005,fldtaxyear2009,fldtaxyear2011,fldtaxyear2012,fldtaxyear2013,fldtaxyear2014,fldtaxyear2015,fldtaxyear2016,fldtaxyear2017,fldtaxyear2018,fldtaxyear2010,fldtaxyear2008,fldOrderNumber,fldtaxyear2019,fldtaxyear2020,fldtaxyear2021,fldtaxyear2022,fldtaxyear2023,fldtaxyear2024,fldtaxyear2024,fldtaxyear2025 from tblOrder "
            strQ += " where fldlistid=" & ListID & " and fldListType=" & CInt(list.fldlisttype) & " and fldstatus='p' and fldSpecialflag='Y' order by fldordernumber "
        Else
            strQ = " select fldlistid,fldTypeOfForm, fldrequestname,fldssnno,fldTaxyear2007,fldtaxyear2006,fldtaxyear2005,fldtaxyear2009,fldtaxyear2011,fldtaxyear2012,fldtaxyear2013,fldtaxyear2014,fldtaxyear2015,fldtaxyear2016,fldtaxyear2017,fldtaxyear2018,fldtaxyear2010,fldtaxyear2008,fldOrderNumber,fldtaxyear2019,fldtaxyear2020,fldtaxyear2021,fldtaxyear2022,fldtaxyear2023,fldtaxyear2024,fldtaxyear2024,fldtaxyear2025 from tblOrder "
            strQ += " where fldlistid=" & ListID & " and fldListType=" & CInt(list.fldlisttype) & " and fldstatus='p' order by fldordernumber "
        End If

        Dim dt As DataTable = DataHelper.ExecuteQuery(strQ)

        dt.Columns.Add("RequestNameWithInitials")
        dt.Columns.Add("FormTypeName")
        dt.Columns.Add("YearsString")
        dt.Columns.Add("YearsTotal", GetType(System.Int32))
        dt.Columns.Add("RowOrder", GetType(System.Int32))


        For temp As Integer = dt.Rows.Count - 1 To 0 Step -1
            Dim listID As Integer = dt.Rows(temp)("fldlistid")
            Dim fldTypeOfForm As Integer = dt.Rows(temp)("fldTypeOfForm")
            Dim fldssnno As String = dt.Rows(temp)("fldssnno")

            Dim yearsList As New Generic.List(Of Integer)

            If TrimNullBool(dt.Rows(temp)("fldtaxyear2005")) Then yearsList.Add("2005")
            If TrimNullBool(dt.Rows(temp)("fldtaxyear2006")) Then yearsList.Add("2006")
            If TrimNullBool(dt.Rows(temp)("fldtaxyear2007")) Then yearsList.Add("2007")
            If TrimNullBool(dt.Rows(temp)("fldtaxyear2008")) Then yearsList.Add("2008")
            If TrimNullBool(dt.Rows(temp)("fldtaxyear2009")) Then yearsList.Add("2009")
            If TrimNullBool(dt.Rows(temp)("fldtaxyear2010")) Then yearsList.Add("2010")
            If TrimNullBool(dt.Rows(temp)("fldtaxyear2011")) Then yearsList.Add("2011")
            If TrimNullBool(dt.Rows(temp)("fldtaxyear2012")) Then yearsList.Add("2012")
            If TrimNullBool(dt.Rows(temp)("fldtaxyear2013")) Then yearsList.Add("2013")
            If TrimNullBool(dt.Rows(temp)("fldtaxyear2014")) Then yearsList.Add("2014")
            If TrimNullBool(dt.Rows(temp)("fldtaxyear2015")) Then yearsList.Add("2015")
            If TrimNullBool(dt.Rows(temp)("fldtaxyear2016")) Then yearsList.Add("2016")
            If TrimNullBool(dt.Rows(temp)("fldtaxyear2017")) Then yearsList.Add("2017")
            If TrimNullBool(dt.Rows(temp)("fldtaxyear2018")) Then yearsList.Add("2018")

            If TrimNullBool(dt.Rows(temp)("fldtaxyear2019")) Then yearsList.Add("2019")
            If TrimNullBool(dt.Rows(temp)("fldtaxyear2020")) Then yearsList.Add("2020")
            If TrimNullBool(dt.Rows(temp)("fldtaxyear2021")) Then yearsList.Add("2021")
            If TrimNullBool(dt.Rows(temp)("fldtaxyear2022")) Then yearsList.Add("2022")
            If TrimNullBool(dt.Rows(temp)("fldtaxyear2023")) Then yearsList.Add("2023")
            If TrimNullBool(dt.Rows(temp)("fldtaxyear2024")) Then yearsList.Add("2024")
            If TrimNullBool(dt.Rows(temp)("fldtaxyear2025")) Then yearsList.Add("2025")

            dt.Rows(temp)("YearsString") = Utilities.Translators.GenericArrayToString(yearsList).Replace(",", "-")
            dt.Rows(temp)("YearsTotal") = yearsList.Count

            If fldTypeOfForm <> 4 Then Continue For
            If dt.Select("fldssnno='" & fldssnno & "'").Length > 1 Then
                dt.Rows.RemoveAt(temp)
            End If
        Next
        For temp As Integer = 0 To dt.Rows.Count - 1
            Dim isROA As Boolean = False
            Dim isAT As Boolean = False
            dt.Rows(temp)("RequestNameWithInitials") = GetRequestName(TrimNull(dt.Rows(temp)("fldRequestName")), isROA, isAT)
            dt.Rows(temp)("FormTypeName") = Globals.GetFormTypeName(TrimNull(dt.Rows(temp)("fldTypeOfForm")))
            dt.Rows(temp)("RowOrder") = temp + 1
            If isROA Then
                If dt.Rows(temp)("FormTypeName").ToString.Contains("1040") Then
                    dt.Rows(temp)("FormTypeName") = "ROA"
                Else
                    dt.Rows(temp)("FormTypeName") += " ROA"
                End If
            ElseIf isAT Then
                If dt.Rows(temp)("FormTypeName").ToString.Contains("1040") Then
                    dt.Rows(temp)("FormTypeName") = "AT"
                Else
                    dt.Rows(temp)("FormTypeName") += " AT"
                End If

            End If
        Next


        Session("DataSource") = dt
        Me.grdMain.DataSource = dt
    End Sub
    Private Function GetRequestName(ByVal name As String, ByRef IsROA As Boolean, ByRef IsAT As Boolean) As String

        Dim outNames As New Generic.List(Of String)
        For Each thisName As String In Split(name, " ")
            If thisName.Trim = "" Then Continue For
            If thisName.Trim = "1099" Then Continue For

            outNames.Add(thisName.Trim)
        Next
        If outNames.Count < 1 Then Return name


        Dim names = New List(Of String)(Split(name, " "))

        If names(names.Count - 1).Trim.ToUpper = "ROA" Then
            names.RemoveAt(names.Count - 1)
            isROA = True
        End If
        If names(names.Count - 1).Trim.ToUpper = "AT" Then
            names.RemoveAt(names.Count - 1)
            isAT = True
        End If
        If IsNumeric(names(names.Count - 1).Trim) Then
            names.RemoveAt(names.Count - 1)
        End If

        outNames = names


        Dim outName As String = Left(outNames(outNames.Count - 1), 4) & "/" & Left(outNames(0).Trim, 1)
        Return outName
    End Function
    Private Function TrimNull(ByVal value As Object) As String
        If value.Equals(DBNull.Value) Then Return ""
        Return value.ToString.Trim
    End Function
End Class