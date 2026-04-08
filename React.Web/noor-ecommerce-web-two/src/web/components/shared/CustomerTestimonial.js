import React from "react";
import { Row, Col, Container, Media } from "reactstrap";
import Slider from "react-slick";
import myImage from '../../../resources/custom/images/customer_testimonial.jpg';

var settings = {
  autoplay: false,
  autoplaySpeed: 2500,
};

const Review = [
  {
    img: "/images/testimonial/1.jpg",
    user: "James T.",
    review:
      "The team at Generic Store did a fantastic job. Their attention to detail is second to none, and they genuinely care about getting the best result. Highly recommended to anyone looking for quality products and service.",
  },
  {
    img: "/images/testimonial/2.jpg",
    user: "Sarah M.",
    review:
      "Brilliant service from start to finish. They were transparent, kept us updated throughout, and delivered exactly what was promised. Will definitely be shopping here again.",
  },
  {
    img: "/images/testimonial/3.jpg",
    user: "David R.",
    review:
      "We've shopped at Generic Store multiple times and have always been impressed. Professional, friendly and completely no-nonsense. They treat every customer with care and attention. We wouldn't go anywhere else.",
  },
];
const CustomerTestimonial = () => {
  return (
    <>
      <section className="testimonial testimonial-inverse">
        <Container>
          <Row>
            <Col md="12">
              <div className="slide-1 no-arrow">
                <Slider {...settings}>
                  {Review.map((data, i) => (
                    <div key={i}>
                      <div className="testimonial-contain">
                        <div className="media">
                          <div className="testimonial-img">
                            <Media src={myImage} className="img-fluid rounded-circle" alt="testimonial" />
                          </div>
                          <div className="media-body">
                            <h5>{data.user}</h5>
                            <p>{data.review}</p>
                          </div>
                        </div>
                      </div>
                    </div>
                  ))}
                </Slider>
              </div>
            </Col>
          </Row>
        </Container>
      </section>
    </>
  );
};

export default CustomerTestimonial;
