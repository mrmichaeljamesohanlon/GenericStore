import React from 'react';
import { Link } from 'react-router-dom';
import { getLanguageCodeFromSession } from '../../../helpers/CommonHelper';

const HeroBanner = () => {
    const langCode = getLanguageCodeFromSession();

    return (
        <section className="hero-banner-section">
            {/* SVG background decorations */}
            <svg className="hero-svg-bg" aria-hidden="true" xmlns="http://www.w3.org/2000/svg">
                <defs>
                    <radialGradient id="rg1" cx="30%" cy="40%" r="60%">
                        <stop offset="0%" stopColor="#d32f2f" stopOpacity="0.18" />
                        <stop offset="100%" stopColor="#1a1a2e" stopOpacity="0" />
                    </radialGradient>
                    <radialGradient id="rg2" cx="75%" cy="65%" r="50%">
                        <stop offset="0%" stopColor="#ff6f00" stopOpacity="0.14" />
                        <stop offset="100%" stopColor="#1a1a2e" stopOpacity="0" />
                    </radialGradient>
                </defs>
                <rect width="100%" height="100%" fill="url(#rg1)" />
                <rect width="100%" height="100%" fill="url(#rg2)" />

                {/* Decorative circles */}
                <circle className="hero-svg-circle hero-svg-circle--1" cx="8%" cy="20%" r="60" fill="none" stroke="#d32f2f" strokeWidth="1.5" strokeOpacity="0.3" />
                <circle className="hero-svg-circle hero-svg-circle--2" cx="92%" cy="78%" r="90" fill="none" stroke="#ff6f00" strokeWidth="1.5" strokeOpacity="0.25" />
                <circle className="hero-svg-circle hero-svg-circle--3" cx="85%" cy="15%" r="45" fill="none" stroke="#d32f2f" strokeWidth="1" strokeOpacity="0.2" />

                {/* Speed lines */}
                <line x1="0" y1="55%" x2="18%" y2="55%" stroke="#d32f2f" strokeWidth="2" strokeOpacity="0.35" />
                <line x1="0" y1="57%" x2="10%" y2="57%" stroke="#ff6f00" strokeWidth="1" strokeOpacity="0.25" />
                <line x1="82%" y1="45%" x2="100%" y2="45%" stroke="#d32f2f" strokeWidth="2" strokeOpacity="0.35" />
                <line x1="90%" y1="47%" x2="100%" y2="47%" stroke="#ff6f00" strokeWidth="1" strokeOpacity="0.25" />

                {/* Gear / cog SVG icon — top right */}
                <g className="hero-svg-gear" transform="translate(88%, 12%)" opacity="0.12">
                    <circle cx="0" cy="0" r="32" fill="none" stroke="#ffffff" strokeWidth="3" />
                    <circle cx="0" cy="0" r="14" fill="none" stroke="#ffffff" strokeWidth="3" />
                    {[0, 45, 90, 135, 180, 225, 270, 315].map((deg, i) => {
                        const rad = (deg * Math.PI) / 180;
                        const x1 = Math.cos(rad) * 32;
                        const y1 = Math.sin(rad) * 32;
                        const x2 = Math.cos(rad) * 44;
                        const y2 = Math.sin(rad) * 44;
                        return <line key={i} x1={x1} y1={y1} x2={x2} y2={y2} stroke="#ffffff" strokeWidth="6" strokeLinecap="round" />;
                    })}
                </g>

                {/* Checkered flag pattern — bottom left */}
                <g className="hero-svg-flag" transform="translate(3%, 72%)" opacity="0.1">
                    {Array.from({ length: 4 }).map((_, row) =>
                        Array.from({ length: 4 }).map((_, col) => (
                            (row + col) % 2 === 0 && (
                                <rect key={`${row}-${col}`} x={col * 14} y={row * 14} width="14" height="14" fill="#ffffff" />
                            )
                        ))
                    )}
                </g>
            </svg>

            <div className="hero-banner-inner container">
                <div className="hero-banner-text">
                    <span className="hero-banner-eyebrow">Premier Motorsport Solutions</span>
                    <h1 className="hero-banner-title">
                        Built for <span className="hero-banner-title--accent">Champions</span>
                    </h1>
                    <p className="hero-banner-subtitle">
                        Professional workshop services, race preparation, performance parts &amp; hire packages — everything you need to compete and win.
                    </p>
                    <div className="hero-banner-actions">
                        <Link
                            to={`/${langCode}/all-products/0/all-categories`}
                            className="hero-banner-btn hero-banner-btn--primary"
                        >
                            Shop Parts
                        </Link>
                        <Link
                            to={`/${langCode}/contact-us`}
                            className="hero-banner-btn hero-banner-btn--secondary"
                        >
                            Our Services
                        </Link>
                    </div>
                </div>

                {/* Inline SVG motorsport graphic */}
                <div className="hero-banner-graphic" aria-hidden="true">
                    <svg viewBox="0 0 260 220" xmlns="http://www.w3.org/2000/svg" className="hero-svg-car">
                        {/* Track oval */}
                        <ellipse cx="130" cy="170" rx="110" ry="28" fill="none" stroke="#d32f2f" strokeWidth="2" strokeOpacity="0.4" strokeDasharray="8 5" />

                        {/* Simplified race car body */}
                        <g transform="translate(130,120)">
                            {/* Main body */}
                            <path d="M-70,10 Q-60,-18 -20,-22 L20,-22 Q60,-18 70,10 Z" fill="#d32f2f" />
                            {/* Cockpit canopy */}
                            <path d="M-22,-22 Q-12,-46 12,-46 Q22,-46 22,-22 Z" fill="#111" />
                            {/* Front wing */}
                            <path d="M55,10 L80,14 L75,22 L55,18 Z" fill="#b71c1c" />
                            {/* Rear wing */}
                            <rect x="-82" y="-8" width="20" height="4" rx="2" fill="#b71c1c" />
                            <rect x="-80" y="-12" width="16" height="4" rx="1" fill="#9e1515" />
                            {/* Wheels */}
                            <circle cx="-50" cy="18" r="14" fill="#222" />
                            <circle cx="-50" cy="18" r="8" fill="#444" />
                            <circle cx="46" cy="18" r="14" fill="#222" />
                            <circle cx="46" cy="18" r="8" fill="#444" />
                            {/* Speed lines */}
                            <line x1="-90" y1="0" x2="-110" y2="0" stroke="#ff6f00" strokeWidth="2" strokeLinecap="round" opacity="0.7" />
                            <line x1="-90" y1="6" x2="-120" y2="6" stroke="#ff6f00" strokeWidth="1.5" strokeLinecap="round" opacity="0.5" />
                            <line x1="-90" y1="-6" x2="-105" y2="-6" stroke="#ff6f00" strokeWidth="1" strokeLinecap="round" opacity="0.4" />
                        </g>

                        {/* Trophy icon — top right of card */}
                        <g transform="translate(205,30)" opacity="0.7">
                            <rect x="-14" y="24" width="28" height="5" rx="2" fill="#ff6f00" />
                            <rect x="-6" y="18" width="12" height="8" rx="1" fill="#ff6f00" />
                            <path d="M-16,0 Q-20,12 -12,18 L12,18 Q20,12 16,0 Z" fill="#ff6f00" />
                            <path d="M-16,0 Q-26,-4 -24,8 Q-20,14 -12,14" fill="none" stroke="#ff6f00" strokeWidth="2" />
                            <path d="M16,0 Q26,-4 24,8 Q20,14 12,14" fill="none" stroke="#ff6f00" strokeWidth="2" />
                            <text x="0" y="12" textAnchor="middle" fontSize="10" fill="#1a1a2e" fontWeight="bold">1</text>
                        </g>

                        {/* Helmet icon */}
                        <g transform="translate(42,40)" opacity="0.65">
                            <path d="M0,-20 Q22,-20 22,0 Q22,18 0,22 Q-8,22 -14,16 L-14,-4 Q-14,-20 0,-20 Z" fill="#d32f2f" />
                            <path d="M-2,-4 Q8,-4 10,6 Q10,14 2,16" fill="none" stroke="rgba(255,255,255,0.4)" strokeWidth="3" />
                            <path d="M-14,0 Q-18,0 -18,8 Q-18,16 -14,16" fill="none" stroke="#b71c1c" strokeWidth="2" />
                        </g>
                    </svg>
                </div>
            </div>
        </section>
    );
};

export default HeroBanner;
