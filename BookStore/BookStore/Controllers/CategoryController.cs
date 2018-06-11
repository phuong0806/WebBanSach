using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Model.EF;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        BookStoreDbContext db = new BookStoreDbContext();
        BookCategoryDAO bCTLDao = new BookCategoryDAO();
        // GET: Category
        public ActionResult Index(int? page)
        {
            setViewBagForCategory();
            string Alias = Request.Url.Segments[2];
            int pasize = 1;
            int panum = (page ?? 1);
            setViewBagForCategory();
            if (Alias == "new-book")
            {
                var model = new BookDAO().GetNewBook();
                var md = "Mới Tháng " + DateTime.Now.Month.ToString();
                ViewBag.CTLG = md;
                DateTime a = DateTime.Now;
                int month = a.Month;
                return View(db.Books.Where(x => x.Status == true && x.CreatedDate.Value.Month == month).OrderBy(x => x.Name).ToPagedList(panum, pasize));
            }
            else
            {
                var model = new BookDAO().GetBookByAliasCategory(Alias);
                var md = new BookCategoryDAO().Alias(Alias).SingleOrDefault();
                ViewBag.CTLG = md.Name;
                var ctlg = db.BookCategories.Where(x => x.Alias == Alias).FirstOrDefault();
                return View(db.Books.Where(x => x.CategoryID == ctlg.ID && x.Status == true).OrderBy(x => x.Name).ToPagedList(panum, pasize));
            }
        }

        //public ActionResult loadBookByAliasCategory(int? page)
        //{
        //    string Alias = Request.Url.Segments[2];
        //    int pasize = 1;
        //    int panum = (page ?? 1);
        //    setViewBagForCategory();
        //    //if (Alias == "new-book")
        //    //{
        //    //    var model = new BookDAO().GetNewBook();
        //    //    var md = "Mới Tháng " + DateTime.Now.Month.ToString();
        //    //    ViewBag.CTLG = md;
        //    //    ViewBag.ListBook = model;
        //    //}
        //    //else
        //    //{
        //    //    var model = new BookDAO().GetBookByAliasCategory(Alias);
        //    //    var md = new BookCategoryDAO().Alias(Alias).SingleOrDefault();
        //    //    ViewBag.CTLG = md.Name;
        //    //    ViewBag.ListBook = model;
        //    //}
        //    //return View("Index");
        //    if (Alias == "new-book")
        //    {
        //        var model = new BookDAO().GetNewBook();
        //        var md = "Mới Tháng " + DateTime.Now.Month.ToString();
        //        ViewBag.CTLG = md;
        //        DateTime a = DateTime.Now;
        //        int month = a.Month;
        //        return View(db.Books.Where(x => x.Status == true && x.CreatedDate.Value.Month == month).OrderBy(x => x.Name).ToPagedList(panum, pasize));
        //    }
        //    else
        //    {
        //        var model = new BookDAO().GetBookByAliasCategory(Alias);
        //        var md = new BookCategoryDAO().Alias(Alias).SingleOrDefault();
        //        ViewBag.CTLG = md.Name;
        //        var ctlg = db.BookCategories.Where(x => x.Alias == Alias).FirstOrDefault();
        //        return View(db.Books.Where(x => x.CategoryID == ctlg.ID && x.Status == true).OrderBy(x => x.Name).ToPagedList(panum, pasize));
        //    }

        //}

        public void setViewBagForCategory()
        {
            ViewBag.ListBookCTL = bCTLDao.GetCTL();
        }
    }
}