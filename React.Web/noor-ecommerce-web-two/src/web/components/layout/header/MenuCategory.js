import React, { Fragment, useContext, useEffect, Component, useState } from "react";
import { Container, Row, Col } from "reactstrap";
import { useSelector, useDispatch } from 'react-redux';
import { Link, useNavigate } from 'react-router-dom';
import { useTranslation } from "react-i18next";
import Config from "../../../../helpers/Config";
import { MakeApiCallAsync } from "../../../../helpers/ApiHelpers";
import { getLanguageCodeFromSession, GetLocalizationControlsJsonDataForScreen, replaceLoclizationLabel } from "../../../../helpers/CommonHelper";
import GlobalEnums from "../../../../helpers/GlobalEnums";
import { makeAnyStringLengthShort, makeProductShortDescription, replaceWhiteSpacesWithDashSymbolInUrl } from "../../../../helpers/ConversionHelper";
import rootAction from "../../../../stateManagment/actions/rootAction";


const MenuCategory = () => {
    const dispatch = useDispatch();
    const [showState, setShowState] = useState(false);
    const [expandedCategories, setExpandedCategories] = useState({});

    const toggleSubCategories = (e, categoryId) => {
        e.preventDefault();
        e.stopPropagation();
        setExpandedCategories(prev => ({ ...prev, [categoryId]: !prev[categoryId] }));
    };

    const { t } = useTranslation();

    let leftMenuState = useSelector(state => state.commonReducer.isLeftMenuSet);


    const setLeftMenuManual = (value) => {
        dispatch(rootAction.commonAction.setLeftMenu(value));
    }


    const [CategoriesList, setCategoriesList] = useState([]);
    const [langCode, setLangCode] = useState('');
    const [LocalizationLabelsArray, setLocalizationLabelsArray] = useState([]);

    const forceLoadCategory = (url) => {
        window.location.href = url;
    }


    useEffect(() => {
        // declare the data fetching function
        const getCategories = async () => {

            //--Get language code
            let lnCode = getLanguageCodeFromSession();
            await setLangCode(lnCode);

            const headers = {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            }


            const param = {
                requestParameters: {
                    PageNo: 1,
                    PageSize: 100,
                    recordValueJson: "[]",
                },
            };


            const response = await MakeApiCallAsync(Config.END_POINT_NAMES['GET_CATEGORIES_LIST'], null, param, headers, "POST", true);
            if (response != null && response.data != null) {
                const categoriesData = JSON.parse(response.data.data);
                const uniqueCategories = [...new Map(categoriesData.map(item => [item.CategoryID, item])).values()];
                setCategoriesList(uniqueCategories);

            }

            //-- Get website localization data
            let arryRespLocalization = await GetLocalizationControlsJsonDataForScreen(GlobalEnums.Entities["PopularCategories"], null);
            if (arryRespLocalization != null && arryRespLocalization != undefined && arryRespLocalization.length > 0) {
                await setLocalizationLabelsArray(arryRespLocalization);
            }

        }

        // call the function
        getCategories().catch(console.error);
    }, [])

    const getCategoryDisplayName = (item) => {
        if (langCode != null && langCode == Config.LANG_CODES_ENUM["Arabic"]) {
            return (item.LocalizationJsonData != null && item.LocalizationJsonData.length > 0
                ? makeAnyStringLengthShort(item.LocalizationJsonData?.find(l => l.langId == Config.LANG_CODES_IDS_ENUM["Arabic"])?.text, 22)
                : makeAnyStringLengthShort(item.Name, 17)
            );
        }
        return makeAnyStringLengthShort(item.Name, 17);
    };

    const sortByName = (a, b) => (a.Name ?? '').localeCompare(b.Name ?? '', langCode || undefined);

    const parentCategories = CategoriesList
        .filter(x => x.ParentCategoryID == null)
        .sort(sortByName);

    const getSubCategories = (parentId) =>
        CategoriesList
            .filter(x => x.ParentCategoryID === parentId)
            .sort(sortByName);


    return (
        <>
            <div className="nav-block" onClick={() => setShowState(!showState)}>
                <div className="nav-left">
                    <nav className="navbar" data-toggle="collapse" data-target="#navbarToggleExternalContent">
                        <button className="navbar-toggler" type="button" onClick={() => setShowState(!showState)}>
                            <span className="navbar-icon">
                                <i className="fa fa-arrow-down"></i>
                            </span>
                        </button>
                        <h5 className="mb-0  text-white title-font">
                            {LocalizationLabelsArray.length > 0 ?
                                replaceLoclizationLabel(LocalizationLabelsArray, " shop By Category", "lbl_shopby_category")
                                :
                                "shop By Category"
                            }
                        </h5>
                    </nav>
                    <div className={`collapse  nav-desk ${showState ? "show" : ""}`} id="navbarToggleExternalContent">
                        <a
                            href="#"
                            onClick={() => {
                                setLeftMenuManual(!leftMenuState);
                                document.body.style.overflow = "visible";
                            }}
                            className={`overlay-cat ${leftMenuState ? "showoverlay" : ""}`}></a>
                        <ul className={`nav-cat title-font ${leftMenuState ? "openmenu" : ""}`}>
                            <li
                                className="back-btn"
                                onClick={() => {
                                    setLeftMenuManual(false);
                                    document.body.style.overflow = "visible";
                                }}>
                                <a>
                                    <i className="fa fa-angle-left"></i>Back
                                </a>
                            </li>

                            {parentCategories.map((item, i) => {
                                const subCategories = getSubCategories(item.CategoryID);
                                const hasChildren = subCategories.length > 0;
                                const isExpanded = !!expandedCategories[item.CategoryID];
                                return (
                                    <li key={i}>
                                        <Link to="#"
                                            onClick={(e) => {
                                                e.preventDefault();
                                                e.stopPropagation();
                                                forceLoadCategory(`/${getLanguageCodeFromSession()}/all-products/${item.CategoryID ?? 0}/${replaceWhiteSpacesWithDashSymbolInUrl(item.Name)}`);
                                            }}
                                        >
                                            {getCategoryDisplayName(item)}
                                        </Link>
                                        {hasChildren && (
                                            <span
                                                onClick={(e) => toggleSubCategories(e, item.CategoryID)}
                                                style={{ cursor: 'pointer', marginLeft: '6px' }}
                                                title={isExpanded ? 'Hide subcategories' : 'Show subcategories'}
                                            >
                                                <i className={`fa ${isExpanded ? 'fa-angle-up' : 'fa-angle-down'}`}></i>
                                            </span>
                                        )}
                                        {hasChildren && isExpanded && (
                                            <ul className="nav-sub-cat">
                                                {subCategories.map((child, j) => (
                                                    <li key={j}>
                                                        <Link to="#"
                                                            onClick={(e) => {
                                                                e.preventDefault();
                                                                e.stopPropagation();
                                                                forceLoadCategory(`/${getLanguageCodeFromSession()}/all-products/${child.CategoryID ?? 0}/${replaceWhiteSpacesWithDashSymbolInUrl(child.Name)}`);
                                                            }}
                                                        >
                                                            {getCategoryDisplayName(child)}
                                                        </Link>
                                                    </li>
                                                ))}
                                            </ul>
                                        )}
                                    </li>
                                );
                            })}

                        </ul>
                    </div>
                </div>
            </div>
        </>
    );

}


export default MenuCategory;
