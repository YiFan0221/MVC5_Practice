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
            ViewData["Message"] = "網站實現實現部分功能";
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "嗨!   我是逸帆!" +
                "我原本寫C++但因為興趣開始利用時間開始試著建置自己的網站。" +
                "這是我練習試做的網站，裏頭有留言板、會員登入系統、以及購物車的練習。" ;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "你可以透過 : g1991221@gmail.com 來連繫我!";

            return View();
        }
    }
}