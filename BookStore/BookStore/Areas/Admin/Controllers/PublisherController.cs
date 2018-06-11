using BookStore.Areas.Admin.Models.BusinessModel;
using Model.DAO;
using Model.EF;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class PublisherController : Controller
    {
        // GET: Admin/Publisher
        public ActionResult Index()
        {
            setViewbagforPub();
            return View();
        }

        public void setViewbagforPub()
        {
            PublisherDAO pub = new PublisherDAO();
            ViewBag.listPub = pub.getListPublisher();
        }

        public JsonResult LoadData()
        {
            setViewbagforPub();
            IEnumerable<Publisher> model = new PublisherDAO().getListPublisher();
            model = model.OrderBy(x => x.Name);
            var TotalRow = model.Count();
            var output = JsonConvert.SerializeObject(model, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            var jsonresult = Json(new
            {
                data = output,
                totalRow = TotalRow,
                status = true
            }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }

        [HttpPost]
        public JsonResult Save(Publisher pub)
        {
            if (pub.ID > 0)
            {
                return Json(new
                {
                    status = new PublisherDAO().UpdatePublisher(pub) // Them
                });
            }
            else
            {
                return Json(new
                {
                    status = new PublisherDAO().Insert(pub)
                });
            }
        }

        [HttpPost]
        public JsonResult LoadDetails(int id)
        {
            return Json(new
            {
                pub = new PublisherDAO().GetDetails(id)
            });
        }

        public JsonResult Del(int id)
        {
            return Json(new
            {
                stt = new PublisherDAO().DelPublisher(id)
            });
        }
    }
}