using Common;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Admin/Author
        ImageHelper imgHelper = new ImageHelper();
        public ActionResult Index()
        {
            setViewbagforAuthor();
            return View();
        }

        public void setViewbagforAuthor()
        {
            ImageHelper imgHelper = new ImageHelper();
            AuthorDAO author = new AuthorDAO();
            ViewBag.listImage = imgHelper.loadListImage();
            ViewBag.listAuthor = author.getListAuthor();
        }
        [HttpPost]
        public JsonResult LoadData()
        {
            IEnumerable<AuthorViewModel> model =
        }

    }
}