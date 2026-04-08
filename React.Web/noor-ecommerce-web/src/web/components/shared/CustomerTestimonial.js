import React from 'react';
import banner1 from '../../resources/themeContent/images/main-banner1.jpg';
import banner2 from '../../resources/themeContent/images/main-banner2.jpg';
import banner3 from '../../resources/themeContent/images/main-banner3.jpg';

const reviews = [
    {
        image: banner1,
        user: "James T.",
        review:
            "Spark Developments did a fantastic job preparing our car for the rally season. Their attention to detail is second to none, and the team genuinely cares about getting the best result. Highly recommended to anyone serious about motorsport.",
    },
    {
        image: banner2,
        user: "Sarah M.",
        review:
            "Brilliant service from start to finish. The Spark Standard is real – they were transparent, kept us updated throughout, and delivered exactly what was promised. Our car came back performing better than ever.",
    },
    {
        image: banner3,
        user: "David R.",
        review:
            "We've used Spark Developments for both hire packages and workshop services. Professional, friendly and completely no-nonsense. They treat your car like their own. We wouldn't go anywhere else.",
    },
];

const CustomerTestimonial = () => {
    return (
        <section className="testimonial-area ptb-60">
            <div className="container">
                <div className="section-title">
                    <h2>What Our Customers Say</h2>
                </div>
                <div className="row">
                    {reviews.map((data, i) => (
                        <div key={i} className="col-sm-12 col-md-4">
                            <div className="testimonial-item text-center">
                                <div className="testimonial-img mb-3">
                                    <img
                                        src={data.image}
                                        className="img-fluid"
                                        alt={data.user}
                                        style={{ width: '100%', height: '200px', objectFit: 'cover', borderRadius: '8px' }}
                                    />
                                </div>
                                <h5>{data.user}</h5>
                                <p>{data.review}</p>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </section>
    );
};

export default CustomerTestimonial;
