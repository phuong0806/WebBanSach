using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using PagedList;
using PagedList.Mvc;
using Model.DAO;

namespace BookStore.Controllers
{
    public class AllController : Controller
    {
        BookStoreDbContext db = new BookStoreDbContext();
        BookCategoryDAO bCTLDao = new BookCategoryDAO();
        // GET: All
        public ActionResult Index(int? page)
        {
            ViewBag.ListBookCTL = bCTLDao.GetCTL();
            int pasize = 1;
            int panum = (page ?? 1);
            return View(db.Books.OrderBy(x => x.Name).ToPagedList(panum, pasize));
            
        }
    }
}