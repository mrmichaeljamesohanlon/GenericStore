import React from 'react';

const services = [
    {
        icon: 'fas fa-shipping-fast',
        badge: 'Worldwide',
        title: 'Free Shipping',
        description: 'Free shipping world wide on all orders, no minimum spend required. Fast and reliable delivery straight to your door.',
    },
    {
        icon: 'fas fa-headset',
        badge: '24 × 7',
        title: '24 × 7 Service',
        description: 'Round-the-clock customer support. Our team is available any time of day or night to assist you with any enquiry.',
    },
    {
        icon: 'fas fa-user-plus',
        badge: 'New Customers',
        title: 'Online Service',
        description: 'Exclusive online service for new customers. Get personalised help to set up your account, browse products and place your first order with ease.',
    },
    {
        icon: 'fas fa-gift',
        badge: 'Limited Time',
        title: 'Festival Offer',
        description: 'New online special festival offer — enjoy bumper seasonal discounts, bundle deals, and flash sales exclusively for our online shoppers.',
    },
];

const paymentTypes = [
    { icon: 'fas fa-money-bill-wave', label: 'Cash on Delivery' },
    { icon: 'far fa-credit-card', label: 'Credit Card' },
    { icon: 'fas fa-credit-card', label: 'Debit Card' },
    { icon: 'fab fa-paypal', label: 'PayPal' },
    { icon: 'fas fa-university', label: 'Bank Transfer' },
    { icon: 'fab fa-apple-pay', label: 'Apple Pay' },
    { icon: 'fab fa-google-pay', label: 'Google Pay' },
    { icon: 'fas fa-wallet', label: 'Digital Wallet' },
];

const ServicesInfoPanel = () => {
    return (
        <section className="services-info-panel">
            <div className="container">

                {/* Panel header */}
                <div className="panel-header">
                    <h2>Why Shop With Us?</h2>
                    <div className="header-divider"></div>
                    <p>Everything you need for a great shopping experience — in one place</p>
                </div>

                {/* Service cards */}
                <div className="row g-4">
                    {services.map((svc, idx) => (
                        <div className="col-lg-3 col-sm-6" key={idx}>
                            <div className="service-card">
                                <div className="service-icon">
                                    <i className={svc.icon}></i>
                                </div>
                                <span className="service-badge">{svc.badge}</span>
                                <h4>{svc.title}</h4>
                                <p>{svc.description}</p>
                            </div>
                        </div>
                    ))}
                </div>

                {/* Online Payment section */}
                <div className="payment-section">
                    <h3>
                        <i className="fas fa-lock"></i>
                        Online Payment
                    </h3>

                    <p>
                        Contrary to popular belief, online payment is not limited to cards or digital wallets.
                        We support all major payment methods so you can check out your way — whether you
                        prefer to pay digitally or keep it traditional. We are proud to offer{' '}
                        <strong style={{ color: '#FFD600' }}>Cash on Delivery</strong> for customers who would
                        rather pay when their order arrives at the door. Every transaction on our platform is
                        protected by industry-standard SSL encryption and trusted payment gateways, so your
                        financial details stay private at every step. No hidden fees, no surprises — just a
                        fast, secure, and flexible checkout experience from start to finish.
                    </p>

                    <div className="payment-types-grid">
                        {paymentTypes.map((pt, idx) => (
                            <div className="payment-type-badge" key={idx}>
                                <i className={pt.icon}></i>
                                {pt.label}
                            </div>
                        ))}
                    </div>
                </div>

            </div>
        </section>
    );
};

export default ServicesInfoPanel;
