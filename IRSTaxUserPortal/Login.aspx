<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.Master" CodeBehind="Login.aspx.vb" Inherits="IRSTaxUserPortal.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-5">
        <div class="row justify-content-center">
            <div class="col-lg-8 col-xl-6">
                <div class="card shadow-lg border-0 rounded-4 p-4">

                    <div class="row align-items-center mb-4">
                        <!-- Left: Logo + Site Name -->
                        <div class="col-md-6 text-center mb-3 mb-md-0">
                            <img src="newsite/Login-.jpg"
     alt="IRS Tax Records"
     style="width:556px !important; height:107px !important; max-width:none !important;"
                            <h6 class="text-primary fw-semibold fs-5 mb-0">
                            </h6>
                        </div>
                        <div class="col-md-6 text-center">
                            <h2 class="text-primary fw-bold"></h2>
                        </div>
                    </div>
                    <div class="mb-3">
                        <span class="failureNotification">
                            <asp:Literal ID="FailureText" runat="server" Visible="false"></asp:Literal>
                            <asp:Label ID="lblErrorDetails" runat="server">
                            <p><b><font color="#FF0000"><font size="4"><center>
                                Authentication failed. Please confirm your username and password </font>
                            </asp:Label>
                            </font></b>
                        </span>
                    </div>
                    <div class="mb-3">
                        <label for="userId" class="form-label fw-semibold text-secondary">User ID</label>
                        <asp:TextBox ID="txtUsername" runat="server" class="form-control form-control-lg border-primary" placeholder="Enter your user ID" required></asp:TextBox>
                    </div>

                    <div class="mb-4">
                        <label for="password" class="form-label fw-semibold text-secondary">Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" class="form-control form-control-lg border-primary" TextMode="Password" placeholder="Enter your password" required></asp:TextBox>
                    </div>

                    <div class="d-grid">
					<style>
.btn.btn-navy {
  background-color: #00498C; /* solid navy */
  color: white;
  border: 2px solid #00498C;
  border-radius: 6px;
  transition: all 0.3s ease;
}

.btn.btn-navy:hover {
  background-color: #5dade2; /* pale blue hover */
  border-color: #5dade2;
  color: #fff;
}
</style>

                       <asp:Button ID="btnSubmit" runat="server" Text="Continue" CssClass="btn btn-navy" />
                    </div>

                    <div class="d-flex justify-content-between mt-3">
                        <%--<a href="/userid.asp" class="text-decoration-none small text-primary fw-semibold">Forgot Password?</a>--%>
                        <%--<a href="/wizard7.asp" class="text-decoration-none small text-primary fw-semibold">Create an account</a>--%>
                    </div>

                    <div class="row mt-4">
                        <div class="col text-center">
                            <h6 class="mb-0"><a href="/userid.asp">Forgot Password</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 <strong class="text-primary">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 <a href="/wizard7.asp">Create an Account</a></strong>
                            </h6>
                        </div>
                    </div>
                </div>
            </div>
        </div> <br><center><h6 class="mb-0">Need help? Call <strong class="text-primary">1-866-848-4506</strong>
    </div>
</asp:Content>