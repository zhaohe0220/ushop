using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bhtz.Controllers
{
    public class NeedController :Controller
    {

        BLL.NeedServices NeedServices = new BLL.NeedServices();
       

        #region 需求列表
        public ActionResult NeedList()
        {
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
            int pageSize = 30;
            int pageCount = NeedServices.GetNeedPageCount(pageSize);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<NeedInfo> needInfo = NeedServices.GetNeedList(pageIndex, pageSize);
            ViewData["needlist"] = needInfo;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            return View();
        } 
        #endregion


       
        #region 需求详细
        public ActionResult NeedDetail()
        {
            string nid = Request["nid"];
            List<NeedInfo> Needdetail = NeedServices.GetNeedDetail(nid);
            ViewData["Needdetail"] = Needdetail;
            return View();
        } 
        #endregion
      

    }
}
