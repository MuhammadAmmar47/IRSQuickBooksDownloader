<!--#include file="../header.asp"-->
<html>
<head>
	<title>:: IRSTaxRecords ::</title>
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
</head>

<script language="JavaScript">
</script>
<body bgcolor="#CCCCCC">
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
										<font size="3" face="Arial"><br><b>Customer Wise Orders Placed</b></font>
									</td>
								</tr>
								<tr valign="top">
									<td width="100%">
						        <table align=center cellspacing=0 cellpadding =0 border="0" ID="Table3" width="100%">
							        <tr><td>&nbsp;</td></tr>

								<%m_dtmFrom = Request("fromDate")
									m_dtmTo = Request("toDate")
									strSql1 = "SELECT CompanyName,Name,count(fldlisttype),fldlisttype FROM tblorder INNER JOIN "&_
									" Customer ON Customer.CustomerID = tblorder.fldcustomeriD "&_
									" WHERE fldOrderDate >= '"& m_dtmFrom &"' AND fldOrderDate < '"& DateAdd("d",1,m_dtmTo) &"'  "&_
									" AND fldCompanyID = '" & Request("Compid") & "' GROUP BY Name,CompanyName,fldlisttype"
								' 	Response.Write strSql1
									Set rs1 = server.CreateObject("ADODB.Recordset")
									rs1.Open strSql1,conn,3,2
								
									If rs1.EOF Then
								%>
									<tr>
										<td width="100%" align="center" colspan="2">
											<table bgcolor="" border="2" cellpadding="5" cellspacing="1" ID="Table6">
												<tr>
													<td><font size="2" face="Arial"><b>No Records Found.</td>
												</tr>
											</table>	
										</td>
									</tr>
								<%Else %>									
									<tr>
										<td width="100%" colspan="2">
											<table bgcolor="#000000" border="0" cellpadding="5" cellspacing="1" ID="Table4">
												<TR >
													<TD bgcolor="#808080" class="bodytextBOLDCenter" colspan="5"><font color=#ffffff>Comapany Name - <%=rs1(0)%></font></TD>
												</TR>
											<%
												Cnt = 0
												sname = ""
												While Not rs1.EOF
													If sname <> rs1(1) Then %>
													<tr bgcolor="#ffffff">
														<td>Name</td>
														<td align="center" bgcolor="#ffffcc" width="200px"><%=rs1(1)%></td>
														<%sname = rs1(1)%>
													</tr>
												<%End If 
													If rs1(3) = "1" Then %>		
													<tr bgcolor="#ffffff">
														<td>1040</td>									
														<TD align="center" bgcolor="#ffffcc" width="50px"><%=rs1(2)%>&nbsp;</TD>
												<%End If %>
													</tr>
												<%If rs1(3) = "2" Then %>		
													<tr bgcolor="#ffffff">
															<td>W2</td>									
															<TD align="center" bgcolor="#ffffcc" width="50px"><%=rs1(2)%>&nbsp;</TD>
														<%End If %>
													</tr>
												<%If rs1(3) = "3" Then%>		
													<tr bgcolor="#ffffff">
															<td>SSN</td>									
															<TD align="center" bgcolor="#ffffcc" width="50px"><%=rs1(2)%>&nbsp;</TD>
													<%End If %>
													</tr>
														<%Cnt = Cnt + rs1(2)
															rs1.MoveNext
													Wend
													Set rs1 = Nothing
												%>
												<tr>
													<td bgcolor="#ffffff">Total</td>
													<TD align="center" bgcolor="#ffffcc" width="50px"><b><%=Cnt%></b></TD>
													</TR>
												</tr>
												</table>
										</td>
									</tr>
								<%End If %>
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

</body>
</html>
