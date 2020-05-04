using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ThucChien
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Create link index of controller khachhang
            routes.MapRoute(
                name: "khachhang",
                url: "khach-hang",
                defaults: new { controller = "KhachHang", action = "Index", id = UrlParameter.Optional }
            );

            //Create link xemchitiet of controller sanpham
            routes.MapRoute(
                name: "XemChiTiet",
                url: "{tensp}-{id}",
                defaults: new {Controller = "SanPham", action= "XemChiTiet", id = UrlParameter.Optional}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
