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
                        <asp:TextBox ID="txtTaxPayerName" class="form-control" runat="server" required></asp:TextBox>
                    </div>
                    <div class="col-md-4 mb-2">
                        <label class="form-label fw-semibold small">Social Security Number:</label>
                         <asp:TextBox ID="txtSocialSecurityNumber" class="form-control" runat="server" required></asp:TextBox>
                    </div>
                    <div class="col-md-4 mb-2">
                        <label class="form-label fw-semibold small">Loan Number:</label>
                        <asp:TextBox ID="txtLoanNumber" class="form-control" runat="server" required></asp:TextBox>
                    </div>
                </div>

                <!-- Row 2: Tax Years -->
                <div class="mb-4 pb-3 border-bottom border-dark">
                    <label class="form-label fw-semibold small mb-2 d-block">Tax Years Requested:</label>
                    <div class="d-flex flex-wrap gap-3">
                        <div class="form-check">
                            <asp:CheckBox ID="chk2024" runat="server" Text="2024" />
                        </div>
                        <div class="form-check">
                           <asp:CheckBox ID="CheckBox1" runat="server" Text="2023" />
                        </div>
                        <div class="form-check">
                            <asp:CheckBox ID="CheckBox2" runat="server" Text="2022" />
                        </div>
                        <div class="form-check">
                            <asp:CheckBox ID="CheckBox3" runat="server" Text="2021" />
                        </div>
                        <div class="form-check">
                            <asp:CheckBox ID="CheckBox4" runat="server" Text="2020" />
                        </div>
                    </div>
                </div>

                <!-- Row 3: Forms -->
                <div class="mb-4 pb-3 border-bottom border-dark">
                    <label class="form-label fw-semibold small mb-2 d-block">Forms Requested:</label>
                    <div class="d-flex flex-wrap gap-3">
                        <div class="form-check">
                            <asp:CheckBox ID="chk1040Rec" runat="server" Text="1040 + Record of Account" />
                        </div>
                        <div class="form-check">
                            <asp:CheckBox ID="CheckBox5" runat="server" Text="1040" />
                        </div>
                        <div class="form-check">
                            <asp:CheckBox ID="CheckBox6" runat="server" Text="W2" />
                        </div>
                        <div class="form-check">
                            <asp:CheckBox ID="CheckBox7" runat="server" Text="1099" />
                        </div>
                        <div class="form-check">
                            <asp:CheckBox ID="CheckBox8" runat="server" Text="Record of Account" />
                        </div>
                        <div class="form-check">
                            <asp:CheckBox ID="CheckBox11" runat="server" Text="Account Transcript" />
                        </div>
                        <div class="form-check">
                            <asp:CheckBox ID="CheckBox9" runat="server" Text="Form 1120" />
                        </div>
                        <div class="form-check">
                            <asp:CheckBox ID="CheckBox10" runat="server" Text="Form 1065" />
                        </div>
                    </div>
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
