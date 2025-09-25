<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="order_8821.aspx.vb" Inherits="IRSTaxUserPortal.order_8821" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>order_8821</title>
    <!-- Bootstrap Icons -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />
    <!-- Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
         <!-- Centering Wrapper -->    <div class="d-flex justify-content-center align-items-center min-vh-100 px-2">
        <div class="card shadow border-0 w-100" style="max-width: 800px;">

            <!-- Header -->
            <div class="card-header d-flex align-items-center gap-3 bg-primary bg-opacity-10">
                <i class="fa-solid fa-file-invoice-dollar fa-3x text-primary"></i>
                <div>
                    <h4 class="mb-1 fw-bold">Form 8821</h4>
                    <small class="text-primary fs-6 fw-semibold">Upload PDF for IRS Transcripts</small>
                </div>
            </div>

            <!-- Body -->
            <div class="card-body p-3">
                <!-- Row 1: Name, SSN, Loan -->
                <div class="row mb-4 pb-3 border-bottom border-dark">
                    <div class="col-md-4 mb-2">
                        <label class="form-label fw-semibold small">Taxpayer Name:</label>
                        <asp:TextBox ID="txtTaxPayerName" class="form-control" runat="server" required  MaxLength="50" ></asp:TextBox>
                    </div>
                    <div class="col-md-4 mb-2">
                        <label class="form-label fw-semibold small">Social Security Number:</label>
                         <asp:TextBox ID="txtSocialSecurityNumber" class="form-control" runat="server" required  MaxLength="50" ></asp:TextBox>
                    </div>
                    <div class="col-md-4 mb-2">
                        <label class="form-label fw-semibold small">Loan Number:</label>
                        <asp:TextBox ID="txtLoanNumber" class="form-control" runat="server" required  MaxLength="50" ></asp:TextBox>
                    </div>
                </div>

                <!-- Row 2: Tax Years -->
                <div class="mb-4 pb-3 border-bottom border-dark">
                    <label class="form-label fw-semibold small mb-2 d-block">Tax Years Requested:</label>
                    <asp:CheckBoxList ID="chkTaxyears" runat="server" 
                        RepeatDirection="Horizontal" 
                        RepeatLayout="Flow"
                        CssClass="d-flex flex-wrap gap-3">
                        <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                        <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                        <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                        <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                        <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                    </asp:CheckBoxList>
                </div>

                <!-- Row 3: Forms -->
                <div class="mb-4 pb-3 border-bottom border-dark">
                    <label class="form-label fw-semibold small mb-2 d-block">Forms Requested:</label>
                      <asp:RadioButtonList ID="rdoTaxForms" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" class="d-flex flex-wrap gap-3">
                          <asp:ListItem Text="1040" Value="1040"></asp:ListItem>
                          <asp:ListItem Text="1040/W2" Value="1040/W2"></asp:ListItem>
                          <asp:ListItem Text="1120" Value="1120"></asp:ListItem>
                          <asp:ListItem Text="1065" Value="1065"></asp:ListItem>
                          <asp:ListItem Text="W-2" Value="W-2"></asp:ListItem>
                          <asp:ListItem Text="1099" Value="1099"></asp:ListItem>
                      </asp:RadioButtonList>
                </div>

                <!-- Row 4: File Upload -->
                <div class="mb-4 pb-3 border-bottom border-dark bg-primary bg-opacity-10 p-3 rounded">
                    <label class="form-label fw-semibold small mb-2">Upload Form 8821</label>
                    <asp:FileUpload ID="fuform8821" class="form-control"  runat="server" required />
                </div>

                <!-- Submit Button -->
                <div class="text-center bg-light py-3 rounded mt-3">
                    <asp:Button ID="btnSubmit" runat="server" Text="Upload and Submit Order" class="btn btn-primary px-5 fw-bold"/>
                </div>

            </div>
        </div>
    </div>
    </form>
</body>
</html>
