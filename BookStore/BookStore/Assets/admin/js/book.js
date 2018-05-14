$('#CategoryID').chosen();
$('#AuthorID').chosen();
$("#AuthorID").chosen({ no_results_text: "Không tìm thấy :(( " });
$('#AuthorID').trigger('chosen:updated');

$(document).on('click', '#delete-link', function () {
    if (confirm('Bạn có chắc là xóa hình này?')) {
        var filename = $('#result .hinh-anh.active').attr('src');
        $.ajax({
            type: "POST",
            url: "/Admin/Book/deleteImage",
            dataType: "json",
            data: { filename: filename } ,
            success: function (result) {
                reset();
                console.log(result);
                alert("Xóa thành công");
                var html = "";
                for (var i = 0; i < result.length; i++) {
                    html += "<div class='col-sm-3 a-book'>";
                    html += "<img src=" + result[i].path + " class='hinh-anh' alt='Alternate Text' />";
                    html += "<span class='ten-hinh-anh'>" + result[i].name + "</span>";
                    html += "</div>";
                }
                $('#result').html(html);
            },
            error: function (err) {
                alert(err.statusText);
            }
        });
    }
});

