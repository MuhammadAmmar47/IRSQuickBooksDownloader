<%@ Page Title="Change Password"
    Language="vb"
    AutoEventWireup="false"
    MasterPageFile="~/Main.Master"
    CodeBehind="ChangePassword.aspx.vb"
    Inherits="IRSTaxUserPortal.ChangePassword" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
  <style>
    :root{
      --brand:#00498C;
      --brand-ink:#0b2a4a;
      --brand-light:#e8f2fb; /* not used here */
    }

    /* Center the outer section */
    #change-password-shell{
      display:flex;
      justify-content:center;
      margin-top:15px;
      margin-bottom:19px;
    }

    /* Outer card */
    .card-outer{
      width: clamp(320px, 95vw, 800px);
      flex: 0 0 auto;
      margin: 24px auto 48px;
      background:#F9FFFF;
      font-size:.93rem;              
      line-height:1.45;
    }

    /* Constrain inner cards */
    .inner-constrain{
      width: 100%;
      max-width: 540px;              
      margin-inline: auto;
    }

    /* Inside layout spacing */
    .inner-stack{
      display: grid;
      gap: 1rem;
    }

    .card-outer .h4{ font-size:1.1rem; }

    /* Guidelines nested card */
    .nested-card{
      border:1px solid var(--brand);
      background:#FFFFFF;
    }
    /* Smaller text inside nested card */
    .nested-card .card-body{
      font-size:.88rem;
      line-height:1.4;
    }

    /* Bullets tidy */
    .flush-bullets{
      list-style: disc outside;
      padding-left: 1.25rem;
      margin: 0;
    }
    .flush-bullets li{
      text-indent: 0;
      margin-left: 0;
      margin-bottom: .25rem;
    }

    /* Labels & inputs slightly smaller */
    .card-outer .form-label{ font-size:.9rem; }
    .card-outer .form-control{ font-size:.92rem; padding:.375rem .5rem; } /* fallback if -sm class missed */

    /* Reduce button scale */
    .card-outer .btn{ font-size:.92rem; padding:.4rem .9rem; }

    .lock-icon{ width:110px; height:102px; }
  </style>
</asp:Content>

<asp:Content ID="MainContentArea" ContentPlaceHolderID="MainContent" runat="server">
  <div id="change-password-shell" class="container">
    <!-- OUTER CARD -->
    <div class="card shadow rounded-4 mx-auto card-outer">
      <div class="card-body p-4 p-md-1">

        <!-- Section header -->
        <div class="text-center mb-3">
          <img src="https://www.irstaxrecords.com/img/lock.gif" alt="Padlock icon" class="lock-icon" />
          <h1 class="h4 fw-bold text-dark mt-1 mb-0">Change Password</h1>
        </div>

        <!-- Inside content (constrained width + spacing) -->
        <div class="inner-stack">

          <!-- Guidelines card (same width as form) -->
          <div class="card nested-card rounded-3 inner-constrain">
            <div class="card-body py-3 py-md-3">
              <ul class="flush-bullets mb-0">
                <li>Minimum of 8 characters</li>
                <li>At least 1 uppercase letter (A–Z)</li>
                <li>At least 1 lowercase letter (a–z)</li>
                <li>At least 1 digit (0–9)</li>
                <li>At least 1 special character (e.g., @ # $ * !)</li>
              </ul>
            </div>
          </div>

          <!-- FORM card (same width) -->
          <asp:Panel ID="pnlChangePassword" runat="server" DefaultButton="btnChangePassword">
            <div class="card rounded-3 shadow-sm inner-constrain">
              <div class="card-body">
                <div class="mb-2 text-start">
                  <label for="txtCurrentPassword" class="form-label fw-semibold">Current Password</label>
                  <asp:TextBox ID="txtCurrentPassword" runat="server"
                               CssClass="form-control form-control-sm"
                               TextMode="Password" placeholder="Enter current password" />
                </div>

                <div class="mb-2 text-start">
                  <label for="txtNewPassword" class="form-label fw-semibold">New Password</label>
                  <asp:TextBox ID="txtNewPassword" runat="server"
                               CssClass="form-control form-control-sm"
                               TextMode="Password" placeholder="Enter new password" />
                </div>

                <div class="mb-3 text-start">
                  <label for="txtConfirmPassword" class="form-label fw-semibold">Confirm New Password</label>
                  <asp:TextBox ID="txtConfirmPassword" runat="server"
                               CssClass="form-control form-control-sm"
                               TextMode="Password" placeholder="Re-enter new password" />
                </div>

                <!-- Validation Message -->
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger fw-semibold d-block mb-2"></asp:Label>

                <!-- Button (smaller) -->
                <div class="d-grid">
                  <asp:Button ID="btnChangePassword" runat="server" Text="Change Password"
                              CssClass="btn btn-info btn-sm rounded-pill fw-semibold"
                              Style="
                                --bs-btn-bg:#00498C;
                                --bs-btn-border-color:#00498C;
                                --bs-btn-color:#ffffff;
                                --bs-btn-hover-bg:#ADD8E6;
                                --bs-btn-hover-border-color:#ADD8E6;
                                --bs-btn-hover-color:#00366;
                              " />
                </div>
              </div>
            </div><p>
          </asp:Panel>

        </div><!-- /.inner-stack -->

      </div>
    </div>
  </div>
</asp:Content>
