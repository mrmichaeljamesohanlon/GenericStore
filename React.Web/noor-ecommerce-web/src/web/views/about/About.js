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
                <h2>About Generic Store</h2>
                <p>Welcome to Generic Store – your go-to destination for quality products at great prices. We are committed to providing an outstanding shopping experience with a wide selection of items to suit every need and budget.</p>

                <p>Our team is passionate about customer satisfaction. From browsing to checkout, we strive to make every step of your shopping journey smooth, transparent, and enjoyable.</p>

                <p>Whether you are looking for everyday essentials or unique finds, Generic Store has something for everyone. Honest, reliable, and customer-focused – that is our promise.</p>

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
              <img src="https://placehold.co/600x400/cccccc/666666?text=Store+Image+1" alt="Our store" className="img-fluid" />
            </div>
            <div className="col-md-4 mb-3">
              <img src="https://placehold.co/600x400/cccccc/666666?text=Store+Image+2" alt="Our products" className="img-fluid" />
            </div>
            <div className="col-md-4 mb-3">
              <img src="https://placehold.co/600x400/cccccc/666666?text=Store+Image+3" alt="Our team" className="img-fluid" />
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
