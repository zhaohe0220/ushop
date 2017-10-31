using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace bhtz.Controllers
{
    public class AdminController : BaseController //Controller
    {

        #region 读取个人中心商品
        public ActionResult CenterPro()
        {
            string userid = Session["userInfo"].ToString();
            BLL.ProductInfoServices ProductInfoServices = new BLL.ProductInfoServices();
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
            int pageSize = 15;
            int pageCount = ProductInfoServices.GetCenterPageCount(userid, pageSize);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<ProductInfo> CenterList = ProductInfoServices.GetCenterList(userid, pageIndex, pageSize);
            ViewData["CenterList"] = CenterList;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            ViewData["id"] = userid;
            return View();
        } 
        #endregion


        #region 删除个人中心商品
        public ActionResult DeleteProduct()
        {
            string deleteid = Request["id"].ToString();
            BLL.ProductInfoServices ProductInfoServices = new BLL.ProductInfoServices();
            if (ProductInfoServices.DeleteProduct(deleteid))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion


        #region 个人中心需求列表
        public ActionResult CenterNeed()
        {
            string needuserid = Session["userInfo"].ToString();
            BLL.NeedServices NeedServices = new BLL.NeedServices();
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
            int pageSize = 30;
            int pageCount = NeedServices.GetCenterNeedPageCount(needuserid, pageSize);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<NeedInfo> CenterNeed = NeedServices.GetCenterNeedList(needuserid, pageIndex, pageSize);
            ViewData["CenterNeed"] = CenterNeed;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            ViewData["id"] = needuserid;
            return View();
        } 
        #endregion

        #region 删除个人中心需求
        public ActionResult DeleteNeed()
        {
            int deleteid = Convert.ToInt32(Request["id"]);
            BLL.NeedServices NeedServices = new BLL.NeedServices();
            if (NeedServices.DeleteNeed(deleteid))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        
        #endregion

    
        public ActionResult publish()
        {
            return View();
        }



        #region 读取大类
        public ActionResult dalei()
        {
            string id = "0";
            BLL.NavServices NavServices = new BLL.NavServices();
            List<Nav> list = NavServices.GetNav(id);
            ViewData["list"] = list;
            return View();
        } 
        #endregion



        /// <summary>
        /// 获取小类
        /// </summary>
        /// <returns></returns>
        #region 获取小类
        public ActionResult xiaolei()
        {
            string did = Request["did"];
            Session["did"] = did;
            BLL.NavServices NavServices = new BLL.NavServices();
            List<Nav> list = NavServices.GetNav(did);
            ViewData["littlelist"] = list;
            return View();
        } 
        #endregion



        #region 发布信息页面
        public ActionResult addmes()
        {
            string xid = Request["littleid"];
            Session["xid"] = xid;
            return View();
        } 
        #endregion



        #region 更新商品页面
        public ActionResult Update()
        {
            string updateid = Request["updateid"];
            Session["updateid"] = updateid;
            BLL.ProductInfoServices ProductInfoServices = new BLL.ProductInfoServices();
            List<ProductInfo> updatelist = ProductInfoServices.GetDetail(updateid);
            ViewData["update"] = updatelist;
            return View();
        } 
        #endregion



        #region 图片上传
        public ActionResult FileUpLoad()
        {
            HttpPostedFileBase postFile = Request.Files["fileUp"];
            if (postFile == null)
            {
                return Content("no:请选择文件");
            }
            else
            {
                string fileName = Path.GetFileName(postFile.FileName);
                string fileExt = Path.GetExtension(fileName);
                if (fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".png" || fileExt == ".bmp")
                {
                    string dir = "/ImagePath/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                    Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));//创建文件夹
                    string newfileName = Guid.NewGuid().ToString();//新文件夹名
                    string fullDir = dir + newfileName + fileExt;//完整文件名
                    postFile.SaveAs(Request.MapPath(fullDir));//保存文件
                    return Content("ok:" + fullDir);
                }
                else
                {
                    return Content("no:文件格式错误");
                }
            }
        }
        #endregion



        #region 发布信息
        [ValidateInput(false)]
        public ActionResult AddMessage(ProductInfo productinfo)
        {
            
            productinfo.UserID = Session["userInfo"].ToString();
            productinfo.ProductID = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            productinfo.Btype = Session["did"].ToString();
            productinfo.Stype = Session["xid"].ToString();
            BLL.ProductInfoServices ProductInfoServices = new BLL.ProductInfoServices();
            if (ProductInfoServices.AddMessage(productinfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion



        #region 更新商品
        [ValidateInput(false)]
        public ActionResult UpdateMessage(ProductInfo productinfo)
        {
            productinfo.State = "0";
            productinfo.ProductID = Session["updateid"].ToString();
            productinfo.ReleaseTime = DateTime.Now;
            BLL.ProductInfoServices ProductInfoServices = new BLL.ProductInfoServices();
            if (ProductInfoServices.UpdateMessage(productinfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        } 
        #endregion



         public ActionResult NeedPublish()
         {
             return View();
         }
       


         #region 发布需求
         [ValidateInput(false)]
         public ActionResult AddNeed(NeedInfo needinfo)
         {
             needinfo.NeedsID = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
             needinfo.UserID = Session["userInfo"].ToString();
             BLL.NeedServices NeedServices = new BLL.NeedServices();
             if (NeedServices.AddNeed(needinfo))
             {
                 return Content("ok");
             }
             else
             {
                 return Content("no");
             }
         } 
         #endregion



         #region 需求更新
         [ValidateInput(false)]
         public ActionResult NeedUpdate(NeedInfo needinfo)
         {
             needinfo.ReleaseTime = DateTime.Now;
             needinfo.Id = Convert.ToInt32(Session["needupdateid"]);
             needinfo.State = "0";
             BLL.NeedServices NeedServices = new BLL.NeedServices();
             if (NeedServices.NeedUpdate(needinfo))
             {
                 return Content("ok");
             }
             else
             {
                 return Content("no");
             }
         } 
         #endregion


         #region 更新需求页面
         public ActionResult UpdateNeed()
         {
             string needupdateid = Request["nid"];
             Session["needupdateid"] = needupdateid;
             BLL.NeedServices NeedServices = new BLL.NeedServices();
             List<NeedInfo> Needdetail = NeedServices.GetNeedDetail(needupdateid);
             ViewData["Needdetail"] = Needdetail;
             return View();
         } 
         #endregion


         public ActionResult CenterPwd()
         {
             return View();
         }

         public ActionResult zl()
         {
             string id = Session["userInfo"].ToString();
             BLL.UserInfoService UserInfoService = new BLL.UserInfoService();
             List<UserInfo> userlist = UserInfoService.GetUser(id);
             ViewData["userlist"] = userlist;
             return View();
         }


          [ValidateInput(false)]
         public ActionResult updatepwd(UserInfo userinfo)
         {
              string oldpwd=Request["OldPwd"].ToString();
             string pwd = Session["pwd"].ToString();
             if (oldpwd != pwd)
             {
                 return Content("false");
             }
             userinfo.Name = Session["userInfo"].ToString();
             BLL.UserInfoService UserInfoService = new BLL.UserInfoService();
             if (UserInfoService.updatepwd(userinfo))
             {
                 return Content("ok");
             }
             else
             {
                 return Content("no");
             }
         }


         [ValidateInput(false)]
          public ActionResult updatemes(UserInfo userinfo)
          {
              userinfo.Name = Session["userInfo"].ToString();
              BLL.UserInfoService UserInfoService = new BLL.UserInfoService();
              if (UserInfoService.updatemes(userinfo))
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
