import React from 'react';
import myImage from '../../resources/custom/images/call_img.png';

const ContactBanner = () => {
    return (
        <section className="contact-banner ptb-60">
            <div className="container">
                <div className="row align-items-center justify-content-center">
                    <div className="col-md-12 text-center">
                        <div className="contact-banner-contain">
                            <div className="contact-banner-img mb-3">
                                <img src={myImage} className="img-fluid" alt="contact-banner" />
                            </div>
                            <h3>If you have any questions please call us</h3>
                            <h2>07516 783331</h2>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};

export default ContactBanner;
