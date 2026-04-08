import React from 'react';
import Config from './Config';
import { checkIfStringIsEmtpy } from './ValidationHelper';

export const makeProductShortDescription = (inputString, length) => {

  length = parseInt(length) ?? 50;

  if (inputString != undefined && inputString != null && inputString.length > 0) {
    let strippedString = typeof window !== 'undefined'
      ? (new DOMParser().parseFromString(inputString, 'text/html').body.textContent || '').trim()
      : inputString.replace(/<[^>]*>/g, '').trim();
    let newString = strippedString.length > length ? (strippedString.substring(0, length) + '...') : (strippedString.substring(0, length))
    return newString;
  } else {
    return "";
  }

}

export const makeAnyStringLengthShort = (inputString, length) => {

  length = parseInt(length) ?? 50;

  if (inputString != undefined && inputString != null && inputString.length > 0) {
    let newString = inputString.length > length ? (inputString.substring(0, length) + '...') : (inputString.substring(0, length))
    return newString;
  } else {
    return "";
  }


}

const normalizeUrlSlugPart = (inputString) => {
  if (inputString == undefined || inputString == null) {
    return undefined;
  }

  const normalizedInput = `${inputString}`.trim();

  if (!normalizedInput) {
    return undefined;
  }

  const lowerCaseInput = normalizedInput.toLowerCase();
  if (lowerCaseInput === 'undefined' || lowerCaseInput === 'null') {
    return undefined;
  }

  return normalizedInput;
}


export const replaceWhiteSpacesWithDashSymbolInUrl = (inputString) => {
  const normalizedInput = normalizeUrlSlugPart(inputString);
  if (normalizedInput != undefined) {

    //--replace extra space with one space
    let newString = normalizedInput.replace(/\s\s+/g, ' ');

    //--replace space with '-' character
    newString = newString.replace(/\s+/g, '-').toLowerCase();

    //--replace '/' with '-' character
    return newString.replaceAll('/', '-').toLowerCase();

  } else {
    return undefined;
  }
}

export const buildProductDetailUrl = (productId, categoryName, productName, languageCode) => {
  const resolvedLanguageCode = replaceWhiteSpacesWithDashSymbolInUrl(languageCode) ?? 'en';
  const resolvedProductId = getProductIdValue(productId);
  const resolvedCategorySlug = replaceWhiteSpacesWithDashSymbolInUrl(categoryName) ?? 'shop';
  const resolvedProductSlug = replaceWhiteSpacesWithDashSymbolInUrl(productName) ?? 'product';

  return `/${resolvedLanguageCode}/product-detail/${resolvedProductId}/${resolvedCategorySlug}/${resolvedProductSlug}`;
}

export const getProductIdValue = (productOrId) => {
  if (productOrId != null && typeof productOrId === 'object') {
    return getProductIdValue(
      productOrId.ProductId
      ?? productOrId.ProductID
      ?? productOrId.productId
      ?? productOrId.productID
      ?? productOrId.Id
      ?? productOrId.ID
      ?? productOrId.id
    );
  }

  const parsedValue = parseInt(productOrId, 10);
  return Number.isNaN(parsedValue) || parsedValue < 1 ? 0 : parsedValue;
}

export const convertDateToDifferentFormats = (inputDate, format) => {
  let formattedDate = inputDate;

  if (inputDate == undefined || inputDate == null || inputDate == "") {
    return formattedDate;
  }

  if (format == "dd/mm/yyyy") {
    let today = new Date(inputDate);
    let yyyy = today.getFullYear();
    let mm = today.getMonth() + 1; // Months start at 0!
    let dd = today.getDate();

    if (dd < 10) dd = '0' + dd;
    if (mm < 10) mm = '0' + mm;

    formattedDate = dd + '/' + mm + '/' + yyyy;
  } else if (format == "dd-mm-yyyy") {
    let today = new Date(inputDate);
    let yyyy = today.getFullYear();
    let mm = today.getMonth() + 1; // Months start at 0!
    let dd = today.getDate();

    if (dd < 10) dd = '0' + dd;
    if (mm < 10) mm = '0' + mm;

    formattedDate = dd + '-' + mm + '-' + yyyy;
  } else {
    let today = new Date(inputDate);
    let yyyy = today.getFullYear();
    let mm = today.getMonth() + 1; // Months start at 0!
    let dd = today.getDate();

    if (dd < 10) dd = '0' + dd;
    if (mm < 10) mm = '0' + mm;

    formattedDate = dd + '-' + mm + '-' + yyyy;
  }

  return formattedDate;
}


