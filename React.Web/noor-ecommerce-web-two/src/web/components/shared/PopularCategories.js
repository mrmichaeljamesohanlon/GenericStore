import React, { useEffect, useState } from 'react';
import { Row, Col, Media, Container } from "reactstrap";
import { Link, useNavigate } from 'react-router-dom';
import Slider from "react-slick";
import Config from '../../../helpers/Config';
import myImage from '../../../resources/themeContent/images/layout-2/collection-banner/2.jpg';
import { getLanguageCodeFromSession, GetLocalizationControlsJsonDataForScreen, replaceLoclizationLabel } from '../../../helpers/CommonHelper';
import { MakeApiCallAsync } from '../../../helpers/ApiHelpers';
import GlobalEnums from '../../../helpers/GlobalEnums';
import { makeAnyStringLengthShort, replaceWhiteSpacesWithDashSymbolInUrl, makeImageUrl } from '../../../helpers/ConversionHelper';

import RCat1 from '../../../resources/themeContent/images/layout-1/rounded-cat/cat-1.svg';
import RCat2 from '../../../resources/themeContent/images/layout-1/rounded-cat/cat-2.svg';
import RCat3 from '../../../resources/themeContent/images/layout-1/rounded-cat/cat-3.svg';
import RCat4 from '../../../resources/themeContent/images/layout-1/rounded-cat/cat-4.svg';
import RCat5 from '../../../resources/themeContent/images/layout-1/rounded-cat/cat-5.svg';
import RCat6 from '../../../resources/themeContent/images/layout-1/rounded-cat/cat-6.svg';
import RCat7 from '../../../resources/themeContent/images/layout-1/rounded-cat/cat-7.svg';

const fallbackCategoryImages = [RCat1, RCat2, RCat3, RCat4, RCat5, RCat6, RCat7];

const getSettings = (categoryCount) => ({
  dots: false,
  infinite: categoryCount > 6,
  speed: 300,
  slidesToShow: 6,
  slidesToScroll: 6,
  responsive: [
    {
      breakpoint: 1367,
      settings: {
        slidesToShow: 5,
        slidesToScroll: 5,
        infinite: categoryCount > 5,
      },
    },
    {
      breakpoint: 1024,
      settings: {
        slidesToShow: 4,
        slidesToScroll: 4,
        infinite: categoryCount > 4,
      },
    },
    {
      breakpoint: 767,
      settings: {
        slidesToShow: 3,
        slidesToScroll: 3,
        infinite: categoryCount > 3,
      },
    },
    {
      breakpoint: 480,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 2,
        infinite: categoryCount > 2,
      },
    },
  ],
});

const CategoryList = [
  { img: "/images/layout-1/rounded-cat/1.png", category: "Flower" },
  { img: "/images/layout-1/rounded-cat/2.png", category: "Furniture" },
  { img: "/images/layout-1/rounded-cat/3.png", category: "Bag" },
  { img: "/images/layout-1/rounded-cat/4.png", category: "Tools" },
  { img: "/images/layout-1/rounded-cat/5.png", category: "Grocery" },
  { img: "/images/layout-1/rounded-cat/6.png", category: "Camera" },
  { img: "/images/layout-1/rounded-cat/7.png", category: "cardigans" },
];

