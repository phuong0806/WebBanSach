using BookStore.Areas.Admin.Models.BusinessModel;
using Common;
using Model.DAO;
using Model.EF;
using Model.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BookStore.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class CategoryController : Controller
    {
        private ImageHelper imgHelper = new ImageHelper();

        // GET: Admin/Category
        public ActionResult Index()
        {
            setViewBagForCategory();
            return View();
        }

        [HttpGet]
        public JsonResult loadData()
        {
            IEnumerable<CategoryViewModel> model = new BookCategoryDAO().getAllCategory();

            model = model.OrderBy(x => x.CatalogID);

            var totalRow = model.Count();

            return Json(new
            {
                data = model,
                totalRow = totalRow,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public void setViewBagForCategory()
        {
            ImageHelper imgHelper = new ImageHelper();
            BookCatalogDAO catalogDAO = new BookCatalogDAO();
            ViewBag.listImage = imgHelper.loadListImage();
            ViewBag.listCatalog = catalogDAO.getListBookCatalog();
        }

        [HttpPost]
        public JsonResult saveData(string categoryStr, HttpPostedFileBase file)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            BookCategory category = serializer.Deserialize<BookCategory>(categoryStr);

            bool status = false;

            if (file != null)
            {
                category.Image = imgHelper.saveImage(file);
            }

            if (category.ID == 0)
            {
                status = new BookCategoryDAO().insert(category);
            }
            else
            {
                status = new BookCategoryDAO().update(category);
            }

            setViewBagForCategory();

            return Json(new
            {
                status = status
            });
        }

        [HttpGet]
        public JsonResult getDetail(int id)
        {
            BookCategoryDAO b = new BookCategoryDAO();
            var model = b.getCategoryByID(id);
            return Json(new
            {
                data = model,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            var result = new BookCategoryDAO().delete(id);
            return Json(new
            {
                status = result
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult changeStatus(int id)
        {
            return Json(new
            {
                status = new BookCategoryDAO().changeStatus(id)
            });
        }
    }
}