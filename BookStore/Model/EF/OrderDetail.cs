namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetail
    {
        public int ID { get; set; }

        [StringLength(250)]
        public string CustomerName { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Province { get; set; }

        [StringLength(250)]
        public string District { get; set; }

        [StringLength(250)]
        public string Precinct { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        public int? OrderID { get; set; }

        public virtual Order Order { get; set; }
    }
}
