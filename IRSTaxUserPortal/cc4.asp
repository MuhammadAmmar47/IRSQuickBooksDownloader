<%@ Language=VBScript %>
<%
' ---------- CONFIG ----------
Const FEED_NEWS_URL = "http://www.mortgagenewsdaily.com/rss/news" ' Industry news
Const FEED_MBS_URL  = "http://www.mortgagenewsdaily.com/rss/mbs"  ' MBS market updates
Const MAX_ITEMS = 6

Function H(s) : If IsNull(s) Or IsEmpty(s) Then H="" Else H=Server.HTMLEncode(CStr(s)) : End If : End Function

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

Dim newsXml, mbsXml
Set newsXml = LoadFeed(FEED_NEWS_URL)
Set mbsXml  = LoadFeed(FEED_MBS_URL)
%>


<!doctype html>
<html lang="en">
<head>
<meta charset="utf-8" />
<meta name="viewport" content="width=device-width,initial-scale=1" />
<title>IRStaxRecords.com • Customer Support Center</title>

<!-- Global overflow / hero full-bleed (same pattern as yours) -->
<style>
  html, body { overflow-x: clip; }
  @supports not (overflow-x: clip) { html, body { overflow-x: hidden; } }
  .hero { margin: 0; }
  .hero img{
    display:block; width:100vw; max-width:100vw; height:auto;
    margin-left:calc(50% - 50vw); margin-right:calc(50% - 50vw);
  }
