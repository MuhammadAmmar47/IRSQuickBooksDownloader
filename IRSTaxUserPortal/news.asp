<%@ Language=VBScript %>
<%
Option Explicit
Response.Expires = -1

' ---------- CONFIG ----------
Const SRC_URL       = "https://fortune.com/article/current-mortgage-rates-10-27-2025/"
Const CACHE_FILE    = "C:\IRSPDF\cache\fortune_rates_2025-10-27.html"
Const CACHE_MINUTES = 60

' Which labels to try to pull (label text as it appears on the page)
Dim LABELS
LABELS = Array( _
  "30-year", _
  "15-year", _
  "Jumbo", _
  "FHA", _
  "VA", _
  "5/1 ARM" _
)

' ---------- HELPERS ----------
Function H(s) : If IsNull(s) Or IsEmpty(s) Then H="" Else H=Server.HTMLEncode(CStr(s)) : End If : End Function

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
  ts.Write text
  ts.Close
  On Error GoTo 0
End Sub

Function Fetch(url)
  On Error Resume Next
  Dim http : Set http = Server.CreateObject("MSXML2.ServerXMLHTTP.6.0")
  http.open "GET", url, False
  ' Be polite: set UA + timeouts
  http.setRequestHeader "User-Agent", "IRStaxRecordsRatesEdu/1.0 (+https://www.irstaxrecords.com)"
  http.setRequestHeader "Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"
  http.setRequestHeader "Accept-Language", "en-US,en;q=0.9"
  http.setTimeouts 5000, 5000, 15000, 15000
  http.send
  If http.status = 200 Then
    Fetch = http.responseText
  Else
    Fetch = ""  ' gracefully fail if blocked (403/404/etc.)
  End If
  Set http = Nothing
  On Error GoTo 0
End Function

' Return the first % number near a label within a small window of text
Function FindRateNear(html, label)
  Dim pos, window, re, m, rate
  FindRateNear = ""
  If Len(html) = 0 Then Exit Function

  pos = InStr(1, html, label, vbTextCompare)
  If pos = 0 Then Exit Function

  ' Look ahead ~600 chars from the label for the first percent number like 6.167%
  window = Mid(html, pos, 600)

  Set re = New RegExp
  re.Global = False
  re.IgnoreCase = True
  re.Pattern = "([0-9]{1,2}\.[0-9]{2,3})\s*%"  ' captures 5.389% / 6.17% / 6.167%
  Set m = re.Execute(window)
  If m.Count > 0 Then
    rate = m(0).SubMatches(0)
    FindRateNear = rate & "%"
  End If
  Set re = Nothing
End Function

' ---------- LOAD HTML (with caching) ----------
Dim html : html = ReadCache(CACHE_FILE, CACHE_MINUTES)
If Len(html) = 0 Then
  html = Fetch(SRC_URL)
  If Len(html) > 0 Then Call WriteCache(CACHE_FILE, html)
End If

' ---------- EXTRACT ----------
Dim i, label, val, foundCount : foundCount = 0
Dim results() : ReDim results(UBound(LABELS), 1)

For i = 0 To UBound(LABELS)
  label = CStr(LABELS(i))
  val = FindRateNear(html, label)
  results(i,0) = label
  results(i,1) = val
  If Len(val) > 0 Then foundCount = foundCount + 1
Next
%>
<!doctype html>
<html lang="en">
<head>
<meta charset="utf-8" />
<title>Current Mortgage Rates (Fortune scrape)</title>
<style>
  :root{
    --brand:#00468c; --line:#e7eaf0; --ink:#0b1320; --muted:#6a7280;
  }
  body{ font:15px/1.5 system-ui,-apple-system,Segoe UI,Roboto,Helvetica,Arial,sans-serif; background:#f5f7fb; color:var(--ink); margin:0; padding:20px; }
  .card{ background:#fff; border:1px solid var(--line); border-radius:14px; padding:16px 18px; max-width:560px; margin:0 auto; box-shadow:0 8px 28px rgba(2,12,44,.06); }
  h2{ margin:0 0 6px; font-size:22px; color:var(--brand); }
  .sub{ margin:0 0 12px; color:var(--muted); }
  table{ width:100%; border-collapse:separate; border-spacing:0; }
  th, td{ padding:10px 8px; border-bottom:1px solid #eef2fa; text-align:left; }
  th{ color:#5f6b7a; font-weight:600; background:#f6f9ff; }
  .fine{ font-size:12px; color:var(--muted); margin-top:10px; }
  .err{ color:#8a1f1f; background:#fff5f5; border:1px solid #f1c1c1; padding:10px; border-radius:8px; }
</style>
</head>
<body>
  <div class="card">
    <h2>Current Mortgage Rates</h2>
    <p class="sub">Source: Fortune — <a href="<%=H(SRC_URL)%>" target="_blank" rel="noopener">link</a></p>

<%
If Len(html) = 0 Then
%>
    <div class="err">Couldn’t fetch the source (site may block bots or require JS). Try again later or update manually.</div>
<%
Else
%>
    <table aria-label="Mortgage rates">
      <thead><tr><th>Loan type</th><th>Rate</th></tr></thead>
      <tbody>
      <%
        For i = 0 To UBound(LABELS)
          If Len(results(i,1)) > 0 Then
            Response.Write "<tr><td>" & H(results(i,0)) & "</td><td><strong>" & H(results(i,1)) & "</strong></td></tr>"
          End If
        Next
        If foundCount = 0 Then
          Response.Write "<tr><td colspan='2'>No recognizable rates found near labels—Fortune’s markup may have changed.</td></tr>"
        End If
      %>
      </tbody>
    </table>
    <p class="fine">Cached locally for <%= CACHE_MINUTES %> minutes to avoid frequent requests.</p>
<%
End If
%>
  </div>
</body>
</html>
