$(document).ready(function () {
    var AuthorController = {
        init: function () {//load dữ liệu
            AuthorController.LoadData();
            AuthorController.registerEvent();
        },

        registerEvent: function () {//xử lý sự kiện
            $('.btn-save').click(function () {
                var Name = $('#name').val();
                var Birthday = $('#date').val();
                var Info = $('#info').val();
                var Status = true;
                var Author = {
                    Name: Name,
                    Birthday: Birthday,
                    Info: Info,
                    Status: Status
                };
                AuthorController.Add(Author);
            });

        },
        //Lấy toàn bộ tác giả
        LoadData: function () {
            $.ajax({
                url: "/Author/LoadData",
                type: "Get",
                dataType: "json",
                success: function (result) {
                    if (result.status == true) {
                        var data = result.data;
                        var template = $('#data-template').html();
                        var html = '';
                        $.each(data, function (i, item) {//dòng foreach chạy dữ liệu
                            html += Mustache.render(template, {
                                ID: item.ID,
                                Name: item.Name,
                                Info: item.Info,
                                Birthday: item.Birthday,
                                btnStatus: item.Status == true ? `<a class="my-btn btn-status btn-unpublish" data-id=${item.ID}>Unpublish</a>` : `<a class="my-btn btn-status btn-publish" data-id=${item.ID}>Publish</a>`,
                            });
                        });
                        $('#resultAuthor').html(html);
                    }

                }
            });
        },

        Add: function (Author) {
            $.ajax({
                url: "/Author/Add",
                data:{
                    author:Author
                },
                type: "Post",
                dataType: "json",
                success: function (result) {
                    if(result.status==true)
                    {
                        AuthorController.LoadData();
                    }
                }
            })
        }
    };


    AuthorController.init();//chạy đầu tiên
})