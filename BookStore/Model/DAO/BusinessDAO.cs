using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class BusinessDAO
    {
        private BookStoreDbContext db = null;

        public BusinessDAO()
        {
            db = new BookStoreDbContext();
        }

        public IEnumerable<Business> getListBusiness()
        {
            var model = db.Businesses.ToList();
            return model;
        }

        public Business getBusinessByID(string id)
        {
            var model = db.Businesses.Where(x => x.ID == id).SingleOrDefault();
            return model;
        }

        public IEnumerable<Business> getListBusinessOfUser(string username)
        {
            var model = (from b in db.Businesses
                         where b.Administrators.Any(x => x.Username == username)
                         select b).ToList();
            return model;
        }

        public bool updateName(Business b)
        {
            try
            {
                var entity = db.Businesses.Find(b.ID);

                entity.Name = b.Name;

                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool updateStatus(string username, string business)
        {
            try
            {
                var itemBusiness = db.Businesses.Find(business);
                var user = db.Administrators.Find(username);
                var BusinessOfUser = (from b in db.Businesses
                                      where b.Administrators.Any(x => x.Username == username)
                                      select b).ToList();
                if (BusinessOfUser.Contains(itemBusiness))
                {
                    user.Businesses.Remove(itemBusiness);
                }
                else
                {
                    user.Businesses.Add(itemBusiness);
                }
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
