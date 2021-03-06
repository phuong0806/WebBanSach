namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            Order_Book = new HashSet<Order_Book>();
            Authors = new HashSet<Author>();
            Tags = new HashSet<Tag>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Alias { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImages { get; set; }

        [StringLength(50)]
        public string BookCover { get; set; }

        public int? NumberPages { get; set; }

        [StringLength(50)]
        public string Size { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PublicationDate { get; set; }

        public string Content { get; set; }

        public decimal? Price { get; set; }

        public decimal? PromotionPrice { get; set; }

        public int? ViewCount { get; set; }

        public int? Quanlity { get; set; }

        public bool? Status { get; set; }

        [StringLength(250)]
        public string MetaKeyword { get; set; }

        [StringLength(250)]
        public string MetaDesciption { get; set; }

        public bool? PromotionFlag { get; set; }

        public bool? HotFlag { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? TagID { get; set; }

        public int? CategoryID { get; set; }

        public int? PublisherID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdateBy { get; set; }

        public virtual BookCategory BookCategory { get; set; }

        public virtual Publisher Publisher { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Book> Order_Book { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Author> Authors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
