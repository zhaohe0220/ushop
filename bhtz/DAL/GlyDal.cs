using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class GlyDal
    {
        #region 登录
        public GlyInfo GetGlyInfo(string userName, string userPwd)
        {
            string sql = "select * from T_Admin where AdminName=@Name and PassWord=@Pwd";
            SqlParameter[] pars = {
                                      new SqlParameter("@Name",SqlDbType.NVarChar,50),
                                      new SqlParameter("@Pwd",SqlDbType.VarChar,50),
                                  };
            pars[0].Value = userName;
            pars[1].Value = userPwd;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            GlyInfo glyInfo = null;
            if (da.Rows.Count > 0)
            {
                glyInfo = new GlyInfo();
                LoadEntity(glyInfo, da.Rows[0]);
            }
            return glyInfo;
        }
        #endregion


        #region 关系转对象
        public void LoadEntity(GlyInfo glyInfo, DataRow row)
        {
            glyInfo.AdminID = Convert.ToInt32(row["AdminID"]);
            glyInfo.AdminName = row["AdminName"] != DBNull.Value ? row["AdminName"].ToString() : string.Empty;
            glyInfo.PassWord = row["PassWord"] != DBNull.Value ? row["PassWord"].ToString() : string.Empty;
            glyInfo.Phone = row["Phone"] != DBNull.Value ? row["Phone"].ToString() : string.Empty;
            glyInfo.AddTime = Convert.ToDateTime(row["AddTime"]);
        }
        #endregion




        #region 添加管理员
        public int Addgly(GlyInfo glyInfo)
        {
            string sql = "insert into T_Admin(AdminName,PassWord,Phone)values(@AdminName,@PassWord,@Phone)";
            SqlParameter[] pars ={
                                    new SqlParameter("@AdminName",SqlDbType.NVarChar,50),
                                    new SqlParameter("@PassWord",SqlDbType.VarChar,50),
                                    new SqlParameter("@Phone",SqlDbType.VarChar,20)
                                };
            pars[0].Value = glyInfo.AdminName;
            pars[1].Value = glyInfo.PassWord;
            pars[2].Value = glyInfo.Phone;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        }
        #endregion



        #region 读取管理员列表
        public List<GlyInfo> GetGlyList()
        {
            string sql = "select * from T_Admin order by AddTime desc";
            DataTable da = SqlHelper.Get(sql, CommandType.Text);
            List<GlyInfo> GlyList = null;
            if (da.Rows.Count > 0)
            {
                GlyList = new List<GlyInfo>();
                foreach (DataRow row in da.Rows)
                {
                    GlyInfo glyInfo = null;
                    glyInfo = new GlyInfo();
                    LoadEntity(glyInfo, row);
                    GlyList.Add(glyInfo);
                }
            }
            return GlyList;
        }
        #endregion


        #region 读取一个管理员列表
        public List<GlyInfo> GetOneGlyList(int glyid)
        {
            string sql = "select * from T_Admin where AdminID=@glyid";
            SqlParameter[] pars ={
                                    new SqlParameter("@glyid",SqlDbType.Int)
                                };
            pars[0].Value = glyid;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text,pars);
            List<GlyInfo> OneGlyList = null;
            if (da.Rows.Count > 0)
            {
                OneGlyList = new List<GlyInfo>();
                foreach (DataRow row in da.Rows)
                {
                    GlyInfo oneglyInfo = null;
                    oneglyInfo = new GlyInfo();
                    LoadEntity(oneglyInfo, row);
                    OneGlyList.Add(oneglyInfo);
                }
            }
            return OneGlyList;
        }
        #endregion



        #region 更新管理员
        public int Glyupdategly(GlyInfo glyInfo)
        {
            string sql = "update T_Admin set AdminName=@AdminName,PassWord=@PassWord,Phone=@Phone,AddTime=@AddTime where AdminId=@AdminId";
            SqlParameter[] pars ={
                                    new SqlParameter("@AdminName",SqlDbType.NVarChar,50),
                                    new SqlParameter("@PassWord",SqlDbType.VarChar,50),
                                    new SqlParameter("@Phone",SqlDbType.VarChar,20),
                                    new SqlParameter("@AddTime",SqlDbType.DateTime),
                                    new SqlParameter("@AdminId",SqlDbType.Int)
                                };
            pars[0].Value = glyInfo.AdminName;
            pars[1].Value = glyInfo.PassWord;
            pars[2].Value = glyInfo.Phone;
            pars[3].Value = glyInfo.AddTime;
            pars[4].Value = glyInfo.AdminID;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        }
        #endregion




        #region 删除管理员
        public int DeleteGly(int deleteglyid)
        {
            string sql = "delete from T_Admin where AdminID=@deleteglyid";
            SqlParameter[] pars ={
                                     new SqlParameter("@deleteglyid",SqlDbType.Int)
                                 };
            pars[0].Value = deleteglyid;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        }
        #endregion



    }
}
