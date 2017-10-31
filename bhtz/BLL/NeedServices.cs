using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NeedServices
    {
        DAL.NeedDal NeedDal = new DAL.NeedDal();



        #region 添加需求
        public bool AddNeed(NeedInfo needinfo)
        {
            return NeedDal.AddNeed(needinfo) > 0;
        } 
        #endregion



        #region 读取需求列表
        public List<NeedInfo> GetNeedList(int pageIndex, int pageSize)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            List<NeedInfo> needInfo = NeedDal.GetNeedList(start, end);
            return needInfo;
        } 
        #endregion


        #region 读取个人中心需求列表
        public List<NeedInfo> GetCenterNeedList(string userid,int pageIndex, int pageSize)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            List<NeedInfo> CenterNeed = NeedDal.GetCenterNeedList(userid,start, end);
            return CenterNeed;
        } 
        #endregion


        #region 用户删除需求
        public bool DeleteNeed(int deleteid)
        {
            return NeedDal.DeleteNeed(deleteid) > 0;
        }
        #endregion



        #region 读取需求详细信息
        public List<NeedInfo> GetNeedDetail(string nid)
        {
            List<NeedInfo> Needdetail = NeedDal.GetNeedDetail(nid);
            return Needdetail;
        } 
        #endregion



        #region 需求页数总数
        public int GetNeedPageCount(int pageSize)
        {
            int recordCount = NeedDal.GetNeedRecordCount();
            int pageCount = Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));
            return pageCount;
        } 
        #endregion



        #region 读取管理员中心需求列表
        public List<NeedInfo> GetGlyNeedList(int pageIndex, int pageSize)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            List<NeedInfo> GlyNeed = NeedDal.GetGlyNeedList(start, end);
            return GlyNeed;
        }
        #endregion



        #region 管理员审核需求
        public bool shenheNeed(string shenheid, string temp)
        {
            return NeedDal.shenheNeed(shenheid, temp) > 0;
        }
        #endregion



        #region 管理员需求页数总数
        public int GetGlyNeedPageCount(int pageSize)
        {
            int recordCount = NeedDal.GetGlyRecordCount();
            int pageCount = Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));
            return pageCount;
        }
        #endregion


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="ProductInfo"></param>
        /// <returns></returns>
        #region 更新
        public bool NeedUpdate(NeedInfo needInfo)
        {
            return NeedDal.NeedUpdate(needInfo) > 0;
        } 
        #endregion



        #region 个人中心需求页数总数
        public int GetCenterNeedPageCount(string userid, int pageSize)
        {
            int recordCount = NeedDal.GetCenterNeedRecordCount(userid);
            int pageCount = Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));
            return pageCount;
        }
        #endregion

    }
}
