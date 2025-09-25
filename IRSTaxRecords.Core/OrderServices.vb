Imports System.Web

Public Class OrderServices


    Public Shared Function GetOrder(ByVal OrderID As Integer) As Orders.Order
        Dim dt As DataTable = DataHelper.ExecuteQuery("SELECT * FROM tblOrder WHERE FldOrdernumber = " & OrderID)
        If dt.Rows.Count = 0 Then Return Nothing
        Return DataRowToOrder(dt.Rows(0))
    End Function

    Public Shared Function GetCustomerOrders(ByVal CustomerID As Integer) As Orders.Order
        Dim dt As DataTable = DataHelper.ExecuteQuery("SELECT * FROM tblOrder WHERE fldcustomeriD = " & CustomerID)
        If dt.Rows.Count = 0 Then Return Nothing
        Return DataRowToOrder(dt.Rows(0))
    End Function
    Public Shared Function CreateNewOrder(ByVal o As Orders.Order) As Boolean
        Dim _helper As New DataServices()
        o.fldordernumber = _helper.Orders_AddNew(o.fldListid, o.fldlisttype, o.fldCompanyID, o.fldcustomeriD, o.fldrequestname, o.fldsecondname, o.fldssnno, o.fldtaxyear2003, o.fldtaxyear2002, o.fldtaxyear2001, o.fldtaxyear2000, o.fldtypeofform, o.fldemail, o.fldfax, o.fldfaxno, o.fldstatus, o.fldDOB, o.fldSex, o.fldbillingstatus, o.flddeliverydate, o.fldPdf, o.fldOrderdate, o.fldTaxyear2004, o.fldSpecialFlag, o.fldTaxyear2005, o.fldTaxyear2006, o.ListID, o.bUpdatedInQB, o.fldTaxyear2007, o.fldTaxyear2008, o.fldTaxyear2009, o.fldTaxyear2010, o.fldLoanNumber, o.QBBatchNumber, o.UpdatedInQBOn, o.fldTaxyear2011, o.fldTaxyear2012, o.IsRejected, o.RejectCode, "", o.IsDismissedForRejection, o.fldTaxyear2013, o.fldTaxyear2014, o.fldTaxyear2015, o.fldTaxyear2016, o.fldTaxyear2017, o.fldTaxyear2018, o.fldTaxyear2019, o.fldTaxyear2020, o.fldTaxyear2021, o.fldTaxyear2022, o.fldTaxyear2023, o.fldTaxyear2024, o.fldTaxyear2025)
        Return o.fldordernumber > 0
    End Function
    Private Shared Function DataRowToOrder(ByVal dr As DataRow) As Orders.Order
        Dim o As New Orders.Order
        With o
            If Not dr("fldordernumber") Is DBNull.Value Then .fldordernumber = dr("fldordernumber")
            If Not dr("fldListid") Is DBNull.Value Then .fldListid = dr("fldListid")
            If Not dr("fldlisttype") Is DBNull.Value Then .fldlisttype = dr("fldlisttype")
            If Not dr("fldCompanyID") Is DBNull.Value Then .fldCompanyID = dr("fldCompanyID")
            If Not dr("fldcustomeriD") Is DBNull.Value Then .fldcustomeriD = dr("fldcustomeriD")
            If Not dr("fldrequestname") Is DBNull.Value Then .fldrequestname = dr("fldrequestname")
            If Not dr("fldsecondname") Is DBNull.Value Then .fldsecondname = dr("fldsecondname")
            If Not dr("fldssnno") Is DBNull.Value Then .fldssnno = dr("fldssnno")
            If Not dr("fldtaxyear2003") Is DBNull.Value Then .fldtaxyear2003 = dr("fldtaxyear2003")
            If Not dr("fldtaxyear2002") Is DBNull.Value Then .fldtaxyear2002 = dr("fldtaxyear2002")
            If Not dr("fldtaxyear2001") Is DBNull.Value Then .fldtaxyear2001 = dr("fldtaxyear2001")
            If Not dr("fldtaxyear2000") Is DBNull.Value Then .fldtaxyear2000 = dr("fldtaxyear2000")
            If Not dr("fldtypeofform") Is DBNull.Value Then .fldtypeofform = dr("fldtypeofform")
            If Not dr("fldemail") Is DBNull.Value Then .fldemail = dr("fldemail")
            If Not dr("fldfax") Is DBNull.Value Then .fldfax = dr("fldfax")
            If Not dr("fldfaxno") Is DBNull.Value Then .fldfaxno = dr("fldfaxno")
            If Not dr("fldstatus") Is DBNull.Value Then .fldstatus = dr("fldstatus")
            If Not dr("fldDOB") Is DBNull.Value Then .fldDOB = dr("fldDOB")
            If Not dr("fldSex") Is DBNull.Value Then .fldSex = dr("fldSex")
            If Not dr("fldbillingstatus") Is DBNull.Value Then .fldbillingstatus = dr("fldbillingstatus")
            If Not dr("flddeliverydate") Is DBNull.Value Then .flddeliverydate = dr("flddeliverydate")
            If Not dr("fldPdf") Is DBNull.Value Then .fldPdf = dr("fldPdf")
            If Not dr("fldOrderdate") Is DBNull.Value Then .fldOrderdate = dr("fldOrderdate")
            If Not dr("fldTaxyear2004") Is DBNull.Value Then .fldTaxyear2004 = dr("fldTaxyear2004")
            If Not dr("fldSpecialFlag") Is DBNull.Value Then .fldSpecialFlag = dr("fldSpecialFlag")
            If Not dr("fldTaxyear2005") Is DBNull.Value Then .fldTaxyear2005 = dr("fldTaxyear2005")
            If Not dr("fldTaxyear2006") Is DBNull.Value Then .fldTaxyear2006 = dr("fldTaxyear2006")
            If Not dr("ListID") Is DBNull.Value Then .ListID = dr("ListID")
            If Not dr("bUpdatedInQB") Is DBNull.Value Then .bUpdatedInQB = dr("bUpdatedInQB")
            If Not dr("fldTaxyear2007") Is DBNull.Value Then .fldTaxyear2007 = dr("fldTaxyear2007")
            If Not dr("fldTaxyear2008") Is DBNull.Value Then .fldTaxyear2008 = dr("fldTaxyear2008")
            If Not dr("fldTaxyear2009") Is DBNull.Value Then .fldTaxyear2009 = dr("fldTaxyear2009")
            If Not dr("fldTaxyear2010") Is DBNull.Value Then .fldTaxyear2010 = dr("fldTaxyear2010")
            If Not dr("fldLoanNumber") Is DBNull.Value Then .fldLoanNumber = dr("fldLoanNumber")
            If Not dr("QBBatchNumber") Is DBNull.Value Then .QBBatchNumber = dr("QBBatchNumber")
            If Not dr("UpdatedInQBOn") Is DBNull.Value Then .UpdatedInQBOn = dr("UpdatedInQBOn")
            If Not dr("fldTaxyear2011") Is DBNull.Value Then .fldTaxyear2011 = dr("fldTaxyear2011")
            If Not dr("fldTaxyear2012") Is DBNull.Value Then .fldTaxyear2012 = dr("fldTaxyear2012")
            If Not dr("fldTaxyear2013") Is DBNull.Value Then .fldTaxyear2013 = dr("fldTaxyear2013")
            If Not dr("fldTaxyear2014") Is DBNull.Value Then .fldTaxyear2014 = dr("fldTaxyear2014")
            If Not dr("fldTaxyear2015") Is DBNull.Value Then .fldTaxyear2015 = dr("fldTaxyear2015")
            If Not dr("fldTaxyear2016") Is DBNull.Value Then .fldTaxyear2016 = dr("fldTaxyear2016")
            If Not dr("fldTaxyear2017") Is DBNull.Value Then .fldTaxyear2017 = dr("fldTaxyear2017")
            If Not dr("fldTaxyear2018") Is DBNull.Value Then .fldTaxyear2018 = dr("fldTaxyear2018")

            If Not dr("fldTaxyear2019") Is DBNull.Value Then .fldTaxyear2019 = dr("fldTaxyear2019")
            If Not dr("fldTaxyear2020") Is DBNull.Value Then .fldTaxyear2020 = dr("fldTaxyear2020")
            If Not dr("fldTaxyear2021") Is DBNull.Value Then .fldTaxyear2021 = dr("fldTaxyear2021")
            If Not dr("fldTaxyear2022") Is DBNull.Value Then .fldTaxyear2022 = dr("fldTaxyear2022")
            If Not dr("fldTaxyear2023") Is DBNull.Value Then .fldTaxyear2023 = dr("fldTaxyear2023")
            If Not dr("fldTaxyear2024") Is DBNull.Value Then .fldTaxyear2024 = dr("fldTaxyear2024")
            If Not dr("fldTaxyear2025") Is DBNull.Value Then .fldTaxyear2025 = dr("fldTaxyear2025")

            If Not dr("IsRejected") Is DBNull.Value Then .IsRejected = dr("IsRejected")
            If Not dr("RejectCode") Is DBNull.Value Then .RejectCode = dr("RejectCode")
            'If Not dr("NoticeReason") Is DBNull.Value Then .NoticeReason = dr("NoticeReason")
            If Not dr("IsDismissedForRejection") Is DBNull.Value Then .IsDismissedForRejection = dr("IsDismissedForRejection")
        End With
        Return o
    End Function
End Class

