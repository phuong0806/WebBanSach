var BookStore = (function () {

    var cart = [];

    function Item(id, price, count) {
        this.id = id;
        this.count = count;
        this.price = price;
    };

    function saveCart() {
        localStorage.setItem("BookStore", JSON.stringify(cart));
    }

    function loadCart() {
        cart = JSON.parse(localStorage.getItem("BookStore"));
    }

    //Public methods and properties
    var obj = {};

    obj.addItemToCart = function (id, price) { // add a item
        for (var i in cart) {
            if (cart[i].id === id) {
                return;
            }
        }
        var item = new Item(id, price, 1);
        if (cart === null)
            cart = [];
        cart.push(item);
        saveCart();
    }

    obj.increaseItemInCart = function (id, count) { // add a item
        for (var i in cart) {
            if (cart[i].id === id) {
                cart[i].count += count;
                saveCart();
                return cart[i].count;
            }
        }
    }

    obj.increaseItemInCart = function (id, count) { // add a item
        for (var i in cart) {
            if (cart[i].id === id) {
                cart[i].count += count;
                saveCart();
                return cart[i].count;
            }
        }
    }

    obj.decreaseItemInCart = function (id, count) {
        for (var i in cart) {
            if (cart[i].id === id && cart[i].count > 1) {
                cart[i].count -= count;
                saveCart();
                return cart[i].count;
            } else {
                return 1;
            }
        }
    }

    obj.displayNumberItem = function () {
        for (var i in cart) {
            $('.number.id' + cart[i].id).val(cart[i].count);
        }
    }

    obj.totalItem = function () {
        var total = 0;
        for (var i in cart) {
            total += cart[i].count;
        }
        $('.total-product').html(total);
    }

    obj.totalCost = function () {
        var total = 0;
        for (var i in cart) {
            total += cart[i].price * cart[i].count;
        }
        $('.cost').html(obj.format_money(total));
    }

    obj.removeItemInCart = function (id) {
        for (var i in cart) {
            if (cart[i].id === id) {
                cart.splice(i, 1);
                saveCart();
                return;
            }
        }
    }

    obj.countItemInCart = function (id) {
        $('.number-item-cart').html(cart.length);
    }

    obj.format_money = function (number) {
        return number.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });;
    }

    loadCart();

    obj.countItemInCart();

    return obj;
})();

$('.add-to-cart').click(function (event) { // thêm sản phẩm vào giỏ hàng
    event.preventDefault();
    var id = $(this).data('id');
    var price = $(this).data('price');
    BookStore.addItemToCart(id, price);
    BookStore.countItemInCart();
});


