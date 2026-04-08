using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class ProductAttributeColumn
    {
        public int ProductAttributeColumnId { get; set; }
        public int ProductAttributeId { get; set; }
        public string ColumnName { get; set; } = null!;
        public string? ColumnType { get; set; }
    }
}
