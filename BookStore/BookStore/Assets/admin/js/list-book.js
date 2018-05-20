$(document).ready(function () {
    //parse date
    //function convertParseJsonDate(str) {
    //    var str = new Date(parseInt(str.replace('/Date(', ''))); //parse date from json to string
    //    //foarmat dd-MM-yyyy
    //    var date = new Date(str),
    //        mnth = ("0" + (date.getMonth() + 1)).slice(-2),
    //        day = ("0" + date.getDate()).slice(-2);
    //    return [mnth, date.getFullYear()].join("-");
    //}


    $(document).on('click', '.title-book', function () {
        var id = $(this).attr('id');
        $('#detail-book-' + id).toggle();
        console.log("AA");
    });

    var bookConfig = {
        pageSize: 10,
        pageIndex: 1,
    }

    var bookController = {
        init: function () {
            bookController.registerEvent();
            bookController.loadData();
        },

        registerEvent: function () {
            $(document).on('click', '.btn-delete', function (e) {
                e.preventDefault();
                var id = $(this).data('id');
                if (confirm("Bạn có chắc xóa sách này không?")) {
                    bookController.deleteBook(id);
                }
            });

            $(document).on('keyup', '.search-box', function () {
                bookController.loadData(true);
            });

            $(document).on('change', '.sel-list', function () {
                bookController.loadData(true);
            });

            $(document).on('click', '.btn-status', function () {
                var id = $(this).data('id');
                bookController.changeStatus(id);
            });
        },

        changeStatus: function (id) {
            $.ajax({
                url: '/Admin/Book/ChangeStatus',
                data: {
                    id: id
                },
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.status == true) {
                        notify({
                            type: "success", //alert | success | error | warning | info
                            title: "Thông báo",
                            message: "Thay đổi thành công.",
                            position: {
                                x: "right", //right | left | center
                                y: "top" //top | bottom | center
                            },
                            size: "normal",
                            autoHide: true
                        });
                        bookController.loadData();
                    }
                }
            });
        },
        deleteBook: function (id) {
            $.ajax({
                url: '/Admin/Book/DeleteBook',
                data: {
                    id: id
                },
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.status == true) {
                        notify({
                            type: "success", //alert | success | error | warning | info
                            title: "Xóa sách thành công!",
                            position: {
                                x: "right", //right | left | center
                                y: "top" //top | bottom | center
                            },
                            size: "small",
                            autoHide: true
                        });
                        bookController.loadData(true);
                    }
                }
            });
        },

        loadData: function (changePageSize) {
            var searchText = $('#search-text').val();
            var statusSelect = $('.sel-list').val();
            $.ajax({
                url: '/Admin/Book/loadData',
                contentType: "application/json; charset=utf-8",
                type: "GET",
                data: {
                    searchText: searchText,
                    statusSelect: statusSelect,
                    page: bookConfig.pageIndex,
                    pageSize: bookConfig.pageSize
                },
                dataType: 'json',
                success: function (response) {
                    console.log("AA");
                    if (response.status == true) {
                        var data = response.data;
                        var html = '';
                        var template = $('#data-template').html();
                        $.each(data, function (i, item) {
                            html += Mustache.render(template, {
                                ID: item.ID,
                                Name: item.Name,
                                Alias: item.Alias,
                                Price: item.Price,
                                Quanlity: item.Quanlity,
                                ViewCount: item.ViewCount,
                                Status: item.Status == true ? "<span class=\"badge badge-success\">ON</span>" : "<span class=\"badge badge-secondary\">OFF</span>",
                                btnStatus: item.Status == true ? `<a class="my-btn btn-status btn-unpublish" data-id=${item.ID}>Unpublish</a>` : `<a class="my-btn btn-status btn-publish" data-id=${item.ID}>Publish</a>`,
                                Image: item.Image != null ? item.Image : "/Assets/admin/css/images/alter-image.jpg",
                        
                            });
                        });
                        $('.amount').html(response.totalRow);
                        $('#resultBook').html(html);
                        bookController.paging(response.totalRow, function () {
                            bookController.loadData();
                        }, changePageSize);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
        },

        paging: function (totalRow, Callback, changePageSize) {
            var totalPage = Math.ceil(totalRow / bookConfig.pageSize);

            if ($('#pagination a').length === 0 || changePageSize === true) {
                $('#pagination').empty();
                $('#pagination').removeData("twbs-pagination");
                $('#pagination').unbind("page");
            }

            $('#pagination').twbsPagination({
                totalPages: totalPage,
                visiblePages: 7,
                first: "Đầu",
                next: "Tiếp",
                last: "Cuối",
                prev: "Trước",
                onPageClick: function (event, page) {
                    bookConfig.pageIndex = page;
                    setTimeout(Callback, 200);
                }
            });
        }
    }
    bookController.init();
});

//loadData: function (changePageSize) {
//    var searchText = $('#search-text').val();
//    var statusSelect = $('.sel-list').val();
//    $.ajax({
//        url: '/Admin/Book/loadData',
//        type: "GET",
//        data: {
//            searchText: searchText,
//            statusSelect: statusSelect,
//            page: bookConfig.pageIndex,
//            pageSize: bookConfig.pageSize
//        },
//        dataType: 'json',
//        success: function (response) {
//            if (response.status == true) {
//                var data = response.data;
//                var html = '';
//                var template = $('#data-template').html();
//                $.each(data, function (i, item) {
//                    html += Mustache.render(template, {
//                        ID: item.ID,
//                        Name: item.Name,
//                        Alias: item.Alias,
//                        Price: item.Price,
//                        Quanlity: item.Quanlity,
//                        ViewCount: item.ViewCount,
//                        Status: item.Status == true ? "<span class=\"badge badge-success\">ON</span>" : "<span class=\"badge badge-secondary\">OFF</span>",
//                        Image: item.Image != null ? item.Image : "/Assets/admin/css/images/alter-image.jpg",
//                        PublicationDate: convertParseJsonDate(item.PublicationDate),
//                        BookCover: item.BookCover,
//                        Catalog: item.CatalogName,
//                        Publisher: item.PublisherName,
//                        Category: item.CategoryName,
//                        Author: item.Authors,
//                    });
//                });
//                $('.amount').html(response.totalRow);
//                $('#resultBook').html(html);
//                bookController.paging(response.totalRow, function () {
//                    bookController.loadData();
//                }, changePageSize);
//            }
//        }
//    });