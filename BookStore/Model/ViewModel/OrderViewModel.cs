using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class OrderViewModel
    {
        //Order
        public int ID { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string ConfirmBy { get; set; }
        public int? TotalCost { get; set; }
        public bool? isFinish { get; set; }
        public bool? isConfirm { get; set; }
        public string TotalCostString { get; set; }
        //OrderDetail
        public string CustomerName {get;set;}
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Precinct { get; set; }
        public string Address { get; set; }
    }
}
