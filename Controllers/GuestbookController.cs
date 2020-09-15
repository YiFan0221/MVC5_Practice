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
    public class GuestbookController : Controller
    {
                                            
        private readonly GuestbookDBService GuestbookService = new GuestbookDBService(); //提取GuestbookDBService的函式
        #region Index:取出資訊
       
        public ActionResult Index()
        {
            //create obj of VM
            GuestbookViewModel Data = new GuestbookViewModel();
            //get data from service to vmlist
            Data.DataList = GuestbookService.GetDataList();
            //return disp data to "view"
            return View(Data);
        }
        #endregion
        # region Create:"新增留言"一開始的載入頁面      
        public ActionResult Create() 
        {
            return PartialView();
        }
        #endregion

        #region Create:新增留言傳入動作時的Action       
        //只接受頁面Post資料傳入
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name,Content")] Guestbooks Data)
        {
            GuestbookService.InsertGuestbooks(Data);
            return RedirectToAction("Index");
        }
        #endregion
        #region Edit:修改留言Action
        //修改留言頁面要根據傳入編號來決定要修改的資料
        public ActionResult Edit(int Id)
        {
            //從Service取得頁面所需需求
            Guestbooks Data = GuestbookService.GetDataById(Id);
            //將資料傳入View中
            return View(Data);
        }

        //修改留言頁面要根據傳入編號來決定要修改的資料
        [HttpPost]
        public ActionResult Edit(int Id , [Bind(Include = "Name,Content")]Guestbooks UpdateData)
        {
            if (GuestbookService.CheckUpdate(Id))
            {
                //將編號設定至修改資料中
                UpdateData.Id = Id;
                //使用Service來修改資料
                GuestbookService.UpdateGuestBooks(UpdateData);
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
            Guestbooks Data = GuestbookService.GetDataById(Id);
            //將資料傳入View中
            return View(Data);
        }

        //修改留言頁面要根據傳入編號來決定要修改的資料
        [HttpPost]
        public ActionResult Reply(int Id, [Bind(Include = "Reply,ReplyTime")] Guestbooks ReplyData)
        {
            if (GuestbookService.CheckUpdate(Id))
            {
                //將編號設定至修改資料中
                ReplyData.Id = Id;
                //使用Service來修改資料
                GuestbookService.ReplyGuestBooks(ReplyData);
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
            GuestbookService.DeleteGuestbooks(Id);
            return RedirectToAction("Index");
        }
        #endregion
    }
}