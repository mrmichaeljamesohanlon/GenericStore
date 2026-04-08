using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class User
    {
        public User()
        {
            DiscountUsageHistories = new HashSet<DiscountUsageHistory>();
            OrderShippingDetails = new HashSet<OrderShippingDetail>();
            Orders = new HashSet<Order>();
            Otplogs = new HashSet<Otplog>();
            Products = new HashSet<Product>();
            UserRoles = new HashSet<UserRole>();
            VendorsCommissionSetups = new HashSet<VendorsCommissionSetup>();
        }

        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string? UserName { get; set; }
        public string EmailAddress { get; set; } = null!;
        public string? Password { get; set; }
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public int? ProfilePictureId { get; set; }
        public bool IsVerified { get; set; }
        public string? VerificationCode { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsTermAccepted { get; set; }
        public bool? IsPrivacyPolicyAccepted { get; set; }
        public bool? IsNewsLetterAccepted { get; set; }
        public int? CountryId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual Attachment? ProfilePicture { get; set; }
        public virtual UserType UserType { get; set; } = null!;
        public virtual ICollection<DiscountUsageHistory> DiscountUsageHistories { get; set; }
        public virtual ICollection<OrderShippingDetail> OrderShippingDetails { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Otplog> Otplogs { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<VendorsCommissionSetup> VendorsCommissionSetups { get; set; }
    }
}
