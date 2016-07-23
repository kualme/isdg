//window.CKEDITOR_BASEPATH = "http://test.isdg-site.org/Scripts/ckeditor/";
function showForm(elem, parentClass, formClass) {
    var form = $(elem).parents("." + parentClass).children("." + formClass);
    if (form.hasClass("hidden")) {
        form.removeClass("hidden");
    } else {
        form.addClass("hidden");
    } 
}
function closeFormOnButton(className, elem) {    
    $(elem).parents("." + className).addClass("hidden");
}
function openCreationForm(commonClassName) {
    $("." + commonClassName + "__add_open").hide();
    $("." + commonClassName + "__form_add").show();
    $("." + commonClassName + "__add_close").show();
}
function closeCreationForm(commonClassName) {
    $("." + commonClassName + "__add_open").show();
    $("." + commonClassName + "__form_add").hide();
    $("." + commonClassName + "__add_close").hide();
}
function showSpinner() {
    $(".fa-spinner").removeClass("hidden");
}
function hideSpinner() {
    $(".fa-spinner").addClass("hidden");
}
function showAlbum(elem, parentClass, formClass) {
    var form = $(elem).parents("." + parentClass).children("." + formClass);
    if (form.hasClass("hidden")) {
        form.removeClass("hidden");
        bindCarousel();
    } else {
        form.addClass("hidden");
    }
}
function bindCkeditor() {
    $.each($("textarea"), function (index, textarea) {
        if ($(textarea).hasClass("huge"))
            $(textarea).ckeditor({ height: 400 });
        else if ($(textarea).hasClass("medium"))
            $(textarea).ckeditor({ height: 250 });
        else
            $(textarea).ckeditor();
    });
}
$(bindCkeditor);
$(function () {
    $(".album__button-upload").click(function () {        
        var parent = $(this).parents('.album__images');        
        var imageUpload = $(parent.children()[0]).children('.album__image-upload');
        var errorMessage = parent.children('.error-message');
        var successMessage = parent.children('.success-message');
        var spinner = parent.find(".fa-spinner");
        var albumId = imageUpload.data("albumId");
        var formData = new FormData();
        var totalFiles = imageUpload.prop('files').length;
        for (var i = 0; i < totalFiles; i++) {
            var file = imageUpload.prop('files')[i];
            formData.append("FileUpload", file);
        }
        $.ajax({
            type: "POST",
            url: '/Album/UploadImages?albumId=' + albumId,
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            beforeSend: function () {
                spinner.removeClass("hidden");
                successMessage.addClass("hidden");
                errorMessage.addClass("hidden");
            },
            complete: function () { spinner.addClass("hidden") },
            success: function (response) {
                successMessage.removeClass("hidden");
                errorMessage.addClass("hidden");
                successMessage.text(response.message);
            },
            error: function (error) {
                successMessage.addClass("hidden");
                errorMessage.removeClass("hidden");
                errorMessage.text("An error occurred while downloading files");
            }
        });
    });
});
$(function () {
    $(".award__submit").click(function () {
        var parent = $(this).parents('.award__new');
        var images = parent.children('.image');
        var imageUploadFirst = $(images[0]).children('.award__image-upload-first');
        var imageUploadSecond = $(images[1]).children('.award__image-upload-second');
        var errorMessage = parent.children('.error-message');
        var spinner = parent.find(".fa-spinner");
        var formData = new FormData();
        var totalFiles = imageUploadFirst.prop('files').length;
        for (var i = 0; i < totalFiles; i++) {
            var file = imageUploadFirst.prop('files')[i];
            formData.append("FileUpload", file);
        }
        var totalFiles = imageUploadSecond.prop('files').length;
        for (var j = 0; j < totalFiles; j++) {
            var file = imageUploadSecond.prop('files')[j];
            formData.append("FileUpload", file);
        }
        var model = parent.serializeArray();
        for (var k = 0; k < model.length; k++) {
            formData.append(model[k].name, model[k].value);
        }
        $.ajax({
            type: "POST",
            url: '/Award/CreateEditAward',
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            beforeSend: function () {
                spinner.removeClass("hidden");                
                errorMessage.addClass("hidden");
            },
            complete: function () { spinner.addClass("hidden") },
            success: function (response) {
                if (response.success)
                    window.location.reload();
                else {
                    errorMessage.removeClass("hidden");
                    errorMessage.text(response.message);
                }
            },
            error: function (error) {
                errorMessage.removeClass("hidden");
                errorMessage.text("An error occurred while downloading files");
            }
        });
    });
});
function bindCarousel() {
    $(".carousel").each(function () {
        var $this = $(this);
        var galleryId = "#" + $this.closest(".album__content").attr("id");
        $this.jCarouselLite({
            btnNext: galleryId + " .jcarousel-next",
            btnPrev: galleryId + " .jcarousel-prev",
            visible: 7,
            circular: true,
            speed: 300,
            scroll: 1
        });
    });
    $(".carousel img").click(function () {
        var $this = $(this);
        var galleryId = "#" + $this.closest(".album__content").attr("id");
        $(galleryId + " .mid img").attr("src", $(this).attr("src"));
        $(galleryId + " .mid-caption").text($(this).attr("title"))
    });
}
$(bindCarousel);
function showSuccessMessage(formClass, entity) {    
    var form = $("." + formClass);
    var success = form.children('.success-message')
    success.removeClass("hidden");
    success.text(entity + " is successfully added and will be published after validation")
}