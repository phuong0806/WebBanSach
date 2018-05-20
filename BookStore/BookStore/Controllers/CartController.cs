using Model.DAO;
using Model.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BookStore.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult emptyCart()
        {
            return View();
        }

        [HttpPost]
        public JsonResult loadData(string listCartString)
        {
            var status = false;
            var json = "";
            if (listCartString != "null" && listCartString != "[]")
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                var listBook = serializer.Deserialize<List<Book>>(listCartString);

                json = JsonConvert.SerializeObject(new BookDAO().getListBooksInCart(listBook), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

                status = true;
            }

            return Json(new {
                data = json,
                status = status
            });
        }
    }
}