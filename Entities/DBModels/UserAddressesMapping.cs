using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class UserAddressesMapping
    {
        public int UserAddressMappingId { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
