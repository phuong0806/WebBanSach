using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class UserDAO
    {
        private BookStoreDbContext db = null;

        public UserDAO()
        {
            db = new BookStoreDbContext();
        }

        public bool checkIfDontHaveUserThenCreate()
        {
            if (db.Administrators.Count() == 0)
            {
                var newAdmin = new Administrator()
                {
                    Username = "admin",
                    Password = "21232f297a57a5a743894a0e4a801fc3",
                    Fullname = "Admin",
                    Image = "/Assets/admin/images/avatars/avatar2.png",
                    Email = "admin@gmail.com",
                    isAdmin = true,
                    Status = true,
                };
                db.Administrators.Add(newAdmin);

                var newUser = new Administrator()
                {
                    Username = "user",
                    Password = "ee11cbb19052e40b07aac0ca060c23ee",
                    Fullname = "user",
                    Image = "/Assets/admin/images/avatars/avatar2.png",
                    Email = "user@gmail.com",
                    isAdmin = false,
                    Status = true,
                };
                db.Administrators.Add(newUser);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public Administrator getUser(string username, string password)
        {
            var model = db.Administrators.SingleOrDefault(x => x.Username == username && x.Password == password && x.Status == true);
            return model;
        }
        
        public IEnumerable<Administrator> getListUser()
        {
            var model = db.Administrators.ToList();
            return model;
        }
    }
}
