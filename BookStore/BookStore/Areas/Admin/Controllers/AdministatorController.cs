using Model.DAO;
using Model.EF;
using Model.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    public class AdministatorController : Controller
    {
        // GET: Admin/Administator
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string loadData ()
        {
            var model = new UserDAO().getListUser();
            ViewBag.CountBusiness = model;
            var json = JsonConvert.SerializeObject(model, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return json;
        }

        [HttpGet]
        public JsonResult getBusiness(string username)
        {
            BusinessDAO businessDAO = new BusinessDAO();
            var listBusiness = businessDAO.getListBusiness();

            var listOfUser = businessDAO.getListBusinessOfUser(username);

            var list = new List<BusinessViewModel>();

            foreach (var item in listBusiness)
            {
                var b = new BusinessViewModel();
                if (listOfUser.Contains(item))
                {
                    b.ID = item.ID;
                    b.Name = item.Name;
                    b.Url = item.Url;
                    b.Status = true;
                }
                else
	            {
                    b.ID = item.ID;
                    b.Name = item.Name;
                    b.Url = item.Url;
                    b.Status = false;
                }
                list.Add(b);
            }
            return Json(new
            {
                username = username,
                data = list.OrderByDescending(x => x.Status)
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult updateBusiness(string username, string business)
        {
            return Json(new {
                status = new BusinessDAO().updateStatus(username, business)
            });
        }
    }
}