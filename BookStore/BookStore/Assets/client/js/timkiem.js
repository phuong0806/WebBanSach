var common = {
    init: function () {
        common.registerEvent();
    },
    registerEvent: function () {
        $('#txttukhoa').autocomplete({
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/Timkiem/ListName",
                    dataType: "json",
                    data: {
                        term: request.term
                    },
                    success: function (respond) {
                        response(respond.data);
                    }
                });
            },
            focus: function (event, ui) {
                $("#txttukhoa").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txttukhoa").val(ui.item.label);
                return false;
            }
        }).autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>")
              .append("<div>" + item.label + "<br>" + "</div>")
              .appendTo(ul);
        };
    }
}
common.init();