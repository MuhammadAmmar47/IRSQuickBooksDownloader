<!--#include file="../header.asp"-->

<%
'Set connect = Server.CreateObject("ADODB.Connection")
	'connect.Open g_kstrConnection
	
mon=Request.QueryString("mon")
if(mon="")then
	mon=month(now)
end if
Select case mon
	case 1:
		mnth="JAN"
		fullMnth="January"
	case 2:
		mnth="FEB"
		fullMnth="February"
	case 3:
		mnth="MAR"	
		fullMnth="March"
	case 4:
		mnth="APR"	
		fullMnth="April"
	case 5:
		mnth="MAY"	
		fullMnth="May"
	case 6:
		mnth="JUN"	
		fullMnth="June"
	case 7:
		mnth="JUL"	
		fullMnth="July"
	case 8:
		mnth="AUG"	
		fullMnth="August"
	case 9:
		mnth="SEP"	
		fullMnth="September"
	case 10:
		mnth="OCT"	
		fullMnth="October"
	case 11:
		mnth="NOV"	
		fullMnth="November"
	case 12:
		mnth="DEC"
		fullMnth="December"	
end select
yr=Request.QueryString("yr")
if(yr="")then
	yr=year(now)
end if
dim i,j
i=0
j=0
dim total(15),total0(15),total1(15),total2(15),total3(15),total4(15),total5(15),total6(15),total7(15),total8(15),total9(15)
dim totalq(15),totalq0(15),totalq1(15),totalq2(15),totalq3(15),totalq4(15),totalq5(15),totalq6(15),totalq7(15),totalq8(15),totalq9(15)

Function getDaysInMonth(strMonth,strYear)
		Dim strDays
		Select Case cint(strMonth)
			Case 1,3,5,7,8,10,12:
			  strDays = 31
			Case 4,6,9,11:
				 strDays = 30
			Case 2:
		        If  ((cint(strYear) Mod 4 = 0  and  _
			      cint(strYear) Mod 100 <> 0)  _
				  Or ( cint(strYear) mod 400 = 0) ) Then
				strDays = 29
				Else
					strDays = 28
				End If
		End Select 
		getDaysInMonth = strDays
	End Function
%>	
<html>
	<head>
		<title>:: IRSTaxRecords ::</title>
		<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<link rel="stylesheet" type="text/css" href="../include/spiffyCal.css">
			<script language="JavaScript" src="../include/spiffyCal.js"></script>
			<script language="JavaScript">
			function fnGo()
			{
				var mon=document.getElementById("cmbMonth").options[document.getElementById("cmbMonth").selectedIndex].value;
				var yr=document.frmPost.txtyear.value;
				document.frmPost.action = "monthlyReport.asp?mon="+mon+"&yr="+yr;
				document.frmPost.submit();
			}
			</script>
	</head>
	<body bgcolor="#cccccc">
		<%
	Function getDaysInMonth(strMonth,strYear)
		Dim strDays
		Select Case cint(strMonth)
			Case 1,3,5,7,8,10,12:
			  strDays = 31
			Case 4,6,9,11:
				 strDays = 30
			Case 2:
		        If  ((cint(strYear) Mod 4 = 0  and  _
			      cint(strYear) Mod 100 <> 0)  _
				  Or ( cint(strYear) mod 400 = 0) ) Then
				strDays = 29
				Else
					strDays = 28
				End If
		End Select 
		getDaysInMonth = strDays
	End Function

	Dim m_dtmFrom
	Dim m_dtmTo
	Dim strSQL
	m_dtmFrom = Request("fromdate")
	m_dtmTo = Request("todate")
' 	Response.Write m_dtmFrom & "::" & m_dtmTo

	If Request("fromdate") = "" Then 
		On Error Resume Next
		blnDefaultDate = CDate(m_dtmFrom) >= CDate(m_dtmTo)
		If Err > 0 Then blnDefaultDate = True
		On Error Goto 0
		If blnDefaultDate Then
			m_dtmFrom = CDate(DatePart("m", Now()) & "/1/" & right(DatePart("yyyy",Now()),4))
			m_monthDays = getDaysInMonth( CDate(DatePart("m", Now())), cDate(right(DatePart("yyyy",Now()),4)))
			m_dtmTo = CDate(DatePart("m", Now()) & "/"& m_monthDays &"/" & right(DatePart("yyyy",Now()),4))
		End If
	End IF
	
	sql = "Select count(fldordernumber) from tblorder where fldOrderDate> '"&DateAdd("d",-1,m_dtmFrom)&"' and fldOrderDate < '"& DateAdd("d",1,m_dtmTo) &"'"
