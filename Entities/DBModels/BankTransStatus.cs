using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class BankTransStatus
    {
        public int TransStatusId { get; set; }
        public string TransStatusName { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
