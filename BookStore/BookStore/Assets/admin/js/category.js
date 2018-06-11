$(document).ready(function () {
    var categoryController = {
        init: function () {
            categoryController.registerEvent();
            categoryController.loadData();
        },

        registerEvent: function () {
            $('#form-save').validate({
                rules: {
                    title: "required",
                    catalog: "required"
                },
                messages: {
                    title: "Bạn phải nhập tên",
                    catalog: "Bạn phải chọn danh mục"
                },
            })

            $(document).on('click', '.btn-delete', function (e) {
                e.preventDefault();
                var id = $(this).data('id');
                if (confirm('Bạn có chắc xóa?')) {
                    categoryController.deleteCategory(id);
                }
            });

            $(document).on('click', '.btn-status', function () {
                var id = $(this).data('id');
                categoryController.changeStatus(id);
            });

            $(document).on('change', '.sel-list', function () {
                categoryController.loadData(true);
            });

            $(document).on('click', '.btn-save', function () {
                if ($('#form-save').valid()) {
                    categoryController.saveData();
                }
            });

            $(document).on('click', '.btn-create', function () {
                var Name = $('#title').val('');
                var Alias = $('#slug').val('');
                var CatalogID = $('#catalog').val('');
                var Image = $('.img-selected').attr('src', '/Assets/admin/css/images/alter-image.jpg');
            });

            $(document).on('click', '.btn-edit', function () {
                var id = $(this).data('id');
                categoryController.loadDetail(id);
            });
        },

        resetModal: function () {
            var Name = $('#title').val('');
            var Alias = $('#slug').val('');
            var CatalogID = $('#catalog').val('');
            var Image = $('.img-selected').attr('src', '/Assets/admin/css/images/alter-image.jpg');
        },

        saveData: function () {
            //var id = $('#id').val() == "" ? 0 : $('#id').val();
            var id = $('#id').val();
            var Name = $('#title').val();
            var Alias = $('#slug').val();
            var CatalogID = $('#catalog').val();
            var Image = $('.img-selected').attr('src');
            var category = {
                ID: id,
                Name: Name,
                Alias: Alias,
                CatalogID: CatalogID,
                Image: Image,
            };
            $.ajax({
                url: '/Admin/Category/saveData',
                data: function () {
                    var data = new FormData();
                    data.append("categoryStr", JSON.stringify(category));
                    data.append("file", $("#upload").get(0).files[0]);
                    return data;
                }(),
                contentType: false,
                processData: false,
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.status == true) {
                        categoryController.resetModal();
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
                        categoryController.loadData();
                        categoryController.resetModal();
                    }
                }
            });
        },

        changeStatus: function (id) {
            $.ajax({
                url: '/Admin/Category/changeStatus',
                data: {
                    id: id
                },
                type: 'POST',
                datatype: 'json',
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
                        categoryController.loadData();
                    }
                    else {
                        notify({
                            type: "error", //alert | success | error | warning | info
                            title: "Thông báo",
                            message: "Thay đổi thành công.",
                            position: {
                                x: "right", //right | left | center
                                y: "top" //top | bottom | center
                            },
                            size: "small",
                            autoHide: true
                        });
                    }
                }
            });
        },
        loadDetail: function (id) {
            $.ajax({
                url: '/Admin/Category/getDetail',
                type: 'GET',
                data: {
                    id: id
                },
                dataType: 'json',
                success: function (response) {
                    if (response.status == true) {
                        var data = response.data;
                        $('#title').val(data.Name);
                        $('#id').val(data.ID);
                        $('#slug').val(data.Alias);
                        $('#catalog').val(data.CatalogID);
                        $('.img-selected').attr('src', data.Image);
                    }
                },
                error: function () {
                    alert("dadsa");
                }
            })
        },

        loadData: function () {
            $.ajax({
                url: '/Admin/Category/loadData',
                type: 'GET',
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        var data = response.data;
                        var html = '';
                        var template = $('#data-template').html();
                        $.each(data, function (i, item) {
                            html += Mustache.render(template, {
                                ID: item.ID,
                                Name: item.Name,
                                Alias: item.Alias,
                                Status: item.Status == true ? "<span class=\"badge badge-success\">ON</span>" : "<span class=\"badge badge-secondary\">OFF</span>",
                                btnStatus: item.Status == true ? `<a class="my-btn btn-status btn-unpublish" data-id=${item.ID}>Unpublish</a>` : `<a class="my-btn btn-status btn-publish" data-id=${item.ID}>Publish</a>`,
                                Image: item.Image != null ? item.Image : "/Assets/admin/css/images/alter-image.jpg",
                                DisplayOrder: item.DisplayOrder,
                                CatalogID: item.CatalogID,
                                CatalogName: item.CatalogName,
                            });
                        });
                        $('.amount').html(response.totalRow);
                        $('#resultBook').html(html);
                    }
                },
                error: function () {
                    alert("error");
                }
            })
        },

        deleteCategory: function (id) {
            $.ajax({
                url: "/Admin/Category/Delete",
                data: {
                    id: id
                },
                type: "POST",
                dataType: "json",
                success: function (response) {
                    if (response.status) {
                        notify({
                            type: "success", //alert | success | error | warning | info
                            title: "Xóa thành công!",
                            position: {
                                x: "right", //right | left | center
                                y: "top" //top | bottom | center
                            },
                            size: "small",
                            autoHide: true
                        });
                        categoryController.loadData();
                    }
                }
            });
        }
    };
    categoryController.init();
});