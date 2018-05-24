using Model.DAO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BookStore.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {

        // GET: Admin/Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderDetail(int id)
        {
            var dao = new OrderDetailDAO();
            var order = dao.getOrderDetailByID(id);

            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN"); 
            order.TotalCostString = double.Parse(order.TotalCost.ToString()).ToString("#,###", cul.NumberFormat);
            ViewBag.Order = order;
            ViewBag.ListBook = dao.getListBookOfOder(id);
            return View();
        }

        public JsonResult ChangeStatus(int id, int idStatus)
        {
            return Json(new
            {
                status = new OrderDAO().changeStatus(id,idStatus),
                idStatus = idStatus
            });
        }

        public JsonResult changeFinish(int id)
        {
            return Json(new
            {
                status = new OrderDAO().changeFinish(id),
            });
        }

        public JsonResult ConfirmOrder(int id)
        {
            return Json(new
            {
                status = new OrderDAO().confirmOrder(id, Session["Username"].ToString())
            });
        }

        public string loadData(string searchText, string idStatus, string fromDate, string toDate)
        {
            var model = new OrderDAO().getList(searchText, idStatus,fromDate, toDate);
            var json = JsonConvert.SerializeObject(model, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return json;
        }
    }
}