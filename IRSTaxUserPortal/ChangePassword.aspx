<%@ Page Title="Change Password"
    Language="vb"
    AutoEventWireup="false"
    MasterPageFile="~/Main.Master"
    CodeBehind="ChangePassword.aspx.vb"
    Inherits="IRSTaxUserPortal.ChangePassword" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <style>
        
        #confirm-shell{
            display:flex;
            justify-content:center;
            align-items:flex-start;   
            margin-top:25px;          
            padding-bottom: clamp(24px, 5vh, 56px);  
        }
    </style>
</asp:Content>

<asp:Content ID="MainContentArea" ContentPlaceHolderID="MainContent" runat="server">
    <div id="change-password-shell">
    <div class="container">
        <div class="text-center bg-white shadow rounded p-5 mx-auto" style="max-width:600px;">

            <!-- Icon and Heading -->
            <div class="mb-4">
                <i class="fas fa-key fa-3x text-info mb-3"></i>
                <h2 class="fw-bold text-dark">Change Your Password</h2>
                <p class="text-muted">Please enter your current password and choose a new one.</p>
            </div>

            <!-- Password Change Form -->
            <asp:Panel ID="pnlChangePassword" runat="server" DefaultButton="btnChangePassword">
                <div class="mb-3 text-start">
                    <label for="txtCurrentPassword" class="form-label fw-semibold">Current Password</label>
                    <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="form-control form-control-lg" TextMode="Password" placeholder="Enter current password"></asp:TextBox>
                </div>

                <div class="mb-3 text-start">
                    <label for="txtNewPassword" class="form-label fw-semibold">New Password</label>
                    <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control form-control-lg" TextMode="Password" placeholder="Enter new password"></asp:TextBox>
                </div>

                <div class="mb-4 text-start">
                    <label for="txtConfirmPassword" class="form-label fw-semibold">Confirm New Password</label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control form-control-lg" TextMode="Password" placeholder="Re-enter new password"></asp:TextBox>
                </div>

                <!-- Validation Message -->
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger fw-semibold d-block mb-3"></asp:Label>

                <!-- Buttons -->
                <div class="d-grid gap-3">
                    <asp:Button ID="btnChangePassword" runat="server" Text="Change Password"
                        CssClass="btn btn-info btn-lg rounded-pill fw-semibold"
                        Style="
                            --bs-btn-bg:#00498C;
                            --bs-btn-border-color:#00498C;
                            --bs-btn-color:#ffffff;
                            --bs-btn-hover-bg:#ADD8E6;
                            --bs-btn-hover-border-color:#ADD8E6;
                            --bs-btn-hover-color:#003366;
                        " />

                    <a href="Welcome.aspx"
                        class="btn btn-outline-secondary btn-lg rounded-pill fw-semibold">
                        <i class="fas fa-arrow-left me-2"></i> Back to Account
                    </a>
                </div>
            </asp:Panel>
        </div>
    </div>
</div>

</asp:Content>
