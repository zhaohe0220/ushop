using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Net.Http;
using BLL;
using Common;
using Model;
using System.Web.Http;
using System.Web.Security;

namespace bhtz.Controllers
{
   


    public class IndexController : Controller
    {
        #region 首页 读取类别 读取商品 读取公告 
        public ActionResult Index()
        {
            //类型
            string id = "0";
            BLL.NavServices NavServices = new BLL.NavServices();
            List<Nav> typelist = NavServices.GetNav(id);
            ViewData["list"] = typelist;
            //读取商品
            BLL.ProductInfoServices ProductInfoServices = new BLL.ProductInfoServices();
            List<ProductInfo> productList = ProductInfoServices.GetAllProductList();
            ViewData["plist"] = productList;
            //读取公告
            BLL.NoticeServices NoticeServices = new BLL.NoticeServices();
            List<NoticeInfo> noticeInfo = NoticeServices.GetNotice();
            ViewData["Notice"] = noticeInfo;
            return View();
        } 
        #endregion

        public ActionResult Login()
        {
            return View();
           
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult AllType()
        {
            return View();
        }
        public ActionResult exam()
        {
            return View();
        }

        public ActionResult usercenter(){
            return View();
        }


        #region 验证码
        public ActionResult ShowCode()
        {
            Common.ValidateCode validateCode = new Common.ValidateCode();
            string code = validateCode.CreateValidateCode(4);//获取验证码
            Session["code"] = code;
            byte[] buffer = validateCode.CreateValidateGraphic(code);
            return File(buffer, "image/jpeg");
        } 
        #endregion



        #region 登录
        public ActionResult UserLogin()
        {
            string url = Request["url"];
            string userName = Request["LoginCode"];
            string userPwd = Request["LoginPwd"];
            //获取要加密的字段，并转化为Byte[]数组
           // byte[] data = System.Text.Encoding.Unicode.GetBytes(userPwd.ToCharArray());
            //建立加密服务  
          //  System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            //加密Byte[]数组 
          //  byte[] result = md5.ComputeHash(data);
            //将加密后的数组转化为字段  
            //string Pwd = System.Text.Encoding.Unicode.GetString(result);

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
            BLL.UserInfoService UserInfoService = new BLL.UserInfoService();
            UserInfo userInfo = UserInfoService.GetUserInfo(userName, userPwd);
            if (userInfo != null)
            {
                Session["pwd"] = userInfo.Pwd;
                Session["userInfo"] = userInfo.Name;
                return Content("ok:"+url);
            }
            else
            {
                return Content("no:用户名或密码错误！");
            }
        }
        #endregion


        public ActionResult userlogout()
        {
            Session.Abandon();
                return Content("ok");
          
        }
        
            


    }
}
