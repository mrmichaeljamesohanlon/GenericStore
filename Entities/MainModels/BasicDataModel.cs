using Entities.CommonModels;
using Entities.DBInheritedModels;
using Entities.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.MainModels
{
    public class BasicDataModel
    {
        public Product? productsObj { get; set; }
        public List<ColorEntity>? ColorsList { get; set; }
        public List<CategoryEntity>? CategoryList { get; set; }
        public List<CategoryEntity>? ParentCategoryList { get; set; }
        public List<SizeEntity>? SizeList { get; set; }
        public List<ManufacturerEntity>? ManufacturerList { get; set; }
        public List<PaymentMethodEntity>? PaymentMethodsList { get; set; }
        public List<CurrencyEntity>? CurrenciesList { get; set; }
        public List<AttachmentTypeEntity>? AttachmentTypeList { get; set; }
        public List<TagEntity>? TagsList { get; set; }

        public PageBasicInfo? PageBasicInfoObj { get; set; }
        public PagerHelper? pageHelperObj { get; set; }
        public SuccessErrorMsgEntity? SuccessErrorMsgEntityObj { get; set; }
    }
}
