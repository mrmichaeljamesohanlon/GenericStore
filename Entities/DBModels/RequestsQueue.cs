using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class RequestsQueue
    {
        public RequestsQueue()
        {
            OrderRefundRequests = new HashSet<OrderRefundRequest>();
            VendorsAccountRequests = new HashSet<VendorsAccountRequest>();
        }

        public int TaskId { get; set; }
        public int RequestTypeId { get; set; }
        public int RequestStatusId { get; set; }
        public string? Comment { get; set; }
        public int? ReferenceId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? StatusChangeDate { get; set; }

        public virtual RequestStatus RequestStatus { get; set; } = null!;
        public virtual RequestType RequestType { get; set; } = null!;
        public virtual ICollection<OrderRefundRequest> OrderRefundRequests { get; set; }
        public virtual ICollection<VendorsAccountRequest> VendorsAccountRequests { get; set; }
    }
}
