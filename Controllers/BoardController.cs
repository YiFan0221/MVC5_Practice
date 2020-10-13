using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Service;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class BoardController : Controller
    {
                                            
        private readonly BoradDBService GuestbooksService = new BoradDBService(); //提取GuestbookDBService的函式
        #region Index:取出資訊
        /*public ActionResult Index()
        {
            //create obj of VM
            GuestbookViewModel Data = new GuestbookViewModel();
            //get data from service to vmlist
            Data.DataList = GuestbooksService.GetDataList();
            //return disp data to "view"
            return View(Data);
        }
        public ActionResult Index(string Search)
        {
            GuestbookViewModel Data = new GuestbookViewModel();
            Data.Search = Search;
            Data.DataList = GuestbooksService.GetDataList(Data.Search);
            return View(Data);
        }*/
        /*public ActionResult Index(string Search,int Page = 1)
        {
            GuestbookViewModel Data = new GuestbookViewModel();
            Data.Search = Search;
            Data.Paging = new ForPaging(Page);
            Data.DataList = GuestbooksService.GetDataList(Data.Paging , Data.Search);
            return View(Data);
        }*/
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDataList(string Search, int Page = 1)
        {//取得資料陣列用Action，將Page(頁數)預設為1
            //宣告一個新頁面模型
            Board_ViewModels Data = new Board_ViewModels();
            //將傳入值Search(搜尋)放入頁面模型中
            Data.Search = Search;
            //新增頁面模型中的分頁
            Data.Paging = new ForPaging(Page);
            //從Service中取得頁面所需陣列資料
            Data.DataList = GuestbooksService.GetDataList(Data.Paging, Data.Search);
            //將頁面資料傳入View中
            return PartialView(Data);
        }
        [HttpPost]
        //設定搜尋為接受頁面POST傳入
        //使用Bind的Inculde來定義只接受的欄位，用來避免傳入其他不相干值
        public ActionResult GetDataList([Bind(Include = "Search")] Board_ViewModels Data)
        {
            //重新導向頁面至開始頁面，並傳入搜尋值 
            return RedirectToAction("GetDataList", new { Search = Data.Search });

        }
        #endregion
        # region Create:"新增留言"    
        public ActionResult Create() 
        {
            return PartialView();
        }
           
        //只接受頁面Post資料傳入
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name,Content")] Boards Data)
        {
            GuestbooksService.InsertGuestbooks(Data);
            return RedirectToAction("Index");
        }
        #endregion
        #region Edit:修改留言Action
        //修改留言頁面要根據傳入編號來決定要修改的資料
        public ActionResult Edit(int Id)
        {
            //從Service取得頁面所需需求
            Boards Data = GuestbooksService.GetDataById(Id);
            //將資料傳入View中
            return View(Data);
        }

        //修改留言頁面要根據傳入編號來決定要修改的資料
        [HttpPost]
        public ActionResult Edit(int Id , [Bind(Include = "Name,Content")]Boards UpdateData)
        {
            if (GuestbooksService.CheckUpdate(Id))
            {
                //將編號設定至修改資料中
                UpdateData.Id = Id;
                //使用Service來修改資料
                GuestbooksService.UpdateGuestBooks(UpdateData);
                return RedirectToAction("Index"); //重新導向
            }
            else
            {
                return RedirectToAction("Index"); //重新導向
            }
        }
        #endregion
        #region Reply:回覆留言Action
        //修改留言頁面要根據傳入編號來決定要修改的資料
        public ActionResult Reply(int Id)
        {
            //從Service取得頁面所需需求
            Boards Data = GuestbooksService.GetDataById(Id);
            //將資料傳入View中
            return View(Data);
        }

        //修改留言頁面要根據傳入編號來決定要修改的資料
        [HttpPost]
        public ActionResult Reply(int Id, [Bind(Include = "Reply,ReplyTime")] Boards ReplyData)
        {
            if (GuestbooksService.CheckUpdate(Id))
            {
                //將編號設定至修改資料中
                ReplyData.Id = Id;
                //使用Service來修改資料
                GuestbooksService.ReplyGuestBooks(ReplyData);
                return RedirectToAction("Index"); //重新導向
            }
            else
            {
                return RedirectToAction("Index"); //重新導向
            }
        }
        #endregion
        #region Delete:刪除留言Action
        public ActionResult Delete(int Id)
        {
            GuestbooksService.DeleteGuestbooks(Id);
            return RedirectToAction("Index");
        }
        #endregion
    }
}