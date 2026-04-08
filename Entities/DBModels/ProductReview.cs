using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class ProductReview
    {
        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public decimal Rating { get; set; }
        public string? ReviewerName { get; set; }
        public string? ReviewerEmail { get; set; }
        public int? UserId { get; set; }
        public bool IsActive { get; set; }
        public DateTime ReviewDate { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
