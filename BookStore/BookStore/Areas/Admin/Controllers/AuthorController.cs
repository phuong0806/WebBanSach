using BookStore.Areas.Admin.Models.BusinessModel;
using Common;
using Model.DAO;
using Model.EF;
using Model.ViewModel;
using Newtonsoft.Json;
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
        //[HttpPost]
        //public JsonResult LoadData()
        //{
        //    IEnumerable<AuthorViewModel> model =
        //}
        //public JsonResult LoadData()
        //{
        //    setViewbagforAuthor();
        //    List<Author> model = new AuthorDAO().getListAuthor();
        //    model = model.OrderBy(x => x.Name).ToList();
        //    var TotalRow = model.Count();
        //    return Json(new
        //    {
        //        data = model,
        //        totalRow = TotalRow,
        //        status = true
        //    }, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult LoadData()
        {
            setViewbagforAuthor();
            List<Author> model = new AuthorDAO().getListAuthor();
            model = model.OrderBy(x => x.Name).ToList();
            var TotalRow = model.Count();
            var output = JsonConvert.SerializeObject(model, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            return Json(new
            {
                data = output,
                totalRow = TotalRow,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Save(Author author)
        {
            if (author.ID > 0)
            {
                return Json(new
                {
                    status = new AuthorDAO().UpdateAuthor(author) // Them
                });
            }
            else
            {
                return Json(new
                {
                    status = new AuthorDAO().Insert(author) // Them
                });
            }
        }

        [HttpPost]
        public JsonResult LoadDetails(int id)
        {
            return Json(new
            {
                author = new AuthorDAO().GetDetails(id)
            });
        }

        public JsonResult Del(int id)
        {
            return Json(new
            {
                stt = new AuthorDAO().DelAuthor(id)
            });
        }
    }
}