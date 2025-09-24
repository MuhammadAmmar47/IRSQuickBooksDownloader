<!--#include file="../header.asp"-->
<html>
<head>
	<title>:: IRSTaxRecords ::</title>
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
	<link rel="stylesheet" type="text/css" href="../include/spiffyCal.css">
	<script language="JavaScript" src="../include/spiffyCal.js"></script>
</head>

<script language="JavaScript">
var cal1 = new ctlSpiffyCalendarBox("cal1", "frmPost", "fromdate","btnDate1",""  ,0,0,15);
var cal2 = new ctlSpiffyCalendarBox("cal2", "frmPost", "todate","btnDate2",""  ,0,0,15);
function fnGo()
{
	var cname = document.getElementById("txtCompName").value;
	document.frmPost.action = "CompanyWiseReport.asp?CompName="+cname;
	document.frmPost.submit();
}
</script>

<body bgcolor="#CCCCCC">
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
	If Request("fromdate") = "" Then 
		On Error Resume Next
		blnDefaultDate = CDate(m_dtmFrom) >= CDate(m_dtmTo)
		If Err > 0 Then blnDefaultDate = True
		On Error Goto 0
		If blnDefaultDate Then
			'm_dtmFrom = CDate(DatePart("m", Now()) & "/1/" & right(DatePart("yyyy",Now()),4))
			m_dtmFrom = CDate(DatePart("m", Now()) & "/" & right(DatePart("d",Now()),2) & "/" & right(DatePart("yyyy",Now()),4))
 			m_monthDays = getDaysInMonth( CDate(DatePart("m", Now())), cDate(right(DatePart("yyyy",Now()),4)))
 			'm_dtmTo = CDate(DatePart("m", Now()) & "/"& m_monthDays &"/" & right(DatePart("yyyy",Now()),4))
 			m_dtmTo = CDate(DatePart("m", Now()) & "/" & right(DatePart("d",Now()),2) & "/" & right(DatePart("yyyy",Now()),4))
		End If
	End If
%>
<form name="frmPost" id="frmPost" method="post">
  <div id="pageLyr"> 

    <div id="mainLyr"> 
      <div id="pageBody"> 
				<table cellpadding="0" cellspacing="0" align="center" width="50%" border="0" ID="Table1">
					<tr height="30px;"><td>&nbsp;</td></tr>
					<tr valign="top">
						<td bgColor="#00FF00" width="20px" height="500px" bordercolor="#CCCCCC">&nbsp;</td>
						<td bgColor="#ffffff">
							<table ID="Table2">
								<tr>
									<td>
										<font size="3" face="Arial"><br><b>Company Wise Orders Placed</b></font>
									</td>
								</tr>
								<tr height="50px;"><td>&nbsp;</td></tr>
								<tr valign="top">
									<td>
						        <table align=center cellspacing=0 cellpadding =0 border="0" ID="Table3">
							        <tr valign="top">
								        <td>
													<script language="JavaScript" >cal1.writeControl(3,'');</script>
													<script language="JavaScript">
													document.frmPost.fromdate.value='<%=m_dtmFrom%>'
													</script>	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								        </td>
								        <td>
													<script language="JavaScript" >cal2.writeControl(3,'');</script>
													<script language="JavaScript">
													document.frmPost.todate.value='<%=m_dtmTo%>'
													</script>		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								        </td>
				        			</tr>
				        			<tr><td colspan="2">&nbsp;</td></tr>
									<tr valign="top">
								        <td colspan="2">
													 Name: <input type="text" runat="server" id="txtCompName"/>
													<input type="button" name="submit2" value="GO" onclick="javascript:fnGo();" ID="Button1">
													
								        </td>
				        			</tr>
							        <tr><td colspan="2">&nbsp;</td></tr>
							        
							        <!-- Added By Sachin Pawar -->
							        <tr>
										<td colspan="2"><font size="2" face="Arial"><b>&nbsp;</b></font>
										</td>
									</tr>
<% 
	strsql1="Select fldCompanyID,CompanyName from tblorder, Customer where Customer.CustomerID = tblorder.fldCompanyID and fldOrderDate >= '"& m_dtmFrom &"' and fldOrderDate < '"& DateAdd("d",1,m_dtmTo) &"'"
	If Request("CompName") <> "" then
		strwhere = " and Customer.CompanyName like '" & Request("CompName") & "%'"
	Else
		strwhere = ""
	end If
	strGrp = " GROUP BY fldCompanyID,CompanyName"
	
	strsql1 = strsql1 + strwhere + strGrp
	'Response.Write strsql1
	Set rsorders1 = server.CreateObject("ADODB.Recordset")
	rsorders1.Open strsql1,conn,3,2
	If rsorders1.EOF Then
%>
									<tr>
										<td width="502px" align="center" colspan="2">
											<table bgcolor="" border="0" cellpadding="5" cellspacing="1" ID="Table6">
												<tr>
													<td><font size="2" face="Arial"><b>No Records Found.</td>
												</tr>
											</table>	
										</td>
									</tr>
