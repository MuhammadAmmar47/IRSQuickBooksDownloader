<%
' ---- Simple login state check (Classic ASP) ----
Dim isLoggedIn, loginText, loginHref, loginStyle
isLoggedIn = False

If Not IsEmpty(Session("UserID")) Then
  If Len(Trim(CStr(Session("UserID")))) > 0 Then isLoggedIn = True
End If

If isLoggedIn Then
  loginText  = "Logout"
  loginHref  = "login.aspx"
  loginStyle = "background-color:red;"               ' no red background when logged in
Else
  loginText  = "Account Login"
  loginHref  = "login.aspx"
  loginStyle = "background-color:red;"
End If
%>

<style>
/* Custom styles to replicate Bootstrap utilities without global conflicts */
.header {
  background-color: white;
  border-bottom: 1px solid #dee2e6;
  margin-bottom: 0;
}
.header-inner {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 3rem;
  padding: 0.5rem 0;
  flex-direction: column;
  text-align: center;
}
@media (min-width: 768px) {
  .header-inner {
    flex-direction: row;
    text-align: start;
  }
}
.nav-bar {
  padding: 0.5rem 0;
  background-color: #00468c;
}
.nav-container {
  width: min(1160px, 95vw);
  margin: 0 auto;
  padding: 0 1rem;
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 3rem;
  flex-wrap: wrap;
  text-align: center;
}
.nav-link {
  color: white;
  text-decoration: none;
  padding: 0;
  font-size: 16px;  /* Assuming from previous; adjust if needed */
}
.login-link {
  color: white;
  text-decoration: none;
  padding: 0.25rem 0.75rem;
  border-radius: 0.25rem;
  margin-left: 1rem;
  margin-top: 0.5rem;
  font-size: 16px;  /* Assuming from previous; adjust if needed */
}
@media (min-width: 768px) {
  .login-link {
    margin-top: 0;
  }
}
/* Added to prevent hover color change */
.nav-link:hover {
  color: white !important;
}
.login-link:hover {
  color: white !important;
}
</style>

<!-- Header -->
<div class="header">
  <div class="header-inner">
    <a href="default.aspx" style="display: inline-block;">
      <img src="/img/top3.gif" alt="IRSTaxRecords.com" style="max-width: 100%; height: auto;" />
    </a>

    <!-- Optional legacy header kept as HTML comment for Classic ASP -->
    <!--
    <div style="display: flex; align-items: center; justify-content: center; margin-bottom: 0.5rem;">
      <a href="default.aspx">
        <img src="/img/header.jpg" alt="Logo" style="max-height:100px; width:auto;" />
      </a>
    </div>
    -->
  </div>
</div>

<!-- Navigation Bar -->
<nav class="nav-bar">
  <div class="nav-container">
    <a class="nav-link" href="expand7.asp">Services Details and Solutions</a>
    <a class="nav-link" href="cc.asp">Customer Center</a>
    <a class="nav-link" href="wizard7.asp">Set up an Account and begin ordering today</a>

    <a href="<%= loginHref %>"
       class="login-link"
       style="<%= loginStyle %>"><%= loginText %></a>
  </div>
</nav>
<div style="border-top: 7px solid #FBD600; height: 6px;"></div>