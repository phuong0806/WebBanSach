using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class OrderDAO
    {
        private BookStoreDbContext db = null;

        public OrderDAO()
        {
            db = new BookStoreDbContext();
        }

        public int insert(List<ProductCartViewModel> listProduct, int totalCost)
        {
            var Order = new Order();

            var max = 0;

            if (db.Orders.Count() > 0)
            {
                max = db.Orders.Max(x => x.ID);
            }
            else
            {
                Order.ID = 111111111;
            }

            if (max > 0)
            {
                Order.ID = max + 1;
            }

            Order.CreatedDate = DateTime.Now;
            Order.CreatedBy = null;
            Order.TotalCost = totalCost;
            Order.Status = 0;

            foreach (var item in listProduct)
            {
                var Order_Book = new Order_Book();
                Order_Book.OrderID = Order.ID;
                Order_Book.BookID = item.id;
                Order_Book.Number = item.count;
                db.Order_Book.Add(Order_Book);
            }
            db.Orders.Add(Order);
            db.SaveChanges();
            return Order.ID;
        }
    }
}
