﻿using BookStore.Areas.Admin.Models.BusinessModel;
using Common;
using Model.DAO;
using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BookStore.Areas.Admin.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Admin/Author
        public ActionResult Index()
        {
            setViewbagforAuthor();
            return View();
        }

        public void setViewbagforAuthor()
        {
            AuthorDAO author = new AuthorDAO();
            ViewBag.listAuthor = author.getListAuthor();
        }
<<<<<<< HEAD
        //[HttpPost]
        //public JsonResult LoadData()
        //{
        //    IEnumerable<AuthorViewModel> model =
        //}
=======
        public JsonResult LoadData()
        {
            setViewbagforAuthor();
            IEnumerable<Author> model = new AuthorDAO().getListAuthor();
            model = model.OrderBy(x => x.Name);
            var TotalRow = model.Count();
            return Json(new
            {
                data = model,
                totalRow = TotalRow,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(Author author)
        {
            var status = false;
            if (author.ID > 0)
            {
                return Json(new
                {
                    status = new AuthorDAO().UpdateAuthor(author) // Them
                });
            }
            else
            {
                return Json(new
                {
                    status = new AuthorDAO().Insert(author) // Them
                });
            }
        }
>>>>>>> 149dc1acc6dc06e33bb7deca7049da37a951bfd9

        [HttpPost]
        public JsonResult LoadDetails(int id)
        {
            return Json(new
            {
                author = new AuthorDAO().GetDetails(id)
            });
        }

        public JsonResult Del(int id)
        {
            return Json(new
            {
                stt = new AuthorDAO().DelAuthor(id)
            });
        }
    }
}