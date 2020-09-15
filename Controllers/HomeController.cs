using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace WebApplication1.Controllers
{
    public class TestFormModel
    { 
        public string Content { get; set; }
        public string UserId { get; set; }
        public int Age { get; set; }
    }
    public class HomeController : Controller
    {
        //public ActionResult TestAction(string Content)
        public ActionResult TestAction(TestFormModel form)
        {            
            ViewData["Content"] = form.Content;
            ViewData["UserId"] = form.UserId;
            ViewData["Age"] = form.Age;
            return View();
        }
        
        public ActionResult Index()
        {
            ViewData["Message"] = "修改此範本即可開始著手進行您的ASP.NET MVC 應用程式。";
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}