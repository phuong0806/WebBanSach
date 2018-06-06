using BookStore.Models;
using Facebook;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;
using Common;
using BookStore.Common;
using BotDetect.Web.Mvc;

namespace BookStore.Controllers
{
    public class UserController : Controller
    {
        CustomerDAO CusDao = new CustomerDAO();

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FaceBookCallBack");
                return uriBuilder.Uri;
            }
        }
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Change()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");

        }
        [HttpPost]
        public ActionResult Change(ChangeModel model)
        {
            if (ModelState.IsValid)
            {
                var result = CusDao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == true)
                {
                    if (CusDao.CheckEmailUp(model.Email,model.UserName)==false)
                    {
                        ModelState.AddModelError("", "Đã có người sử dụng email này");
                    }
                    else
                    {
                        var cus = new Customer();
                        cus.ID = model.Id;
                        cus.Username = model.UserName;
                        cus.Pass = Encryptor.MD5Hash(model.NewPassword);
                        cus.Name = model.Name;
                        cus.Phone = model.Phone;
                        cus.Email = model.Email;
                        cus.Address = model.Address;
                        var result1 = CusDao.UpdateCus(cus);
                        if (result1 == true)
                        {
                            ViewBag.Suc = "Thay đổi thành công";
                            var user = CusDao.GetByUN(model.UserName);
                            var userSession = "";
                            var phone = "";
                            var mail = "";
                            var name = "";
                            var address = "";
                            userSession = user.Username;
                            phone = user.Phone;
                            mail = user.Email;
                            name = user.Name;
                            address = user.Address;
                            Session.Add(CommonConstants.USER_SESSION, userSession);
                            Session.Add(CommonConstants.Phone, phone);
                            Session.Add(CommonConstants.Mail, mail);
                            Session.Add(CommonConstants.Name, name);
                            Session.Add(CommonConstants.Address, address);
                        }
                        else
                        {
                            ModelState.AddModelError("", "Thay đổi thất bại");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Sai mật khẩu");
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                var result = CusDao.Login(model.UserName,Encryptor.MD5Hash(model.Password));
                if(result==true)
                {
                    var user = CusDao.GetByUN(model.UserName);
                    var userSession="";
                    var phone = "";
                    var mail = "";
                    var name = "";
                    var address = "";
                    userSession = user.Username;
                    phone = user.Phone;
                    mail = user.Email;
                    name = user.Name;
                    address = user.Address;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    Session.Add(CommonConstants.Phone, phone);
                    Session.Add(CommonConstants.Mail, mail);
                    Session.Add(CommonConstants.Name, name);
                    Session.Add(CommonConstants.Address, address);
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập thất bại");
                }
            }
            return View(model);
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCapcha", "Sai mã xác nhận!")]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                
                if(CusDao.CheckUser(model.UserName))
                {
                    ModelState.AddModelError("", "Đã có người sử dụng tên đăng nhập này");
                }
                else if(CusDao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Đã có người sử dụng email này");
                }
                else
                {
                    var cus = new Customer();
                    cus.Username = model.UserName;
                    cus.Pass = Encryptor.MD5Hash(model.Password);
                    cus.Name = model.Name;
                    cus.Phone = model.Phone;
                    cus.Email = model.Email;
                    cus.Address = model.Address;
                    var result = CusDao.Insert(cus);
                    if(result==true)
                    {
                        ViewBag.Suc = "Đăng ký thành công";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký thất bại");
                    }
                }
            }
            return View(model);
        }


        public ActionResult LoginFB()
        {
            var fb = new FacebookClient();
            var logUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                respone_type = "code",
                scope = "email",
            });
            return Redirect(logUrl.AbsoluteUri);
        }

        public ActionResult FaceBookCallBack(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });
            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string username = me.username;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;

                var Customer = new Customer();
                Customer.Email = email;
                Customer.Username = firstname + middlename + lastname;
                Customer.Name = firstname + " " + middlename + " " + lastname;
                var res = CusDao.InsertFB(Customer);
                if (res>0)
                {
                    var userSession = "";
                    userSession = Customer.Name;

                    Session.Add(CommonConstants.USER_SESSION, userSession);
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập thất bại");
                }
            }
            return Redirect("/");

        }
    }
}