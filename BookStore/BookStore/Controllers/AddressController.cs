﻿using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class AddressController : Controller
    {
        // GET: Address
        public ActionResult Index()
        {
            ViewBag.ListCatalogi = new BookCategoryDAO().GetCTL();
            return View();
        }
    }
}