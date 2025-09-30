<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.Master" CodeBehind="Welcome.aspx.vb" Inherits="IRSTaxUserPortal.Default1" %>

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
    <section class="hero-section border-top-yellow mt-2">
        <div id="heroCarousel" class="carousel slide carousel-fade h-80" data-bs-ride="carousel" data-bs-interval="3000">
            <div class="carousel-inner h-100">
                <div class="carousel-item active h-100">
                    <img src="https://www.irstaxrecords.com/new/images/bannerflip1.jpg" class="d-block w-100 h-100 object-fit-cover" alt="Slide 1">
                </div>
                <div class="carousel-item h-100">
                    <img src="https://www.irstaxrecords.com/new/images/bannerflip2.jpg" class="d-block w-100 h-100 object-fit-cover" alt="Slide 2">
                </div>
                <div class="carousel-item h-100">
                    <img src="https://www.irstaxrecords.com/new/images/bannerflip3.jpg" class="d-block w-100 h-100 object-fit-cover" alt="Slide 3">
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

                        <h4>Form 4506 Orders</h4>
                        <asp:GridView ID="Grid1" runat="server"
                            CssClass="table table-striped table-bordered"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            PageSize="5"
                            OnPageIndexChanging="Grid1_PageIndexChanging"
                            PagerStyle-CssClass="pagination-container"
                            PagerSettings-Mode="NumericFirstLast"
                            PagerSettings-FirstPageText="« First"
                            PagerSettings-LastPageText="Last »"
                            PagerSettings-NextPageText="Next ›"
                            PagerSettings-PreviousPageText="‹ Prev">
                            <Columns>
                                <asp:BoundField DataField="Order Number" HeaderText="Order Number" />
                                <asp:BoundField DataField="Tax Payer" HeaderText="Tax Payer" />
                                <asp:BoundField DataField="SSN" HeaderText="SSN" />
                                <asp:BoundField DataField="Loan Number" HeaderText="Loan Number" />
                                <asp:BoundField DataField="Order Date" HeaderText="Order Date"
                                    DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                <asp:TemplateField HeaderText="Form Type">
                                    <ItemTemplate>
                                        <%# GetFormTypeName(Convert.ToInt32(Eval("Form Type"))) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Name">
                                    <ItemTemplate>
                                        <a href='<%#  ResolveUrl("~/Uploads/") & Eval("File Name") %>' target="_blank">
                                            <%# Eval("File Name") %>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="OrderType" HeaderText="Order Type" Visible="false" />

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="btnView" runat="server"
                                            CommandName="ViewOrder"
                                            CommandArgument='<%# Eval("Order Number") %>'
                                            Text="View" CssClass="btn btn-primary btn-sm" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblGrid1Message" runat="server" ForeColor="Red" Visible="False" />
                        <hr />

                        <h4 class="mt-4">Form 8821 Orders</h4>
                        <asp:GridView ID="Grid2" runat="server"
                            CssClass="table table-striped table-bordered"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            PageSize="5"
                            OnPageIndexChanging="Grid2_PageIndexChanging"
                            PagerStyle-CssClass="pagination-container"
                            PagerSettings-Mode="NumericFirstLast"
                            PagerSettings-FirstPageText="« First"
                            PagerSettings-LastPageText="Last »"
                            PagerSettings-NextPageText="Next ›"
                            PagerSettings-PreviousPageText="‹ Prev">
                            <Columns>
                                <asp:BoundField DataField="Order Number" HeaderText="Order Number" />
                                <asp:BoundField DataField="Tax Payer" HeaderText="Tax Payer" />
                                <asp:BoundField DataField="SSN" HeaderText="SSN" />
                                <asp:BoundField DataField="Loan Number" HeaderText="Loan Number" />
                                <asp:BoundField DataField="Order Date" HeaderText="Order Date"
                                    DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                <asp:TemplateField HeaderText="Form Type">
                                    <ItemTemplate>
                                        <%# GetFormTypeName(Convert.ToInt32(Eval("Form Type"))) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Name">
                                    <ItemTemplate>
                                        <a href='<%#  ResolveUrl("~/Uploads/") & Eval("File Name") %>' target="_blank">
                                            <%# Eval("File Name") %>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="OrderType" HeaderText="Order Type" Visible="false" />

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="btnView" runat="server"
                                            CommandName="ViewOrder"
                                            CommandArgument='<%# Eval("Order Number") %>'
                                            Text="View" CssClass="btn btn-primary btn-sm" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblGrid2Message" runat="server" ForeColor="Red" Visible="False" />
                        <hr />

                        <h4 class="mt-4">SSV Orders</h4>
                        <asp:GridView ID="Grid3" runat="server"
                            CssClass="table table-striped table-bordered"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            PageSize="5"
                            OnPageIndexChanging="Grid3_PageIndexChanging"
                            PagerStyle-CssClass="pagination-container"
                            PagerSettings-Mode="NumericFirstLast"
                            PagerSettings-FirstPageText="« First"
                            PagerSettings-LastPageText="Last »"
                            PagerSettings-NextPageText="Next ›"
                            PagerSettings-PreviousPageText="‹ Prev">
                            <Columns>
                                <asp:BoundField DataField="Order Number" HeaderText="Order Number" />
                                <asp:BoundField DataField="Tax Payer" HeaderText="Tax Payer" />
                                <asp:BoundField DataField="SSN" HeaderText="SSN" />
                                <asp:BoundField DataField="Loan Number" HeaderText="Loan Number" />
                                <asp:BoundField DataField="Order Date" HeaderText="Order Date"
                                    DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                <asp:TemplateField HeaderText="Form Type">
                                    <ItemTemplate>
                                        <%# GetFormTypeName(Convert.ToInt32(Eval("Form Type"))) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Name">
                                    <ItemTemplate>
                                        <a href='<%#  ResolveUrl("~/Uploads/") & Eval("File Name") %>' target="_blank">
                                            <%# Eval("File Name") %>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="OrderType" HeaderText="Order Type" Visible="false" />

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="btnView" runat="server"
                                            CommandName="ViewOrder"
                                            CommandArgument='<%# Eval("Order Number") %>'
                                            Text="View" CssClass="btn btn-primary btn-sm" />
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


    <!-- Trust Icons Section -->
    <section>
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
    </section>
</asp:Content>
