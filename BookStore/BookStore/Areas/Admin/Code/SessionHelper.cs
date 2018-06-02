using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Areas.Admin.Code
{
    public class SessionHelper
    {
        public static void SetSession(UserSession ses)
        {
            HttpContext.Current.Session["LoginSession"] = ses;
        }
        public static UserSession GetSession()
        {
            var ses = HttpContext.Current.Session["LoginSession"];
            if (ses == null)
                return null;
            else
                return ses as UserSession;
        }
    }
}