using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    public class CommonController : Controller
    {
        ImageHelper imgHelper = new ImageHelper();
        // GET: Common
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult deleteImage(string filename)
        {
            return Json(imgHelper.DeleteImageByfilename(filename), JsonRequestBehavior.AllowGet);
        }
    }
}