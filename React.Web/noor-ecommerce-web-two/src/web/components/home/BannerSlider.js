import React, { useEffect, useMemo, useState } from "react";
import Slider from "react-slick";

const makeLocalBannerCandidates = () => {
  const extensions = ["jpg", "jpeg", "png", "webp"];
  const prefixes = ["banner-", "banner", "slide", "Why", "why", "home-", "home"];
  const candidates = [];

  for (let index = 1; index <= 50; index++) {
    for (const prefix of prefixes) {
      for (const ext of extensions) {
        candidates.push({
          src: `/images/home/${prefix}${index}.${ext}`,
          alt: `Home Banner ${index}`,
        });
      }
    }

    for (const ext of extensions) {
      const paddedIndex = index.toString().padStart(2, "0");
      candidates.push({
        src: `/images/home/banner-${paddedIndex}.${ext}`,
        alt: `Home Banner ${index}`,
      });
      candidates.push({
        src: `/images/home/banner${paddedIndex}.${ext}`,
        alt: `Home Banner ${index}`,
      });
    }
  }

  return candidates;
};

const localBannerCandidates = makeLocalBannerCandidates();
const fallbackBanner = [{ src: "/images/home/banner-1.jpg", alt: "Home Banner" }];

const sliderSettings = {
  dots: true,
  arrows: true,
  infinite: true,
  speed: 600,
  slidesToShow: 1,
  slidesToScroll: 1,
  autoplay: true,
  autoplaySpeed: 3500,
  pauseOnHover: true,
  responsive: [
    {
      breakpoint: 768,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1,
      },
    },
    {
      breakpoint: 1024,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1,
      },
    },
  ],
};

const BannerSlider = () => {
  const [bannerImages, setBannerImages] = useState([]);
  const [failedIndexes, setFailedIndexes] = useState([]);

  const MIN_BANNER_WIDTH = 300;
  const MIN_BANNER_HEIGHT = 150;

  const getLoadableLocalBanners = async () => {
    const checks = localBannerCandidates.map(
      (banner) =>
        new Promise((resolve) => {
          const image = new Image();
          image.onload = () => {
            if (
              image.naturalWidth >= MIN_BANNER_WIDTH &&
              image.naturalHeight >= MIN_BANNER_HEIGHT
            ) {
              resolve(banner);
              return;
            }
            resolve(null);
          };
          image.onerror = () => resolve(null);
          image.src = banner.src;
        })
    );

    const results = await Promise.all(checks);
    const unique = new Map();

    results.filter(Boolean).forEach((item) => {
      const key = item.src.toLowerCase();
      if (!unique.has(key)) {
        unique.set(key, item);
      }
    });

    return Array.from(unique.values());
  };

  useEffect(() => {
    let isMounted = true;

    const loadBannerImages = async () => {
      const localImages = await getLoadableLocalBanners();
      if (!isMounted) {
        return;
      }

      if (localImages.length > 0) {
        setBannerImages(localImages);
        return;
      }

      setBannerImages([]);
    };

    loadBannerImages();

    return () => {
      isMounted = false;
    };
  }, []);

  useEffect(() => {
    setFailedIndexes([]);
  }, [bannerImages]);

  const imagesToDisplay = useMemo(
    () => (bannerImages.length > 0 ? bannerImages : fallbackBanner),
    [bannerImages]
  );

  const handleImageError = (idx) => () => {
    setFailedIndexes((prev) => (prev.includes(idx) ? prev : [...prev, idx]));
  };

  return (
    <section className="spark-banner-slider">
      <Slider {...sliderSettings}>
        {imagesToDisplay.map((banner, idx) => (
          <div key={idx} className="spark-banner-slide">
            {failedIndexes.includes(idx) ? (
              <div
                className="spark-banner-placeholder"
                data-label={banner.alt}
                aria-label={`${banner.alt} - Image failed to load`}
              />
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
