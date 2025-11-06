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
