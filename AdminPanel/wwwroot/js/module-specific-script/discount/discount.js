
function EnableCouponCodeField() {
    let IsCouponCodeRequired = ($('#IsCouponCodeRequired').is(":checked") == true) ? true : false;
    if (IsCouponCodeRequired) {
        $("#CouponValueDiv").css("display", "block");
        $("#CouponCode").prop('required', true);

    } else {
        $("#CouponValueDiv").css("display", "none");
        $("#CouponCode").val("");
        $("#CouponCode").prop('required', false);

    }
}

function EnableIsBoundToMaxQuantityField() {
    let IsCouponCodeRequired = ($('#IsBoundToMaxQuantity').is(":checked") == true) ? true : false;
    if (IsCouponCodeRequired) {
        $("#MaxQuantityDiv").css("display", "block");
        $("#MaxQuantity").prop('required', true);

    } else {
        $("#MaxQuantityDiv").css("display", "none");
        $("#MaxQuantity").val("");
        $("#MaxQuantity").prop('required', false);

    }
}



function GetSelectedDiscountTypeData() {
    let DiscountTypeId = $("#DiscountTypeId").val();

    // ✅ Enable
    if (!checkIfStringIsEmtpy(DiscountTypeId)) {
        return false;
    }




    switch (DiscountTypeId) {
        case '3': //--DiscountTypeId = 3 is for "Applied on products" discount type
            $("#discount-type-data-anchor").text("Link Products");
            $("#discount-type-data-nav-item").css("display", "list-item");

            GetDiscountProductsPartialPage();

            break;
        case '4': //--DiscountTypeId = 4 is for "Applied on categories" discount type
            $("#discount-type-data-anchor").text("Link  Categories");
            $("#discount-type-data-nav-item").css("display", "list-item");
            GetDiscountCategoriesPartialPage();

            break;

        case '5': //--DiscountTypeId = 5 is for "Applied on manufacturers" discount type
            $("#discount-type-data-anchor").text("Link  Manufacturers");
            $("#discount-type-data-nav-item").css("display", "list-item");


            break;

        case '6': //--DiscountTypeId = 6 is for "Applied on cities" discount type
            $("#discount-type-data-anchor").text("Link  Cities");
            $("#discount-type-data-nav-item").css("display", "list-item");


            break;
        case '7': //--DiscountTypeId = 7 is for "Applied on shipping" discount type
            $("#discount-type-data-anchor").text("Link  Shipping Methods");
            $("#discount-type-data-nav-item").css("display", "list-item");


            break;

        default:
            $("#discount-type-data-anchor").text("");
            $("#discount-type-data-nav-item").css("display", "none");
            $("#discount-info-ancho").trigger("click");

    }



}



function GetDiscountProductsPartialPage() {
    let DiscountId = $("#DiscountId").val();
    if (!checkIfStringIsEmtpy(DiscountId)) {
        DiscountId = -1;
    }

    // --make form data
    var formDate = {
        DiscountId: DiscountId
    }

    let requestUrl = "/Discounts/GetDiscountProductsPartialPage";
    $.ajax({
        type: "GET",
        url: requestUrl,
        data: formDate,
        // datatype: "json",
        cache: false,
        async: false,

        success: function (data) {

            $("#discount_type_data_list").html(data);

        },
        error: function (xhr, ajaxOptions, thrownError) {
            showSuccessErrorMsg("error", "Error", "An error occured. Please try again!");
        }
    })

}

function GetDiscountCategoriesPartialPage() {

    let DiscountId = $("#DiscountId").val();
    if (!checkIfStringIsEmtpy(DiscountId)) {
        DiscountId = -1;
    }
    // --make form data
    var formDate = {
        DiscountID: DiscountId
    }

    let requestUrl = "/Discounts/GetDiscountCategoriesPartialPage";
    $.ajax({
        type: "GET",
        url: requestUrl,
        data: formDate,
        // datatype: "json",
        cache: false,
        async: false,

        success: function (data) {

            $("#discount_type_data_list").html(data);

        },
        error: function (xhr, ajaxOptions, thrownError) {
            showSuccessErrorMsg("error", "Error", "An error occured. Please try again!");
        }
    })

}


function ShowAllProductsForDiscount() {
    // --make form data
    var formDate = {
        ProductId: -1,

    }

    let requestUrl = "/Discounts/ShowAllProductsForDiscount";
    $.ajax({
        type: "GET",
        url: requestUrl,
        data: formDate,
        // datatype: "json",
        cache: false,
        async: false,

        success: function (data) {

            $("#ProductsModalArea").html(data);
            $("#modal_full_products_discount").modal('show');

        },
        error: function (xhr, ajaxOptions, thrownError) {
            showSuccessErrorMsg("error", "Error", "An error occured. Please try again!");
        }
    })
}



function AddSelectedCheckProductToDiscountList(thisParam) {
    
    let IsFieldChecked = ($(thisParam).is(":checked") == true) ? true : false;
    if (IsFieldChecked) {
        let data_product_id = $(thisParam).attr("data_product_id");
        let data_product_name = $(thisParam).attr("data_product_name");


        //--check if row already exists in table
        let isRowExists = false;
        $('.product-discount-row').each(function () {

            let data_discount_product_id = $(this).attr("data_discount_product_id");

            if (data_discount_product_id == data_product_id) {
                isRowExists = true;
            }

        });

        if (isRowExists == true) {
            event.preventDefault();
            showSuccessErrorMsg("error", "Error", "This product already exists!");
            return false;
        }

        //--check if no data row exists in table, then remove
        if ($("#product_discounted_no_data_row").length) {
            $("#product_discounted_no_data_row").remove();
        }

        //--Make table row
        $('#discount_selected_products tbody').append("<tr>"
            + "<td> "
            + "<input type='hidden'  class='product-discount-row' data_discount_product_id='" + data_product_id + "' data_discount_product_mapping_id='" + -1 + "'/>"
            + data_product_id
            + "</td>"
            + "<td> " + data_product_name + "</td>"
            + "<td>"
            + "<div class=''>"

            + "<a href='#!' class='dropdown-item text-pink-600' onclick='DeleteProductFromDiscountList( this,-1);'>"
            + "<i class='icon-trash'></i> Delete"
            + "</a>"
            + "</div> "
            + "</td>"
            + "</tr>");

        showSuccessErrorMsg("success", "Success", "Product added successfully!");
    }



}


