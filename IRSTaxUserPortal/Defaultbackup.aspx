<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.Master" CodeBehind="Default.aspx.vb" Inherits="IRSTaxUserPortal.Welcome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap CSS + JS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />

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
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Card Section -->
<script>
  document.addEventListener('DOMContentLoaded', function () {
    const el = document.getElementById('heroCarousel');
    const count = el.querySelectorAll('.carousel-item').length;
    const idx = Math.floor(Math.random() * count);
    bootstrap.Carousel.getOrCreateInstance(el).to(idx);
  });
</script>
    <!-- Trust Icons Section -->
    <section class="hero-section border-top border-warning border-5">
        <div id="heroCarousel" class="carousel slide carousel-fade h-100 h-md-80" data-bs-ride="carousel" data-bs-interval="5000">
            <div class="carousel-inner h-100">

                <div class="carousel-item active">
                    <img src="/new/images/bannerflip1.jpg"
                        class="d-block w-100 img-fluid object-fit-cover"
                        alt="Slide 1">
                </div>

                <div class="carousel-item">
                    <img src="/new/images/bannerflip2.jpg"
                        class="d-block w-100 img-fluid object-fit-cover"
                        alt="Slide 2">
                </div>

                <div class="carousel-item">
                    <img src="/new/images/bannerflip3.jpg"
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



    <div class="container-fluid " style="background-color: #f9f9f9;">
        <div class="container px-4">
            <!-- Card Section -->
            <section>
                <div class="container py-5">
                    <div class="row g-4">

                        <!-- Card 1 -->
                        <div class="col-md-4 col-sm-12">
                            <div class="card h-100 rounded-4 custom-shadow-right">
                                <div class="card-body d-flex flex-column text-center text-md-start">
                                    <div class="d-flex align-items-center justify-content-center justify-content-md-start mb-3">
                                        <img src="/img/Pdf_4506-C.png" alt="Order 4506-C"
     class="img-fluid"
     style="width:302px !important; height:126px !important; max-width:none !important;" />
                                    </div>
                                    <h6>IRS Income Verification</h6>
                                    <h5 class="text-custom-blue">Verify your applicant's income with US Government tax records.
                                    </h5>
                                    <br>
                                    <br>
                                    <p>
                                        Obtain 1040s, W2s, 1099s, Corporate 1120s, 1065s, Record of Account, and Account Transcripts directly from the IRS.
              Do not be fooled by altered W2s, forged verification of deposits, fake tax returns and Bogus Financial Statements.
              <br>
                                        <br>
                                        Cross reference your applicant's declared income with what is actually being reported to the
              Internal Revenue Service
              <br>
                                        <br>
                                        Turn around time with form 4506-C is 48 hours
                                    </p>
                                    <img src="/pennymac-.gif"
                                        alt="HUD Partners"
                                        class="img-fluid mb-3 mt-auto w-95 w-md-432 mx-auto mx-md-0" />

                                    <a href="order_4506.aspx" class="btn btn-custom-blue w-100 text-decoration-e">Order Transcripts</a>
                                </div>
                            </div>
                        </div>

                        <!-- Card 2 -->
                        <div class="col-md-4 col-sm-12">
                            <div class="card h-100 rounded-4 custom-shadow-right">
                                <div class="card-body d-flex flex-column text-center text-md-start">
                                    <div class="d-flex align-items-center justify-content-center justify-content-md-start mb-3">
                                        <img src="/img/Pdf8821.png" alt="PDF Icon"
     class="img-fluid"
     style="width:295px !important; height:107px !important; max-width:none !important;" />
                                    </div><p>
                                    <h6>Expedited Income Verification</h6>
                                    <h5 class="text-custom-blue">Verify your applicant's income with same day service.
                                    </h5>
                                    <br>
                                    <br>
                                    <p>Secure online ordering — IRS transcripts delivered within hours of request.</p>
                                    <p>
                                        Last minute transcript conditions? We have you covered with 8821 expedited service.
              <br>
                                        <br>
                                        Fast, Easy and Secure online ordering makes obtaining IRS transcripts a snap!
              Secure, efficient, and trusted nationwide.
                                    </p>
                                    <img src="/pennymac-.gif"
                                        alt="HUD Partners"
                                        class="img-fluid mb-3 mt-auto w-95 w-md-432 mx-auto mx-md-0" />

                                    <a href="order_88215.aspx" class="btn btn-custom-blue w-100 text-decoration-">Get Started</a>
                                </div>
                            </div>
                        </div>

                        <!-- Card 3 -->
                        <div class="col-md-4 col-sm-12">
                            <div class="card h-100 rounded-4 custom-shadow-right">
                                <div class="card-body d-flex flex-column text-center text-md-start">
                                    <div class="d-flex align-items-center justify-content-center justify-content-md-start mb-3">
                                        <img src="/ssv.gif" alt="PDF Icon"
     class="img-fluid"
     style="width:336px !important; height:106px !important; max-width:none !important;" />
                                    </div><p>
                                    <h6>Social Security Verification</h6>
                                    <h5 class="text-custom-blue">Verify your applicant's Social Security Number with our fast and accurate online system via the U.S. Social Security Administration.
                                    </h5>
                                    <ul class="text-start text-md-start mx-auto mx-md-0">
                                        <li>Verification the Social Security Number was issued to your browser</li>
                                        <li>Verification of your browser identity</li>
                                        
                                        <li class="text-danger">Fast 15 minute turn times</li>
                                        <li>100% accurate information, all data is authenticated via U.S. Social Security Administration database</li>
                                        <li>Easy and cost effective</li>
                                        <li>Receive Social Security MATCH validation certificate or NO MATCH certificate</li>
                                    </ul>
                                    <a href="orderSSV.aspx" class="btn btn-custom-blue w-100 text-decoration- mt-auto">Start ordering today</a>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </section>

        </div>
    </div>

</asp:Content>
