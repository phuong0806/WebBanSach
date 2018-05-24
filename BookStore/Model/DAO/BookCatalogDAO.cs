using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class BookCatalogDAO
    {
        BookStoreDbContext db = null;

        public BookCatalogDAO() {
            db = new BookStoreDbContext();
        }

        public List<BookCatalog> getListBookCatalog()
        {
            var result = db.BookCatalogs.Where(x => x.Status == true).ToList();
            return result;
        }

        public bool Insert(BookCatalog ca)
        {
            try
            {
                db.BookCatalogs.Add(ca);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public BookCatalog GetDetails(int id)
        {
            var catalog = db.BookCatalogs.Find(id);
            return catalog;
        }

        public bool UpdateAuthor(BookCatalog ca)
        {
            var model = db.BookCatalogs.Find(ca.ID);
            model.Name = ca.Name;
            model.Alias = ca.Alias;
            db.SaveChanges();
            return true;
        }

        public bool DelCatalog(int id)
        {
            var model = db.BookCatalogs.Find(id);
            db.BookCatalogs.Remove(model);
            db.SaveChanges();
            return true;
        }
    }
}
