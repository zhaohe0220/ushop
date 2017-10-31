using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class NoticeServices
    {
        DAL.NoticeDal NoticeDal = new DAL.NoticeDal();



        #region 读取公告
        public List<NoticeInfo> GetNotice()
        {
            List<NoticeInfo> noticeInfo = NoticeDal.GetNotice();
            return noticeInfo;
        } 
        #endregion



        #region 读取公告详细信息
        public List<NoticeInfo> GetNoticeDetail(string nid)
        {
            List<NoticeInfo> Ndetail = NoticeDal.GetNoticeDetail(nid);
            return Ndetail;
        } 
        #endregion




        #region 添加公告
        public bool Addgg(NoticeInfo noticeInfo)
        {
            return NoticeDal.Addgg(noticeInfo) > 0;
        }
        #endregion
        
    }
}
