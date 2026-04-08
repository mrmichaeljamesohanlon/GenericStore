import React, { useEffect, useState } from 'react';
import BannerSlider from '../../components/home/BannerSlider'
import PopularProducts from '../../components/products/PopularProducts';
import CompaignSection from '../../components/shared/CompaignSection';
import NewProducts from '../../components/products/NewProducts';
import PopularCategories from '../../components/shared/PopularCategories';
import BestFacilities from '../../components/shared/BestFacilities';
import SubscribeNewsLetter from '../../components/shared/SubscribeNewsLetter';
import DiscountBannerOmg from '../../components/shared/DiscountBannerOmg';
import CustomerTestimonial from '../../components/shared/CustomerTestimonial';
import ContactBanner from '../../components/shared/ContactBanner';
import ServicesInfoPanel from '../../components/shared/ServicesInfoPanel';
import { Link, useNavigate } from 'react-router-dom';
import { Helmet } from 'react-helmet-async';
import Config from '../../../helpers/Config';


const Home = () => {
    const navigate = useNavigate();
    const [siteTitle, setSiteTitle] = useState(Config['SITE_TTILE']);

    return (
        <>

            <Helmet>
                <title>{siteTitle} - Home</title>
                <meta name="description" content={siteTitle + " - Home"} />
                <meta name="keywords" content="Home"></meta>
            </Helmet>

            <BannerSlider />

            <PopularCategories />

            {/* <DiscountBannerOmg /> */}

            <NewProducts />

            <CompaignSection />

            <PopularProducts />

            <CustomerTestimonial />

            <BestFacilities />

            <ServicesInfoPanel />

            <ContactBanner />

            <SubscribeNewsLetter />

        </>
    );

}

export default Home;


