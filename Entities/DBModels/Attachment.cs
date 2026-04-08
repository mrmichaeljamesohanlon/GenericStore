using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class Attachment
    {
        public Attachment()
        {
            AccountTransAttachments = new HashSet<AccountTransAttachment>();
            BankAccountAttachments = new HashSet<BankAccountAttachment>();
            Categories = new HashSet<Category>();
            Countries = new HashSet<Country>();
            HomeScreenBanners = new HashSet<HomeScreenBanner>();
            ProductDigitalFileMappings = new HashSet<ProductDigitalFileMapping>();
            ProductPicturesMappings = new HashSet<ProductPicturesMapping>();
            Users = new HashSet<User>();
        }

        public int AttachmentId { get; set; }
        public string AttachmentName { get; set; } = null!;
        public int AttachmentTypeId { get; set; }
        public string? AttachmentUrl { get; set; }
        public byte[]? ByteArrayAttachment { get; set; }
        public bool IsByteArray { get; set; }
        public string? SeoName { get; set; }
        public string? AltAttribute { get; set; }
        public string? TitleAttribute { get; set; }
        public string? MimeType { get; set; }
        public string? Guid { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsCommonImageUpload { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }

        public virtual AttachmentType AttachmentType { get; set; } = null!;
        public virtual ICollection<AccountTransAttachment> AccountTransAttachments { get; set; }
        public virtual ICollection<BankAccountAttachment> BankAccountAttachments { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
        public virtual ICollection<HomeScreenBanner> HomeScreenBanners { get; set; }
        public virtual ICollection<ProductDigitalFileMapping> ProductDigitalFileMappings { get; set; }
        public virtual ICollection<ProductPicturesMapping> ProductPicturesMappings { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
