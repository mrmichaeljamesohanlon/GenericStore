import React, { useState } from 'react';
import Slider from 'react-slick';

const bannerImages = [
    {
        src: 'https://github.com/user-attachments/assets/27cf0e78-3a45-4632-b081-40334006d61c',
        alt: 'Rally Car Interior',
    },
    {
        src: 'https://github.com/user-attachments/assets/be566816-6f0a-4414-b601-fe91b74292a5',
        alt: 'Blue Mini Cooper Rally Car',
    },
    {
        src: 'https://github.com/user-attachments/assets/3a63692d-6bd7-464e-8e5d-d373fb6b2243',
        alt: 'Motorsport Suspension Kit',
    },
    {
        src: 'https://github.com/user-attachments/assets/a7f8c527-79f8-4730-bbbf-2131e6914372',
        alt: 'Threaded Fittings',
    },
];

const sliderSettings = {
    dots: true,
    arrows: true,
    infinite: true,
    speed: 600,
    slidesToShow: 1,
    slidesToScroll: 1,
    autoplay: true,
    autoplaySpeed: 4000,
    pauseOnHover: true,
    fade: false,
};

const BannerSlider = () => {
    const [failedIndexes, setFailedIndexes] = useState([]);

    const handleImageError = (idx) => () => {
        setFailedIndexes((prev) => (prev.includes(idx) ? prev : [...prev, idx]));
    };

    return (
        <section className="spark-banner-slider">
            <Slider {...sliderSettings}>
                {bannerImages.map((banner, idx) => (
                    <div key={idx} className="spark-banner-slide">
                        {failedIndexes.includes(idx) ? (
                            <div className="spark-banner-placeholder" data-label={banner.alt} />
                        ) : (
                            <img
                                src={banner.src}
                                className="spark-banner-img"
                                alt={banner.alt}
                                onError={handleImageError(idx)}
                            />
                        )}
                    </div>
                ))}
            </Slider>
        </section>
    );
};

export default BannerSlider;
