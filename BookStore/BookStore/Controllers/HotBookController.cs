using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class HotBookController : Controller
    {
        BookCategoryDAO bCTLDao = new BookCategoryDAO();
        // GET: HotBook
        public ActionResult Index()
        {
            var model = new BookDAO().Hot();
            ViewBag.ListBookCTL = bCTLDao.GetCTL();
            ViewBag.Hot = model;
            return View();
        }
    }
}