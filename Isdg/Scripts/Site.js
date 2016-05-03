function showForm(elem, parentClass, formClass) {    
    var form = $(elem).parents("." + parentClass).children("." + formClass);
    if (form.hasClass("hidden")) {
        form.removeClass("hidden");
    } else {
        form.addClass("hidden");
    } 
}
function closeFormOnButton(className, elem) {    
    $(elem).parents('.' + className).addClass("hidden");
}
function openCreationForm() {
    $('.news__add_open').hide();
    $('.news__form_add').show();
    $('.news__add_close').show();
}
function closeCreationForm() {
    $('.news__add_open').show();
    $('.news__form_add').hide();
    $('.news__add_close').hide();
}
function bindCkeditor() {
    $.each($('textarea'), function (index, textarea) { $(textarea).ckeditor(); });
}
$(bindCkeditor);