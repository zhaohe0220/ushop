using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class NeedDal
    {
        #region 发布需求
        public int AddNeed(NeedInfo NeedInfo)
        {
            string sql = "insert into T_Needs(UserID,Phone,QQ,Detail,Title,NeedsID)values(@UserID,@Phone,@QQ,@Detail,@Title,@NeedsID)";
            SqlParameter[] pars ={
                                    new SqlParameter("@UserId",SqlDbType.VarChar,20),
                                    new SqlParameter("@Phone",SqlDbType.VarChar,20),
                                    new SqlParameter("@QQ",SqlDbType.VarChar,20),
                                    new SqlParameter("@Detail",SqlDbType.NVarChar,500),
                                    new SqlParameter("@Title",SqlDbType.NVarChar,50),
                                    new SqlParameter("@NeedsID",SqlDbType.VarChar,20)
                                };
            pars[0].Value = NeedInfo.UserID;
            pars[1].Value = NeedInfo.Phone;
            if (NeedInfo.QQ==null)
            {
                pars[2].Value = "";
            }
            else { pars[2].Value = NeedInfo.QQ; }
           
            pars[3].Value = NeedInfo.Detail;
            pars[4].Value = NeedInfo.Title;
            pars[5].Value = NeedInfo.NeedsID;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        } 
        #endregion



        #region 读取需求列表
        public List<NeedInfo> GetNeedList(int start, int end)
        {
            string sql = "select * from (select row_number() over(order by ReleaseTime desc) as num,* from T_Needs where State='1') as t where t.num>=@start and t.num<=@end";
            SqlParameter[] pars ={
                                     new SqlParameter("@start",SqlDbType.Int),
                                     new SqlParameter ("@end",SqlDbType.Int)
                                 };
            pars[0].Value = start;
            pars[1].Value = end;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            List<NeedInfo> needList = null;
            if (da.Rows.Count > 0)
            {
                needList = new List<NeedInfo>();
                foreach (DataRow row in da.Rows)
                {
                    NeedInfo needInfo = null;
                    needInfo = new NeedInfo();
                    LoadEntity(needInfo, row);
                    needList.Add(needInfo);
                }
            }
            return needList;
        } 
        #endregion


        #region 读取个人中心需求列表
        public List<NeedInfo> GetCenterNeedList(string UserID,int start, int end)
        {
            string sql = "select * from (select row_number() over(order by ReleaseTime desc) as num,* from T_Needs where UserID=@UserID) as t where t.num>=@start and t.num<=@end";
            SqlParameter[] pars ={
                                     new SqlParameter("@UserID",SqlDbType.VarChar,20),
                                     new SqlParameter("@start",SqlDbType.Int),
                                     new SqlParameter("@end",SqlDbType.Int)
                                 };
            pars[0].Value = UserID;
            pars[1].Value = start;
            pars[2].Value = end;
          
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            List<NeedInfo> CenterNeed = null;
            if (da.Rows.Count > 0)
            {
                CenterNeed = new List<NeedInfo>();
                foreach (DataRow row in da.Rows)
                {
                    NeedInfo centerneed = null;
                    centerneed = new NeedInfo();
                    LoadEntity(centerneed, row);
                    CenterNeed.Add(centerneed);
                }
            }
            return CenterNeed;
        }
        #endregion



        #region 读取管理员中心需求列表
        public List<NeedInfo> GetGlyNeedList(int start, int end)
        {
            string sql = "select * from (select row_number() over(order by ReleaseTime desc) as num,* from T_Needs where State='0') as t where t.num>=@start and t.num<=@end";
            SqlParameter[] pars ={
                                     new SqlParameter("@start",SqlDbType.Int),
                                     new SqlParameter("@end",SqlDbType.Int)
                                 };
            pars[0].Value = start;
            pars[1].Value = end;

            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            List<NeedInfo> GlyNeed = null;
            if (da.Rows.Count > 0)
            {
                GlyNeed = new List<NeedInfo>();
                foreach (DataRow row in da.Rows)
                {
                    NeedInfo glyneed = null;
                    glyneed = new NeedInfo();
                    LoadEntity(glyneed, row);
                    GlyNeed.Add(glyneed);
                }
            }
            return GlyNeed;
        }
        #endregion




        #region 管理员审核需求
        public int shenheNeed(string shenheid, string temp)
        {
            string sql = null;
            if (temp == "1")
            {
                sql = "update T_Needs set State='1' where Id=@shenheid";
            }
            else
            {
                sql = "update T_Needs set State='2' where Id=@shenheid";
            }

            SqlParameter[] pars ={
                                     new SqlParameter("@shenheid",SqlDbType.Int)
                                 };
            pars[0].Value = shenheid;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        }
        #endregion





        #region 用户删除需求
        public int DeleteNeed(int deleteid)
        {
            string sql = "delete from T_Needs where Id=@deleteid";
            SqlParameter[] pars ={
                                     new SqlParameter("@deleteid",SqlDbType.Int)
                                 };
            pars[0].Value = deleteid;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        }
        #endregion





        #region 关系转对象
        public void LoadEntity(NeedInfo needInfo, DataRow row)
        {
            needInfo.Id = Convert.ToInt32(row["Id"]);
            needInfo.UserID = row["UserID"] != DBNull.Value ? row["UserID"].ToString() : string.Empty;
            needInfo.Phone = row["Phone"] != DBNull.Value ? row["Phone"].ToString() : string.Empty;
            needInfo.QQ = row["QQ"] != DBNull.Value ? row["QQ"].ToString() : string.Empty;
            needInfo.ReleaseTime = Convert.ToDateTime(row["ReleaseTime"]);
            needInfo.Title = row["Title"] != DBNull.Value ? row["Title"].ToString() : string.Empty;
            needInfo.Detail = row["Detail"] != DBNull.Value ? row["Detail"].ToString() : string.Empty;
            needInfo.NeedsID = row["NeedsID"] != DBNull.Value ? row["NeedsID"].ToString() : string.Empty;
            needInfo.State = row["State"] != DBNull.Value ? row["State"].ToString() : string.Empty;
        } 
        #endregion




        #region 需求详细信息
        public List<NeedInfo> GetNeedDetail(string nid)
        {
            string sql = "select * from T_Needs where Id=@nid";
            SqlParameter[] pars ={
                                     new SqlParameter("@nid",SqlDbType.Int)
                                 };
            pars[0].Value = nid;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            List<NeedInfo> Needdetail = null;
            if (da.Rows.Count > 0)
            {
                Needdetail = new List<NeedInfo>();
                NeedInfo needInfo = null;
                foreach (DataRow row in da.Rows)
                {
                    needInfo = new NeedInfo();
                    LoadEntity(needInfo, row);
                    Needdetail.Add(needInfo);
                }
            }
            return Needdetail;
        } 
        #endregion




        #region 需求数量
        public int GetNeedRecordCount()
        {
            string sql = "select count(*) from T_Needs where State='1'";
            return Convert.ToInt32(SqlHelper.ExecuteScalare(sql, CommandType.Text));
        } 
        #endregion


        #region 个人中心需求数量
        public int GetCenterNeedRecordCount(string UserID)
        {
            string sql = "select count(*) from T_Needs where UserID=@UserID";
            SqlParameter[] pars ={
                                    new SqlParameter("@UserID",SqlDbType.VarChar,20)
                                };
            pars[0].Value = UserID;
            return Convert.ToInt32(SqlHelper.ExecuteScalare(sql, CommandType.Text,pars));
        }
        #endregion



        #region 管理员中心需求数量
        public int GetGlyRecordCount()
        {
            string sql = "select count(*) from T_Needs where State='0'";
            return Convert.ToInt32(SqlHelper.ExecuteScalare(sql, CommandType.Text));
        }
        #endregion




        /// <summary>
        /// 需求更新
        /// </summary>
        /// <param name="ProductInfo"></param>
        /// <returns></returns>
        #region 需求更新
        public int NeedUpdate(NeedInfo NeedInfo)
        {
            string sql = "update T_Needs set Phone=@Phone,QQ=@QQ,Detail=@Detail,Title=@Title,ReleaseTime=@ReleaseTime,State=@State where Id=@Id";
            SqlParameter[] pars ={
                                    new SqlParameter("@Phone",SqlDbType.VarChar,20),
                                    new SqlParameter("@QQ",SqlDbType.VarChar,20),
                                    new SqlParameter("@Detail",SqlDbType.NVarChar,500),
                                    new SqlParameter("@Title",SqlDbType.NVarChar,50),
                                    new SqlParameter("@ReleaseTime",SqlDbType.DateTime),
                                    new SqlParameter("@State",SqlDbType.Char,1),
                                     new SqlParameter("@Id",SqlDbType.Int)
                                };
            pars[0].Value = NeedInfo.Phone;
            if (NeedInfo.QQ == null)
            {
                pars[1].Value = "";
            }
            else { pars[1].Value = NeedInfo.QQ; }
            pars[2].Value = NeedInfo.Detail;
            pars[3].Value = NeedInfo.Title;
            pars[4].Value = NeedInfo.ReleaseTime;
            pars[5].Value = NeedInfo.State;
            pars[6].Value = NeedInfo.Id;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        } 
        #endregion

    }
}
