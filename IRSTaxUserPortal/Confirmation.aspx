<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.Master"
    CodeBehind="Confirmation.aspx.vb" Inherits="IRSTaxUserPortal.Confirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Confirmation</title>
    <!-- Page-specific CSS/JS -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-center align-items-center vh-100">
        <div class="text-center bg-white shadow rounded p-5" style="max-width: 600px; width: 100%;">

            <!-- Icon and Heading -->
            <div class="mb-4">
                
                <asp:Label ID="lblFormHeading" runat="server" CssClass="mt-3 fw-bold text-dark h2"></asp:Label>
                <p class="text-muted"><img src="/img/Pdf_4506-C.png" alt="Order 4506-C"
     class="img-fluid" runat="server" id="img4506" visible="false"
     style="width:302px !important; height:126px !important; max-width:none !important;" /></p>
	 
	 <img src="/img/Pdf8821.png" alt="PDF Icon" runat="server" id="img8821" visible="false"
     class="img-fluid"
     style="width:295px !important; height:107px !important; max-width:none !important;" />
            </div><p>
			<img src="/ssv.gif" alt="PDF Icon"
     class="img-fluid" runat="server" id="imgSSV" visible="false"
     style="width:336px !important; height:106px !important; max-width:none !important;" />

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
               <a href="Welcome.aspx"
   class="btn btn-info btn-lg rounded-pill fw-semibold"
   style="
     --bs-btn-bg:#00498C;                 
     --bs-btn-border-color:#00498C;
     --bs-btn-color:#ffffff;              
     --bs-btn-hover-bg:#00D936;           /* pale blue */
     --bs-btn-hover-border-color:#ADD8E6;
     --bs-btn-hover-color:#003366;        /* darker text on pale blue */
   ">
  <i class="fas fa-user-circle me-2"></i> Go back to your online account
</a>
                </a>
            </div>

        </div>
    </div>
</asp:Content>