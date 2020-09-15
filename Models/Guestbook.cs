using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace WebApplication1.Models
{
    public class Guestbooks
    {

        [DisplayName("編號:")] //目前型態式int 之後要改成GUID
        public int Id { get; set; }

        [DisplayName("姓名:")]
        [Required(ErrorMessage = "請輸入姓名")]//加入驗證
        [StringLength(20,ErrorMessage = "名字不能超過20")]
        public string Name{ get; set; }

        [DisplayName("留言內容：")]
        [Required(ErrorMessage = "請輸入留言內容")]
        [StringLength(100,ErrorMessage = "留言內容不能超過100字")]

        public string Content{ get; set; }

        [DisplayName("新增時間:")]
        public DateTime CreateTime { get; set; }

        [DisplayName("回覆內容:")]
        [StringLength(100,ErrorMessage = "回覆內容不能超過100字")]
        public string Reply { get; set; }

        [DisplayName("回覆時間:")]
        public DateTime ReplyTime { get; set; }

    }
}