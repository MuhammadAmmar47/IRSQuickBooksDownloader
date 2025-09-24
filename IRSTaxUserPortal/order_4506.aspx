<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="order_4506.aspx.vb" Inherits="IRSTaxUserPortal.order_4506" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>order_4506</title>
    <!-- Bootstrap Icons -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />
    <!-- Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container py-4">
        <div class="d-flex justify-content-center align-items-center min-vh-100">
            <form class="w-100 px-3" style="max-width: 900px;" enctype="multipart/form-data">
                <div class="card shadow">
                    <div class="card-header d-flex align-items-center gap-3 bg-primary bg-opacity-10 flex-wrap">
                        <i class="fa-solid fa-file-invoice-dollar fa-3x text-primary"></i>
                        <div>
                            <h4 class="mb-1 fw-bold">Form 4506-C</h4>
                            <small class="text-primary fs-5 fw-semibold">Upload PDF for IRS Transcripts</small>
                        </div>
                    </div>

                    <div class="card-body">
                        <!-- Row 1 -->
                        <div class="row mb-4">
                            <div class="col-12 col-md-4 mb-3 mb-md-0">
                                <label class="form-label fw-semibold">Taxpayer Name</label>
                                <asp:TextBox ID="txtTaxPayerName" class="form-control" runat="server" required  MaxLength="50" ></asp:TextBox>
                            </div>
                            <div class="col-12 col-md-4 mb-3 mb-md-0">
                                <label class="form-label fw-semibold">Social Security Number</label>
                                <asp:TextBox ID="txtSocialSecurityNumber" class="form-control" runat="server" required  MaxLength="50" ></asp:TextBox>
                            </div>
                            <div class="col-12 col-md-4">
                                <label class="form-label fw-semibold">Loan Number</label>
                                <asp:TextBox ID="txtLoanNumber" class="form-control" runat="server" required  MaxLength="50" ></asp:TextBox>
                            </div>
                        </div>

                        <!-- Row 2: Tax Years -->
                        <div class="mb-4">
                            <label class="fw-semibold d-block mb-2">Tax Years Requested:</label>
                            <div class="d-flex flex-wrap gap-3">
                                <asp:CheckBox ID="chk2024" runat="server" Text="2024" />
                                <asp:CheckBox ID="chk2023" runat="server" Text="2023" />
                                <asp:CheckBox ID="chk2022" runat="server" Text="2022" />
                                <asp:CheckBox ID="chk2021" runat="server" Text="2021" />
                                <asp:CheckBox ID="chk2020" runat="server" Text="2020" />
                            </div>
                        </div>

                        <!-- Row 3: Forms -->
                        <div class="mb-4">
                            <label class="fw-semibold d-block mb-2">Forms Requested:</label>
                            <div class="d-flex flex-wrap gap-3">
                                <asp:CheckBox ID="chk1040Rec" runat="server" Text="1040 + Record of Account" />
                                <asp:CheckBox ID="chk1040" runat="server" Text="1040" />
                                <asp:CheckBox ID="chkW2" runat="server" Text="W2" />
                                <asp:CheckBox ID="chk1099" runat="server" Text="1099" />
                                <asp:CheckBox ID="chkRecAcc" runat="server" Text="Record of Account" />
                                <asp:CheckBox ID="chk1120" runat="server" Text="Form 1120" />
                                <asp:CheckBox ID="chk1065" runat="server" Text="Form 1065" />
                            </div>
                        </div>

                        <!-- Row 4: File Upload -->
                        <div class="mb-4 p-3 rounded bg-primary bg-opacity-10">
                            <label class="fw-semibold d-block mb-2">Upload Form 4506-C:</label>
                            <asp:FileUpload ID="fuform4506C" class="form-control"  runat="server" required />
                        </div>

                        <!-- Submit -->
                        <div class="text-center bg-light py-3 rounded mt-4">
                            <asp:Button ID="btnSubmit" runat="server" Text="Upload and Submit Order" class="btn btn-primary px-5 fw-bold"/>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
    </form>
</body>
</html>
