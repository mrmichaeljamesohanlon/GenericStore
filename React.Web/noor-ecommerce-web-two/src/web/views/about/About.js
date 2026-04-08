import React, { useEffect, useState } from 'react';
import { Helmet } from 'react-helmet-async';
import Config from '../../../helpers/Config';
import SiteBreadcrumb from '../../components/layout/SiteBreadcrumb';
import { Row, Col } from "reactstrap";


const About = () => {
  const [siteTitle] = useState(Config['SITE_TTILE']);
  return (
    <>
      <Helmet>
        <title>{siteTitle} - About Us</title>
        <meta name="description" content={siteTitle + " - About Us"}  />
        <meta name="keywords" content="About Us, Our Store, Products, Shopping"></meta>
      </Helmet>
      <SiteBreadcrumb title="About" parent="Home"/>
      <section className="about-page section-big-py-space">
        <div className="custom-container">
          <Row>
            <Col md="4" className="mb-3">
              <img src="https://placehold.co/600x400/cccccc/666666?text=Store+Image+1" alt="Our store" className="img-fluid" />
            </Col>
            <Col md="4" className="mb-3">
              <img src="https://placehold.co/600x400/cccccc/666666?text=Store+Image+2" alt="Our products" className="img-fluid" />
            </Col>
            <Col md="4" className="mb-3">
              <img src="https://placehold.co/600x400/cccccc/666666?text=Store+Image+3" alt="Our team" className="img-fluid" />
            </Col>
          </Row>
          <h4 className="mb-4">Our Core Services</h4>
          <Row className="mb-5">
            <Col lg="3" sm="6" className="mb-4">
              <img src="https://placehold.co/400x300/cccccc/666666?text=Service+1" alt="Service 1" className="img-fluid mb-2" />
              <p>Discover our wide range of products carefully selected to meet your everyday needs and beyond.</p>
            </Col>
            <Col lg="3" sm="6" className="mb-4">
              <img src="https://placehold.co/400x300/cccccc/666666?text=Service+2" alt="Service 2" className="img-fluid mb-2" />
              <p>Quality you can trust – every item in our store meets our high standards for value and reliability.</p>
            </Col>
            <Col lg="3" sm="6" className="mb-4">
              <img src="https://placehold.co/400x300/cccccc/666666?text=Service+3" alt="Service 3" className="img-fluid mb-2" />
              <p>Stay up to date with our latest arrivals and exclusive deals. Sign up to our newsletter for updates.</p>
            </Col>
            <Col lg="3" sm="6" className="mb-4">
              <img src="https://placehold.co/400x300/cccccc/666666?text=Service+4" alt="Service 4" className="img-fluid mb-2" />
              <p>Fast, reliable delivery and hassle-free returns – shopping with us is always a great experience.</p>
            </Col>
          </Row>
          <h4 className="mb-3">Contact Info</h4>
          <ul>
            <li><b>Address:</b> Your Store Name, Your Address Line 1, Your City, Your Country</li>
            <li><b>Phone:</b> <a href="tel:0000000000">Your Phone Number</a></li>
            <li><b>Email:</b> <a href="mailto:contact@yourstore.com">contact@yourstore.com</a></li>
          </ul>
        </div>
      </section>
    </>
  );
}

export default About;
