using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class AddressType
    {
        public AddressType()
        {
            UserAddresses = new HashSet<UserAddress>();
            VendorsAccountRequestAddressOneTypes = new HashSet<VendorsAccountRequest>();
            VendorsAccountRequestAddressTwoTypes = new HashSet<VendorsAccountRequest>();
        }

        public int AddressTypeId { get; set; }
        public string AddressTypeName { get; set; } = null!;
        public bool? IsActive { get; set; }
        public decimal? DisplaySeqNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual ICollection<UserAddress> UserAddresses { get; set; }
        public virtual ICollection<VendorsAccountRequest> VendorsAccountRequestAddressOneTypes { get; set; }
        public virtual ICollection<VendorsAccountRequest> VendorsAccountRequestAddressTwoTypes { get; set; }
    }
}
