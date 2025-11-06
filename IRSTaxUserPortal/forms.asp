<!--
  Forgot Password (NEW.asp-style shell) â€” Classic ASP
  - Uses DB connection and app SMTP settings from header.asp
  - Preserves original classic ASP logic (lookup by userId, send CDO email)
-->
<!--#include file="header.asp"-->
<%
' ---------- server logic ----------
Dim isPost : isPost = (Request.ServerVariables("REQUEST_METHOD") = "POST")
Dim statusMsg, statusType

If isPost Then
  Dim usr : usr = Trim(Request.Form("userId"))
  If usr = "" Then
    statusMsg = "Please enter your User ID"
    statusType = "danger"
  Else
    Dim safeUsr : safeUsr = Replace(usr, "'", "''")
    Dim sql, rs : sql = "SELECT * FROM customer WHERE userId = '" & safeUsr & "'"
    Set rs = Conn.Execute(sql)
    If rs.EOF Then
      ' Same behavior as legacy script
      Response.Redirect "login3.htm"
    Else
      Dim email, password
      email = rs("email")
      password = rs("password")

      On Error Resume Next
      Dim config, sch : sch = "http://schemas.microsoft.com/cdo/configuration/"
      Set config = CreateObject("CDO.Configuration")
      With config.Fields
        .Item(sch & "sendusing")       = 2
        .Item(sch & "smtpserver")      = Application("smtpserver")
        .Item(sch & "smtpserverport")  = Application("smtpport")
        .Item(sch & "smtpauthenticate")= 1
        .Item(sch & "sendusername")    = Application("smtpusername")
        .Item(sch & "sendpassword")    = Application("smtppassword")
        .Update
      End With

      If ("" & password) = "" Then
        ' Account pending confirmation â€” send the manual HTML message
        Dim msg
        msg = "Dear " & rs("name") & ":<br>" & _
              "Your account is under review for confirmation. You may send in requests with your<br>" & _
              "Temporary I.D., however, you will not be allowed to view your records online until your<br>" & _
              "account is confirmed. The entire process will take less than 24 hours. You will<br>" & _
              "receive confirmation of your account shortly.<br><br>" & _
              "There is no reason to wait for confirmationâ€”send in your cover letter and Form 4506<br>" & _
              "as outlined on the <a href='http://www.IRSTAXRECORDS.com'>http://www.IRSTAXRECORDS.com</a> site and receive both your borrowers'<br>" & _
              "tax records and your account confirmation shortly.<br><br>" & _
              "We look forward to being of service.<br><br>" & _
              "Tom Irwin<br>" & _
              "<a href='mailto:tirwin@irstaxrecords.com'>tirwin@irstaxrecords.com</a><br>"
        Dim m : Set m = CreateObject("CDO.Message")
        Set m.Configuration = config
        m.To       = email
        m.From     = Application("smtpusername")
        m.Subject  = "Password from irstaxrecords.com"
        m.HTMLBody = msg
        m.Send
        Set m = Nothing
      Else
        ' Template-based email (EMAILS.EmailID = 3)
        Dim emailRS, sqltemp, message
        sqltemp = "SELECT * FROM EMAILS WHERE EmailID = 3"
        Set emailRS = Conn.Execute(sqltemp)
        message = emailRS("emailmessage")
        message = Replace(message, "<name>",  rs("name"))
        message = Replace(message, "<passwd>", password)
        message = Replace(message, vbCrLf, "<br>")

        Dim m2 : Set m2 = CreateObject("CDO.Message")
        Set m2.Configuration = config
        m2.To       = email
        m2.From     = Application("smtpusername")
        m2.Subject  = "Password from irstaxrecords.com"
        m2.HTMLBody = message
        m2.Send
        Set m2 = Nothing
        If Not emailRS Is Nothing Then emailRS.Close : Set emailRS = Nothing
      End If

      If Err.Number = 0 Then
        statusMsg  = "We've sent your password to the email address on file for User ID " & Server.HTMLEncode(usr) & ".<br> If you continue to have trouble, please call 1&#8209;866&#8209;848&#8209;4506."
        statusType = "success"
      Else
        statusMsg  = "We couldn't send the email right now (error " & Err.Number & "). Please call 1&#8209;866&#8209;848&#8209;4506 or try again later."
        statusType = "danger"
        Err.Clear
      End If
      On Error GoTo 0
    End If
    rs.Close : Set rs = Nothing
  End If
