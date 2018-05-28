using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class ProductCartViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int? Number { get; set; }
        public string Image { get; set; }
        public string Alias { get; set; }
        public string TotalCost { get; set; }
        public string PriceString { get; set; }
    }
}
