
function ChangeToSlug() {
    var title, slug;

    //Lấy text từ thẻ input title 
    title = document.getElementById("title").value;

    //Đổi chữ hoa thành chữ thường
    slug = title.toLowerCase();

    //Đổi ký tự có dấu thành không dấu
    slug = slug.replace(/á|à|ả|ạ|ã|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ/gi, 'a');
    slug = slug.replace(/é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ/gi, 'e');
    slug = slug.replace(/i|í|ì|ỉ|ĩ|ị/gi, 'i');
    slug = slug.replace(/ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ/gi, 'o');
    slug = slug.replace(/ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự/gi, 'u');
    slug = slug.replace(/ý|ỳ|ỷ|ỹ|ỵ/gi, 'y');
    slug = slug.replace(/đ/gi, 'd');
    //Xóa các ký tự đặt biệt
    slug = slug.replace(/\`|\~|\!|\@|\#|\||\$|\%|\^|\&|\*|\(|\)|\+|\=|\,|\.|\/|\?|\>|\<|\'|\"|\:|\;|_/gi, '');
    //Đổi khoảng trắng thành ký tự gạch ngang
    slug = slug.replace(/ /gi, "-");
    //Đổi nhiều ký tự gạch ngang liên tiếp thành 1 ký tự gạch ngang
    //Phòng trường hợp người nhập vào quá nhiều ký tự trắng
    slug = slug.replace(/\-\-\-\-\-/gi, '-');
    slug = slug.replace(/\-\-\-\-/gi, '-');
    slug = slug.replace(/\-\-\-/gi, '-');
    slug = slug.replace(/\-\-/gi, '-');
    //Xóa các ký tự gạch ngang ở đầu và cuối
    slug = '@' + slug + '@';
    slug = slug.replace(/\@\-|\-\@|\@/gi, '');
    //In slug ra textbox có id “slug”
    document.getElementById('slug').value = slug;
}


/* MODAL IMAGE */
$(function () {
    $("#upload-link").on('click', function (e) {
        e.preventDefault();
        $("#upload:hidden").trigger('click');
    });
});

//$.validator.setDefaults({ ignore: ":hidden:not(select)" })
function readURL(input) {
    let url = URL.createObjectURL(input.files[0]);
    $('.img-selected')
             .attr('src', url);
    $('.image-selected').text(input.files[0].name);
    $('#img-book').val(input.files[0]);
}
function reset() {
    $('.image-selected').text('...');
    $('.img-selected').attr('src', '/Assets/admin/css/images/alter-image.jpg');
    $('#result .hinh-anh').removeClass('active');
    $('#img-book').val('');
}

$(document).on('click', '#result .hinh-anh', function () {
    var urlImg = $(this).attr('src');
    var idImg = $(this).attr('id');
    $('#result .hinh-anh').removeClass('active');
    $(this).toggleClass('active');
    $('.img-selected').attr('src', urlImg)
    $('#img-book').val(urlImg);
    $('.image-selected').text(idImg);
})

$(document).on('click', '#reset-link', function () {
    reset();
});

$('.background-black').on('click', function () {
    $('.manage-image').css({ 'display': 'none', 'visibility': 'hidden' });
    $('.background-black').css({ 'display': 'none', 'visibility': 'hidden' });
});

$('.btn-chon-anh').on('click', function () {
    $('.manage-image').css({ 'display': 'block', 'visibility': 'visible' });
    $('.background-black').css({ 'display': 'block', 'visibility': 'visible' });
});
/* end MODAL IMAGE */

/* MODAL BOX ADD/UPDATE */

function hideModal() {
    setTimeout(function () {
        $('.modalBox').removeClass('active');
        $('.modalBox').removeClass('fadeInDown');
    }, 500);
    $('.modalBox').addClass('fadeOut');
    $('.blurBackground').removeClass('active');
};

$(document).on('click', '.btn-action', function () {
    $('.modalBox').removeClass('fadeOut');
    $('.modalBox').addClass('active');
    $('.modalBox').addClass('fadeInDown');
    $('.blurBackground').addClass('active');
    if ($(this).hasClass('btn-edit')) {
        $('.title-modal').text('CẬP NHẬT');
        $('.btn-save').text('Lưu')
    }
    else {
        $('.title-modal').text('THÊM');
        $('.btn-save').text('Thêm');
    }
});

$(document).on('click', '.ti-close', function () {
    setTimeout(function () {
        $('.modalBox').removeClass('active');
        $('.modalBox').removeClass('fadeInDown');
    }, 200);
    $('.modalBox').addClass('fadeOut');
    $('.blurBackground').removeClass('active');
});

/* end MODAL BOX ADD/UPDATE */