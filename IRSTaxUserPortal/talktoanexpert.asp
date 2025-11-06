<%@ LANGUAGE="VBScript" %>
<%
Option Explicit

'--------------- Helpers ---------------
Function H(s)
  If IsNull(s) Then s = ""
  H = Server.HTMLEncode(CStr(s))
End Function

Function IsBlank(s)
  IsBlank = (Trim(CStr(s & "")) = "")
End Function

Function IsValidEmail(e)
  Dim atPos, dotPos
  e = CStr(e & "")
  atPos = InStr(1, e, "@")
  dotPos = InStrRev(e, ".")
  IsValidEmail = (atPos > 1 And dotPos > atPos + 1 And dotPos < Len(e))
End Function

'--------------- State ---------------
Dim isPost : isPost = (UCase(Request.ServerVariables("REQUEST_METHOD")) = "POST")
Dim yourName, companyName, phoneDirect, emailAddr
Dim errName, errCompany, errPhone, errEmail, sendError, sentOK
sentOK = False

yourName    = Trim(Request.Form("yourName"))
companyName = Trim(Request.Form("companyName"))
phoneDirect = Trim(Request.Form("phoneDirect"))
emailAddr   = Trim(Request.Form("emailAddr"))

If isPost Then
  If IsBlank(yourName)    Then errName    = "Please enter your name."
  If IsBlank(companyName) Then errCompany = "Please enter your company."
  If IsBlank(phoneDirect) Then errPhone   = "Please enter your direct phone."
  If IsBlank(emailAddr) Or Not IsValidEmail(emailAddr) Then errEmail = "Please enter a valid email address."

  If errName = "" And errCompany = "" And errPhone = "" And errEmail = "" Then
    On Error Resume Next

    ' Build HTML email body
    Dim bodyHtml
    bodyHtml = "<div style=""font-family:Arial,Helvetica,sans-serif;font-size:14px;line-height:1.45;color:#0b1324"">" & _
               "<h2 style=""margin:0 0 10px 0;color:#00498C;font-size:18px"">New &ldquo;Talk to an Expert&rdquo; request</h2>" & _
               "<table role=""presentation"" cellpadding=""6"" cellspacing=""0"" border=""0"" style=""border-collapse:collapse;background:#f7f9fc;border:1px solid #e6ecf2;border-radius:8px"">" & _
               "<tr><td><b>Name</b></td><td>" & H(yourName) & "</td></tr>" & _
               "<tr><td><b>Company</b></td><td>" & H(companyName) & "</td></tr>" & _
               "<tr><td><b>Direct Phone</b></td><td>" & H(phoneDirect) & "</td></tr>" & _
               "<tr><td><b>Email</b></td><td>" & H(emailAddr) & "</td></tr>" & _
               "</table>" & _
               "<div style=""margin-top:12px;font-size:12px;color:#5f6b7a"">" & _
               "Source IP: " & H(Request.ServerVariables("REMOTE_ADDR")) & "<br>" & _
               "Submitted: " & H(CStr(Now())) & "</div></div>"

    ' CDO config
    Dim cfg, sch, msg
    Set cfg = CreateObject("CDO.Configuration")
    sch = "http://schemas.microsoft.com/cdo/configuration/"
    With cfg.Fields
      .Item(sch & "sendusing")        = 2      ' cdoSendUsingPort
      .Item(sch & "smtpserver")       = "mail.irstaxrecords.com"
      .Item(sch & "smtpserverport")   = 25     ' change to 587 if required
      .Item(sch & "smtpauthenticate") = 1
      .Item(sch & "sendusername")     = "admin@irstaxrecords.com"
      .Item(sch & "sendpassword")     = "newpass1515"
      .Item(sch & "smtpusessl")       = False  ' True if server requires TLS
      .Update
    End With

    Set msg = CreateObject("CDO.Message")
    Set msg.Configuration = cfg
    msg.From    = """IRStaxRecords.com"" <admin@IRSTAXRECORDS.com>"
    msg.To      = "grant23@icloud.com"
    msg.Subject = "Talk to an Expert request"
    msg.HTMLBody = bodyHtml
    msg.Send

    If Err.Number <> 0 Then
      sendError = "We couldn't send your request. (" & Err.Description & ")"
    Else
      sentOK = True
    End If

    On Error GoTo 0
    Set msg = Nothing
    Set cfg = Nothing
  End If
End If
%>
<!doctype html>
<html lang="en">
<head>
<meta charset="utf-8" />
<meta name="viewport" content="width=device-width,initial-scale=1" />
<title>Talk to an Expert</title>
<style>
  .font-10 { font-size:10pt; line-height:1.25; }
</style>



<style>
  :root{ --brand:#00498C; --accent:#f7b500; --ink:#0b1324; --panel:#f7f9fc; --ring:rgba(0,0,0,.08); }
  body{ margin:0; padding:0; background:#00468C; color:var(--ink); font-family:Arial,Helvetica,sans-serif; }
  .wrap{ max-width:720px; margin:48px auto; padding:0 16px; }
  .card{ background:#fff; border:1px solid #e6ecf2; border-radius:16px; box-shadow:0 8px 24px rgba(2,12,27,.06); overflow:hidden; }
  .card-h{ background:linear-gradient(180deg, rgba(0,73,140,.08), rgba(0,73,140,0)); padding:20px 24px; border-bottom:1px solid #e6ecf2; }
  .title{ margin:0; font-size:22px; color:var(--brand); }
  .card-b{ padding:24px; }
  label{ display:block; font-weight:bold; margin:12px 0 6px; }
  input[type="text"], input[type="tel"], input[type="email"]{ width:80%; padding:12px 14px; border:1px solid #cfd8e3; border-radius:10px; font-size:16px; outline:none; transition:.15s border-color ease; }
  input:focus{ border-color:var(--brand); box-shadow:0 0 0 3px rgba(0,73,140,.12); }
  .hint{ font-size:12px; color:#5f6b7a; margin-top:4px; }
  .errors{ background:#fff4f4; border:1px solid #ffd3d3; color:#8b0000; padding:12px 14px; border-radius:10px; margin-bottom:12px; }
  .btn-row{ display:flex; justify-content:flex-end; gap:12px; margin-top:18px; }
  .btn-ghost{ display:inline-block; padding:12px 20px; font-weight:600; text-decoration:none; border:2px solid var(--brand); color:var(--brand); background:transparent; border-radius:999px; transition:background .15s ease, color .15s ease, transform .06s ease; cursor:pointer; }
  .btn-ghost:hover{ background:var(--brand); color:#fff; }
  .btn-ghost:active{ transform:translateY(1px); }
  .note{ font-size:12px; color:#5f6b7a; margin-top:10px; }
  .thanks{ font-size:16px; line-height:1.5; }
</style>
</head>
<body>
  <div class="wrap">
    <div class="card">
      <div class="card-h">
        <h1 class="title"><img border="0" src="https://www.irstaxrecords.com/img/logosm.gif"><br><% If sentOK Then Response.Write("Successfully Submitted. Thank you " & H(yourName) & "!") Else Response.Write("Talk to an Expert") End If %></h1>
        <% If Not sentOK Then %><div class="hint">Share your contact details and we’ll connect you with a resident expert.</div><% End If %>
      </div>
      <div class="card-b">
        <% If sentOK Then %>
          <div class="thanks">
            <p>Your contact details have been sent to our team.<br>
               One of our resident experts will contact you shortly.</p>
            <p></p>
            <%
Function FirstNameOnly(n)
  Dim s, p
  s = Trim(CStr(n & ""))
  If s = "" Then FirstNameOnly = "" : Exit Function
  ' If name is "Last, First..."
  p = InStr(1, s, ",")
  If p > 0 Then s = Trim(Mid(s, p + 1))
  ' Collapse double spaces
  Do While InStr(s, "  ") > 0 : s = Replace(s, "  ", " ") : Loop
  FirstNameOnly = Split(s, " ")(0)
End Function
%>

<p>In the meantime <%= H( FirstNameOnly(yourName) ) %>, would you like to lean more details about
<a href="https://www.irstaxrecords.com/expand7.asp">Our Services?</a>&nbsp;&nbsp;</p>

<p>Get started today by <a href="https://www.irstaxrecords.com/wizard7.asp">Creating
an account</a></p><p>
<h1 class="trust"><img border="0" src="https://www.irstaxrecords.com/img/trustfootersm.jpg">
          </div>
        <% Else %>
          <% If sendError <> "" Or errName<>"" Or errCompany<>"" Or errPhone<>"" Or errEmail<>"" Then %>
            <div class="errors">
              <%= H(sendError) %>
              <% If errName<>"" Then Response.Write("<div>" & H(errName) & "</div>") %>
              <% If errCompany<>"" Then Response.Write("<div>" & H(errCompany) & "</div>") %>
              <% If errPhone<>"" Then Response.Write("<div>" & H(errPhone) & "</div>") %>
              <% If errEmail<>"" Then Response.Write("<div>" & H(errEmail) & "</div>") %>
            </div>
          <% End If %>

          <form method="post" action="<%= H(Request.ServerVariables("SCRIPT_NAME")) %>" novalidate>
            <label for="yourName">Your Name</label>
            <input type="text" id="yourName" name="yourName" value="<%= H(yourName) %>" required>

            <label for="companyName">Your Company name</label>
            <input type="text" id="companyName" name="companyName" value="<%= H(companyName) %>" required>

            <label for="phoneDirect">Your direct contact phone number</label>
            <input type="tel" id="phoneDirect" name="phoneDirect" value="<%= H(phoneDirect) %>" required>

            <label for="emailAddr">Your email address</label>
            <input type="email" id="emailAddr" name="emailAddr" value="<%= H(emailAddr) %>" required>

            <div class="btn-row">
              <button type="submit" class="btn-ghost">Submit</button>
            </div>
          </form>

<div class="font-10">
  &nbsp;<%= Stamp12() %> ET
</div>


<%
Function Stamp12()
  Dim d, hh, mm, mer
  d = Now() ' or DateAdd("h", 2, Now()) for +2 hours
  hh = Hour(d) : mm = Minute(d)
  If hh >= 12 Then mer = "pm" Else mer = "am"
  If hh = 0 Then
    hh = 12
  ElseIf hh > 12 Then
    hh = hh - 12
  End If
  Stamp12 = Month(d) & "/" & Day(d) & "/" & Year(d) & _
            " " & hh & ":" & Right("0" & mm, 2) & " " & mer
End Function
%>



          <div class="note"></div>
        <% End If %>
      </div>
    </div>
  </div>
</body>
</html>
