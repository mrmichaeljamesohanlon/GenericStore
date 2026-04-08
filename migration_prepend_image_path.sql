-- This script fixes and normalises AttachmentURL values for product images (AttachmentTypeID = 1).
-- Only updates records where AttachmentTypeID = 1 (product images) and the URL is a bare filename (no leading /content/ path)
-- Run in ECommerceShopDB

-- Step 1: Remediate any previously corrupted URLs where the productImages prefix was incorrectly
-- prepended to a URL that already contained a full /content/ path (e.g. /content/commonImages/otherImages/...)
-- This covers ALL attachment types to ensure category images, campaign images, logos, etc. are also fixed.
UPDATE ECommerceShopDB.dbo.Attachments
SET AttachmentURL = SUBSTRING(AttachmentURL, LEN('/content/commonImages/productImages/') + 1, LEN(AttachmentURL))
WHERE AttachmentURL LIKE '/content/commonImages/productImages//content/%';

-- Step 2: Prepend the productImages directory only for bare filenames that have no path prefix yet
UPDATE ECommerceShopDB.dbo.Attachments
SET AttachmentURL = '/content/commonImages/productImages/' + AttachmentURL
WHERE AttachmentTypeID = 1
  AND AttachmentURL IS NOT NULL
  AND AttachmentURL NOT LIKE '/content/%';
