$(document).ready(function () {
    var PubController = {
        init: function () {//load dữ liệu
            PubController.LoadData();
            PubController.registerEvent();
        },

        registerEvent: function () {//xử lý sự kiện
            $('.btn-save').click(function () {
                var Name = $('#title').val();
                var Alias = $('#slug').val();
                var id = $('#id').val();
                var phone = $('#PhoneNumber').val();
                var dc = $('#Address').val();
                var Status = true;
                var Pub = {
                    ID: id,
                    Alias: Alias,
                    Name: Name,
                    Address: dc,
                    PhoneNumber:phone,
                    Status: Status
                };
                PubController.Save(Pub);
            });

            $(document).on('click', '.btn-edit', function () {
                var id = $(this).data('id');
                PubController.LoadDetails(id);
            });

            $(document).on('click', '.btn-create', function () {
                PubController.reset();
            });

            $(document).on('click', '.btn-delete', function () {
                if (confirm("Bạn chắc chắn xóa?")) {
                    var id = $(this).data('id');
                    PubController.Del(id);
                }
            });

        },
        //Lấy toàn bộ tác giả
        reset: function () {
            $('#tittle').val('');
            $('#slug').val('');
            $('#id').val('');
            $('#Address').val('');
            $('#PhoneNumber').val('');
        },

        LoadData: function () {
            $.ajax({
                url: "/Publisher/LoadData",
                type: "Get",
                dataType: "json",
                success: function (result) {
                    if (result.status == true) {
                        var data = JSON.parse(result.data);
                        var template = $('#data-template').html();
                        var html = '';
                        $.each(data, function (i, item) {//dòng foreach chạy dữ liệu
                            html += Mustache.render(template, {
                                ID: item.ID,
                                Name: item.Name,
                                Alias: item.Alias,
                                Address: item.Address,
                                PhoneNumber: item.PhoneNumber,
                                btnStatus: item.Status == true ? `<a class="my-btn btn-status btn-unpublish" data-id=${item.ID}>Unpublish</a>` : `<a class="my-btn btn-status btn-publish" data-id=${item.ID}>Publish</a>`,
                            });
                        });
                        $('#resultPub').html(html);
                    }

                }
            });
        },
        // thêm tác giả mới
        Save: function (Pub) {
            $.ajax({
                url: "/Publisher/Save",
                data: {
                    pub: Pub
                },
                type: "Post",
                dataType: "json",
                success: function (result) {
                    if (result.status == true) {
                        PubController.LoadData();
                    }
                }
            })
        },

        //Load dữ liệu dựa theo id tác giả
        LoadDetails: function (id) {
            $.ajax({
                url: "/Publisher/LoadDetails",
                data: { id: id },
                type: "Post",
                dataType: "json",
                success: function (result) { // result nhận json
                    var data = result.pub
                    $('#id').val(data.ID);
                    $('#title').val(data.Name);
                    $('#slug').val(data.Alias);
                    $('#PhoneNumber').val(data.PhoneNumber);
                    $('#Address').val(data.Address);
                }
            });
        },

        Del: function (id) {
            $.ajax({
                url: "/Publisher/Del",
                data: { id: id },
                type: "Post",
                dataType: "json",
                success: function (result) { // result nhận json
                    PubController.LoadData();
                }
            });
        },

    };
    PubController.init();//chạy đầu tiên
})