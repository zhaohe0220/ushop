using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using Model;
using DAL;

namespace bhtz
{
    /// <summary>
    /// DataWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class DataWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        //读取所有数据
        [WebMethod(Description = "获取所有信息")]
        public List<Nav> get()
        {
            string id = "0";
            BLL.NavServices NavServices = new BLL.NavServices();
            List<Nav> typelist = NavServices.GetNav(id);
            return typelist;
        }
        //登录
        [WebMethod(Description = "登录")]
        public void Login()
        {
            string userName =Context.Request["name"].ToString();
            string userPwd = Context.Request["pwd"].ToString();
            BLL.UserInfoService UserInfoService = new BLL.UserInfoService();
            UserInfo userInfo = UserInfoService.GetUserInfo(userName, userPwd);
            if (userInfo != null)
            {
                Context.Response.Write("{\"Result\":\"1\"}");
            }
            else
            {
                Context.Response.Write("{\"Result\":\"0\"}");
            }
        }


    }
}
