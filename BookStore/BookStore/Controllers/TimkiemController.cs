using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;

namespace BookStore.Controllers
{
    public class TimkiemController : Controller
    {
        BookDAO book = new BookDAO();
        // GET: Timkiem
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            var key = Request["tukhoa"];
            var model = book.Search(key);
            ViewBag.KQ = model;
            ViewBag.key = key;
            return View("Index");
        }

        public JsonResult ListName(string term)
        {
            var model = book.ListName(term);
            return Json(new
            {
                data = model,
                status = true
            },JsonRequestBehavior.AllowGet);
        }
    }
}