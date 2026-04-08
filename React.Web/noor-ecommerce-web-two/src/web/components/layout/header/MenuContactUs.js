import React from "react";


const MenuContactUs = (spanClass) => {
  return (
    <div className={`d-xxl-flex d-none ${spanClass !== "contact-item" ? "contact-block" : ""}`}>
      <div className="d-flex">
        <i className="fa fa-volume-control-phone"></i>
        <span className={spanClass}>
          call us <span>Your Phone Number</span>
        </span>
      </div>
    </div>
  );
};

export default MenuContactUs;
