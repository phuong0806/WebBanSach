using Model.DAO;
using Model.EF;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BookStore.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {

        // GET: Admin/Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderDetail(int id)
        {
            var dao = new OrderDetailDAO();
            var order = dao.getOrderDetailByID(id);

            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            order.TotalCostString = double.Parse(order.TotalCost.ToString()).ToString("#,###", cul.NumberFormat);
            ViewBag.Order = order;
            ViewBag.ListBook = dao.getListBookOfOder(id);
            return View();
        }

        public JsonResult ChangeStatus(int id, int idStatus)
        {
            return Json(new
            {
                status = new OrderDAO().changeStatus(id, idStatus),
                idStatus = idStatus
            });
        }

        public JsonResult changeFinish(int id)
        {
            return Json(new
            {
                status = new OrderDAO().changeFinish(id),
            });
        }

        public JsonResult ConfirmOrder(int id)
        {
            return Json(new
            {
                status = new OrderDAO().confirmOrder(id, Session["Username"].ToString())
            });
        }

        public string loadData(string searchText, string idStatus, string fromDate, string toDate)
        {
            var model = new OrderDAO().getList(searchText, idStatus, fromDate, toDate);
            var json = JsonConvert.SerializeObject(model, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return json;
        }

        public ActionResult ExportToExcel(string name, string searchText, string statusSelect, string fromDate, string toDate)
        {
            IEnumerable<Order> list;
            OrderDAO dao = new OrderDAO();

            list = dao.getList(searchText, statusSelect, fromDate, toDate);

            var totalRecord = list.Count();

            ViewBag.totalRecord = totalRecord;

            ExcelPackage excel = new ExcelPackage();
            ExcelWorksheet ws = excel.Workbook.Worksheets.Add("sheet1");

            ws.Cells["A1"].Value = "Cửa hàng:";
            ws.Cells["B1"].Value = "UnicornBookShop.vn";

            ws.Cells["A2"].Value = "Thời gian";
            ws.Cells["B2"].Value = string.Format("{0:dd/MM/yyyy} lúc {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A3"].Value = "Thống kê - Báo cáo:";
            ws.Cells["B3"].Value = "DANH SÁCH - Đơn hàng";

            ws.Cells["A6"].Value = "Có " + totalRecord + "  quyển sách.";

            ws.Cells["A7"].Value = "Đơn hàng";
            ws.Cells["B7"].Value = "Ngày đặt hàng";
            ws.Cells["C7"].Value = "Tổng tiền";
            ws.Cells["D7"].Value = "Trạng thái";
            ws.Cells["E7"].Value = "Xác thực";
            CultureInfo culture = new CultureInfo("vi-VN");
            var rowStart = 8;
            foreach (var item in list)
            {
                var statusOrder = "";
                if (item.Status == null && item.isFinish == false)
                {
                    statusOrder = "Đơn hàng mới";
                } else if (item.Status == 0 && item.isFinish == false) {
                    statusOrder = "Đang xử lý";
                } else if (item.Status == 1 && item.isFinish == false) {
                    statusOrder = "Đang vận chuyển";
                } else {
                    statusOrder = "Hoàn tất";
                }
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.ID;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.CreatedDate;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.TotalCost.Value.ToString("c", culture);
                ws.Cells[string.Format("D{0}", rowStart)].Value = statusOrder;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.isConfirm == true ? "YES" : "NO";
                rowStart++;
            }

            using (var range = ws.Cells[string.Format("A1:K{0}", rowStart)])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[string.Format("B8:F{0}", rowStart)].Style.Numberformat.Format = "dd/MM/yyyy";
            }

            ws.Cells["A:AZ"].AutoFitColumns();

            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=" + name + ".xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
            return RedirectToAction("Index");
        }
    }
}