using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookStore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             name: "Giỏ hàng",
             url: "gio-hang",
             defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "BookStore.Controllers" }
            );

            routes.MapRoute(
             name: "Chi tiết sản phẩm",
             url: "chi-tiet-sach/{Alias}",
             defaults: new { controller = "Detail", action = "GetDetail", id = UrlParameter.Optional },
             namespaces: new[] { "BookStore.Controllers" }
            );

            routes.MapRoute(
              name: "Đặt hàng",
              url: "dat-hang",
              defaults: new { controller = "Order", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "BookStore.Controllers" }
           );

            routes.MapRoute(
             name: "Danh mục",
             url: "the-loai",
             defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "SanGiaoDichBatDongSan.Controllers" }
            );

            routes.MapRoute(
             name: "Danh mục sách",
             url: "the-loai/{Alias}",
             defaults: new { controller = "Category", action = "loadBookByAliasCategory", id = UrlParameter.Optional },
             namespaces: new[] { "BookStore.Controllers" }
            );

            routes.MapRoute(
               name: "Liên hệ",
               url: "lien-he",
               defaults: new { controller = "Address", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "BookStore.Controllers" }
              );
            routes.MapRoute(
             name: "Default",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "BookStore.Controllers" }
            );
          
        }
    }
}
