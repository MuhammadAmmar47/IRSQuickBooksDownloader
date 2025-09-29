<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.Master"
    CodeBehind="Confirmation.aspx.vb" Inherits="IRSTaxUserPortal.Confirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Order 4506-C</title>
    <!-- Page-specific CSS/JS -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-center align-items-center vh-100">
        <div class="text-center bg-white shadow rounded p-5" style="max-width: 600px; width: 100%;">

            <!-- Icon and Heading -->
            <div class="mb-4">
                <i class="fas fa-file-signature fa-5x text-info"></i>
                <asp:Label ID="lblFormHeading" runat="server" CssClass="mt-3 fw-bold text-dark h2"></asp:Label>
                <h4 class="text-primary fw-semibold">UPLOAD PDF</h4>
                <p class="text-muted">for IRS Tax Information</p>
            </div>

            <!-- Confirmation Message -->
            <div class="mb-5">
                <h3 class="text-success fw-bold">Your Order has been Submitted</h3>
            </div>

            <!-- Buttons -->
            <div class="d-grid gap-3">
                <asp:HyperLink ID="lnkSubmitAnother" runat="server"
                    CssClass="btn btn-outline-info btn-lg rounded-pill fw-semibold">
                    <i class="fas fa-plus-circle me-2"></i> Submit another Form
                </asp:HyperLink>
                <a href="Welcome.aspx" class="btn btn-info btn-lg rounded-pill fw-semibold text-white">
                    <i class="fas fa-user-circle me-2"></i> Go back to your online account
                </a>
            </div>

        </div>
    </div>
</asp:Content>
