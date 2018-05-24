$(document).ready(function () {
    var orderDetailController = {
        init: function () {
            orderDetailController.registerEvents();
        },

        registerEvents: function () {
            $('#statusOrder').on('change', function () {
                var id = $('#orderID').text();
                var idStatus = $('#statusOrder').val();
                orderDetailController.changeStatus(idStatus, id);
            });

            $('.btn-isfinish').on('click', function () {
                var id = $('#orderID').text();
                orderDetailController.changeFinish(id);
            });
        },

        changeFinish: function (id) {
            $.ajax({
                url: '/Admin/Order/changeFinish',
                data: {
                    id: id
                },
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        window.location.reload();
                    }
                    else {
                        notify({
                            type: "error", //alert | success | error | warning | info
                            title: "Đơn hàng chưa xác thực",
                            position: {
                                x: "right", //right | left | center
                                y: "bottom" //top | bottom | center
                            },
                            size: "small",
                            autoHide: true
                        });
                    }
                }
            });
        },

        changeStatus: function (idStatus, id) {
            $.ajax({
                url: '/Admin/Order/ChangeStatus',
                data: {
                    idStatus: idStatus,
                    id: id
                },
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        window.location.reload();
                    }
                    else {
                        alert("error oderdetail");
                    }
                }
            });
        },
    }
    orderDetailController.init();
});