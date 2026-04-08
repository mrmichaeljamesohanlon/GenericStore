import React, { useEffect, useState } from 'react';
import SiteBreadcrumb from '../../components/layout/SiteBreadcrumb';
import { Helmet } from 'react-helmet-async';
import Config from '../../../helpers/Config';
import { getLanguageCodeFromSession, GetLocalizationControlsJsonDataForScreen, replaceLoclizationLabel } from '../../../helpers/CommonHelper';
import GlobalEnums from '../../../helpers/GlobalEnums';
import { Collapse, Card, CardHeader, Container, Row, Col } from "reactstrap";


const faqData = [
    {
        qus: "What shipping methods are available?",
        ans: "We use dedicated UK couriers to ship our products, ensuring your order arrives safely and efficiently. For most orders, we select the most reliable service based on your location and the size of your item. If you have a preferred courier, please let us know and we will do our best to accommodate your request.",
    },
    {
        qus: "What shipping times and costs?",
        ans: "Shipping is usually within 24 hours of your order being placed. Shipping costs may vary depending on the size and weight of the item ordered, as well as your delivery location. We always aim to keep shipping costs as low as possible and will confirm the exact amount at checkout. For urgent orders, please contact us directly to discuss express options.",
    },
    {
        qus: "What payment methods can I use?",
        ans: "We are integrated with Stripe as a payment gateway, allowing you to use all major credit and debit cards for your purchase. Stripe is a secure and trusted platform, so you can shop with confidence. If you have any issues with payment, please contact our support team for assistance.",
    },
    {
        qus: "Are there next day or economy shipping options?",
        ans: "Next day delivery is available in some cases; however, orders received after 3pm UK time will be processed the following day. We also offer economy shipping services for less urgent deliveries. If you need your order by a specific date, please let us know and we will do our best to help.",
    },
];


const FaqPage = () => {
    const [siteTitle, setSiteTitle] = useState(Config['SITE_TTILE']);
    const [LocalizationLabelsArray, setLocalizationLabelsArray] = useState([]);
    const [langCode, setLangCode] = useState('');
    const [id, setId] = useState(0);

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

            <SiteBreadcrumb 
            title= {LocalizationLabelsArray.length > 0 ?
                replaceLoclizationLabel(LocalizationLabelsArray, "FAQ", "lbl_faq_pagetitle")
                :
                "FAQ"
            }
            
            parent="Home" />

            <section className="faq-section section-big-py-space bg-light">
                <Container>
                    <Row>
                        <Col sm="12">
                            <div className="accordion theme-accordion" id="accordionExample">
                                {faqData.map((faq, i) => (
                                    <Card key={i}>
                                        <CardHeader id="headingOne">
                                            <h5 className={`mb-0 ${id === i ? "show" : ""}`}>
                                                <button
                                                    className="btn btn-link"
                                                    type="button"
                                                    data-toggle="collapse"
                                                    onClick={() => {
                                                        id === i ? setId(null) : setId(i);
                                                    }}
                                                    data-target="#collapseOne"
                                                    aria-expanded="true"
                                                    aria-controls="collapseOne">
                                                    {faq.qus}
                                                </button>
                                            </h5>
                                        </CardHeader>
                                        <Collapse isOpen={id === i} id="collapseOne" className="collapse" aria-labelledby="headingOne" data-parent="#accordionExample">
                                            <div className="card-body">
                                                <p>{faq.ans}</p>
                                            </div>
                                        </Collapse>
                                    </Card>
                                ))}
                            </div>
                        </Col>
                    </Row>
                </Container>
            </section>


        </>
    );

}

export default FaqPage;
