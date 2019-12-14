replaceContent = function (xhr, htmlElementId) {
    //alert('updateContent');
    var id = "#" + htmlElementId;
    $(id).replaceWith(xhr.responseText);
};
