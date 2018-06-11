using BookStore.Areas.Admin.Models.BusinessModel;
using Model.DAO;
using Model.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BookStore.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class BusinessController : Controller
    {
        private BookStoreDbContext db = new BookStoreDbContext();

        // GET: Admin/Business
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpdateBusiness()
        {
            MyReflectionController rc = new MyReflectionController();
            List<Type> listControllerType = rc.GetControllers("BookStore.Areas.Admin");
            List<string> listControllerOld = new BusinessDAO().getListBusiness().Select(x => x.ID).ToList();
            foreach (var item in listControllerType)
            {
                if (!listControllerOld.Contains(item.Name) && item.Name != "CommonController")
                {
                    var url = "/Admin/" + item.Name.Replace("Controller", "") + "/Index";
                    Business b = new Business() { ID = item.Name, Name = "Empty", Url = url };
                    db.Businesses.Add(b);
                }
            }
            db.SaveChanges();
            ViewBag.Success = true;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public string loadData()
        {
            var model = new BusinessDAO().getListBusiness();
            ViewBag.CountBusiness = model;
            var json = JsonConvert.SerializeObject(model, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return json;
        }

        [HttpGet]
        public string getDetail(string id)
        {
            var model = new BusinessDAO().getBusinessByID(id);
            var json = JsonConvert.SerializeObject(model, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return json;
        }

        [HttpPost]
        public JsonResult updateDescription(string businessStr)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            Business business = serializer.Deserialize<Business>(businessStr);

            bool status = false;

            status = new BusinessDAO().updateName(business);

            return Json(new
            {
                status = true
            });
        }
    }
}