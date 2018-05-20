$(document).ready(function () {
    var authorityController = {
        //Contrucstor ===========
        init: function () {
            authorityController.registerEvent();
            authorityController.loadData();
        },

        //Register Envent function ===========
        registerEvent: function () {
            $('#form-save-add').validate({
                rules: {
                    username: "required",
                    password: "required",
                    fullname: "required",
                    email: "required"
                },
                messages: {
                    username: "Bạn phải nhập tài khoản",
                    password: "Bạn phải nhập mật khẩu",
                    fullname: "Bạn phải nhập họ tên",
                    email: "Bạn phải nhập email"
                },
            });

            $(document).on('click', '.btn-edit', function () {
                var username = $(this).data('id');
                authorityController.loadBusiness(username);
            });
            
            //$(document).on('click', '.btn-status', function () {
            //    var business = $(this).data('id');
            //    authorityController.updateBusiness(business);
            //});

            $(document).on('click', '.btn-status', function () {
                var user = $(this).data('id');
                authorityController.changeStatus(user);
            });

            $(document).on('click', '.btn-save', function () {
                if ($('#form-save-add').valid()) {
                    authorityController.add();
                    authorityController.resetModal();
                }
            });

            $(document).on('click', '.btn-delete', function (e) {
                e.preventDefault();
                var username = $(this).data('id');
                if (confirm('Bạn có chắc xóa?')) {
                    authorityController.deleteUser(username);
                    authorityController.loadData();
                }
            });
        },

        //function =========== 
        resetModal: function () {
            $('#username').val('');
            $('#password').val('');
            $('#fullname').val('');
            $('#email').val('');
        },

        changeStatus: function (username) {
            $.ajax({
                url: '/Admin/Administator/changeStatus',
                data: {
                    username: username
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
                        authorityController.loadData();
                    }
                }
            });
        },

        add: function () {
            var username = $('#username').val();
            var password = $('#password').val();
            var fullname = $('#fullname').val();
            var email = $('#email').val();
            var isAdmin = $('#isAdmin').val();

            var user = {
                Username: username,
                Password: password,
                Fullname: fullname,
                Email: email,
                isAdmin: isAdmin,
            }

            $.ajax({
                url: '/Admin/Administator/Add',
                data: {
                    userStr : JSON.stringify(user)
                },
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        notify({
                            type: "success", //alert | success | error | warning | info
                            title: "Thêm thành viên thành công",
                            position: {
                                x: "right", //right | left | center
                                y: "top" //top | bottom | center
                            },
                            size: "small",
                            autoHide: true
                        });
                        hideModal();
                        authorityController.loadData();
                    }
                    else {
                        notify({
                            type: "error", //alert | success | error | warning | info
                            title: "Tài khoản đã tồn tại",
                            position: {
                                x: "right", //right | left | center
                                y: "top" //top | bottom | center
                            },
                            size: "small",
                            autoHide: true
                        });
                    }
                },
            });
        },

        loadData: function () {
            $.ajax({
                url: '/Admin/Administator/loadData',
                type: 'GET',
                dataType: 'json',
                success: function (data) {
                    var html = '';
                    var template = $('#data-template').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            Username: item.Username,
                            Image: item.Image,
                            Fullname: item.Fullname,
                            Email: item.Email,
                            btnStatus: item.Status == true ? `<a class="my-btn btn-status btn-unpublish" data-id=${item.Username}>Lock</a>` : `<a class="my-btn btn-status btn-publish" data-id=${item.Username}>Active</a>`,
                            isAdmin: item.isAdmin == true ? 'ADMIN' : 'USER'
                        });
                    });
                    $('#result').html(html);
                },
                error: function () {
                    alert('loadData Error');
                },
            })
        },

        loadBusiness: function (username) {
            $.ajax({
                url: "/Admin/Administator/getBusiness",
                data: {
                    username: username
                },
                type: 'GET',
                dataType: 'json',
                success: function (response) {
                    var html = '';
                    var data = response.data;
                    var template = $('#business-template').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ID: item.ID,
                            Name: item.Name,
                            Url: item.Url,
                            Status: item.Status == true ? "Checked" : "",
                        });
                    });
                    $('.username').val(response.username);
                    $('.username').text(response.username);
                    $('#result-business').html(html);
                },
            })
        },

        updateBusiness: function (business) {
            var username = $('.username').val();
            $.ajax({
                url: "/Admin/Administator/updateBusiness",
                data: {
                    business: business,
                    username: username
                },
                type:'POST',
                dataType: 'json',
                success: function (result) {
                    if (result.status) {
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
                    }
                    else {
                        notify({
                            type: "error", //alert | success | error | warning | info
                            title: "Cập nhật thất bại",
                            position: {
                                x: "right", //right | left | center
                                y: "top" //top | bottom | center
                            },
                            size: "small",
                            autoHide: true
                        });
                    }
                },
                error: function () {
                    notify({
                        type: "error", //alert | success | error | warning | info
                        title: "Cập nhật thất bại",
                        position: {
                            x: "right", //right | left | center
                            y: "top" //top | bottom | center
                        },
                        size: "small",
                        autoHide: true
                    });
                }
            });
        },

        deleteUser: function (username) {
            $.ajax({
                url: "/Admin/Administator/Delete",
                data: {
                    username: username
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
                        authorityController.loadData();
                    }
                    else {
                        notify({
                            type: "error", //alert | success | error | warning | info
                            title: "Xóa không thành công!",
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
    };
    authorityController.init();
});