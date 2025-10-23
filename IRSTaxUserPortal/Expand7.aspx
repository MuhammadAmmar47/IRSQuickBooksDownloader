<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Expand7.aspx.vb" Inherits="IRSTaxUserPortal.Expand7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
<!-- Footer CTA styles (yours, preserved) -->
<style>
    /* Remove horizontal scrollbar from full-bleed elements */
    html, body {
        overflow-x: clip;
    }
    /* modern */
    @supports not (overflow-x: clip) {
        html, body {
            overflow-x: hidden;
        }
        /* fallback */
    }

    /* Keep the hero full-bleed without causing overflow */
    .hero {
        margin: 0;
    }

        .hero img {
            display: block;
            width: 100vw; /* fills the viewport width */
            max-width: 100vw;
            height: auto;
            margin-left: calc(50% - 50vw);
            margin-right: calc(50% - 50vw);
        }
</style>

<style>
    .itr-foot {
        text-align: center;
        padding: 60px 20px;
        background-color: #f8f9fa;
    }

        .itr-foot h3 {
            color: #00498C;
            font-weight: 700;
            margin-bottom: 10px;
        }

        .itr-foot p {
            color: #333;
            margin-bottom: 25px;
            font-size: 1.1rem;
        }

    .itr-cta-row {
        display: flex;
        justify-content: center;
        gap: 20px;
        flex-wrap: wrap;
    }

    .itr-btn {
        display: inline-block;
        padding: 12px 28px;
        font-size: 1rem;
        font-weight: 600;
        border-radius: 6px;
        text-decoration: none;
        transition: all .3s ease;
    }

    .itr-btn-primary {
        background-color: #00498C;
        color: #fff;
        border: none;
    }

        .itr-btn-primary:hover {
            background-color: #5dade2;
            color: #fff;
        }

    .itr-btn-ghost {
        background-color: transparent;
        color: #00498C;
        border: 2px solid #00498C;
    }

        .itr-btn-ghost:hover {
            background-color: #5dade2;
            color: #fff;
            border-color: #5dade2;
        }
    /* NEW: navy solid button (used only for “Talk to an Expert”) */
    .itr-btn-navy {
        background: #0b2d6b;
        color: #fff;
        border: 1px solid #0b2d6b;
    }

        .itr-btn-navy:hover {
            background: #ffffff;
            color: #fff;
            border-color: #08224f;
            color: #000000;
        }
</style>

<!-- Table + expandable rows (yours, preserved) -->
<style>
    :root {
        --bg: #E7EBEF;
        --txt: #1a1a1a;
        --muted: #555;
        --row: #f9fbff;
        --row-hover: #f0f6ff;
        --edge: #dce6f5;
        --neon: #0077ff;
        --radius: 12px;
    }

    body {
        margin: 0;
        background: var(--bg);
        color: var(--txt);
        font: 15px/1.5 system-ui, sans-serif;
        /* was padding:40px 16px; — removed top padding so Services sits closer to banner */
        padding: 0 16px;
    }

    .wrap {
        width: min(1100px,95vw);
        margin: 0 auto
    }

    .title {
        margin: 0 0 16px;
        font-weight: 700
    }

    .sub {
        margin: 0 0 20px;
        color: var(--muted)
    }

    .table-shell {
        background: #fff;
        border: 1px solid var(--edge);
        border-radius: var(--radius);
        box-shadow: 0 6px 18px rgba(0,0,0,.06);
        overflow: hidden;
    }

    table {
        width: 100%;
        border-collapse: separate;
        border-spacing: 0
    }

    thead th {
        text-align: left;
        font-weight: 600;
        color: var(--muted);
        padding: 14px 18px;
        border-bottom: 1px solid var(--edge);
        background: #f6f9ff;
    }

    tbody tr.data {
        transition: background .25s
    }

        tbody tr.data:hover {
            background: var(--row-hover)
        }

    tbody td {
        padding: 14px 18px;
        border-bottom: 1px solid #eef2fa
    }

    .lm-btn {
        appearance: none;
        border: 0;
        background: none;
        padding: 0;
        cursor: pointer;
        font: inherit;
        color: var(--neon);
        text-decoration: none;
        font-weight: 500;
        transition: color .15s ease;
    }

        .lm-btn:hover {
            color: #005ecc
        }

        .lm-btn:focus-visible {
            outline: 2px solid var(--neon);
            outline-offset: 3px;
            border-radius: 4px
        }

    tr.details > td {
        padding: 0;
        border-bottom: 1px solid #eef2fa;
        background: #fafcff;
    }

    .panel {
        height: 0;
        overflow: hidden;
        transition: height .33s cubic-bezier(.2,.75,.2,1)
    }

    .panel-inner {
        padding: 18px;
        color: var(--muted)
    }
</style>

