﻿using BookStore.Areas.Admin.Models.BusinessModel;
using Common;
using Model.DAO;
using Model.EF;
using Model.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BookStore.Areas.Admin.Controllers
{
    public class CatalogController : Controller
    {
        // GET: Admin/Catalog
        public ActionResult Index()
        {
            setViewbagforCatalog();
            return View();
        }

        public void setViewbagforCatalog()
        {
            BookCatalogDAO catalog = new BookCatalogDAO();
            ViewBag.listcatalog = catalog.getListBookCatalog();
        }
        public JsonResult LoadData()
        {
            setViewbagforCatalog();
            IEnumerable<BookCatalog> model = new BookCatalogDAO().getListBookCatalog();
            model = model.OrderBy(x => x.DisplayOrder);
            var TotalRow = model.Count();
            var output = JsonConvert.SerializeObject(model, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return Json(new
            {
                data = output,
                totalRow = TotalRow,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(BookCatalog catalog)
        {
            var status = false;
            if (catalog.ID > 0)
            {
                return Json(new
                {
                    status = new BookCatalogDAO().UpdateAuthor(catalog) // Them
                });
            }
            else
            {
                return Json(new
                {
                    status = new BookCatalogDAO().Insert(catalog)
                });
            }
        }

        [HttpPost]
        public JsonResult LoadDetails(int id)
        {
            return Json(new
            {
                ca = new BookCatalogDAO().GetDetails(id)
            });
        }

        public JsonResult Del(int id)
        {
            return Json(new
            {
                stt = new BookCatalogDAO().DelCatalog(id)
            });
        }
    }
}