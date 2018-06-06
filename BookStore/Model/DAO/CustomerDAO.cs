using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class CustomerDAO
    {
        private BookStoreDbContext db = null;
        public CustomerDAO()
        {
            db = new BookStoreDbContext();
        }

        public bool Insert(Customer cus)
        {
            try
            {
                db.Customers.Add(cus);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateCus(Customer cus)
        {
            var model = db.Customers.Find(cus.Username);
            model.Pass = cus.Pass;
            model.Name = cus.Name;
            model.Email = cus.Email;
            model.Phone = cus.Phone;
            model.Address = cus.Address;
            db.SaveChanges();
            return true;
        }

        public int InsertFB(Customer cus)
        {
            var Cus = db.Customers.SingleOrDefault(x => x.Username == cus.Username);
            if (Cus == null)
            {
                db.Customers.Add(cus);
                db.SaveChanges();
                return cus.ID;
            }
            else
            {
                return Cus.ID;
            }
        }
        public bool CheckUser(string name)
        {
            return db.Customers.Count(x => x.Username == name) > 0;
        }

        public bool CheckEmail(string email)
        {
            return db.Customers.Count(x => x.Email == email) > 0;
        }

        public bool CheckEmailUp(string email,string us)
        {
            if (db.Customers.Count(x => x.Email == email && x.Username.Equals(us))>0)
                return true;
            else
                return false;
        }


        public Customer GetByUN(string UserName)
        {
            return db.Customers.SingleOrDefault(x => x.Username == UserName);
        }

        public bool Login(string UserName,string Pass)
        {
            object[] sqlParams =
            {
                new SqlParameter("@Username",UserName),
                new SqlParameter("@Pass",Pass),
            };
            var result = db.Database.SqlQuery<bool>("Sp_Account_Login @Username,@Pass",sqlParams).SingleOrDefault();
            return result;
        }
    }
}
