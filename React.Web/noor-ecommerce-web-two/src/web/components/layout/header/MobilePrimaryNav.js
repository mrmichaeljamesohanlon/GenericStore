import React, { useEffect, useState } from "react";
import { NavLink } from "react-router-dom";
import {
  getLanguageCodeFromSession,
  GetLocalizationControlsJsonDataForScreen,
  replaceLoclizationLabel,
} from "../../../../helpers/CommonHelper";
import GlobalEnums from "../../../../helpers/GlobalEnums";

/**
 * Slim text links on small screens — no second hamburger.
 * Categories stay on the dedicated Categories control (MenuCategory).
 */
const MobilePrimaryNav = () => {
  const lang = getLanguageCodeFromSession();
  const [labels, setLabels] = useState([]);

  useEffect(() => {
    const load = async () => {
      const arry = await GetLocalizationControlsJsonDataForScreen(GlobalEnums.Entities["MegaMenu"], null);
      if (arry?.length) setLabels(arry);
    };
    load().catch(console.error);
  }, []);

  const L = (fallback, key) =>
    labels.length > 0 ? replaceLoclizationLabel(labels, fallback, key) : fallback;

  const cls = ({ isActive }) =>
    `mobile-primary-nav__link${isActive ? " mobile-primary-nav__link--active" : ""}`;

  return (
    <nav className="mobile-primary-nav" aria-label="Main">
      <NavLink to={`/${lang}/`} end className={cls}>
        {L("Home", "lbl_mgmenu_home")}
      </NavLink>
      <span className="mobile-primary-nav__sep" aria-hidden="true">
        |
      </span>
      <NavLink to={`/${lang}/all-products/0/all-categories`} className={cls}>
        Shop
      </NavLink>
      <span className="mobile-primary-nav__sep" aria-hidden="true">
        |
      </span>
      <NavLink to={`/${lang}/workshop`} className={cls}>
        Workshop
      </NavLink>
      <span className="mobile-primary-nav__sep" aria-hidden="true">
        |
      </span>
      <NavLink to={`/${lang}/contact-us`} className={cls}>
        {L("Contact", "lbl_thead_contct")}
      </NavLink>
    </nav>
  );
};

export default MobilePrimaryNav;
