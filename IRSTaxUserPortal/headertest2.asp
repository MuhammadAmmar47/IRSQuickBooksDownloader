
<%
Option Explicit

' ---- Simple login state check (Classic ASP) ----
Dim isLoggedIn, loginText, loginHref, loginStyle
isLoggedIn = False

If Not IsEmpty(Session("UserID")) Then
  If Len(Trim(CStr(Session("UserID")))) > 0 Then isLoggedIn = True
End If

If isLoggedIn Then
  loginText  = "Logout"
  loginHref  = "logout.asp"
  loginStyle = ""                ' no red background when logged in
Else
  loginText  = "Account Login"
  loginHref  = "login.asp"
  loginStyle = "background-color:red;"
End If
%>
<!doctype html>
<html lang="en">
<head>
<meta charset="utf-8" />
<meta name="viewport" content="width=device-width,initial-scale=1" />
<title>Header Test</title>

'<!-- Optional: load Bootstrap if you want the nav-link classes to style -->
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
  <!-- Header -->
  <div class="row g-0 bg-white border-bottom mb-0">
    <div class="col-12">
      <div class="d-flex align-items-center justify-content-center gap-5 py-2 flex-column flex-md-row text-center text-md-start">
        <a href="default.aspx" class="d-inline-block">
          <img src="/img/top3.gif" alt="IRSTaxRecords.com" class="img-fluid" />
        </a>

        <!-- Optional legacy header kept as HTML comment for Classic ASP -->
        <!--
        <div class="d-flex align-items-center justify-content-center mb-2 mb-md-0">
          <a href="default.aspx">
            <img src="/img/header.jpg" class="img-fluid" alt="Logo" style="max-height:100px;width:auto;" />
          </a>
        </div>
        -->
      </div>
    </div>
  </div>

  <!-- Navigation Bar -->
  <nav class="py-2" style="background-color:#00468c;">
    <div class="container d-flex justify-content-center align-items-center gap-5 flex-wrap text-center">
      <a class="nav-link text-white px-0" href="expand7.asp">Services Details and Solutions</a>
      <a class="nav-link text-white px-0" href="cc.asp">Customer Center</a>
      <a class="nav-link text-white px-0" href="wizard7.asp">Set up an Account and begin ordering today</a>

      <a href="<%= loginHref %>"
         class="nav-link text-white px-3 py-1 rounded ms-3 mt-2 mt-md-0"
         style="<%= loginStyle %>"><%= loginText %></a>
    </div>
  </nav>
</body><div class="border-top" style="border-color: #FFD700 !important; border-top-width: 5px !important;"></div>
</html>
