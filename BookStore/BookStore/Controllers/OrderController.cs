using BookStore.Models;
using Model.DAO;
using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace BookStore.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SuccessOrder(int id)
        {
            Session["confirm"] = null;
            ViewBag.ID = Request["id"];
            return View();
        }

        public JsonResult Add(string orderDetail, List<ProductCartViewModel> listProduct, int totalCost)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            var OrderDetail = serializer.Deserialize<OrderDetail>(orderDetail);

            var OrderID = new OrderDAO().insert(listProduct, totalCost);

            var status = new OrderDetailDAO().insert(OrderDetail, OrderID);

            return Json(new
            {
                status = status,
                id = OrderID
            });

        }

        public JsonResult LoadProvince()
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/Assets/client/data/Provinces_District.xml"));

            var xElements = xmlDoc.Element("Root").Elements("Item").Where(x => x.Attribute("type").Value == "province");
            var list = new List<ProvinceModel>();
            ProvinceModel province = null;
            foreach (var item in xElements)
            {
                province = new ProvinceModel();
                province.ID = int.Parse(item.Attribute("id").Value);
                province.Name = item.Attribute("value").Value;
                list.Add(province);

            }
            return Json(new
            {
                data = list,
                status = true
            });
        }
        public JsonResult LoadDistrict(int provinceID)
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/Assets/client/data/Provinces_District.xml"));

            var xElement = xmlDoc.Element("Root").Elements("Item")
                .Single(x => x.Attribute("type").Value == "province" && int.Parse(x.Attribute("id").Value) == provinceID);

            var list = new List<DistrictModel>();
            DistrictModel district = null;
            foreach (var item in xElement.Elements("Item").Where(x => x.Attribute("type").Value == "district"))
            {
                district = new DistrictModel();
                district.ID = int.Parse(item.Attribute("id").Value);
                district.Name = item.Attribute("value").Value;
                district.ProvinceID = int.Parse(xElement.Attribute("id").Value);
                list.Add(district);

            }
            return Json(new
            {
                data = list,
                status = true
            });
        }

        public JsonResult LoadPrecinct(int districtID)
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/Assets/client/data/Provinces_District.xml"));
            var xElement = xmlDoc.Element("Root").Elements("Item").Elements("Item").Single(x => x.Attribute("type").Value == "district" && int.Parse(x.Attribute("id").Value) == districtID);
            var list = new List<PrecinctModel>();
            PrecinctModel precinct = null;
            foreach (var item in xElement.Elements("Item").Where(x => x.Attribute("type").Value == "precinct"))
            {
                precinct = new PrecinctModel();
                precinct.ID = int.Parse(item.Attribute("id").Value);
                precinct.Name = item.Attribute("value").Value;
                precinct.DistrictID = int.Parse(xElement.Attribute("id").Value);
                list.Add(precinct);
            }
            return Json(new
            {
                data = list,
                status = true
            });
        }
    }
}