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
    $('.' + commonClassName + "__add_open").hide();
    $('.' + commonClassName + "__form_add").show();
    $('.' + commonClassName + "__add_close").show();
}
function closeCreationForm(commonClassName) {
    $('.' + commonClassName + "__add_open").show();
    $('.' + commonClassName + "__form_add").hide();
    $('.' + commonClassName + "__add_close").hide();
}
function bindCkeditor() {
    $.each($("textarea"), function (index, textarea) { $(textarea).ckeditor(); });
}
$(bindCkeditor);
$(function () {
    $(".album__button-upload").click(function () {        
        var parent = $(this).parents('.album__images');        
        var imageUpload = $(parent.children()[0]).children('.album__image-upload');
        var errorMessage = parent.children('.error-message');
        var successMessage = parent.children('.success-message');
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
    });
}
$(bindCarousel);