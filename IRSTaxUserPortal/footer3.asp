<tr>
 <td width="4%" height="19"></td>
 <td width="2%" height="19" align="center"></td>
 <td width="47%" height="19">
 <p align="right"><font face="Arial" size="1">NOT LOGGED IN&nbsp; 
 <%
  NewDate = DateAdd("h", -0, Now)
  nd = split (NewDate, " ")
  tm = split (nd(1), ":") 
 %>
  I.P. Address <%=request.serverVariables("REMOTE_HOST")%>&nbsp;&nbsp;<%=tm(0) &":" & tm(1)& " " & nd(2)%> EST</font></td>
</tr>