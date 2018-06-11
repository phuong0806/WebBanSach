using BookStore.Areas.Admin.Models.BusinessModel;
using Common;
using Model.DAO;
using Model.EF;
using Model.ViewModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BookStore.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class AdministatorController : Controller
    {
        // GET: Admin/Administator
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string loadData()
        {
            var userCurrent = Session["Username"].ToString();
            var model = new UserDAO().getListUser(userCurrent);
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
            return Json(new
            {
                status = new BusinessDAO().updateStatus(username, business)
            });
        }

        [HttpPost]
        public JsonResult add(string userStr)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            Administrator user = serializer.Deserialize<Administrator>(userStr);

            var password = RandomString.CreateRandomPassword(12);
            user.Password = Encryptor.MD5Hash(password);
            user.Status = true;

            var dao = new UserDAO();

            if (dao.checkUsername(user.Username))
            {
                return Json(new
                {
                    status = dao.add(user)
                });
            }
            else
            {
                return Json(new
                {
                    status = dao.add(user)
                });
            }
        }

        [HttpPost]
        public JsonResult delete(string username)
        {
            return Json(new
            {
                status = new UserDAO().deleteUser(username)
            });
        }

        [HttpPost]
        public JsonResult changeStatus(string username)
        {
            return Json(new
            {
                status = new UserDAO().changeStatus(username)
            });
        }
    }
}