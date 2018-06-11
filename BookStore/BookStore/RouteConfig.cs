using System.Web.Mvc;
using System.Web.Routing;

namespace BookStore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{*botdetect}",
              new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

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

            //routes.MapRoute(
            // name: "Danh mục",
            // url: "the-loai",
            // defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional },
            // namespaces: new[] { "SanGiaoDichBatDongSan.Controllers" }
            //);

            routes.MapRoute(
             name: "Danh mục sách",
             url: "the-loai/{Alias}",
             defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "BookStore.Controllers" }
            );

            routes.MapRoute(
             name: "Tất cả sách",
             url: "tat-ca",
             defaults: new { controller = "All", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "BookStore.Controllers" }
            );

            routes.MapRoute(
               name: "Liên hệ",
               url: "lien-he",
               defaults: new { controller = "Address", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "BookStore.Controllers" }
              );

            routes.MapRoute(
               name: "Sách bán chạy",
               url: "hot-book",
               defaults: new { controller = "HotBook", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "BookStore.Controllers" }
              );
            routes.MapRoute(
                name: "Tìm kiếm sách",
                url: "tim-kiem",
                defaults: new { controller = "Timkiem", action = "Search", id = UrlParameter.Optional },
                namespaces: new[] { "BookStore.Controllers" }
            );

            routes.MapRoute(
                name: "Thành viên đăng ký",
                url: "dang-ky",
                defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
                namespaces: new[] { "BookStore.Controllers" }
            );

            routes.MapRoute(
                name: "Thành viên đăng nhập",
                url: "dang-nhap",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "BookStore.Controllers" }
            );

            routes.MapRoute(
                name: "Thành viên đăng xuất",
                url: "thoat",
                defaults: new { controller = "User", action = "Logout", id = UrlParameter.Optional },
                namespaces: new[] { "BookStore.Controllers" }
            );

            routes.MapRoute(
                name: "Thành viên thay đổi",
                url: "thay-doi",
                defaults: new { controller = "User", action = "Change", id = UrlParameter.Optional },
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