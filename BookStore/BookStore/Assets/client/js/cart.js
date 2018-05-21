var BookStore = (function () {

    var cart = [];

    function Item(id,name, price, count) {
        this.id = id;
        this.name = name;
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

    obj.addItemToCart = function (id,name, price) { // add a item
        for (var i in cart) {
            if (cart[i].id === id) {
                return;
            }
        }
        var item = new Item(id,name, price, 1);
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
            output += '<div class="product id'+ cart[i].id +'" data-id="' + cart[i].id + '">';
            output += '<div class="title-product"><span>' + cart[i].count + '</span> x ' + cart[i].name + ' </div>';
            output += '<div class="price"> ' + obj.format_money(cart[i].price) + '</div>';
            output += '</div>';
        };
        $('.list-product').html(output);
        obj.totalItem();
        obj.totalCost();
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

    obj.returnTotalCost = function () {
        var total = 0;
        for (var i in cart) {
            total += cart[i].price * cart[i].count;
        }
        return total;
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
        if (cart == null) {
            cart = [];
        }
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
    var name = $(this).data('name');
    BookStore.addItemToCart(id,name, price);
    BookStore.countItemInCart();
});


