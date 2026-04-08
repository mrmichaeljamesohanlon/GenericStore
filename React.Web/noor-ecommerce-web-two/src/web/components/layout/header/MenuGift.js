import React, { useState } from "react";
import { Media } from "reactstrap";
import icon1 from "../../../../resources/themeContent/images/icon/1.png";
import icon2 from "../../../../resources/themeContent/images/icon/2.png";
import icon3 from "../../../../resources/themeContent/images/icon/3.png";
import icon5 from "../../../../resources/themeContent/images/icon/5.png";
import icon6 from "../../../../resources/themeContent/images/icon/6.png";
import iconFire from "../../../../resources/themeContent/images/icon/fire.png";
import iconGift from "../../../../resources/themeContent/images/icon/gift-card.png";

const giftData = [
  {
    img1: icon1,
    img2: iconFire,
    title: "Workshop Special",
    desc: "Free inspection with any service",
  },
  {
    img1: icon2,
    img2: iconFire,
    title: "Parts Discount",
    desc: "10% off performance parts",
  },
  {
    img1: icon3,
    img2: "",
    title: "Hire Package",
    desc: "First hire 5% off - no code needed",
  },
  {
    img1: icon6,
    img2: "",
    title: "Race Prep Deal",
    desc: "Save on full car preparation",
  },
  {
    img1: icon5,
    img2: iconFire,
    title: "Loyalty Reward",
    desc: "Earn points on every purchase",
  },
];

const GiftList = (props) => {
  return (
    <div className="media">
      <div className="me-3">
        <Media src={props.gift.img1} alt={props.gift.title} />
      </div>
      <div className="media-body">
        <h5 className="mt-0">{props.gift.title}</h5>
        <div>
          {props.gift.img2 ? (
            <Media src={props.gift.img2} className="cash" alt="offer" />
          ) : null}
          {props.gift.desc}
        </div>
      </div>
    </div>
  );
};

const MenuGift = () => {
  const [showState, setShowState] = useState(false);
  return (
    <div style={{ position: "relative" ,visibility:"hidden"}}>
      <button
        className="gift-block"
        onClick={() => setShowState(!showState)}
        aria-expanded={showState}
        aria-label="View current offers"
      >
        <div className="grif-icon">
          <Media src={iconGift} alt="gift" style={{ width: "35px" }} />
        </div>
        <div className="gift-offer" style={{ visibility: "hidden" }}>
          <p>offer for you</p>
          <span>Motorsport Deals</span>
        </div>
      </button>
      {showState && (
        <div
          className="gift-dropdown"
          style={{
            position: "absolute",
            right: 0,
            top: "100%",
            background: "#fff",
            boxShadow: "0 4px 16px rgba(0,0,0,0.15)",
            padding: "16px",
            zIndex: 999,
            minWidth: "280px",
          }}
        >
          {giftData.map((gift, i) => (
            <div key={i} style={{ marginBottom: i < giftData.length - 1 ? "12px" : 0 }}>
              <GiftList gift={gift} />
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default MenuGift;
