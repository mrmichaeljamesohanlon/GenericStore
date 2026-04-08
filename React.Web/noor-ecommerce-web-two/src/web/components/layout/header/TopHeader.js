import React, { useContext, useEffect, useState, Component } from "react";
import { Row, Col, Dropdown, DropdownToggle } from "reactstrap";
import { useTranslation } from "react-i18next";
import GlobalEnums from '../../../../helpers/GlobalEnums';
import { getLanguageCodeFromSession, GetLocalizationControlsJsonDataForScreen, replaceLoclizationLabel, setLanguageCodeInSession } from '../../../../helpers/CommonHelper';
import { Link } from 'react-router-dom';

const langCodeArray = [
  {
    langCode: "en",
    name: "English"
  }
]


const TopHeader = () => {

  const { i18n, t } = useTranslation();
  const [openLang, setOpenLang] = useState(false);
  const [url, setUrl] = useState("");
  const toggleLang = () => {
    setOpenLang(!openLang);
  };

  useEffect(() => {
    const path = window.location.pathname.split("/");
    const urlTemp = path[path.length - 1];
    setUrl(urlTemp);
  }, []);


  const [langCode, setLangCode] = useState('');
  const [LocalizationLabelsArray, setLocalizationLabelsArray] = useState([]);
  const handleLangCodeInSession = async (value) => {

    await setLanguageCodeInSession(value);
    await setLangCode(value);

    let homeUrl = '/' + value + '/';
    window.location.href = homeUrl;
    // navigate(homeUrl, { replace: true });
  }

  useEffect(() => {
    // declare the data fetching function
    const dataOperationFunc = async () => {
      let lnCode = getLanguageCodeFromSession();
      setLangCode(lnCode);

      //-- Get website localization data
      let arryRespLocalization = await GetLocalizationControlsJsonDataForScreen(GlobalEnums.Entities["TopHeader"], null);
      if (arryRespLocalization != null && arryRespLocalization != undefined && arryRespLocalization.length > 0) {
        await setLocalizationLabelsArray(arryRespLocalization);
      }
    }
    // call the function
    dataOperationFunc().catch(console.error);
  }, [])




  return (
    <div className={`top-header ${url === "layout6" ? "top-header-inverse" : ""}`} style={{ background: '#FFD600', minHeight: '10px' }}>
      {/* Yellow band only, all content removed as requested */}
    </div>
  );

}


export default TopHeader;
