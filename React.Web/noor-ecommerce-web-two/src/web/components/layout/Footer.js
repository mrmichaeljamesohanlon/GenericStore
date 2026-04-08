import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { Row, Col, Container } from "reactstrap";
import logoDataUri from '../../../helpers/logoDataUri';
import SubscribeNewsLetter from "../shared/SubscribeNewsLetter";
import { useSelector, useDispatch } from 'react-redux';
import Config from '../../../helpers/Config';
import { MakeApiCallAsync } from '../../../helpers/ApiHelpers';
import { checkIfStringIsEmtpy } from '../../../helpers/ValidationHelper';
import rootAction from '../../../stateManagment/actions/rootAction';
import { LOADER_DURATION } from '../../../helpers/Constants';
import { getLanguageCodeFromSession, GetLocalizationControlsJsonDataForScreen, replaceLoclizationLabel } from '../../../helpers/CommonHelper';
import GlobalEnums from '../../../helpers/GlobalEnums';
import { makeImageUrl } from '../../../helpers/ConversionHelper';

const Footer = ({ layoutLogo }) => {

  const dispatch = useDispatch();
  const [paymentMethods, setPaymentMethods] = useState([]);
  const [adminPanelBaseURL, setadminPanelBaseURL] = useState(Config['ADMIN_BASE_URL']);
  const [LocalizationLabelsArray, setLocalizationLabelsArray] = useState([]);
  const websiteLogoFromRedux = useSelector(state => state.commonReducer.websiteLogoInLocalStorage);
  const [LogoImageFromStorage, setLogoImageFromStorage] = useState(websiteLogoFromRedux);


  useEffect(() => {
    // declare the data fetching function
    const DataOperationFunc = async () => {


      const headers = {
        Accept: 'application/json',
        'Content-Type': 'application/json',

      }

      const param = {
        requestParameters: {
          recordValueJson: "[]",
        },
      };


      //--Get payment methods
      const responsePaymentMethods = await MakeApiCallAsync(Config.END_POINT_NAMES['GET_PAYMENT_METHODS'], null, param, headers, "POST", true);
      if (responsePaymentMethods != null && responsePaymentMethods.data != null) {
        await setPaymentMethods(JSON.parse(responsePaymentMethods.data.data));

      }

      //--Get Website Logo
      if (!checkIfStringIsEmtpy(LogoImageFromStorage)) {

        let paramLogo = {
          requestParameters: {
            recordValueJson: "[]",
          }
        };

        let WebsiteLogoInLocalStorage = "";
        let logoResponse = await MakeApiCallAsync(Config.END_POINT_NAMES['GET_WEBSITE_LOGO'], null, paramLogo, headers, "POST", true);
        if (logoResponse != null && logoResponse.data != null) {
          console.log(logoResponse.data)

          if (logoResponse.data.data != "") {
            let logoData = JSON.parse(logoResponse.data.data);
            WebsiteLogoInLocalStorage = logoData[0].AppConfigValue;
            dispatch(rootAction.commonAction.setWebsiteLogo(WebsiteLogoInLocalStorage));
            setLogoImageFromStorage(WebsiteLogoInLocalStorage);
          }


        }
      }

    }

    //--start loader
    dispatch(rootAction.commonAction.setLoading(true));

    // call the function
    DataOperationFunc().catch(console.error);

    //--stop loader
    setTimeout(() => {
      dispatch(rootAction.commonAction.setLoading(false));
    }, LOADER_DURATION);


  }, [])


  useEffect(() => {
    // declare the data fetching function
    const dataOperationFunc = async () => {

      //-- Get website localization data
      let arryRespLocalization = await GetLocalizationControlsJsonDataForScreen(GlobalEnums.Entities["Footer"], null);
      if (arryRespLocalization != null && arryRespLocalization != undefined && arryRespLocalization.length > 0) {
        await setLocalizationLabelsArray(arryRespLocalization);
      }
    }
    // call the function
    dataOperationFunc().catch(console.error);
  }, [])

  return (
    <footer className="footer-2">
      <Container>
        <Row className="row">
          <Col xs="12">
            <div className="footer-main-contian">
              <Row>
                <Col lg="4" md="12">
                  <div className="footer-left">
                    <div className="footer-logo">
                      <img src={logoDataUri} className="img-fluid" alt="logo" />
                    </div>
                    <div className="footer-detail">
                      <p>
                        Welcome to Generic Store – your go-to destination for quality products at great prices. We are committed to providing an outstanding shopping experience for every customer.
                      </p>
                      <ul className="paymant-bottom">

                        {
                          paymentMethods?.map((item, idx) =>




                            <li key={item.PaymentMethodId ?? idx}>
                              <a href="#">
                                <img src={makeImageUrl(adminPanelBaseURL, item.ImageUrl)} className="img-fluid" alt="pay" />
                              </a>
                            </li>

                          )}




                      </ul>
                    </div>
                  </div>
                </Col>
                <Col lg="8" md="12">
                  <div className="footer-right">
                    <Row className="row">
                      <Col md="12">
                        <SubscribeNewsLetter />
                      </Col>
                      <Col md="12">
                        <div className="account-right">
                          <div className="row">
                            <div className="col-md-4">
                              <div className="footer-box">
                                <div className="footer-title">
                                  <h5>
                                    {LocalizationLabelsArray.length > 0 ?
                                      replaceLoclizationLabel(LocalizationLabelsArray, "Links", "lbl_footr_myaccount")
                                      :
                                      "Links"
                                    }
                                  </h5>
                                </div>
                                <div className="footer-contant">
                                  <ul>
                                    {/* Login link removed */}
                                    <li>
                                      <Link to={`/${getLanguageCodeFromSession()}/about`} id="lbl_footr_about">
                                        {LocalizationLabelsArray.length > 0 ?
                                          replaceLoclizationLabel(LocalizationLabelsArray, "About Us", "lbl_footr_about")
                                          :
                                          "About Us"
                                        }
                                      </Link>
                                    </li>
                                    <li>
                                      <Link to={`/${getLanguageCodeFromSession()}/contact-us`} id="lbl_footr_cont">
                                        {LocalizationLabelsArray.length > 0 ?
                                          replaceLoclizationLabel(LocalizationLabelsArray, "Contact Us", "lbl_footr_cont")
                                          :
                                          "Contact Us"
                                        }
                                      </Link>
                                    </li>
                                    
                                    <li>
                                      <Link to={`/${getLanguageCodeFromSession()}/terms-and-conditions`} >
                                        Terms and Conditions
                                      </Link>
                                    </li>
                                    {/* Create Account link removed */}
                                    {/* Become Vendor link removed */}
                                  </ul>
                                </div>
                              </div>
                            </div>
                            <div className="col-md-3">
                              <div className="footer-box">
                                <div className="footer-title">
                                  <h5>
                                    {LocalizationLabelsArray.length > 0 ?
                                      replaceLoclizationLabel(LocalizationLabelsArray, "Quick Links", "lbl_footr_quicklink")
                                      :
                                      "Quick Links"
                                    }
                                  </h5>
                                </div>
                                <div className="footer-contant">
                                  <ul>
                                    <li>
                                      <Link to={`/${getLanguageCodeFromSession()}/cart`} >
                                        {LocalizationLabelsArray.length > 0 ?
                                          replaceLoclizationLabel(LocalizationLabelsArray, "Cart", "lbl_footr_cart")
                                          :
                                          "Cart"
                                        }
                                      </Link>
                                    </li>
                                    <li>
                                      <Link to={`/${getLanguageCodeFromSession()}/faq`} id="lbl_footr_faq">
                                        {LocalizationLabelsArray.length > 0 ?
                                          replaceLoclizationLabel(LocalizationLabelsArray, " FAQ", "lbl_footr_faq")
                                          :
                                          " FAQ"
                                        }
                                      </Link>
                                    </li>
                                    <li>
                                      <Link to={`/${getLanguageCodeFromSession()}/`} id="lbl_footr_home">
                                        {LocalizationLabelsArray.length > 0 ?
                                          replaceLoclizationLabel(LocalizationLabelsArray, "Home", "lbl_footr_home")
                                          :
                                          "Home"
                                        }
                                      </Link>
                                    </li>
                                    <li>
                                      <Link to={`/${getLanguageCodeFromSession()}/compare`} >
                                        {LocalizationLabelsArray.length > 0 ?
                                          replaceLoclizationLabel(LocalizationLabelsArray, "Compare", "lbl_footr_Compare")
                                          :
                                          "Compare"
                                        }
                                      </Link>
                                    </li>
                                    <li>
                                      <Link to={`/${getLanguageCodeFromSession()}/all-products/0/all-categories`} >
                                        {LocalizationLabelsArray.length > 0 ?
                                          replaceLoclizationLabel(LocalizationLabelsArray, "All Products", "lbl_footr_all_prd")
                                          :
                                          "All Products"
                                        }
                                      </Link>
                                    </li>
                                  </ul>
                                </div>
                              </div>
                            </div>
                            <div className="col-md-5">
                              <div className="footer-box footer-contact-box">
                                <div className="footer-title">
                                  <h5>
                                    {LocalizationLabelsArray.length > 0 ?
                                      replaceLoclizationLabel(LocalizationLabelsArray, "Contact Us", "lbl_footr_cont")
                                      :
                                      "Contact Us"
                                    }
                                  </h5>
                                </div>
                                <div className="footer-contant">
                                  <ul className="contact-list">
                                    <li>
                                      <i className="fa fa-map-marker"></i>
                                      <span>
                                        <strong>Generic Store</strong><br />
                                        Your Address Line 1,<br />
                                        Your Address Line 2,<br />
                                        Your City,<br />
                                        Your Country
                                      </span>
                                    </li>
                                    <li>
                                      <i className="fa fa-phone"></i>
                                      <span>call us: Your Phone Number</span>
                                    </li>
                                    <li>
                                      <i className="fa fa-envelope-o"></i>
                                      <span>email us: contact@yourstore.com</span>
                                    </li>
                                  </ul>
                                </div>
                              </div>
                            </div>
                          </div>
                        </div>
                      </Col>
                    </Row>
                  </div>
                </Col>
              </Row>
            </div>
          </Col>
        </Row>
      </Container>
      <div className="app-link-block  bg-transparent">
        <Container>
          <Row>
            <div className="app-link-bloc-contain app-link-bloc-contain-1">
              {/* <div className="app-item-group">
                <div className="app-item">
                  <Media src="/images/layout-1/app/1.png" className="img-fluid" alt="app-banner" />
                </div>
                <div className="app-item">
                                        <li>
                                          <Link to={`/${getLanguageCodeFromSession()}/terms-and-conditions`} >
                                            Terms and Conditions
                                          </Link>
                                        </li>
                  <Media src="/images/layout-1/app/2.png" className="img-fluid" alt="app-banner" />
                </div>
              </div> */}
              <div className="app-item-group ">
                <div className="social-block">
                  <h6>follow us</h6>
                  <ul className="social">
                    <li>
                      <Link to="#">
                        <i className="fa fa-facebook"></i>
                      </Link>
                    </li>
                    <li>
                      <Link to="#">
                        <i className="fa fa-linkedin"></i>
                      </Link>
                    </li>
                    <li>
                      <Link to="#">
                        <i className="fa fa-youtube-play"></i>
                      </Link>
                    </li>
                    

                  </ul>
                </div>
              </div>
            </div>
          </Row>
        </Container>
      </div>
      <div className="sub-footer">
        <Container>
          <Row>
            <Col xs="12">
              <div className="sub-footer-contain">
                <span>&copy; {new Date().getFullYear()} </span>Copyright Generic Store. All Rights Reserved.
              </div>
            </Col>
          </Row>
        </Container>
      </div>
    </footer>
  );
};

export default Footer;