</style>
<style>
/* --- Brand look --- */
:root{
  --brand:#00498C;          /* IRSTaxRecords navy */
  --hover:#5dade2;          /* pale blue hover */
  --muted:#6c757d;
  --card:#ffffff; --edge:#e7ecf3;
  --radius:18px;
}
.mortgage-wrap{border:1px solid var(--edge); border-radius:var(--radius); background:var(--card); box-shadow:0 6px 20px rgba(0,0,0,.06); overflow:hidden}
.mortgage-head{display:flex; justify-content:space-between; align-items:center; padding:14px 16px; border-bottom:1px solid var(--edge); background:linear-gradient(0deg,#f9fbfe,#fff)}
.mortgage-head h3{margin:0; font:700 20px/1.2 system-ui; color:var(--brand)}
.btn-brand{display:inline-flex; align-items:center; gap:.5rem; padding:.5rem .9rem; border-radius:999px; border:1px solid var(--brand); background:var(--brand); color:#fff; text-decoration:none; font-weight:600}
.btn-brand:hover{background:var(--hover); border-color:var(--hover); color:#002a52}
.btn-ghost{background:transparent; color:var(--brand)}
.btn-ghost:hover{background:#eef6ff}

.mortgage-body{display:grid; gap:0; grid-template-columns:1.2fr .8fr}
@media (max-width: 900px){ .mortgage-body{grid-template-columns:1fr} }

.news-pane{padding:16px}
.news-section{margin:0 0 12px; font:700 16px/1.2 system-ui; color:#111}

/* List becomes a responsive card grid */
.news-list{list-style:none; margin:0; padding:0; display:grid; gap:12px;
  grid-template-columns:1fr}
@media (min-width:640px){ .news-list{grid-template-columns:repeat(2, minmax(0,1fr))} }
@media (min-width:1100px){ .news-list{grid-template-columns:repeat(3, minmax(0,1fr))} }

/* Card */
.news-card{display:block; background:#fff; border:1px solid var(--edge); border-radius:12px; padding:12px 14px;
  box-shadow:0 2px 10px rgba(0,0,0,.04); text-decoration:none; transition:border-color .15s ease, box-shadow .2s ease, transform .03s ease}
.news-card:hover{border-color:var(--brand); box-shadow:0 6px 20px rgba(0,0,0,.08)}
.news-title{font:600 15px/1.25 system-ui; color:#111; margin:0}
.news-card:hover .news-title{color:var(--brand); text-decoration:underline}
.news-date{font:12px/1.2 system-ui; color:var(--muted); margin-top:6px}

.rates-pane{border-left:1px solid var(--edge); padding:16px}
@media (max-width: 900px){ .rates-pane{border-left:0; border-top:1px solid var(--edge)} }
.rates-head{font:700 16px/1.2 system-ui; color:#111; margin:0 0 10px}
.rates-slot{min-height:320px; display:grid; place-items:center; border:1px dashed var(--edge); border-radius:12px; padding:8px}
.attribution{border-top:1px solid var(--edge); background:#fafbfd; color:var(--muted); font:12px/1.2 system-ui; padding:10px 14px}
</style>
<!-- Theme + layout (aligned with your .itr-* system) -->
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
    padding:0 16px;
  }
  .wrap{width:min(1160px,95vw); margin:0 auto;}
  .muted{color:var(--muted);}

  .card{
    background:#fff; border:1px solid var(--line); border-radius:var(--radius);
    box-shadow:var(--shadow); padding:22px;
  }
  .section{padding:14px 0 36px;}
  .section h2{margin:0 0 6px; font-size:28px; letter-spacing:.2px;}
  .section .sub{margin:0 0 12px; color:var(--muted);}

  .grid-2{display:grid; grid-template-columns:1fr; gap:18px;}
  @media(min-width:900px){ .grid-2{ grid-template-columns:1fr 1fr; } }

  .btn{
    display:inline-block; padding:10px 14px; border-radius:12px; text-decoration:none; font-weight:600;
    border:1px solid var(--line); transition:transform .06s ease, background .15s, border-color .15s;
  }
  .btn:hover{ transform:translateY(-1px); }
  .btn-primary{ background:var(--brand); color:#fff; border-color:var(--brand); }
  .btn-primary:hover{ background:#0a58a3; border-color:#0a58a3; }
  .btn-ghost{ background:#fff; color:var(--ink); }
  .btn-navy{ background:var(--brand-ink); color:#fff; border-color:var(--brand-ink); }
  .btn-navy:hover{ background:#1d3f84; border-color:#1d3f84; }
  .btn:focus{ outline:none; box-shadow:0 0 0 4px var(--ring); }

  /* Turn Times bar */
  .turntimes{
    display:grid; grid-template-columns:1fr; gap:12px;
  }
  @media(min-width:780px){ .turntimes{ grid-template-columns:repeat(4,1fr); } }
  .stat{
    background:#fafbff; border:1px solid var(--line); border-radius:14px; padding:14px 12px; text-align:center;
  }
  .stat .label{font-size:12px; color:var(--muted);}
  .stat .value{font-size:20px; font-weight:700; margin-top:4px;}
  .highlight{ color:var(--brand); }

  /* Expandable Q&A table (same pattern as your details table) */
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

  /* Form styles */
  .form-grid{ display:grid; grid-template-columns:1fr; gap:12px; }
  @media(min-width:800px){ .form-grid{ grid-template-columns:repeat(2, 1fr); } }
  .control{ display:flex; flex-direction:column; gap:6px; }
  label{ font-weight:600; color:#344053; }
  input[type="text"], input[type="email"], input[type="tel"], select, textarea{
    width:100%; padding:10px 12px; border:1px solid var(--line); border-radius:10px; font:inherit;
  }
  textarea{ min-height:120px; }

  /* Journey timeline */
  .journey{ display:grid; grid-template-columns:1fr; gap:14px; counter-reset:step; }
  .step{ background:#fafbff; border:1px solid var(--line); border-radius:14px; padding:14px 12px; position:relative; }
  .step:before{
    counter-increment:step; content:counter(step);
    position:absolute; left:-10px; top:-10px;
    background:var(--brand); color:#fff; width:28px; height:28px; border-radius:999px;
    display:grid; place-items:center; font-weight:700; font-size:14px; box-shadow:0 4px 10px rgba(0,0,0,.12);
  }

  /* News & Rates */
  .rates-grid{ display:grid; grid-template-columns:1fr; gap:18px; }
  @media(min-width:900px){ .rates-grid{ grid-template-columns:2fr 1fr; } }
  .news-list{ list-style:none; padding:0; margin:0; }
  .news-list li{ padding:10px 0; border-top:1px dashed var(--line); }
  .news-list li:first-child{ border-top:none; }
  .news-list a{ color:var(--brand); text-decoration:none; font-weight:600; }
  .news-list a:hover{ text-decoration:underline; }
  .rate-table{ width:100%; border-collapse:separate; border-spacing:0; }
  .rate-table th, .rate-table td{ padding:10px 12px; border-bottom:1px solid var(--line); text-align:left; }
  .rate-table thead th{ background:#f6f9ff; color:var(--muted); font-weight:700; }
  .fine{ font-size:12px; color:var(--muted); }

  /* Footer CTA (kept from your style) */
  .itr-foot { text-align:center; padding:48px 20px; background-color:#f8f9fa; border:1px solid var(--line); border-radius:16px; }
  .itr-foot h3 { color:var(--brand); font-weight:700; margin-bottom:10px; }
  .itr-foot p { color:#333; margin-bottom:20px; font-size:1.05rem; }
  .cta-row { display:flex; justify-content:center; gap:12px; flex-wrap:wrap; }
</style>

<!-- Centered popup helper (same as your Talk to an Expert button) -->
<script>
function openCenteredPopup(url, title, w, h){
  var dualLeft = (window.screenLeft ?? window.screenX ?? 0);
  var dualTop  = (window.screenTop  ?? window.screenY ?? 0);
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
  <div class="hero">
    <!-- Replace image if desired -->
    <img src="https://www.irstaxrecords.com/new/images/top2.jpg" alt="IRS Tax Records banner" width="1902" height="190">
  </div>

  <main class="wrap">

    <!-- Header Card -->
    <section class="section">
      <article class="card">
        <h2>Customer Support Center</h2>
        <p class="sub">Fast help, clear answers, and secure assistance for you and your team.</p>
        <div class="cta-row" style="margin-top:8px;">
          <a href="/wizard7.asp" class="btn btn-primary">Create a new Account</a>
          <a href="#" class="btn btn-navy" onclick="return openCenteredPopup('/talktoanexpert.asp','TalkToAnExpert',800,740);">Talk to an Expert</a>
          <a href="/forms.asp" class="btn btn-ghost">Download Forms</a>
        </div>
      </article>
    </section>

    <!-- Turn Times -->
    <section class="section">
      <article class="card">
        <div style="display:flex; justify-content:space-between; align-items:baseline; gap:12px; flex-wrap:wrap;">
          <h2>Turn Times</h2>
          <div class="muted">Last updated: <strong><!-- edit manually --><%
' Classic ASP: output date as MM-DD-YYYY
Dim d, mm, dd, yyyy, formatted
d = Date()             ' use Now() if you prefer current date/time
mm = Right("0" & Month(d), 2)
dd = Right("0" & Day(d), 2)
yyyy = Year(d)

formatted = mm & "-" & dd & "-" & yyyy
Response.Write formatted
%></strong></div>
        </div>
        <p class="sub">Typical completion windows for common requests. Expedited options available.</p>
        <div class="turntimes">
          <div class="stat">
            <div class="label">IRS 4506-C Transcripts</div>
            <div class="value">24–48 hrs</div>
          </div>
          <div class="stat">
            <div class="label">IRS 8821 (Expedited)</div>
            <div class="value highlight">~3 hrs</div>
          </div>
          <div class="stat">
            <div class="label">SSA-89 Verification</div>
            <div class="value">~15 min</div>
          </div>
          <div class="stat">
            <div class="label">Support Response</div>
            <div class="value">Under 1 hr</div>
          </div>
        </div>
      </article>
    </section>

    <!-- Answering Questions (Expandable Q&A) -->
    <section class="section">
      <h2>Answering Questions</h2>
      <p class="sub">Quick guidance for the most common support topics. Click “Learn more” to expand.</p>
      <div class="table-shell">
        <table>
          <thead>
            <tr>
              <th>Topic</th>
              <th>Summary</th>
              <th style="width:140px">More</th>
            </tr>
          </thead>
          <tbody>
            <!-- Row 1 -->
            <tr class="data">
              <td>How do I upload a 4506-C / 8821?</td>
              <td>Log in, choose “Order Transcripts,” attach your completed form, and submit.</td>
              <td><button class="lm-btn" type="button" aria-expanded="false" aria-controls="d-uploada">Learn more</button></td>
            </tr>
            <tr class="details" aria-hidden="true">
              <td colspan="3">
                <div class="panel" id="d-uploada" role="region">
                  <div class="panel-inner">
                    <p>After logging in, click <strong>Order Transcripts</strong>. Drag and drop your completed IRS Form 4506-C or 8821, confirm the borrower details, and select your delivery preference. You’ll receive an on-screen order number and an email confirmation immediately.</p>
                    <p class="fine">Tip: Use our OrderPad desktop icon for one-click, drag-and-drop uploads without logging in.</p>
                  </div>
                </div>
              </td>
            </tr>

            <!-- Row 2 -->
            <tr class="data">
              <td>Where do I see order status?</td>
              <td>All active requests appear on your dashboard with real-time updates.</td>
              <td><button class="lm-btn" type="button" aria-expanded="false" aria-controls="d-status">Learn more</button></td>
            </tr>
            <tr class="details" aria-hidden="true">
              <td colspan="3">
                <div class="panel" id="d-status" role="region">
                  <div class="panel-inner">
                    <p>Open your <strong>Administrator Dashboard</strong> to track progress, see timestamps, and download completed transcripts or SSA match/no-match certificates. Every transaction is logged for audit purposes.</p>
                  </div>
                </div>
              </td>
            </tr>

            <!-- Row 3 -->
            <tr class="data">
              <td>What if my form is rejected?</td>
              <td>We’ll notify you with the exact reason and how to fix it.</td>
              <td><button class="lm-btn" type="button" aria-expanded="false" aria-controls="d-reject">Learn more</button></td>
            </tr>
            <tr class="details" aria-hidden="true">
              <td colspan="3">
                <div class="panel" id="d-reject" role="region">
                  <div class="panel-inner">
                    <p>Rejections typically stem from signature/date issues or TIN/name mismatches. Our team provides a clear correction note and a ready-to-use template so you can resubmit quickly.</p>
                  </div>
                </div>
              </td>
            </tr>

            <!-- Row 4 -->
            <tr class="data">
              <td>Invoice & reporting options</td>
              <td>View PDFs online or download Excel summaries with your loan numbers.</td>
              <td><button class="lm-btn" type="button" aria-expanded="false" aria-controls="d-billing">Learn more</button></td>
            </tr>
            <tr class="details" aria-hidden="true">
              <td colspan="3">
                <div class="panel" id="d-billing" role="region">
                  <div class="panel-inner">
                    <p>Access current and past invoices in your admin account. Export a consolidated Excel file to reconcile charges by loan number or cost center.</p>
                  </div>
                </div>
              </td>
            </tr>

          </tbody>
        </table>
      </div>
    </section>

    <!-- Providing Information -->
    <section class="section">
      <div class="grid-2">
        <article class="card">
          <h2>Providing Information</h2>
          <p class="sub">Resources your team can use immediately.</p>
          <ul class="news-list">
            <li><a href="/forms.asp">IRS Forms Library</a> — 4506-C, 8821, SSA-89 and more.</li>
            <li><a href="/expand7.asp#details">How transcripts are delivered</a> — step-by-step overview.</li>
            <li><a href="/wizard7.asp">Create an account</a> — secure onboarding in minutes.</li>
            <li><a href="/login2.aspx">Client login</a> — place and track orders.</li>
          </ul>
        </article>

        <!-- Contact Support -->
        <article class="card">
          <h2>Contact Support</h2>
          <p class="sub">Prefer email or a quick call? Use the sample fields below (edit names/endpoint to fit your system).</p>

          <!-- NOTE: Update 'action' to your processing script (e.g., /talktoanexpert.asp) -->
          <form action="/talktoanexpert.asp" method="post" novalidate>
            <div class="form-grid">
              <div class="control">
                <label for="yourName">Your Name</label>
                <input id="yourName" name="yourName" type="text" placeholder="Jane Doe">
              </div>
              <div class="control">
                <label for="yourCompany">Company</label>
                <input id="yourCompany" name="yourCompany" type="text" placeholder="Sunrise Bank N.A.">
              </div>
              <div class="control">
                <label for="contactPhone">Direct Phone</label>
                <input id="contactPhone" name="contactPhone" type="tel" placeholder="(555) 555-1234">
              </div>
              <div class="control">
                <label for="email">Email</label>
                <input id="email" name="email" type="email" placeholder="name@company.com">
              </div>
              <div class="control">
                <label for="accountId">Account / Client ID (optional)</label>
                <input id="accountId" name="accountId" type="text" placeholder="e.g., 100482">
              </div>
              <div class="control">
                <label for="priority">Priority</label>
                <select id="priority" name="priority">
                  <option>Normal</option>
                  <option>Urgent</option>
                  <option>Time-Sensitive (Closing)</option>
                </select>
              </div>
              <div class="control" style="grid-column:1/-1;">
                <label for="topic">Topic</label>
                <select id="topic" name="topic">
                  <option>Ordering / Upload</option>
                  <option>Form Correction</option>
                  <option>Status / Turn-Time</option>
                  <option>Billing / Invoice</option>
                  <option>Technical / Login</option>
                  <option>Other</option>
                </select>
              </div>
              <div class="control" style="grid-column:1/-1;">
                <label for="message">Message</label>
                <textarea id="message" name="message" placeholder="How can we help? Please include order # if available."></textarea>
              </div>
            </div>

            <div style="margin-top:12px; display:flex; gap:10px; flex-wrap:wrap;">
              <button type="submit" class="btn btn-navy">Submit</button>
              <a href="#" class="btn btn-ghost" onclick="return openCenteredPopup('/talktoanexpert.asp','TalkToAnExpert',800,740);">Talk to an Expert</a>
            </div>

            <p class="fine" style="margin-top:10px;">Customer Support: 1-866-848-4506 • <a href="mailto:info@irstaxrecords.com">info@IRStaxRecords.com</a></p>
          </form>
        </article>
      </div>
    </section>

    <!-- Supporting the Customer Journey -->
    <section class="section">
      <article class="card">
        <h2>Supporting the Customer Journey</h2>
        <p class="sub">From onboarding to audit—clear steps and human help at every stage.</p>
        <div class="journey">
          <div class="step">
            <strong>Onboarding</strong>
            <div class="muted">Create secure accounts, set roles, and enable OrderPad or bulk upload if desired.</div>
          </div>
          <div class="step">
            <strong>Ordering</strong>
            <div class="muted">Upload 4506-C / 8821 / SSA-89, confirm borrower details, choose delivery preferences.</div>
          </div>
          <div class="step">
            <strong>Tracking</strong>
            <div class="muted">Real-time status, timestamps, and notifications—no guesswork, no delays.</div>
          </div>
          <div class="step">
            <strong>Delivery</strong>
            <div class="muted">Download official IRS transcripts or SSA match/no-match certificates—secure and fast.</div>
          </div>
          <div class="step">
            <strong>Billing & Reporting</strong>
            <div class="muted">PDF invoices, Excel summaries with loan numbers, and exportable audit logs.</div>
          </div>
          <div class="step">
            <strong>Live Support</strong>
            <div class="muted">Dedicated reps and a real person on the phone—when you need it most.</div>
          </div>
        </div>
      </article>
    </section>

    <!-- Mortgage News & Current Interest Rates -->
    <section class="section">
      <div class="rates-grid">
        <!-- Mortgage News -->
        <article class="card">
          <h2>Mortgage News</h2>
          <p class="sub">Add timely updates relevant to your lenders and partners.</p>
          <ul class="news-list">
            <!-- Replace with your latest items -->
            <li><a href="#">Fannie Mae updates income documentation guidance</a> <span class="fine">— Oct 20, 2025</span></li>
            <li><a href="#">HUD clarifies FHA underwriting requirements</a> <span class="fine">— Oct 18, 2025</span></li>
            <li><a href="#">Freddie Mac bulletin: verification best practices</a> <span class="fine">— Oct 14, 2025</span></li>
          </ul>
          <p class="fine" style="margin-top:10px;">Links provided for convenience; verify latest rules with each agency.</p>
        </article>

        <!-- Current Interest Rates -->
        <article class="card">
          <h2>Current Interest Rates</h2>
          <p class="sub">Edit figures below each day or as needed.</p>
          <table class="rate-table" aria-label="Current Interest Rates">
            <thead>
              <tr><th>Product</th><th>Rate</th><th>APR</th></tr>
            </thead>
            <tbody>
              <tr><td>30-Year Fixed</td><td>7.24%</td><td>7.41%</td></tr>
              <tr><td>15-Year Fixed</td><td>6.58%</td><td>6.71%</td></tr>
              <tr><td>5/1 ARM</td><td>6.32%</td><td>6.85%</td></tr>
              <tr><td>Jumbo 30-Year</td><td>7.39%</td><td>7.55%</td></tr>
            </tbody>
          </table>
          <p class="fine" style="margin-top:8px;">Last updated: <strong><!-- edit manually --><%= FormatDateTime(Date, vbLongDate) %></strong>. Sample data—replace with your current rate sheet.</p>
        </article>
      </div>
    </section>
<div class="mortgage-wrap">
  <div class="mortgage-head">
    <h3 class="m-0">Mortgage News & Rates</h3>
  </div>

  <div class="mortgage-body">
    <!-- LEFT: News (RSS headlines) -->
    <div class="news-pane">

      <!-- Industry News -->
      <div class="news-section">Industry News</div>
      <ul class="news-list">
        <%
          Dim items, n, i, it, title, link, pub
          If Not newsXml Is Nothing Then
            Set items = newsXml.selectNodes("//item")
            n = items.length : If n > MAX_ITEMS Then n = MAX_ITEMS
            If n > 0 Then
              For i = 0 To n - 1
                Set it = items.item(i)
                title = "" : link = "" : pub = ""
                If Not it.selectSingleNode("title") Is Nothing Then title = it.selectSingleNode("title").text
                If Not it.selectSingleNode("link")  Is Nothing Then link  = it.selectSingleNode("link").text
                If Not it.selectSingleNode("pubDate") Is Nothing Then pub = it.selectSingleNode("pubDate").text
        %>
        <li class="news-item">
          <a class="news-card" href="<%=H(link)%>" target="_blank" rel="noopener">
            <div class="news-title"><%=H(title)%></div>
            <% If Len(pub) > 0 Then %><div class="news-date"><%=H(pub)%></div><% End If %>
          </a>
        </li>
        <%
              Next
            Else
              Response.Write("<li>No items found.</li>")
            End If
          Else
            Response.Write("<li>Feed unavailable.</li>")
          End If
        %>
      </ul>

      <!-- MBS Market Updates -->
      <div class="news-section" style="margin-top:18px">MBS Market</div>
      <ul class="news-list">
        <%
          If Not mbsXml Is Nothing Then
            Set items = mbsXml.selectNodes("//item")
            n = items.length : If n > MAX_ITEMS Then n = MAX_ITEMS
            If n > 0 Then
              For i = 0 To n - 1
                Set it = items.item(i)
                title = "" : link = "" : pub = ""
                If Not it.selectSingleNode("title") Is Nothing Then title = it.selectSingleNode("title").text
                If Not it.selectSingleNode("link")  Is Nothing Then link  = it.selectSingleNode("link").text
                If Not it.selectSingleNode("pubDate") Is Nothing Then pub = it.selectSingleNode("pubDate").text
        %>
        <li class="news-item">
          <a class="news-card" href="<%=H(link)%>" target="_blank" rel="noopener">
            <div class="news-title"><%=H(title)%></div>
            <% If Len(pub) > 0 Then %><div class="news-date"><%=H(pub)%></div><% End If %>
          </a>
        </li>
        <%
              Next
            Else
              Response.Write("<li>No items found.</li>")
            End If
          Else
            Response.Write("<li>Feed unavailable.</li>")
          End If
        %>
      </ul>

    </div>

    <!-- RIGHT: Rates widget (official embed) -->
    <div class="rates-pane">
      <div class="rates-head"></div>

      <div class="mnd-rates-widget" style="width:220px;height:480px;font-size:12px;">
        <div class="w-header" style="text-align:center;padding:4px 0;background-color:#336699;color:#FFFFFF;">
          <a href="https://www.mortgagenewsdaily.com/charts/mbssvg/fnma50?c=%20rate-better%20rate-last-worse" target="_blank" style="color:#FFFFFF;text-decoration:none;">Home Mortgage Rates</a>
        </div>
        <iframe src="https://widgets.mortgagenewsdaily.com/widget/f/rates?t=expanded&sn=true&sc=true&c=336699&u=&cbu=&w=218&h=430"
                width="220" height="430" frameborder="0" scrolling="no"
                style="border:0;box-sizing:border-box;width:220px;height:430px;display:block;"></iframe>
        <div class="w-footer" style="text-align:center;padding:4px 0;background-color:#336699;color:#FFFFFF;">
          View More <a href="https://www.mortgagenewsdaily.com/mortgage-rates" target="_blank" style="color:#FFFFFF;text-decoration:none;">Mortgage Rates</a>
        </div>
      </div>
    </div>
  </div>

  
</div>
    <!-- Footer CTA -->
    <section class="section">
      <footer class="itr-foot">
        <h3>Fast. Secure. Reliable. For 25 Years and Counting.</h3>
        <p>Partner with IRS Tax Records and experience proven accuracy, unmatched turnaround, and responsive support.</p>
        <div class="cta-row">
          <a href="/wizard7.asp" class="btn btn-primary">Create Your Secure Account</a>
          <a href="#" class="btn btn-navy" onclick="return openCenteredPopup('/talktoanexpert.asp','TalkToAnExpert',800,740);">Talk to an Expert</a>
          <a href="/expand7.asp#details" class="btn btn-ghost">Learn More</a>
        </div>
      </footer>
    </section>
  </main>

  <!-- Optional full-bleed footer image (keep or remove) -->
  <div class="hero" style="margin-top:12px;">
    <img src="https://www.irstaxrecords.com/new/images/Footer.jpg" alt="IRS Tax Records footer" width="1902" height="289">
  </div>

  <!-- Expand/collapse behavior (same pattern you use) -->
  <script>
    (function(){
      const buttons = document.querySelectorAll('.lm-btn');
      const singleOpen = true;

      function setPanelHeight(panel, open){
        panel.style.height = open ? panel.firstElementChild.scrollHeight + 'px' : '0px';
      }
      function closeAll(){
        document.querySelectorAll('.lm-btn[aria-expanded="true"]').forEach(b=>{
          b.setAttribute('aria-expanded','false');
          const p = document.getElementById(b.getAttribute('aria-controls'));
          if(!p) return;
          const row = p.closest('tr.details');
          if(row){ row.setAttribute('aria-hidden','true'); }
          setPanelHeight(p,false);
        });
      }
      buttons.forEach(btn=>{
        const panel = document.getElementById(btn.getAttribute('aria-controls'));
        if(!panel) return;
        const row = panel.closest('tr.details');
        btn.addEventListener('click', e=>{
          e.preventDefault();
          const isOpen = btn.getAttribute('aria-expanded') === 'true';
          if(singleOpen && !isOpen) closeAll();
          btn.setAttribute('aria-expanded', String(!isOpen));
          if(row){ row.setAttribute('aria-hidden', String(isOpen)); }
          setPanelHeight(panel, !isOpen);
        });
        window.addEventListener('resize', ()=>{ if(btn.getAttribute('aria-expanded') === 'true') setPanelHeight(panel, true); });
      });
    })();
  </script><!--#include file="footer3.asp"-->
</body>
</html>
