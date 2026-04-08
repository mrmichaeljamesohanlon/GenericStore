import React, { useMemo } from "react";
import { Link } from "react-router-dom";
import { getLanguageCodeFromSession } from "../../../helpers/CommonHelper";

const MeteorHero = () => {
  const langCode = getLanguageCodeFromSession();

  const meteors = useMemo(() => [
    { top: 2,  left: 15, delay: 0.0, duration: 6 },
    { top: 10, left: 40, delay: 1.5, duration: 8 },
    { top: 5,  left: 65, delay: 0.8, duration: 5 },
    { top: 20, left: 80, delay: 2.5, duration: 7 },
    { top: 30, left: 5,  delay: 0.3, duration: 9 },
    { top: 0,  left: 55, delay: 3.1, duration: 6 },
    { top: 15, left: 90, delay: 1.2, duration: 8 },
    { top: 8,  left: 30, delay: 4.0, duration: 5 },
    { top: 25, left: 70, delay: 0.6, duration: 7 },
    { top: 40, left: 20, delay: 2.0, duration: 6 },
    { top: 3,  left: 85, delay: 3.7, duration: 9 },
    { top: 18, left: 50, delay: 1.8, duration: 5 },
    { top: 35, left: 10, delay: 0.4, duration: 8 },
    { top: 12, left: 75, delay: 2.9, duration: 7 },
    { top: 45, left: 35, delay: 1.1, duration: 6 },
    { top: 6,  left: 60, delay: 3.5, duration: 9 },
    { top: 28, left: 95, delay: 0.9, duration: 5 },
    { top: 50, left: 45, delay: 2.3, duration: 8 },
    { top: 22, left: 25, delay: 4.2, duration: 6 },
    { top: 38, left: 88, delay: 1.6, duration: 7 },
  ], []);

  return (
    <section className="meteor-hero-section">
      <div className="meteor-hero-overlay" />

      {meteors.map((m, i) => (
        <span
          key={i}
          className="meteor-streak"
          style={{
            top: `${m.top}%`,
            left: `${m.left}%`,
            animationDelay: `${m.delay}s`,
            animationDuration: `${m.duration}s`,
          }}
        />
      ))}

      <div className="meteor-hero-content">
        <p className="meteor-hero-eyebrow">Premier Motorsport Solutions</p>
        <h1 className="meteor-hero-title">
          <span className="meteor-hero-title-line">Workshop &amp; Race</span>
          <span className="meteor-hero-title-line meteor-hero-title-highlight">
            Preparation
          </span>
        </h1>
        <p className="meteor-hero-subtitle">
          Professional car preparation for the rally stage and race track
        </p>
        <Link
          to={`/${langCode}/all-products/0/all-categories`}
          className="meteor-hero-btn"
        >
          Explore Now
        </Link>
      </div>
    </section>
  );
};

export default MeteorHero;

