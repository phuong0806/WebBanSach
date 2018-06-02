using Common;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            UserDAO userDAO = new UserDAO();

            if (userDAO.checkIfDontHaveUserThenCreate() == true)
            {
                ViewBag.Error = "Đã khởi tạo Admin";
                return View();
            }

            string passwordMD5 = Encryptor.MD5Hash(password);

            var user = userDAO.getUser(username, passwordMD5);

            if (user != null)
            {
                if (user.Status == true)
                {
                    Session["Username"] = user.Username;
                    Session["Fullname"] = user.Fullname;
                    Session["Image"] = user.Image;
                    Session["Menu"] = new BusinessDAO().getListBusinessOfUser(username);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Tài khoản đã khóa";
                    return View();
                }
            }
            ViewBag.Error = "Username hoặc Password không xác định";

            return View();
        }

        public ActionResult Logout()
        {
            Session["Username"] = null;
            Session["Fullname"] = null;
            Session["Image"] = null;
            return View("Login");
        }

        public ActionResult _404()
        { 
            return View();
        }
    }
}