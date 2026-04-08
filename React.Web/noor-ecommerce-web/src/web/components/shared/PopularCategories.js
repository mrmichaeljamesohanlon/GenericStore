import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

import { MakeApiCallSynchronous, MakeApiCallAsync } from '../../../helpers/ApiHelpers';
import { getLanguageCodeFromSession, GetLocalizationControlsJsonDataForScreen, replaceLoclizationLabel } from '../../../helpers/CommonHelper';

import Config from '../../../helpers/Config';
import { makeAnyStringLengthShort, replaceWhiteSpacesWithDashSymbolInUrl, makeImageUrl } from '../../../helpers/ConversionHelper';
import GlobalEnums from '../../../helpers/GlobalEnums';

import CPimg1 from '../../resources/themeContent/images/category-product-image/cp-svg1.svg';
import CPimg2 from '../../resources/themeContent/images/category-product-image/cp-svg2.svg';
import CPimg3 from '../../resources/themeContent/images/category-product-image/cp-svg3.svg';
import CPimg4 from '../../resources/themeContent/images/category-product-image/cp-svg4.svg';
import CPimg5 from '../../resources/themeContent/images/category-product-image/cp-svg5.svg';
import CPimg6 from '../../resources/themeContent/images/category-product-image/cp-svg6.svg';
import CPimg7 from '../../resources/themeContent/images/category-product-image/cp-svg7.svg';
import CPimg8 from '../../resources/themeContent/images/category-product-image/cp-svg8.svg';
import CPimg9 from '../../resources/themeContent/images/category-product-image/cp-svg9.svg';
import CPimg10 from '../../resources/themeContent/images/category-product-image/cp-svg10.svg';

const fallbackCategoryImages = [CPimg1, CPimg2, CPimg3, CPimg4, CPimg5, CPimg6, CPimg7, CPimg8, CPimg9, CPimg10];

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

                    recordValueJson: "[]",
                },
            };

            const response = await MakeApiCallAsync(Config.END_POINT_NAMES['GET_POPULAR_CATEGORIES'], null, param, headers, "POST", true);
            if (response != null && response.data != null) {
                const data = JSON.parse(response.data.data);
                const uniqueCategories = [...data.reduce((acc, item) => {
                    const key = (item.Name ?? '').trim().toLowerCase();
                    const existing = acc.get(key);
                    if (!existing || (item.AttachmentURL?.trim() && !existing.AttachmentURL?.trim())) {
                        acc.set(key, item);
                    }
                    return acc;
                }, new Map()).values()];
                setPopularCategories(uniqueCategories);
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
            <section className="category-boxes-area pt-60">
                <div className="container">
                    <div className="section-title">
                        <h2><span className="dot"></span> <span id="lbl_popct_category">

                            {LocalizationLabelsArray.length > 0 ?
                                replaceLoclizationLabel(LocalizationLabelsArray, " Popular Categories!", "lbl_popct_category")
                                :
                                " Popular Categories!"
                            }


                        </span></h2>
                    </div>

                    <div className="row">


                        {
                            PopularCategoriesList?.map((item, idx) =>

                                <div className="col-lg-3 col-sm-6">
                                    <div className="category-boxes">
                                        <img
                                            src={item.AttachmentURL?.trim() ? makeImageUrl(adminPanelBaseURL, item.AttachmentURL) : fallbackCategoryImages[idx % fallbackCategoryImages.length]}
                                            alt={item.Name ?? "category"}
                                            onError={(e) => { e.target.onerror = null; e.target.src = fallbackCategoryImages[idx % fallbackCategoryImages.length]; }}
                                        />
                                        <div className="content">
                                            <h3>
                                                {
                                                    
                                                    langCode != null && langCode == Config.LANG_CODES_ENUM["Arabic"]
                                                        ?
                                                        (item.LocalizationJsonData != null && item.LocalizationJsonData.length > 0
                                                            ?
                                                            makeAnyStringLengthShort(item.LocalizationJsonData?.find(l => l.langId == Config.LANG_CODES_IDS_ENUM["Arabic"])?.text, 27)
                                                            :
                                                            makeAnyStringLengthShort(item.Name, 27)
                                                        )

                                                        :
                                                        makeAnyStringLengthShort(item.Name, 27)
                                                }
                                            </h3>
                                            <p ><span style={{ display: "inline-block" }}>{item.TotalProducts} &nbsp;</span>
                                                <span style={{ display: "inline-block" }} id="lbl_popct_prd">

                                                    {LocalizationLabelsArray.length > 0 ?
                                                        replaceLoclizationLabel(LocalizationLabelsArray, "Products", "lbl_popct_prd")
                                                        :
                                                        "Products"
                                                    }

                                                </span>

                                            </p>



                                            {(() => {


                                                let allProductsUrl = `/${getLanguageCodeFromSession()}/all-products/${item.CategoryID ?? 0}/${replaceWhiteSpacesWithDashSymbolInUrl(item.Name)}`
                                                return (
                                                    <>
                                                        <Link to={allProductsUrl} className="shop-now-btn" id="lbl_popct_shopnow">

                                                            {LocalizationLabelsArray.length > 0 ?
                                                                replaceLoclizationLabel(LocalizationLabelsArray, "Shop Now", "lbl_popct_shopnow")
                                                                :
                                                                "Shop Now"
                                                            }



                                                        </Link>
                                                    </>
                                                );
                                            })()}




                                        </div>
                                    </div>
                                </div>


                            )}




                    </div>
                </div>
            </section>
        </>
    );

}


export default PopularCategories;
