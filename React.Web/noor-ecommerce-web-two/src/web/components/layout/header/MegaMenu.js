import React from "react";
import { Container, Row, Col } from "reactstrap";
import MenuCategory from "./MenuCategory";
import HorizaontalMenu from "./HorizaontalMenu";
import MobilePrimaryNav from "./MobilePrimaryNav";
import MobileSearch from "./MobileSearch";
import MenuContactUs from "./MenuContactUs";
import MenuGift from "./MenuGift";
import Wishlist from "./Wishlist";
import useMobileSize from "../../../../helpers/utils/isMobile";

const MegaMenu = () => {
    const mobileSize = useMobileSize();

    return (
        <>
            <div className="custom-container">
                <Row>
                    <Col>
                        <div className="navbar-menu">
                            <div className="category-left">
                                <MenuCategory />
                                {mobileSize ? <MobilePrimaryNav /> : <HorizaontalMenu />}
                                <div className="icon-block">
                                    <ul>
                                        <Wishlist />
                                        <MobileSearch />
                                    </ul>
                                </div>
                            </div>
                            <div className="category-right">
                                <MenuContactUs spanClass="" />
                                <MenuGift />
                            </div>
                        </div>
                    </Col>
                </Row>
            </div>
        </>
    );
};

export default MegaMenu;
