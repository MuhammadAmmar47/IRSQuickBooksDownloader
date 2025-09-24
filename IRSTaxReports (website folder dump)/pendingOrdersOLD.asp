<!--#include file="../header.asp"-->
<html>
<head>
	<title>:: IRSTaxRecords ::</title>
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
	<link rel="stylesheet" type="text/css" href="../include/spiffyCal.css">
	<script language="JavaScript" src="../include/spiffyCal.js"></script>
</head>

<script language="JavaScript">
var cal1 = new ctlSpiffyCalendarBox("cal1", "frmPost", "todate","btnDate1",""  ,0,0,15);
</script>

<body bgcolor="#CCCCCC">
<%
	Dim m_dtmTo
	Dim strSQL
	
	if not IsEmpty(Request.Form("btnSubmit")) then
		If Request("todate") = "" Then 
			If blnDefaultDate Then
 				m_dtmTo = CDate(DatePart("m", Now()) & "/"& DatePart("d", Now()) &"/" & right(DatePart("yyyy",Now()),4))
			End If
		Else
			m_dtmTo = Request("todate")
		End If
		Todate = m_dtmTo
		Dim cSql
		cSql = "select fldrequestname,fldssnno,fldTypeofform,fldTaxyear2005,fldTaxyear2006,fldTaxyear2007,fldTaxyear2008,fldtaxyear2004 From tblorder where fldOrderDate <= '"& Todate &"' and fldOrderDate > '12/31/2009' AND fldStatus = 'p' AND fldTypeofform in (1,2,3) ORDER BY fldOrderdate desc"

'		Response.Write cSql

		set rsUid = Server.CreateObject("Adodb.Recordset")
		rsUid.open cSql,conn,3,2 
		
		dim filesys, filetxt
		Const ForReading = 1, ForWriting = 2, ForAppending = 8
		Set filesys = CreateObject("Scripting.FileSystemObject")
		dim filePath
		filePath = server.MapPath("txt") & "\" & "pendingOrders.txt"
		Set filetxt = filesys.OpenTextFile(filePath, ForWriting, True)
		if not rsUid.EOF then
			filetxt.WriteLine("1040 Requests:")
			filetxt.WriteLine("")				
			Dim i
			i = 0
			Dim str
			while not rsUid.EOF 
				Dim year1
				year1 = ""
				if trim(rsUid("fldtaxyear2008"))="True" then
					year1="2008" 
				end if
				if trim(rsUid("fldtaxyear2007"))="True" then
					if(year1<>"")then
						year1=year1 & ", 2007"
					else
						year1="2007"
					end if
				end if
									
				if trim(rsUid("fldtaxyear2006"))="True" then
					if(year1<>"")then
						year1=year1 & ", 2006"
					else
						year1="2006"
					end if
				end if
				if trim(rsUid("fldtaxyear2005"))="True" then
					if(year1<>"")then
						year1=year1 & ", 2005"
					else
						year1="2005"
					end if
				end if
				if trim(rsUid("fldtaxyear2004"))="True" then
					if(year1<>"")then
						year1=year1 & ", 2004"
					else
						year1="2004"
					end if
				end if
				'Dim strFullName,arrName, strLastName
				'if rsUid("fldrequestname") <> "" Then
				'	strFullName = rsUid("fldrequestname")
				'	arrName = Split(strFullName," ")
				'	strLastName = arrName(1)
				'else
				'	strLastName = ""
				'end if
				
				str = i+1 & "." & " " & VbCr & Trim(rsUid("fldrequestname")) & " " & VbCr & Trim(rsUid("fldssnno")) & " " & VbCr & year1
				filetxt.WriteLine(str)				
				i=i+1
				rsUid.movenext
			wend
		End if
		cSql = "select fldrequestname,fldssnno,fldTypeofform,fldTaxyear2005,fldTaxyear2006,fldTaxyear2007,fldTaxyear2008,fldtaxyear2004 From tblorder where fldOrderDate <= '"& Todate &"' and fldOrderDate > '12/31/2009' AND fldStatus = 'p' AND fldTypeofform in (4,5) ORDER BY fldOrderdate desc"
		set rsUid = Server.CreateObject("Adodb.Recordset")
		rsUid.open cSql,conn,3,2
		if not rsUid.EOF then
			filetxt.WriteLine("")
			filetxt.WriteLine("")
			filetxt.WriteLine("W2 Requests:")
			filetxt.WriteLine("")				
			i = 0
			while not rsUid.EOF 
				year1 = ""
				if trim(rsUid("fldtaxyear2008"))="True" then
					year1="2008" 
				end if
				if trim(rsUid("fldtaxyear2007"))="True" then
					if(year1<>"")then
						year1=year1 & ", 2007"
					else
						year1="2007"
					end if
				end if
									
				if trim(rsUid("fldtaxyear2006"))="True" then
					if(year1<>"")then
						year1=year1 & ", 2006"
					else
						year1="2006"
					end if
				end if
				if trim(rsUid("fldtaxyear2005"))="True" then
					if(year1<>"")then
						year1=year1 & ", 2005"
					else
						year1="2005"
					end if
				end if
				if trim(rsUid("fldtaxyear2004"))="True" then
					if(year1<>"")then
						year1=year1 & ", 2004"
					else
						year1="2004"
					end if
				end if
				'if rsUid("fldrequestname") <> "" Then
				'	strFullName = rsUid("fldrequestname")
				'	arrName = Split(strFullName," ")
				'	strLastName = arrName(1)
				'else
				'	strLastName = ""
				'end if
				str = i+1 & "." & " " & VbCr & Trim(rsUid("fldrequestname")) & " " & VbCr & Trim(rsUid("fldssnno")) & " " & VbCr & year1
				filetxt.WriteLine(str)				
				i=i+1
				rsUid.movenext
			wend
		End if
		filetxt.Close
		rsUid.close()
		call DownloadFile(filePath)
		if filesys.FileExists(filePath) then
			filesys.DeleteFile(filepath)
		end if
		Response.End
	Else
		Todate = date()
	End If