const PopularCategories = () => {
  const [PopularCategoriesList, setPopularCategories] = useState([]);
  const [adminPanelBaseURL, setBaseUrl] = useState(Config['ADMIN_BASE_URL']);
  const [LocalizationLabelsArray, setLocalizationLabelsArray] = useState([]);
  const [langCode, setLangCode] = useState('');

  useEffect(() => {
    // declare the data fetching function
    const getPopularCategories = async () => {

      //--Get language code
      let lnCode = getLanguageCodeFromSession();
      await setLangCode(lnCode);

      const headers = {
        // customerid: userData?.UserID,
        // customeremail: userData.EmailAddress,
        Accept: 'application/json',
        'Content-Type': 'application/json',

      }


      const param = {
        requestParameters: {
          PageNo: 1,
          PageSize: 20,
          recordValueJson: "[]",
        },
      };


      const response = await MakeApiCallAsync(Config.END_POINT_NAMES['GET_POPULAR_CATEGORIES'], null, param, headers, "POST", true);
      if (response != null && response.data != null) {
        const categoriesData = JSON.parse(response.data.data);
        const uniqueCategories = [...categoriesData.reduce((acc, item) => {
          const key = (item.Name ?? '').trim().toLowerCase();
          const existing = acc.get(key);
          if (!existing || (item.AttachmentURL?.trim() && !existing.AttachmentURL?.trim())) {
            acc.set(key, item);
          }
          return acc;
        }, new Map()).values()];
        setPopularCategories([...uniqueCategories].sort((a, b) => (a.Name ?? '').localeCompare(b.Name ?? '')));

      }

      //-- Get website localization data
      let arryRespLocalization = await GetLocalizationControlsJsonDataForScreen(GlobalEnums.Entities["PopularCategories"], null);
      if (arryRespLocalization != null && arryRespLocalization != undefined && arryRespLocalization.length > 0) {
        await setLocalizationLabelsArray(arryRespLocalization);
      }


    }

    // call the function
    getPopularCategories().catch(console.error);
  }, [])

  return (
    <>

      {
        PopularCategoriesList != undefined && PopularCategoriesList != null && PopularCategoriesList.length > 0
          ?
          <>
            <div className="title6 ">
              <h4>   {LocalizationLabelsArray.length > 0 ?
                replaceLoclizationLabel(LocalizationLabelsArray, " Popular Categories!", "lbl_popct_category")
                :
                " Popular Categories!"
              }
              </h4>
            </div>

            <section className="rounded-category rounded-category-inverse">
              <Container>
                <Row>
                  <Col>
                    <div className="slide-6 no-arrow">
                      <Slider {...getSettings(PopularCategoriesList.length)}>
                        {PopularCategoriesList && PopularCategoriesList.map((item, i) => {
                          const allProductsUrl = `/${getLanguageCodeFromSession()}/all-products/${item.CategoryID ?? 0}/${replaceWhiteSpacesWithDashSymbolInUrl(item.Name)}`;
                          return (
                          <div key={i}>
                            <div className="category-contain">
                              <Link to={allProductsUrl} style={{ color: 'inherit', textDecoration: 'none' }}>
                                <div className="img-wrapper">
                                  <Media src={item.AttachmentURL?.trim() ? makeImageUrl(adminPanelBaseURL, item.AttachmentURL) : fallbackCategoryImages[i % fallbackCategoryImages.length]} alt="category"
                                    style={{ width: "100%", height: "100%", objectFit: "cover" }} className=""
                                    onError={(e) => { e.target.onerror = null; e.target.src = fallbackCategoryImages[i % fallbackCategoryImages.length]; }}
                                    title={
                                      langCode != null && langCode == Config.LANG_CODES_ENUM["Arabic"]
                                        ?
                                        (item.LocalizationJsonData != null && item.LocalizationJsonData.length > 0
                                          ?
                                          makeAnyStringLengthShort(item.LocalizationJsonData?.find(l => l.langId == Config.LANG_CODES_IDS_ENUM["Arabic"])?.text, 22)
                                          :
                                          makeAnyStringLengthShort(item.Name, 22)
                                        )

                                        :
                                        makeAnyStringLengthShort(item.Name, 22)
                                    }

                                  />
                                </div>
                                <div>
                                  <div className="btn-rounded">

                                    {
                                      langCode != null && langCode == Config.LANG_CODES_ENUM["Arabic"]
                                        ?
                                        (item.LocalizationJsonData != null && item.LocalizationJsonData.length > 0
                                          ?
                                          makeAnyStringLengthShort(item.LocalizationJsonData?.find(l => l.langId == Config.LANG_CODES_IDS_ENUM["Arabic"])?.text, 22)
                                          :
                                          makeAnyStringLengthShort(item.Name, 22)
                                        )

                                        :
                                        makeAnyStringLengthShort(item.Name, 22)
                                    }

                                  </div>
                                </div>
                              </Link>
                            </div>
                          </div>
                          );
                        })}
                      </Slider>
                    </div>
                  </Col>
                </Row>
              </Container>
            </section>
          </>
          :
          <>
          </>
      }


    </>
  );
};

export default PopularCategories;