<!-- Services section styles (with hero full-bleed + tighter spacing) -->
<style>
    /* Full-bleed banner */
    .hero {
        margin: 0;
    }

        .hero img {
            display: block;
            width: 100vw;
            max-width: 100vw;
            height: auto;
            margin-left: calc(50% - 50vw);
            margin-right: calc(50% - 50vw);
        }

    /* ===== Scoped Styles: .itr-services ===== */
    .itr-services {
        --itr-bg: #ffffff;
        --itr-ink: #0b1320;
        --itr-muted: #6a7280;
        --itr-brand: #1f6fff;
        --itr-brand-ink: #0d47a1;
        --itr-ring: rgba(31,111,255,.25);
        --itr-line: #e7eaf0;
        --itr-card: #ffffff;
        --itr-card-shadow: 0 8px 28px rgba(2,12,44,.06);
        background: var(--itr-bg);
        color: var(--itr-ink);
        padding: 12px 16px 48px; /* &#8595; from 48px: pulls Services closer to the banner */
    }

    .itr-wrap {
        max-width: 1160px;
        margin: 0 auto;
    }

    /* Hero card for the section heading/intro */
    .itr-card-hero {
        background: var(--itr-card);
        border: 1px solid var(--itr-line);
        border-radius: 16px;
        box-shadow: var(--itr-card-shadow);
        padding: 22px;
        margin-bottom: 20px;
    }

        .itr-card-hero h2 {
            font-size: 32px;
            line-height: 1.2;
            margin: 0 0 8px;
            letter-spacing: .2px;
        }

        .itr-card-hero .itr-sub {
            font-size: 16px;
            color: var(--itr-muted);
            margin: 0 0 8px;
        }

        .itr-card-hero .itr-intro {
            font-size: 17px;
            line-height: 1.6;
            margin: 0;
            max-width: 900px;
            color: var(--itr-ink);
        }

    .itr-grid {
        display: grid;
        grid-template-columns: 1fr;
        gap: 18px;
        margin: 24px 0 10px;
    }

    @media (min-width:900px) {
        .itr-grid {
            grid-template-columns: 1fr 1fr;
            gap: 20px;
        }
    }

    .itr-card {
        background: var(--itr-card);
        border: 1px solid var(--itr-line);
        border-radius: 16px;
        padding: 22px;
        box-shadow: var(--itr-card-shadow);
        position: relative;
    }

    .itr-card-tag {
        position: absolute;
        right: 16px;
        top: 16px;
        font-size: 12px;
        background: rgba(31,111,255,.08);
        color: var(--itr-brand);
        border: 1px solid rgba(31,111,255,.25);
        padding: 4px 8px;
        border-radius: 999px;
    }

    .itr-card h3 {
        margin: 0 0 6px;
        font-size: 22px;
        line-height: 1.3;
    }

    .itr-card-lead {
        margin: 0 0 12px;
        color: var(--itr-muted);
        font-size: 16px;
    }

    .itr-list {
        list-style: none;
        padding: 0;
        margin: 0 0 18px;
    }

        .itr-list li {
            display: flex;
            align-items: flex-start;
            gap: 10px;
            padding: 8px 0;
            border-top: 1px dashed var(--itr-line);
        }

            .itr-list li:first-child {
                border-top: none;
            }

    .itr-bullet {
        min-width: 20px;
        line-height: 1.2;
    }

    .itr-cta-row {
        display: flex;
        flex-wrap: wrap;
        gap: 10px;
    }

    .itr-btn {
        display: inline-block;
        padding: 10px 14px;
        border-radius: 12px;
        text-decoration: none;
        font-weight: 600;
        border: 1px solid var(--itr-line);
        transition: transform .06s ease, background-color .15s ease, border-color .15s ease;
        will-change: transform;
    }

        .itr-btn:hover {
            transform: translateY(-1px);
        }

    .itr-btn-primary {
        background: var(--itr-brand);
        color: #fff;
        border-color: var(--itr-brand);
    }

        .itr-btn-primary:hover {
            background: var(--itr-brand-ink);
            border-color: var(--itr-brand-ink);
        }

    .itr-btn-ghost {
        background: #fff;
        color: var(--itr-ink);
    }

    .itr-btn:focus {
        outline: none;
        box-shadow: 0 0 0 4px var(--itr-ring);
    }
    /* Navy outline for the four ghost buttons inside the two service cards */
    .itr-services .itr-grid .itr-btn-ghost {
        background: transparent;
        color: #4000FF; /* (left as provided) */
        border-color: #0b2d6b; /* navy border */
        border-width: 1px;
    }

        .itr-services .itr-grid .itr-btn-ghost:hover {
            background: #0b2d6b;
            color: #fff;
            border-color: #0b2d6b;
        }

    .itr-btn:focus {
        outline: none;
        box-shadow: 0 0 0 4px var(--itr-ring);
    }

    .itr-proof {
        display: grid;
        grid-template-columns: repeat(2,1fr);
        gap: 12px;
        margin: 20px 0 24px;
    }

    @media (min-width:780px) {
        .itr-proof {
            grid-template-columns: repeat(4,1fr);
        }
    }

    .itr-proof-item {
        text-align: center;
        background: #fafbff;
        border: 1px solid var(--itr-line);
        border-radius: 14px;
        padding: 16px 10px;
    }

    .itr-proof-num {
        font-size: 20px;
        font-weight: 700;
        margin-bottom: 4px;
    }

    .itr-proof-label {
        font-size: 12.5px;
        color: var(--itr-muted);
    }

    .itr-services .itr-foot {
        text-align: center;
        border-top: 1px solid var(--itr-line);
        padding-top: 18px;
        background: transparent;
    }

        .itr-services .itr-foot h3 {
            margin: 0 0 6px;
            font-size: 22px;
            color: inherit;
        }

        .itr-services .itr-foot p {
            margin: 0 0 14px;
            color: var(--itr-muted);
            font-size: 16px;
        }
</style>

