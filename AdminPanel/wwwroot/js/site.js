
function resetSearchForm() {
    $('#searchform').trigger("reset");
}


function formatDateToSQLFormat(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('-');
}

function checkIfStringIsEmtpy(str) {
    if (str == null || str == undefined) {
        str = '';
    }
    return /\S+/.test(str);
}


function showDeleteModal(entityId, primarykeyValue, primaryKeyColumn, tableName, SqlDeleteType) {

    //--first empty existing fields
    $('#entityIdDeleteValue').val('');
    $('#primarykeyValue').val('');
    $('#primaryKeyColumn').val('');
    $('#tableName').val('');
    $('#SqlDeleteType').val('');

    //--now assign new values
    $('#entityIdDeleteValue').val(entityId);
    $('#primarykeyValue').val(primarykeyValue);
    $('#primaryKeyColumn').val(primaryKeyColumn);
    $('#tableName').val(tableName);
    $('#SqlDeleteType').val(SqlDeleteType);


    $('#DeleteRecordModal').modal('show');

}

function deleteRecordDynamic() {
    
    let entityId = $('#entityIdDeleteValue').val();
    entityId = entityId ?? 0;
    let primarykeyValue = $('#primarykeyValue').val();
    let primaryKeyColumn = $('#primaryKeyColumn').val();
    let tableName = $('#tableName').val();
    let SqlDeleteType = $('#SqlDeleteType').val();

    if (!checkIfStringIsEmtpy(primarykeyValue) || !checkIfStringIsEmtpy(primaryKeyColumn) || !checkIfStringIsEmtpy(tableName)
        || primarykeyValue == 0) {

        $('#confirmation-modal-delete').modal('hide');
        showSuccessErrorMsg("error", "Error", "Table not exits or primary key value is null.");

        return false;
    }

    //let deleteURL = '@Url.Action("DeleteAnyRecord","Dynamic")';
    let deleteURL = '/Dynamic/DeleteAnyRecord';

    $.ajax({
        type: "POST",
        url: deleteURL,
        data: { EntityId: entityId, primarykeyValue: primarykeyValue, primaryKeyColumn: primaryKeyColumn, tableName: tableName, SqlDeleteType: SqlDeleteType },
        cache: false,
        async: false,

        success: function (data) {

            console.log(data);
            if (data.success) {
                showSuccessErrorMsg("success", "Sucess", "Deleted Successfully!");
                setTimeout(function () {
                    location.reload();
                }, 1500);
            } else {
                $('#DeleteRecordModal').modal('hide');
                showSuccessErrorMsg("error", "Error", "An error occured. Please try again!");
            }


        },
        error: function (xhr, ajaxOptions, thrownError) {
            $('#DeleteRecordModal').modal('hide');
            showSuccessErrorMsg("error", "Error", "An error occured. Please try again!");
        }
    });

}

function showSuccessErrorMsg(msg_type, msg_title, msg_text) {

    // ✅ Get element. This button is defined in _HiddenHtmlFields.cshtml partial view
    const btnElmnt = document.getElementById('success-error-msg-hidden-btn');

    // ✅ Set element attributes
    $(btnElmnt).attr("data_msg_type", msg_type);
    $(btnElmnt).attr("data_msg_title", msg_title);
    $(btnElmnt).attr("data_msg_text", msg_text);

    // ✅ Click on the button
    btnElmnt.click();

}

function resetAnyFormById(formId) {
    formId = "#" + formId;
    $(formId).trigger("reset");
}

function GetEmptyRequiredFieldsOfForm(formId) {

    formId = "#" + formId;

    var ArrayRequiredFields = [];
    $.each($(formId).serializeArray(), function (i, field) {

        //var input = $('input[name='+field.name+']');

        let isRequired = $('[name=' + field.name + ']').prop('required');

        if (isRequired == true || isRequired == 'true') {
            if (!checkIfStringIsEmtpy(field.value)) {
                ArrayRequiredFields.push(
                    {
                        field_name: field.name,
                        field_value: field.value,
                    }
                );
            }
        }
    })
}

function showHideSiteMainLoader(visibleIndicator) {
    if (visibleIndicator === true) {
        $("#site_main_loader").css("display", "flex");
    } else if (visibleIndicator === false) {
        $("#site_main_loader").css("display", "none");
    } else {
        $("#site_main_loader").css("display", "none");
    }
}


