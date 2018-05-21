using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class ProductCartViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int price { get; set; }
        public int count { get; set; }
    }
}
