using Model.DAO;
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
using System.Globalization;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using OfficeOpenXml.Table;

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
            return Json(new
            {
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

        //public JsonResult BookExport(string searchText, string statusSelect)
        //{
        //    ExportToExcel(searchText, statusSelect);
        //    return Json(new
        //    {
        //        status = true
        //    });
        //}

        //private Stream CreateExcelFile(Stream stream = null)
        //{
        //    IEnumerable<BookViewModel> list;
        //    BookDAO dao = new BookDAO();

        //     list = dao.getListBook();

        //    var totalRecord = list.Count();


        //    ViewBag.totalRecord = totalRecord;

        //    using (var excelPackage = new ExcelPackage(stream ?? new MemoryStream()))
        //    {
        //        // Tạo author cho file Excel
        //        excelPackage.Workbook.Properties.Author = "Hanker";
        //        // Tạo title cho file Excel
        //        excelPackage.Workbook.Properties.Title = "EPP test background";
        //        // thêm tí comments vào làm màu 
        //        excelPackage.Workbook.Properties.Comments = "This is my fucking generated Comments";
        //        // Add Sheet vào file Excel
        //        excelPackage.Workbook.Worksheets.Add("First Sheet");
        //        // Lấy Sheet bạn vừa mới tạo ra để thao tác 
        //        var workSheet = excelPackage.Workbook.Worksheets[1];
        //        // Đổ data vào Excel file
        //        workSheet.Cells[1, 1].LoadFromCollection(list, true, TableStyles.Dark9);
        //        // BindingFormatForExcel(workSheet, list);
        //        excelPackage.Save();
        //        return excelPackage.Stream;
        //    }
        //}

        //public void Export(string searchText, string statusSelect)
        //{
        //    // Gọi lại hàm để tạo file excel
        //    var stream = CreateExcelFile();
        //    // Tạo buffer memory strean để hứng file excel
        //    var buffer = stream as MemoryStream;
        //    // Đây là content Type dành cho file excel, còn rất nhiều content-type khác nhưng cái này mình thấy okay nhất
        //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
        //    // File name của Excel này là ExcelDemo
        //    Response.AddHeader("Content-Disposition", "attachment; filename=ExcelDemo.xlsx");
        //    // Lưu file excel của chúng ta như 1 mảng byte để trả về response
        //    Response.BinaryWrite(buffer.ToArray());
        //    // Send tất cả ouput bytes về phía clients
        //    Response.Flush();
        //    Response.End();
        //    // Redirect về luôn trang index <img draggable="false" class="emoji" alt="😀" src="https://s0.wp.com/wp-content/mu-plugins/wpcom-smileys/twemoji/2/svg/1f600.svg">
        //}

        public ActionResult ExportToExcel(string searchText, string statusSelect)
        {
            IEnumerable<BookViewModel> list;
            BookDAO dao = new BookDAO();

            if (!string.IsNullOrEmpty(searchText))
            {
                list = dao.getListBookBySearchText(searchText);
            }
            else
            {
                list = dao.getListBook();
            }


            if (!string.IsNullOrEmpty(statusSelect))
            {
                var status = bool.Parse(statusSelect);
                list = list.Where(x => x.Status == status);
            }

            var totalRecord = list.Count();


            ViewBag.totalRecord = totalRecord;
            ExcelPackage excel = new ExcelPackage();
            ExcelWorksheet ws = excel.Workbook.Worksheets.Add("sheet1");

            ws.Cells["A1"].Value = "Cửa hàng:";
            ws.Cells["B1"].Value = "UnicornBookShop.vn";

            ws.Cells["A2"].Value = "Thời gian";
            ws.Cells["B2"].Value = string.Format("{0:dd/MM/yyyy} lúc {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A3"].Value = "Thống kê - Báo cáo:";
            ws.Cells["B3"].Value = "DANH SÁCH - SÁCH";

            ws.Cells["A6"].Value = "Có " + totalRecord + "  quyển sách.";

            ws.Cells["A7"].Value = "Mã sách";
            ws.Cells["B7"].Value = "Tên sách";
            ws.Cells["C7"].Value = "URL";
            ws.Cells["D7"].Value = "Giá";
            ws.Cells["E7"].Value = "Tác giả";
            ws.Cells["F7"].Value = "Thể loại";
            ws.Cells["G7"].Value = "Trạng thái";
            ws.Cells["H7"].Value = "Hình ảnh";
            ws.Cells["I7"].Value = "Ngày xuất bản";
            ws.Cells["J7"].Value = "Nhà xuất bản";


            CultureInfo culture = new CultureInfo("vi-VN");
            var rowStart = 8;
            foreach (var item in list)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.ID;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Name;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Alias;
                ws.Cells[string.Format("D{0}", rowStart)].Value = "";
                ws.Cells[string.Format("E{0}", rowStart)].Value = "";
                ws.Cells[string.Format("F{0}", rowStart)].Value = "";
                ws.Cells[string.Format("G{0}", rowStart)].Value = "";
                ws.Cells[string.Format("H{0}", rowStart)].Value = "";
                ws.Cells[string.Format("I{0}", rowStart)].Value = "";
                ws.Cells[string.Format("J{0}", rowStart)].Value = "";
                rowStart++;
            }


            //using (var range = ws.Cells[string.Format("A1:I{0}", rowStart)])
            //{
            //    //align cnter in Excel
            //    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //    //Convert date Datetime from C# to excel
            //    ws.Cells[string.Format("I8:I{0}", rowStart)].Style.Numberformat.Format = "dd/MM/yyyy";
            //    ws.Cells["A:AZ"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            //    ws.Cells[string.Format("A1:K6", rowStart)].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(198, 217, 235));
            //    ws.Cells[string.Format("A7:K7", rowStart)].Style.Font.Color.SetColor(Color.Black);
            //    ws.Cells[string.Format("A7:K7", rowStart)].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(66, 130, 189));
            //    ws.Cells[string.Format("A8:K{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(198, 217, 235));
            //}

            ws.Cells["A:AZ"].AutoFitColumns();

            var excelName = "report-books";

            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
            return RedirectToAction("Index");
        }
    }
}