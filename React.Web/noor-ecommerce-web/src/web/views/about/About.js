import React, { useEffect, useState } from 'react';
import { Helmet } from 'react-helmet-async';
import Config from '../../../helpers/Config';
import SiteBreadcrumb from '../../components/layout/SiteBreadcrumb';
import BestFacilities from '../../components/shared/BestFacilities';
import CustomerTestimonial from '../../components/shared/CustomerTestimonial';
import about1 from '../../resources/themeContent/images/theme-images/about1.jpg';
import about2 from '../../resources/themeContent/images/theme-images/about2.jpg';
import signature from '../../resources/themeContent/images/theme-images/signature.png';


const About = () => {
  const [siteTitle, setSiteTitle] = useState(Config['SITE_TTILE']);

  return (
    <>


      <Helmet>
        <title>{siteTitle} - About Us</title>
        <meta name="description" content={siteTitle + " - About us page"}  />
        <meta name="keywords" content="About us"></meta>
      </Helmet>

      <SiteBreadcrumb title="About Us" />

      <section className="about-area ptb-60">
        <div className="container">
          <div className="row align-items-center">
            <div className="col-lg-6 col-md-12">
              <div className="about-content">
                <h2>About Spark Developments</h2>
                <p>Spark Developments is your premier destination for motorsport solutions. We pride ourselves on the Spark Standard – a promise to provide results-driven service at all times, whether in the workshop, on the rally stage, or on the race track.</p>

                <p>We are competitors ourselves – we know what it takes to finish, and to win. When you work with Spark Developments, you join our team. Our approach is professional, friendly, and completely transparent with no hidden costs.</p>

                <p>From car hire packages to full workshop services, we treat every vehicle with the same care and attention as our own. Honest, upfront and flexible – that is the Spark Standard.</p>

                <div className="signature mb-0">
                  <img src={signature} alt="image" />
                </div>
              </div>
            </div>

            <div className="col-lg-6 col-md-12">
              <div className="about-image">
                <img src={about1} className="about-img1" alt="image" />
                <img src={about2} className="about-img2" alt="image" />
              </div>
            </div>
          </div>
        </div>
      </section>

      <section className="about-area ptb-60 bg-light">
        <div className="container">
          <div className="row">
            <div className="col-md-4 mb-3">
              <img src="https://www.sparkdevelopments.co.uk/assets/images/Why1.jpg" alt="Spark Developments motorsport team" className="img-fluid" />
            </div>
            <div className="col-md-4 mb-3">
              <img src="https://www.sparkdevelopments.co.uk/assets/images/Why2.jpg" alt="Spark Developments rally car" className="img-fluid" />
            </div>
            <div className="col-md-4 mb-3">
              <img src="https://www.sparkdevelopments.co.uk/assets/images/Why3.jpg" alt="Spark Developments workshop" className="img-fluid" />
            </div>
          </div>
        </div>
      </section>

      <CustomerTestimonial />

      <BestFacilities />

    </>
  );

}

export default About;
