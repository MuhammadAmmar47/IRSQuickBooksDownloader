<%
  '=============================================================================
    'Page Created By : sachin.pawar@ecotechservices.com
    'On              : 13 Aug 2009
    'Purpose         : To download the report in excel format
  '=============================================================================
    Server.ScriptTimeout = 3600
%>

<!--#include file="../header.asp"-->
<html xmlns:x="urn:schemas-microsoft-com:office:excel">
<head>
<style>
<!--table
br {mso-data-placement:same-cell;}
tr {vertical-align:top;}
@page
{mso-page-orientation:landscape;}
-->
</style>
</head>
<body>
<%
Function pd(n, totalDigits) 
    if totalDigits > len(n) then 
        pd = String(totalDigits-len(n),"0") & n 
    else 
        pd = n 
    end if 
End Function 
	 Dim SQL,objRS,intlclRegId,strType, j, hdate, currentStartDay, currentEndDay, flname, Counter
		j = 1
		todayDate = FormatDateTime(Date,2)
		Dim dtFrom, dtTo
		dtFrom = Request.Form("fromdate")
		dtTo = Request.Form("todate")
		ExportFileName = Replace(dtFrom,"/","") &"-"& Replace(dtTo,"/","")
		
		'ExportFileName = YEAR(dtTo) &  Pd(Month(dtTo),2) & Pd(DAY(dtTo),2) 
		ExportFileName = YEAR(dtTo) &  Pd(Month(dtTo),2) & Pd(DAY(dtTo),2) & HOUR(date()) & MINUTE(date()) & SECOND(date())
		ExportFileName = ExportFileName & "_listReport"
		
		Set fso = Server.CreateObject("Scripting.FileSystemObject")
		path = Server.MapPath("../export")
		If Not (fso.FolderExists(path)) Then
			fso.CreateFolder path 
		End If
    	path = path & "\" &  ExportFileName &".xls"
    	Set ExportFile = fso.CreateTextFile(path,true)
    	
    	Dim i
    	
    	ExportFile.writeline "<table border=1>"
    	''ExportFile.writeline "<tr>"
		''ExportFile.writeline"<td colspan='8'>Monthly Report for:</td>"
		''ExportFile.writeline "</tr>"
		
		ExportFile.writeline "<tr>" 
		ExportFile.writeline"<td bgcolor='#C0C0C0'><b>Customer ID</b></td><td bgcolor='#C0C0C0'><b>Customer Name</b></td><td bgcolor='#C0C0C0'><b>Order Name</b></td><td bgcolor='#C0C0C0'><b>Loan Number</b></td><td bgcolor='#C0C0C0'><b>Request</b></td><td bgcolor='#C0C0C0'><b>Tax Years</b></td><td bgcolor='#C0C0C0'><b>SSN</b></td><td bgcolor='#C0C0C0'><b>Status</b></td><td bgcolor='#C0C0C0'><b>Delivery Date</b></td><td bgcolor='#C0C0C0'><b>Price</b></td>"
		ExportFile.writeline "</tr>"
		
		for i = 1 to 10 step 1
			userid = trim (Request.Form("txtCust"&i))
			Dim AllUserId
			if userid <> "" Then
				size=int(6)
				''s Commented By : Sachinp@ecotech
				''s On           : 20 Jan 2010
				''s Purpose      : To add Loan No, Price fields
				''sNote          : for this i used irs_fee and for SSN i used ssn_fee
				if AllUserId <> "" Then
					AllUserId = AllUserId + "," & "'" & userid & "'"
				Else
					 AllUserId = "'" & userid & "'"
				End If
			end if
		Next
		if AllUserId <> "" Then
			''s sqllist = "SELECT fldTypeOfForm,fldtaxyear2008,fldtaxyear2007,fldtaxyear2006,fldtaxyear2005,fldtaxyear2004,fldStatus,Customer.UserId,fldRequestName,fldssnno,fldDeliveryDate,'cuName'= Customer.Name,'cuID'=Customer.userId from tblOrder inner join customer on tblOrder.fldCustomerID = customer.CustomerID where fldtypeofform < "&size&" and Customer.userId='"&userid&"' and not fldstatus = 'c' and fldDeliveryDate BETWEEN '" & dtFrom & "' AND '" & dtTo & "' order by fldDeliveryDate desc"
			sqllist = "SELECT fldTypeOfForm,fldtaxyear2008,fldtaxyear2007,fldtaxyear2006,fldtaxyear2005,fldtaxyear2004,fldStatus,Customer.UserId,fldRequestName,fldssnno,fldDeliveryDate,'cuName'= Customer.Name,'cuID'=Customer.userId,fldLoannumber,Customer.irs_fee,Customer.ssn_fee,rushRate"&_
						" from tblOrder inner join customer on tblOrder.fldCustomerID = customer.CustomerID where (fldtypeofform < "&size&" or fldtypeofform = 6) and Customer.userId IN ("& AllUserId &") and fldDeliveryDate BETWEEN '" & dtFrom & "' AND '" & dtTo & "' order by cuID"
			set rslist = server.CreateObject("adodb.recordset")
			rslist.CursorLocation = 3
			rslist.open sqllist,conn,2,2
			Dim intcounter
			Dim myLoanNumber()
			Dim myAmount()
			ReDim Preserve myLoanNumber(1)
			ReDim Preserve myAmount(1)
			
			if not rslist.EOF Then
				Dim totalCount
				totalCount = rslist.RecordCount
				intCounter = 0
			End if
			Do While Not rslist.EOF
					nameofForm = ""
					year1 = ""
					status = ""
					if trim(rslist("fldTypeofForm"))="1" then 
						nameofForm= "1040"
					elseif trim(rslist("fldTypeofForm"))="2" then 
						nameofForm= "1120"
					elseif trim(rslist("fldTypeofForm"))="3" then 
						nameofForm= "1065"
					elseif trim(rslist("fldTypeofForm"))="4" then 
						nameofForm= "W-2"
					elseif trim(rslist("fldTypeofForm"))="5" then 
						nameofForm= "1099"
					end if
					Dim CountYear
					CountYear = 0
					if trim(rslist("fldtaxyear2008"))="True" Then
						year1="2008" 
						CountYear = CountYear + 1
					end if
					if trim(rslist("fldtaxyear2007"))="True" then
						if(year1<>"")then
							year1=year1 & "-2007"
							CountYear = CountYear + 1
						else
							year1="2007"
							CountYear = CountYear + 1
						end if
					end if
					if trim(rslist("fldtaxyear2006"))="True" then
						if(year1<>"")then
							year1=year1 & "-2006"
							CountYear = CountYear + 1
						else
							year1="2006"
							CountYear = CountYear + 1
						end if
					end if
					if trim(rslist("fldtaxyear2005"))="True" then
						if(year1<>"")then
							year1=year1 & "-2005"
							CountYear = CountYear + 1
						else
							year1="2005"
							CountYear = CountYear + 1
						end if
					end if
					if trim(rslist("fldtaxyear2004"))="True" then
						if(year1<>"")then
							year1=year1 & "-2004"
							CountYear = CountYear + 1
						else
							year1="2004"
							CountYear = CountYear + 1
						end if
					end if
					Dim irsfee
					if rslist("rushRate") <> "" then
						rushrate = rslist("rushRate")
					else 
						rushrate = 0
					end if
					irsfee = rushrate + (rslist("irs_fee") * CountYear)
					if trim(rslist("fldstatus")) = "p" then
						status = "Pending"
					elseif trim(rslist("fldstatus")) = "s" then
						status = "Completed"
					elseif trim(rslist("fldstatus")) = "n" then
						status = "Completed"
					elseif trim(rslist("fldstatus")) = "m" then
						status = "Completed"
					elseif trim(rslist("fldstatus")) = "e" then
						status = "Completed"
					elseif trim(rslist("fldstatus")) = "i" then
				  		status = "Completed"
					elseif trim(rslist("fldstatus")) = "a" then
						status = "Completed"
					elseif trim(rslist("fldstatus")) = "d" then
						status = "Completed"
					elseif trim(rslist("fldstatus")) = "r" then
						status = "Completed"
					elseif trim(rslist("fldstatus")) = "u" then
						status = "Updated"
					elseif trim(rslist("fldstatus")) = "c" then
						status = "Cancelled"
					end if
				Dim flag
				if rslist("fldTypeofForm") = 6 Then
					ExportFile.writeline "<tr><td align='center' bgcolor='#C0C0C0'>" & rslist("cuID") & "</td><td align='center'>" & rslist("cuName") & "</td><td align='left'>" & checkencode(rslist("fldRequestName")) & "</td><td align='left'>" & rslist("fldLoannumber") & "</td><td align='left'><b>N/A</b></td><td align='left'><b>N/A</b></td><td align='left'>" & rslist("fldssnno") & "</td><td align='left'>" & status & "</td><td align='left'>" & rslist("fldDeliveryDate") & "</td><td align='left'>" & rslist("ssn_fee") & "</td></tr>"
				else
					ExportFile.writeline "<tr><td align='center' bgcolor='#C0C0C0'>" & rslist("cuID") & "</td><td align='center'>" & rslist("cuName") & "</td><td align='left'>" & checkencode(rslist("fldRequestName")) & "</td><td align='left'>" & rslist("fldLoannumber") & "</td><td align='left'>" & nameofForm & "</td><td align='left'>" & year1 & "</td><td align='left'>" & rslist("fldssnno") & "</td><td align='left'>" & status & "</td><td align='left'>" & rslist("fldDeliveryDate") & "</td><td align='left'>" & irsfee & "</td></tr>"
				End If
				rslist.MoveNext
			Loop
		End If
		ExportFile.writeline("</table>")
		Set rslist = Nothing
		Set ExportFile = Nothing
		Set fso = Nothing
		Set File = Server.CreateObject("ActiveFile.File")
		File.Name = path 
		Response.AddHeader "Content-Disposition", "attachment;filename=" & File.FileName
		Response.ContentType = "application/vnd.ms-excel"
		Response.Clear
		File.Download
%>
</form>
</body>
</html>
