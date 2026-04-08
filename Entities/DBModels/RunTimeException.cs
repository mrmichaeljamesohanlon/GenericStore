using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class RunTimeException
    {
        public int ExceptionId { get; set; }
        public string ExceptionMessage { get; set; } = null!;
        public string? StackTrace { get; set; }
        public string? Source { get; set; }
        public string? StatusCode { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
