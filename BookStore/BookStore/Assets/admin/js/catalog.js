$(document).ready(function () {
    var CatalogController = {
        init: function () {//load dữ liệu
            CatalogController.LoadData();
            CatalogController.registerEvent();
        },

        registerEvent: function () {//xử lý sự kiện
            $('.btn-save').click(function () {
                var Name = $('#title').val();
                var Alias = $('#slug').val();
                var id = $('#id').val();
                var Status = true;
                var Catalog = {
                    ID: id,
                    Alias: Alias,
                    Name: Name,
                    Status: Status
                };
                CatalogController.Save(Catalog);
            });

            $(document).on('click', '.btn-edit', function () {
                var id = $(this).data('id');
                CatalogController.LoadDetails(id);
            });

            $(document).on('click', '.btn-create', function () {
                CatalogController.reset();
            });

            $(document).on('click', '.btn-delete', function () {
                if (confirm("Bạn chắc chắn xóa?")) {
                    var id = $(this).data('id');
                    CatalogController.Del(id);
                }
            });
        },

        reset: function () {
            $('#tittle').val('');
            $('#slug').val('');
            $('#id').val('');
        },

        LoadData: function () {
            $.ajax({
                url: "/Catalog/LoadData",
                type: "Get",
                dataType: "json",
                success: function (result) {
                    if (result.status == true) {
                        var data = JSON.parse(result.data);
                        var template = $('#data-template').html();
                        var html = '';

                        $.each(data, function (i, item) {//dòng foreach chạy dữ liệu
                            var a = js_yyyy_mm_dd_hh_mm_ss(toDateTime(item.Birthday));
                            html += Mustache.render(template, {
                                ID: item.ID,
                                Name: item.Name,
                                Alias: item.Alias,
                                btnStatus: item.Status == true ? `<a class="my-btn btn-status btn-unpublish" data-id=${item.ID}>Unpublish</a>` : `<a class="my-btn btn-status btn-publish" data-id=${item.ID}>Publish</a>`,
                            });
                        });
                        $('#resultCatalog').html(html);
                    }
                }
            });
        },

        Save: function (Catalog) {
            $.ajax({
                url: "/Catalog/Save",
                data: {
                    catalog: Catalog
                },
                type: "Post",
                dataType: "json",
                success: function (result) {
                    if (result.status == true) {
                        CatalogController.reset();
                        CatalogController.LoadData();
                    }
                }
            })
        },

        LoadDetails: function (id) {
            $.ajax({
                url: "/Catalog/LoadDetails",
                data: { id: id },
                type: "Post",
                dataType: "json",
                success: function (result) { // result nhận json
                    var data = result.ca;
                    $('#id').val(data.ID);
                    $('#title').val(data.Name);
                    $('#slug').val(data.Alias);
                }
            });
        },

        Del: function (id) {
            $.ajax({
                url: "/Catalog/Del",
                data: { id: id },
                type: "Post",
                dataType: "json",
                success: function (result) { // result nhận json
                    CatalogController.LoadData();
                }
            });
        },
    };
    CatalogController.init();//chạy đầu tiên
})