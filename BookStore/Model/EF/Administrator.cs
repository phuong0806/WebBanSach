namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Administrator")]
    public partial class Administrator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Administrator()
        {
            Businesses = new HashSet<Business>();
        }

        [Key]
        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(32)]
        public string Password { get; set; }

        [StringLength(250)]
        public string Fullname { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        public bool? Status { get; set; }

        public bool? isAdmin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Business> Businesses { get; set; }
    }
}
