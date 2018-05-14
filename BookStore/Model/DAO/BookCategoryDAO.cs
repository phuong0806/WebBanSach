using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class BookCategoryDAO
    {
        BookStoreDbContext db = null;

        public BookCategoryDAO()
        {
            db = new BookStoreDbContext();
        }

        public List<BookCategory> getListCategory()
        {
            var result = db.BookCategories.Where(x => x.Status == true).ToList();
            return result;
        }

        public IEnumerable<CategoryViewModel> getAllCategory()
        {
            IQueryable<CategoryViewModel> result =
                (from category in db.BookCategories
                 join catalog in db.BookCatalogs on category.CatalogID equals catalog.ID
                 select new CategoryViewModel()
                 {
                     ID = category.ID,
                     Name = category.Name,
                     Alias = category.Alias,
                     Image = category.Image,
                     DisplayOrder = category.DisplayOrder,
                     Status = category.Status,
                     CatalogName = catalog.Name,
                     CatalogID = catalog.ID
                 });
            return result;
        }

        public CategoryViewModel getCategoryByID(int id)
        {
            var model = db.BookCategories.Where(x => x.ID == id).Select(x => new CategoryViewModel {
                Name = x.Name,
                CatalogID = x.CatalogID,
                Alias = x.Alias,
                Image = x.Image,
                ID = x.ID,
            }).FirstOrDefault();
            return model;
        }

        public bool insert(BookCategory category)
        {
            try
            {
                db.BookCategories.Add(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool update(BookCategory category)
        {
            try
            {
                var entity = db.BookCategories.Find(category.ID);
                entity.Name = category.Name;
                entity.Image = category.Image;
                entity.Alias = category.Alias;
                entity.CatalogID = category.CatalogID;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool delete(int id)
        {
            try
            {
                var Books = db.Books.Where(x => x.CategoryID == id).ToList();
                foreach (var item in Books)
                {
                    item.CategoryID = null;
                }
                var category = db.BookCategories.Find(id);
                db.BookCategories.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool changeStatus(int id)
        {
            try
            {
                var model = db.BookCategories.Find(id);
                model.Status = !model.Status;
                db.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }
        }
    }
}
