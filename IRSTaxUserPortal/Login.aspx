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
                            <img src="/img/logo.png.png" alt="IRS Logo" class="img-fluid mb-2" style="max-height: 80px;" />
                            <h6 class="text-primary fw-semibold fs-5 mb-0">
                                IRStaxrecords.com
                            </h6>
                        </div>
                        <div class="col-md-6 text-center">
                            <h2 class="text-primary fw-bold">Welcome back!</h2>
                        </div>
                    </div>                        
                        <div class="mb-3">
                             <span class="failureNotification">
                                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                            </span>
                        </div>
                        <div class="mb-3">
                            <label for="userId" class="form-label fw-semibold text-secondary">User ID</label>
                            <asp:TextBox id="txtUsername" runat="server" class="form-control form-control-lg border-primary" placeholder="Enter your user ID" required></asp:TextBox>
                        </div>

                        <div class="mb-4">
                            <label for="password" class="form-label fw-semibold text-secondary">Password</label>
                            <asp:TextBox id="txtPassword" runat="server" class="form-control form-control-lg border-primary" placeholder="Enter your password" required></asp:TextBox>
                        </div>

                        <div class="d-grid">
                            
                            <asp:Button id="btnSubmit" runat="server" Text=" Continue" class="btn btn-primary btn-lg shadow rounded-pill fw-semibold"/>
                        </div>

                    <div class="d-flex justify-content-between mt-3">
                        <a href="#" class="text-decoration-none small text-primary fw-semibold">Forgot Password?</a>
                        <a href="#" class="text-decoration-none small text-primary fw-semibold">Create an account</a>
                    </div>

                    <div class="row mt-4">
                        <div class="col text-center">
                            <h6 class="mb-0">
                                Need help? Call <strong class="text-primary">1-866-848-4506</strong>
                            </h6>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <hr class="border border-primary border-2 opacity-100 my-4">
</asp:Content>
