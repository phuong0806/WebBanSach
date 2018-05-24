using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class PublisherDAO
    {
        BookStoreDbContext db = null;

        public PublisherDAO()
        {
            db = new BookStoreDbContext();
        }

        public List<Publisher> getListPublisher()
        {
            var result = db.Publishers.Where(x => x.Status == true).ToList();
            return result;
        }

        public bool Insert(Publisher pub)
        {
            try
            {
                db.Publishers.Add(pub);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Publisher GetDetails(int id)
        {
            var pub = db.Publishers.Find(id);
            return pub;
        }

        public bool UpdatePublisher(Publisher pub)
        {
            var model = db.Publishers.Find(pub.ID);
            model.Name = pub.Name;
            model.Alias = pub.Alias;
            model.PhoneNumber = pub.PhoneNumber;
            model.Address = pub.Address;
            db.SaveChanges();
            return true;
        }

        public bool DelPublisher(int id)
        {
            var model = db.Publishers.Find(id);
            db.Publishers.Remove(model);
            db.SaveChanges();
            return true;
        }
    }
}
