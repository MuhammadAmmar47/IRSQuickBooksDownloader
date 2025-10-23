<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Wizard7.aspx.vb" Inherits="IRSTaxUserPortal.Wizard7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8" />
    <title>IRS Tax Records — Client Setup</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <!-- Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

    <!-- Styles -->
    <style>
        :root {
            --brand-blue: #0d2b52; /* dark blue border color */
            --row-blue: #e9f3fc; /* light blue row */
        }

        body {
            background: #f6f7fb;
        }

        .brand-hero img {
            width: 100%;
            height: auto;
            display: block;
        }

        .page-wrap {
            max-width: 980px;
            margin: 24px auto;
        }

        /* Card container gets a subtle dark-blue border */
        .card {
            border-radius: 1rem;
            box-shadow: 0 10px 24px rgba(0,0,0,.04);
            border: 1px solid var(--brand-blue);
        }

        .form-section-title {
            font-weight: 700;
            font-size: 1.1rem;
            color: var(--brand-blue);
        }

        .divider {
            border-top: 1px solid #e9edf3;
            margin: .75rem 0 1.25rem;
        }

        .required::after {
            content: " *";
            color: #dc3545;
        }

        .small-help {
            font-size: .875rem;
            color: #6c757d;
        }

        /* Pale blue highlight for the Service Agreement box + border */
        .agreement-box {
            background-color: #e6f2ff !important;
            padding: 10px;
            border-radius: 8px;
            border: 1px solid var(--brand-blue);
        }

        /* Alternating row colors with borders around each row “box” */
        .alt-wrap > .row.row-alt:nth-child(odd) {
            background: var(--row-blue);
        }

        .alt-wrap > .row.row-alt:nth-child(even) {
            background: #ffffff;
        }

        .alt-wrap > .row.row-alt {
            padding: 10px 12px;
            border-radius: 6px;
            border: 1px solid var(--brand-blue);
        }

            .alt-wrap > .row.row-alt + .row.row-alt {
                margin-top: 8px;
            }

        /* “All boxes” — inputs, selects, checkbox, captcha get small dark-blue borders */
        .form-control,
        .form-select {
            border: 1px solid var(--brand-blue) !important;
            box-shadow: none;
        }

            .form-control:focus,
            .form-select:focus {
                border-color: var(--brand-blue) !important;
                box-shadow: 0 0 0 .2rem rgba(13,43,82,.15);
            }

        .form-check-input {
            border: 1px solid var(--brand-blue);
        }

            .form-check-input:focus {
                border-color: var(--brand-blue);
                box-shadow: 0 0 0 .2rem rgba(13,43,82,.15);
            }

        .captcha-img {
            border: 1px solid var(--brand-blue);
            border-radius: .5rem;
        }
    </style>

    <script type="text/javascript">
        var IS_NEW = true;

        function NewWindow(mypage, myname, w, h, pos, infocus) {
            var myleft, mytop;
            if (pos === "random") {
                myleft = (screen.width) ? Math.floor(Math.random() * (screen.width - w)) : 100;
                mytop = (screen.height) ? Math.floor(Math.random() * ((screen.height - h) - 75)) : 100;
            } else if (pos === "center") {
                myleft = (screen.width) ? (screen.width - w) / 2 : 100;
                mytop = (screen.height) ? (screen.height - h) / 2 : 100;
            } else { myleft = 0; mytop = 20; }
            var settings = "width=" + w + ",height=" + h + ",top=" + mytop + ",left=" + myleft +
                ",scrollbars=yes,location=no,directories=no,status=no,menubar=no,toolbar=no,resizable=yes";
            var win = window.open(mypage, myname, settings);
            if (win && win.focus) win.focus();
        }

        function trim(v) { return (v || "").replace(/^\s+|\s+$/g, ''); }
        function focusAndAlert(el, msg) {
            alert(msg);
            try { el.focus(); } catch (e) { }
            return false;
        }
        function validateEmailBasic(v) {
            if (v.indexOf('@') === -1) return false;
            var re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            return re.test(v);
        }
        function validatePhoneBasic(v) {
            var digits = (v.match(/\d/g) || []).length;
            return digits >= 7;
        }
        function validateZipBasic(v) {
            return /^\d{5}(-?\d{4})?$/.test(v);
        }

        function validate() {
            var companyName = document.getElementById('companyName');
            var nameEl = document.getElementById('name');
            var telephone = document.getElementById('telephone');
            var faxNumber = document.getElementById('faxNumber');
            var email = document.getElementById('email');
            var billToName = document.getElementById('billToName');
            var address = document.getElementById('address');
            var city = document.getElementById('city');
            var state = document.getElementById('state');
            var zipcode = document.getElementById('zipcode');
            var refferal = document.getElementById('refferal');
            var conditions = document.getElementById('conditions');
            var captcha = document.getElementById('captchacode');

            if (!trim(companyName.value)) return focusAndAlert(companyName, "Please enter your company name.");
            if (!trim(nameEl.value)) return focusAndAlert(nameEl, "Please enter your name.");
            if (!trim(telephone.value)) return focusAndAlert(telephone, "Please enter your telephone number.");
            if (!validatePhoneBasic(telephone.value)) return focusAndAlert(telephone, "Please enter a valid telephone number.");

            if (IS_NEW) {
                if (!trim(faxNumber.value)) return focusAndAlert(faxNumber, "Please enter your fax number.");
                if (!trim(email.value)) return focusAndAlert(email, "Please enter your email address.");
            }
            if (trim(email.value) && !validateEmailBasic(email.value)) {
                return focusAndAlert(email, "Please enter a valid email address that includes '@' (e.g., name@example.com).");
            }

            if (!trim(billToName.value)) return focusAndAlert(billToName, "Please enter your name for billing.");
            if (!trim(address.value)) return focusAndAlert(address, "Please enter your address.");
            if (!trim(city.value)) return focusAndAlert(city, "Please enter your city.");
            if (!trim(state.value)) return focusAndAlert(state, "Please select a state.");
            if (!trim(zipcode.value)) return focusAndAlert(zipcode, "Please enter a ZIP code.");
            if (!validateZipBasic(zipcode.value)) return focusAndAlert(zipcode, "Please enter a valid ZIP code (5 digits or ZIP+4).");
            if (!trim(refferal.value)) return focusAndAlert(refferal, "Please tell us how you heard about IRSTAXRECORDS.com.");
            if (!conditions.checked) { alert("You must agree with the Terms and Conditions to proceed."); conditions.focus(); return false; }

            if (!trim(captcha.value)) return focusAndAlert(captcha, "Please enter the security code.");
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
            var lbl = document.getElementById("lblMessage");
            if (!code) { if (lbl) lbl.textContent = "Enter Security Code."; return false; }
            fetch("matchcode.asp?code=" + encodeURIComponent(code), { cache: "no-store" })
                .then(function (r) { return r.text(); })
                .then(function (txt) {
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
                .catch(function () { /* ignore network errors */ });
            return true;
        }

        document.addEventListener("DOMContentLoaded", function () {
            var cc = document.getElementById("captchacode");
            if (cc) {
                cc.addEventListener("keyup", function (e) {
                    if (e.key === "Enter") { checkCaptcha(); }
                });
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <main class="page-wrap px-3 px-md-0">
        <div class="mb-3">
            <img border="0" src="https://www.irstaxrecords.com/wizardlogo.gif" width="85" height="73">

            <h1 class="h3 mb-1">Welcome to the Account Set up Wizard</h1>
            <p class="text-muted mb-0">Create your account and begin ordering IRS transcripts and Social Security Verifications.</p>

        </div>

        <div class="card">
            <div class="card-body p-4 p-md-5">
                <div id="setup" name="setup" action="Wizardconfirmation.asp" method="post" onsubmit="return validate()" novalidate>

                    <!-- Section: Client Information -->
                    <div class="mb-3">
                        <div class="form-section-title">Client Information   </div>
                        <div class="divider"></div>


                        <div class="alt-wrap">
                            <!-- Company Name row -->
                            <div class="row g-3 row-alt">
                                <div class="col-12">
                                    <label for="companyName" class="form-label required">Company Name</label>
                                    <asp:TextBox runat="server" type="text" class="form-control" ID="companyName" name="companyName" value="" autocomplete="organization" />
                                </div>
                            </div>

                            <!-- Your Name row (under Company Name) -->
                            <div class="row g-3 row-alt">
                                <div class="col-12">
                                    <label for="name" class="form-label required">Your Name</label>
                                    <asp:TextBox runat="server" type="text" class="form-control" ID="name" name="name" value="" autocomplete="name" />
                                </div>
                            </div>

                            <!-- Contact row -->
                            <div class="row g-3 row-alt">
                                <div class="col-md-4">
                                    <label for="telephone" class="form-label required">Telephone</label>
                                    <asp:TextBox runat="server" type="tel" class="form-control" ID="telephone" name="telephone" value="" autocomplete="tel" />
                                </div>
                                <div class="col-md-4">
                                    <label for="faxNumber" class="form-label required">Fax Number</label>
                                    <asp:TextBox runat="server" type="text" class="form-control" ID="faxNumber" name="faxNumber" value="" />
                                </div>
                                <div class="col-md-4">
                                    <label for="email" class="form-label required">Email Address</label>
                                    <asp:TextBox runat="server" type="email" class="form-control" ID="email" name="email" value="" autocomplete="email" />
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
                                    <asp:TextBox runat="server" type="text" class="form-control" ID="billToName" name="billToName" value="" />
                                </div>
                                <div class="col-md-6">
                                    <label for="address" class="form-label required">Address</label>
                                    <asp:TextBox runat="server" type="text" class="form-control" ID="address" name="address" value="" autocomplete="address-line1" />
                                </div>
                            </div>

                            <div class="row g-3 row-alt">
                                <div class="col-md-6">
                                    <label for="address1" class="form-label">Address 2</label>
                                    <asp:TextBox runat="server" type="text" class="form-control" ID="address1" name="address1" value="" autocomplete="address-line2" />
                                </div>
                                <div class="col-md-4">
                                    <label for="city" class="form-label required">City</label>
                                    <asp:TextBox runat="server" type="text" class="form-control" ID="city" name="city" value="" autocomplete="address-level2" />
                                </div>
                                <div class="col-md-2">
                                    <label for="zipcode" class="form-label required">Zip</label>
                                    <asp:TextBox runat="server" type="text" class="form-control" ID="zipcode" name="zipcode" value="" autocomplete="postal-code" />
                                </div>
                            </div>

                            <div class="row g-3 row-alt">
                                <div class="col-md-4">
                                    <label for="state" class="form-label required">State</label>
                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-select">
                                        <asp:ListItem Text="State?" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Alabama" Value="Alabama"></asp:ListItem>
                                        <asp:ListItem Text="Alaska" Value="Alaska"></asp:ListItem>
                                        <asp:ListItem Text="Arizona" Value="Arizona"></asp:ListItem>
                                        <asp:ListItem Text="Arkansas" Value="Arkansas"></asp:ListItem>
                                        <asp:ListItem Text="California" Value="California"></asp:ListItem>
                                        <asp:ListItem Text="Colorado" Value="Colorado"></asp:ListItem>
                                        <asp:ListItem Text="Connecticut" Value="Connecticut"></asp:ListItem>
                                        <asp:ListItem Text="DC, Washington" Value="DC, Washington"></asp:ListItem>
                                        <asp:ListItem Text="Delaware" Value="Delaware"></asp:ListItem>
                                        <asp:ListItem Text="Florida" Value="Florida"></asp:ListItem>
                                        <asp:ListItem Text="Georgia" Value="Georgia"></asp:ListItem>
                                        <asp:ListItem Text="Hawaii" Value="Hawaii"></asp:ListItem>
                                        <asp:ListItem Text="Idaho" Value="Idaho"></asp:ListItem>
                                        <asp:ListItem Text="Illinois" Value="Illinois"></asp:ListItem>
                                        <asp:ListItem Text="Indiana" Value="Indiana"></asp:ListItem>
                                        <asp:ListItem Text="Iowa" Value="Iowa"></asp:ListItem>
                                        <asp:ListItem Text="Kansas" Value="Kansas"></asp:ListItem>
                                        <asp:ListItem Text="Kentucky" Value="Kentucky"></asp:ListItem>
                                        <asp:ListItem Text="Louisiana" Value="Louisiana"></asp:ListItem>
                                        <asp:ListItem Text="Maine" Value="Maine"></asp:ListItem>
                                        <asp:ListItem Text="Maryland" Value="Maryland"></asp:ListItem>
                                        <asp:ListItem Text="Massachusetts" Value="Massachusetts"></asp:ListItem>
                                        <asp:ListItem Text="Michigan" Value="Michigan"></asp:ListItem>
                                        <asp:ListItem Text="Minnesota" Value="Minnesota"></asp:ListItem>
                                        <asp:ListItem Text="Mississippi" Value="Mississippi"></asp:ListItem>
                                        <asp:ListItem Text="Missouri" Value="Missouri"></asp:ListItem>
                                        <asp:ListItem Text="Montana" Value="Montana"></asp:ListItem>
                                        <asp:ListItem Text="Nebraska" Value="Nebraska"></asp:ListItem>
                                        <asp:ListItem Text="Nevada" Value="Nevada"></asp:ListItem>
                                        <asp:ListItem Text="New Hampshire" Value="New Hampshire"></asp:ListItem>
                                        <asp:ListItem Text="New Jersey" Value="New Jersey"></asp:ListItem>
                                        <asp:ListItem Text="New Mexico" Value="New Mexico"></asp:ListItem>
                                        <asp:ListItem Text="New York" Value="New York"></asp:ListItem>
                                        <asp:ListItem Text="North Carolina" Value="North Carolina"></asp:ListItem>
                                        <asp:ListItem Text="North Dakota" Value="North Dakota"></asp:ListItem>
                                        <asp:ListItem Text="Ohio" Value="Ohio"></asp:ListItem>
                                        <asp:ListItem Text="Oklahoma" Value="Oklahoma"></asp:ListItem>
                                        <asp:ListItem Text="Oregon" Value="Oregon"></asp:ListItem>
                                        <asp:ListItem Text="Pennsylvania" Value="Pennsylvania"></asp:ListItem>
                                        <asp:ListItem Text="South Carolina" Value="South Carolina"></asp:ListItem>
                                        <asp:ListItem Text="South Dakota" Value="South Dakota"></asp:ListItem>
                                        <asp:ListItem Text="Tennessee" Value="Tennessee"></asp:ListItem>
                                        <asp:ListItem Text="Texas" Value="Texas"></asp:ListItem>
                                        <asp:ListItem Text="Utah" Value="Utah"></asp:ListItem>
                                        <asp:ListItem Text="Vermont" Value="Vermont"></asp:ListItem>
                                        <asp:ListItem Text="Virginia" Value="Virginia"></asp:ListItem>
                                        <asp:ListItem Text="Washington" Value="Washington"></asp:ListItem>
                                        <asp:ListItem Text="West Virginia" Value="West Virginia"></asp:ListItem>
                                        <asp:ListItem Text="Wisconsin" Value="Wisconsin"></asp:ListItem>
                                        <asp:ListItem Text="Wyoming" Value="Wyoming"></asp:ListItem>
                                    </asp:DropDownList>

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
                                    <asp:DropDownList ID="ddlRefferal" runat="server" CssClass="form-select">
                                        <asp:ListItem Text="Choose one" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Referral" Value="Referral"></asp:ListItem>
                                        <asp:ListItem Text="Search Engines" Value="Search Engines"></asp:ListItem>
                                        <asp:ListItem Text="Magazine Ad" Value="Magazine Ad"></asp:ListItem>
                                        <asp:ListItem Text="Trade Show" Value="Trade Show"></asp:ListItem>
                                        <asp:ListItem Text="Sales Call" Value="Sales Call"></asp:ListItem>
                                        <asp:ListItem Text="Direct Mail" Value="Direct Mail"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>

                            <!-- Row 2: Service Agreement on its own row (full width, pale blue) -->
                            <div class="row g-3 row-alt">
                                <div class="col-12 agreement-box">
                                    <div class="form-check">
                                        <asp:CheckBox ID="conditions" class="form-check-input" runat="server" />
                                        <label class="form-check-label" for="conditions">
                                            I accept the IRSTAXRECORDS.com
                    <a href="javascript:NewWindow('agreement.htm#fax','agree','480','560','center','front');"><strong>Service Agreement</strong></a>.
                                        </label>
                                    </div>
                                </div>
                            </div>

                            <!-- Row 3: CAPTCHA stacked (Security Code above Enter code) -->
                            <div class="row g-3 row-alt">
                                <div class="col-md-6">
                                    <label class="form-label required">Security Code</label><br />
                                    <img id="imgCaptcha" class="captcha-img" src="captcha.asp" alt="Captcha image" />
                                    <button type="button" class="btn btn-link btn-sm p-0 ms-2 align-text-bottom" onclick="RefreshImage('imgCaptcha')" title="Refresh security code">
                                        Refresh
                                    </button>

                                    <label for="captchacode" class="form-label required mt-3">Enter code as shown</label>
                                    <asp:TextBox runat="server" type="text" class="form-control" id="captchacode" name="captchacode" onblur="checkCaptcha()" autocomplete="off" />
                                    <div id="lblMessage" class="small-help mt-1" style="color: #dc3545;"></div>
                                </div>
                                <div class="col-md-6"></div>
                            </div>
                        </div>

                        <hr class="my-4" />

                        <!-- Actions (preserved logic) -->

                        <div class="d-flex gap-2">
                            <asp:Button runat="server" type="submit" name="submit" class="btn btn-primary" Text="Submit Information"></asp:Button>
                            <asp:Button runat="server" type="reset" class="btn btn-outline-secondary" Text="Start Over"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
    </main>
</asp:Content>
