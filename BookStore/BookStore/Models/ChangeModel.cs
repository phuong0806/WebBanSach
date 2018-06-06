using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class ChangeModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu cũ")]
        [Required(ErrorMessage = "Không bỏ trống mật khẩu cũ")]
        public string Password { get; set; }

        [Display(Name = "Mật khẩu mới")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Ít nhất 6 ký tự")]
        [Required(ErrorMessage = "Không bỏ trống mật khẩu")]
        public string NewPassword { get; set; }

        [Display(Name = "Xác nhận mật khẩu mới")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu không trùng khớp")]
        public string ConfirmNewPassword { get; set; }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Xin cho biết họ tên")]
        public string Name { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Email liên hệ")]
        [Required(ErrorMessage = "Không bỏ trống email")]
        [RegularExpression(@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
     + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
     + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
     + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$", ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Không bỏ trống số điện thoại")]
        [RegularExpression("^(\\+84|0)\\d{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; }
    }
}