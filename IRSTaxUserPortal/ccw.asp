<%@ Language=VBScript %>
<%
Option Explicit
Response.Expires = -1

' --- Build today's date strings: 10-30-2025 and 2025-10-30 ---
Dim d, mm, dd, yyyy, date_mdy, date_ymd
d        = Date()
mm       = Right("0" & Month(d), 2)
dd       = Right("0" & Day(d), 2)
yyyy     = Year(d)
date_mdy = mm & "-" & dd & "-" & yyyy     ' e.g., 10-30-2025
date_ymd = yyyy & "-" & mm & "-" & dd     ' e.g., 2025-10-30

'========================= CONFIG: Fortune rates scrape =========================
Dim SRC_URL, CACHE_FILE
SRC_URL       = "https://fortune.com/article/current-mortgage-rates-" & date_mdy & "/"
CACHE_FILE    = "C:\IRSPDF\cache\fortune_rates_" & date_ymd & ".html"
Const CACHE_MINUTES = 60

Dim LABELS : LABELS = Array("30-year","15-year","Jumbo","FHA","VA","5/1 ARM")

'========================= CONFIG: Email (Contact Support) ======================
Const CUSTOMER_SUPPORT_EMAIL = "admin@irstaxrecords.com"
Const CUSTOMER_SUPPORT_NAME  = "IRSTAXRECORDS.com"
Const EMAIL_FROM_EMAIL       = "admin@IRSTAXRECORDS.com"
Const EMAIL_FROM_NAME        = "IRStaxRecords.com"
Const EMAIL_PASSWORD         = "newpass1515"
Const EMAIL_SERVER           = "mail.irstaxrecords.com"
Const EMAIL_USERNAME         = "admin@irstaxrecords.com"
Const SAVE_PATH              = "C:\IRSPDF\IRINPUT\"
Const DESTINATION_TO         = "grant23@icloud.com"

'========================= CONFIG: RSS (Mortgage News Daily) ====================
Const FEED_NEWS_URL = "http://www.mortgagenewsdaily.com/rss/news"
Const FEED_MBS_URL  = "http://www.mortgagenewsdaily.com/rss/mbs"
Const MAX_ITEMS     = 6

'========================= HELPERS (one copy only) =============================
Function H(s) : If IsNull(s) Or IsEmpty(s) Then H = "" Else H = Server.HTMLEncode(CStr(s)) : End If : End Function

Function ReadCache(path, minutes)
  On Error Resume Next
  Dim fso, f, txt
  Set fso = CreateObject("Scripting.FileSystemObject")
  If fso.FileExists(path) Then
    Set f = fso.GetFile(path)
    If DateDiff("n", f.DateLastModified, Now) <= minutes Then
      Dim ts : Set ts = fso.OpenTextFile(path, 1, False, -1)
      txt = ts.ReadAll : ts.Close
      ReadCache = txt
    End If
  End If
  On Error GoTo 0
End Function

