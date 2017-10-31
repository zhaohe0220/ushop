using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bhtz.Controllers
{
    public class StaticController : Controller
    {
        //关于我们
        public ActionResult about()
        {
            return View();
        }
        //反馈
        public ActionResult Backfeed()
        {
            return View();
        }
        //免责
        public ActionResult mianze()
        {
            return View();
        }
       //联系我们
        public ActionResult lianxi()
        {
            return View();
        }

        public ActionResult fwxy()
        {
            return View();
        }

        public ActionResult href()
        {
            return View();
        }

         [ValidateInput(false)]
        public ActionResult AddAdvise(AdviseInfo adviseInfo)
        {
            adviseInfo.ReleaseTime = DateTime.Now;
            BLL.AdviseServices AdviseServices = new BLL.AdviseServices();
            if (AdviseServices.AddAdvise(adviseInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
    }
}
