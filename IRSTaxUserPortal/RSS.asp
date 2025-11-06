<%@ Language=VBScript %>
<%
Option Explicit
Response.ContentType = "text/html"
Const FEED_URL = "https://www.mortgagenewsdaily.com/rss/news" ' permitted example

Function H(s) : If IsNull(s) Then H="" Else H=Server.HTMLEncode(CStr(s)) : End If : End Function

Dim http, xml, ok : Set http = Server.CreateObject("MSXML2.ServerXMLHTTP.6.0")
http.open "GET", FEED_URL, False
http.setRequestHeader "User-Agent", "IRStaxRecordsFeedBot/1.0"
http.send
ok = (http.status = 200)
If ok Then
  Set xml = Server.CreateObject("MSXML2.DOMDocument.6.0")
  xml.async = False : xml.setProperty "SelectionLanguage", "XPath"
  ok = xml.LoadXML(http.responseText)
End If
%>
<!doctype html><html><head><meta charset="utf-8" />
<title>Mortgage News</title>
<style>
:root{--brand:#00498C;--muted:#6c757d}
.news-card{border:1px solid #e6e9ef;border-radius:18px;padding:18px;background:#fff;box-shadow:0 2px 10px rgba(0,0,0,.05)}
.news-card h3{margin:0 0 12px;font:600 18px/1.2 system-ui;color:var(--brand)}
.news-card ul{list-style:none;margin:0;padding:0;display:grid;gap:10px}
.news-card li a{text-decoration:none;display:block}
.news-card .t{font:600 15px/1.25 system-ui;color:#111}
.news-card .d{font:12px/1.2 system-ui;color:var(--muted)}
.news-card .src{margin-top:12px;font:12px system-ui;color:var(--muted)}
</style></head><body>
<div class="news-card">
  <h3>Mortgage News <small style="font-weight:400">• <a href="https://www.bankrate.com/mortgages/analysis/" target="_blank" rel="noopener">More from Bankrate</a></small></h3>
  <ul>
  <%
    If ok Then
      Dim items, i, n, node, title, link, pub
      Set items = xml.selectNodes("//item") : n = items.length : If n>6 Then n=6
      If n>0 Then
        For i=0 To n-1
          Set node = items.item(i)
          title = node.selectSingleNode("title").text
          link  = node.selectSingleNode("link").text
          If Not node.selectSingleNode("pubDate") Is Nothing Then pub = node.selectSingleNode("pubDate").text Else pub=""
  %>
    <li><a href="<%=H(link)%>" target="_blank" rel="noopener">
      <span class="t"><%=H(title)%></span><br><span class="d"><%=H(pub)%></span>
    </a></li>
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
  <div class="src">Feeds shown must permit embedding and require attribution/link as their terms state.</div>
</div>
</body></html>
