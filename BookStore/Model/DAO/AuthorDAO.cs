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

        public Author GetDetails(int id)
        {
            var author=db.Authors.Find(id);
            return author;
        }

        public bool UpdateAuthor(Author a)
        {
            var model = db.Authors.Find(a.ID);
            model.Name = a.Name;
            model.Birthday = a.Birthday;
            model.Info = a.Info;
            db.SaveChanges();
            return true;
        }

        public bool DelAuthor(int id)
        {
            var model = db.Authors.Find(id);
            db.Authors.Remove(model);
            db.SaveChanges();
            return true;
        }
    }
}