Sub WriteCache(path, text)
  On Error Resume Next
  Dim fso, ts, folder
  Set fso = CreateObject("Scripting.FileSystemObject")
  folder = Left(path, InStrRev(path, "\"))
  If Not fso.FolderExists(folder) Then fso.CreateFolder folder
  Set ts = fso.OpenTextFile(path, 2, True, -1)
  ts.Write text : ts.Close
  On Error GoTo 0
End Sub

Function Fetch(url)
  On Error Resume Next
  Dim http : Set http = Server.CreateObject("MSXML2.ServerXMLHTTP.6.0")
  http.open "GET", url, False
  http.setRequestHeader "User-Agent", "IRStaxRecordsRatesEdu/1.0 (+https://www.irstaxrecords.com)"
  http.setRequestHeader "Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"
  http.setRequestHeader "Accept-Language", "en-US,en;q=0.9"
  http.setTimeouts 5000,5000,15000,15000
  http.send
  If http.status = 200 Then Fetch = http.responseText Else Fetch = ""
  Set http = Nothing
  On Error GoTo 0
End Function

Function FindRateNear(html, label)
  Dim pos, window, re, m, rate
  FindRateNear = ""
  If Len(html) = 0 Then Exit Function
  pos = InStr(1, html, label, vbTextCompare)
  If pos = 0 Then Exit Function
  window = Mid(html, pos, 600)
  Set re = New RegExp
  re.Global = False : re.IgnoreCase = True
  re.Pattern = "([0-9]{1,2}\.[0-9]{2,3})\s*%"
  Set m = re.Execute(window)
  If m.Count > 0 Then
    rate = m(0).SubMatches(0)
    FindRateNear = rate & "%"
  End If
  Set re = Nothing
End Function

Function CleanHeader(s)
  Dim t : t = CStr("" & s)
  t = Replace(t, vbCr, " ")
  t = Replace(t, vbLf, " ")
  t = Replace(t, ":", "")
  CleanHeader = t
End Function

Function FirstNameOf(s)
  Dim parts : parts = Split(Trim(CStr(s & "")))
  If UBound(parts) >= 0 Then FirstNameOf = parts(0) Else FirstNameOf = ""
End Function

Function HhMmAmPm(d)
  Dim hh, mm, mer
  hh = Hour(d) : mm = Minute(d) : mer = "am"
  If hh >= 12 Then mer = "pm"
  If hh = 0 Then
    hh = 12
  ElseIf hh > 12 Then
    hh = hh - 12
  End If
  HhMmAmPm = hh & ":" & Right("0" & mm, 2) & " " & mer
End Function

Function NewGuid()
  On Error Resume Next
  Dim g : g = CreateObject("Scriptlet.TypeLib").Guid
  If Err.Number = 0 Then
    g = Replace(Replace(g, "{",""), "}","")
    NewGuid = g
  Else
    Randomize : NewGuid = Replace(CStr(Timer * 1000000), ".", "")
  End If
  On Error GoTo 0
End Function

Sub SafeSaveToDisk(content)
  On Error Resume Next
  Dim fso, fn, ts, folderPath
  folderPath = SAVE_PATH
  Set fso = CreateObject("Scripting.FileSystemObject")
  If fso.FolderExists(folderPath) Then
    fn = folderPath & "support_" & Year(Now) & Right("0" & Month(Now),2) & Right("0" & Day(Now),2) & "_" & NewGuid() & ".txt"
    Set ts = fso.CreateTextFile(fn, True, True)
    ts.Write content : ts.Close
  End If
  Set ts = Nothing : Set fso = Nothing
  On Error GoTo 0
End Sub

Function RowHtml(k, v)
  RowHtml = "<tr><td style='padding:6px 8px;border-bottom:1px solid #eee;width:180px;font-weight:bold'>" & _
            H(k) & "</td><td style='padding:6px 8px;border-bottom:1px solid #eee'>" & v & "</td></tr>"
End Function

Function SendWith(port, ssl, htmlBody, replyTo, ByRef errOut)
  On Error Resume Next
  Dim cfg, sch, msg
  Set cfg = CreateObject("CDO.Configuration")
  sch = "http://schemas.microsoft.com/cdo/configuration/"
  With cfg.Fields
    .Item(sch & "sendusing")        = 2
    .Item(sch & "smtpserver")       = EMAIL_SERVER
    .Item(sch & "smtpserverport")   = port
    .Item(sch & "smtpauthenticate") = 1
    .Item(sch & "sendusername")     = EMAIL_USERNAME
    .Item(sch & "sendpassword")     = EMAIL_PASSWORD
    .Item(sch & "smtpusessl")       = ssl
    .Item(sch & "smtpconnectiontimeout") = 30
    .Update
  End With
  Set msg = CreateObject("CDO.Message")
  Set msg.Configuration = cfg
  msg.From    = EMAIL_FROM_NAME & " <" & EMAIL_FROM_EMAIL & ">"
  msg.To      = DESTINATION_TO
  msg.BCC     = CUSTOMER_SUPPORT_EMAIL
  msg.Subject = "Customer Support Request - " & Date & " " & HhMmAmPm(Now)
  If Len(Trim(CStr(replyTo)))>0 Then msg.ReplyTo = CleanHeader(replyTo)
  msg.HTMLBody = htmlBody
  msg.Send
  If Err.Number <> 0 Then
    errOut = "CDO send failed on port " & port & IIf(ssl," (SSL)","") & " — " & Err.Number & " — " & Err.Description
    Err.Clear : SendWith = False
  Else
    SendWith = True
  End If
  Set msg = Nothing : Set cfg = Nothing
  On Error GoTo 0
End Function

Function SendSupportMail(htmlBody, replyTo, ByRef errOut)
  Dim ports(2), useSSL(2), j, ok
  ports(0)=587: useSSL(0)=False
  ports(1)=25 : useSSL(1)=False
  ports(2)=465: useSSL(2)=True
  ok = False
  For j = 0 To UBound(ports)
    ok = SendWith(ports(j), useSSL(j), htmlBody, replyTo, errOut)
    If ok Then Exit For
  Next
  SendSupportMail = ok
End Function

Function LoadFeed(url)
  On Error Resume Next
  Dim http, dom
  Set http = Server.CreateObject("MSXML2.ServerXMLHTTP.6.0")
  http.open "GET", url, False
  http.setRequestHeader "User-Agent", "IRStaxRecordsFeedBot/1.0"
  http.send
  If http.status = 200 Then
    Set dom = Server.CreateObject("MSXML2.DOMDocument.6.0")
    dom.async = False
    dom.setProperty "SelectionLanguage", "XPath"
    If dom.LoadXML(http.responseText) Then
      Set LoadFeed = dom
      Exit Function
    End If
  End If
  Set LoadFeed = Nothing
  On Error GoTo 0
End Function

'========================= FORTUNE: Load + extract =============================
Dim html : html = ReadCache(CACHE_FILE, CACHE_MINUTES)
If Len(html) = 0 Then
  html = Fetch(SRC_URL)
  If Len(html) > 0 Then Call WriteCache(CACHE_FILE, html)
End If

Dim results() : ReDim results(UBound(LABELS), 1)
Dim i, label, val, foundCount : foundCount = 0
For i = 0 To UBound(LABELS)
  label = CStr(LABELS(i))
  val   = FindRateNear(html, label)
  results(i,0) = label
  results(i,1) = val
  If Len(val) > 0 Then foundCount = foundCount + 1
Next

'========================= CONTACT SUPPORT: Handle POST ========================
Dim isPost : isPost = (UCase(Request.ServerVariables("REQUEST_METHOD"))="POST")
Dim sentOK, errMsg : sentOK = False : errMsg = ""

Dim yourName, yourCompany, contactPhone, email, accountId, priority, topic, messageText, sourceIP
Dim htmlBody, textCopy
yourName="" : yourCompany="" : contactPhone="" : email="" : accountId="" : priority="" : topic="" : messageText="" : sourceIP=""

If isPost Then
  yourName     = Trim(Request.Form("yourName"))
  yourCompany  = Trim(Request.Form("yourCompany"))
  contactPhone = Trim(Request.Form("contactPhone"))
  email        = Trim(Request.Form("email"))
  accountId    = Trim(Request.Form("accountId"))
  priority     = Trim(Request.Form("priority"))
  topic        = Trim(Request.Form("topic"))
  messageText  = Trim(Request.Form("message"))
  sourceIP     = Request.ServerVariables("REMOTE_ADDR")

  htmlBody = "<html><body style='font:14px Arial,Helvetica,sans-serif;color:#111'>" & _
    "<h2 style='margin:0 0 8px;color:#00498C'>Customer Support Request</h2>" & _
    "<table style='border-collapse:collapse;width:100%'>" & _
      RowHtml("Name", H(yourName)) & _
      RowHtml("Company", H(yourCompany)) & _
      RowHtml("Phone", H(contactPhone)) & _
      RowHtml("Email", H(email)) & _
      RowHtml("Account/Client ID", H(accountId)) & _
      RowHtml("Priority", H(priority)) & _
      RowHtml("Topic", H(topic)) & _
      RowHtml("Message", Replace(H(messageText), vbCrLf, "<br>")) & _
      RowHtml("Timestamp", H(WeekdayName(Weekday(Date)) & ", " & MonthName(Month(Date)) & " " & Day(Date) & ", " & Year(Date) & " " & HhMmAmPm(Now))) & _
      RowHtml("Source IP", H(sourceIP)) & _
    "</table>" & _
    "<p style='font-size:12px;color:#9aa4b2'>Sent by " & H(EMAIL_FROM_NAME) & " via " & H(EMAIL_SERVER) & "</p>" & _
  "</body></html>"

  textCopy = "Customer Support Request" & vbCrLf & String(27,"-") & vbCrLf & _
    "Name: " & yourName & vbCrLf & _
    "Company: " & yourCompany & vbCrLf & _
    "Phone: " & contactPhone & vbCrLf & _
    "Email: " & email & vbCrLf & _
    "Account/Client ID: " & accountId & vbCrLf & _
    "Priority: " & priority & vbCrLf & _
    "Topic: " & topic & vbCrLf & _
    "Message:" & vbCrLf & messageText & vbCrLf & _
    "Timestamp: " & WeekdayName(Weekday(Date)) & ", " & MonthName(Month(Date)) & " " & Day(Date) & ", " & Year(Date) & " " & HhMmAmPm(Now) & vbCrLf & _
    "Source IP: " & sourceIP & vbCrLf

  Call SafeSaveToDisk(textCopy)
  sentOK = SendSupportMail(htmlBody, email, errMsg)
End If

'========================= RSS: Load ================================
Dim newsXml, mbsXml
Set newsXml = LoadFeed(FEED_NEWS_URL)
Set mbsXml  = LoadFeed(FEED_MBS_URL)

' Variables for RSS loops (declare once)
Dim items, n, it, title, link, pub
%>
<!doctype html>
<html lang="en">
<head>
<meta charset="utf-8" />
<meta name="viewport" content="width=device-width,initial-scale=1" />
<title>IRStaxRecords.com • Customer Support Center</title>
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
<!-- Your existing <style> blocks here -->
</head>

<style>
  html, body { overflow-x: clip; }
  @supports not (overflow-x: clip) { html, body { overflow-x: hidden; } }
  .hero { margin: 0; }
  .hero img{ display:block; width:100vw; max-width:100vw; height:auto; margin-left:calc(50% - 50vw); margin-right:calc(50% - 50vw); }
</style>

<style>
/* Fit input boxes inside cards */
*, *::before, *::after { box-sizing: border-box; }
.card { overflow: hidden; }
.control { min-width: 0; }
.control input[type="text"], .control input[type="email"], .control input[type="tel"], .control select, .control textarea{
  width:100%; max-width:100%; box-sizing:border-box; display:block;
}
</style>

<style>
:root{
  --brand:#00498C; --brand-ink:#0b2d6b; --accent:#f7b500;
  --ink:#0b1320; --muted:#6a7280; --bg:#E7EBEF; --line:#e7eaf0;
  --ring:rgba(31,111,255,.25); --shadow:0 8px 28px rgba(2,12,44,.06);
  --radius:16px;
}
body{
  margin:0; background:var(--bg); color:var(--ink);
  font:15px/1.5 system-ui, -apple-system, Segoe UI, Roboto, "Helvetica Neue", Arial, "Noto Sans", "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", sans-serif;
  padding:0;  /* Changed from padding:0 16px; */
}
.wrap{ width:min(1160px,95vw); margin:0 auto; }
.muted{ color:var(--muted); }

.card{
  background:#fff; border:1px solid var(--line); border-radius:var(--radius);
  box-shadow:var(--shadow); padding:22px;
}
.section{ padding:14px 0 36px; }
.section h2{ margin:0 0 6px; font-size:28px; letter-spacing:.2px; }
.section .sub{ margin:0 0 12px; color:var(--muted); }

.grid-2{ display:grid; grid-template-columns:1fr; gap:18px; }
@media(min-width:900px){ .grid-2{ grid-template-columns:1fr 1fr; } }

.btn{
  display:inline-block; padding:10px 14px; border-radius:12px; text-decoration:none; font-weight:600;
  border:1px solid var(--line); transition:transform .06s ease, background .15s, border-color .15s;
}
.btn:hover{ transform:translateY(-1px); }
.btn-primary{ background:var(--brand); color:#fff; border-color:var(--brand); }
.btn-primary:hover{ background:#0a58a3; border-color:#0a58a3; }
.btn-ghost{ background-color: transparent; color: #00498C; border: 2px solid #00498C; }
.btn-ghost:hover{ background-color: #1d3f84; color: #fff; border-color: #1d3f84; }
.btn-navy{ background:var(--brand-ink); color:#fff; border-color:var(--brand-ink); }
.btn-navy:hover{ background:#1d3f84; border-color:#1d3f84; }
.btn:focus{ outline:none; box-shadow:0 0 0 4px var(--ring); }

/* Turn Times */
.turntimes{ display:grid; grid-template-columns:1fr; gap:12px; }
@media(min-width:780px){ .turntimes{ grid-template-columns:repeat(4,1fr); } }
.stat{ background:#fafbff; border:1px solid var(--line); border-radius:14px; padding:14px 12px; text-align:center; }
.stat .label{ font-size:12px; color:var(--muted); }
.stat .value{ font-size:20px; font-weight:700; margin-top:4px; }
.highlight{ color:var(--brand); }

/* Table / FAQ */
.table-shell{ background:#fff; border:1px solid var(--line); border-radius:12px; box-shadow:var(--shadow); overflow:hidden; }
table{ width:100%; border-collapse:separate; border-spacing:0; }
thead th{ text-align:left; font-weight:600; color:var(--muted); padding:14px 18px; border-bottom:1px solid var(--line); background:#f6f9ff; }
tbody tr.data{ transition:background .25s; }
tbody tr.data:hover{ background:#f0f6ff; }
tbody td{ padding:14px 18px; border-bottom:1px solid #eef2fa; }
.lm-btn{ appearance:none; border:0; background:none; padding:0; cursor:pointer; font:inherit; color:#1f6fff; text-decoration:none; font-weight:600; }
.lm-btn:hover{ color:#0d47a1; }
.lm-btn:focus-visible{ outline:2px solid #1f6fff; outline-offset:3px; border-radius:4px; }
tr.details > td{ padding:0; border-bottom:1px solid #eef2fa; background:#fafcff; }
.panel{ height:0; overflow:hidden; transition:height .33s cubic-bezier(.2,.75,.2,1); }
.panel-inner{ padding:18px; color:var(--muted); }

/* Form */
.form-grid{ display:grid; grid-template-columns:1fr; gap:12px; }
@media(min-width:800px){ .form-grid{ grid-template-columns:repeat(2, 1fr); } }
.control{ display:flex; flex-direction:column; gap:6px; }
label{ font-weight:600; color:#344053; }
input[type="text"], input[type="email"], input[type="tel"], select, textarea{
  width:100%; padding:10px 12px; border:1px solid var(--line); border-radius:10px; font:inherit;
}
textarea{ min-height:120px; }

/* Rates card (Fortune) */
.rate-card .err{ color:#8a1f1f; background:#fff5f5; border:1px solid #f1c1c1; padding:10px; border-radius:8px; }
.rate-card h2{ margin:0 0 6px; font-size:22px; color:#00498C; }
.rate-card .sub{ margin:0 0 12px; color:#6a7280; }

/* Footer CTA */
.itr-foot { text-align:center; padding:48px 20px; background-color:#f8f9fa; border:1px solid var(--line); border-radius:16px; }
.itr-foot h3 { color:var(--brand); font-weight:700; margin-bottom:10px; }
.itr-foot p { color:#333; margin-bottom:20px; font-size:1.05rem; }
.itr-foot { max-width:100%; margin:0 auto; }
.cta-row { display:flex; justify-content:center; gap:12px; flex-wrap:wrap; }
</style>

<style>
/* Field color tokens */
:root{
  --field-bg:#fff; --field-text:var(--ink); --field-border:var(--line); --field-focus-ring:var(--ring);
}
.control input[type="text"], .control input[type="email"], .control input[type="tel"], .control textarea, .control select{
  background:var(--field-bg); color:var(--field-text); border-color:var(--field-border);
}
.control input:focus, .control textarea:focus, .control select:focus{
  outline:none; border-color:var(--brand); box-shadow:0 0 0 3px var(--field-focus-ring);
}
.control select{
  appearance:none; -webkit-appearance:none;
  background-image:url("data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' width='14' height='14' viewBox='0 0 24 24' fill='%2300498C'><path d='M7 10l5 5 5-5z'/></svg>");
  background-repeat:no-repeat; background-position:right 12px center; padding-right:38px;
}
.control select option{ color:var(--ink); background:#fff; }
</style>
<style>
/* Cloud links */
.cloud-grid{
  display:grid;
  grid-template-columns:1fr;
  gap:12px;
}
@media (min-width:700px){ .cloud-grid{ grid-template-columns:1fr 1fr; } }
@media (min-width:1050px){ .cloud-grid{ grid-template-columns:1fr 1fr; } }

.cloud{
  display:block;
  text-decoration:none;
  border:1px solid #e7ecf3;
  background:linear-gradient(180deg,#f9fbff,#f4f7fb);
  border-radius:18px;
  padding:14px 16px;
  box-shadow: var(--shadow);
  transition: transform .08s ease, background .15s ease, border-color .15s ease;
}

.cloud:hover{
  transform: translateY(-1px);
  background:#f7faff;
  border-color:#dbe4f0;
}

.cloud-title{
  display:block;
  font-weight:700;
  color:#111;
}

.cloud-sub{
  display:block;
  font-size:13px;
  color:var(--muted);
  margin-top:4px;
}
</style>

<script>
(function(){
  function setPanelHeight(panel, open){
    panel.style.height = open ? panel.firstElementChild.scrollHeight + 'px' : '0px';
  }
  window.addEventListener('DOMContentLoaded', function(){
    const buttons = document.querySelectorAll('.lm-btn');
    const singleOpen = true;
    function closeAll(){
      document.querySelectorAll('.lm-btn[aria-expanded="true"]').forEach(function(b){
        b.setAttribute('aria-expanded','false');
        const p = document.getElementById(b.getAttribute('aria-controls'));
        if(!p) return;
        const row = p.closest('tr.details');
        if(row){ row.setAttribute('aria-hidden','true'); }
        setPanelHeight(p,false);
      });
    }
    buttons.forEach(function(btn){
      const panel = document.getElementById(btn.getAttribute('aria-controls'));
      if(!panel) return;
      const row = panel.closest('tr.details');
      btn.addEventListener('click', function(e){
        e.preventDefault();
        const isOpen = btn.getAttribute('aria-expanded') === 'true';
        if(singleOpen && !isOpen) closeAll();
        btn.setAttribute('aria-expanded', String(!isOpen));
        if(row){ row.setAttribute('aria-hidden', String(isOpen)); }
        setPanelHeight(panel, !isOpen);
      });
      window.addEventListener('resize', function(){ if(btn.getAttribute('aria-expanded') === 'true') setPanelHeight(panel, true); });
    });
  });
})();
function openCenteredPopup(url, title, w, h){
  var dualLeft = (window.screenLeft || window.screenX || 0);
  var dualTop  = (window.screenTop  || window.screenY || 0);
  var outerW   = window.outerWidth  || document.documentElement.clientWidth  || screen.width;
  var outerH   = window.outerHeight || document.documentElement.clientHeight || screen.height;
  var left = Math.max(0, Math.round(dualLeft + (outerW - w)/2));
  var top  = Math.max(0, Math.round(dualTop  + (outerH - h)/2));
  var features = 'scrollbars=yes,resizable=yes,width=' + w + ',height=' + h + ',left=' + left + ',top=' + top;
  var win = window.open(url, title, features);
  if (!win) { window.location.assign(url); return false; }
  try { win.focus(); } catch(e) {}
  return false;
}
</script>
</head>

<body bgcolor="#E7EBEF">
  <!-- Full-bleed banner -->

  

  <main class="wrap">
<%
' ---------- Confirmation vs. Main Content ----------
If isPost And sentOK Then
%>
    <!-- Confirmation -->
    <section class="section">
      <article class="card ok">
        <h2>Thanks, <%= H(FirstNameOf(yourName)) %>. Your message was sent.</h2>
        <p class="sub">We’ll reply to <strong><%= H(email) %></strong>. For urgent matters call 1-866-848-4506.</p>
        <p class="fine">Timestamp: <%= H(WeekdayName(Weekday(Date)) & ", " & MonthName(Month(Date)) & " " & Day(Date) & ", " & Year(Date) & " " & HhMmAmPm(Now)) %> • From IP: <%= H(sourceIP) %></p>
        <div class="cta-row" style="margin-top:10px">
          <a href="/login.aspx" class="btn btn-primary">Go to Client Login</a>
          <a href="/wizard7.asp" class="btn btn-ghost">Create a new Account</a>
        </div>
      </article>
    </section>
<%
Else
%>
    <!-- Header Card -->
    <section class="section">
      <article class="card"><meta name="viewport" content="width=device-width,initial-scale=1" />
        <h2>Customer Support Center</h2>
        <p class="sub">Fast help, clear answers, and secure assistance—tailored for banks, mortgage lenders, and financial teams.</p>

<% If isPost And Not sentOK Then %>
        <div class="alert" style="color:#842029;background:#f8d7da;border:1px solid #f5c2c7;padding:10px;border-radius:8px;">
          <strong>We couldn’t send your message.</strong><br>
          Please try again, or email <a href="mailto:admin@irstaxrecords.com">admin@irstaxrecords.com</a>.<br>
          <span class="fine">Error: <%= H(errMsg) %></span>
        </div>
<% End If %>

        <div class="cta-row" style="margin-top:8px;">
  <a href="/wizard7.asp" class="btn btn-ghost">Create an Account</a>
  <a href="#" class="btn btn-ghost" onclick="return openCenteredPopup('/talktoanexpert.asp','TalkToAnExpert',800,740);">Talk to an Expert</a>
  <a href="/forms.asp" class="btn btn-ghost">Download Forms</a>
</div>
      </article>
    </section>

    <!-- Turn Times -->
    <section class="section">
      <article class="card">
        <div style="display:flex; justify-content:space-between; align-items:baseline; gap:12px; flex-wrap:wrap;">
          <h2>Turn Times</h2>
          <div class="muted">Last updated: <strong><%= FormatDateTime(Date, vbLongDate) %></strong></div>
        </div>
        <p class="sub">Typical completion windows for common requests. Expedited options available.</p>
        <div class="turntimes">
          <div class="stat"><div class="label">IRS 4506-C Transcripts</div><div class="value">24–48 hrs</div></div>
          <div class="stat"><div class="label">IRS 8821 (Expedited)</div><div class="value highlight">~3 hrs</div></div>
          <div class="stat"><div class="label">SSA-89 Verification</div><div class="value">~15 min</div></div>
          <div class="stat"><div class="label">Email Support Response</div><div class="value">~15 min</div></div>
        </div>
      </article>
    </section>

    <!-- Answering Questions -->
    <section class="section">
      <h2>Answering Questions</h2>
      <p class="sub">Quick guidance for common support topics. Click “Learn more.”</p>
      <div class="table-shell">
        <table>
          <thead>
            <tr><th>Topic</th><th>Summary</th><th style="width:140px">More</th></tr>
          </thead>
          <tbody>
            <tr class="data">
              <td>How do I upload a 4506-C / 8821?</td>
              <td>Log in, choose “Order Transcripts,” attach your completed form, and submit.</td>
              <td><button class="lm-btn" type="button" aria-expanded="false" aria-controls="d-uploada">Learn more</button></td>
            </tr>
            <tr class="details" aria-hidden="true">
              <td colspan="3">
                <div class="panel" id="d-uploada" role="region">
                  <div class="panel-inner">
                    <p>After logging in, click <strong>Order Transcripts</strong>. Drag and drop your completed IRS Form 4506-C or 8821, confirm the borrower details, and select your delivery preference. You’ll receive an on-screen order number and an email confirmation immediately. For more details <a href="https://www.irstaxrecords.com/expand7.asp#details">click here</a></p>
                    <p class="fine">Tip: Use our OrderPad desktop icon for one-click uploads.</p>
                  </div>
                </div>
              </td>
            </tr>

            <tr class="data">
              <td>Where do I see order status?</td>
              <td>All active requests appear on your dashboard with real-time updates.</td>
              <td><button class="lm-btn" type="button" aria-expanded="false" aria-controls="d-status">Learn more</button></td>
            </tr>
            <tr class="details" aria-hidden="true">
              <td colspan="3">
                <div class="panel" id="d-status" role="region">
                  <div class="panel-inner">
                    <p>Open your <strong>Online Account Dashboard</strong> to track progress, see timestamps, and download completed transcripts or SSA match/no-match certificates.</p>
                  </div>
                </div>
              </td>
            </tr>

            <tr class="data">
              <td>What if my form is rejected?</td>
              <td>We’ll notify you with the exact reason and how to fix it.</td>
              <td><button class="lm-btn" type="button" aria-expanded="false" aria-controls="d-reject">Learn more</button></td>
            </tr>
            <tr class="details" aria-hidden="true">
              <td colspan="3">
                <div class="panel" id="d-reject" role="region">
                  <div class="panel-inner">
                    <p>Most rejections are address issues or name mismatches. We guide your through the process to re-order.<br> All re-submitted requests are processed with a IRS Supervisor for management level review.</p>
                  </div>
                </div>
              </td>
            </tr>

            <tr class="data">
              <td>Invoice & reporting options</td>
              <td>View PDFs or download Excel summaries with your loan numbers.</td>
              <td><button class="lm-btn" type="button" aria-expanded="false" aria-controls="d-billing">Learn more</button></td>
            </tr>
            <tr class="details" aria-hidden="true">
              <td colspan="3">
                <div class="panel" id="d-billing" role="region">
                  <div class="panel-inner">
                    <p>Access current and past invoices export a consolidated Excel file to reconcile charges by loan number or cost center.</p>
                  </div>
                </div>
              </td>
            </tr>

          </tbody>
        </table>
      </div>
    </section>

    <!-- Information + Contact Support -->
    <section class="section">
      <div class="grid-2">
        <article class="card">
  <h2>Providing Information</h2>
  <p class="sub">Resources your team can use immediately.</p>

  <div class="cloud-grid">
    <a class="cloud" href="/forms.asp">
      <span class="cloud-title">IRS Forms Library</span>
      <span class="cloud-sub">4506-C, 8821, SSA-89 and more</span>
    </a>

    <a class="cloud" href="/expand7.asp#details">
      <span class="cloud-title">How transcripts are delivered</span>
      <span class="cloud-sub">Step-by-step overview</span>
    </a>

    <a class="cloud" href="/wizard7.asp">
      <span class="cloud-title">Create an account</span>
      <span class="cloud-sub">Secure onboarding in minutes</span>
    </a>

    <a class="cloud" href="/login.aspx">
      <span class="cloud-title">Client login</span>
      <span class="cloud-sub">Place and track orders</span>
    </a><P>
  </div><h1>Did you know?</h1>
  <a class="cloud" href="/news/articles/article.asp">
      <span class="cloud-title">The Differences between Form 4506-C and Form 8821.</span>
      <span class="cloud-sub">Side by Side Comparison</span>
    </a><p>
	  <a class="cloud" href="/news/articles/article2.asp">
      <span class="cloud-title">Why Zero Tolerance Fraud Controls Matter today.</span>
      <span class="cloud-sub">Fraud is accelerating. Compliance isn’t optional. It is your only defense</span>
    </a><p>
	  <a class="cloud" href="/news/articles/article3.asp">
      <span class="cloud-title">Dispel the Myth! What really governs Mortgage Interest Rates</span>
      <span class="cloud-sub">Hint: It’s not the Fed “setting” mortgage rates. Relief is likely ahead</span>
    </a>
</article>


        <article class="card">
          <h2>Contact Support</h2>
          <p class="sub">Prefer email or a quick call? Use the fields below.</p>

          <form action="<%= H(Request.ServerVariables("SCRIPT_NAME")) %>" method="post" novalidate>
            <div class="form-grid">
              <div class="control">
                <label for="yourName">Your Name</label>
                <input id="yourName" name="yourName" type="text" placeholder="Jane Smith" value="<%= H(yourName) %>">
              </div>
              <div class="control">
                <label for="yourCompany">Company</label>
                <input id="yourCompany" name="yourCompany" type="text" placeholder="Abc Company" value="<%= H(yourCompany) %>">
              </div>
              <div class="control">
                <label for="contactPhone">Direct Phone</label>
                <input id="contactPhone" name="contactPhone" type="tel" placeholder="(555) 555-1234" value="<%= H(contactPhone) %>">
              </div>
              <div class="control">
                <label for="email">Email</label>
                <input id="email" name="email" type="email" placeholder="name@company.com" value="<%= H(email) %>">
              </div>
              <div class="control">
                <label for="accountId">Account / Client ID (optional)</label>
                <input id="accountId" name="accountId" type="text" placeholder="e.g., 100482" value="<%= H(accountId) %>">
              </div>
              <div class="control">
                <label for="priority">Priority</label>
                <select id="priority" name="priority">
                  <option<% If priority="" Or priority="Normal" Then Response.Write(" selected") End If %>>Normal</option>
                  <option<% If priority="Urgent" Then Response.Write(" selected") End If %>>Urgent</option>
                  <option<% If priority="Time-Sensitive (Closing)" Then Response.Write(" selected") End If %>>Time-Sensitive (Closing)</option>
                </select>
              </div>
              <div class="control" style="grid-column:1/-1;">
                <label for="topic">Topic</label>
                <select id="topic" name="topic">
                  <option<% If topic="" Or topic="Ordering / Upload" Then Response.Write(" selected") End If %>>Ordering / Upload</option>
                  <option<% If topic="Form Correction" Then Response.Write(" selected") End If %>>Form Correction</option>
                  <option<% If topic="Status / Turn-Time" Then Response.Write(" selected") End If %>>Status / Turn-Time</option>
                  <option<% If topic="Billing / Invoice" Then Response.Write(" selected") End If %>>Billing / Invoice</option>
                  <option<% If topic="Technical / Login" Then Response.Write(" selected") End If %>>Technical / Login</option>
                  <option<% If topic="Other" Then Response.Write(" selected") End If %>>Other</option>
                </select>
              </div>
              <div class="control" style="grid-column:1/-1;">
                <label for="message">Message</label>
                <textarea id="message" name="message" placeholder="How can we help? Please include order # if available." rows="3" cols="20"><%= H(messageText) %></textarea>
              </div>
            </div>

            <div style="margin-top:12px; display:flex; gap:10px; flex-wrap:wrap;">
              <button type="submit" class="btn btn-navy" style="flex:1 1 240px;">Submit</button>
            </div>

            <p class="fine" style="margin-top:10px;">If the matter is time sensitive please call Customer Support at 1-866-848-4506</p>
          </form>
        </article>
      </div>
    </section>

    <!-- Mortgage News & Current Interest Rates -->
    <section class="section">
      <div class="grid-2">
        <!-- Mortgage News (RSS) -->
        <article class="card">
          <h2>Mortgage News</h2>
          <p class="sub">Headlines from Mortgage News Daily.</p>

          <div class="news-section" style="font-weight:700;margin-top:6px;">Industry News</div>
          <ul class="news-list" style="list-style:none;margin:0;padding:0;display:grid;gap:12px;">
          <%
            If Not newsXml Is Nothing Then
              Set items = newsXml.selectNodes("//item")
              n = items.length : If n > MAX_ITEMS Then n = MAX_ITEMS
              If n > 0 Then
                For i = 0 To n-1
                  Set it = items.item(i)
                  title = "" : link = "" : pub = ""
                  If Not it.selectSingleNode("title")   Is Nothing Then title = it.selectSingleNode("title").text
                  If Not it.selectSingleNode("link")    Is Nothing Then link  = it.selectSingleNode("link").text
                  If Not it.selectSingleNode("pubDate") Is Nothing Then pub   = it.selectSingleNode("pubDate").text
          %>
            <li>
              <a class="news-card" style="display:block;background:#fff;border:1px solid #e7ecf3;border-radius:12px;padding:12px 14px;text-decoration:none"
                 href="<%=H(link)%>" target="_blank" rel="noopener">
                <div class="news-title" style="font-weight:600;color:#111;"><%=H(title)%></div>
                <% If Len(pub) > 0 Then %><div class="news-date" style="font-size:12px;color:#6c757d;margin-top:6px;"><%=H(pub)%></div><% End If %>
              </a>
            </li>
          <%
                Next
              Else
                Response.Write "<li>No items found.</li>"
              End If
            Else
              Response.Write "<li>Feed unavailable.</li>"
            End If
          %>
          </ul>

          <div class="news-section" style="font-weight:700;margin-top:18px;">MBS Market</div>
          <ul class="news-list" style="list-style:none;margin:0;padding:0;display:grid;gap:12px;">
          <%
            If Not mbsXml Is Nothing Then
              Set items = mbsXml.selectNodes("//item")
              n = items.length : If n > MAX_ITEMS Then n = MAX_ITEMS
              If n > 0 Then
                For i = 0 To n-1
                  Set it = items.item(i)
                  title = "" : link = "" : pub = ""
                  If Not it.selectSingleNode("title")   Is Nothing Then title = it.selectSingleNode("title").text
                  If Not it.selectSingleNode("link")    Is Nothing Then link  = it.selectSingleNode("link").text
                  If Not it.selectSingleNode("pubDate") Is Nothing Then pub   = it.selectSingleNode("pubDate").text
          %>
            <li>
              <a class="news-card" style="display:block;background:#fff;border:1px solid #e7ecf3;border-radius:12px;padding:12px 14px;text-decoration:none"
                 href="<%=H(link)%>" target="_blank" rel="noopener">
                <div class="news-title" style="font-weight:600;color:#111;"><%=H(title)%></div>
                <% If Len(pub) > 0 Then %><div class="news-date" style="font-size:12px;color:#6c757d;margin-top:6px;"><%=H(pub)%></div><% End If %>
              </a>
            </li>
          <%
                Next
              Else
                Response.Write "<li>No items found.</li>"
              End If
            Else
              Response.Write "<li>Feed unavailable.</li>"
            End If
          %>
          </ul>
        </article>

        <!-- Current Mortgage Rates (Fortune scrape result) -->
        <article class="card rate-card">
          <h2>Current Mortgage Rates</h2>
          <p class="sub"><a href="<%=H(SRC_URL)%>" target="_blank" rel="noopener"></a></p>
<%
If Len(html) = 0 Then
%>
          <div class="err">Rates are bring updated at this time.</div>
<%
Else
%>
          <table aria-label="Mortgage rates">
            <thead><tr><th>Loan type</th><th>Rate</th></tr></thead>
            <tbody>
            <%
              Dim printed : printed = 0
              For i = 0 To UBound(LABELS)
                If Len(results(i,1)) > 0 Then
                  Response.Write "<tr><td>" & H(results(i,0)) & "</td><td><strong>" & H(results(i,1)) & "</strong></td></tr>"
                  printed = printed + 1
                End If
              Next
              If printed = 0 Then
                Response.Write "<tr><td colspan='2'>No recognizable rates found near labels—page format may have changed.</td></tr>"
              End If
            %>
            </tbody>
          </table>
         <!-- <p class="fine">Cached locally for <%= CACHE_MINUTES %> minutes to avoid frequent requests.</p> -->
<%
End If
%> <p> <!-- ==================== Article: Where Mortgage Rates Are Going (with citations) ==================== -->
<section class="container my-5" id="rate-outlook-brief">
  <div class="row justify-content-center">
    <div class="col-lg-10">
      <header class="mb-4"><p><br>
         <h2 class="h4 mt-3">Where Are Mortgage Rates Headed? </h1>
        <p class="text-muted">What Government & Markets Are Signaling</p>
      </header>

      <h2 class="h4 mt-2">What the public sector says</h2>
      <ul class="mb-3">
        <li>
          <strong>Fed outlook:</strong> The Federal Reserve’s latest Summary of Economic Projections (SEP) shows policy
          rates drifting lower into 2026, but remember: the Fed targets the <em>overnight</em> federal funds rate; it doesn’t
          set long-term mortgage rates. 
        </li>
        <li>
          <strong>Treasury yields:</strong> The Congressional Budget Office projects the 10-year Treasury yield to ease from
          about <em>4.3%</em> in Q4-2025 to <em>3.9%</em> by Q4-2028—directionally supportive of lower mortgage rates over time. 
        </li>
      </ul>

      <h2 class="h4 mt-4">What lenders & market pros are forecasting</h2>
      <ul class="mb-3">
        <li>
          <strong>MBA baseline:</strong> MBA expects the 10-year to remain above 4% and 30-year mortgage rates to hover
          roughly <em>6%–6.5%</em> over the next couple of months, with occasional dips that can spur refis. 
        </li>
        <li>
          <strong>Fannie Mae ESR:</strong> The ESR group sees mortgage rates ending <em>2026 near 5.9%</em> (6.4% end-2025),
          implying gradual relief into 2026 as inflation cools. 
        </li>
        <li>
          <strong>Right now:</strong> As a reference point, the national 30-year fixed averaged about the low-6s in early
          November 2025. 
        </li>
      </ul>

      <h2 class="h4 mt-4">Key drivers to watch (not just the Fed)</h2>
      <ul class="mb-3">
        <li><strong>10-year Treasury yield</strong> as the anchor for 30-year mortgage pricing.</li>
        <li><strong>MBS spreads</strong> (prepayment/convexity/liquidity risk) that widen or narrow borrower rates at a given 10-year level. </li>
        <li><strong>Inflation & growth expectations</strong> that move longer-term yields.</li>
      </ul>

      
      <h2 class="h4 mt-4">Verify with government records</h2>
      <p class="mb-2">
        Regardless of the rate path, investors and regulators will ask, “What did you verify?” Make <em>government-source</em>
        checks standard on every file: <strong>SSA identity verification</strong> to verify borrower identity, and
        <strong>IRS income verification</strong> via transcripts from the Internal Revenue Service to confirm borrower stated income. 
      </p>

      <h2 class="h4 mt-3">The easy path with IRSTaxRecords.com</h2>
      <p class="mb-4">
        When rates start easing and volume returns, the last thing you need is friction. IRSTaxRecords.com gives lenders a
        fast, compliant channel to >SSA identity checks and IRS transcripts, helping you mitigate fraud and keep loans
        investor ready as the mortgage market improves.
      </p>
    </div>
  </div>
</section>
<!-- ==================== /Article ==================== -->

        </article>
      </div>
    </section>

  <!-- Supporting the Customer Journey -->
<section class="section">
  <article class="card">
    <h2>Supporting the Customer Journey</h2>
    <p class="sub">From onboarding to audit—clear steps and human help at every stage.</p>

    <div class="cloud-grid">
      <div class="cloud">
        <span class="cloud-title">Onboarding</span>
        <span class="cloud-sub">Create secure accounts, set roles, and enable OrderPad or bulk upload if desired.</span>
      </div>

      <div class="cloud">
        <span class="cloud-title">Ordering</span>
        <span class="cloud-sub">Upload 4506-C / 8821 / SSA-89, confirm borrower details, choose delivery preferences.</span>
      </div>

      <div class="cloud">
        <span class="cloud-title">Tracking</span>
        <span class="cloud-sub">Real-time status, timestamps, and notifications—no guesswork, no delays.</span>
      </div>

      <div class="cloud">
        <span class="cloud-title">Delivery</span>
        <span class="cloud-sub">Download official IRS transcripts or SSA match/no-match certificates—secure and fast.</span>
      </div>

      <div class="cloud">
        <span class="cloud-title">Billing &amp; Reporting</span>
        <span class="cloud-sub">PDF invoices, Excel summaries with loan numbers, and exportable audit logs.</span>
      </div>

      <div class="cloud">
        <span class="cloud-title">Live Support</span>
        <span class="cloud-sub">Dedicated reps and a real person on the phone—when you need it most.</span>
      </div>
    </div>
  </article>
</section>


<%
End If ' end confirmation vs main
%>

    <!-- Footer CTA -->
    <section class="section">
      <footer class="itr-foot">
        <h3>Fast. Secure. Reliable. For 25 Years and Counting.</h3>
        <p>Partner with IRS Tax Records and experience proven accuracy, unmatched turnaround, and responsive support.</p>
        <div class="cta-row">
          <a href="/wizard7.asp" class="btn btn-ghost">Create Your Secure Account</a>
          <a href="#" class="btn btn-ghost" onclick="return openCenteredPopup('/talktoanexpert.asp','TalkToAnExpert',800,740);">Talk to an Expert</a>
          <a href="/expand7.asp#details" class="btn btn-ghost">Learn More</a>
        </div>
      </footer>
    </section>
  </main>

  <!-- Optional full-bleed footer image -->
  <div class="hero" style="margin-top:12px;">
    <map name="FPMap1">
    <area href="https://www.irstaxrecords.com/default.aspx" shape="rect" coords="381, 32, 697, 139">
    <area href="https://www.irstaxrecords.com/expand7.asp" shape="rect" coords="804, 113, 1141, 143">
    <area href="https://www.irstaxrecords.com/expand7.asp#details" shape="rect" coords="801, 147, 1094, 170">
    <area href="https://www.irstaxrecords.com/expand7.asp" shape="rect" coords="802, 175, 1073, 197">
    <area href="https://www.irstaxrecords.com/wizard7.asp" shape="rect" coords="804, 202, 1132, 228">
    <area href="https://www.irstaxrecords.com/wizard7.asp" shape="rect" coords="1275, 101, 1488, 150">
    <area href="https://www.irstaxrecords.com/expand7.asp" shape="rect" coords="1267, 160, 1569, 193">
    <area href="forms.asp" shape="rect" coords="1267, 197, 1637, 241"></map><img src="https://www.irstaxrecords.com/new/images/Footer.jpg" alt="IRS Tax Records footer" width="1902" height="289" usemap="#FPMap1">
  </div>
</body>
</html>