<!-- Layout for sticky-bottom footer image -->
<style>
    html, body {
        height: 100%;
    }

    body {
        min-height: 100vh;
        display: flex;
        flex-direction: column;
    }
    /* Wrap all page content (except the footer image) */
    main.site-main {
        flex: 1 0 auto;
    }
    /* Full-bleed footer banner */
    .footer-banner {
        flex-shrink: 0;
    }

        .footer-banner img {
            display: block;
            width: 100vw;
            max-width: 100vw;
            height: auto;
            margin-left: calc(50% - 50vw);
            margin-right: calc(50% - 50vw);
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <!-- ===== IRS Tax Records • Services Section ===== -->
    <section class="itr-services">
      <div class="itr-wrap">

        <!-- HERO CARD -->
        <article class="itr-card itr-card-hero" aria-labelledby="services-title">
          <h2 id="services-title">Our Services</h2>
          <p class="itr-intro"><strong>We’re Government Records specialists—the experienced partner you can count on to obtain official IRS and Social Security documents quickly and easily.</strong></p>
          <p class="itr-intro">Trusted Verification. Proven Experience. Unmatched Speed.</p>
          <p class="itr-intro">
            For more than <strong>25 years</strong>, IRS Tax Records has helped lenders, banks, and financial professionals verify income and identity with confidence.
            Our secure platform delivers <strong>fast, compliant, and fraud-resistant</strong> solutions that keep your pipeline protected and moving.
          </p>
        </article>

        <!-- Two service cards -->
        <div class="itr-grid">
          <!-- IRS Tax Transcript Retrieval -->
          <article class="itr-card">
            <div class="itr-card-tag">Expedited Available</div>
            <h3>IRS Tax Transcript Retrieval</h3>
            <p class="itr-card-lead">It’s the simplest way to meet compliance standards, detect fraud, and accelerate your loan process—all backed by official IRS data.</p>
            <p class="itr-card-lead">Obtain official IRS Tax Transcripts directly from the <strong>Internal Revenue Service</strong> using <strong>Forms 4506-C or 8821</strong>.</p>
            <ul class="itr-list">
              <li><span class="itr-bullet">•</span> <span><strong>Same-day turn-time</strong> option when every hour counts.</span></li>
              <li><span class="itr-bullet">•</span> <span>Secure and reliable—backed by <strong>millions</strong> of transcripts processed.</span></li>
              <li><span class="itr-bullet">•</span> <span>Government records—it's all we do. Compliance made easy</span></li>
              <li><span class="itr-bullet">•</span> <span><strong>Easy setup—start ordering in minutes</strong></span></li>
            </ul>
            <div class="itr-cta-row">
              <a href="/wizard7.aspx" class="itr-btn itr-btn-ghost">Order Transcripts</a>
              <a href="#details" class="itr-btn itr-btn-ghost">Service Details</a>
            </div>
          </article>

          <!-- Social Security Verification -->
          <article class="itr-card">
            <div class="itr-card-tag">Expedited • ~15 minutes</div>
            <h3>Social Security Verification</h3>
            <p class="itr-card-lead">
              Confirm borrower identity in minutes with data from the Social Security Administration. Detect identity fraud early with verified SSA results and simplify compliance with clear, auditable records. Secure confirmations typically available within minutes—via a fast, easy ordering interface.
            </p>
            <ul class="itr-list">
              <li><span class="itr-bullet">•</span> <span>SSA-sourced identity confirmation in minutes.</span></li>
              <li><span class="itr-bullet">•</span> <span>Early fraud detection via verified matches / mismatches.</span></li>
              <li><span class="itr-bullet">•</span> <span>Simple ordering (single or bulk) and quick onboarding.</span></li>
              <li><span class="itr-bullet">•</span> <span>Secure portal with admin controls and <strong>live phone support</strong>.</span></li>
            </ul>
            <div class="itr-cta-row">
              <a href="/wizard7.aspx" class="itr-btn itr-btn-ghost">Verify Identity</a>
              <a href="#details" class="itr-btn itr-btn-ghost">Service Details</a>
            </div>
          </article>
        </div>

        <!-- Trust + Proof Bar -->
        <aside class="itr-proof">
          <div class="itr-proof-item"><div class="itr-proof-num">25+</div><div class="itr-proof-label">Years Trusted</div></div>
          <div class="itr-proof-item"><div class="itr-proof-num">Millions</div><div class="itr-proof-label">Transcripts Processed</div></div>
          <div class="itr-proof-item"><div class="itr-proof-num">Same-Day</div><div class="itr-proof-label">Expedited Option</div></div>
          <div class="itr-proof-item"><div class="itr-proof-num">Bank-Grade</div><div class="itr-proof-label">Security</div></div>
        </aside>

        <!-- Closing CTA -->
        <footer class="itr-foot">
          <h3>Fast. Secure. Reliable. For 25 Years and Counting.</h3>
          <p>Partner with IRS Tax Records and experience proven accuracy, unmatched turnaround, and responsive support.</p>
          <div class="itr-cta-row">
            <a href="/wizard7.aspx" class="itr-btn itr-btn-primary">Create Your Secure Account</a>
            <a href="/contactform.asp" class="itr-btn itr-btn-navy">Talk to an Expert</a>
          </div>
        </footer>

      

    <a name="details"></a>
    <div class="wrap">
      <h1 class="title">Service Details:</h1>
      <p class="sub">Services Details and FAQ.
      Click <em>Learn more</em>  about IRStaxRecords.com.</p>
      <p>
      <div class="table-shell">
        <table>
          <thead>
            <tr>
              <th>Service</th>
              <th>Turn-time</th>
              <th>Price</th>
              <th style="width:140px">More</th>
            </tr>
          </thead>
          <tbody>
            <!-- Row 1 -->
            <tr class="data" id="row-4506c">
              <td>How are transcripts ordered and delivered with form 4506-C?</td>
              <td>24–48 hrs</td>
              <td>Custom</td>
              <td>
                <button class="lm-btn" type="button" aria-expanded="false" aria-controls="d-4506c" id="t-4506c">
                  Learn more
                </button>
              </td>
            </tr>
            <tr class="details" aria-hidden="true">
              <td colspan="4">
                <div class="panel" id="d-4506c" role="region" aria-labelledby="t-4506c">
                  <div class="panel-inner">
                    <img
  src="https://www.irstaxrecords.com/HowTranscriptsAreDelivered.gif"
  alt="How transcripts are delivered"
  width="964"
  height="175"
  style="all: revert; display:inline-block; width:964px; height:175px; max-width:none; border:0;"
>

                    <p>Accessing IRS tax transcripts is quick and effortless. First Log in securely with your username and password. Click “Order Transcripts” and upload your completed IRS Form 4506-C or IRS Form 8821.<br><br>
                    Once uploaded, you’ll instantly receive a confirmation and order number. Your request is then immediately submitted to the Internal Revenue Service for processing. As soon as your transcripts are available, you’ll receive an email notification. Simply log in to your account to view and download your transcripts instantly.<br><br>
                    Our process is designed with speed, simplicity, and security in mind. With more than 25 years of experience and millions of transcripts processed, we’ve perfected a system that delivers accuracy, reliability, and peace of mind—every single time</p>
                    <p>&nbsp;</p>
                  </div>
                </div>
              </td>
            </tr>

            <!-- Row 2 -->
            <tr class="data" id="row-8821">
              <td>Expedited Service to obtain transcripts with same day service</td>
              <td>3 hours</td>
              <td>Custom</td>
              <td>
                <button class="lm-btn" type="button" aria-expanded="false" aria-controls="d-8821" id="t-8821">
                  Learn more
                </button>
              </td>
            </tr>
            <tr class="details" aria-hidden="true">
              <td colspan="4">
                <div class="panel" id="d-8821" role="region" aria-labelledby="t-8821">
                  <div class="panel-inner">
                    <h2 data-start="126" data-end="177"><strong data-start="129" data-end="177">IRS
                    Form 8821 – Expedited Transcript Service</strong></h2>
                    <p data-start="179" data-end="440">When time is short and
                    transcripts are critical, <strong data-start="228" data-end="247">IRS
                    TaxRecords.com</strong> delivers.<br data-start="257" data-end="260">
                    Our <strong data-start="264" data-end="299">expedited IRS Form
                    8821 service</strong> allows us to obtain verified IRS tax
                    transcripts <strong data-start="349" data-end="391">within hours
                    of receiving your request</strong> — not days or weeks like
                    traditional channels.</p>
                    <p data-start="442" data-end="754">With over <strong data-start="452" data-end="490">25
                    years of specialized experience</strong>, our systems and
                    long-standing relationships with the IRS make it possible to
                    retrieve and deliver transcripts with exceptional speed and
                    precision. This capability is the result of years of refinement,
                    process automation, and deep understanding of IRS systems.</p>
                    <h3 data-start="756" data-end="782"><strong data-start="760" data-end="782">Speed
                    That Matters</strong></h3>
                    <p data-start="783" data-end="1088">Whether you’re facing a
                    loan closing deadline, compliance review, or time-sensitive
                    audit, our expedited 8821 service gets you back into compliance <strong data-start="931" data-end="939">fast</strong>.
                    The difference between waiting days and having your transcripts
                    the same day can make or break a closing or approval — and
                    that’s where we excel.</p>
                    <h3 data-start="1090" data-end="1118"><strong data-start="1094" data-end="1118">Why
                    It’s So Valuable</strong></h3>
                    <p data-start="1119" data-end="1427">When lenders, auditors, or
                    underwriters need tax transcripts urgently, delays can cost
                    time, money, and credibility. Our expedited service eliminates
                    the waiting and delivers verified IRS transcripts securely and
                    quickly, so your team can move forward with confidence and meet
                    critical deadlines with ease.</p>
                    <h3 data-start="1429" data-end="1461"><strong data-start="1433" data-end="1461">Experience
                    You Can Trust</strong></h3>
                    <p data-start="1462" data-end="1740">We’ve processed <strong data-start="1478" data-end="1505">millions
                    of transcripts</strong> over the years, giving us the insight
                    and efficiency needed to move faster than anyone else in the
                    industry. Our experience ensures accuracy, reliability, and
                    compliance with strict IRS and Treasury guidelines — even
                    under pressure.</p>
                    <h3 data-start="1742" data-end="1783"><strong data-start="1746" data-end="1783">Fast,
                    Friendly, and Ready to Help</strong></h3>
                    <p data-start="1784" data-end="2077">Our knowledgeable staff is
                    always glad to assist, answering questions promptly and guiding
                    you through every step of the process. We take pride in being
                    both fast <strong data-start="1947" data-end="1954">and</strong>
                    friendly — ensuring you get the documents you need to <strong data-start="2009" data-end="2059">close
                    loans, verify income, and stay compliant</strong> without
                    stress.</p>
                    <p data-start="2079" data-end="2238">When time is short, trust
                    the team that’s built for speed, precision, and reliability.<br data-start="2165" data-end="2168">
                    <strong data-start="2168" data-end="2238">IRSTaxRecords.com—
                    Expedited Transcript Solutions You Can Count On.</strong></p>
                    &nbsp;
                  </div>
                </div>
              </td>
            </tr>

            <!-- Row 3 -->
            <tr class="data" id="row-ssa">
              <td>How do I verify my clients Identity from the Social Security
                Administration?</td>
              <td>~15 minutes</td>
              <td>Custom</td>
              <td>
                <button class="lm-btn" type="button" aria-expanded="false" aria-controls="d-ssa" id="t-ssa">
                  Learn more
                </button>
              </td>
            </tr>
            <tr class="details" aria-hidden="true">
              <td colspan="4">
                <div class="panel" id="d-ssa" role="region" aria-labelledby="t-ssa">
                  <div class="panel-inner"><img
  src="https://www.irstaxrecords.com/HowSSN.gif"
  alt="How transcripts are delivered"
  width="964"
  height="175"
  style="all: revert; display:inline-block; width:964px; height:175px; max-width:none; border:0;"
                    <h2 data-start="186" data-end="250"><strong data-start="189" data-end="250">Social
                    Security Verification – Fast, Reliable, and Secure</strong></h2>
                    <p data-start="252" data-end="623">When accuracy and compliance
                    matter most, <strong data-start="294" data-end="313">IRSTaxRecords</strong> delivers instant confidence with our <strong data-start="351" data-end="391">Social
                    Security Verification Service</strong>. Designed for speed and
                    simplicity, our system confirms borrower identity directly with
                    the <strong data-start="484" data-end="524">Social Security
                    Administration (SSA)</strong> — helping you detect fraud
                    early, stay compliant, and keep your loan process moving without
                    delay.</p>
                    <h3 data-start="625" data-end="655"><strong data-start="629" data-end="655">Simple.
                    Fast. Trusted.</strong></h3>
                    <p data-start="656" data-end="1056">The process couldn’t be
                    easier.<br data-start="687" data-end="690">
                    Simply <strong data-start="697" data-end="736">log into your
                    secure online account</strong>, enter the borrower information
                    you wish to verify, and <strong data-start="793" data-end="815">upload
                    Form SSA-89</strong>, the Social Security Administration’s
                    authorization form. With a single click, your verification
                    request is submitted — and <strong data-start="940" data-end="974">within
                    as little as 15 minutes</strong>, you’ll receive a certified
                    result indicating either a <strong data-start="1030" data-end="1039">match</strong>
                    or <strong data-start="1043" data-end="1055">no match</strong>.</p>
                    <h3 data-start="1058" data-end="1084"><strong data-start="1062" data-end="1084">Detect
                    Fraud Early</strong></h3>
                    <p data-start="1085" data-end="1410">Our verification service
                    helps lenders and financial institutions quickly identify
                    discrepancies before they become problems. By verifying Social
                    Security Numbers directly with the SSA, you can prevent identity
                    fraud and ensure every borrower is exactly who they claim to be
                    — protecting your organization and your customers.</p>
                    <h3 data-start="1412" data-end="1448"><strong data-start="1416" data-end="1448">Stay
                    Compliant and Confident</strong></h3>
                    <p data-start="1449" data-end="1720">Remaining compliant with
                    federal verification standards has never been easier. The <strong data-start="1532" data-end="1551">IRSTaxRecords.com</strong> platform is built to meet rigorous security
                    and privacy requirements, ensuring every request and response is
                    handled with the highest level of protection and integrity.</p>
                    <h3 data-start="1722" data-end="1751"><strong data-start="1726" data-end="1751">Why
                    Lenders Choose Us</strong></h3>
                    <ul data-start="1752" data-end="2042">
                      <li data-start="1752" data-end="1789">
                        <p data-start="1754" data-end="1789"><strong data-start="1754" data-end="1787">Results
                        in 15 Minutes or Less</strong></p>
                      </li>
                      <li data-start="1790" data-end="1859">
                        <p data-start="1792" data-end="1859"><strong data-start="1792" data-end="1819">Direct
                        SSA Verification</strong> — official match/no match
                        certificate</p>
                      </li>
                      <li data-start="1860" data-end="1926">
                        <p data-start="1862" data-end="1926"><strong data-start="1862" data-end="1887">Secure,
                        Online Access</strong> — no phone calls or paperwork
                        delays</p>
                      </li>
                      <li data-start="1927" data-end="1974">
                        <p data-start="1929" data-end="1974"><strong data-start="1929" data-end="1972">Trusted
                        by Banks and Lenders Nationwide</strong></p>
                      </li>
                      <li data-start="1975" data-end="2042">
                        <p data-start="1977" data-end="2042"><strong data-start="1977" data-end="2012">Friendly,
                        Knowledgeable Support</strong> to assist whenever you need</p>
                      </li>
                    </ul>
                    <p data-start="2044" data-end="2323">Fast, simple, and
                    dependable — our <strong data-start="2079" data-end="2119">Social
                    Security Verification Service</strong> gives you the clarity you
                    need to move forward with confidence. Whether you’re
                    processing loans, onboarding clients, or screening applicants,
                    we help you make informed decisions in minutes — not hours.</p>
                    <p data-start="2325" data-end="2374"><strong data-start="2325" data-end="2374">IRSTaxRecords.com – Verification You Can Trust.</strong></p>
                    .
                  </div>
                </div>
              </td>
            </tr>

            <!-- Row 4 -->
            <tr class="data" id="row-Security">
              <td>Security at IRStaxrecords.com bank grade security and exceeding
                the industry standard</td>
              <td></td>
              <td></td>
              <td>
                <button class="lm-btn" type="button" aria-expanded="false" aria-controls="d-Security" id="t-Security">
                  Learn more
                </button>
              </td>
            </tr>
            <tr class="details" aria-hidden="true">
              <td colspan="4">
                <div class="panel" id="d-Security" role="region" aria-labelledby="t-Security">
                  <div class="panel-inner">
                    <h2 data-start="197" data-end="266"><strong data-start="200" data-end="266">System
                    Security &amp; Encryption – Exceeding the Industry Standard</strong></h2>
                    <p data-start="268" data-end="731">For more than <strong data-start="282" data-end="294">25
                    years</strong>, <strong data-start="296" data-end="315">IRStaxRecords.com</strong> has upheld one uncompromising promise — <strong data-start="356" data-end="456">to
                    protect sensitive client data with the highest levels of
                    security, encryption, and compliance</strong>. From day one, our
                    systems have been engineered to exceed the stringent
                    requirements set forth by the <strong data-start="559" data-end="587">Internal
                    Revenue Service</strong> and the <strong data-start="596" data-end="631">U.S.
                    Department of the Treasury</strong>, ensuring every document,
                    transmission, and verification is handled in a fully secured
                    environment.</p>
                    <h3 data-start="733" data-end="811"><strong data-start="737" data-end="811">Built
                    for Compliance. Verified by the U.S. Department of the Treasury.</strong></h3>
                    <p data-start="812" data-end="1311">Our security framework is
                    continuously reviewed under Treasury oversight to ensure we
                    remain compliant with evolving IRS security protocols. The
                    Department of the Treasury requires regular system validation,
                    penetration testing, and audit review — and our infrastructure
                    has consistently passed every standard with excellence.<br data-start="1138" data-end="1141">
                    Through decades of IRS guideline updates, technology shifts, and
                    new encryption mandates, we’ve maintained uninterrupted
                    compliance and an unblemished operational record.</p>
                    <h3 data-start="1313" data-end="1343"><strong data-start="1317" data-end="1343">Encryption
                    at the Core</strong></h3>
                    <p data-start="1344" data-end="1473">From transmission to
                    storage, <strong data-start="1374" data-end="1406">data is
                    encrypted end-to-end</strong> using advanced multi-layered
                    methods that exceed industry norms.</p>
                    <ul data-start="1474" data-end="2010">
                      <li data-start="1474" data-end="1638">
                        <p data-start="1476" data-end="1638"><strong data-start="1476" data-end="1491">In
                        Transit:</strong> All communications utilize <strong data-start="1519" data-end="1558">TLS
                        1.3 with AES-256-bit encryption</strong> — the same level
                        used by leading financial institutions and federal agencies.</p>
                      </li>
                      <li data-start="1639" data-end="1840">
                        <p data-start="1641" data-end="1840"><strong data-start="1641" data-end="1653">At
                        Rest:</strong> Sensitive data is stored in isolated,
                        hardened environments secured with <strong data-start="1727" data-end="1763">256-bit
                        AES full-disk encryption</strong>, multi-factor
                        administrative access, and continuous key rotation policies.</p>
                      </li>
                      <li data-start="1841" data-end="2010">
                        <p data-start="1843" data-end="2010"><strong data-start="1843" data-end="1874">Integrity
                        &amp; Authentication:</strong> Every transaction is
                        digitally signed and logged in real-time to provide a
                        verified chain of custody for each request and transcript.</p>
                      </li>
                    </ul>
                    <p data-start="2012" data-end="2300">Our network architecture
                    includes <strong data-start="2046" data-end="2065">segmented
                    VLANs</strong>, <strong data-start="2067" data-end="2089">firewall
                    isolation</strong>, and <strong data-start="2095" data-end="2126">intrusion
                    detection systems</strong> tuned to Treasury and IRS security
                    baselines — ensuring that client information remains
                    compartmentalized, traceable, and protected from external and
                    internal threats alike.</p>
                    <h3 data-start="2302" data-end="2338"><strong data-start="2306" data-end="2338">Exceeding
                    Industry Standards</strong></h3>
                    <p data-start="2339" data-end="2450">While many organizations
                    meet the minimum security benchmarks, we design our environment
                    to <strong data-start="2431" data-end="2447">surpass them</strong>.</p>
                    <ul data-start="2451" data-end="2896">
                      <li data-start="2451" data-end="2566">
                        <p data-start="2453" data-end="2566">Independent <strong data-start="2465" data-end="2502">third-party
                        vulnerability testing</strong> and <strong data-start="2507" data-end="2539">white-hat
                        intrusion analysis</strong> are conducted routinely.</p>
                      </li>
                      <li data-start="2567" data-end="2668">
                        <p data-start="2569" data-end="2668">Real-time <strong data-start="2579" data-end="2600">threat
                        monitoring</strong> and <strong data-start="2605" data-end="2624">log
                        correlation</strong> safeguard against emerging cyber
                        threats.</p>
                      </li>
                      <li data-start="2669" data-end="2785">
                        <p data-start="2671" data-end="2785">Redundant data centers
                        ensure <strong data-start="2701" data-end="2746">high
                        availability and disaster resilience</strong>, with zero
                        downtime for our clients.</p>
                      </li>
                      <li data-start="2786" data-end="2896">
                        <p data-start="2788" data-end="2896">Continuous employee
                        security training reinforces data-handling discipline at
                        every level of our operation.</p>
                      </li>
                    </ul>
                    <h3 data-start="2898" data-end="2923"><strong data-start="2902" data-end="2923">A
                    Legacy of Trust</strong></h3>
                    <p data-start="2924" data-end="3204">Over the past 25 years,
                    we’ve processed <strong data-start="2964" data-end="2995">millions
                    of secure requests</strong> without a single data compromise —
                    a record we’re proud of and one we intend to preserve. Our
                    security culture is not static; it evolves with technology,
                    regulation, and the needs of our financial partners.</p>
                    <h3 data-start="3206" data-end="3241"><strong data-start="3210" data-end="3241">Confidence
                    You Can Count On</strong></h3>
                    <p data-start="3242" data-end="3438">In an era where information
                    security is paramount, <strong data-start="3293" data-end="3312">IRStaxRecords.com<</strong> provides the peace of mind that comes only
                    from decades of proven performance, federal oversight, and
                    technical excellence.</p>
                    <p data-start="3440" data-end="3491">We don’t just meet
                    compliance — <strong data-start="3472" data-end="3489">we
                    define it.</strong></p>
                    <p data-start="3493" data-end="3550"><strong data-start="3493" data-end="3550">IRStaxRecords.com–Trusted. Tested.</strong></p>
                    &nbsp;
                  </div>
                </div>
              </td>
            </tr>

            <!-- Row 5 -->
            <tr class="data" id="row-Proven">
              <td>Proven and Dependable. Trusted by thousands for over 25 years</td>
              <td></td>
              <td></td>
              <td>
                <button class="lm-btn" type="button" aria-expanded="false" aria-controls="d-Proven" id="t-Proven">
                  Learn more
                </button>
              </td>
            </tr>
            <tr class="details" aria-hidden="true">
              <td colspan="4">
                <div class="panel" id="d-Proven" role="region" aria-labelledby="t-Proven">
                  <div class="panel-inner">
                    <h2 data-start="184" data-end="237"><strong data-start="187" data-end="237">Proven.
                    Dependable. Trusted for Over 25 Years.</strong></h2>
                    <p data-start="239" data-end="544">At <strong data-start="242" data-end="261">IRStaxRecords.com</strong>, reliability isn’t a promise — it’s a
                    proven track record. For more than <strong data-start="335" data-end="347">25
                    years</strong>, we’ve been the trusted partner for banks,
                    mortgage lenders, and financial institutions across the country,
                    delivering <strong data-start="467" data-end="507">millions of
                    verified IRS transcripts</strong> quickly, securely, and
                    accurately.</p>
                    <p data-start="546" data-end="871">Our history speaks for
                    itself. Many of our clients have remained with us for <strong data-start="623" data-end="643">over
                    two decades</strong>, relying on our consistency and our ability
                    to deliver even when the process becomes complex. When other
                    providers slow down, time out, or fall short, we’re the team
                    that gets it done — quietly, efficiently, and with precision.</p>
                    <h3 data-start="873" data-end="905"><strong data-start="877" data-end="905">Experience
                    That Delivers</strong></h3>
                    <p data-start="906" data-end="1356">After decades in this
                    industry, we’ve seen every possible challenge — delayed
                    transcripts, mismatched forms, IRS processing quirks, and
                    last-minute client deadlines. We’ve built our reputation on
                    being the company that doesn’t give up when others do. Our
                    experienced operations team and advanced tracking systems ensure
                    that when a file hits a roadblock, <strong data-start="1261" data-end="1290">we
                    find a way through it.</strong> That’s what dependability
                    looks like in real-world performance.</p>
                    <h3 data-start="1358" data-end="1396"><strong data-start="1362" data-end="1396">Dependability
                    You Can Count On</strong></h3>
                    <p data-start="1397" data-end="1782">From single transcript
                    requests to high-volume institutional orders, our systems are
                    engineered for consistency. We don’t just meet expectations
                    — we exceed them, every time. Our clients trust that when they
                    submit a request, it will be processed swiftly, securely, and
                    correctly. This dependability has made <strong data-start="1706" data-end="1725">IRStaxRecords.com</strong> the go-to name for lenders who can’t
                    afford uncertainty.</p>
                    <h3 data-start="1784" data-end="1828"><strong data-start="1788" data-end="1828">Personalized
                    Service That Stands Out</strong></h3>
                    <p data-start="1829" data-end="2192">Behind every transcript we
                    deliver is a <strong data-start="1869" data-end="1894">team that
                    truly cares</strong>. Our <strong data-start="1900" data-end="1926">customer
                    service staff</strong> is fast, friendly, and deeply
                    knowledgeable — available when you need them most. Whether
                    it’s clarifying a request, resolving an issue, or assisting
                    with a compliance question, our team goes the extra mile to
                    ensure you receive the support and results you expect.</p>
                    <h3 data-start="2194" data-end="2226"><strong data-start="2198" data-end="2226">Proven.
                    Reliable. Ready.</strong></h3>
                    <p data-start="2227" data-end="2566">Dependability isn’t
                    something we talk about — it’s something we demonstrate
                    every single day. From our long-standing clients who’ve
                    trusted us for 20+ years to the millions of secure transcripts
                    we’ve delivered nationwide, <strong data-start="2450" data-end="2469">IRStaxRecords.com</strong> continues to set the standard for
                    reliability in income verification and transcript retrieval.</p>
                    <p data-start="2568" data-end="2682">When accuracy matters, when
                    deadlines are tight, and when you simply can’t afford delays
                    — you can depend on us.</p>
                    <p data-start="2684" data-end="2734"><strong data-start="2684" data-end="2734">IRStaxRecords.com–Proven. Dependable. Trusted.</strong></p>
                    &nbsp;
                  </div>
                </div>
              </td>
            </tr>

            <!-- Row 6 -->
            <tr class="data" id="row-Government">
              <td>Government Records. It is all we do</td>
              <td></td>
              <td></td>
              <td>
                <button class="lm-btn" type="button" aria-expanded="false" aria-controls="d-Government" id="t-Government">
                  Learn more
                </button>
              </td>
            </tr>
            <tr class="details" aria-hidden="true">
              <td colspan="4">
                <div class="panel" id="d-Government" role="region" aria-labelledby="t-Government">
                  <div class="panel-inner">
                    <h2 data-start="201" data-end="277"><strong data-start="204" data-end="277">Government
                    Records. It’s All We Do — and That’s What Makes Us Unique.</strong></h2>
                    <p data-start="279" data-end="589">At <strong data-start="282" data-end="301">IRS
                    Tax Records</strong>, we focus exclusively on one thing — <strong data-start="339" data-end="427">providing
                    official government records with unmatched speed, precision, and
                    security.</strong> It’s not a side service or an add-on.
                    It’s the core of everything we do, and it’s what has made us
                    a trusted leader in the industry for more than <strong data-start="574" data-end="586">25
                    years</strong>.</p>
                    <p data-start="591" data-end="1045">While other companies may
                    offer a variety of unrelated services, our expertise is
                    laser-focused on the secure retrieval and delivery of <strong data-start="727" data-end="777">IRS
                    and Social Security Administration records</strong>. That
                    singular focus means our systems, processes, and people are all
                    built around one goal — to deliver accurate results <strong data-start="900" data-end="908">fast</strong>,
                    every time. Our clients have come to expect that level of
                    reliability and excellence, and we continue to deliver it
                    without compromise.</p>
                    <h3 data-start="1047" data-end="1094"><strong data-start="1051" data-end="1094">Speed,
                    Accuracy, and Proven Performance</strong></h3>
                    <p data-start="1095" data-end="1483">We understand that in the
                    lending and financial industries, time is critical. Our advanced
                    automation and direct agency integration allow us to retrieve
                    IRS transcripts and SSA verifications <strong data-start="1286" data-end="1316">faster
                    and more accurately</strong> than traditional providers. Every
                    record we deliver is validated, encrypted, and transmitted
                    through secure channels that meet or exceed Treasury and IRS
                    standards.</p>
                    <p data-start="1485" data-end="1669">Our clients rely on us
                    because they know we consistently deliver what others cannot —
                    verified government records, processed swiftly, securely, and
                    with meticulous attention to detail.</p>
                    <h3 data-start="1671" data-end="1726"><strong data-start="1675" data-end="1726">Recognized
                    by the IRS. Trusted by the Industry.</strong></h3>
                    <p data-start="1727" data-end="2295">Our credibility goes far
                    beyond our client base. <strong data-start="1776" data-end="1961">Key
                    principals of IRS Tax Records have been personally selected by
                    IRS officials to participate in IRS beta programs and the IVES
                    (Income Verification Express Service) Working Group</strong> —
                    a distinction given to only a few in our industry.<br data-start="2014" data-end="2017">
                    This ongoing collaboration allows us to provide direct feedback,
                    share insight, and help shape the future of the IRS’s
                    verification systems. It also means our clients benefit from our
                    inside understanding of the process — <strong data-start="2239" data-end="2295">we
                    know how it works because we help make it better.</strong></p>
                    <h3 data-start="2297" data-end="2333"><strong data-start="2301" data-end="2333">Expertise
                    That Sets Us Apart</strong></h3>
                    <p data-start="2334" data-end="2684">Our deep experience and
                    government relationships allow us to stay ahead of every
                    regulatory, procedural, and technical change. When the IRS
                    updates its systems, we’re ready — often before the changes
                    go live. That kind of foresight and preparedness translates
                    directly into faster service, smoother processing, and greater
                    confidence for our clients.</p>
                    <h3 data-start="2686" data-end="2721"><strong data-start="2690" data-end="2721">Focused.
                    Proven. Respected.</strong></h3>
                    <p data-start="2722" data-end="2940">For over two decades, <strong data-start="2744" data-end="2763">IRStaxRecords.com</strong> has been the name professionals trust for
                    government record retrieval. We’re proud to be unique — not
                    because of what we do differently, but because of what we do <strong data-start="2927" data-end="2937">better</strong>.</p>
                    <p data-start="2942" data-end="2999"><strong data-start="2942" data-end="2999">IRStaxRecords.com–Government Records. It’s All We Do.</strong></p>
                    &nbsp;
                  </div>
                </div>
              </td>
            </tr>

            <!-- Row 7 -->
            <tr class="data" id="row-price">
              <td>What is the price for your service?</td>
              <td></td>
              <td></td>
              <td>
                <button class="lm-btn" type="button" aria-expanded="false" aria-controls="d-price" id="t-price">
                  Learn more
                </button>
              </td>
            </tr>
            <tr class="details" aria-hidden="true">
              <td colspan="4">
                <div class="panel" id="d-price" role="region" aria-labelledby="t-price">
                  <div class="panel-inner">
                    <h2 data-start="149" data-end="202"><strong data-start="152" data-end="202">Simple,
                    Transparent Pricing — Built Around You</strong></h2>
                    <p data-start="204" data-end="527">At <strong data-start="207" data-end="226">IRStaxRecords.com</strong>, our pricing is designed to be <strong data-start="258" data-end="304">fair,
                    flexible, and tailored to your needs</strong>. Whether you
                    process a few requests a month or thousands each week, our
                    pricing structure is based on <strong data-start="407" data-end="440">volume
                    and ordering frequency</strong>, ensuring you receive the best
                    possible rate for your organization’s activity level.</p>
                    <p data-start="529" data-end="859">We understand that every
                    client operates differently — that’s why our approach is not
                    one-size-fits-all. High-volume lenders, banks, and financial
                    institutions benefit from <strong data-start="702" data-end="729">preferred
                    pricing tiers</strong>, while smaller firms enjoy competitive
                    rates with the same high level of service, accuracy, and
                    security that define our brand.</p>
                    <h3 data-start="861" data-end="897"><strong data-start="865" data-end="897">Convenient
                    Invoicing Options</strong></h3>
                    <p data-start="898" data-end="966">We make billing simple and
                    convenient. Clients can choose between:</p>
                    <ul data-start="967" data-end="1487">
                      <li data-start="967" data-end="1132">
                        <p data-start="969" data-end="1132"><strong data-start="969" data-end="993">Electronic
                        Invoices:</strong> Delivered securely inside your <strong data-start="1025" data-end="1052">administrator’s
                        account</strong>, where all <strong data-start="1064" data-end="1093">current
                        and past invoices</strong> can be easily viewed and
                        downloaded.</p>
                      </li>
                      <li data-start="1133" data-end="1263">
                        <p data-start="1135" data-end="1263"><strong data-start="1135" data-end="1154">Paper
                        Invoices:</strong> For those who prefer traditional methods,
                        we also provide <strong data-start="1213" data-end="1247">printed
                        invoices via U.S. Mail</strong> upon request.</p>
                      </li>
                      <li data-start="1264" data-end="1487">
                        <p data-start="1266" data-end="1487"><strong data-start="1266" data-end="1289">Excel
                        Spreadsheets:</strong> For easy accounting integration and
                        reconciliation, we provide <strong data-start="1353" data-end="1384">Excel
                        spreadsheet summaries</strong> of all invoices — complete
                        with your company’s <strong data-start="1432" data-end="1448">loan
                        numbers</strong> for accurate, line-by-line tracking.</p>
                      </li>
                    </ul>
                    <h3 data-start="1489" data-end="1531"><strong data-start="1493" data-end="1531">Ease
                    of Use and Total Transparency</strong></h3>
                    <p data-start="1532" data-end="1817">Your administrator
                    dashboard offers complete visibility into your billing history
                    — no more searching through emails or waiting for statements.
                    With just a few clicks, you can view, print, or download
                    invoices, export Excel files, and track every transaction in one
                    convenient place.</p>
                    <p data-start="1819" data-end="1967">We believe pricing should
                    never be complicated — it should be clear, consistent, and
                    backed by the same reliability that defines everything we do. There are no license fees. There is no Initial or Annual contract or fee.</p>
                    <p data-start="1969" data-end="2048"><strong data-start="1969" data-end="2048">IRStaxRecords.com–Transparent Pricing. Simple Billing. Complete
                    Confidence.</strong></p>
                    &nbsp;
                  </div>
                </div>
              </td>
            </tr>

            <!-- Row 8 -->
            <tr class="data" id="row-eight">
              <td>Compliance made easy</td>
              <td></td>
              <td></td>
              <td>
                <button class="lm-btn" type="button" aria-expanded="false" aria-controls="d-eight" id="t-eight">
                  Learn more
                </button>
              </td>
            </tr>
            <tr class="details" aria-hidden="true">
              <td colspan="4">
                <div class="panel" id="d-eight" role="region" aria-labelledby="t-eight">
                  <div class="panel-inner">
                    <h2 data-start="267" data-end="332"><strong data-start="270" data-end="332">Compliance
                    Made Easy for the Mortgage and Banking Industry</strong></h2>
                    <p data-start="334" data-end="678">In today’s lending
                    environment, <strong data-start="366" data-end="413">compliance
                    isn’t optional — it’s essential.</strong> At <strong data-start="417" data-end="436">IRS
                    Tax Records</strong>, we provide the official government
                    documents and verification services that help banks, credit
                    unions, and mortgage lenders <strong data-start="562" data-end="625">stay
                    fully compliant with federal and investor requirements</strong>
                    — quickly, securely, and with complete confidence.</p>
                    <h3 data-start="680" data-end="733"><strong data-start="684" data-end="733">Supporting
                    Every Layer of Risk and Compliance</strong></h3>
                    <p data-start="734" data-end="944">From origination to closing,
                    every loan must meet strict verification standards designed to
                    protect borrowers, lenders, and investors alike. Our services
                    are built to address these critical compliance points:</p>
                    <ul data-start="946" data-end="1955">
                      <li data-start="946" data-end="1211">
                        <p data-start="948" data-end="1211"><strong data-start="948" data-end="972">Income
                        Verification:</strong> We provide <strong data-start="984" data-end="1016">official
                        IRS tax transcripts</strong> through authorized IRS
                        channels, ensuring lenders can validate borrower income
                        directly from the source. This eliminates guesswork and
                        supports regulatory and investor documentation standards.</p>
                      </li>
                      <li data-start="1212" data-end="1449">
                        <p data-start="1214" data-end="1449"><strong data-start="1214" data-end="1240">Identity
                        Verification:</strong> Our <strong data-start="1245" data-end="1292">SSA-89
                        Social Security Verification Service</strong> confirms
                        borrower identity directly with the Social Security
                        Administration, ensuring that every name and number
                        combination is legitimate and verifiable.</p>
                      </li>
                      <li data-start="1450" data-end="1729">
                        <p data-start="1452" data-end="1729"><strong data-start="1452" data-end="1478">Early
                        Fraud Detection:</strong> By identifying mismatched income
                        data or invalid Social Security information before a file
                        reaches underwriting, our system helps institutions detect
                        fraud early — protecting your organization from costly
                        buybacks, penalties, and reputational risk.</p>
                      </li>
                      <li data-start="1730" data-end="1955">
                        <p data-start="1732" data-end="1955"><strong data-start="1732" data-end="1776">Validity
                        of Social Security Information:</strong> Our rapid SSA
                        “match/no-match” response, often within <strong data-start="1831" data-end="1845">15
                        minutes</strong>, provides immediate clarity and peace of
                        mind to lenders who must confirm identity validity before
                        funding.</p>
                      </li>
                    </ul>
                    <h3 data-start="1957" data-end="1998"><strong data-start="1961" data-end="1998">Fast,
                    Secure, and Fully Compliant</strong></h3>
                    <p data-start="1999" data-end="2478">We understand that
                    compliance isn’t just about having the right data — it’s
                    about having it <strong data-start="2091" data-end="2127">on
                    time and properly documented.</strong> That’s why our systems
                    are engineered to deliver government-verified transcripts and
                    verification certificates <strong data-start="2239" data-end="2266">within
                    hours of request</strong>. Every document is encrypted,
                    timestamped, and stored in accordance with <strong data-start="2340" data-end="2386">IRS
                    and Treasury data-protection protocols</strong>, giving your
                    compliance team a clear audit trail and full confidence in every
                    verification.</p>
                    <h3 data-start="2480" data-end="2523"><strong data-start="2484" data-end="2523">Reducing
                    Risk. Strengthening Trust.</strong></h3>
                    <p data-start="2524" data-end="2877">Regulators, investors, and
                    secondary markets demand verifiable proof of borrower identity
                    and income. Our services not only help lenders meet those
                    requirements — they help build trust. By working directly with
                    the IRS and SSA, <strong data-start="2752" data-end="2771">IRS
                    Tax Records</strong> ensures that every record you receive is
                    accurate, defensible, and backed by federal source validation.</p>
                    <h3 data-start="2879" data-end="2931"><strong data-start="2883" data-end="2931">Your
                    Compliance Partner in Every Transaction</strong></h3>
                    <p data-start="2932" data-end="3248">For more than <strong data-start="2946" data-end="2958">25
                    years</strong>, we’ve helped lenders maintain the highest
                    standards of risk management and compliance while streamlining
                    their verification workflows. Our technology, speed, and deep
                    regulatory knowledge make compliance <strong data-start="3164" data-end="3194">fast,
                    simple, and reliable</strong> — exactly what today’s
                    financial institutions need.</p>
                    <p data-start="3250" data-end="3315"><strong data-start="3250" data-end="3315">IRS
                    Tax Records – The Trusted Source for Verified Compliance.</strong></p>
                    &nbsp;
                  </div>
                </div>
              </td>
            </tr>

            <!-- Row 9 -->
            <tr class="data" id="row-nine">
              <td>Transparency and Reporting, Administrative Dashboards, Effortless
                Record Retrieval&nbsp;</td>
              <td></td>
              <td></td>
              <td>
                <button class="lm-btn" type="button" aria-expanded="false" aria-controls="d-nine" id="t-nine">
                  Learn more
                </button>
              </td>
            </tr>
            <tr class="details" aria-hidden="true">
              <td colspan="4">
                <div class="panel" id="d-nine" role="region" aria-labelledby="t-nine">
                  <div class="panel-inner">
                    <h2 data-start="126" data-end="177"><strong data-start="129" data-end="177">Transparency
                    and Reporting is essential</strong></h2>
                    <p data-start="179" data-end="440">When time is short and
                    transcripts are critical, <strong data-start="228" data-end="247">IRS
                    Tax Records</strong>  delivers.<br data-start="257" data-end="260">
                    </p>
                    <h3 data-start="131" data-end="163"><strong data-start="135" data-end="163">Transparency
                    &amp; Reporting</strong></h3>
                    <p data-start="165" data-end="479">Banks and mortgage lenders
                    operate in a world where every action must be traceable and
                    verifiable — and that’s exactly what IRSTaxRecords.com
                    delivers.<br data-start="316" data-end="319">
                    We believe that transparency is the foundation of trust, and our
                    systems are designed to make every transaction fully visible,
                    auditable, and easy to reconcile.</p>
                    <h4 data-start="481" data-end="517"><strong data-start="486" data-end="517">Audit
                    Logs &amp; Order Tracking</strong></h4>
                    <p data-start="518" data-end="759">Every order, upload, and
                    retrieval is time-stamped and logged automatically.
                    Administrators can easily track the status of requests, from
                    form submission to transcript delivery — creating a complete
                    chain of custody for each borrower record.</p>
                    <h4 data-start="761" data-end="790"><strong data-start="766" data-end="790">Invoice
                    Transparency</strong></h4>
                    <p data-start="791" data-end="1064">Clients benefit from clear,
                    itemized invoicing. Each invoice can be viewed online,
                    downloaded as a PDF, or exported into a fully formatted <strong data-start="930" data-end="951">Excel
                    spreadsheet</strong> — a powerful feature that simplifies
                    reconciliation and allows direct integration into your
                    accounting workflow.</p>
                    <h4 data-start="1066" data-end="1100"><strong data-start="1071" data-end="1100">Administrative
                    Dashboards</strong></h4>
                    <p data-start="1101" data-end="1320">Our secure administrative
                    dashboards give compliance officers and auditors real-time
                    oversight. Quickly review account activity, monitor user access,
                    or download audit summaries — all from a single, intuitive
                    interface.</p>
                    <h4 data-start="1322" data-end="1358"><strong data-start="1327" data-end="1358">Effortless
                    Record Retrieval</strong></h4>
                    <p data-start="1359" data-end="1593">Whether for a regulator,
                    internal audit, or external examiner, retrieving records is fast
                    and straightforward. All documents are organized, searchable,
                    and exportable, ensuring you can provide supporting data
                    instantly when requested.</p>
                    &nbsp;
                  </div>
                </div>
              </td>
            </tr>

            <!-- Row 10 -->
            <tr class="data" id="row-ten">
              <td>Integration, Ease of Use and Technology with a Human touch</td>
              <td></td>
              <td></td>
              <td>
                <button class="lm-btn" type="button" aria-expanded="false" aria-controls="d-nine" id="t-ten">
                  Learn more
                </button>
              </td>
            </tr>
            <tr class="details" aria-hidden="true">
              <td colspan="4">
                <div class="panel" id="d-ten" role="region" aria-labelledby="t-ten">
                  <div class="panel-inner">
                    <h3 data-start="195" data-end="228"><strong data-start="199" data-end="228">Integration,
                    Ease of Use and Technology with a human touch</strong></h3>
                    <p data-start="230" data-end="514">At <strong data-start="233" data-end="254">IRSTaxRecords.com</strong>,
                    we believe that technology should make your work easier — not
                    more complicated. That’s why we’ve built every part of our
                    system around your workflow, combining powerful automation with
                    the kind of personal service that’s increasingly rare in
                    today’s world.</p>
                    <h4 data-start="516" data-end="549"><strong data-start="521" data-end="549">Web-Based
                    Secure Portals</strong></h4>
                    <p data-start="550" data-end="803">Our web-based system requires
                    no software installation or IT support — simply log in from
                    any modern browser to order, track, and retrieve your
                    transcripts or certifications securely. It’s simple, fast, and
                    fully compliant with IRS and SSA requirements.</p>
                    <h4 data-start="805" data-end="854"><strong data-start="810" data-end="854">Built
                    for Real People — With Real People</strong></h4>
                    <p data-start="855" data-end="1219">You’ll never get lost in a
                    phone tree. When you call <strong data-start="908" data-end="929">IRSTaxRecords.com</strong>,
                    you’ll always reach a real, live person ready to help — no
                    “press 1 for this” or “press 2 for that.” Each client is
                    assigned a dedicated <strong data-start="1068" data-end="1094">account
                    representative</strong> who understands your business, assists
                    with onboarding, and ensures that your questions are answered
                    quickly and personally.</p>
                    <h4 data-start="1221" data-end="1264"><strong data-start="1226" data-end="1264">Easy
                    Group Setup &amp; User Management</strong></h4>
                    <p data-start="1265" data-end="1560">Whether you’re adding a
                    few users or rolling out access across multiple departments or
                    branches, setting up large groups is effortless. Administrators
                    can create accounts, assign permissions, and manage user access
                    with just a few clicks. We make it easy to keep your teams
                    organized and secure.</p>
                    <h4 data-start="1562" data-end="1600"><strong data-start="1567" data-end="1600">OrderPad
                    &amp; Automation Options</strong></h4>
                    <p data-start="1601" data-end="1908">For clients who prefer
                    automation, our <strong data-start="1640" data-end="1652">OrderPad</strong>
                    desktop icon provides one-click, drag-and-drop ordering —
                    securely sending PDF forms to our servers without any login or
                    typing. Larger organizations can also integrate through our API
                    or secure upload automation for direct system-to-system
                    communication.</p>
                    <h4 data-start="1910" data-end="1942"><strong data-start="1915" data-end="1942">User-Friendly
                    by Design</strong></h4>
                    <p data-start="1943" data-end="2321">Every aspect of our
                    platform — from uploading forms to retrieving IRS transcripts
                    or SSA certifications — is designed for speed, clarity, and
                    minimal effort. With just a few clicks, you can place an order,
                    track its progress, and download completed results. We built our
                    systems with the end user in mind, ensuring that everything
                    works the way you expect — simply and reliably.</p>
                    <hr data-start="2323" data-end="2326">
                    <h3 data-start="2328" data-end="2365"><strong data-start="2332" data-end="2365">Technology
                    with a Human Touch</strong></h3>
                    <p data-start="2367" data-end="2664">While our technology leads
                    the industry in speed and security, it’s our people who truly
                    set us apart. From onboarding to daily support, you’ll have a
                    dedicated contact who knows your team, your processes, and your
                    priorities. It’s modern technology — delivered with
                    old-fashioned customer care.</p>
                    <p data-start="2666" data-end="2795">That’s the <strong data-start="2677" data-end="2698">IRSTaxRecords.com</strong>
                    difference: smart systems, personal service, and seamless
                    integration built entirely around you.
                  </div>
                </div>
              </td>
            </tr>

          </tbody>
        </table>
      </div>
    </div>
  </div>
          <script>
              const buttons = document.querySelectorAll('.lm-btn');
              const singleOpen = true;

              function setPanelHeight(panel, open) {
                  panel.style.height = open ? panel.firstElementChild.scrollHeight + 'px' : '0px';
              }
              function closeAll() {
                  document.querySelectorAll('.lm-btn[aria-expanded="true"]').forEach(b => {
                      b.setAttribute('aria-expanded', 'false');
                      const p = document.getElementById(b.getAttribute('aria-controls'));
                      p.closest('tr.details').setAttribute('aria-hidden', 'true');
                      setPanelHeight(p, false);
                  });
              }

              buttons.forEach(btn => {
                  const panel = document.getElementById(btn.getAttribute('aria-controls'));
                  const detailsRow = panel.closest('tr.details');

                  btn.addEventListener('click', e => {
                      e.preventDefault();
                      const isOpen = btn.getAttribute('aria-expanded') === 'true';
                      if (singleOpen && !isOpen) closeAll();
                      btn.setAttribute('aria-expanded', String(!isOpen));
                      detailsRow.setAttribute('aria-hidden', String(isOpen));
                      setPanelHeight(panel, !isOpen);
                  });

                  window.addEventListener('resize', () => {
                      if (btn.getAttribute('aria-expanded') === 'true') setPanelHeight(panel, true);
                  });
              });
          </script>
</asp:Content>