<% Else %>
									
									<tr>
										<td width="100%" colspan="2">
											<table bgcolor="#000000" border="0" cellpadding="5" cellspacing="1" ID="Table4">
												<TR>
												<TD colspan="6" bgcolor="#c0c0c0" class="bodytextBOLDCenter">Company Wise - Order Placed Report</TD>
												</TR>
												<TR >
													<TD bgcolor="#808080" class="bodytextBOLDCenter"><font color=#ffffff>Comapany Name</font></TD>
													<TD bgcolor="#808080" class="bodytextBOLDCenter"><font color=#ffffff>1040</font></TD>
													<TD bgcolor="#808080" class="bodytextBOLDCenter"><font color=#ffffff>W2</font></TD>
													<TD bgcolor="#808080" class="bodytextBOLDCenter"><font color=#ffffff>SSN</font></TD>
													<TD bgcolor="#808080" class="bodytextBOLDCenter"><font color=#ffffff>Orders Placed</font></TD>
													<TD bgcolor="#808080" class="bodytextBOLDCenter"><font color=#ffffff>Average</font></TD>
												</TR>
												<%
													DaysInRange = DateDiff("d",m_dtmFrom,m_dtmTo)
													while not rsorders1.EOF
													
													strsql2="Select fldCompanyID,count(tblorder.fldlisttype) from tblorder, Customer where Customer.CustomerID = tblorder.fldCompanyID and fldOrderDate >= '"& m_dtmFrom &"' and fldOrderDate < '"& DateAdd("d",1,m_dtmTo) &"' and fldlisttype = 1 and fldCompanyID = '" & rsorders1(0) & "' GROUP BY fldCompanyID"
		                       Set rsorders2 = server.CreateObject("ADODB.Recordset")
		                       rsorders2.Open strsql2,conn,3,2
		                       If Not rsorders2.EOF Then
		                           val1 = rsorders2(1)
		                       Else
		                           val1 = 0
		                       End If
		                       
		                       strsql3="Select fldCompanyID,count(tblorder.fldlisttype) from tblorder, Customer where Customer.CustomerID = tblorder.fldCompanyID and fldOrderDate >= '"& m_dtmFrom &"' and fldOrderDate < '"& DateAdd("d",1,m_dtmTo) &"' and fldlisttype = 2 and fldCompanyID = '" & rsorders1(0) & "' GROUP BY fldCompanyID"
		                       Set rsorders3 = server.CreateObject("ADODB.Recordset")
		                       rsorders3.Open strsql3,conn,3,2
		                       If Not rsorders3.EOF Then
		                           val2 = rsorders3(1)
		                       Else
		                           val2 = 0
		                       End If
		                       
		                       strsql4="Select fldCompanyID,count(tblorder.fldlisttype) from tblorder, Customer where Customer.CustomerID = tblorder.fldCompanyID and fldOrderDate >= '"& m_dtmFrom &"' and fldOrderDate < '"& DateAdd("d",1,m_dtmTo) &"' and fldlisttype = 3 and fldCompanyID = '" & rsorders1(0) & "' GROUP BY fldCompanyID"
		                       Set rsorders4 = server.CreateObject("ADODB.Recordset")
		                       rsorders4.Open strsql4,conn,3,2
		                       If Not rsorders4.EOF Then
		                           val3 = rsorders4(1)
		                       Else
		                           val3 = 0
		                       end If
	                                                
        									val = CInt(val1)+CInt(val2)+CInt(val3)
													Avg = val/(DaysInRange + 1)
												%>
												
												<TR>
													<TD bgcolor="#ffffcc"><%=rsorders1(1)%></TD>
<!-- 
													<TD align="center" bgcolor="#ffffcc"><a href="ViewCustomerDetails.asp?Compid=<%'=rsorders1(2)%>&fromDate=<%=m_dtmFrom%>&toDate=<%=m_dtmTo%>"><%'=rsorders1(1)%></a></TD>
													<TD bgcolor="#ffffcc"><%=Round(Avg,2)%></td>
 -->
													
													<TD bgcolor="#ffffcc"><%=val1%></td>
													    
													<TD bgcolor="#ffffcc"><%=val2%></td>
													<TD bgcolor="#ffffcc"><%=val3%></td>
													<TD align="center" bgcolor="#ffffcc"><a href="ViewCustomerDetails.asp?Compid=<%=rsorders1(0)%>&fromDate=<%=m_dtmFrom%>&toDate=<%=m_dtmTo%>"><%=val%></a></TD>
													<TD bgcolor="#ffffcc"><%=Round(Avg,2)%></td>
												</TR>
												<% 			
													rsorders1.MoveNext							
													wend
													set rsorders1 = nothing
													
												%>
										</table>	
										</td>
									</tr>
<% End If %>
							        <!-- Ends Here -->
							    </table>
							</td>
							</tr>
			    	</table>
					</td>
				</tr>
			</table>
  </div>
 </div>
</div>

</form>
<div id="spiffycalendar" class="text"></div>
</body>
</html>
