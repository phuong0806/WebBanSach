$(document).ready(function () {
    var authorityController = {
        //Contrucstor ===========
        init: function () {
            authorityController.registerEvent();
            authorityController.loadData();
        },

        //Register Envent function ===========
        registerEvent: function () {
            $(document).on('click', '.btn-edit', function () {
                var username = $(this).data('id');
                authorityController.loadBusiness(username);
            });
            
            $(document).on('click', '.btn-status', function () {
                var business = $(this).data('id');
                authorityController.updateBusiness(business);
            });
        },

        //function =========== 
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
                            btnStatus: item.Status == true ? `<a class="my-btn btn-status btn-publish" data-id=${item.ID}>Publish</a>` : `<a class="my-btn btn-status btn-unpublish" data-id=${item.ID}>Unpublish</a>`,
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
    };
    authorityController.init();
});