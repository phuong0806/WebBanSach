$(document).ready(function () {
    var orderController = {
        //Constructor
        init: function () {
            orderController.loadData();
            orderController.registerEvents();
        },
        //Register events function
        registerEvents: function () {
            $(document).on('click', '.btn-isConfirm', function () {
                var id = $(this).data('id');
                orderController.confirmOrder(id);
            });

            $(document).on('keyup', '#search-text', function () {
                orderController.loadData();
            });

            $(document).on('change', '.sel-list', function () {
                orderController.loadData();
            });

            $('#fromDate').change(function () {
                orderController.loadData();
            });

            $('#toDate').change(function () {
                orderController.loadData();
            });

            $('.btn-export').off('click').on('click', function (e) {
                var path = '';
                var searchText = $('#search-text').val();
                var statusSelect = $('.sel-list').val();
                var fromDate = $('#fromDate').val();
                var toDate = $('#toDate').val();
                path += "&searchText=" + searchText;
                path += "&statusSelect=" + statusSelect;
                path += "&fromDate=" + fromDate;
                path += "&toDate=" + toDate;
                window.location = '/Admin/Order/ExportToExcel?name=report-order' + path;
            });
        },

        confirmOrder: function (id) {
            $.ajax({
                url: "/Admin/Order/ConfirmOrder",
                data: {
                    id: id
                },
                dataType: 'JSON',
                type: 'POST',
                success: function (response) {
                    if (response.status) {
                        notify({
                            type: "success", //alert | success | error | warning | info
                            title: "Thay đổi thành công",
                            position: {
                                x: "right", //right | left | center
                                y: "top" //top | bottom | center
                            },
                            size: "small",
                            autoHide: true
                        });
                        orderController.loadData();
                    }
                    else {
                        alert("error confirm");
                    }
                }
            });
        },

        loadData: function () {
            var searchText = $('#search-text').val();
            var statusSelect = $('.sel-list').val();
            var fromDate = $('#fromDate').val();
            var toDate = $('#toDate').val();
            $.ajax({
                url: "/Admin/Order/loadData",
                data: {
                    searchText: searchText,
                    idStatus: statusSelect,
                    fromDate: fromDate,
                    toDate: toDate
                },
                type: "GET",
                dataType: "json",
                success: function (data) {
                    var html = '';
                    var template = $('#data-template').html();
                    $.each(data, function (i, item) {
                        if (item.Status == null && item.isFinish == false) {
                            var status = `<label class="badge badge-default">Đơn hàng mới</label>`;
                        } else if (item.Status == 0 && item.isFinish == false) {
                            var status = `<label class="badge badge-info">Đang xử lý</label>`;
                        } else if (item.Status == 1 && item.isFinish == false) {
                            var status = `<label class="badge badge-primary">Đang vận chuyển</label>`;
                        } else {
                            var status = `<label class="badge badge-success">Hoàn tất</label>`;
                        }

                        html += Mustache.render(template, {
                            ID: item.ID,
                            Status: status,
                            CreatedDate: js_yyyy_mm_dd_hh_mm_ss(item.CreatedDate),
                            ConfirmDate: js_yyyy_mm_dd_hh_mm_ss(item.ConfirmDate) != null ? js_yyyy_mm_dd_hh_mm_ss(item.ConfirmDate) : "-",
                            FinishDate: js_yyyy_mm_dd_hh_mm_ss(item.FinishDate) != null ? js_yyyy_mm_dd_hh_mm_ss(item.FinishDate) : "-",
                            ConfirmBy: item.ConfirmBy == null ? "Chưa có" : `<a href="#">${item.ConfirmBy}</a>`,
                            TotalCost: format_money(item.TotalCost),
                            btnConfirm: item.isConfirm == false ? `<a class="my-btn btn-isConfirm btn-confirm" data-id=${item.ID}>confirm</a>` : `<a class="my-btn btn-isConfirm btn-unconfirm" data-id=${item.ID}>Unconfimred</a>`,
                        });
                    });
                    $('#result').html(html);
                },
                error: function () {
                    alert("error order");
                }
            })
        }
    }
    orderController.init();
});