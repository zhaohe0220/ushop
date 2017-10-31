using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class SqlHelper
    {
        private static readonly string connString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public static DataTable GetTable(string sql, CommandType type, params SqlParameter[] pars)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlDataAdapter apter = new SqlDataAdapter(sql, conn))
                {
                    apter.SelectCommand.CommandType = type;
                    //判断是否有值
                    if (pars != null)
                    {
                        //有值查询
                        apter.SelectCommand.Parameters.AddRange(pars);
                    }
                    DataTable da = new DataTable();
                    apter.Fill(da);
                    return da;
                }
            }
        }

        //******************************//
        public static DataTable Get(string sql, CommandType type)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlDataAdapter apter = new SqlDataAdapter(sql, conn))
                {
                    apter.SelectCommand.CommandType = type;
                    //判断是否有值
                  
                    DataTable da = new DataTable();
                    apter.Fill(da);
                    return da;
                }
            }
        }


        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="sql">连接sql语句</param>
        /// <param name="type"></param>
        /// <param name="pars">参数</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonquery(string sql, CommandType type, params SqlParameter[] pars)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = type;
                    if (pars != null)
                    {
                        cmd.Parameters.AddRange(pars);
                    }
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }



        /// <summary>
        /// 一行一列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="pars"></param>
        /// <returns>首行首列</returns>
        public static object ExecuteScalare(string sql, CommandType type, params SqlParameter[] pars)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = type;
                    if (pars != null)
                    {
                        cmd.Parameters.AddRange(pars);
                    }
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}
