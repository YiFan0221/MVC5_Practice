using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public class GuestbookDBService
    {
        //從Web.config抓取連線字串 ConnectionString
        private readonly static string cnstr = ConfigurationManager.
            ConnectionStrings["ASP.NET MVC"].ConnectionString;

        //建立與資料庫連線
        private readonly SqlConnection conn = new SqlConnection(cnstr);
        public List<Guestbooks> GetDataList(ForPaging Paging, string Search)
        {
            List<Guestbooks> DataList = new List<Guestbooks>();

            //Sql語法
            if (!string.IsNullOrWhiteSpace(Search))
            {
                SetMaxPaging(Paging, Search);
                DataList = GetAllDataList(Paging);
            }
            else
            {
                SetMaxPaging(Paging);
                DataList = GetAllDataList(Paging);
            }
            return DataList;
        }
        public void SetMaxPaging(ForPaging Paging)
        {
            int Row = 0;
            string sql = $@"SELECT * FROM Guestbooks; ";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Row++;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally 
            {
                conn.Close();
            }
            //計算總頁數
            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Row) / Paging.ItemNum));
            //重新設定正確頁數
            Paging.SetRightPage();
        }
        public void SetMaxPaging(ForPaging Paging,string Search)
        {
            int Row = 0;
            string sql = 
                $@"SELECT * FROM Guestbooks WHERE Name LIKE'%{Search}%' 
                OR Content LIKE '%{Search}%' 
                OR Reply LIKE '%{ Search}%'; ";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Row++;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }

            //計算總頁數
            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Row) / Paging.ItemNum));
            //重新設定正確頁數
            Paging.SetRightPage();
        }
        public List<Guestbooks> GetAllDataList(ForPaging paging, string Search)
        {
            List<Guestbooks> DataList = new List<Guestbooks>();
            string sql = $@" select * from (select row_number() over(order by Id) as sort,* from Guestbooks where Name like '%{Search}%' or 
            Content like '%{Search}%' or Reply like '%{Search}%' ) m Where m.sort Between {(paging.NowPage - 1) * paging.ItemNum + 1} and {paging.NowPage * paging.ItemNum} ";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Guestbooks Data = new Guestbooks();
                    Data.Id = Convert.ToInt32(dr["Id"]);
                    Data.Name = dr["Name"].ToString();
                    Data.Content = dr["Content"].ToString();
                    Data.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    if (!dr["ReplyTime"].Equals(DBNull.Value))
                    {
                        Data.Reply = dr["Reply"].ToString();
                        Data.ReplyTime = Convert.ToDateTime(dr["ReplyTime"]);
                    }
                    DataList.Add(Data);
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return DataList;
        }
        public List<Guestbooks> GetAllDataList(ForPaging paging)
        {
            List<Guestbooks> DataList = new List<Guestbooks>();
            string sql = $@" select * from (select row_number() over(order by Id) as sort,* from Guestbooks ) m
                Where m.sort Between {(paging.NowPage - 1) * paging.ItemNum + 1} and {paging.NowPage * paging.ItemNum} ";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Guestbooks Data = new Guestbooks();
                    Data.Id = Convert.ToInt32(dr["Id"]);
                    Data.Name = dr["Name"].ToString();
                    Data.Content = dr["Content"].ToString();
                    Data.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    if (!dr["ReplyTime"].Equals(DBNull.Value))
                    {
                        Data.Reply = dr["Reply"].ToString();
                        Data.ReplyTime = Convert.ToDateTime(dr["ReplyTime"]);
                    }
                    DataList.Add(Data);
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }
            finally {
                conn.Close();
            }
            return DataList;
        }
        public List<Guestbooks> GetDataList(string Search)
        {
            List<Guestbooks> DataList = new List<Guestbooks>();
            //SQL Cmd
            string sql = string.Empty;
            if (!string.IsNullOrWhiteSpace(Search))
            {
                sql = $@" SELECT * FROM Guestbooks WHERE Name LIKE '%{Search}%'
                OR Content LIKE '%{Search}%' OR Reply LIKE '%{Search}%'; ";
            }
            else
            { 
                sql = @"SELECT * FROM Guestbooks; ";
            }
            try
            {
                //Create SQLConnection
                SqlConnection conn = new SqlConnection(cnstr);
                //Open SQLConnectoon
                conn.Open();
                //Execute Cmd
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create SQLReader
                SqlDataReader dr = cmd.ExecuteReader();
                //Reader Data
                while (dr.Read())
                {
                    Guestbooks Data = new Guestbooks();
                    Data.Id = Convert.ToInt32(dr["Id"]);
                    Data.Name = dr["Name"].ToString();
                    Data.Content = dr["Content"].ToString();
                    Data.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    //檢查是否為空白 若不是就擷取資料
                    if (!dr["ReplyTime"].Equals(DBNull.Value))
                    {
                        Data.Reply = dr["Reply"].ToString();
                        Data.ReplyTime = Convert.ToDateTime(dr["ReplyTime"]);
                    }
                    DataList.Add(Data);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return DataList;
        }
        public List<Guestbooks> GetDataList()
        {
            List<Guestbooks> DataList = new List<Guestbooks>();
            //SQL Cmd
            string sql = @"Select * from Guestbooks ";

            try
            {
                //Create SQLConnection
                SqlConnection conn = new SqlConnection(cnstr);
                //Open SQLConnectoon
                conn.Open();
                //Execute Cmd
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create SQLReader
                SqlDataReader dr = cmd.ExecuteReader();
                //Reader Data
                while (dr.Read())
                {
                    Guestbooks Data = new Guestbooks();
                    Data.Id = Convert.ToInt32(dr["Id"]);
                    Data.Name = dr["Name"].ToString();
                    Data.Content = dr["Content"].ToString();
                    Data.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    //檢查是否為空白 若不是就擷取資料
                    if (!dr["ReplyTime"].Equals(DBNull.Value))
                    {
                        Data.Reply = dr["Reply"].ToString();
                        Data.ReplyTime = Convert.ToDateTime(dr["ReplyTime"]);
                    }
                    DataList.Add(Data);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return DataList;
        }
        #region 新增一筆資料
        public void InsertGuestbooks(Guestbooks newData)
        {
            //SQL cmd :: Insert
            string sql = $@" INSERT INTO Guestbooks(Name , Content , CreateTime) VALUES('{newData.Name}','{newData.Content}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' ); ";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region 查詢一筆資料
        //藉由編號取得單筆資料方法
        public Guestbooks GetDataById(int Id)
        {
            Guestbooks Data = new Guestbooks();
            //sql
            string sql = $@" SELECT * FROM Guestbooks WHERE Id = {Id};";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Data.Id = Convert.ToInt32(dr["Id"]);
                Data.Name = dr["Name"].ToString();
                Data.Content = dr["Content"].ToString();
                Data.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                //確定此則留言是否回覆 且不允許空白
                if (!string.IsNullOrWhiteSpace(dr["Reply"].ToString()))
                {
                    Data.Reply = dr["Reply"].ToString();
                    Data.ReplyTime = Convert.ToDateTime(dr["ReplyTime"]);
                }
            }
            catch (Exception)
            {
                //查無資料
                Data = null;
            }
            finally
            {
                //關閉資料庫連線
                conn.Close();
            }
            return Data;
        }
        #endregion

        #region 修改留言
        //修改留言
        public void UpdateGuestBooks(Guestbooks UpdateData)
        {

            //sql
            string sql = $@" UPDATE Guestbooks SET Name ='{UpdateData.Name}',
                Content ='{UpdateData.Content}' WHERE Id= {UpdateData.Id}; ";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                //丟出錯誤
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                //關閉資料庫連線
                conn.Close();
            }         
        }
        #endregion

        #region 回覆留言
        //回覆留言
        public void ReplyGuestBooks(Guestbooks ReplyData)
        {

            //sql
            string sql = $@" UPDATE Guestbooks SET Reply ='{ReplyData.Reply}',
                ReplyTime = '{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}' WHERE Id = {ReplyData.Id}";
            //確保程是不會因錯誤而中斷
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                //丟出錯誤
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                //關閉資料庫連線
                conn.Close();
            }
        }
        #endregion

        #region 檢查相關
        //修改資料判斷的地方
        public bool CheckUpdate(int Id)
        {
            //根據Id取得要修改的資料
            Guestbooks Data = GetDataById(Id);
            //判斷並回傳 (判斷是否有資料以及是否有回應)
            return (Data !=null && (Data.ReplyTime == null || Data.ReplyTime.ToShortDateString() == "0001/1/1"));
        }
        #endregion

        #region 刪除留言
        public void DeleteGuestbooks(int Id)
        {
            string sql = $@" DELETE FROM Guestbooks WHERE Id = {Id};";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }

        }
        #endregion
    }

}
    
