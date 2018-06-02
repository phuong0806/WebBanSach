using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Common
{
    [Serializable]
    public class UserLogin
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }
}