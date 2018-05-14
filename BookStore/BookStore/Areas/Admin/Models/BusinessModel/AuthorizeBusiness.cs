using Model.DAO;
using Model.EF;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Areas.Admin.Models.BusinessModel
{
    public class AuthorizeBusiness : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Username"] == null)
            {
                filterContext.Result = new RedirectResult("/Admin/Home/Login");
                return;
            }

            string username = HttpContext.Current.Session["Username"].ToString();

            //get action
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "Controller";

            BookStoreDbContext db = new BookStoreDbContext();

            var admin = db.Administrators.Where(x => x.Username == username && x.isAdmin == true).FirstOrDefault();
            if (admin != null)
                return;

            var listBusiness = new BusinessDAO().getListBusinessOfUser(username);


            if (!listBusiness.Select(x => x.ID).Contains(controllerName))
            {
                filterContext.Result = new RedirectResult("/Admin/Home/_404");
                return;
            }
        }
    }
}