using Common;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    public class CommonController : Controller
    {
        private ImageHelper imgHelper = new ImageHelper();

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

        public void importFile(HttpPostedFileBase file)
        {
            var path = "";
            var fileName = Path.GetFileName(file.FileName);
            var dir = "/Assets/admin/files/upload/";
            if (file.ContentLength > 0)
            {
                path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~" + dir), fileName);
                if (!System.IO.File.Exists(path))
                {
                    file.SaveAs(path);
                }
            }
        }
    }
}