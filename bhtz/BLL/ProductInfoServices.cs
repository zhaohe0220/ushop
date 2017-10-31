using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductInfoServices
    {
        DAL.ProductDal ProductDal = new ProductDal();



        #region 发布商品
        public bool AddMessage(ProductInfo ProductInfo)
        {
            return ProductDal.AddMessage(ProductInfo) > 0;
        } 
        #endregion


        #region 用户删除商品
        public bool DeleteProduct(string deleteid)
        {
            return ProductDal.DeleteProduct(deleteid) > 0;
        }
        #endregion



        #region 管理员审核商品
        public bool shenheProduct(string shenheid,string temp)
        {
            return ProductDal.shenheProduct(shenheid, temp) > 0;
        }
        #endregion




        /// <summary>
        /// ALL
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region 读取全部商品
        public List<ProductInfo> GetAllProductList()
        {
            List<ProductInfo> productList = ProductDal.GetAllProductList();
            return productList;
        } 
        #endregion





        /// <summary>
        /// 大类分页信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        #region 读取大类
        public List<ProductInfo> GetBList(string id, int pageIndex, int pageSize)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            List<ProductInfo> list = ProductDal.GetBList(id, start, end);
            return list;
        } 
        #endregion



        /// <summary>
        /// 小类分页
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        #region 读取小类
        public List<ProductInfo> GetSList(string id, int pageIndex, int pageSize)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            List<ProductInfo> list = ProductDal.GetSList(id, start, end);
            return list;
        } 
        #endregion



        /// <summary>
        /// searchlist
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        #region 搜索列表
        public List<ProductInfo> GetSearchList(string id, int pageIndex, int pageSize)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            List<ProductInfo> list = ProductDal.GetCSSearchList(id, start, end);
            return list;
        } 
        #endregion


        #region 读取大类
        public List<ProductInfo> GetCenterList(string userid, int pageIndex, int pageSize)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            List<ProductInfo> Centerlist = ProductDal.GetCenterList(userid, start, end);
            return Centerlist;
        }
        #endregion




        #region 大类分页
        public int GetBPageCount(string id, int pageSize)
        {
            int recordCount = ProductDal.GetBRecordCount(id);
            int pageCount = Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));
            return pageCount;
        } 
        #endregion



        #region 管理员页面商品
        public List<ProductInfo> GlyGetList(int pageIndex, int pageSize)
        {
            List<ProductInfo> productList = ProductDal.GlyGetList(pageIndex,pageSize);
            return productList;
        }
        #endregion



        #region 管理员页面商品分页
        public int GlyGetPageCount(int pageSize)
        {
            int recordCount = ProductDal.GlyGetPageCount();
            int pageCount = Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));
            return pageCount;
        }
        #endregion



        #region 小类分页
        public int GetSPageCount(string id, int pageSize)
        {
            int recordCount = ProductDal.GetSRecordCount(id);
            int pageCount = Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));
            return pageCount;
        } 
        #endregion



        #region 搜索分页
        public int GetSearchPageCount(string id, int pageSize)
        {
            int recordCount = ProductDal.GetSearchRecordCount(id);
            int pageCount = Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));
            return pageCount;
        } 
        #endregion



        #region 个人中心分页
        public int GetCenterPageCount(string userid, int pageSize)
        {
            int recordCount = ProductDal.GetCenterRecordCount(userid);
            int pageCount = Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));
            return pageCount;
        }
        #endregion




        /// <summary>
        /// 详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region 读取商品详细信息
        public List<ProductInfo> GetDetail(string proid)
        {
            List<ProductInfo> detaillist = ProductDal.GetDetail(proid);
            return detaillist;
        } 
        #endregion



        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="ProductInfo"></param>
        /// <returns></returns>
        #region 更新商品
        public bool UpdateMessage(ProductInfo ProductInfo)
        {
            return ProductDal.UpdateMessage(ProductInfo) > 0;
        } 
        #endregion



        /// <summary>
        /// 读取商品详细页与商品同类热销商品
        /// </summary>
        /// <returns></returns>
        #region 详细页面同类热销商品
        public List<ProductInfo> GetHotList(string proid)
        {
            List<ProductInfo> productList = ProductDal.GetHotList(proid);
            return productList;
        }
        #endregion
        //获取卖家其他商品
        public List<ProductInfo> GetOther(string id, int pageIndex, int pageSize)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            List<ProductInfo> list = ProductDal.GetOther(id, start, end);
            return list;
        }
        #region 分页
        public int GetOtherCount(string id, int pageSize)
        {
            int recordCount = ProductDal.GetOtherCount(id);
            int pageCount = Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));
            return pageCount;
        }
        #endregion
        //更新收藏数量
        public bool updateCollection(string id)
        {
            return ProductDal.updateCollection(id)>0;
        }

        public List<Model.ProductInfo> getCollectionList(string collectionId)
        {
            List<ProductInfo> collectionList = ProductDal.getCollectionList(collectionId);
            return collectionList;
        }
    }
}
