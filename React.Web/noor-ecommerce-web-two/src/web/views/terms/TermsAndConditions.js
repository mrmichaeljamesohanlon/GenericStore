import React from 'react';
import { Helmet } from 'react-helmet-async';
import SiteBreadcrumb from '../../components/layout/SiteBreadcrumb';
import { Container, Row, Col } from 'reactstrap';


const TermsAndConditions = () => (
  <>
    <Helmet>
      <title>Terms and Conditions – Spark Developments</title>
      <meta name="description" content="Terms and Conditions for Spark Developments" />
    </Helmet>
    <SiteBreadcrumb title="Terms and Conditions" parent="Home" />
    <section className="terms-page section-big-py-space">
      <Container>
        <Row>
          <Col lg="12">
            <h2>Terms and Conditions – Spark Developments</h2>
            <h4>1. Introduction</h4>
            <p>Welcome to the Spark Developments website. By browsing and using this website you agree to comply with and be bound by the following Terms and Conditions of use, together with our Privacy Policy, which govern Spark Developments’ relationship with you in relation to this website.</p>
            <p><b>In these Terms:</b></p>
            <ul>
              <li>“Spark Developments”, “we”, “us” means the owner of the website;</li>
              <li>“you” means the user or viewer of the website.</li>
            </ul>
            <p><b>Our contact details are:</b><br />
              Spark Developments<br />
              Unit 10, Meden Road,<br />
              Boughton,<br />
              Newark,<br />
              Nottinghamshire,<br />
              England NG22 9ZD<br />
              Phone: 07516 783 331<br />
              Email: <a href="mailto:jamie@sparkdevelopments.co.uk">jamie@sparkdevelopments.co.uk</a>
            </p>
            <h4>2. Use of the Website</h4>
            <ul>
              <li>2.1 The content of the pages of this website is for general information and use only. It may change without notice.</li>
              <li>2.2 We make no warranty or guarantee as to the accuracy, timeliness, performance, completeness, or suitability of the information and materials found on this website for any particular purpose.</li>
            </ul>
            <h4>3. No Liability for Content</h4>
            <ul>
              <li>3.1 You acknowledge that such information may contain inaccuracies or errors, and Spark Developments expressly excludes liability for any such inaccuracies to the fullest extent permitted by law.</li>
              <li>3.2 Your use of any information or materials on this website is entirely at your own risk.</li>
            </ul>
            <h4>4. Intellectual Property</h4>
            <ul>
              <li>4.1 This website contains material which is owned by or licensed to Spark Developments. This includes, but is not limited to, the design, layout, look, and graphics.</li>
              <li>4.2 Reproduction is prohibited other than in accordance with the copyright notice.</li>
            </ul>
            <h4>5. Links to Other Websites</h4>
            <ul>
              <li>5.1 From time to time this website may include links to other websites. These links are provided for convenience and do not signify endorsement.</li>
              <li>5.2 We have no responsibility for the content of linked websites.</li>
            </ul>
            <h4>6. Orders and Booking Services</h4>
            <ul>
              <li>6.1 Any bookings for services, hire, workshop work, or parts made through this website or via email/phone are subject to acceptance by Spark Developments.</li>
              <li>6.2 Spark Developments reserves the right to refuse or cancel services at its discretion, including for safety or compliance reasons.</li>
            </ul>
            <h4>7. Payments and Credit Terms</h4>
            <ul>
              <li>7.1 Payment must be received by the due date specified on invoices or booking confirmations.</li>
              <li>7.2 For customers without a credit account, full payment is required before work commences or goods are released.</li>
            </ul>
            <h4>8. Delivery and Risk</h4>
            <ul>
              <li>8.1 Where goods are delivered, risk passes on delivery, but ownership remains with Spark Developments until full payment is received.</li>
              <li>8.2 You shall hold the goods as bailee for Spark Developments and keep them free from charge, lien or encumbrance until paid in full.</li>
            </ul>
            <h4>9. Returns and Cancellations</h4>
            <ul>
              <li>9.1 Goods supplied correctly to order may only be returned for credit with prior written agreement. A handling charge may apply.</li>
              <li>9.2 Cancellation of services or bookings must be made in writing and may be subject to cancellation fees.</li>
            </ul>
            <h4>10. Liability</h4>
            <ul>
              <li>10.1 Spark Developments cannot be held responsible for contingencies beyond our control such as strikes, shipping delays, fire, war, or other events outside reasonable control.</li>
            </ul>
            <h4>11. Governing Law</h4>
            <ul>
              <li>11.1 These Terms and Conditions are governed by the laws of England and Wales and any disputes arising will be determined by the English Courts.</li>
            </ul>
          </Col>
        </Row>
      </Container>
    </section>
  </>
);

export default TermsAndConditions;
