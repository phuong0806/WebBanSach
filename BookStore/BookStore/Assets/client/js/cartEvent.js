//EVENTS ====================
var cartController = {
    init: function () {
        cartController.registerEvents();
        cartController.loadData();
    },  

    registerEvents: function () {   
        $(document).on('click', '.add', function () {
            event.preventDefault();
            var id = $(this).data('id');
            $('.number.id' + id).val(BookStore.increaseItemInCart(id, 1));
            BookStore.totalCost();
            BookStore.totalItem();
        });

        $(document).on('click', '.minus', function () {
            event.preventDefault();
            var id = $(this).data('id');
            $('.number.id' + id).val(BookStore.decreaseItemInCart(id, 1));
            BookStore.totalCost();
            BookStore.totalItem();
        });

        $(document).on('click', '.removeItem', function () {
            event.preventDefault();
            if (confirm("Bạn có chắc xóa")) {
                var id = $(this).data('id');
                BookStore.removeItemInCart(id);
                cartController.loadData();
            }
        });
    },

    loadData: function () {
        $.ajax({
            url: "/Cart/loadData",
            data: {
                listCartString: JSON.stringify(JSON.parse(localStorage.getItem("BookStore")))
            },
            type: "POST",
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '';
                    var template = $('#data-template').html();
                    $.each(JSON.parse(response.data), function (i, item) {
                        html += Mustache.render(template, {
                            ID: item.ID,
                            Name: item.Name,
                            Authors: item.Authors[0].Name,
                            BookCover: item.BookCover,
                            Price: item.Price,
                            Image: item.Image,
                        });
                    });
                    $('.list-product').html(html);
                    BookStore.displayNumberItem();
                    BookStore.totalItem();
                    BookStore.totalCost();
                }
                else {
                    window.location.href = "/Cart/emptyCart";
                }
            },
            error: function () {
                alert("error");
            }
        })
    }
};
cartController.init();