import React, { useState, useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { showErrorMsg, showInfoMsg, showSuccessMsg } from './ValidationHelper';
import rootAction from '../stateManagment/actions/rootAction';
import { getProductIdValue } from './ConversionHelper';


export const getSanitizedCartItems = (cartValue) => {
    try {
        const parsedCart = typeof cartValue === 'string'
            ? JSON.parse(cartValue ?? '[]')
            : (Array.isArray(cartValue) ? cartValue : []);

        if (!Array.isArray(parsedCart)) {
            return [];
        }

        return parsedCart
            .map((item) => {
                const productId = getProductIdValue(item);
                const quantity = parseInt(item?.Quantity ?? 1, 10);

                if (productId < 1 || Number.isNaN(quantity) || quantity < 1) {
                    return null;
                }

                return {
                    ...item,
                    ProductId: productId,
                    Quantity: quantity,
                };
            })
            .filter(Boolean);
    }
    catch (err) {
        return [];
    }
}

export const syncCartStorage = (cartValue) => {
    const sanitizedCartItems = getSanitizedCartItems(cartValue);
    localStorage.setItem("cartItems", JSON.stringify(sanitizedCartItems));
    return sanitizedCartItems;
}


export const AddProductToCart = (ProductId, 
    Quantity, productSelectedAttributes, DefaultImage) => {
    let cartItems = [];
    try {
        const normalizedProductId = getProductIdValue(ProductId);
        if (normalizedProductId < 1) {
            showErrorMsg("An error occured. Please try again!");
            return JSON.stringify([]);
        }

        cartItems = getSanitizedCartItems(localStorage.getItem("cartItems"));

        //--check if product already exists
        if (cartItems?.filter(obj => obj.ProductId == normalizedProductId).length > 0) {
            showInfoMsg("Product already exists in your cart!");
            return JSON.stringify(cartItems);
        } else {
            cartItems.push({
                ProductId: normalizedProductId,
                productSelectedAttributes : productSelectedAttributes,
                Quantity: parseInt(Quantity ?? 1, 10) > 0 ? parseInt(Quantity ?? 1, 10) : 1,
                ShippingCharges : 0,
                DefaultImage: DefaultImage
            });

            console.log(cartItems);

            

            //--store in storage
            syncCartStorage(cartItems);

            showSuccessMsg("Added to the cart!");
              return JSON.stringify(cartItems);;
        }
    }
    catch (err) {
        console.log(err);
        showErrorMsg("An error occured. Please try again!");
          return JSON.stringify(cartItems);;
    }



};

export const AddCustomerWishList = (ProductId, ProductName  , Price,  DiscountedPrice  , DiscountId    ,
     IsDiscountCalculated, CouponCode   ,
     SizeId,  SizeShortName  , ColorId,  ColorName , Quantity, DefaultImage) => {
    let customerWishList = [];
    try {
        
       
        customerWishList = JSON.parse(localStorage.getItem("customerWishList"))
        customerWishList = customerWishList ?? [];

        //--check if product already exists
        if (customerWishList?.filter(obj => obj.ProductId == ProductId).length > 0) {
            showInfoMsg("Product already exists in your wish list!");
            return JSON.stringify(customerWishList);
        } else {
            customerWishList.push({
                ProductId: ProductId,
                ProductName : ProductName,
                Price: Price,
                DiscountedPrice : DiscountedPrice, 
                DiscountId :DiscountId   ,
                IsDiscountCalculated :IsDiscountCalculated, 
                CouponCode :CouponCode ,
                SizeId: SizeId,
                SizeShortName : SizeShortName,
                ColorId: ColorId,
                ColorName: ColorName,
                Quantity: Quantity,
                ShippingCharges: 0 ,
                DefaultImage: DefaultImage
            });

            console.log(customerWishList);

            showSuccessMsg("Added to your wish list!");
              return JSON.stringify(customerWishList);;
        }
    }
    catch (err) {
        console.log(err);
        showErrorMsg("An error occured. Please try again!");
          return JSON.stringify(customerWishList);;
    }



};