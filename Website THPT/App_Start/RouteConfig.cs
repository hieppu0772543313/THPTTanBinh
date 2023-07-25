using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Website_THPT
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
              routes.MapRoute(
               name: "Home",
               url: "",
               defaults: new { controller = "School", action = "Index", id = UrlParameter.Optional }
               // Thêm hàng sau để tránh xung đột giữa các controller Home
               , namespaces: new[] { "Website_THPT.Controllers" });

            routes.MapRoute(
               name: "Trang tin",
               url: "{metatitle}",
               defaults: new { controller = "School", action = "TrangTin", metatitle = UrlParameter.Optional },
                // Thêm hàng sau để tránh xung đột giữa các controller Home
                namespaces: new string[] { "Website_THPT.Controllers" }
                );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "School", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Website_THPT.Controllers" }
            );
           
        }
    }
}
