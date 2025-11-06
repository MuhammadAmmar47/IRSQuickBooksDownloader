<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.Master" CodeBehind="Login.aspx.vb" Inherits="IRSTaxUserPortal.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <style>
    .btn.btn-navy {
      background-color:#00498C;color:#fff;border:2px solid #00498C;border-radius:6px;transition:all .3s ease;
    }
    .btn.btn-navy:hover { background-color:#5dade2;border-color:#5dade2;color:#fff; }
  </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container py-5">
    <div class="row justify-content-center">
      <div class="col-lg-8 col-xl-7">
        <div class="card shadow-lg border-0 rounded-4 p-4">

          <div class="row align-items-center mb-4">
            <div class="col-md-6 text-center mb-3 mb-md-0">
              <img src="newsite/Login-.jpg" alt="IRS Tax Records"
                   style="width:656px !important;height:127px !important;max-width:none !important;" />
            </div>
            <div class="col-md-6 text-center">
              <h2 class="text-primary fw-bold"></h2>
            </div>
          </div>

          <div class="mb-3">
            <span class="failureNotification d-block">
              <asp:Literal ID="FailureText" runat="server" Visible="false"></asp:Literal>
              <asp:Label ID="lblErrorDetails" runat="server" CssClass="text-danger fw-bold fs-5" Visible="false">
                Authentication failed. Please confirm your username and password.
              </asp:Label>
            </span>
          </div>

          <!-- User ID (50% width on md+, centered; restored tall height) -->
          <div class="row mb-3">
            <div class="col-12 col-md-8 mx-auto">
              <label for="txtUsername" class="form-label fw-semibold text-secondary">User ID</label>
              <asp:TextBox ID="txtUsername" runat="server"
                CssClass="form-control form-control-lg border-primary"
                placeholder="Enter your user ID"></asp:TextBox>
            </div>
          </div>

          <!-- Password (50% width on md+, centered; restored tall height) -->
          <div class="row mb-4">
            <div class="col-12 col-md-8 mx-auto">
              <label for="txtPassword" class="form-label fw-semibold text-secondary">Password</label>
              <asp:TextBox ID="txtPassword" runat="server"
                CssClass="form-control form-control-lg border-primary"
                TextMode="Password"
                placeholder="Enter your password"></asp:TextBox>
            </div>
          </div>

          <!-- Button matches width; taller with btn-lg (optional) -->
          <div class="row">
            <div class="col-12 col-md-8 mx-auto d-grid">
              <asp:Button ID="btnSubmit" runat="server" Text="Continue" CssClass="btn btn-navy btn-lg" />
            </div>
          </div>

          <div class="row mt-4">
            <div class="col text-center">
              <h6 class="mb-0">
                <a href="/userid.asp">Forgot Password</a>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <strong class="text-primary"><a href="/wizard7.asp">Create an Account</a></strong>
              </h6>
            </div>
          </div>

        </div>
      </div>
    </div>

    <br />
    <div class="text-center">
      <h6 class="mb-0">Need help? Call <strong class="text-primary">1-866-848-4506</strong></h6>
    </div>
  </div>
</asp:Content>
