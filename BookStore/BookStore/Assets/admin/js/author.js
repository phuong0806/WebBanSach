$(document).ready(function () {
    var AuthorController = {
        init: function () {//load dữ liệu
            AuthorController.LoadData();
            AuthorController.registerEvent();
        },

        registerEvent: function () {//xử lý sự kiện
            $('.btn-save').click(function () {
                var Name = $('#name').val();
                var Birthday = js_dd_MM_yyyy($('#date').val());
                var id = $('#id').val();
                var info = CKEDITOR.instances.info.getData();
                var Status = true;
                var Author = {
                    ID: id,
                    Name: Name,
                    Birthday: Birthday,
                    Info: info,
                    Status: Status
                };
                AuthorController.Save(Author);
            });

            $(document).on('click', '.btn-edit', function () {
                var id = $(this).data('id');
                AuthorController.LoadDetails(id);
            });

            $(document).on('click', '.btn-create', function () {
                CKEDITOR.instances.info.setData("");
                AuthorController.reset();
            });

            $(document).on('click', '.btn-delete', function () {
                if (confirm("Ban co muon xoa hong")) {
                    var id = $(this).data('id');
                    AuthorController.Del(id);
                }
            });
        },
        //Lấy toàn bộ tác giả
        reset: function () {
            $('#name').val('');
            $('#date').val('');
            $('#info').val('');
            $('#id').val('');
        },

        LoadData: function () {
            $.ajax({
                url: "/Author/LoadData",
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
                                Info: item.Info,
                                Birthday: js_dd_MM_yyyy(item.Birthday),
                                btnStatus: item.Status == true ? `<a class="my-btn btn-status btn-unpublish" data-id=${item.ID}>Unpublish</a>` : `<a class="my-btn btn-status btn-publish" data-id=${item.ID}>Publish</a>`,
                            });
                        });
                        $('#resultAuthor').html(html);
                    }
                }
            });
        },
        // thêm tác giả mới
        Save: function (Author) {
            $.ajax({
                url: "/Admin/Author/Save",
                data: {
                    author: Author
                },
                type: "Post",
                dataType: "json",
                success: function (result) {
                    if (result.status == true) {
                        notify({
                            type: "success", //alert | success | error | warning | info
                            title: "Thành công",
                            position: {
                                x: "right", //right | left | center
                                y: "top" //top | bottom | center
                            },
                            size: "small",
                            autoHide: true
                        });
                        hideModal();
                        AuthorController.LoadData();
                    }
                }
            })
        },

        //Load dữ liệu dựa theo id tác giả
        LoadDetails: function (id) {
            $.ajax({
                url: "/Author/LoadDetails",
                data: { id: id },
                type: "Post",
                dataType: "json",
                success: function (result) { // result nhận json
                    var data = JSON.parse(result.author);
                    $('#id').val(data.ID);
                    $('#name').val(data.Name);
                    $('#date').val(js_yyyy_MM_dd(data.Birthday));
                    CKEDITOR.instances.info.setData(data.Info);
                }
            });
        },

        Del: function (id) {
            $.ajax({
                url: "/Author/Del",
                data: { id: id },
                type: "Post",
                dataType: "json",
                success: function (result) { // result nhận json
                    AuthorController.LoadData();
                }
            });
        },
    };
    AuthorController.init();//chạy đầu tiên
})