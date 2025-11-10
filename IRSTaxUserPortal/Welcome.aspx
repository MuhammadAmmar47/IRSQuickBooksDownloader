<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Welcome.aspx.vb" Inherits="IRSTaxUserPortal.Default1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Welcome</title>
    <!-- Bootstrap Icons -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />
    <!-- Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
<style>
#<%= GridView1.ClientID %> tbody tr:nth-of-type(odd) { background-color: #FBFBFB !important; }
#<%= GridView1.ClientID %> tbody tr:nth-of-type(even) { background-color: #EEF2F8 !important; }
/* Pending highlight — wins over striping */
.table.table-striped.table-bordered > tbody > tr.highlightRow > th,
.table.table-striped.table-bordered > tbody > tr.highlightRow > td {
  background-color: #FFF0E1 !important;
}
</style>
    <style>
        .text-custom-blue { color: #003366 !important; }

        .custom-shadow-right {
            border: 2px solid #b3b6af !important;
            box-shadow: 6px 0 12px rgba(0, 0, 0, 0.3) !important;
        }

        .btn-custom-blue {
            --bs-btn-color: #003366;
            --bs-btn-border-color: #003366;
            --bs-btn-hover-color: #fff;
            --bs-btn-hover-bg: #003366;
            --bs-btn-hover-border-color: #003366;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 1rem;
            font-weight: 600;
            border-radius: 0;
        }

        .card h6 {
            font-size: 1.2rem;
            font-weight: 700;
            color: #d80000;
        }

        .card p, .card ul {
            font-size: 0.875rem !important;
            line-height: 1.5;
            font-weight: 500;
            font-family: Arial, Helvetica, sans-serif;
            color: #003366;
        }

        .highlightRow td { background-color: #ADD8E6 !important; }
		
    </style>

    <script type="text/javascript">
        // Set session timeout duration (in minutes)
        const sessionTimeoutMinutes = <%= Session.Timeout %>; // dynamically uses web.config timeout
        const timeoutMilliseconds = sessionTimeoutMinutes * 60 * 1000;

        // Optional: Add a few seconds buffer before redirect
        const bufferTime = 5000; // 5 seconds
        const totalTimeout = timeoutMilliseconds + bufferTime;

        let sessionTimer;

        function startSessionTimer() {
            // Clear any previous timer
            clearTimeout(sessionTimer);

            // Set new timeout
            sessionTimer = setTimeout(function () {
                // Redirect to login page with param
                window.location.href = '/login.aspx?expired=1';
            }, totalTimeout);
        }

        // Restart timer on user activity
        ['click', 'mousemove', 'keydown', 'scroll', 'touchstart'].forEach(function (evt) {
            document.addEventListener(evt, startSessionTimer, false);
        });

        // Start initial timer when page loads
        window.onload = startSessionTimer;
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .pagination-container { text-align: right; padding: 10px; }
        .pagination-container table { margin: 0; }
        .pagination-container a,
        .pagination-container span {
            display: inline-block;
            margin: 0 2px;
            padding: 5px 10px;
            border: 1px solid #dee2e6;
            border-radius: 4px;
            color: #007bff;
            text-decoration: none;
        }
        .pagination-container a:hover { background-color: #007bff; color: #fff; }
        .pagination-container span { background-color: #007bff; color: #fff; cursor: default; }
    </style>

    <script>
      // Swap icon after a delay, set new size, then trigger the server command
      function swapAfterDelay(el, nextSrc, nextW, nextH, delayMs) {
        if (el.dataset.swapping === '1') return false; // prevent double-clicks
        el.dataset.swapping = '1';

        el.style.opacity = '0.7';
        el.style.pointerEvents = 'none';

        var preload = new Image();
        preload.src = nextSrc;

        setTimeout(function () {
          el.src = nextSrc;                      // swap image
          if (nextW) el.width  = parseInt(nextW, 10);
          if (nextH) el.height = parseInt(nextH, 10);

          el.style.opacity = '';
          el.style.pointerEvents = '';

          if (typeof __doPostBack === 'function') {
            __doPostBack(el.name, '');
          } else if (el.form) {
            el.form.submit();
          }
        }, delayMs || 3000);

        return false; // cancel immediate postback; we’ll post back after delay
      }
    </script>

    <!-- Hero -->
    <section class="hero-section border-top border-warning border-5">
        <div id="heroCarousel" class="carousel slide carousel-fade h-100 h-md-80" data-bs-ride="carousel" data-bs-interval="5000">
            <div class="carousel-inner h-100">
                <div class="carousel-item active">
                    <img src="/new/images/bannerflip1.jpg" class="d-block w-100 img-fluid object-fit-cover" alt="Slide 1">
                </div>
                <div class="carousel-item">
                    <img src="/new/images/bannerflip2.jpg" class="d-block w-100 img-fluid object-fit-cover" alt="Slide 2">
                </div>
                <div class="carousel-item">
                    <img src="/new/images/bannerflip3.jpg" class="d-block w-100 img-fluid object-fit-cover" alt="Slide 3">
                </div>
            </div>
            <button class="carousel-control-prev" type="button" data-bs-target="#heroCarousel" data-bs-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span><span class="visually-hidden">Previous</span>
            </button>
            <button class="carousel-control-next" type="button" data-bs-target="#heroCarousel" data-bs-slide="next">
                <span class="carousel-control-next-icon" aria-hidden="true"></span><span class="visually-hidden">Next</span>
            </button>
			<!-- Put this script after the carousel HTML -->
<script>
  (function () {
    var el = document.getElementById('heroCarousel');
    if (!el) return;

    var items = el.querySelectorAll('.carousel-item');
    if (items.length < 2) return;

    // pick a random starting slide
    var start = Math.floor(Math.random() * items.length);

    // clear any existing 'active' and set the random one
    el.querySelectorAll('.carousel-item.active').forEach(function (n) { n.classList.remove('active'); });
    items[start].classList.add('active');

    // if you have indicators, keep them in sync
    var inds = el.querySelectorAll('.carousel-indicators [data-bs-target="#heroCarousel"]');
    if (inds.length === items.length) {
      inds.forEach(function (n) { n.classList.remove('active'); n.removeAttribute('aria-current'); });
      inds[start].classList.add('active');
      inds[start].setAttribute('aria-current', 'true');
    }

    // ensure Bootstrap's internal index matches our starting slide
    if (window.bootstrap && bootstrap.Carousel) {
      var instance = bootstrap.Carousel.getOrCreateInstance(el, {
        interval: 5000,
        ride: 'carousel'
      });
      instance.to(start);
    }
  })();
</script>
			
			
        </div><div class="container py-5"><center>
   <map name="FPMap0">
<area href="/order_4506.aspx" shape="rect" coords="68, 127, 240, 164">
<area href="/orderSSV.aspx" shape="rect" coords="679, 127, 963, 164">
<area href="/order_8821.aspx" shape="rect" coords="411, 127, 588, 163"></map><img border="0" src="/newsite//Newaccountmenu.jpg" usemap="#FPMap0" width="986" height="194">
    </section>
    
<asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered"></asp:GridView>



<style>
/* 0) Set Bootstrap vars for THIS table only */
.table.table-striped.table-bordered {
  /* Make Bootstrap's own stripe color pure white */
  --bs-table-striped-bg: #FFFFFF !important;
  /* Kill the gray accent that tints cells */
  --bs-table-accent-bg: transparent !important;
  /* Optional: nice hover if you also use .table-hover */
  --bs-table-hover-bg: #E6EDF7 !important;
}

/* 1) Remove Bootstrap's cell tint overlay (box-shadow trick) */
.table.table-striped.table-bordered > :not(caption) > * > * {
  box-shadow: none !important;
  background-image: none !important; /* extra belt & suspenders */
}

/* 2) Force exact alternation on the cells */
.table.table-striped.table-bordered > tbody > tr:nth-of-type(odd) > * {
  background-color: #FFFFFF !important;   /* true white */
}
.table.table-striped.table-bordered > tbody > tr:nth-of-type(even) > * {
  background-color: #EEF2F8 !important;   /* your blue-tinted gray */
}

/* 3) (Optional) Keep hover within your palette if .table-hover is present */
.table.table-striped.table-bordered.table-hover > tbody > tr:hover > * {
  background-color: #E6EDF7 !important;
}
</style>
<style>
/* Reusable table skin */
.my-grid {
  /* neutralize Bootstrap tint vars just in case */
  --bs-table-accent-bg: transparent;
  --bs-table-striped-bg: transparent;
  --bs-table-hover-bg: #E6EDF7;
}

/* Remove Bootstrap’s inset tint/shadow on cells */
.my-grid > :not(caption) > * > * {
  box-shadow: none !important;
  background-image: none !important;
  border-color: #DEE2E6 !important; /* cleaner borders */
}

/* Zebra striping (your palette) */
.my-grid > tbody > tr:nth-child(odd) > *  { background-color: #FFFFFF !important; }
.my-grid > tbody > tr:nth-child(even) > * { background-color: #EEF2F8 !important; }

/* Hover color (works because we included .table-hover) */
.my-grid.table-hover > tbody > tr:hover > * { background-color: #E6EDF7 !important; }

/* Header styling */
.my-grid thead th {
  background-color: #FBFBFB !important;
  color: #000 !important;
  border-bottom: 1px solid #DEE2E6 !important;
}

/* Optional: compact rows */
.my-grid td, .my-grid th { padding: 0.55rem 0.75rem !important; }
</style>
   

    <!-- Grids -->
    <section>
        <div class="container py-5">
            <div class="row g-4">
                <div class="d-grid"> 
                    <asp:Label ID="lblGreeting" runat="server" Text="Welcome " Style="font-size:22px; color:#2c3e50; font-weight:bold; font-family:'Segoe UI', sans-serif;"> </asp:Label>
                    <br />
                    <asp:Panel ID="pnlGrid" runat="server" Visible="false">

                        <img runat="server" src="/img/Grid4506.gif" alt="Form 4506 Orders" style="display: block; height: auto;" />

                        <!-- GRID 1 -->
                        <asp:GridView ID="Grid1" runat="server"
                            CssClass="table table-striped table-bordered"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            PageSize="30"
                            OnPageIndexChanging="Grid1_PageIndexChanging"
                            PagerStyle-CssClass="pagination-container"
                            PagerSettings-Mode="NumericFirstLast"
                            PagerSettings-FirstPageText="« First"
                            PagerSettings-LastPageText="Last »"
                            PagerSettings-NextPageText="Next ›"
                            PagerSettings-PreviousPageText="‹ Prev"
                            PagerSettings-Position="Top">
                            <Columns>
                                <asp:BoundField DataField="OrderNumber" HeaderText="Order Number" />

                                <asp:BoundField DataField="TaxPayer" HeaderText="Tax Payer" />
                                <asp:TemplateField HeaderText="Form Type">
                                    <ItemTemplate><%# GetFormTypeName(Convert.ToInt32(Eval("FormType"))) %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RequestedTaxYears" HeaderText="Requested Tax Years" />
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate><%# GetStatusText(Eval("Status")) %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delivery Date">
                                    <ItemTemplate><asp:Label ID="lblDeliveryDate" runat="server" /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DeliveryDate" HeaderText="Delivery Date" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" ApplyFormatInEditMode="True" Visible="false" />
                                <asp:BoundField DataField="OrderType" HeaderText="Order Type" Visible="false" />
                                <asp:TemplateField HeaderText="View File">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnView" runat="server"
                                            CommandName="DownloadFile"
                                            CommandArgument='<%# Eval("File Name") %>'
                                            ImageUrl="/img/viewfile.gif"
                                            Width="61px" Height="66px"
                                            />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Download File">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDownload" runat="server"
                                            CommandName="DownloadFile"
                                            CommandArgument='<%# Eval("File Name") %>'
                                            ImageUrl="/img/downloadfile.gif"
                                            Width="70px" Height="66px"
                                            OnClientClick="return swapAfterDelay(this, '/img/filedownloaded.gif', 82,71, 3000);"
                                            Visible='<%# Eval("File Name") <> "" AndAlso Eval("Status").ToString().Trim() <> "p" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <asp:Label ID="lblGrid1Message" runat="server" ForeColor="Red" Visible="False" />
                        <hr />

                        <img runat="server" src="/img/Grid8821.gif" alt="Form 8821 Orders" style="display: block; height: auto;" />

                        <!-- GRID 2 -->
                        <asp:GridView ID="Grid2" runat="server"
                            CssClass="table table-striped table-bordered"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            PageSize="30"
                            OnPageIndexChanging="Grid2_PageIndexChanging"
                            PagerStyle-CssClass="pagination-container"
                            PagerSettings-Mode="NumericFirstLast"
                            PagerSettings-FirstPageText="« First"
                            PagerSettings-LastPageText="Last »"
                            PagerSettings-NextPageText="Next ›"
                            PagerSettings-PreviousPageText="‹ Prev"
                            PagerSettings-Position="Top">
                            <Columns>
                                <asp:BoundField DataField="OrderNumber" HeaderText="Order Number" />
                                <asp:BoundField DataField="OrderDate" HeaderText="Order Date" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" ApplyFormatInEditMode="True" />
                                <asp:BoundField DataField="TaxPayer" HeaderText="Tax Payer" />
                                <asp:TemplateField HeaderText="Form Type">
                                    <ItemTemplate><%# GetFormTypeName(Convert.ToInt32(Eval("FormType"))) %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RequestedTaxYears" HeaderText="Requested Tax Years" />
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate><%# GetStatusText(Eval("Status")) %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delivery Date">
                                    <ItemTemplate><asp:Label ID="lblDeliveryDate" runat="server" /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DeliveryDate" HeaderText="Delivery Date" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" ApplyFormatInEditMode="True" Visible="false" />
                                <asp:BoundField DataField="OrderType" HeaderText="Order Type" Visible="false" />
                                <asp:TemplateField HeaderText="View File">
                                  <ItemTemplate>
                                        <asp:ImageButton ID="btnView" runat="server"
                                            CommandName="DownloadFile"
                                            CommandArgument='<%# Eval("File Name") %>'
                                            ImageUrl="/img/viewfile.gif"
                                            Width="61px" Height="66px"
                                            />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Download File">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDownload" runat="server"
                                            CommandName="DownloadFile"
                                            CommandArgument='<%# Eval("File Name") %>'
                                            ImageUrl="/img/downloadfile.gif"
                                            Width="70px" Height="66px"
                                            OnClientClick="return swapAfterDelay(this, '/img/filedownloaded.gif', 82,71, 3000);"
                                            Visible='<%# Eval("File Name") <> "" AndAlso Eval("Status").ToString().Trim() <> "p" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <asp:Label ID="lblGrid2Message" runat="server" ForeColor="Red" Visible="False" />
                        <hr />

                        <img runat="server" src="/img/GridSSV.gif" alt="Form SSV Orders" style="display: block; height: auto;" />

                        <!-- GRID 3 -->
                        <asp:GridView ID="Grid3" runat="server"
                            CssClass="table table-striped table-bordered"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            PageSize="30"
                            OnPageIndexChanging="Grid3_PageIndexChanging"
                            PagerStyle-CssClass="pagination-container"
                            PagerSettings-Mode="NumericFirstLast"
                            PagerSettings-FirstPageText="« First"
                            PagerSettings-LastPageText="Last »"
                            PagerSettings-NextPageText="Next ›"
                            PagerSettings-PreviousPageText="‹ Prev"
                            PagerSettings-Position="Top">
                            <Columns>
                                <asp:BoundField DataField="OrderNumber" HeaderText="Order Number" />
                                <asp:BoundField DataField="OrderDate" HeaderText="Order Date" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" ApplyFormatInEditMode="True" />
                                <asp:BoundField DataField="TaxPayer" HeaderText="Tax Payer" />
                                <asp:TemplateField HeaderText="Form Type">
                                    <ItemTemplate><%# GetFormTypeName(Convert.ToInt32(Eval("FormType"))) %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RequestedTaxYears" HeaderText="Requested Tax Years" Visible="false" />
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate><%# GetStatusText(Eval("Status")) %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delivery Date">
                                    <ItemTemplate><asp:Label ID="lblDeliveryDate" runat="server" /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DeliveryDate" HeaderText="Delivery Date" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" ApplyFormatInEditMode="True" Visible="false" />
                                <asp:BoundField DataField="OrderType" HeaderText="Order Type" Visible="false" />
                                <asp:TemplateField HeaderText="View File">
                                   <ItemTemplate>
                                        <asp:ImageButton ID="btnView" runat="server"
                                            CommandName="DownloadFile"
                                            CommandArgument='<%# Eval("File Name") %>'
                                            ImageUrl="/img/viewfile.gif"
                                            Width="61px" Height="66px"
                                            />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Download File">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDownload" runat="server"
                                            CommandName="DownloadFile"
                                            CommandArgument='<%# Eval("File Name") %>'
                                            ImageUrl="/img/downloadfile.gif"
                                            Width="70px" Height="66px"
                                            OnClientClick="return swapAfterDelay(this, '/img/filedownloaded.gif', 82,71, 3000);"
                                            Visible='<%# Eval("File Name") <> "" AndAlso Eval("Status").ToString().Trim() <> "p" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <asp:Label ID="lblGrid3Message" runat="server" ForeColor="Red" Visible="False" />

                    </asp:Panel>
                </div>
            </div>
        </div>
    </section>

    <script type="text/javascript">
        function downloadFile(orderID) {
            var url = 'DownloadDocument.aspx?OrderID=' + orderID;
            var opts = 'width=800,height=600,toolbar=0,menubar=0,location=1,status=1,scrollbars=1,resizable=1,left=0,top=0';
            var newWindow = window.open(url, 'name', opts);
            // newWindow.print();
        }
    </script>
</asp:Content>
