<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Welcome.aspx.vb" Inherits="IRSTaxUserPortal.Default1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>welcome</title>
    <!-- Bootstrap Icons -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />
    <!-- Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        .text-custom-blue {
            color: #003366 !important;
        }

        /* Border + right shadow */
        .custom-shadow-right {
            border: 2px solid #b3b6af !important;
            box-shadow: 6px 0 12px rgba(0, 0, 0, 0.3) !important;
        }

        /* Buttons in cards */
        .btn-custom-blue {
            --bs-btn-color: #003366;
            --bs-btn-border-color: #003366;
            --bs-btn-hover-color: #fff;
            --bs-btn-hover-bg: #003366;
            --bs-btn-hover-border-color: #003366;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 1rem;
            font-weight: 600;
            border-radius: 0; /* squared corners */
        }

        /* Card headings */
        .card h6 {
            font-size: 1.2rem;
            font-weight: 700;
            color: #d80000; /* red */
        }

        /* Card body text */
        .card p, .card ul {
            font-size: 0.875rem !important;
            line-height: 1.5;
            font-weight: 500;
            font-family: Arial, Helvetica, sans-serif;
            color: #003366;
        }

        .highlightRow td {
    background-color: #d6e17e !important;
    color: #fff; /* optional - makes text readable */
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .pagination-container {
            text-align: right; /* Align pager to right */
            padding: 10px;
        }

            .pagination-container table {
                margin: 0;
            }

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

                .pagination-container a:hover {
                    background-color: #007bff;
                    color: #fff;
                }

            .pagination-container span {
                background-color: #007bff;
                color: #fff;
                cursor: default;
            }
    </style>
    <!-- Trust Icons Section -->
    <section class="hero-section border-top border-warning border-5">
        <div id="heroCarousel" class="carousel slide carousel-fade h-100 h-md-80" data-bs-ride="carousel" data-bs-interval="3000">
            <div class="carousel-inner h-100">

                <div class="carousel-item active">
                    <img src="https://www.irstaxrecords.com/new/images/bannerflip1.jpg"
                        class="d-block w-100 img-fluid object-fit-cover"
                        alt="Slide 1">
                </div>

                <div class="carousel-item">
                    <img src="https://www.irstaxrecords.com/new/images/bannerflip2.jpg"
                        class="d-block w-100 img-fluid object-fit-cover"
                        alt="Slide 2">
                </div>

                <div class="carousel-item">
                    <img src="https://www.irstaxrecords.com/new/images/bannerflip3.jpg"
                        class="d-block w-100 img-fluid object-fit-cover"
                        alt="Slide 3">
                </div>
            </div>

            <!-- Carousel Controls -->
            <button class="carousel-control-prev" type="button" data-bs-target="#heroCarousel" data-bs-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                <span class="visually-hidden">Previous</span>
            </button>

            <button class="carousel-control-next" type="button" data-bs-target="#heroCarousel" data-bs-slide="next">
                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                <span class="visually-hidden">Next</span>
            </button>
        </div>
    </section>

    <!-- Card Section -->
    <section>
        <div class="container py-5">
            <div class="row g-4">

                <!-- Card 1: Form 4506-C -->
                <div class="col-md-4">
                    <div class="card h-100 shadow rounded border border-3">
                        <div class="card-body d-flex flex-column">
                            <div class="d-flex align-items-center mb-3">
                                <img src="https://img.icons8.com/color/64/000000/pdf.png" alt="PDF Icon" style="width: 64px; height: 64px;">
                                <h5 class="ms-3 mb-0 fs-3 text-primary">Form <strong>4506-C</strong><br>
                                    <span class="text-primary fs-6">UPLOAD PDF<br>
                                        for IRS Transcripts</span>
                                </h5>
                            </div>
                            <a href="order_4506.aspx" class="btn btn-primary w-100 fw-semibold fs-5">Order 4506-C</a>
                        </div>
                    </div>
                </div>

                <!-- Card 2: Form 8821 -->
                <div class="col-md-4">
                    <div class="card h-100 shadow rounded border border-3">
                        <div class="card-body d-flex flex-column">
                            <div class="d-flex align-items-center mb-3">
                                <img src="https://img.icons8.com/color/64/000000/pdf.png" alt="PDF Icon" style="width: 64px; height: 64px;">
                                <h5 class="ms-3 mb-0 fs-3 text-primary">Form <strong>8821</strong><br>
                                    <span class="text-primary fs-6">UPLOAD PDF<br>
                                        for IRS Transcripts</span>
                                </h5>
                            </div>
                            <a href="order_8821.aspx" class="btn btn-dark w-100 fw-semibold fs-5">Order 8821</a>
                        </div>
                    </div>
                </div>

                <!-- Card 3: Social Security Validation -->
                <div class="col-md-4">
                    <div class="card h-100 shadow rounded border border-3">
                        <div class="card-body d-flex flex-column">
                            <div class="d-flex align-items-center mb-3">
                                <img src="https://img.icons8.com/color/64/000000/verified-account.png" alt="Verify Icon" style="width: 64px; height: 64px;">
                                <h5 class="ms-3 mb-0 fs-3 text-primary">ORDER SOCIAL SECURITY<br>
                                    VALIDATIONS ONLINE
                                </h5>
                            </div>
                            <small class="text-muted">via the SOCIAL SECURITY ADMINISTRATION</small>
                            <a href="orderSSV.aspx" class="btn btn-success w-100 mt-auto fw-semibold fs-5">Order Social Security Verification</a>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </section>

    <section>
        <div class="container py-5">
            <div class="row g-4">
                <div class="d-grid">
                    <asp:Panel ID="pnlGrid" runat="server" Visible="false">

                        <img runat="server" src="/img/Grid4506.gif"
                            alt="Form 4506 Orders"
                            style="display: block; height: auto;" />
                        <asp:GridView ID="Grid1" runat="server"
                            CssClass="table table-striped table-bordered"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            PageSize="10"
                            OnPageIndexChanging="Grid1_PageIndexChanging"
                            PagerStyle-CssClass="pagination-container"
                            PagerSettings-Mode="NumericFirstLast"
                            PagerSettings-FirstPageText="« First"
                            PagerSettings-LastPageText="Last »"
                            PagerSettings-NextPageText="Next ›"
                            PagerSettings-PreviousPageText="‹ Prev">
                            <Columns>
                                <asp:BoundField DataField="Order Number" HeaderText="#" />
                                <asp:BoundField DataField="Order Date" HeaderText="Order Date"
                                    DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" ApplyFormatInEditMode="True" />
                                <asp:BoundField DataField="Tax Payer" HeaderText="Tax Payer" />
                                <asp:TemplateField HeaderText="Form Type">
                                    <ItemTemplate>
                                        <%# GetFormTypeName(Convert.ToInt32(Eval("Form Type"))) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Requested Tax Years" HeaderText="Requested Tax Years" />
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <%# GetStatusText(Eval("Status")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Delivery Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeliveryDate" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Delivery Date" HeaderText="Delivery Date"
                                    DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" ApplyFormatInEditMode="True" Visible="false" />
                                <asp:BoundField DataField="OrderType" HeaderText="Order Type" Visible="false" />
                                <asp:TemplateField HeaderText="View File">
                                    <ItemTemplate>

                                        <asp:ImageButton ID="btnView" runat="server"
                                            CommandName="DownloadFile"
                                            CommandArgument='<%# Eval("File Name") %>'
                                            ImageUrl="img/view.gif"
                                            Width="24px"
                                            Visible='<%# Eval("File Name") <> ""%>'
                                            OnClientClick='<%# Eval("File Name") %>' />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Download File">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDownload" runat="server"
                                            CommandName="DownloadFile"
                                            CommandArgument='<%# Eval("File Name") %>'
                                            ImageUrl="img/download.gif"
                                            Width="24px"
                                            Visible='<%# Eval("File Name") <> ""%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblGrid1Message" runat="server" ForeColor="Red" Visible="False" />
                        <hr />

                        <img runat="server" src="/img/Grid8821.gif"
                            alt="Form 8821 Orders"
                            style="display: block; height: auto;" />
                        <asp:GridView ID="Grid2" runat="server"
                            CssClass="table table-striped table-bordered"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            PageSize="10"
                            OnPageIndexChanging="Grid2_PageIndexChanging"
                            PagerStyle-CssClass="pagination-container"
                            PagerSettings-Mode="NumericFirstLast"
                            PagerSettings-FirstPageText="« First"
                            PagerSettings-LastPageText="Last »"
                            PagerSettings-NextPageText="Next ›"
                            PagerSettings-PreviousPageText="‹ Prev">
                            <Columns>
                                <asp:BoundField DataField="Order Number" HeaderText="#" />
                                <asp:BoundField DataField="Order Date" HeaderText="Order Date"
                                    DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" ApplyFormatInEditMode="True" />
                                <asp:BoundField DataField="Tax Payer" HeaderText="Tax Payer" />
                                <asp:TemplateField HeaderText="Form Type">
                                    <ItemTemplate>
                                        <%# GetFormTypeName(Convert.ToInt32(Eval("Form Type"))) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Requested Tax Years" HeaderText="Requested Tax Years" />
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <%# GetStatusText(Eval("Status")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delivery Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeliveryDate" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Delivery Date" HeaderText="Delivery Date"
                                    DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" ApplyFormatInEditMode="True" Visible="false" />
                                <asp:BoundField DataField="OrderType" HeaderText="Order Type" Visible="false" />
                                <asp:TemplateField HeaderText="View File">
                                    <ItemTemplate>

                                        <asp:ImageButton ID="btnView" runat="server"
                                            CommandName="DownloadFile"
                                            CommandArgument='<%# Eval("File Name") %>'
                                            ImageUrl="img/view.gif"
                                            Width="24px"
                                            Visible='<%# Eval("File Name") <> ""%>'
                                            OnClientClick='<%# Eval("File Name") %>' />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Download File">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDownload" runat="server"
                                            CommandName="DownloadFile"
                                            CommandArgument='<%# Eval("File Name") %>'
                                            ImageUrl="img/download.gif"
                                            Width="24px"
                                            Visible='<%# Eval("File Name") <> ""%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblGrid2Message" runat="server" ForeColor="Red" Visible="False" />
                        <hr />

                        <img runat="server" src="/img/GridSSV.gif"
                            alt="Form SSV Orders"
                            style="display: block; height: auto;" />
                        <asp:GridView ID="Grid3" runat="server"
                            CssClass="table table-striped table-bordered"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            PageSize="10"
                            OnPageIndexChanging="Grid3_PageIndexChanging"
                            PagerStyle-CssClass="pagination-container"
                            PagerSettings-Mode="NumericFirstLast"
                            PagerSettings-FirstPageText="« First"
                            PagerSettings-LastPageText="Last »"
                            PagerSettings-NextPageText="Next ›"
                            PagerSettings-PreviousPageText="‹ Prev">
                            <Columns>
                                <asp:BoundField DataField="Order Number" HeaderText="#" />
                                <asp:BoundField DataField="Order Date" HeaderText="Order Date"
                                    DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" ApplyFormatInEditMode="True" />
                                <asp:BoundField DataField="Tax Payer" HeaderText="Tax Payer" />
                                <asp:TemplateField HeaderText="Form Type">
                                    <ItemTemplate>
                                        <%# GetFormTypeName(Convert.ToInt32(Eval("Form Type"))) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Requested Tax Years" HeaderText="Requested Tax Years" Visible="false" />
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <%# GetStatusText(Eval("Status")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delivery Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeliveryDate" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Delivery Date" HeaderText="Delivery Date"
                                    DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" ApplyFormatInEditMode="True" Visible="false" />
                                <asp:BoundField DataField="OrderType" HeaderText="Order Type" Visible="false" />
                                <asp:TemplateField HeaderText="View File">
                                    <ItemTemplate>

                                        <asp:ImageButton ID="btnView" runat="server"
                                            CommandName="DownloadFile"
                                            CommandArgument='<%# Eval("File Name") %>'
                                            ImageUrl="img/view.gif"
                                            Width="24px"
                                            Visible='<%# Eval("File Name") <> ""%>'
                                            OnClientClick='<%# Eval("File Name") %>' />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Download File">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDownload" runat="server"
                                            CommandName="DownloadFile"
                                            CommandArgument='<%# Eval("File Name") %>'
                                            ImageUrl="img/download.gif"
                                            Width="24px"
                                            Visible='<%# Eval("File Name") <> ""%>' />
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
            //newWindow.print();
        }
    </script>
    <!-- Trust Icons Section -->
    <%--  <section>
        <div class="container-fluid py-5" style="background-color: #f9f9f9;">
            <div class="container px-4">
                <div class="row text-center gy-5">

                    <!-- TRUST & EXPERIENCE -->
                    <div class="col-md-3 d-flex flex-column align-items-center px-3">
                        <img src="https://img.icons8.com/color/128/medal.png" alt="Award Medal" class="mb-4" />
                        <h5 class="fw-bold text-primary mb-3">TRUSTED BY THOUSANDS</h5>
                        <p class="text-primary fs-6">
                            For more than 25 years IRStaxrecords.com has been trusted by thousands of US Banks and Mortgage Companies nationally.
                        </p>
                        <p class="fw-semibold text-primary fs-5 mt-3">TRUST & EXPERIENCE</p>
                    </div>

                    <!-- PROVEN & DEPENDABLE -->
                    <div class="col-md-3 d-flex flex-column align-items-center px-3">
                        <img src="https://img.icons8.com/color/128/usa.png" alt="USA Map" class="mb-4" />
                        <h5 class="fw-bold text-primary mb-3">PROVEN & DEPENDABLE</h5>
                        <p class="text-primary fs-6">
                            With millions of transcripts delivered, IRStaxrecords.com is one of the largest distribution hubs for U.S. government tax data.
                        </p>
                    </div>

                    <!-- SECURE & COMPLIANT -->
                    <div class="col-md-3 d-flex flex-column align-items-center px-3">
                        <img src="https://img.icons8.com/color/128/lock--v1.png" alt="Secure Lock" class="mb-4" />
                        <h5 class="fw-bold text-primary mb-3">SECURE & COMPLIANT</h5>
                        <p class="text-primary fs-6">
                            Fast, easy, and secure processing with top-level encryption. Our data centers exceed compliance standards.
                        </p>
                    </div>

                    <!-- SPEED & ACCURACY -->
                    <div class="col-md-3 d-flex flex-column align-items-center px-3">
                        <img src="https://img.icons8.com/color/128/delivery.png" alt="Speed Icon" class="mb-4" />
                        <h5 class="fw-bold text-primary mb-3">SPEED & ACCURACY</h5>
                        <p class="text-primary fs-6">
                            Government records — we provide accurate transcripts with the fastest turnaround times in the industry.
                        </p>
                    </div>

                </div>
            </div>
        </div>
    </section>--%>
</asp:Content>
