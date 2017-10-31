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
    public class NoticeDal
    {
        #region 读取公告
        public List<NoticeInfo> GetNotice()
        {
            string sql = "select * from T_Notice order by PublishTime desc";
            DataTable da = SqlHelper.Get(sql, CommandType.Text);
            List<NoticeInfo> noticeList = null;
            if (da.Rows.Count > 0)
            {
                noticeList = new List<NoticeInfo>();
                foreach (DataRow row in da.Rows)
                {
                    NoticeInfo noticeInfo = null;
                    noticeInfo = new NoticeInfo();
                    LoadEntity(noticeInfo, row);
                    noticeList.Add(noticeInfo);
                }
            }
            return noticeList;
        } 
        #endregion



        #region 关系转对象
        public void LoadEntity(NoticeInfo noticeInfo, DataRow row)
        {
            noticeInfo.ID = Convert.ToInt32(row["ID"]);
            noticeInfo.Title = row["Title"] != DBNull.Value ? row["Title"].ToString() : string.Empty;
            noticeInfo.NContent = row["NContent"] != DBNull.Value ? row["NContent"].ToString() : string.Empty;
            noticeInfo.PublishTime = Convert.ToDateTime(row["PublishTime"]);
        } 
        #endregion



        #region 读取公告详细信息
        public List<NoticeInfo> GetNoticeDetail(string nid)
        {
            string sql = "select * from T_Notice where ID=@nid";
            SqlParameter[] pars ={
                                     new SqlParameter("@nid",SqlDbType.Char,20)
                                 };
            pars[0].Value = nid;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            List<NoticeInfo> Ndetail = null;
            if (da.Rows.Count > 0)
            {
                Ndetail = new List<NoticeInfo>();
                NoticeInfo noticeInfo = null;
                foreach (DataRow row in da.Rows)
                {
                    noticeInfo = new NoticeInfo();
                    LoadEntity(noticeInfo, row);
                    Ndetail.Add(noticeInfo);
                }
            }
            return Ndetail;
        } 
        #endregion



        #region 发布公告
        public int Addgg(NoticeInfo NoticeInfo)
        {
            string sql = "insert into T_Notice(Title,NContent)values(@Title,@NContent)";
            SqlParameter[] pars ={
                                    new SqlParameter("@Title",SqlDbType.NVarChar,30),
                                    new SqlParameter("@NContent",SqlDbType.Text),
                                };
            pars[0].Value = NoticeInfo.Title;
            pars[1].Value = NoticeInfo.NContent;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        }
        #endregion
    }
}
