using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class CategoryViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Image { get; set; }

        public bool? Status { get; set; }

        public string MetaKeyword { get; set; }

        public string MetaDescription { get; set; }

        public int? DisplayOrder { get; set; }

        public int? CatalogID { get; set; }

        public bool? HomeFlag { get; set; }

        public string CatalogName { get; set; }
    }
}
