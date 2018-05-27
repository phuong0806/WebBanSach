using Model.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Web.Mvc;


namespace Model.ViewModel
{
    public class BookViewModel
    {
        internal string AuthorsName;

        public int ID { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập tiêu đề")]
        public string Name { get; set; }

        [Required]
        public string Alias { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập giá tiền")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập số lượng")]
        public int? Quanlity { get; set; }

        public int? ViewCount { get; set; }

        public bool? Status { get; set; }

        public string Image { get; set; }

        public string MoreImages { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM}")]
        [Required(ErrorMessage = "Bạn phải chọn ngày xuất bản")]
        public DateTime? PublicationDate { get; set; }

        public string BookCover { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập nội dung")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập số trang")]
        public int? NumberPages { get; set; }

        public int[] SelectedValuesAuthor { get; set; }

        public int Catalog { get; set; }

        public int? Category { get; set; }

        public int? Publisher { get; set; }

        public string CatalogName { get; set; }

        public string CategoryName { get; set; }

        public string PublisherName { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập kích thước")]
        public string Size { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<Author> Authors { get; set; }

        public string AuthorName { get; set; }

        public string NumberProduct { get; set; }
    };
}