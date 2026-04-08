using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class ContactU
    {
        public int ContactId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Subject { get; set; }
        public string Message { get; set; } = null!;
        public bool? IsRead { get; set; }
        public int? ReadBy { get; set; }
        public int? Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
