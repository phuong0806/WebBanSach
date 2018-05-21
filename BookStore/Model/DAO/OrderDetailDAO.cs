using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class OrderDetailDAO
    {
        private BookStoreDbContext db = null;

        public OrderDetailDAO()
        {
            db = new BookStoreDbContext();
        }

        public bool insert(OrderDetail orderDetail, int id)
        {
            try
            {
                var entity = db.Orders.Find(id);
                orderDetail.OrderID = id;
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
