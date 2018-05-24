using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class DetailController : Controller
    {
        BookDAO bDao = new BookDAO();
        // GET: Detail
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDetail(string Alias)
        {
            var model = new BookDAO().Detail(Alias).SingleOrDefault();
            //var cungloai = bDao.Same(model.CategoryID);
            ViewBag.Details = model;
            //ViewBag.lienquan = cungloai;
            return View("Index");
        }
    }
}