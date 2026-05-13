import React from "react";
import { Link } from "react-router-dom";
import { getLanguageCodeFromSession } from "../../../helpers/CommonHelper";
import heroImage from "../../../resources/themeContent/images/layout-2/collection-banner/2.jpg";

const MeteorHero = () => {
  const langCode = getLanguageCodeFromSession();
  const shopUrl = `/${langCode}/all-products/0/all-categories`;
  const workshopUrl = `/${langCode}/workshop`;

  return (
    <section
      className="dt-home-hero"
      style={{ "--dt-home-hero-img": `url(${heroImage})` }}
    >
      <div className="dt-home-hero__media" aria-hidden="true" />
      <div className="dt-home-hero__shade" />
      <div className="dt-home-hero__inner">
        <h1 className="dt-home-hero__title">Performance parts &amp; race gear</h1>
        <p className="dt-home-hero__subtitle">
          Shop the catalogue for helmets, consumables, and workshop supplies. Workshop services and
          hire are handled separately — not sold through the basket.
        </p>
        <div className="dt-home-hero__actions">
          <Link to={shopUrl} className="dt-home-hero__btn dt-home-hero__btn--primary">
            Shop products
          </Link>
          <Link to={workshopUrl} className="dt-home-hero__btn dt-home-hero__btn--ghost">
            Workshop &amp; services
          </Link>
        </div>
      </div>
    </section>
  );
};

export default MeteorHero;
