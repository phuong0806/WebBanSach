var orderController = {
    init: function () {
        BookStore.loadCart(); // load product
        orderController.registerEvent();
    },
    // register event function =======================================
    registerEvent: function () {
        $('#form-save-add').validate({
            rules: {
                name: "required",
                email: "required",
                phone: "required",
                Province: "required",
                District: "required",
                Precinct: "required",
                address: "required",
            },
            messages: {
                name: "Bạn phải nhập họ tên",
                email: "Bạn phải nhập email",
                phone: "Bạn phải nhập số điện thoại",
                Province: "Bạn phải chọn Tỉnh/Thành",
                District: "Bạn phải chọn Quận/Huyện",
                Precinct: "Bạn phải chọn Phường/Xã",
                address: "Bạn phải nhập địa chỉ",
            },
        });

        $(document).on('click', '.btn-continue', function () {
            if ($('#form-save-add').valid()) {
                orderController.add();
            }
        });
    },

    // function =======================================
    add: function () {
        var listProduct = BookStore.listID()
        console.log(listProduct);
        var totalCost = BookStore.returnTotalCost();
        var name = $('#name').val();
        var phone = $('#phone').val();
        var email = $('#email').val();
        var province = $('#ddlProvince option:selected').text();
        var district = $('#ddlDistrict option:selected').text();
        var precinct = $('#ddlPrecinct option:selected').text();
        var address = $('#address').val();
        var orderDetail = {
            CustomerName: name,
            Phone: phone,
            Email: email,
            Province: province,
            District: district,
            Precinct: precinct,
            Address: address
        }
        $.ajax({
            url: "/Order/Add",
            data: {
                orderDetail: JSON.stringify(orderDetail),
                listProduct: listProduct,
                totalCost: totalCost
            },
            type: "POST",
            dataType: "json",
            success: function (response) {
                if (response.status) {
                    window.location.href = '/Order/SuccessOrder?id=' + response.id;
                }
            }
        })
    }
}
orderController.init();