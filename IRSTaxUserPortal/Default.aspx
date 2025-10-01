<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.Master" CodeBehind="Default.aspx.vb" Inherits="IRSTaxUserPortal.Welcome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap CSS + JS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
        <link
      href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"
      rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <!-- Card Section -->

 <!-- Trust Icons Section -->
 <section>
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
     <div class="container-fluid py-5" style="background-color: #f9f9f9;">
    <div class="container px-4">
        <!-- Card Section -->
    <section>
        <div class="container py-5">
            <div class="row g-4">

                <!-- Card 1 -->
                <div class="col-md-4">
                    <div class="card shadow-lg h-100 rounded-4 border-1">
                        <div class="card-body d-flex flex-column">
                            <div class="d-flex align-items-center mb-3">
                                <img src="https://img.icons8.com/color/64/000000/pdf.png" alt="PDF Icon" style="width: 64px; height: 64px;" />
                                <h5 class="ms-3 mb-0 fs-3 text-primary">
                                    Form <strong>4506-C</strong><br />
                                    <a href="Login.aspx" class="text-primary fs-8 text-decoration-underline">UPLOAD PDF</a>
                                </h5>
                            </div>
                            <h6 class="text-danger fs-5">IRS Income Verification</h6>
                            <p class="text-primary fs-5">
                                Verify your applicants income with US Government tax records.
                                <br /><br />
                                Obtain 1040s, W2s, 1099s, Corporate 1120s, 1065s, Record of Account, and Account Transcripts directly from the IRS.
                            </p>
                            <p class="text-primary fs-6">
                                Do not be fooled by altered W2s, forged verification of deposits, fake tax returns.
                            </p>
                            <img src="/img/hud.png" alt="HUD Partners" class="img-fluid mb-3 mt-auto" />
                            <a href="Login.aspx" class="btn btn-outline-primary w-100 fw-semibold text-decoration-underline fs-5">Get Started</a>
                        </div>
                    </div>
                </div>

                <!-- Card 2 -->
                <div class="col-md-4">
                    <div class="card shadow-lg h-100 rounded-4 border-1">
                        <div class="card-body d-flex flex-column">
                            <div class="d-flex align-items-center mb-3">
                                <img src="https://img.icons8.com/color/64/000000/pdf.png" alt="PDF Icon" style="width: 64px; height: 64px;" />
                                <h5 class="ms-3 mb-0 fs-3 text-primary">
                                    Form <strong>8821</strong><br />
                                    <a href="Login.aspx" class="text-primary fs-8 text-decoration-underline">UPLOAD PDF</a>
                                </h5>
                            </div>
                            <h6 class="text-danger fs-5">Expedited Income Verification</h6>
                            <p class="text-primary fs-5">
                                Verify your applicants income with same day service.
                                <br /><br />
                                Secure online ordering — IRS transcripts delivered within hours of request.
                            </p>
                            <p class="text-primary fs-6">
                                Last minute transcript conditions? We have you covered.
                            </p>
                            <img src="/img/hud.png" alt="HUD Partners" class="img-fluid mb-3 mt-auto" />
                            <a href="Login.aspx" class="btn btn-outline-primary w-100 fw-semibold text-decoration-underline fs-5">Get Started</a>
                        </div>
                    </div>
                </div>

                <!-- Card 3 -->
                <div class="col-md-4">
                    <div class="card shadow-lg h-100 rounded-4 border-1">
                        <div class="card-body d-flex flex-column">
                            <div class="d-flex align-items-center mb-3">
                                <img src="https://img.icons8.com/color/64/000000/verified-account.png" alt="Verify Icon" style="width: 64px; height: 64px;" />
                                <h5 class="ms-3 mb-0 fs-3 text-primary">
                                    Order Social Security<br />
                                    Validations Online
                                </h5>
                            </div>
                            <h6 class="text-danger fs-5">Social Security Verification</h6>
                            <p class="text-primary fs-5">
                                Verify your applicant's Social Security Number with our fast and accurate online system via the U.S. Social Security Administration.
                            </p>
                            <ul class="small text-primary fs-5">
                                <li>SSN Match or No Match report</li>
                                <li>Data authenticated via SSA</li>
                                <li>Fast &amp; cost-effective</li>
                            </ul>
                            <a href="Login.aspx" class="btn btn-outline-primary w-100 mt-auto text-decoration-underline fw-semibold fs-5">Start ordering today</a>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </section>
    </div>
</div>
 </section>
</asp:Content>
