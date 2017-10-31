using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bhtz.Controllers
{
    public class GlyController :Controller
    {
        public ActionResult Glylogin()
        {
            return View();
        }


   

        public ActionResult GlyPro()
        {
            if (Session["glyInfo"] == null)
            {
              Response.Redirect("/Gly/Glylogin");
            }
            BLL.ProductInfoServices ProductInfoServices = new BLL.ProductInfoServices();
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
            int pageSize = 30;
            int pageCount = ProductInfoServices.GlyGetPageCount(pageSize);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<ProductInfo> GlyList = ProductInfoServices.GlyGetList(pageIndex, pageSize);
            ViewData["GlyList"] = GlyList;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            return View();
        }



        public ActionResult GlyAdd()
        {
            if (Session["glyInfo"] == null)
            {
                Response.Redirect("/Gly/Glylogin");
            }
            return View();
        }


        #region 添加管理员
        [ValidateInput(false)]
        public ActionResult Addgly(GlyInfo glyInfo)
        {
            BLL.GlyServices GlyServices = new BLL.GlyServices();
            if (GlyServices.Addgly(glyInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        } 
        #endregion



        public ActionResult Allgly()
        {
            if (Session["glyInfo"] == null)
            {
                Response.Redirect("/Gly/Glylogin");
            }
            BLL.GlyServices GlyServices = new BLL.GlyServices();
            List<GlyInfo> glyInfo = GlyServices.GetGlyList();
            ViewData["GlyInfo"] = glyInfo;
            return View();
        }


        public ActionResult Updategly()
        {
            if (Session["glyInfo"] == null)
            {
                Response.Redirect("/Gly/Glylogin");
            }
            int glyid = Convert.ToInt32(Request["glyid"]);
            Session["glyid"] = glyid;
            BLL.GlyServices GlyServices = new BLL.GlyServices();
            List<GlyInfo> oneglyInfo = GlyServices.GetOneGlyList(glyid);
            ViewData["OneGlyInfo"] = oneglyInfo;
            return View();
        }

          [ValidateInput(false)]
        public ActionResult Glyupdategly(GlyInfo glyInfo)
        {
            glyInfo.AddTime = DateTime.Now;
            glyInfo.AdminID = Convert.ToInt32(Session["glyid"]);
            BLL.GlyServices GlyServices = new BLL.GlyServices();
            if (GlyServices.Glyupdategly(glyInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
       
        }



          #region 删除管理员
          public ActionResult DeleteGly()
          {
              int deleteglyid = Convert.ToInt32(Request["id"]);
              BLL.GlyServices GlyServices = new BLL.GlyServices();
              if (GlyServices.DeleteGly(deleteglyid))
              {
                  return Content("ok");
              }
              else
              {
                  return Content("no");
              }
          } 
          #endregion



        #region 登录
        public ActionResult LoginGly()
        {
            string userName = Request["glyLoginCode"];
            string userPwd = Request["glyLoginPwd"];
            if (string.IsNullOrEmpty(userName))
            {
                return Content("no:请输入用户名！");
            }
            else if (string.IsNullOrEmpty(userPwd))
            {
                return Content("no:密码不能为空！");
            }
            else
            {
                string code = Session["code"] == null ? string.Empty : Session["code"].ToString();
                if (string.IsNullOrEmpty(code))
                {
                    return Content("no:验证码不能为空！");
                }
                Session["code"] = null;
                string txtcode = Request["vcode"];
                if (!code.Equals(txtcode, StringComparison.InvariantCultureIgnoreCase))
                {
                    return Content("no:验证码错误！");
                }
            }
            BLL.GlyServices GlyServices = new BLL.GlyServices();
            GlyInfo glyInfo = GlyServices.GetGlyInfo(userName, userPwd);
            if (glyInfo != null)
            {
                Session["glyInfo"] = glyInfo.AdminName;
                return Content("ok:");
            }
            else
            {
                return Content("no:用户名或密码错误！");
            }
        }
        #endregion



        #region 审核商品
        public ActionResult shenheProduct()
        {
            string shenheid = Request["id"].ToString();
            string temp = Request["temp"].ToString();
            BLL.ProductInfoServices ProductInfoServices = new BLL.ProductInfoServices();
            if (ProductInfoServices.shenheProduct(shenheid,temp))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion



        #region 管理员需求列表
        public ActionResult GlyNeed()
        {
            if (Session["glyInfo"] == null)
            {
                Response.Redirect("/Gly/Glylogin");
            }
            BLL.NeedServices NeedServices = new BLL.NeedServices();
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
            int pageSize = 30;
            int pageCount = NeedServices.GetGlyNeedPageCount(pageSize);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<NeedInfo> GlyNeed = NeedServices.GetGlyNeedList(pageIndex, pageSize);
            ViewData["GlyNeed"] = GlyNeed;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            return View();
        } 
        #endregion



        #region 审核需求
        public ActionResult shenheNeed()
        {
            string shenheid = Request["id"].ToString();
            string temp = Request["temp"].ToString();
            BLL.NeedServices NeedServices = new BLL.NeedServices();
            if (NeedServices.shenheNeed(shenheid, temp))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion



        public ActionResult Publishgg()
        {
            if (Session["glyInfo"] == null)
            {
                Response.Redirect("/Gly/Glylogin");
            }
            return View();
        }


         [ValidateInput(false)]
        public ActionResult Addgg(NoticeInfo noticeInfo)
        {
            BLL.NoticeServices NoticeServices = new BLL.NoticeServices();
            if (NoticeServices.Addgg(noticeInfo))
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
