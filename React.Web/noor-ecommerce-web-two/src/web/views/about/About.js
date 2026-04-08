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
        <meta name="description" content={siteTitle + " - About Spark Developments"}  />
        <meta name="keywords" content="About Spark Developments, Why Spark, Spark Standard, Motorsport, Car Hire, Workshop"></meta>
      </Helmet>
      <SiteBreadcrumb title="About" parent="Home"/>
      <section className="about-page section-big-py-space">
        <div className="custom-container">
          <Row>
            <Col md="4" className="mb-3">
              <img src="https://www.sparkdevelopments.co.uk/assets/images/Why1.jpg" alt="Spark Developments motorsport team" className="img-fluid" />
            </Col>
            <Col md="4" className="mb-3">
              <img src="https://www.sparkdevelopments.co.uk/assets/images/Why2.jpg" alt="Spark Developments rally car" className="img-fluid" />
            </Col>
            <Col md="4" className="mb-3">
              <img src="https://www.sparkdevelopments.co.uk/assets/images/Why3.jpg" alt="Spark Developments workshop" className="img-fluid" />
            </Col>
          </Row>
          <h4 className="mb-4">Our Core Services</h4>
          <Row className="mb-5">
            <Col lg="3" sm="6" className="mb-4">
              <img src="https://www.sparkdevelopments.co.uk/assets/images/demoes/demo11/about/office-1.jpg" alt="Car Hire" className="img-fluid mb-2" />
              <p>Join the Spark motorsport family - find out how our hire packages take the hassle out of competing.</p>
            </Col>
            <Col lg="3" sm="6" className="mb-4">
              <img src="https://www.sparkdevelopments.co.uk/assets/images/demoes/demo11/about/office-2.jpg" alt="Standard" className="img-fluid mb-2" />
              <p>Put the Spark Standard into your motorsport - we can build you a car to your specification and budget.</p>
            </Col>
            <Col lg="3" sm="6" className="mb-4">
              <img src="https://www.sparkdevelopments.co.uk/assets/images/demoes/demo11/about/office-3.jpg" alt="Socials" className="img-fluid mb-2" />
              <p>Get the latest Spark news! Sign up to get the latest updates and follow Spark Developments on social media.<br /><a href="https://www.facebook.com/SparkDevelopments/" target="_blank" rel="noopener noreferrer">FOLLOW US ON FACEBOOK</a></p>
            </Col>
            <Col lg="3" sm="6" className="mb-4">
              <img src="https://www.sparkdevelopments.co.uk/assets/images/demoes/demo11/about/office-4.jpg" alt="Workshop" className="img-fluid mb-2" />
              <p>When details matter - The Spark Standard is at the heart of our workshop services.<br /><a href="https://www.sparkdevelopments.co.uk/Home/Workshop" target="_blank" rel="noopener noreferrer">VISIT OUR WORKSHOP</a></p>
            </Col>
          </Row>
          <h4 className="mb-3">Contact Info</h4>
          <ul>
            <li><b>Address:</b> Spark Developments, Unit 10, Meden Road, Boughton, Newark, Nottinghamshire, England NG22 9ZD</li>
            <li><b>Phone:</b> <a href="tel:07516783331">07516 783331</a></li>
            <li><b>Email:</b> <a href="mailto:jamie@sparkdevelopments.co.uk">jamie@sparkdevelopments.co.uk</a></li>
          </ul>
        </div>
      </section>
    </>
  );
}

export default About;
