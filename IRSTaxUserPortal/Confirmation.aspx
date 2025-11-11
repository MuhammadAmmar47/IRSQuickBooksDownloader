<%@ Page Title="Confirmation"
    Language="vb"
    AutoEventWireup="false"
    MasterPageFile="~/Main.Master"
    CodeBehind="Confirmation.aspx.vb"
    Inherits="IRSTaxUserPortal.Confirmation" %>

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
    <div id="confirm-shell">
        <div class="container">
            <div class="text-center bg-white shadow rounded p-5 mx-auto" style="max-width:600px;">

                <!-- Icon and Heading -->
                <div class="mb-4">
                    <asp:Label ID="lblFormHeading" runat="server" CssClass="mt-3 fw-bold text-dark h2"></asp:Label>

                    <p class="text-muted">
                        <img src="/img/Pdf_4506-C.png" alt="Order 4506-C"
                             class="img-fluid" runat="server" id="img4506" visible="false"
                             style="width:302px !important; height:126px !important; max-width:none !important;" />
                    </p>

                    <img src="/img/Pdf8821.png" alt="PDF Icon" runat="server" id="img8821" visible="false"
                         class="img-fluid"
                         style="width:295px !important; height:107px !important; max-width:none !important;" />
                </div>

                <p>
                    <img src="/ssv.gif" alt="SSV Icon"
                         class="img-fluid" runat="server" id="imgSSV" visible="false"
                         style="width:336px !important; height:106px !important; max-width:none !important;" />
                </p>

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
                </div>

            </div>
        </div>
    </div>

    <script>
        (function () {
            // Helper: pick the first existing element from these common header selectors
            function pick(selArr){
                for (const s of selArr){
                    const el = document.querySelector(s);
                    if (el) return el;
                }
                return null;
            }

            const header = pick(['header', '.navbar', '#Header', '.site-header', '#MainNav', '.fixed-top', '.sticky-top']);
            const shell  = document.getElementById('confirm-shell');

            function adjustOffset(){
                if (!shell) return;
                // Default (non-fixed header): keep 55px margin-top
                shell.style.marginTop  = '55px';
                shell.style.paddingTop = '';

                if (header) {
                    const pos = getComputedStyle(header).position;
                    const isFixed = (pos === 'fixed' || pos === 'sticky');
                    if (isFixed) {
                        // When header is fixed/sticky, push content down by header height + 25px
                        const h = header.offsetHeight || 0;
                        shell.style.marginTop  = '0px';
                        shell.style.paddingTop = (h + 25) + 'px';
                    }
                }
            }

            // Run early to avoid any visible jump
            if (document.readyState === 'loading') {
                document.addEventListener('DOMContentLoaded', adjustOffset, { once:true });
            } else {
                adjustOffset();
            }

            // Keep it accurate on resize / header size changes
            window.addEventListener('resize', adjustOffset);
            if ('ResizeObserver' in window && header) {
                const ro = new ResizeObserver(adjustOffset);
                ro.observe(header);
            }
        })();
    </script>
</asp:Content>
