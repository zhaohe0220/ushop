using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bhtz.Controllers
{
    public class ListController : Controller
    {
        BLL.ProductInfoServices ProductInfoServices = new BLL.ProductInfoServices();
        BLL.NavServices NavServices = new BLL.NavServices();


        /// <summary>
        /// 大类列表
        /// </summary>
        /// <returns></returns>
        #region 大类列表页
        public ActionResult Index()
        {
            string id = Request["id"];
            Session["indexid"] = id;
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
            int pageSize = 50;
            int pageCount = ProductInfoServices.GetBPageCount(id, pageSize);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<ProductInfo> list = ProductInfoServices.GetBList(id, pageIndex, pageSize);
            ViewData["list"] = list;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            ViewData["id"] = id;
            //读取类
            List<Nav> about = NavServices.GetNav(id);
            ViewData["about"] = about;
            return View();
        } 
        #endregion



        /// <summary>
        /// 小类列表
        /// </summary>
        /// <returns></returns>
        #region 小类商品页
        public ActionResult SIndex()
        {
            string id = Request["id"];
            Session["id"] = id;
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
            int pageSize = 50;
            int pageCount = ProductInfoServices.GetSPageCount(id, pageSize);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<ProductInfo> list = ProductInfoServices.GetSList(id, pageIndex, pageSize);
            ViewData["list"] = list;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            ViewData["id"] = id;
            //读取类
            List<Nav> about = NavServices.GetSIndexNav(id);
            ViewData["about"] = about;
            return View();
        } 
        #endregion



        #region 搜索列表页
        public ActionResult Search()
        {
            string id = Request["id"];
            Session["id"] = id;
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
            int pageSize = 50;
            int pageCount = ProductInfoServices.GetSearchPageCount(id, pageSize);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<ProductInfo> list = ProductInfoServices.GetSearchList(id, pageIndex, pageSize);
            ViewData["list"] = list;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            ViewData["id"] = id;
            return View();
        } 
        #endregion



        #region 商品详细信息页
        public ActionResult Detail()
        {
            string proid = Request["proid"];
            List<ProductInfo> detaillist = ProductInfoServices.GetDetail(proid);
            ViewData["detaillist"] = detaillist;
            //读取商品详细页与商品同类热销商品
            List<ProductInfo> productList = ProductInfoServices.GetHotList(proid);
            ViewData["plist"] = productList;
            return View();
        } 
        #endregion
        //查看卖家其他商品
     public ActionResult Other()
        {
            string otherid = Request["id"].ToString();
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
            int pageSize = 50;
            int pageCount = ProductInfoServices.GetOtherCount(otherid, pageSize);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<ProductInfo> otherlist = ProductInfoServices.GetOther(otherid,pageIndex,pageSize);
            ViewData["otherlist"] = otherlist;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            ViewData["otherid"] = otherid;
            return View();
        }
     

    }
}
