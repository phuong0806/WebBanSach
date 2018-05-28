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
    public class OrderDAO
    {
        private BookStoreDbContext db = null;

        public OrderDAO()
        {
            db = new BookStoreDbContext();
        }

        public IEnumerable<Order> getList(string searchText, string status, string fromDate, string toDate)
        {
            var model = new List<Order>();
            if (status == "Đơn hàng mới")
            {
                model = db.Orders.Where(x => x.ID.ToString().Contains(searchText) && x.isConfirm == false).OrderByDescending(x => x.CreatedDate).ToList();
            }
            else if(status == "Đang xử lý")
            {
                model = db.Orders.Where(x => x.ID.ToString().Contains(searchText) && x.Status == 0 && x.isFinish == false && x.isConfirm == true).OrderByDescending(x => x.CreatedDate).ToList();
            }
            else if (status == "Đang vận chuyển")
            {
                model = db.Orders.Where(x => x.ID.ToString().Contains(searchText) && x.Status == 1 && x.isFinish == false && x.isConfirm == true).OrderByDescending(x => x.CreatedDate).ToList();
            }
            else if (status == "Hoàn tất")
            {
                model = db.Orders.Where(x => x.ID.ToString().Contains(searchText) && x.isFinish == true).OrderByDescending(x => x.CreatedDate).ToList();
            }
            else
            {
                model = db.Orders.Where(x => x.ID.ToString().Contains(searchText)).OrderByDescending(x => x.CreatedDate).ToList();
            }

            if (fromDate != "" && toDate != "")
            {
                var from = Convert.ToDateTime(fromDate);
                var to = Convert.ToDateTime(toDate);
                model = model.Where(x => x.CreatedDate.Value.Date <= to.Date && x.CreatedDate.Value.Date >= from.Date).ToList();
            }

            return model;
        }

        public bool changeFinish(int id)
        {
            try
            {
                var entity = db.Orders.Find(id);

                entity.isFinish = !entity.isFinish;

                if (entity.isConfirm == false)
                    return false;

                if (entity.isFinish == true)
                {
                    entity.FinishDate = DateTime.Now;
                }
                else
                {
                    entity.FinishDate = null;
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool changeStatus(int id, int idStatus)
        {
            try
            {
                var entity = db.Orders.Find(id);
                entity.Status = idStatus;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
            Order.ConfirmBy = null;
            Order.TotalCost = totalCost;
            Order.Status = null;
            Order.isFinish = false;
            Order.isConfirm = false;

            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            
            foreach (var item in listProduct)
            {
                var Order_Book = new Order_Book();
                Order_Book.OrderID = Order.ID;
                Order_Book.BookID = item.ID;
                Order_Book.Number = item.Number;
                Order_Book.TotalCost = double.Parse((item.Number*item.Price).ToString()).ToString("#,###", cul.NumberFormat);
                db.Order_Book.Add(Order_Book);
            }
            db.Orders.Add(Order);
            db.SaveChanges();
            return Order.ID;
        }

        public bool confirmOrder(int id, string username)
        {
            try
            {
                var entity = db.Orders.Find(id);
                entity.isConfirm = !entity.isConfirm;
                entity.ConfirmBy = username;
                if (entity.isConfirm == true)
                {
                    entity.Status = 0;
                    entity.ConfirmDate = DateTime.Now;
                }
                else
                {
                    entity.Status = null;
                    entity.ConfirmDate = null;
                    entity.isFinish = false;
                    entity.FinishDate = null;
                }
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
