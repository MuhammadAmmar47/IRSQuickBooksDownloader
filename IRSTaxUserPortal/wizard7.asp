
<!--#include file="testurl.asp"-->
<%
'=========================================================================
' Module: newcustomer.asp (modernized UI, explicit JS alerts, alternating rows)
' Purpose: Customer sign-up and information update (same functionality)
' Notes:   Requires header.asp (DB Conn), states_array.asp, captcha/matchcode endpoints
'=========================================================================

' --- original IP blocks preserved ---
If Request("REMOTE_ADDR") = "209.155.80.5" Then Server.Transfer("error.asp")
If Request("REMOTE_ADDR") = "209.155.80.122" Then Server.Transfer("error.asp")
If Request("REMOTE_ADDR") = "209.155.80.14" Then Server.Transfer("error.asp")
%>
<!--#include file="header.asp"-->
<!--#include file="states_array.asp"-->
<%
' ------------------- Server variables & data preload --------------------
Dim referalls(6)
referalls(0) = "Referral"
referalls(1) = "Search Engines"
referalls(2) = "Magazine Ad"
referalls(3) = "Trade Show"
referalls(4) = "Sales Call"
referalls(5) = "Direct Mail"

Dim un, sql, rs
Dim name, cname, email, phone, fax, bill, address, address1, city, stateName, zip, reff, userId, acct
name = "" : cname = "" : email = "" : phone = "" : fax = "" : bill = "" : address = "" : address1 = ""
city = "" : stateName = "" : zip = "" : reff = "" : userId = "" : acct = ""

un = Request("un")
If IsNumeric(un) Then
  sql = "SELECT * FROM Customer WHERE CustomerID = " & CLng(un)
  Set rs = Conn.Execute(sql)
  If Not rs.EOF Then
    name      = rs("name") & ""
    cname     = rs("companyname") & ""
    email     = rs("email") & ""
    phone     = rs("telephone") & ""
    fax       = rs("faxnumber") & ""
    bill      = rs("billToName") & ""
    address   = rs("address") & ""
    address1  = rs("address1") & ""
    city      = rs("city") & ""
    stateName = rs("state") & ""
    zip       = rs("Zip") & ""
    reff      = rs("Referal") & ""
    userId    = rs("UserId") & ""
    acct      = "checked"
  End If
  If Not rs Is Nothing Then On Error Resume Next: rs.Close: Set rs = Nothing: On Error GoTo 0
End If

Dim isNew: isNew = (Trim(un) = "")
%>
<!DOCTYPE html>
<html lang="en">

<head>
<style>
  html, body { height: 100%; }
  body { min-height: 100vh; display: flex; flex-direction: column; }
  /* Wrap all page content (except the footer image) */
  main.site-main { flex: 1 0 auto; }
  /* Full-bleed footer banner */
  .footer-banner { flex-shrink: 0; }
  .footer-banner img{
    display:block;
    width:100vw; max-width:100vw; height:auto;
    margin-left:calc(50% - 50vw); margin-right:calc(50% - 50vw);
  }
</style>
  <meta charset="utf-8" />
  <title>IRS Tax Records — Client Setup</title>
  <meta name="viewport" content="width=device-width, initial-scale=1" />

  <!-- Bootstrap 5 -->
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet"/>
  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

  <!-- Styles -->
