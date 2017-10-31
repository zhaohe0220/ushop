using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class GlyServices
    {
        GlyDal GlyDal = new GlyDal();


        #region 登录
        public GlyInfo GetGlyInfo(string userName, string userPwd)
        {
            return GlyDal.GetGlyInfo(userName, userPwd);
        }
        #endregion


        #region 添加管理员
        public bool Addgly(GlyInfo glyInfo)
        {
            return GlyDal.Addgly(glyInfo) > 0;
        }
        #endregion


        #region 读取管理员列表
        public List<GlyInfo> GetGlyList()
        {
            List<GlyInfo> glyInfo = GlyDal.GetGlyList();
            return glyInfo;
        }
        #endregion



        #region 读取一个管理员列表
        public List<GlyInfo> GetOneGlyList(int glyid)
        {
            List<GlyInfo> glyInfo = GlyDal.GetOneGlyList(glyid);
            return glyInfo;
        }
        #endregion



        #region 更新管理员
        public bool Glyupdategly(GlyInfo glyInfo)
        {
            return GlyDal.Glyupdategly(glyInfo) > 0;
        }
        #endregion



        #region 删除管理员
        public bool DeleteGly(int deleteglyid)
        {
            return GlyDal.DeleteGly(deleteglyid) > 0;
        }
        #endregion



    }
}