End If
%>
<!doctype html>
<html lang="en">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>Email Password Request</title>
  <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
   <style>
        .btn-custom {
            background-color: #EAFAFF;
            border: 2px solid navy; /* Increased border width for visibility */
            color: navy;
            width: 300px; /* Set button width to 300px */
        }
        .btn-custom:hover {
            background-color: #00238C;
            color: white;
        }
    </style>
  <style>
    :root{
      --brand:#00498C;        /* IRStaxRecords navy */
      --accent:#f7b500;       /* accent */
      --ink:#0b1324;          /* body text */
      --muted:#5f6b7a;        /* secondary */
      --ring:rgba(0,0,0,.08);
      --panel:#ffffff;        /* card bg */
      --bg:#E8EEF7;           /* page bg */
      --radius:16px;          /* big rounded corners */
      --shadow:0 10px 30px rgba(2,12,27,.08), 0 2px 8px rgba(2,12,27,.06);
    }
    html,body{height:100%}
    body{margin:0;background:var(--bg);font-family:system-ui,-apple-system,Segoe UI,Roboto,Helvetica,Arial,sans-serif;color:var(--ink)}
    .wrap{max-width:880px;margin:32px auto;padding:16px}
    .card{background:var(--panel);border-radius:var(--radius);box-shadow:var(--shadow);overflow:hidden}
    .card-head{padding:24px;border-bottom:1px solid #e7ecf2;background:linear-gradient(0deg, #fff, #fff), radial-gradient(1200px 100px at 50% -50%, rgba(0,73,140,.06), transparent)}
    .title{margin:0;font-size:28px;line-height:1.2;color:var(--brand)}
    .muted{color:var(--muted);font-size:14px}
    .card-body{padding:24px}
    label{display:block;font-weight:600;margin:0 0 8px}

    /* Ensure inputs never exceed the card; set base look */
    input[type=text]{ 
      box-sizing: border-box;
      width:100%;
      padding:12px 14px;
      border:1px solid #d7dee6;
      border-radius:12px;
      outline:none
    }
    input[type=text]:focus{border-color:var(--brand);box-shadow:0 0 0 4px rgba(0,73,140,.12)}

    
    .input-narrow{ max-width:420px; width:100%; }

   
    .is-error{
      background:#FFFFFF!important;
      border:1px solid #dc3545 !important;
      color:#6b0000 !important;
    }
    #userId.is-error::placeholder{ color:#a94442; opacity:.85; }
    #userId.is-error:focus{
      background:#ffffff !important;
      border-color:#b02a37 !important;
      box-shadow:0 0 0 4px rgba(220,53,69,.15) !important;
    }

    .actions{margin-top:16px;display:flex;gap:12px;align-items:center}
    .btn{display:inline-block;padding:12px 18px;font-weight:700;border-radius:999px;border:0;cursor:pointer;text-decoration:none}

    /* Primary button + hover */
    .btn-primary{background:var(--brand);color:#fff;border:1px solid var(--brand)}
    .btn-primary:hover,
    .btn-primary:focus,
    .btn-primary:active{
      background-color:#1c6fb8 !important;
      border-color:#1c6fb8 !important;
    }

    /* Ghost button base + white text on hover (per your earlier request) */
    .btn-ghost{
      background:#eef4fa !important;
      border:1px solid #00498C !important;
      color:#00498C !important;
    }
    .btn-ghost:hover,
    .btn-ghost:focus,
    .btn-ghost:active{
      background-color:#1c6fb8 !important;
      border-color:#1c6fb8 !important;
      color:#ffffff !important;
    }
    .btn-ghost:hover i,
    .btn-ghost:hover .fa,
    .btn-ghost:hover .bi{
      color:#ffffff !important;
    }

    .alert{border-radius:12px;padding:12px 16px;margin:0 0 16px}
    .alert-success{background:#ecf9f1;border:1px solid #b6efce}
    .alert-danger{background:#fff2f2;border:1px solid #ffc9c9}
    .stack{display:grid;gap:20px}
    .options{padding:20px;border-top:1px dashed #FFE1E1;background:#FEEDD6}
    .opt{display:flex;gap:12px;align-items:start}
    .opt img{width:36px;height:36px}
    .grid{display:grid;gap:18px}
    @media(min-width:640px){.grid{grid-template-columns:1fr 1fr}}
    .footnote{font-size:14px;color:var(--muted);text-align:center;margin-top:24px}
  </style>

  <script>
  function validateForm(){
    var f = document.getElementById('pwForm');
    var v = f.userId.value.replace(/^\s+|\s+$/g,'');
    if(!v){ alert('Please enter your User ID'); f.userId.focus(); return false; }
    return true;
  }

  // Optional: clear red state once user types something
  (function () {
    var el = document.getElementById('userId');
    if(!el) return;
    el.addEventListener('input', function(){
      if (this.value.trim()) { this.classList.remove('is-error'); }
      else { this.classList.add('is-error'); }
    });
  })();

  function NewWindow(mypage,myname,w,h,pos){
    var myleft=0,mytop=20;
    if(pos==="center"){myleft=(screen.width?(screen.width-w)/2:100);mytop=(screen.height?(screen.height-h)/2:100)}
    var settings="width="+w+",height="+h+",top="+mytop+",left="+myleft+",scrollbars=yes,location=no,directories=no,status=no,menubar=no,toolbar=no,resizable=no";
    var win = window.open(mypage,myname,settings); if(win) win.focus();
  }
  </script>
</head>
<body>
  <div class="wrap">
    <div class="card">
      <div class="card-head">
        <h1 class="title"><img border="0" src="https://www.irstaxrecords.com/img/USERID.gif" width="192" height="82" alt="User ID"></h1>
        <h1 class="title">Forms&nbsp;Download Section&nbsp;</h1>
        <div class="muted">The forms required for IRS tax transcripts and Social
          Security verification are listed below.<br>If you’re unsure which form
          to use, please contact Customer Service</div>
      </div>
      <div class="card-body">
        <% If Len(statusMsg & "") > 0 Then %>
          <div class="alert alert-<%=statusType%>"><%=statusMsg%></div>
        <% End If %>

        <form id="pwForm" method="post" onsubmit="return validateForm()">
          <div class="stack">
            <div>
              <a href="https://www.irs.gov/pub/irs-pdf/f4506c.pdf" target="_blank">
              Download IRS form 4506-C</a> to order IRS transcripts: Use 4506-C to
              obtain 1040, W2, 1099, Record of Account, Account Transcripts,
              Corporate 1120 and 1065 and more.
              <p><a href="https://www.irs.gov/pub/irs-pdf/f8821.pdf" target="_blank">Download IRS form 8821</a> to order IRS transcripts: Use 8821 to
              obtain expedited service on tax transcripts 1040, W2, 1099, Record
              of account, Account Transcripts, Corporate 1120 and 1065 and more.</p>
              <p><a href="https://www.ssa.gov/forms/ssa-89.pdf" target="_blank">Download Social Security Validation Request form&nbsp;
              SSA-89</a>:
              Use form SSA89 to obtain Social Security Verifications via the
              Social Security Administration
            </div>
<button class="btn btn-custom d-block mx-auto" onclick="history.go(-1);">Return to Previous Page</button>
          </div>
        </form>
<div class="opt">
            <div class="rounded-card">
  
    
    <p class="line"><strong></strong><br>
</p>
  </div>
</div>              &nbsp;
              <div><center>
                If you are not sure about what form to use please contact<br>
                 <strong>Customer Service: 1-866-848-4506</strong><br>
              </div>
            </div>
            <div class="opt">
            </div>
          </div>
          <p class="footnote"><strong><!--#include file="footer3.asp"-->
</strong></p>
        </div>
      </div>
    </div>
  </div>

  <!-- Optional shared site footer include -->
 
</body>
</html>
