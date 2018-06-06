using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class RegisterModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name="Tên đăng nhập")]
        [Required(ErrorMessage ="Không bỏ trống Tên đăng nhập")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [StringLength(20,MinimumLength =6,ErrorMessage ="Ít nhất 6 ký tự")]
        [Required(ErrorMessage = "Không bỏ trống mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Xác nhận Mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu không trùng khớp")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Xin cho biết họ tên")]
        public string Name { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Email liên hệ")]
        [Required(ErrorMessage = "Không bỏ trống email")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Không bỏ trống số điện thoại")]
        public string Phone { get; set; }
    }
}