function validateInsertForm(formId) {


    let IsFormValid = true;

    let ArrayRequiredFields = [];
    ArrayRequiredFields = GetEmptyRequiredFieldsOfForm(formId ?? "");


    if (ArrayRequiredFields != null && ArrayRequiredFields != undefined && ArrayRequiredFields.length > 0) {

        IsFormValid = false;

        let errorDiv = "<div class='row'>";
        errorDiv = errorDiv + "<div class='col-lg-12 col-md-12 col-sm-12'>";
        errorDiv = errorDiv + "<ul style='color:red;'>";

        let s = '';

        for (let i = 0; i < ArrayRequiredFields.length; i++) {
            s += '<li>' + ArrayRequiredFields[i].field_name + ' is required' + '</li>';
        }

        errorDiv = errorDiv + s;
        errorDiv = errorDiv + "</ul>";
        errorDiv = errorDiv + "</<div>";
        errorDiv = errorDiv + "</<div>";

        $("#error-messages-area").html(errorDiv);
    }


    return IsFormValid;
}

function GetLocalizationControlsJsonDataForScreen(EntityId, languageCode = null, htmlElementId = null) {
    

    if (languageCode == undefined || languageCode == null || languageCode == "") {
        return false;
    }

    if (languageCode == "en") { //-- do not perform localization for english
        return false;
    }

    if (EntityId == undefined || EntityId == null || EntityId == "" || EntityId < 1) {
        return false;
    }

    //--make form data
    var formData = {
        EntityId: EntityId,

    }

    // ✅ Show loader area starts here
    showHideSiteMainLoader(true);
    // ✅ Show loader area ends here

    let getUrl = '/Dynamic/GetLocalizationControlsJsonDataForScreen';
    $.ajax({
        type: "GET",
        url: getUrl,
        data: formData,
        // datatype: "json",
        cache: false,
        async: false,

        success: function (data) {

            console.log(data);
            if (data.success && checkIfStringIsEmtpy(data.dataLocalization)) {
                let localizationData = JSON.parse(data.dataLocalization);
                if (localizationData.labelsJsonData != undefined && localizationData.labelsJsonData.length > 0) {
                    replaceLocalizationControlsOfScreen(localizationData.labelsJsonData, htmlElementId);
                }
            }

            // ✅ Stop loader area starts here
            setTimeout(function () {
                showHideSiteMainLoader(false);
            }, 1000);
            // ✅ Stop loader area ends here

        },
        error: function (xhr, ajaxOptions, thrownError) {

            // ✅ Stop loader area starts here
            setTimeout(function () {
                showHideSiteMainLoader(false);
            }, 1000);
            // ✅ Stop loader area ends here
        }
    })

}

function replaceLocalizationControlsOfScreen(labelsJsonData, htmlElementId = null) {

    if (htmlElementId == null || htmlElementId == undefined) { //-- if htmlElementId is null, then its mean to run for whole body
        for (i = 0; i <= labelsJsonData.length - 1; i++) {
            let htmlElement = document.getElementById(labelsJsonData[i].labelHtmlId);
            if (htmlElement != null && htmlElement != undefined && checkIfStringIsEmtpy(labelsJsonData[i].text)) {
                let labelHtmlId = '#' + labelsJsonData[i].labelHtmlId;
                $(labelHtmlId).text(labelsJsonData[i].text);
            }
        }
    } else {//--if htmlElementId param is not null then only check localization for this specific html tag

        let parentElement = document.getElementById(htmlElementId);

        for (i = 0; i <= labelsJsonData.length - 1; i++) {
            let htmlElement = document.getElementById(labelsJsonData[i].labelHtmlId);
            if (htmlElement != null && htmlElement != undefined && checkIfStringIsEmtpy(labelsJsonData[i].text) && parentElement.contains(htmlElement)) {
                let labelHtmlId = '#' + labelsJsonData[i].labelHtmlId;
                $(labelHtmlId).text(labelsJsonData[i].text);

            }
        }
    }

}

function setProductDescriptionImagesUrl(FullDescription) {
    try {
        if (FullDescription.includes('<img src="../../../content/commonImages/')) {
            let replaceText = '<img src="/content/commonImages/';
            FullDescription = FullDescription.replace('<img src="../../../content/commonImages/', replaceText);
        }
    }
    catch (err) {
        console.log(err.message);
    }

    return FullDescription;
}