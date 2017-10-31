using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bhtz.Controllers
{
    public class BaseController : Controller
    {
       //继承basecontroller,控制用户是否登录
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["userInfo"] == null)
            {
                filterContext.HttpContext.Response.Redirect("/Index/Login");
            }
            base.OnActionExecuting(filterContext);
        }
       

    }
}
