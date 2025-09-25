<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Confirmation.aspx.vb" Inherits="IRSTaxUserPortal.Confirmation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Confirmation</title>
    <!-- Bootstrap Icons -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />
    <!-- Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
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
                <a href="default.aspx" class="btn btn-info btn-lg rounded-pill fw-semibold text-white">
                    <i class="fas fa-user-circle me-2"></i> Go back to your online account
                </a>
            </div>

        </div>
    </div>

    </form>
</body>
</html>
