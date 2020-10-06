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
            ViewBag.Message = "嗨!  您好!" +
                "因興趣而開始利用時間開始試著建立自己的網站。" +
                "裏頭試做與資料庫對接並用Ajax顯示的留言板" +
                "未來還會增加會員登入系統、以及購物車的功能。" ;

            return View();
        }

        public ActionResult Contact()
        {
           return View();
        }
    }
}