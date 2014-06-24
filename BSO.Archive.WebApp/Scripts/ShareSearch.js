$(document).ready(function () {
    $('.shareResults').on('click', function () {
        var searchIdForEmail = $("#searchIdForEmail");
        searchIdForEmail.val($(this).attr('data-id'));
        var searchTypeForEmail = $("#searchTypeForEmail");
        searchTypeForEmail.val($(this).attr('data-type'));
        $('.modalWrap').fadeIn(150);
        return false;
    });


    $('.close, .modalOverlay').on('click', function () {
        $('.modalWrap').fadeOut(150);
        $("#emailErrorMessage").hide();
        return false;
    });

    placeHolderText();
});

function validateEmail() {
    $("#emailErrorMessage").hide();

    if ($("#toEmail").val() !== "" && $("#fromEmail").val() !== "")
        return true;

    $("#emailErrorMessage").show();
    return false;
}

function placeHolderText() {
    var watermarkText = null;
    var selector = 'div.modalWrap input[type=text]';

    $(selector).each(function (i) {
        watermarkText = $(this).attr('waterMarkText');
        $(this).addClass('placeholder').val(watermarkText);

    });

    $(selector).focus(function () {
        watermarkText = $(this).val();
        if ($(this).val() == watermarkText) {
            $(this).val('').removeClass('placeholder');
        }
    });

    $(selector).blur(function () {
        watermarkText = $(this).attr('waterMarkText');
        if ($(this).val() == "") {
            $(this).val(watermarkText).addClass('placeholder');
        }

    });
}