using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public class GuestbookService
    {
        
        private readonly string cnstr = @"Persist Security Info=False;
        Integrated Security=true; Server=LAPTOP-8FAAALRS\SQLEXPRESS;initial catalog=ASP.NET MVC;
        User ID=;Password=;";
        

        //private readonly string cnstr = @"provider connection string='data source=LAPTOP-8FAAALRS\SQLEXPRESS;initial catalog=ASP.NET MVC;integrated security=True;";

        public List<Guestbooks> GetDataList()
        {
            List<Guestbooks> DataList = new List<Guestbooks>();
            //SQL Cmd
            string sql = @"Select * from Guestbooks ";
            //Create SQLConnection
            SqlConnection   conn    = new SqlConnection(cnstr);
            //Open SQLConnectoon
            conn.Open();
            //Execute Cmd
            SqlCommand      cmd     = new SqlCommand(sql, conn);
            //Create SQLReader
            SqlDataReader   dr      = cmd.ExecuteReader();
            //Reader Data
            while (dr.Read())
            {
                Guestbooks Data = new Guestbooks();
                Data.Id         = Convert.ToInt32(dr["Id"]);
                Data.Name       = dr["Name"].ToString();
                Data.Content    = dr["Content"].ToString();
                DataList.Add(Data);

            }
            conn.Close();
            return DataList;
        }
    }
}