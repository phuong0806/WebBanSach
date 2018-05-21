using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class AuthorDAO
    {
        BookStoreDbContext db = null;

        public AuthorDAO()
        {
            db = new BookStoreDbContext();
        }

        public List<Author> getListAuthor()
        {
            var result = db.Authors.ToList();
            return result;
        }

        public bool Insert(Author author)
        {
            try
            {
                db.Authors.Add(author);
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
