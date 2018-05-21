using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class PrecinctModel
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public int DistrictID { set; get; }
    }
}