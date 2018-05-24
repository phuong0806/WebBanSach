var BookStore = (function () {

    var cart = [];

    function Item(id,name, Price, Number) {
        this.ID = id;
        this.Name = name;
        this.Number = Number;
        this.Price = Price;
    };

    function saveCart() {
        localStorage.setItem("BookStore", JSON.stringify(cart));
    }

    function loadCart() {
        cart = JSON.parse(localStorage.getItem("BookStore"));
    }

    //Public methods and properties
    var obj = {};

    obj.addItemToCart = function (id,name, Price) { // add a item
        for (var i in cart) {
            if (cart[i].ID === id) {
                return;
            }
        }
        var item = new Item(id,name, Price, 1);
        if (cart === null)
            cart = [];
        cart.push(item);
        saveCart();
    }

    obj.increaseItemInCart = function (id, Number) { // add a item
        for (var i in cart) {
            if (cart[i].ID === id) {
                cart[i].Number += Number;
                saveCart();
                return cart[i].Number;
            }
        }
    }

    obj.decreaseItemInCart = function (id, Number) {
        for (var i in cart) {
            if (cart[i].ID === id && cart[i].Number > 1) {
                cart[i].Number -= Number;
                saveCart();
                return cart[i].Number;
            } else {
                return 1;
            }
        }
    }

    obj.displayNumberItem = function () {
        for (var i in cart) {
            $('.Number.id' + cart[i].ID).val(cart[i].Number);
        }
    }

    obj.listID = function () {
        var list = [];
        console.log(cart);
        for (var i in cart) {
            list.push(cart[i]);
        }
        return list;
    };

    obj.loadCart = function () {
        var output = "";
        for (var i in cart) {
            output += '<div class="product id'+ cart[i].ID +'" data-id="' + cart[i].ID + '">';
            output += '<div class="title-product"><span>' + cart[i].Number + '</span> x ' + cart[i].Name + ' </div>';
            output += '<div class="Price"> ' + obj.format_money(cart[i].Price) + '</div>';
            output += '</div>';
        };
        $('.list-product').html(output);
        obj.totalItem();
        obj.totalCost();
    }

    obj.totalItem = function () {
        var total = 0;
        for (var i in cart) {
            total += cart[i].Number;
        }
        $('.total-product').html(total);
    }

    obj.totalCost = function () {
        var total = 0;
        for (var i in cart) {
            total += cart[i].Price * cart[i].Number;
        }
        $('.cost').html(obj.format_money(total));
    }

    obj.returnTotalCost = function () {
        var total = 0;
        for (var i in cart) {
            total += cart[i].Price * cart[i].Number;
        }
        return total;
    }

    obj.removeItemInCart = function (id) {
        for (var i in cart) {
            if (cart[i].ID === id) {
                cart.splice(i, 1);
                saveCart();
                return;
            }
        }
    }

    obj.NumberItemInCart = function () {
        if (cart == null) {
            cart = [];
        }
        $('.number-item-cart').html(cart.length);
    }

    obj.format_money = function (Number) {
        return Number.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });;
    }

    loadCart();

    obj.NumberItemInCart();

    return obj;
})();

$('.add-to-cart').click(function (event) { // thêm sản phẩm vào giỏ hàng
    event.preventDefault();
    var id = $(this).data('id');
    var Price = $(this).data('Price');
    var name = $(this).data('name');
    BookStore.addItemToCart(id,name, Price);
    BookStore.NumberItemInCart();
});


