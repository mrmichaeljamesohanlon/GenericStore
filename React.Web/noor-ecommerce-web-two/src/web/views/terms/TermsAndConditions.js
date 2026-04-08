import React from 'react';
import { Helmet } from 'react-helmet-async';
import SiteBreadcrumb from '../../components/layout/SiteBreadcrumb';
import { Container, Row, Col } from 'reactstrap';


const TermsAndConditions = () => (
  <>
    <Helmet>
      <title>Terms and Conditions – Generic Store</title>
      <meta name="description" content="Terms and Conditions for Generic Store" />
    </Helmet>
    <SiteBreadcrumb title="Terms and Conditions" parent="Home" />
    <section className="terms-page section-big-py-space">
      <Container>
        <Row>
          <Col lg="12">
            <h2>Terms and Conditions – Generic Store</h2>
            <h4>1. Introduction</h4>
            <p>Welcome to the Generic Store website. By browsing and using this website you agree to comply with and be bound by the following Terms and Conditions of use, together with our Privacy Policy, which govern Generic Store's relationship with you in relation to this website.</p>
            <p><b>In these Terms:</b></p>
            <ul>
              <li>"Generic Store", "we", "us" means the owner of the website;</li>
              <li>"you" means the user or viewer of the website.</li>
            </ul>
            <p><b>Our contact details are:</b><br />
              Generic Store<br />
              Your Address Line 1,<br />
              Your Address Line 2,<br />
              Your City,<br />
              Your Country<br />
              Phone: Your Phone Number<br />
              Email: <a href="mailto:contact@yourstore.com">contact@yourstore.com</a>
            </p>
            <h4>2. Use of the Website</h4>
            <ul>
              <li>2.1 The content of the pages of this website is for general information and use only. It may change without notice.</li>
              <li>2.2 We make no warranty or guarantee as to the accuracy, timeliness, performance, completeness, or suitability of the information and materials found on this website for any particular purpose.</li>
            </ul>
            <h4>3. No Liability for Content</h4>
            <ul>
              <li>3.1 You acknowledge that such information may contain inaccuracies or errors, and Generic Store expressly excludes liability for any such inaccuracies to the fullest extent permitted by law.</li>
              <li>3.2 Your use of any information or materials on this website is entirely at your own risk.</li>
            </ul>
            <h4>4. Intellectual Property</h4>
            <ul>
              <li>4.1 This website contains material which is owned by or licensed to Generic Store. This includes, but is not limited to, the design, layout, look, and graphics.</li>
              <li>4.2 Reproduction is prohibited other than in accordance with the copyright notice.</li>
            </ul>
            <h4>5. Links to Other Websites</h4>
            <ul>
              <li>5.1 From time to time this website may include links to other websites. These links are provided for convenience and do not signify endorsement.</li>
              <li>5.2 We have no responsibility for the content of linked websites.</li>
            </ul>
            <h4>6. Orders and Services</h4>
            <ul>
              <li>6.1 Any orders for products or services made through this website or via email/phone are subject to acceptance by Generic Store.</li>
              <li>6.2 Generic Store reserves the right to refuse or cancel orders at its discretion.</li>
            </ul>
            <h4>7. Payments</h4>
            <ul>
              <li>7.1 Payment must be received by the due date specified on invoices or order confirmations.</li>
              <li>7.2 Full payment is required before goods are released.</li>
            </ul>
            <h4>8. Delivery and Risk</h4>
            <ul>
              <li>8.1 Where goods are delivered, risk passes on delivery, but ownership remains with Generic Store until full payment is received.</li>
              <li>8.2 You shall hold the goods as bailee for Generic Store and keep them free from charge, lien or encumbrance until paid in full.</li>
            </ul>
            <h4>9. Returns and Cancellations</h4>
            <ul>
              <li>9.1 Goods supplied correctly to order may only be returned for credit with prior written agreement. A handling charge may apply.</li>
              <li>9.2 Cancellation of orders must be made in writing and may be subject to cancellation fees.</li>
            </ul>
            <h4>10. Liability</h4>
            <ul>
              <li>10.1 Generic Store cannot be held responsible for contingencies beyond our control such as strikes, shipping delays, fire, war, or other events outside reasonable control.</li>
            </ul>
            <h4>11. Governing Law</h4>
            <ul>
              <li>11.1 These Terms and Conditions are governed by applicable law and any disputes arising will be determined by the relevant courts.</li>
            </ul>
          </Col>
        </Row>
      </Container>
    </section>
  </>
);

export default TermsAndConditions;
