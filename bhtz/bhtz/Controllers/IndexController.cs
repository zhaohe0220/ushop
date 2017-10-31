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
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Net.Mail;
using System.Web.Mail;

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
        //用户注册功能
        public ActionResult UserRegister()
        {
            string iss= Request["check"];
            BLL.UserInfoService UserInfoService = new BLL.UserInfoService();
            string name = Request["username"].ToString().Trim();
            string email = Request["email"].ToString().Trim();
            string pwd = Request["LoginPwd"].ToString().Trim();
            string okpwd = Request["LoginPwdok"].ToString().Trim();
            string state = "0";
            string emailStr = @"([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,5})+";
            //邮箱正则表达式对象
            Regex emailReg = new Regex(emailStr);
            if (string.IsNullOrEmpty(name))
            {
                return Content("no0:用户名不能为空！");
            }
            else if (!emailReg.IsMatch(email))
            {
                return Content("no1:邮箱格式不正确！");
            }
            else if (string.IsNullOrEmpty(email))
            {
                return Content("no2:邮箱不能为空！");
            }
            else if(string.IsNullOrEmpty(pwd))
            {
                return Content("no3:密码不能为空！");
            }
            else if(string.IsNullOrEmpty(okpwd))
            {
                return Content("no4:密码不能为空！");
            }
            else if(pwd != okpwd)
            {
                return Content("no5:两次密码不相同！");
            }
            else
            {
                string code = Session["code"] == null ? string.Empty : Session["code"].ToString();
                if (string.IsNullOrEmpty(code))
                {
                    return Content("no6:验证码不能为空！");
                }
                Session["code"] = null;
                string txtcode = Request["vCode"];
                if (!code.Equals(txtcode, StringComparison.InvariantCultureIgnoreCase))
                {
                    return Content("no7:验证码错误！");
                }
            }
            
            UserInfo userInfo = new UserInfo();
            userInfo.Name = name;
            userInfo.Pwd = pwd;
            userInfo.Email = email;
            userInfo.state = state;
            if (UserInfoService.checkName(name)>0)
            {
                return Content("no8:用户名已存在！");
            }
           else if (UserInfoService.UserRegister(userInfo))
           {
                if (sendEMail(email, name))
                {

                    return Content("ok:ok");
                }
                else
                {
                    return Content("no:系统错误！");
                }
            }
            else
            {
                return Content("no:系统错误！");
            }
        }

        //发送邮件
       public bool sendEMail(string Email,string username)
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.From = new System.Net.Mail.MailAddress("postmaster@bingowang.com");    //发送邮件邮箱
            message.To.Add(Email);   //收件人
            message.Subject = "滨购网";    //邮件标题
            message.IsBodyHtml = true;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            string activecode = Guid.NewGuid().ToString().Substring(0, 8);
            //Session["name"] = username;
            StringBuilder content = new StringBuilder();
            content.Append("亲爱的用户，感谢您注册滨购网。");
            content.Append("请单击激活完成注册");
            content.Append("<a href='http://www.bingowang.com/Index/CheckSuccess?activecode=" + activecode + "&checkuser=" + username + "'>激活</a>");
            message.Body = content.ToString();     //邮件内容
            message.IsBodyHtml = true;
            message.Priority = System.Net.Mail.MailPriority.Normal;
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.mxhichina.com", 25);   //qq的邮件地址，25端口号
            client.Credentials = new System.Net.NetworkCredential("postmaster@bingowang.com", "456123789.Zh");                  //第一个参数发件邮箱名，第二个参数邮箱密码

           // client.EnableSsl = true; //必须经过ssl加密 
            try
            {
                client.Send(message);      //发送
                return true;
            }
            catch (Exception ee)
            {
                return false;
            }
        }
        //发送邮件

            public ActionResult EmailCheck()
        {
            return View();
        }
        public ActionResult CheckSuccess()
        {
            string checkuser = Request["checkuser"].ToString();
            Session["userInfo"] = checkuser;
            if (checkuser != null)
            {
                BLL.UserInfoService UserInfoService = new UserInfoService();
                UserInfoService.updateState(checkuser);
            }
            
                return View();
        }

        [ValidateInput(false)]
        public ActionResult addUserMes(UserInfo userinfo)
        {
            string checkuser = Session["userInfo"].ToString();
            userinfo.Name = checkuser;
            BLL.UserInfoService userInfoService = new UserInfoService();
            if (userInfoService.addUserMes(userinfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }


        public ActionResult AllType()
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
            //string url = Request.UrlReferrer.ToString();
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
                string txtcode = Request["vCode"];
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
                Session["userid"] = userInfo.Id;
                return Content("ok:");
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
