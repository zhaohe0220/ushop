using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;


namespace BLL
{
    public class NavServices
    {
        DAL.NavDal NavDal = new DAL.NavDal();



        /// <summary>
        /// 大类bll
        /// </summary>
        /// <returns></returns>
        #region 大类BLL
        public List<Nav> GetNav(string id)
        {
            List<Nav> list = NavDal.GetNav(id);
            return list;
        } 
        #endregion



        /// <summary>
        ///小类别页面头部相关分类
        /// </summary>
        /// <returns></returns>
        #region 小类BLL
        public List<Nav> GetSIndexNav(string id)
        {
            List<Nav> littlelist = NavDal.GetSIndexNav(id);
            return littlelist;
        } 
        #endregion
    }
}
