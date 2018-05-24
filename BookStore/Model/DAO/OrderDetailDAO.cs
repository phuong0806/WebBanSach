using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public OrderViewModel getOrderDetailByID(int id)
        {
            var entity = (from order in db.Orders
                          join detail in db.OrderDetails on order.ID equals detail.OrderID
                          where order.ID == id
                          select new OrderViewModel {
                            ID = order.ID,
                            Status = order.Status,
                            CreatedDate = order.CreatedDate,
                            ConfirmDate = order.ConfirmDate,
                            FinishDate = order.FinishDate,
                            ConfirmBy = order.ConfirmBy,
                            TotalCost = order.TotalCost,
                            TotalCostString = order.TotalCost.ToString(),
                            isFinish = order.isFinish,  
                            isConfirm = order.isConfirm,
                            CustomerName = detail.CustomerName,
                            Phone = detail.Phone,
                            Email = detail.Email,
                            Province = detail.Province,
                            District = detail.District,
                            Precinct = detail.Precinct,
                            Address = detail.Address    
                         }).FirstOrDefault();
            return entity;
        }

        public IEnumerable<ProductCartViewModel> getListBookOfOder(int id)
        {
            var model = (from order in db.Order_Book
                         join book in db.Books on order.BookID equals book.ID
                         where order.OrderID == id
                         select new ProductCartViewModel {
                             Number = order.Number,
                             ID = order.OrderID,
                             Name = book.Name,
                             Price = book.Price,
                             Image = book.Image,
                             Alias = book.Alias,
                             TotalCost = order.TotalCost
                         }).ToList();
            return model;
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
