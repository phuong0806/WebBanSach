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
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool checkUsername(string username)
        {
            return db.Administrators.Any(x => x.Username == username);
        }

        public bool add(Administrator ad)
        {
            try
            {
                db.Administrators.Add(ad);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Administrator getUser(string username, string password)
        {
            var model = db.Administrators.SingleOrDefault(x => x.Username == username && x.Password == password && x.Status == true);
            return model;
        }
        
        public IEnumerable<Administrator> getListUser(string username)
        {
            var model = db.Administrators.Where(x => x.Username != username).ToList();
            return model;
        }

        public bool deleteUser(string username)
        {
            try
            {
                var user = db.Administrators.Find(username);
                db.Administrators.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool changeStatus(string username)
        {
            try
            {
                var user = db.Administrators.Find(username);
                user.Status = !user.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