<style>
    :root {
      --brand-blue:#0d2b52;          /* dark blue border color */
      --row-blue:#e9f3fc;            /* light blue row */
    }

    body { background:#f6f7fb; }
    .brand-hero img { width:100%; height:auto; display:block; }
    .page-wrap { max-width:980px; margin:24px auto; }

    /* Card container gets a subtle dark-blue border */
    .card { 
      border-radius:1rem;
      box-shadow:0 10px 24px rgba(0,0,0,.04);
      border:1px solid var(--brand-blue);
    }

    .form-section-title { font-weight:700; font-size:1.1rem; color:var(--brand-blue); }
    .divider { border-top:1px solid #e9edf3; margin: .75rem 0 1.25rem; }
    .required::after { content:" *"; color:#dc3545; }
    .small-help { font-size:.875rem; color:#6c757d; }

    /* Pale blue highlight for the Service Agreement box + border */
    .agreement-box {
      background-color:#e6f2ff !important;
      padding:10px;
      border-radius:8px;
      border:1px solid var(--brand-blue);
    }

    /* Alternating row colors with borders around each row “box” */
    .alt-wrap > .row.row-alt:nth-child(odd)  { background:var(--row-blue); }
    .alt-wrap > .row.row-alt:nth-child(even) { background:#ffffff; }
    .alt-wrap > .row.row-alt { 
      padding:10px 12px; 
      border-radius:6px; 
      border:1px solid var(--brand-blue); 
    }
    .alt-wrap > .row.row-alt + .row.row-alt { margin-top:8px; }

    /* “All boxes” — inputs, selects, checkbox, captcha get small dark-blue borders */
    .form-control,
    .form-select {
      border:1px solid var(--brand-blue) !important;
      box-shadow:none;
    }
    .form-control:focus,
    .form-select:focus {
      border-color:var(--brand-blue) !important;
      box-shadow:0 0 0 .2rem rgba(13,43,82,.15);
    }
    .form-check-input {
      border:1px solid var(--brand-blue);
    }
    .form-check-input:focus {
      border-color:var(--brand-blue);
      box-shadow:0 0 0 .2rem rgba(13,43,82,.15);
    }
    .captcha-img {
      border:1px solid var(--brand-blue);
      border-radius:.5rem;
    }
  </style>

  <script type="text/javascript">
    var IS_NEW = <% If isNew Then %>true<% Else %>false<% End If %>;

    function NewWindow(mypage, myname, w, h, pos, infocus) {
      var myleft, mytop;
      if (pos === "random") {
        myleft = (screen.width)? Math.floor(Math.random()*(screen.width-w)) : 100;
        mytop  = (screen.height)? Math.floor(Math.random()*((screen.height-h)-75)) : 100;
      } else if (pos === "center") {
        myleft = (screen.width)? (screen.width-w)/2 : 100;
        mytop  = (screen.height)? (screen.height-h)/2 : 100;
      } else { myleft = 0; mytop = 20; }
      var settings = "width=" + w + ",height=" + h + ",top=" + mytop + ",left=" + myleft +
                     ",scrollbars=yes,location=no,directories=no,status=no,menubar=no,toolbar=no,resizable=yes";
      var win = window.open(mypage, myname, settings);
      if (win && win.focus) win.focus();
    }

    function trim(v){ return (v||"").replace(/^\s+|\s+$/g,''); }
    function focusAndAlert(el, msg){
      alert(msg);
      try { el.focus(); } catch(e){}
      return false;
    }
    function validateEmailBasic(v){
      if (v.indexOf('@') === -1) return false;
      var re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      return re.test(v);
    }
    function validatePhoneBasic(v){
      var digits = (v.match(/\d/g) || []).length;
      return digits >= 7;
    }
    function validateZipBasic(v){
      return /^\d{5}(-?\d{4})?$/.test(v);
    }

    function validate(){
      var companyName = document.getElementById('companyName');
      var nameEl      = document.getElementById('name');
      var telephone   = document.getElementById('telephone');
      var faxNumber   = document.getElementById('faxNumber');
      var email       = document.getElementById('email');
      var billToName  = document.getElementById('billToName');
      var address     = document.getElementById('address');
      var city        = document.getElementById('city');
      var state       = document.getElementById('state');
      var zipcode     = document.getElementById('zipcode');
      var refferal    = document.getElementById('refferal');
      var conditions  = document.getElementById('conditions');
      var captcha     = document.getElementById('captchacode');

      if (!trim(companyName.value)) return focusAndAlert(companyName, "Please enter your company name.");
      if (!trim(nameEl.value))     return focusAndAlert(nameEl, "Please enter your name.");
      if (!trim(telephone.value))  return focusAndAlert(telephone, "Please enter your telephone number.");
      if (!validatePhoneBasic(telephone.value)) return focusAndAlert(telephone, "Please enter a valid telephone number.");

      if (IS_NEW) {
        if (!trim(faxNumber.value)) return focusAndAlert(faxNumber, "Please enter your fax number.");
        if (!trim(email.value))     return focusAndAlert(email, "Please enter your email address.");
      }
      if (trim(email.value) && !validateEmailBasic(email.value)) {
        return focusAndAlert(email, "Please enter a valid email address that includes '@' (e.g., name@example.com).");
      }

      if (!trim(billToName.value)) return focusAndAlert(billToName, "Please enter your name for billing.");
      if (!trim(address.value))    return focusAndAlert(address, "Please enter your address.");
      if (!trim(city.value))       return focusAndAlert(city, "Please enter your city.");
      if (!trim(state.value))      return focusAndAlert(state, "Please select a state.");
      if (!trim(zipcode.value))    return focusAndAlert(zipcode, "Please enter a ZIP code.");
      if (!validateZipBasic(zipcode.value)) return focusAndAlert(zipcode, "Please enter a valid ZIP code (5 digits or ZIP+4).");
      if (!trim(refferal.value))   return focusAndAlert(refferal, "Please tell us how you heard about IRSTAXRECORDS.com.");
      if (!conditions.checked)     { alert("You must agree with the Terms and Conditions to proceed."); conditions.focus(); return false; }

      if (!trim(captcha.value))    return focusAndAlert(captcha, "Please enter the security code.");
      return true;
    }

    function RefreshImage(id) {
      var img = document.getElementById(id);
      if (!img) return;
      var base = img.src.split("?")[0];
      img.src = base + "?x=" + new Date().toUTCString();
    }
    function checkCaptcha() {
      var codeEl = document.getElementById("captchacode");
      var code = codeEl ? codeEl.value : "";
      var lbl  = document.getElementById("lblMessage");
      if (!code) { if (lbl) lbl.textContent = "Enter Security Code."; return false; }
      fetch("matchcode.asp?code=" + encodeURIComponent(code), { cache: "no-store" })
        .then(function(r){ return r.text(); })
        .then(function(txt){
          if (txt.trim() === "0") {
            if (lbl) lbl.textContent = "Invalid Security Code.";
            if (codeEl) codeEl.value = "";
            RefreshImage("imgCaptcha");
            alert("Please enter a valid Security Code.");
            return false;
          } else {
            if (lbl) lbl.textContent = "";
            return true;
          }
        })
        .catch(function(){ /* ignore network errors */ });
      return true;
    }

    document.addEventListener("DOMContentLoaded", function(){
      var cc = document.getElementById("captchacode");
      if (cc) {
        cc.addEventListener("keyup", function (e) {
          if (e.key === "Enter") { checkCaptcha(); }
        });
      }
    });
  </script>
</head>
<body>
  <!-- Full-bleed banner -->

     
	  <!--#include file="headertest.asp"-->

  <main class="page-wrap px-3 px-md-0">
    <div class="mb-3"><img border="0" src="/img/wizardwelcome.gif" width="700" height="73" >
      <% If isNew Then %>
        <h1 class="h3 mb-1"></h1>
		
        <p class="text-muted mb-0">Register to create your online account and begin placing requests for IRS transcripts and Social Security Verifications</p>
      <% Else %>
        <h1 class="h3 mb-1">Member Confirmation</h1>
        <p class="text-muted mb-0">Edit the details below, then confirm to add/update the member record.</p>
      <% End If %>
    </div>

    <div class="card">
      <div class="card-body p-4 p-md-5">
        <form id="setup" name="setup" action="Wizardconfirmation.asp" method="post" onsubmit="return validate()" novalidate>

          <!-- Section: Client Information --><div class="mb-3">
          <div class="form-section-title">Client Information   </div>
          <div class="divider"></div>
        

          <div class="alt-wrap">
            <!-- Company Name row -->
            <div class="row g-3 row-alt">
              <div class="col-12">
                <label for="companyName" class="form-label required">Company Name</label>
                <input type="text" class="form-control" id="companyName" name="companyName" value="<%=Server.HTMLEncode(cname)%>" autocomplete="organization">
              </div>
            </div>

            <!-- Your Name row (under Company Name) -->
            <div class="row g-3 row-alt">
              <div class="col-12">
                <label for="name" class="form-label required">Your Name</label>
                <input type="text" class="form-control" id="name" name="name" value="<%=Server.HTMLEncode(name)%>" autocomplete="name">
              </div>
            </div>

            <!-- Contact row -->
            <div class="row g-3 row-alt">
              <div class="col-md-4">
                <label for="telephone" class="form-label required">Telephone</label>
                <input type="tel" class="form-control" id="telephone" name="telephone" value="<%=Server.HTMLEncode(phone)%>" autocomplete="tel">
              </div>
              <div class="col-md-4">
                <label for="faxNumber" class="form-label <% If isNew Then %>required<% End If %>">Fax Number</label>
                <input type="text" class="form-control" id="faxNumber" name="faxNumber" value="<%=Server.HTMLEncode(fax)%>">
              </div>
              <div class="col-md-4">
                <label for="email" class="form-label <% If isNew Then %>required<% End If %>">Email Address</label>
                <input type="email" class="form-control" id="email" name="email" value="<%=Server.HTMLEncode(email)%>" autocomplete="email">
              </div>
            </div>
          </div>

          <!-- Section: Client Billing -->
          <div class="mt-4 form-section-title">Client Billing Information</div>
          <div class="divider"></div>

          <div class="alt-wrap">
            <div class="row g-3 row-alt">
              <div class="col-md-6">
                <label for="billToName" class="form-label required">Bill to Name</label>
                <input type="text" class="form-control" id="billToName" name="billToName" value="<%=Server.HTMLEncode(bill)%>">
              </div>
              <div class="col-md-6">
                <label for="address" class="form-label required">Address</label>
                <input type="text" class="form-control" id="address" name="address" value="<%=Server.HTMLEncode(address)%>" autocomplete="address-line1">
              </div>
            </div>

            <div class="row g-3 row-alt">
              <div class="col-md-6">
                <label for="address1" class="form-label">Address 2</label>
                <input type="text" class="form-control" id="address1" name="address1" value="<%=Server.HTMLEncode(address1)%>" autocomplete="address-line2">
              </div>
              <div class="col-md-4">
                <label for="city" class="form-label required">City</label>
                <input type="text" class="form-control" id="city" name="city" value="<%=Server.HTMLEncode(city)%>" autocomplete="address-level2">
              </div>
              <div class="col-md-2">
                <label for="zipcode" class="form-label required">Zip</label>
                <input type="text" class="form-control" id="zipcode" name="zipcode" value="<%=Server.HTMLEncode(zip)%>" autocomplete="postal-code">
              </div>
            </div>

            <div class="row g-3 row-alt">
              <div class="col-md-4">
                <label for="state" class="form-label required">State</label>
                <select id="state" name="state" class="form-select">
                  <option value="">State?</option>
                  <%
                    Dim Item, op
                    For Each Item In state_array
                      If Len(Trim(Item)) > 0 Then
                        op = ""
                        If Item = stateName Then op = " selected"
                  %>
                    <option value="<%=Item%>"<%=op%>><%=Item%></option>
                  <%
                      End If
                    Next
                  %>
                </select>
              </div>
              <div class="col-md-8 d-flex align-items-end">
                <div class="small text-muted">Please ensure your billing address matches your payment method records.</div>
              </div>
            </div>
          </div>

          <!-- Section: Completion -->
          <div class="mt-4 form-section-title">Completion</div>
          <div class="divider"></div>

          <div class="alt-wrap">
            <!-- Row 1: Referral alone -->
            <div class="row g-3 row-alt">
              <div class="col-md-7">
                <label for="refferal" class="form-label required">How did you hear about IRSTAXRECORDS.com?</label>
                <select id="refferal" name="refferal" class="form-select">
                  <option value="">Choose one</option>
                  <%
                    Dim sele
                    For Each Item In referalls
                      If Len(Trim(Item)) > 0 Then
                        sele = ""
                        If Item = reff Then sele = " selected"
                  %>
                    <option value="<%=Item%>"<%=sele%>><%=Item%></option>
                  <%  End If : Next %>
                </select>
              </div>
            </div>

            <!-- Row 2: Service Agreement on its own row (full width, pale blue) -->
            <div class="row g-3 row-alt">
              <div class="col-12 agreement-box">
                <div class="form-check">
                  <input class="form-check-input" type="checkbox" id="conditions" name="conditions" value="yes" <%=acct%> />
                  <label class="form-check-label" for="conditions">
                    I accept the IRSTAXRECORDS.com
                    <a href="javascript:NewWindow('agreement.asp','agree','980','560','center','front');"><strong>Service Agreement</strong></a>.
                  </label>
                </div>
              </div>
            </div>

            <!-- Row 3: CAPTCHA stacked (Security Code above Enter code) -->
            <div class="row g-3 row-alt">
              <div class="col-md-6">
                <label class="form-label required">Security Code</label><br/>
                <img id="imgCaptcha" class="captcha-img" src="captcha.asp" alt="Captcha image" />
                <button type="button" class="btn btn-link btn-sm p-0 ms-2 align-text-bottom" onclick="RefreshImage('imgCaptcha')" title="Refresh security code">
                  Refresh
                </button>

                <label for="captchacode" class="form-label required mt-3">Enter code as shown</label>
                <input type="text" class="form-control" id="captchacode" name="captchacode" onblur="checkCaptcha()" autocomplete="off" />
                <div id="lblMessage" class="small-help mt-1" style="color:#dc3545;"></div>
              </div>
              <div class="col-md-6"></div>
            </div>
          </div>

          <hr class="my-4"/>

          <!-- Actions (preserved logic) -->
          <% If isNew Then %>
            <div class="d-flex gap-2">
              <button type="submit" name="submit" value="Submit Information" class="btn btn-primary">Submit Information</button>
              <button type="reset" class="btn btn-outline-secondary">Start Over</button>
            </div>
          <% Else %>
            <input type="hidden" name="id" value="<%=Server.HTMLEncode(un)%>" />
            <div class="d-flex flex-wrap gap-2">
              <button type="submit" name="submit" value="Confirm Member and Continue" class="btn btn-success">
                Confirm Member and Continue
              </button>
              <button type="submit" name="submit" value="Deny Membership" class="btn btn-outline-danger">
                Deny Membership
              </button>
            </div>
          <% End If %>
        </form>
      </div>
    </div>

    <% If Not isNew Then %>
      <div class="text-end small text-muted mt-3">
        <%
          Dim displayName, NewDate, nd, tm
          displayName = "Admin"
          NewDate = DateAdd("h", -2, Now())
          nd = Split(CStr(NewDate), " ")
          tm = Split(nd(1), ":")
        %>
        LOGGED IN <%=UCase(displayName)%> • IP <%=Request.ServerVariables("REMOTE_HOST")%> • <%=tm(0) & ":" & tm(1) & " " & nd(2)%> PT
      </div>
    <% End If %>
  </main><div class="footer-banner">
        <map name="FPMap1">
        <area href="https://www.irstaxrecords.com" shape="rect" coords="399, 57, 705, 143">
        <area href="mailto:info@irstaxrecords.com" shape="rect" coords="396, 230, 619, 260">
        <area href="https://www.irstaxrecords.com/expand7.asp" shape="rect" coords="803, 112, 1131, 143">
        <area href="https://www.irstaxrecords.com/expand7.asp#details" shape="rect" coords="801, 149, 1127, 170">
        <area href="https://www.irstaxrecords.com/expand7.asp" shape="rect" coords="799, 176, 1130, 199">
        <area href="https://www.irstaxrecords.com/wizard7.asp" shape="rect" coords="801, 203, 1127, 228">
        <area href="https://www.irstaxrecords.com/wizard7.asp" shape="rect" coords="1273, 99, 1497, 155">
        <area href="https://www.irstaxrecords.com/security.asp" shape="rect" coords="1273, 162, 1547, 194">
        <area href="https://www.irstaxrecords.com/forms.asp" shape="rect" coords="1269, 198, 1641, 245"></map><img src="https://www.irstaxrecords.com/new/images/Footer.jpg" alt="IRS Tax Records footer" width="1902" height="289" usemap="#FPMap1">
  </div>
</body>
</html>
