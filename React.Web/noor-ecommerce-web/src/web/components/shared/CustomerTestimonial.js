import React from 'react';
import banner1 from '../../resources/themeContent/images/main-banner1.jpg';
import banner2 from '../../resources/themeContent/images/main-banner2.jpg';
import banner3 from '../../resources/themeContent/images/main-banner3.jpg';

const reviews = [
    {
        image: banner1,
        user: "James T.",
        review:
            "The team at Generic Store did a fantastic job. Their attention to detail is second to none, and they genuinely care about getting the best result. Highly recommended to anyone looking for quality products and service.",
    },
    {
        image: banner2,
        user: "Sarah M.",
        review:
            "Brilliant service from start to finish. They were transparent, kept us updated throughout, and delivered exactly what was promised. Will definitely be shopping here again.",
    },
    {
        image: banner3,
        user: "David R.",
        review:
            "We've shopped at Generic Store multiple times and have always been impressed. Professional, friendly and completely no-nonsense. They treat every customer with the same care and attention. We wouldn't go anywhere else.",
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
