using BookStore.Areas.Admin.Models.BusinessModel;
using Model.DAO;
using Model.EF;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
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

            var jsonresult = Json(new
            {
                data = output,
                totalRow = TotalRow,
                status = true
            }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }

        [HttpPost]
        [AllowAnonymous, ValidateInput(false)]
        public JsonResult Save(Author author)
        {
            if (author.ID > 0)
            {
                return Json(new
                {
                    status = new AuthorDAO().UpdateAuthor(author) // update
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
            var output = JsonConvert.SerializeObject(new AuthorDAO().GetDetails(id), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return Json(new
            {
                author = output
            }, JsonRequestBehavior.AllowGet);
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