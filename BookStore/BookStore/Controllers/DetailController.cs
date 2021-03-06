﻿using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class DetailController : Controller
    {
        BookDAO bDao = new BookDAO();
        // GET: Detail
        public ActionResult Index()
        {
            return View("Error");
        }

        public ActionResult GetDetail(string Alias)
        {
            try
            {
                var model = new BookDAO().Detail(Alias).SingleOrDefault();
                var cungloai = bDao.Same(model.CategoryID);
                var hot = bDao.Hot();
                ViewBag.Details = model;
                ViewBag.lienquan = cungloai;
                ViewBag.HB = hot;
                return View("Index");
            }
            catch (Exception)
            {
                return View("Error");
                throw;
            }
            
        }
    }
}