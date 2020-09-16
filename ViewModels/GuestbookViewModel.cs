using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class GuestbookViewModel
    {
        //搜尋欄位
        [DisplayName("搜尋:")]
        public string Search { get; set; }
        //顯示資料陣列
        public List<Guestbooks> DataList { get; set; }
    }
}