%>
<form name="frmPost" id="frmPost" action=pendingOrders.asp method="post">
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
										<font size="3" face="Arial"><br><b>Pending Orders</b></font>
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
													document.frmPost.todate.value='<%=Todate%>'
													</script>	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								        </td>
								        <td width="13%" colspan="2" align="left"><input type="submit" name="btnsubmit" value="GO"></td>
				        			</tr>
									<tr><td colspan="3">&nbsp;</td></tr>
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
<%
	'SUB TO DOWNLOAD THE FILE
	Private Sub DownloadFile(file)
 	    '--declare variables
 	    Dim strAbsFile
 	    Dim strFileExtension
 	    Dim objFSO
 	    Dim objFile
 	    Dim objStream
 	    '-- set absolute file location
 	    strAbsFile = file
 	    '-- create FSO object to check if file exists and get properties
 	    Set objFSO = Server.CreateObject("Scripting.FileSystemObject")
 	    '-- check to see if the file exists
 	    If objFSO.FileExists(strAbsFile) Then
 	        Set objFile = objFSO.GetFile(strAbsFile)
 	        '-- first clear the response, and then set the appropriate headers
 	        Response.Clear
 	        '-- the filename you give it will be the one that is shown
 	        ' to the users by default when they save
 	        Response.AddHeader "Content-Disposition", "attachment; filename=" & objFile.Name
 	        Response.AddHeader "Content-Length", objFile.Size
 	        Response.ContentType = "application/octet-stream"
 	        Set objStream = Server.CreateObject("ADODB.Stream")
 	        objStream.Open
 	        '-- set as binary
 	        objStream.Type = 1
	        Response.CharSet = "UTF-8"
 	        '-- load into the stream the file
 	        objStream.LoadFromFile(strAbsFile)
 	        '-- send the stream in the response
 	        Response.BinaryWrite(objStream.Read)
 	        objStream.Close
 	        Set objStream = Nothing
 	        Set objFile = Nothing
 	    Else 'objFSO.FileExists(strAbsFile)
 	        Response.Clear
 	        Response.Write("No such file exists.")
 	    End If
 	    Set objFSO = Nothing
 	End Sub
%>