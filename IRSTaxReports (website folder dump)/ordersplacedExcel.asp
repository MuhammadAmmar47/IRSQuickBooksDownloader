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
	document.frmPost.method = "POST";
	document.frmPost.action = "ordersplacedExcelAction.asp";
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
			m_dtmFrom = CDate(DatePart("m", Now()) & "/1/" & right(DatePart("yyyy",Now()),4))
 			m_monthDays = getDaysInMonth( CDate(DatePart("m", Now())), cDate(right(DatePart("yyyy",Now()),4)))
 			m_dtmTo = CDate(DatePart("m", Now()) & "/"& m_monthDays &"/" & right(DatePart("yyyy",Now()),4))
' 			m_dtmTo = CDate(DatePart("m", Now()) & "/5/" & right(DatePart("yyyy",Now()),4))
		End If
	End If
	
	Set objCmd = Server.CreateObject("ADODB.Command")
	With objCmd
		Fromdate = DateAdd("d",-1,m_dtmFrom)
		Todate   = DateAdd("d",1,m_dtmTo)
	 	.ActiveConnection = conn 
	 	.CommandText = "count_years"
	 	.CommandType = adCmdStoredProc
	 	.Parameters.Append .CreateParameter("@fromdate",adVarChar, adParamInput, 12,Fromdate)  
	 	.Parameters.Append .CreateParameter("@todate", adVarChar, adParamInput, 12,Todate)  
	 	.Parameters.Append .CreateParameter("@cnt", adInteger, adParamOutput, , 0) 
	 	.Execute, , adExecuteNoRecords
	 	count = .Parameters("@cnt") 
	End With 
	Set objCmd = Nothing 
%>
<form name="frmPost" id="frmPost" method="post">
  <div id="pageLyr"> 

    <div id="mainLyr"> 
      <div id="pageBody"> 
				<table cellpadding="0" cellspacing="0" align="center" width="50%" border="0">
					<tr height="30px;"><td>&nbsp;</td></tr>
					<tr valign="top">
						<td bgColor="#00FF00" width="20px" height="500px" bordercolor="#CCCCCC">&nbsp;</td>
						<td bgColor="#ffffff">
							<table>
								<tr>
									<td>
										<font size="3" face="Arial"><br><b>Orders Placed</b></font>
									</td>
								</tr>
								<tr height="50px;"><td>&nbsp;</td></tr>
								<tr valign="top">
									<td>
						        <table align=center cellspacing=0 cellpadding =0 border="0" >
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
					        			<td width="13%"></td>
				        			</tr>
											<tr><td colspan="3">&nbsp;</td></tr>
										<tr>
							        		<td colspan="3">
							        			<input type="text" name="txtCust1" id="txtCust1" maxlength="5">
							        		</td>
										</tr>
										<tr><td colspan="3">&nbsp;</td></tr>
										<tr>
							        		<td colspan="3">
							        			<input type="text" name="txtCust2" id="txtCust2" maxlength="5">
							        		</td>
										</tr>
										<tr><td colspan="3">&nbsp;</td></tr>
										<tr>
							        		<td colspan="3">
							        			<input type="text" name="txtCust3" id="txtCust3" maxlength="5">
							        		</td>
										</tr>
										<tr><td colspan="3">&nbsp;</td></tr>
										<tr>
							        		<td colspan="3">
							        			<input type="text" name="txtCust4" id="txtCust4" maxlength="5">
							        		</td>
										</tr>
										<tr><td colspan="3">&nbsp;</td></tr>
										<tr>
							        		<td colspan="3">
							        			<input type="text" name="txtCust5" id="txtCust5" maxlength="5">
							        		</td>
										</tr>
										<tr><td colspan="3">&nbsp;</td></tr>
										<tr>
							        		<td colspan="3">
							        			<input type="text" name="txtCust6" id="txtCust6" maxlength="5">
							        		</td>
										</tr>
										<tr><td colspan="3">&nbsp;</td></tr>
										<tr>
							        		<td colspan="3">
							        			<input type="text" name="txtCust7" id="txtCust7" maxlength="5">
							        		</td>
										</tr>
										<tr><td colspan="3">&nbsp;</td></tr>
										<tr>
							        		<td colspan="3">
							        			<input type="text" name="txtCust8" id="txtCust8" maxlength="5">
							        		</td>
										</tr>
										<tr><td colspan="3">&nbsp;</td></tr>
										<tr>
							        		<td colspan="3">
							        			<input type="text" name="txtCust9" id="txtCust9" maxlength="5">
							        		</td>
										</tr>
										<tr><td colspan="3">&nbsp;</td></tr>
										<tr>
							        		<td colspan="3">
							        			<input type="text" name="txtCust10" id="txtCust10" maxlength="5">
							        		</td>
										</tr>
										<tr>
											<td>&nbsp;</td>
											<td>&nbsp;</td>
											<td><input type="button" name="btnSubmit" value="GO" onclick="javascript:fnGo();" ID="btnSubmit"></td>
										</tr>
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
