using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        BookCategoryDAO bCTLDao = new BookCategoryDAO();
        // GET: Category
        public ActionResult Index()
        {
            setViewBagForCategory();
            return View();
        }

        public ActionResult loadBookByAliasCategory(string Alias)
        {
            setViewBagForCategory();
            if (Alias == "new-book")
            {
                var model = new BookDAO().GetNewBook();
                var md = "Mới Tháng " + DateTime.Now.Month.ToString();
                ViewBag.CTLG = md;
                ViewBag.ListBook = model;
            }
            else
            {
                var model = new BookDAO().GetBookByAliasCategory(Alias);
                var md = new BookCategoryDAO().Alias(Alias).SingleOrDefault();
                ViewBag.CTLG = md.Name;
                ViewBag.ListBook = model;
            }

            return View("Index");
        }

        public void setViewBagForCategory()
        {
            ViewBag.ListBookCTL = bCTLDao.GetCTL();
        }
    }
}