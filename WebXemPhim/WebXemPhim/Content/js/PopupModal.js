$(document).ready(function () {

    $("#myModal").css("display", "none");

    /*Khi hide modal thì gán src của iframe = null */
    $("#myModal").on('hide.bs.modal', function () {
        $("#videoYOU").attr('src', '');
        $("#myModal").css("display", "none");
    });

    /* Khi show modal thì gán src của iframe = URL */
    $("#myModal").on('show.bs.modal', function (e) {
        var $modal = $(this),
        phimURL = e.relatedTarget.href;
        $("#videoYOU").attr('src', phimURL);
        $("#myModal").css("display", "block");
    });
});
