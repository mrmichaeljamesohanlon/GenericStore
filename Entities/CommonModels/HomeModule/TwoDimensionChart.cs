using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.CommonModels.HomeModule
{
    public class TwoDimensionChart
    {
        public string? ChartLabel { get; set; }
        public string? ChartValue { get; set; }
        public int? Year { get; set; }
        public int? MonthInNumber { get; set; }
    }
}
