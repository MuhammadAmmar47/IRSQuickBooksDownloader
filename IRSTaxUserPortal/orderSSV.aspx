<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="orderSSV.aspx.vb" Inherits="IRSTaxUserPortal.orderSSV" Title="Order SSV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Order SSV</title>
    <link rel="stylesheet" 
      href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />

    <!-- Optional: Add page-specific CSS or JS here -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.min.js"></script>
        <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>


    <script>
        $(function () {
            $('#<%= txtSocialSecurityNumber.ClientID %>').mask('000-00-0000');
            $('#<%= txtDob.ClientID %>').mask('00/00/0000');
            $("#<%= txtDob.ClientID %>").datepicker({
                dateFormat: "mm/dd/yy"   // change format as needed
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        <div class="d-flex justify-content-center align-items-center">
            <div class="w-100 px-3">
                <div class="card shadow">
<style>
.card-header {
  background-color: white !important;
}
</style><style>
  
  #ssv-card .card-header { padding: .5rem 2rem !important; }
</style>

<div id="ssv-card" class="card">
                    <!-- Header -->
                    <div class="card-header bg-primary bg-opacity-10 p-0 border-0" style="line-height:0;">
  <img
    src="/ssv.gif"
    alt="SSV Icon"
    class="d-block"
    style="width:336px !important; height:106px !important; max-width:none !important; border:0 !important; outline:0 !important; margin:0 !important;"/>
</div>
                        <%--<i class="fa-solid fa-file-invoice-dollar fa-3x text-primary"></i>
                        <div>
                            <h4 class="mb-1 fw-bold">Form SSV</h4>
                            <small class="text-primary fs-6 fw-semibold">Upload PDF for IRS Transcripts</small>
                        </div>--%>
                    </div>

                    <!-- Body -->
                    <div class="card-body p-3">
                        <asp:Label ID="lblMessage" runat="server"
                            CssClass="text-danger fw-bold d-block mb-3"></asp:Label>
                        <!-- Row 1: Name + SSN -->
                        <div class="row mb-3 pb-2 border-bottom border-dark bg-primary bg-opacity-10 rounded p-3">
                            <div class="col-md-6 mb-2">
                                <label class="form-label fw-semibold small mb-1">Taxpayer Full Name:</label>
                                <asp:TextBox ID="txtTaxPayerName" class="form-control form-control-sm" runat="server" MaxLength="50"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label fw-semibold small mb-1">Social Security Number:</label>
                                <asp:TextBox ID="txtSocialSecurityNumber" class="form-control form-control-sm" runat="server" MaxLength="50" placeholder="123-45-6789"></asp:TextBox>
                                <small class="text-muted">Example Format: 123-45-6789</small>
                            </div>
                        </div>

                        <!-- Row 2: DOB + Gender -->
                        <div class="row mb-3 pb-2 border-bottom border-dark p-3">
                            <div class="col-md-6 mb-2">
                                <label class="form-label fw-semibold small mb-1">Date of Birth:</label>
                                <asp:TextBox ID="txtDob" class="form-control form-control-sm" runat="server" MaxLength="10" placeholder="04/15/1941"></asp:TextBox>
                                <small class="text-muted">Example Format: 04/15/1941</small>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label fw-semibold small mb-2 d-block">Gender:</label>
                                <div class="form-check form-check-inline">
                                    <input type="radio" name="gender" class="form-check-input" id="rbMale" value="Male" runat="server" checked="True" />
                                    <label class="form-check-label small" for="rbMale">Male</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <input type="radio" name="gender" class="form-check-input" id="rbFemale" value="Female" runat="server" />
                                    <label class="form-check-label small" for="rbFemale">Female</label>
                                </div>
                            </div>
                        </div>

                        <!-- Row 3: Loan Number -->
                        <div class="mb-3 pb-2 border-bottom border-dark bg-primary bg-opacity-10 rounded p-3">
                            <label class="form-label fw-semibold small mb-1">Loan Number:</label>
                            <asp:TextBox type="text" ID="txtLoanNumber" class="form-control form-control-sm" runat="server" MaxLength="50"></asp:TextBox>
                        </div>

                        <!-- Row 4: File Upload -->
                        <div class="mb-3 pb-2 border-bottom border-dark p-3">
                            <label class="form-label fw-semibold small mb-2">Upload Form SSA-89:</label>
                            <asp:FileUpload ID="ssa89File" class="form-control form-control-sm" runat="server" />
                        </div>

                        <!-- Submit -->
                        <div class="text-center bg-primary bg-opacity-10 py-3 rounded mt-3">
                            <asp:Button ID="btnSubmit" runat="server" Text="Upload and Submit Order" class="btn btn-primary px-5 fw-bold" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
