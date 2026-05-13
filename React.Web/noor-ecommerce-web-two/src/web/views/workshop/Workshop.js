import React from "react";
import { Helmet } from "react-helmet-async";
import { Container, Row, Col } from "reactstrap";
import { Link } from "react-router-dom";
import Config from "../../../helpers/Config";
import { getLanguageCodeFromSession } from "../../../helpers/CommonHelper";

/**
 * Workshop = services / preparation — not the ecommerce product catalogue.
 * Shop routes stay under /all-products; this page is informational only.
 */
const Workshop = () => {
  const lang = getLanguageCodeFromSession();
  const shopHref = `/${lang}/all-products/0/all-categories`;

  return (
    <>
      <Helmet>
        <title>{Config["SITE_TTILE"]} - Workshop</title>
        <meta
          name="description"
          content="Workshop services, race preparation, and hire — separate from our online parts shop."
        />
      </Helmet>

      <div className="workshop-page bg-light">
        <section className="workshop-hero">
          <Container>
            <Row className="align-items-center py-5">
              <Col lg="8" md="12">
                <p className="workshop-hero-eyebrow">Workshop</p>
                <h1 className="workshop-hero-title">Race preparation &amp; workshop services</h1>
                <p className="workshop-hero-lead">
                  Our workshop covers inspection, setup, hire packages, and track-side support. This is
                  not our online store — browse and buy parts in the shop only.
                </p>
                <p className="workshop-hero-note">
                  <strong>Shop</strong> is for products (parts, helmets, consumables).{" "}
                  <strong>Workshop</strong> is for what we do in the garage and at the circuit.
                </p>
                <div className="d-flex flex-wrap gap-2 mt-4">
                  <a href="tel:+441234567890" className="btn btn-dark btn-lg rounded-0">
                    Call the workshop
                  </a>
                  <Link to={`/${lang}/contact-us`} className="btn btn-outline-dark btn-lg rounded-0">
                    Enquire
                  </Link>
                  <Link to={shopHref} className="btn btn-outline-secondary btn-lg rounded-0">
                    Go to shop (products)
                  </Link>
                </div>
              </Col>
            </Row>
          </Container>
        </section>

        <Container className="py-5">
          <Row>
            <Col md="6" className="mb-4">
              <h2 className="h4 fw-bold text-uppercase mb-3">What we offer</h2>
              <ul className="workshop-list">
                <li>Pre-event safety checks and nut &amp; bolt inspections</li>
                <li>Corner weighting, geometry, and damper baseline setup</li>
                <li>Race car hire packages (availability on request)</li>
                <li>Track-day support and logistics (by arrangement)</li>
              </ul>
            </Col>
            <Col md="6" className="mb-4">
              <h2 className="h4 fw-bold text-uppercase mb-3">Looking for parts?</h2>
              <p className="text-muted">
                Consumables, safety gear, and performance parts are sold through the online shop — same
                checkout and delivery as any other order.
              </p>
              <Link to={shopHref} className="btn btn-warning btn-lg rounded-0 fw-bold text-dark">
                Browse the shop
              </Link>
            </Col>
          </Row>
        </Container>
      </div>
    </>
  );
};

export default Workshop;
