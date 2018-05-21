using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class ProductCartModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int price { get; set; }
        public int count { get; set; }
    }
}