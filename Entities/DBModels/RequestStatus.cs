using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class RequestStatus
    {
        public RequestStatus()
        {
            RequestsQueues = new HashSet<RequestsQueue>();
        }

        public int RequestStatusId { get; set; }
        public string StatusKey { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual ICollection<RequestsQueue> RequestsQueues { get; set; }
    }
}
