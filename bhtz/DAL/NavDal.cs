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
    public class NavDal
    {
        /// <summary>
        /// 获取大类
        /// </summary>
        /// <returns></returns>
        #region 读取大分类
        public List<Nav> GetNav(string pid)
        {
            string sql = "select * from T_Type where pid=@pid";
            SqlParameter[] pars ={
                                     new SqlParameter("@pid",SqlDbType.Char,2),
                                 };
            pars[0].Value = pid;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            List<Nav> list = null;

            if (da.Rows.Count > 0)
            {
                list = new List<Nav>();
                foreach (DataRow row in da.Rows)
                {
                    Nav nav = null;
                    nav = new Nav();
                    LoadEntity(row, nav);
                    list.Add(nav);
                }
            }
            return list;
        } 
        #endregion




        /// <summary>
        /// 小类别页面头部相关分类
        /// </summary>
        /// <returns></returns>
        #region 读取小类别
        public List<Nav> GetSIndexNav(string id)
        {
            string sql = "select * from T_Type where pid=(select pid from T_Type where id=@id) ";
            SqlParameter[] pars ={
                                     new SqlParameter("@id",SqlDbType.Char,4),
                                 };
            pars[0].Value = id;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            List<Nav> littlelist = null;
            if (da.Rows.Count > 0)
            {
                littlelist = new List<Nav>();
                foreach (DataRow row in da.Rows)
                {
                    Nav nav = null;
                    nav = new Nav();
                    LoadEntity(row, nav);
                    littlelist.Add(nav);
                }
            }
            return littlelist;
        } 
        #endregion




        #region 关系转对象
        private void LoadEntity(DataRow row, Nav nav)
        {
            nav.id = row["id"] != DBNull.Value ? row["id"].ToString() : string.Empty;
            nav.pid = row["pid"] != DBNull.Value ? row["pid"].ToString() : string.Empty;
            nav.name = row["name"] != DBNull.Value ? row["name"].ToString() : string.Empty;
        } 
        #endregion
    }
}



