$(document).ready(function myfunction() {
    var businessController = {
        //Contructor
        init: function () {
            businessController.registerEvent();
            businessController.loadData();
        },
        //Register event function
        registerEvent: function () {
            $(document).on('click', '.btn-save', function () {
                var id = $('#id').val();
                businessController.update(id);
            });

            $(document).on('click', '.btn-edit', function () {
                var id = $(this).data('id');
                businessController.loadDetail(id);
            });
        },
        resetModal: function () {
            $('#description').val('');
            setTimeout(function () {
                $('.modalBox').removeClass('active');
                $('.modalBox').removeClass('zoomIn');
            }, 500);
            $('.modalBox').addClass('zoomOut');
            $('.blurBackground').removeClass('active');
        },
        //function
        update: function (id) {
            var Name = $('#description').val();
            var business = {
                ID: id,
                Name: Name
            };
            $.ajax({
                url: '/Admin/Business/updateDescription',
                data: {
                    businessStr: JSON.stringify(business)
                },
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.status == true) {
                        businessController.resetModal();
                        notify({
                            type: "success", //alert | success | error | warning | info
                            title: "Cập nhật thành công",
                            position: {
                                x: "right", //right | left | center
                                y: "top" //top | bottom | center
                            },
                            size: "small",
                            autoHide: true
                        });
                        businessController.loadData();
                    }
                }
            });
        },

        loadData: function () {
            $.ajax({
                url: '/Admin/Business/loadData',
                type: 'GET',
                dataType: 'json',
                success: function (data) {
                        var html = '';
                        var template = $('#data-template').html();
                        $.each(data, function (i, item) {
                            html += Mustache.render(template, {
                                ID: item.ID,
                                Name: item.Name
                            });
                        });
                        $('#resultBook').html(html);
                },
                error: function () {
                    alert("error");
                }
            })
        },

        loadDetail: function (id) {
            $.ajax({
                url: '/Admin/Business/getDetail',
                data: {
                    id: id
                },
                type: 'GET',
                dataType: 'json',
                success: function (data) {
                        $('#description').val(data.Name);
                        $('#id').val(data.ID);
                },
                error: function () {
                    alert("loadDetail error");
                }
            })
        },
    }
    businessController.init();
});
