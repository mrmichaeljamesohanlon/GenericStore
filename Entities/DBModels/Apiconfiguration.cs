using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class Apiconfiguration
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? UrlName { get; set; }
        public string? MethodType { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string SqlQuery { get; set; } = null!;
        public string? DefaultParams { get; set; }
        public bool? IsActive { get; set; }
        public string? UrlParams { get; set; }
        public string? DataParams { get; set; }
        public string? SuccessResponse { get; set; }
        public string? ErrorResponse { get; set; }
        public string? SampleCall { get; set; }
        public string? Notes { get; set; }
        public string? Ormtype { get; set; }
        public bool? IsFurtherCalculationRequired { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsCacheAllowed { get; set; }
        public bool? IsDynamicFormField { get; set; }
        public bool? IsEmail { get; set; }
        public bool? IsNotification { get; set; }
        public bool? IsAuthorizationNeeded { get; set; }
    }
}
