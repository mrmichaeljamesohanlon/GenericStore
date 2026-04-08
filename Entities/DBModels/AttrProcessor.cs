using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class AttrProcessor
    {
        public int ProcessorId { get; set; }
        public string ProcessorName { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? MofiedBy { get; set; }
    }
}