' 	Response.Write sql
	Set rs = Server.CreateObject("ADODB.Recordset")
	rs.Open sql,conn, 3, 2
	If Not rs.EOF Then 
		recordcount = rs(0)
	End If 
%>
		<form name="frmPost" id="frmPost" method="post">
			<div id="pageLyr">
				<!-- main layer - starts -->
				<div id="mainLyr">
					<!-- page body - starts  -->
					<div id="pageBody">
						<table cellpadding="0" cellspacing="0" align="center" border="0" ID="Table1">
							<tr height="30">
								<td>&nbsp;</td>
							</tr>
							<tr valign="top">
								<td bgColor="#00ff00" width="20" height="500" bordercolor="#cccccc">&nbsp;</td>
								<td bgColor="#ffffff">
									<table ID="Table2" width="100%">
										<tr>
											<td>
												<font size="3" face="Arial">
													<br>
													<b>Monthly Report - Daily Orders Placed</b></font>
											</td>
										</tr>
										<tr height="50">
											<td>&nbsp;</td>
										</tr>
										<tr valign="top">
											<td>
												<table align="center" cellspacing="0" cellpadding="0" border="0" ID="Table3">
													<tr valign="top">
														<td width="10px" rowspan="15">&nbsp;</td>
														<td class='bodytextcenter' colspan="5"><font size="2" face="Arial"><b>&nbsp; Month:&nbsp;
															<select name="cmbMonth" class="field" id="cmbMonth">
																<option value="0">Select Month</option>
																<option <% if(mon=1)then %> selected <%End If%> value="1">January</option>	
																<option <% if(mon=2)then %> selected <%End If%> value="2">February</option>	
																<option <% if(mon=3)then %> selected <%End If%> value="3">March</option>	
																<option <% if(mon=4)then %> selected <%End If%> value="4">April</option>	
																<option <% if(mon=5)then %> selected <%End If%> value="5">May</option>	
																<option <% if(mon=6)then %> selected <%End If%> value="6">June</option>	
																<option <% if(mon=7)then %> selected <%End If%> value="7">July</option>	
																<option <% if(mon=8)then %> selected <%End If%> value="8">August</option>	
																<option <% if(mon=9)then %> selected <%End If%> value="9">September</option>	
																<option <% if(mon=10)then %> selected <%End If%> value="10">October</option>	
																<option <% if(mon=11)then %> selected <%End If%> value="11">November</option>	
																<option <% if(mon=12)then %> selected <%End If%> value="12">December</option>	
															</select>				
															&nbsp; Year: <input type="text" class="field" name="txtyear" maxlength=4 size=5 tabindex=22 value="<%=yr%>" ID="Text1"></b></font>
														<input type="button" name="submit2" value="GO" onclick="javascript:fnGo();" ID="Button1">
														</td>
													</tr>
													<tr>
														<td colspan="3">&nbsp;</td>
													</tr>
													<tr>
														<td width="10px" rowspan="15">&nbsp;</td>
														<td colspan="3"><font size="2" face="Arial"><b>&nbsp;</b></font>
														</td>
													</tr>
													<% 
														strsql1="select count(fldordernumber) from tblorder where month(fldorderdate)="& mon &" and year(fldorderdate)=" & yr
														Set rsorders1 = server.CreateObject("ADODB.Recordset")
														rsorders1.Open strsql1,conn,3,2
														If rsorders1(0) < 1 Then
													%>
													<tr>
														<td width="502px" align="center" colspan="4">
															<table bgcolor="" border="0" cellpadding="5" cellspacing="1" ID="Table6">
																<tr>
																	<td><font size="2" face="Arial"><b>No Records Found.</td>
																</tr>
															</table>	
														</td>
													</tr>
													<% Else %>
													
													<tr>
														<td width="10px" rowspan="15">&nbsp;</td>
														<td width="50%">
															<table bgcolor="#000000" border="0" cellpadding="5" cellspacing="1" ID="Table4">
																<TR>
																<TD colspan=3 bgcolor="#c0c0c0" class="bodytextBOLDCenter"><%=fullMnth%> - Order Placed Report</TD>
																</TR>
																<TR >
																	<TD bgcolor="#808080" class="bodytextBOLDCenter">Date</TD>
																	<TD bgcolor="#808080" class="bodytextBOLDCenter">Daily Total</TD>
																</TR>
																
																<%Dim totalDays,loopVar
																	totalDays = getDaysInMonth(mon,yr)
																	If (totalDays < 30) Then
																		loopVar = 15
																	Else
																		loopVar = round(totalDays/2)
																	End If
																	For i=1 To loopVar
																dt=mon & "/" & i & "/" & yr
																j=instr(1,formatdatetime(dt,1),",")
																day1=left(formatdatetime(dt,1),j-1)	
																%>
																<TR>
																<TD bgcolor="#ffffcc"><%=mon%>/<%=i%>/<%=yr%> (<%=day1%>)</TD>
																<%
																	strsql="select count(fldordernumber) from tblorder where month(fldorderdate)="& mon &" and day(fldorderdate) = "& i &" and year(fldorderdate)=" & yr
																	
																	Set rsorders=conn.execute(strsql)
																	
																	runingtot = rsorders(0) +runingtot 
																	runningper = Round((runingtot / rsorders1(0)) * 100,2)
																	
																%>
																<TD align="center" bgcolor="#ffffcc"><%=rsorders(0)%></TD>
																<!--<TD align="center" bgcolor="#ffffcc"><%=runningper%>% = <font size="-1"><%=runingtot%>/<%=rsorders1(0)%></font></TD>-->
																</TR>
																<% next 
																
																
																	set rsorders = nothing
																	set connect = nothing
																
																%>
														</table>	
														</td>
														<td width="10px" rowspan="15">&nbsp;</td>
														<td valign="top" width="50%">
															<table bgcolor="#000000" border="0" cellpadding="5" cellspacing="1" ID="Table5">
																<TR>
																<TD colspan=3 bgcolor="#c0c0c0" class="bodytextBOLDCenter">&nbsp;</TD>
																</TR>
																<TR >
																	<TD bgcolor="#808080" class="bodytextBOLDCenter">Date</TD>
																	<TD bgcolor="#808080" class="bodytextBOLDCenter">Daily Total</TD>
																</TR>
																
																<% 
																	strsql1="select count(fldordernumber) from tblorder where month(fldorderdate)="& mon &" and year(fldorderdate)=" & yr
																	'set rsorders1=conn.execute(strsql1)
																
																	totalDays = getDaysInMonth(mon,yr)
																	for i=(loopVar + 1) to totalDays
																	dt=mon & "/" & i & "/" & yr
																	j=instr(1,formatdatetime(dt,1),",")
																	day1=left(formatdatetime(dt,1),j-1)	
																%>
																<TR>
																<TD bgcolor="#ffffcc"><%=mon%>/<%=i%>/<%=yr%> (<%=day1%>)</TD>
																<%
																	'strsql="select count(fldorderid) FROM tblOrder LEFT OUTER JOIN tblInspectiondetails ON tblOrder.fldorderID = tblinspectiondetails.fldpropertyID  where convert(varchar(12),convert(datetime,fldstartdate,101))=convert(varchar(12),convert(datetime,'" & dt &"',101)) and fldstatus>=8 "
																	strsql="select count(fldordernumber) from tblorder where month(fldorderdate)="& mon &" and day(fldorderdate) = "& i &" and year(fldorderdate)=" & yr
																	'strsql="select count(tblorderstatus.flddate) from tblorderstatus where  fldstatusid=8 and convert(varchar(12),convert(datetime,flddate,101))=convert(varchar(12),convert(datetime,'" & dt &"',101))"
																	
																	set rsorders=conn.execute(strsql)
																	
																	runingtot = rsorders(0) +runingtot 
																	runningper = Round((runingtot / rsorders1(0)) * 100,2)
																	
																%>
																<TD align="center" bgcolor="#ffffcc"><%=rsorders(0)%></TD>
																<!--<TD align="center" bgcolor="#ffffcc"><%=runningper%>% = <font size="-1"><%=runingtot%>/<%=rsorders1(0)%></font></TD>-->
																</TR>
																<% next 
																
																
																	set rsorders = nothing
																	set connect = nothing
																
																%>
														</table>	
														</td>
													</tr>
													<% End If %>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<!-- page body - ends -->
					</div>
					<!-- main layer - ends -->
				</div>
				<!-- footer layer - starts -->
				<!-- footer layer - ends -->
				<!-- footer layer - ends -->
			</div>
			<!-- main page layer - ends -->
		</form>
		<div id="spiffycalendar" class="text"></div>
	</body>
</html>
