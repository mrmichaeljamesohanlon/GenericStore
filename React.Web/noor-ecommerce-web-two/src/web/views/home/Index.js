import React from 'react';
import { Helmet } from 'react-helmet-async';
import Config from '../../../helpers/Config';
import BannerSlider from '../../components/home/BannerSlider';
import MeteorHero from '../../components/home/MeteorHero';
import PopularCategories from '../../components/shared/PopularCategories';
import NewProducts from '../../components/products/NewProducts';
import PopularProducts from '../../components/products/PopularProducts';
import ContactBanner from '../../components/shared/ContactBanner';


const Home = () => {
    const siteTitle = Config['SITE_TTILE'];

    return (
        <>

            <Helmet>
                <title>{siteTitle} - Home</title>
                <meta name="description" content={siteTitle + " - Home"} />
                <meta name="keywords" content="Home"></meta>
            </Helmet>

            <div className="bg-light home-page-dt">
                <MeteorHero />
                <PopularCategories />
                <section className="home-product-region container-fluid px-lg-4">
                    <div className="home-section-head">
                        <h2 className="home-section-head__title">Featured products</h2>
                        <p className="home-section-head__sub">Popular picks from the shop — add to basket and checkout online.</p>
                    </div>
                    <PopularProducts hoverEffect="icon-inline" />
                </section>
                <section className="home-product-region home-product-region--alt container-fluid px-lg-4">
                    <div className="home-section-head">
                        <h2 className="home-section-head__title">New &amp; on sale</h2>
                        <p className="home-section-head__sub">Latest arrivals and promotions in the catalogue.</p>
                    </div>
                    <NewProducts effect="icon-inline" />
                </section>
                <div className="container-fluid px-4 spark-banner-slider-wrap">
                    <BannerSlider />
                </div>
                <ContactBanner/>
            </div>
        </>
    );

}

export default Home;


