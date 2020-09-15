using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "View",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "TestAction", id = UrlParameter.Optional }
            );
            
            routes.MapRoute(
                name: "Route1",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Guestbook", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
