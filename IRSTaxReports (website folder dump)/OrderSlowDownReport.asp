<!--#include file="../header.asp"-->
<html>
<head>
	<title>:: IRSTaxRecords ::</title>
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
</head>

<body bgcolor="#CCCCCC">
<form name="frmPost" id="frmPost" method="post">
  <div id="pageLyr"> 

    <div id="mainLyr"> 
      <div id="pageBody"> 
      <table width="auto" height="auto" cellpadding="5">
      <tr><td>
				<table cellpadding="0" cellspacing="0" align="center" width="50%" border="0" ID="Table1">
					<tr height="30px;"><td>&nbsp;</td></tr>
					<tr valign="top">
						<td bgColor="#00FF00" width="20px" height="500px" bordercolor="#CCCCCC">&nbsp;</td>
						<td bgColor="#ffffff">
							<table ID="Table2">
								<tr>
									<td>
										<font size="3" face="Arial"><br><b>Orders Explorer</b></font>
									</td>
								</tr>
								<tr height="50px;"><td>&nbsp;</td></tr>
								<tr valign="top">
									<td>
						        <table align=center cellspacing=0 cellpadding =0 border="0" ID="Table3">
							        	        <!-- Added By Sachin Pawar -->
							        <tr>
										<td colspan="2"><font size="2" face="Arial"><b>&nbsp;</b></font>
										</td>
									</tr>
<% 
	Set objCmd = Server.CreateObject("ADODB.Command") 
	objCmd.ActiveConnection = conn 
	objCmd.CommandText = "sp_checkSlowDown" 
	objCmd.CommandType = adCmdStoredProc
	
	set rsorders1 = server.CreateObject("ADODB.Recordset")
	Set rsorders1 = objCmd.Execute
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
												<TD colspan="7" bgcolor="#c0c0c0" class="bodytextBOLDCenter">Company Wise - Order Slow Down Report</TD>
												</TR>
												<TR >
													<TD bgcolor="#808080" class="bodytextBOLDCenter"><font color=#ffffff>Comapany Name</font></TD>
													<TD bgcolor="#808080" class="bodytextBOLDCenter"><font color=#ffffff>Percentage Drop</font></TD>
													<TD bgcolor="#808080" class="bodytextBOLDCenter"><font color=#ffffff>Orders Previous Month/Current Month</font></TD>
													<TD bgcolor="#808080" class="bodytextBOLDCenter"><font color=#ffffff>Last Six Month Average</font></TD>
													<TD bgcolor="#808080" class="bodytextBOLDCenter"><font color=#ffffff>UserName</font></TD>
													<TD bgcolor="#808080" class="bodytextBOLDCenter"><font color=#ffffff>Contact Email</font></TD>
													<TD bgcolor="#808080" class="bodytextBOLDCenter"><font color=#ffffff>Phone Number</font></TD>
												</TR>
<%	While Not rsorders1.EOF 
	
%>

									
									
												<TR>
													<TD bgcolor="#ffffcc"><%=rsorders1("compName")%></TD>
													<TD align="center" bgcolor="#ffffcc"><%=rsorders1("percentageDrop") & "%"%></TD>
													<TD bgcolor="#ffffcc"><%=rsorders1("prevCount") & "/" & rsorders1("thisCount")%></td>
													<TD bgcolor="#ffffcc"><%=rsorders1("SixMonthAvg")%></td>
													<TD bgcolor="#ffffcc"><%=rsorders1("UName")%></td>
													    
													<TD bgcolor="#ffffcc"><a href="mailto:<%=rsorders1("UEmail")%>"><%=rsorders1("UEmail")%></a></td>
													<TD bgcolor="#ffffcc"><%=rsorders1("PhoneNumber")%></td>
												</TR>
										
<%
rsorders1.MoveNext
Wend %>

</table>	
</td>
</tr>
<% End If %>
<% Set rsorders1 = nothing%>
							        <!-- Ends Here -->
							    </table>
							</td>
							</tr>
			    	</table>
					</td>
				</tr>
			</table>
			</td></tr>
			</table>
  </div>
 </div>
</div>

</form>
<div id="spiffycalendar" class="text"></div>
</body>
</html>
