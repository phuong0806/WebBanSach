using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Areas.Admin.Code
{
    [Serializable]
    public class UserSession
    {
        public string Username { get; set; }
    }
}