function DeleteProductFromDiscountList(thisParam, DiscountProductMappingID) {
    event.preventDefault();

    let confirmClick = confirm('Do you really want to remove this record?');
    if (confirmClick) {

        if (DiscountProductMappingID == -1) {//-- delete only temperory row that is not saved on data base
            $(thisParam).closest('tr').remove();
            return true;
        } else {//--delete permanently

            var formData = {
                primarykeyValue: DiscountProductMappingID,
                primaryKeyColumn: "DiscountProductMappingID",
                tableName: "DiscountProductsMapping",
                SqlDeleteType: "@((short)SqlDeleteTypes.PlainTableDelete)"

            }

            let requestUrl = "/Dynamic/DeleteAnyRecord";

            $.ajax({
                type: "POST",
                url: requestUrl,
                data: formData,
                success: function (data) {

                    if (data.success) {
                        showSuccessErrorMsg("success", "Success", data.message);
                        $(thisParam).closest('tr').remove();
                    }
                    else {

                        showSuccessErrorMsg("error", "Error", "Something went wrong during saving of record");
                    }


                },
                error: function (xhr, ajaxOptions, thrownError) {

                    showSuccessErrorMsg("error", "Error", "An error occured. Please try again.");
                }
            });
        }

    }

}

function ShowAllCategoriesForDiscount() {
    // --make form data
    var formDate = {
        DiscountId: -1,

    }

    let requestUrl = "/Discounts/ShowAllCategoriesForDiscount";
    $.ajax({
        type: "GET",
        url: requestUrl,
        data: formDate,
        // datatype: "json",
        cache: false,
        async: false,

        success: function (data) {

            $("#CategoriesModalArea").html(data);
            $("#modal_full_categories_discount").modal('show');

        },
        error: function (xhr, ajaxOptions, thrownError) {
            showSuccessErrorMsg("error", "Error", "An error occured. Please try again!");
        }
    })
}



function AddSelectedCheckCategoryToDiscountList(thisParam) {
    
    let IsFieldChecked = ($(thisParam).is(":checked") == true) ? true : false;
    if (IsFieldChecked) {
        let data_category_id = $(thisParam).attr("data_category_id");
        let data_category_name = $(thisParam).attr("data_category_name");
        let data_parent_category_name = $(thisParam).attr("data_parent_category_name");


        //--check if row already exists in table
        let isRowExists = false;
        $('.category-discount-row').each(function () {

            let data_discount_category_id = $(this).attr("data_discount_category_id");

            if (data_discount_category_id == data_category_id) {
                isRowExists = true;
            }

        });

        if (isRowExists == true) {
            event.preventDefault();
            showSuccessErrorMsg("error", "Error", "This category already exists!");
            return false;
        }

        //--check if no data row exists in table, then remove
        if ($("#category_discounted_no_data_row").length) {
            $("#category_discounted_no_data_row").remove();
        }

        //--Make table row
        $('#discount_selected_categories tbody').append("<tr>"
            + "<td> "
            + "<input type='hidden'  class='category-discount-row' data_discount_category_id='" + data_category_id + "' data_discount_category_mapping_id='" + -1 + "'/>"
            + data_category_id
            + "</td>"
            + "<td> " + data_category_name + "</td>"
            + "<td> " + data_parent_category_name + "</td>"
            + "<td>"
            + "<div class=''>"

            + "<a href='#!' class='dropdown-item text-pink-600' onclick='DeleteCategoryFromDiscountList( this,-1);'>"
            + "<i class='icon-trash'></i> Delete"
            + "</a>"
            + "</div> "
            + "</td>"
            + "</tr>");

        showSuccessErrorMsg("success", "Success", "Category added successfully!");
    }



}



function DeleteCategoryFromDiscountList(thisParam, DiscountCategoryMappingID) {
    event.preventDefault();

    let confirmClick = confirm('Do you really want to remove this record?');
    if (confirmClick) {

        if (DiscountCategoryMappingID == -1) {//-- delete only temperory row that is not saved on data base
            $(thisParam).closest('tr').remove();
            return true;
        } else {//--delete permanently

            var formData = {
                primarykeyValue: DiscountCategoryMappingID,
                primaryKeyColumn: "DiscountCategoryMappingID",
                tableName: "DiscountCategoriesMapping",
                SqlDeleteType: "@((short)SqlDeleteTypes.PlainTableDelete)"

            }


            let requestUrl = "/Dynamic/DeleteAnyRecord";

            $.ajax({
                type: "POST",
                url: requestUrl,
                data: formData,
                success: function (data) {

                    if (data.success) {
                        showSuccessErrorMsg("success", "Success", data.message);
                        $(thisParam).closest('tr').remove();
                    }
                    else {

                        showSuccessErrorMsg("error", "Error", "Something went wrong during saving of record");
                    }


                },
                error: function (xhr, ajaxOptions, thrownError) {

                    showSuccessErrorMsg("error", "Error", "An error occured. Please try again.");
                }
            });
        }

    }

}

