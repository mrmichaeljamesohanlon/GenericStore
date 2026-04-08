import React, { useEffect, useState } from 'react';
import SiteBreadcrumb from '../../components/layout/SiteBreadcrumb';
import BestFacilities from '../../components/shared/BestFacilities';
import { Helmet } from 'react-helmet-async';
import {
    Accordion,
    AccordionItem,
    AccordionItemHeading,
    AccordionItemButton,
    AccordionItemPanel,
} from 'react-accessible-accordion';
import Config from '../../../helpers/Config';
import { getLanguageCodeFromSession, GetLocalizationControlsJsonDataForScreen, replaceLoclizationLabel } from '../../../helpers/CommonHelper';
import GlobalEnums from '../../../helpers/GlobalEnums';


const FaqPage = () => {
    const [siteTitle, setSiteTitle] = useState(Config['SITE_TTILE']);
    const [LocalizationLabelsArray, setLocalizationLabelsArray] = useState([]);
    const [langCode, setLangCode] = useState('');

    useEffect(() => {
        // declare the data fetching function
        const dataOperationFunc = async () => {
            let lnCode = getLanguageCodeFromSession();
            setLangCode(lnCode);

            //-- Get website localization data
            let arryRespLocalization = await GetLocalizationControlsJsonDataForScreen(GlobalEnums.Entities["FaqPage"], null);
            if (arryRespLocalization != null && arryRespLocalization != undefined && arryRespLocalization.length > 0) {
                await setLocalizationLabelsArray(arryRespLocalization);
            }
        }
        // call the function
        dataOperationFunc().catch(console.error);
    }, [])



    return (
        <>
            <Helmet>
                <title>{siteTitle} - Frequently Asked Questions (FAQ)</title>
                <meta name="description" content={siteTitle + " - Frequently Asked Questions (FAQ)"} />
                <meta name="keywords" content="Frequently Asked Questions, FAQ"></meta>
            </Helmet>

            <SiteBreadcrumb title="FAQ's" />

            <section className="faq-area ptb-60">
                <div className="container">
                    <div className="section-title">
                        <h2><span className="dot"></span>
                            <span id="lbl_faq_pagetitle">

                                {LocalizationLabelsArray.length > 0 ?
                                    replaceLoclizationLabel(LocalizationLabelsArray, "Frequently Asked Questions", "lbl_faq_pagetitle")
                                    :
                                    "Frequently Asked Questions"
                                }
                            </span>
                        </h2>
                    </div>

                    <div className="faq-accordion">
                        <ul className="accordion">
                            <Accordion>
                                <AccordionItem>
                                    <AccordionItemHeading>
                                        <AccordionItemButton>
                                            What shipping methods are available?
                                        </AccordionItemButton>
                                    </AccordionItemHeading>
                                    <AccordionItemPanel>
                                        <p>
                                            We use dedicated UK couriers to ship our products, ensuring your order arrives safely and efficiently. For most orders, we select the most reliable service based on your location and the size of your item. If you have a preferred courier, please let us know and we will do our best to accommodate your request.
                                        </p>
                                    </AccordionItemPanel>
                                </AccordionItem>

                                <AccordionItem>
                                    <AccordionItemHeading>
                                        <AccordionItemButton>
                                            What are shipping times and costs?
                                        </AccordionItemButton>
                                    </AccordionItemHeading>
                                    <AccordionItemPanel>
                                        <p>
                                            Shipping is usually within 24 hours of your order being placed. Shipping costs may vary depending on the size and weight of the item ordered, as well as your delivery location. We always aim to keep shipping costs as low as possible and will confirm the exact amount at checkout. For urgent orders, please contact us directly to discuss express options.
                                        </p>
                                    </AccordionItemPanel>
                                </AccordionItem>

                                <AccordionItem>
                                    <AccordionItemHeading>
                                        <AccordionItemButton>
                                            What payment methods can I use?
                                        </AccordionItemButton>
                                    </AccordionItemHeading>
                                    <AccordionItemPanel>
                                        <p>
                                            We are integrated with Stripe as a payment gateway, allowing you to use all major credit and debit cards for your purchase. Stripe is a secure and trusted platform, so you can shop with confidence. If you have any issues with payment, please contact our support team for assistance.
                                        </p>
                                    </AccordionItemPanel>
                                </AccordionItem>

                                <AccordionItem>
                                    <AccordionItemHeading>
                                        <AccordionItemButton>
                                            Are there next day or economy shipping options?
                                        </AccordionItemButton>
                                    </AccordionItemHeading>
                                    <AccordionItemPanel>
                                        <p>
                                            Next day delivery is available in some cases; however, orders received after 3pm UK time will be processed the following day. We also offer economy shipping services for less urgent deliveries. If you need your order by a specific date, please let us know and we will do our best to help.
                                        </p>
                                    </AccordionItemPanel>
                                </AccordionItem>
                            </Accordion>
                        </ul>
                    </div>
                </div>
            </section>

            <BestFacilities />

        </>
    );

}

export default FaqPage;
