namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        [StringLength(50)]
        public string TaiKhoanAD { get; set; }

        [StringLength(50)]
        public string MatKhauAD { get; set; }

        [StringLength(250)]
        public string HoTen { get; set; }

        public bool? TrangThai { get; set; }

        public bool? isAdmin { get; set; }
    }
}
