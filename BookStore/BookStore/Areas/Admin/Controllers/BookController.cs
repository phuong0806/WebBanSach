﻿using Model.DAO;
using Model.EF;
using Model.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Common;
using BookStore.Areas.Admin.Models.BusinessModel;

namespace BookStore.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class BookController : Controller
    {
        ImageHelper imgHelper = new ImageHelper();

        private BookStoreDbContext db = new BookStoreDbContext();

        // GET: Admin/Sach
        public ActionResult Index()
        {
            var model = new BookDAO().getListBook();
            return View(model);
        }

        [HttpGet]
        public JsonResult loadData(string searchText, string statusSelect, int page, int pageSize = 3)
        {
            BookDAO bookDAO = new BookDAO();

            IEnumerable<BookViewModel> model;

            if (!string.IsNullOrEmpty(searchText))
            {
                model = bookDAO.getListBookBySearchText(searchText);
            }
            else
            {
                model = bookDAO.getListBook();
            }

            if (!string.IsNullOrEmpty(statusSelect))
            {
                var status = bool.Parse(statusSelect);
                model = model.Where(x => x.Status == status);
            }

            var totalRow = model.Count();

            //model = model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
            return Json(new {
                data = model,
                totalRow = totalRow,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult AddBook(bool checkInsert = false)
        {
            ViewBag.checkInsert = checkInsert;
            setViewBagForBook();
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddBook(BookViewModel book, int[] Authors, HttpPostedFileBase file)
        {
            ModelState.Remove("Authors");
            if (ModelState.IsValid)
            {
                var BookDAO = new BookDAO();
                var newBook = new Book();
                if (new BookDAO().checkAliasExist(book.Alias))
                {
                    ModelState.AddModelError("Name", "Tiêu đề đã tồn tại");
                    setViewBagForBook();
                    return View("AddBook");
                }
                if (Authors == null)
                {
                    ModelState.AddModelError("Authors", "Bạn phải chọn tác giả");
                    setViewBagForBook();
                    return View("AddBook");
                }
                newBook.Name = book.Name;
                newBook.Alias = book.Alias;
                newBook.BookCover = book.BookCover;
                newBook.NumberPages = book.NumberPages;
                newBook.Size = book.Size;
                newBook.Content = book.Content;
                newBook.Price = book.Price;
                newBook.Quanlity = book.Quanlity;
                newBook.PublicationDate = book.PublicationDate;
                newBook.CategoryID = book.Category;
                newBook.PublisherID = book.Publisher;
                newBook.Image = book.Image;
                if (file != null)
                {
                    newBook.Image = imgHelper.saveImage(file);
                }
                var author_book = new Author_Book_DAO();
                if (author_book.insertAuthorBook(newBook, Authors) > 0)
                {
                    return RedirectToAction("AddBook", new { checkInsert = true });
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                ModelState.AddModelError("", "không thành công");
            }
            setViewBagForBook();
            return View("AddBook");
        }

        public void setViewBagForBook(long? selectedCategoryID = null, long? selectedAuthorID = null, long? selectedPublisherID = null)
        {
            ViewBag.listImage = imgHelper.loadListImage();
            var categoryDAO = new BookCategoryDAO();
            var authorDAO = new AuthorDAO();
            var publisherDAO = new PublisherDAO();
            ViewBag.Category = new SelectList(categoryDAO.getListCategory(), "ID", "Name");
            ViewBag.Authors = new SelectList(authorDAO.getListAuthor(), "ID", "Name");
            ViewBag.Publisher = new SelectList(publisherDAO.getListPublisher(), "ID", "Name");
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult deleteImage(string filename)
        {
            return Json(imgHelper.DeleteImageByfilename(filename), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult UpdateBook(int id, HttpPostedFileBase file)
        {
            setViewBagForBook();

            Book book = new BookDAO().getBookById(id);

            List<int> list = new List<int>();

            var bookView = new BookViewModel();

            foreach (var item in book.Authors)
            {
                list.Add(item.ID);
            }
            bookView.SelectedValuesAuthor = list.ToArray();
            bookView.Name = book.Name;
            bookView.Alias = book.Alias;
            bookView.Category = book.CategoryID;
            bookView.Content = book.Content;
            bookView.Price = book.Price;
            bookView.BookCover = book.BookCover;
            bookView.NumberPages = book.NumberPages;
            bookView.Publisher = book.PublisherID;
            bookView.PublicationDate = book.PublicationDate;
            bookView.Size = book.Size;
            bookView.Quanlity = book.Quanlity;
            bookView.Image = book.Image;
            return View(bookView);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateBook(int id, BookViewModel bookView, HttpPostedFileBase file)
        {
            setViewBagForBook();

            if (file != null)
            {
                bookView.Image = imgHelper.saveImage(file);
            }
            if (new BookDAO().UpdateBookDAO(id, bookView))
            {
                ViewBag.Success = true;
            }
            else
            {
                ViewBag.Success = false;
            }
            return View(bookView);
        }

        [HttpPost]
        public JsonResult DeleteBook(int id)
        {
            var result = new BookDAO().deleteBook(id);
            return Json(new
            {
                status = result
            });
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CheckUrlExist(string Alias)
        {
            if (new BookDAO().checkAliasExist(Alias))
            {
                return Json(string.Format("{0} is not available", Alias), JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);

        }

        [AllowAnonymous]
        public JsonResult CheckName(string Name)
        {
            return Json(!db.Books.Any(b => b.Name == Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult changeStatus(int id)
        {
            return Json(new
            {
                status = new BookDAO().changeStatus(id)
            });
        }
    }
}