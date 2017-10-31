using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bhtz.Controllers
{
    public class NoticeController : Controller
    {
        BLL.NoticeServices NoticeServices = new BLL.NoticeServices();

        #region 公告列表
        public ActionResult List()
        {
            List<NoticeInfo> noticeInfo = NoticeServices.GetNotice();
            ViewData["Notice"] = noticeInfo;
            return View();
        } 
        #endregion


        #region 公告详细
        public ActionResult Detail()
        {
            string nid = Request["nid"];
            List<NoticeInfo> Ndetail = NoticeServices.GetNoticeDetail(nid);
            ViewData["Notice"] = Ndetail;
            return View();
        } 
        #endregion

    }
}
