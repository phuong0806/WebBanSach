using BookStore.Areas.Admin.Models.BusinessModel;
using Common;
using Model.DAO;
using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BookStore.Areas.Admin.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Admin/Author
        public ActionResult Index()
        {
            setViewbagforAuthor();
            return View();
        }

        public void setViewbagforAuthor()
        {
            AuthorDAO author = new AuthorDAO();
            ViewBag.listAuthor = author.getListAuthor();
        }
        public JsonResult LoadData()
        {
            IEnumerable<Author> model = new AuthorDAO().getListAuthor();
            model = model.OrderBy(x => x.Name);
            var TotalRow = model.Count();
            return Json(new
            {
                data = model,
                totalRow = TotalRow,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Add(Author author)
        {
            return Json(new {
                status = new AuthorDAO().Insert(author)
            });
        }

    }
}