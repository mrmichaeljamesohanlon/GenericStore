using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class Otplog
    {
        public int OtplogId { get; set; }
        public int StatusCode { get; set; }
        public string? StatusMsg { get; set; }
        public string? CustomMsg { get; set; }
        public int Otp { get; set; }
        public bool IsActive { get; set; }
        public string? PhoneNo { get; set; }
        public string? EmailAddress { get; set; }
        public int? UserId { get; set; }
        public int? NoOfAttempts { get; set; }
        public bool AllowMultipleAttemps { get; set; }
        public string? JsonResponse { get; set; }
        public DateTime OtpcreatedOn { get; set; }
        public DateTime? LastAttmptCreatedOn { get; set; }

        public virtual User? User { get; set; }
    }
}
