using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BusinessModel
{
    public class BookStoreDbInitializer : DropCreateDatabaseAlways<BookStoreDbContext>
    {
        protected override void Seed(BookStoreDbContext db)
        {
            var admin = new Administrator()
            {
                Username = "admin",
                Password = "21232f297a57a5a743894a0e4a801fc3",
                Fullname = "Admin",
                Image = "/Assets/admin/images/avatars/avatar2.png",
                Email = "admin@gmail.com",
                isAdmin = true,
                Status = true,
            };
            db.Administrators.Add(admin);

            var user = new Administrator()
            {
                Username = "user",
                Password = "ee11cbb19052e40b07aac0ca060c23ee",
                Fullname = "user",
                Image = "/Assets/admin/images/avatars/avatar2.png",
                Email = "user@gmail.com",
                isAdmin = false,
                Status = true,
            };
            db.Administrators.Add(user);
            db.SaveChanges();
        }
    }
}
