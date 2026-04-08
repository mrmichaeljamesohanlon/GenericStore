using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class ProductPicturesMapping
    {
        public int ProductPictureMappingId { get; set; }
        public int ProductId { get; set; }
        public int PictureId { get; set; }
        public int? ColorId { get; set; }

        public virtual Color? Color { get; set; }
        public virtual Attachment Picture { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
