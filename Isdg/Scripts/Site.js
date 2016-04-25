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