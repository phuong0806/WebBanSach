﻿using Model.EF;
using Model.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Model.DAO
{
    public class BookDAO
    {
        BookStoreDbContext db = null;

        public BookDAO()
        {
            db = new BookStoreDbContext();
        }

        public int insertBook(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
            return book.ID;
        }

        public IEnumerable<BookViewModel> getListBookBySearchText(string searchText)
        {
            var result =
                    (from book in db.Books
                     where book.Name.Contains(searchText)
                     select new BookViewModel()
                     {
                         ID = book.ID,
                         Authors = book.Authors,
                         Name = book.Name,
                         Alias = book.Alias,
                         Price = book.Price,
                         Quanlity = book.Quanlity,
                         ViewCount = book.ViewCount,
                         Status = book.Status,
                         Image = book.Image,
                         MoreImages = book.MoreImages,
                     }).ToList();
            return result;
        }
        

        public IEnumerable<BookViewModel> getBookExport()
        {
            var result = (from book in db.Books
                          join publish in db.Publishers on book.PublisherID equals publish.ID
                          join category in db.BookCategories on book.CategoryID equals category.ID
                          select new BookViewModel()
                          {
                              ID = book.ID,
                              Name = book.Name,
                              Alias = book.Alias,
                              Price = book.Price,
                              Quanlity = book.Quanlity,
                              PublicationDate = book.PublicationDate,
                              PublisherName = publish.Name,
                              CategoryName = category.Name,
                              Authors = book.Authors,
                          }).ToList();

            return result;
        }

        public IEnumerable<BookViewModel> getBookExportBySearchText(string searchText)
        {
            var result = (from book in db.Books
                          join publish in db.Publishers on book.PublisherID equals publish.ID
                          join category in db.BookCategories on book.CategoryID equals category.ID
                          where book.Name.Contains(searchText)
                          select new BookViewModel()
                          {
                              ID = book.ID,
                              Name = book.Name,
                              Alias = book.Alias,
                              Price = book.Price,
                              Quanlity = book.Quanlity,
                              PublicationDate = book.PublicationDate,
                              PublisherName = publish.Name,
                              CategoryName = category.Name,
                              Authors = book.Authors,
                          }).ToList();

            return result;
        }

        public IEnumerable<BookViewModel> getListBook()
        {
            var result =
                (from book in db.Books
                 select new BookViewModel()
                 {
                     ID = book.ID,
                     Name = book.Name,
                     Alias = book.Alias,
                     Price = book.Price,
                     Quanlity = book.Quanlity,
                     ViewCount = book.ViewCount,
                     Status = book.Status,
                     Image = book.Image,
                     MoreImages = book.MoreImages,
                 }).ToList();
            return result;
        }


        public string getAuthorOfBookById(int BookID)
        {
            var data = (from book in db.Books
                        from author in book.Authors
                        join author_book in db.Books on book.ID equals author_book.ID
                        select new BookViewModel()
                        {
                            Name = author.Name,
                        });
            string result = "";
            int i = 0;
            foreach (var item in data)
            {
                i++;
                if (data.Count() == i)
                {
                    result += item.Name;
                    return result;
                }
                result += item.Name + ", ";
            }
            return result;
        }

        public bool deleteBook(int id)
        {
            try
            {
                var book = db.Books.Find(id);
                if (new Author_Book_DAO().deleteBookInAuthorBook(book))
                {
                    db.Books.Remove(book);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public int countBook()
        {
            return db.Books.Where(x => x.Status == true).Count();
        }

        public Book getBookById(int id)
        {
            return db.Books.Find(id);
        }

        public bool checkAliasExist(string Alias)
        {
            if (db.Books.Any(x => x.Alias == Alias))
            {
                return true;
            }
            return false;
        }

        public bool UpdateBookDAO(int id, BookViewModel bookView)
        {
            try
            {
                Book book = new Book();
                book = db.Books.Where(x => x.ID == id).FirstOrDefault();

                List<int> list = new List<int>();

                foreach (var item in book.Authors.ToList())
                {
                    book.Authors.Remove(item);
                }

                foreach (var item in bookView.SelectedValuesAuthor)
                {
                    list.Add(item);
                }

                foreach (var item in list)
                {
                    book.Authors.Add(db.Authors.FirstOrDefault(x => x.ID == item));
                }

                book.Name = bookView.Name;
                book.Alias = bookView.Alias;
                book.CategoryID = bookView.Category;
                book.Content = bookView.Content;
                book.Price = bookView.Price;
                book.BookCover = bookView.BookCover;
                book.NumberPages = bookView.NumberPages;
                book.PublisherID = bookView.Publisher;
                book.PublicationDate = bookView.PublicationDate;
                book.Size = bookView.Size;
                book.Quanlity = bookView.Quanlity;
                book.Image = bookView.Image;
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                string x = ex.GetType().Name;
                return false;
            }

        }

        public bool checkName(string name)
        {
            if (db.Books.Any(x => x.Name == name))
            {
                return true;
            }
            return false;
        }

        public bool changeStatus(int id)
        {
            try
            {
                var model = db.Books.Find(id);
                model.Status = !model.Status;
                db.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException)
            {
                return false;
            }
        }

        public IEnumerable<Book> getListBooksInCart(List<Book> listBook)
        {
            List<Book> listBookInCart = new List<Book>();
            foreach (var item in listBook)
            {
                var entity = db.Books.Find(item.ID);
                listBookInCart.Add(entity);
            }
            return listBookInCart;
        }
        public List<Book> GetBook()
        {
            DateTime a = DateTime.Now;
            int month = a.Month;
            var listBook = db.Books.Where(x=>x.CreatedDate.Value.Month==month).ToList();
            //var listBook = (from book in db.Books
            //                from author in book.Authors
            //                join author_book in db.Books on book.ID equals author_book.ID
            //                where book.Status == true && book.PublicationDate.Value.Month == month
            //                select new BookViewModel()
            //                {
            //                    Authors = author.Name,
            //                    Name = book.Name,
            //                    Image = book.Image,
            //                    Price = book.Price,
            //                    PublicationDate = book.PublicationDate

            //                }).ToList();
            return listBook;
            //return db.Books.Where(x => x.Status == true).ToList();
        }

        public List<Book> GetBookPre()
        {
            DateTime a = DateTime.Now;
            int month = a.Month - 1;
            var book = db.Books.Where(x => x.Status == true && x.CreatedDate.Value.Month == month && x.HotFlag==true).ToList();
            //var listBook = (from book in db.Books
            //                from author in book.Authors
            //                join author_book in db.Books on book.ID equals author_book.ID
            //                where book.Status == true && book.PublicationDate.Value.Month == month
            //                select new BookViewModel()
            //                {
            //                    AuthorsName = author.Name,
            //                    Name = book.Name,
            //                    Image = book.Image,
            //                    Price = book.Price,
            //                    PublicationDate = book.PublicationDate

            //                }).ToList();
            return book;
        }

        public List<Book> GetBookByAliasCategory(string Alias)
        {
            var ctlg = db.BookCategories.Where(x => x.Alias == Alias).FirstOrDefault();
            var listBook = db.Books.Where(x => x.CategoryID == ctlg.ID && x.Status == true).ToList();
            //var listBook = (from book in db.Books
            //                from author in book.Authors
            //                join author_book in db.Books on book.ID equals author_book.ID
            //                where book.Status == true && book.CategoryID == ctlg.ID
            //                select new BookViewModel()
            //                {
            //                    AuthorsName = author.Name,
            //                    Name = book.Name,
            //                    Image = book.Image,
            //                    Price = book.Price,
            //                    Alias = book.Alias

            //                }).ToList();
            return listBook;
        }
        public List<Book> GetNewBook()
        {
            DateTime a = DateTime.Now;
            int month = a.Month;
            var book= db.Books.Where(x => x.Status == true &&x.CreatedDate.Value.Month==month).ToList();
            //var listBook = (from book in db.Books
            //                from author in book.Authors
            //                join author_book in db.Books on book.ID equals author_book.ID
            //                where book.Status == true && book.PublicationDate.Value.Month == month
            //                select new BookViewModel()
            //                {
            //                    AuthorsName = author.Name,
            //                    Name = book.Name,
            //                    Image = book.Image,
            //                    Price = book.Price,
            //                    Alias = book.Alias,
            //                    PublicationDate = book.PublicationDate

            //                }).ToList();
            return book;
        }

        public List<Book>Detail(string Alias)
        {
            var sach= db.Books.Where(x => x.Alias == Alias).FirstOrDefault();
            var tim = db.Books.Find(sach.ID);
            tim.ViewCount += 1;
            db.SaveChanges();
            return db.Books.Where(x => x.Alias == Alias).ToList();
        }
        
        public List<Book>Same(int? id=null)
        {
            return db.Books.Where(x => x.CategoryID == id).ToList();
        }

        public List<Book>Hot()
        {
            return db.Books.Where(x => x.ViewCount >= 10).ToList();
        }

        public List<string> ListName(string tukhoa)
        {
            return db.Books.Where(x => x.Name.Contains(tukhoa)).Select(x => x.Name).ToList();
        }

        public List<Book> Search(string tukhoa)
        {
            return db.Books.Where(x => x.Name.Contains(tukhoa)).ToList();
        }
    }
}