export const makePriceRoundToTwoPlaces = (price) => {
  price = price ?? 0;
  return +(Math.round(price + "e+2") + "e-2");

}

export const setProductDescriptionImagesUrl = (FullDescription) => {

  try {
    if (FullDescription != undefined && FullDescription != null && FullDescription != '') {
      if (FullDescription.includes('<img src="/content/commonImages/')) {
        let adminPanelBaseURL = Config['ADMIN_BASE_URL'];
        let replaceText = '<img src="' + adminPanelBaseURL + "content/commonImages/";
        FullDescription = FullDescription.replace('<img src="/content/commonImages/', replaceText);
      }

    }
  }
  catch (err) {
    console.log(err.message);
  }

  return FullDescription;

}


export const getFileExtensionFromContentType = (contentType) => {
  switch (contentType) {
    case "application/pdf":
      return ".pdf";
    case "application/msword":
      return ".doc";
    case "application/vnd.ms-excel":
      return ".xls";
    case "application/vnd.ms-powerpoint":
      return ".ppt";
    case "image/jpeg":
      return ".jpg";
    case "image/png":
      return ".png";
    case "image/bmp":
      return ".bmp";
    case "image/gif":
      return ".gif";
    case "text/plain":
      return ".txt";
    case "text/csv":
      return ".csv";
    case "text/html":
      return ".html";
    case "application/zip":
      return ".zip";
    case "application/x-rar-compressed":
      return ".rar";
    case "application/x-7z-compressed":
      return ".7z";
    case "application/x-tar":
      return ".tar";
    case "application/gzip":
      return ".gz";
    case "audio/mpeg":
      return ".mp3";
    case "audio/wav":
      return ".wav";
    case "audio/ogg":
      return ".ogg";
    case "video/mp4":
      return ".m4v";
    case "video/x-msvideo":
      return ".avi";
    case "video/x-ms-wmv":
      return ".wmv";
    case "video/x-flv":
      return ".flv";
    case "video/quicktime":
      return ".mov";
    case "video/x-matroska":
      return ".mkv";
    case "application/illustrator":
      return ".ai";
    case "application/postscript":
      return ".eps";
    case "image/vnd.adobe.photoshop":
      return ".psd";
    case "application/x-indesign":
      return ".indd";
    case "image/svg+xml":
      return ".svg";
    case "text/javascript":
      return ".js";
    case "text/css":
      return ".css";
    case "application/json":
      return ".json";
    case "application/xml":
      return ".xml";
    default:
      return "application/octet-stream";
  }
}


export const calculatePriceDiscountPercentage = (originalPrice, discountedPrice) => {
  let discount = (originalPrice ?? 0) - (discountedPrice ?? 0);
  let discountPercentage = (discount / originalPrice) * 100;
  return discountPercentage.toFixed(2) + "%";
}

export const makeImageUrl = (baseUrl, imagePath) => {
  if (!imagePath || !imagePath.trim()) return '';
  if (imagePath.startsWith('http://') || imagePath.startsWith('https://')) return imagePath;
  const base = (baseUrl ?? '').replace(/\/$/, '');
  let path = imagePath.replace(/^\//, '');
  // Fix corrupted paths where a sub-directory was incorrectly prepended to a path that
  // already contained a full /content/ reference, producing a double-slash such as:
  //   "content/commonImages/productImages//content/commonImages/otherImages/file.png"
  // We look for "//content/" specifically to avoid false positives on other "//" patterns.
  const corruptIdx = path.indexOf('//content/');
  if (corruptIdx !== -1) {
    path = path.substring(corruptIdx + 2);
  }
  return `${base}/${path}`;
};

export const getFileExtensionNameFromPath = (url) => {
  try {
    if (!checkIfStringIsEmtpy(url)) {
      return "";
    }

    // split the URL into an array using the dot as a separator
    const urlParts = url.split('.');

    // get the last item in the array, which should be the extension
    const extension = urlParts[urlParts.length - 1];

    // return the extension in lowercase
    return extension.toLowerCase();
  } catch (error) {
    console.error('An exception occurred in getFileExtensionNameFromPath():', error.message);
    return "";
  }
}




