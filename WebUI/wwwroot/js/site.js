replaceContent = function (xhr, htmlElementId) {
    //alert('updateContent');
    var id = "#" + htmlElementId;
    $(id).replaceWith(xhr.responseText);
};

reloadNavi = function (target) {
    $.ajax({
        url: "/Account/ReloadNavi",
        complete: function (xhr) {
            $('#' + target).replaceWith(xhr.responseText);
        }
    });
};
