$(document).ready(function () {
    $('.btn-order').off('click').on('click', function () {
        $.ajax({
            url: "/Cart/Confirm",
            dataType: 'json',
            type: "POST",
            success: function (response) {
                window.location = "/dat-hang";
            }
        })
    });
})