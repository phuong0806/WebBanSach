var address = {
    init: function () {
        address.loadProvince();
        address.registerEvent();
    },
    registerEvent: function () {
        $('#ddlProvince').off('change').on('change', function () {
            var id = $(this).val();
            if (id != '') {
                address.loadDistrict(parseInt(id));
            }
            else {
                $('#ddlDistrict').html('');
            }
        });

        $('#ddlDistrict').off('change').on('change', function () {
            var id = $(this).val();
            if (id != '') {
                address.loadPrecinct(parseInt(id));
            }
            else {
                $('#ddlPrecinct').html('');
            }
        });
    },
    loadProvince: function () {
        $.ajax({
            url: '/Order/LoadProvince',
            type: "POST",
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option disabled selected>--Chọn tỉnh thành--</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.ID + '">' + item.Name + '</option>'
                    });
                    $('#ddlProvince').html(html);
                }
            }
        })
    },
    loadDistrict: function (id) {
        $.ajax({
            url: '/Order/LoadDistrict',
            type: "POST",
            data: { provinceID: id },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option disabled selected>--Chọn quận huyện--</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.ID + '">' + item.Name + '</option>'
                    });
                    $('#ddlDistrict').html(html);
                }
            }
        })
    },

    loadPrecinct: function (id) {
        $.ajax({
            url: '/Order/LoadPrecinct',
            type: 'POST',
            data: { districtID: id },
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var html = '<option disabled selected>--Chọn phường xã--</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.ID + '">' + item.Name + '</option>'
                    });
                    $('#ddlPrecinct').html(html);
                }
            },
        });
    }
}
address.init();