<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.Master" CodeBehind="order_8821.aspx.vb" Inherits="IRSTaxUserPortal.order_8821" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Order 8821</title>
    <!-- Optional: Add page-specific CSS or JS here -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Centering Wrapper -->
    <div class="container py-4">
        <div class="d-flex justify-content-center align-items-center">
            <div class="w-100 px-3">
                <div class="card shadow">

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
                        <asp:Label ID="lblMessage" runat="server"
                            CssClass="text-danger fw-bold d-block mb-3"></asp:Label>
                        <!-- Row 1: Name, SSN, Loan -->
                        <div class="row mb-4 pb-3 border-bottom border-dark">
                            <div class="col-md-4 mb-2">
                                <label class="form-label fw-semibold small">Taxpayer Name:</label>
                                <asp:TextBox ID="txtTaxPayerName" class="form-control" runat="server" MaxLength="50"></asp:TextBox>
                            </div>
                            <div class="col-md-4 mb-2">
                                <label class="form-label fw-semibold small">Social Security Number:</label>
                                <asp:TextBox ID="txtSocialSecurityNumber" class="form-control" runat="server" MaxLength="50"></asp:TextBox>
                            </div>
                            <div class="col-md-4 mb-2">
                                <label class="form-label fw-semibold small">Loan Number:</label>
                                <asp:TextBox ID="txtLoanNumber" class="form-control" runat="server" MaxLength="50"></asp:TextBox>
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
                            <asp:CheckBoxList ID="chkTaxForms" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" class="d-flex flex-wrap gap-3">
                                <asp:ListItem Text="1040 + Record of Account" Value="1040R"></asp:ListItem>
                                <asp:ListItem Text="1040" Value="1040"></asp:ListItem>
                                <asp:ListItem Text="W2" Value="W-2"></asp:ListItem>
                                <asp:ListItem Text="1099" Value="1099"></asp:ListItem>
                                <asp:ListItem Text="Record of Account" Value="ROA"></asp:ListItem>
                                <asp:ListItem Text="Account Transcript" Value="AT"></asp:ListItem>
                                <asp:ListItem Text="1040/W2" Value="1040/W2"></asp:ListItem>
                                <asp:ListItem Text="1120" Value="1120"></asp:ListItem>
                                <asp:ListItem Text="1065" Value="1065"></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>

                        <!-- Row 4: File Upload -->
                        <div class="mb-4 pb-3 border-bottom border-dark bg-primary bg-opacity-10 p-3 rounded">
                            <label class="form-label fw-semibold small mb-2">Upload Form 8821</label>
                            <asp:FileUpload ID="fuform8821" class="form-control" runat="server" />
                        </div>

                        <!-- Submit Button -->
                        <div class="text-center bg-light py-3 rounded mt-3">
                            <asp:Button ID="btnSubmit" runat="server" Text="Upload and Submit Order" class="btn btn-primary px-5 fw-bold" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
