using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ProductDal
    {
        #region 插入商品
        public int AddMessage(ProductInfo ProductInfo)
        {
            string sql = "insert into T_Product(UserID,ProductID,Btype,Stype,ProductName,ProductIntroduce,ProductImageAdd,Price,Phone,ProductNum,Address,QQ,Collection)values(@UserID,@ProductID,@Btype,@Stype,@ProductName,@ProductIntroduce,@ProductImageAdd,@Price,@Phone,@ProductNum,@Address,@QQ,@Collection)";
            SqlParameter[] pars ={
                                    new SqlParameter("@UserId",SqlDbType.VarChar,20),
                                    new SqlParameter("@ProductID",SqlDbType.Char,20),
                                    new SqlParameter("@Btype",SqlDbType.Char,2),
                                    new SqlParameter("@Stype",SqlDbType.Char,4),
                                    new SqlParameter("@ProductName",SqlDbType.NVarChar,50),
                                    new SqlParameter("@ProductIntroduce",SqlDbType.Text),
                                    new SqlParameter("@ProductImageAdd",SqlDbType.VarChar,200),
                                    new SqlParameter("@Price",SqlDbType.Char,10),
                                    new SqlParameter("@Phone",SqlDbType.VarChar,20),
                                    new SqlParameter("@ProductNum",SqlDbType.Char,10),
                                    new SqlParameter("@Address",SqlDbType.NVarChar,50),
                                    new SqlParameter("@QQ",SqlDbType.VarChar,20),
                                    new SqlParameter("@Collection",SqlDbType.Int)
                                };
            pars[0].Value = ProductInfo.UserID;
            pars[1].Value = ProductInfo.ProductID;
            pars[2].Value = ProductInfo.Btype;
            pars[3].Value = ProductInfo.Stype;
            pars[4].Value = ProductInfo.ProductName;
            pars[5].Value = ProductInfo.ProductIntroduce;
            pars[6].Value = ProductInfo.ProductImageAdd;
            pars[7].Value = ProductInfo.Price;
            pars[8].Value = ProductInfo.Phone;
            pars[9].Value = ProductInfo.ProductNum;
            pars[10].Value = ProductInfo.Address;
            if (ProductInfo.QQ == null)
            {
                pars[11].Value = "";
            }
            else { pars[11].Value = ProductInfo.QQ; }
            pars[12].Value = 0;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        }
        #endregion


        #region 用户删除商品
        public int DeleteProduct(string deleteid)
        {
            string sql = "delete from T_Product where ProductID=@deleteid";
            SqlParameter[] pars ={
                                     new SqlParameter("@deleteid",SqlDbType.Char,20)
                                 };
            pars[0].Value = deleteid;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        }
        #endregion



        #region 更新商品
        public int UpdateMessage(ProductInfo ProductInfo)
        {
            string sql = "update T_Product set ProductName=@ProductName,ProductIntroduce=@ProductIntroduce,ProductImageAdd=@ProductImageAdd,Price=@Price,ReleaseTime=@ReleaseTime,Phone=@Phone,ProductNum=@ProductNum,Address=@Address,QQ=@QQ,State=@State where ProductID=@ProductID";
            SqlParameter[] pars ={
                                    new SqlParameter("@ProductName",SqlDbType.NVarChar,50),
                                    new SqlParameter("@ProductIntroduce",SqlDbType.Text),
                                    new SqlParameter("@ProductImageAdd",SqlDbType.VarChar,200),
                                    new SqlParameter("@Price",SqlDbType.Char,10),
                                    new SqlParameter("@ReleaseTime",SqlDbType.DateTime),
                                    new SqlParameter("@Phone",SqlDbType.VarChar,20),
                                    new SqlParameter("@ProductNum",SqlDbType.Char,10),
                                    new SqlParameter("@Address",SqlDbType.NVarChar,50),
                                    new SqlParameter("@QQ",SqlDbType.VarChar,20),
                                      new SqlParameter("@State",SqlDbType.Char,2),
                                     new SqlParameter("@ProductID",SqlDbType.Char,20)
                                };
            pars[0].Value = ProductInfo.ProductName;
            pars[1].Value = ProductInfo.ProductIntroduce;
            pars[2].Value = ProductInfo.ProductImageAdd;
            pars[3].Value = ProductInfo.Price;
            pars[4].Value = ProductInfo.ReleaseTime;
            pars[5].Value = ProductInfo.Phone;
            pars[6].Value = ProductInfo.ProductNum;
            pars[7].Value = ProductInfo.Address;
            if (ProductInfo.QQ == null)
            {
                pars[8].Value = "";
            }
            else { pars[8].Value = ProductInfo.QQ; }
            pars[9].Value = ProductInfo.State;
            pars[10].Value = ProductInfo.ProductID;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        }
        #endregion



        #region 读取全部商品
        public List<ProductInfo> GetAllProductList()
        {
            string sql = "select * from T_Product where State='1' order by ReleaseTime desc";
            //SqlParameter[] pars ={
            //                         new SqlParameter("@pid",SqlDbType.Char,2)
            //                    };
            //pars[0].Value = pid;
            DataTable da = SqlHelper.Get(sql, CommandType.Text);
            List<ProductInfo> productList = null;
            if (da.Rows.Count > 0)
            {
                productList = new List<ProductInfo>();

                foreach (DataRow row in da.Rows)
                {
                    ProductInfo productInfo = null;
                    productInfo = new ProductInfo();
                    LoadEntity(productInfo, row);
                    productList.Add(productInfo);
                }
            }
            return productList;
        }
        #endregion



        #region 管理员全部商品
        public List<ProductInfo> GlyGetList(int start, int end)
        {
            string sql = "select * from (select row_number() over(order by ReleaseTime desc) as num,* from T_Product where State='0') as t where t.num>=@start and t.num<=@end ";
            SqlParameter[] pars ={
                                     new SqlParameter("@start",SqlDbType.Int),
                                     new SqlParameter ("@end",SqlDbType.Int)

                                 };

            pars[0].Value = start;
            pars[1].Value = end;

            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            List<ProductInfo> list = null;
            if (da.Rows.Count > 0)
            {
                list = new List<ProductInfo>();
                ProductInfo productInfo = null;
                foreach (DataRow row in da.Rows)
                {
                    productInfo = new ProductInfo();
                    LoadEntity(productInfo, row);
                    list.Add(productInfo);
                }
            }
            return list;
        }
        #endregion



        #region 管理员审核商品
        public int shenheProduct(string shenheid, string temp)
        {
            string sql = null;
            if (temp == "1")
            {
                sql = "update T_Product set State='1' where ProductID=@shenheid";
            }
            else
            {
                sql = "update T_Product set State='2' where ProductID=@shenheid";
            }

            SqlParameter[] pars ={
                                     new SqlParameter("@shenheid",SqlDbType.Char,20)
                                 };
            pars[0].Value = shenheid;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        }
        #endregion




        #region 关系转对象
        public void LoadEntity(ProductInfo productInfo, DataRow row)
        {
            productInfo.Id = Convert.ToInt32(row["Id"]);
            productInfo.UserID = row["UserID"] != DBNull.Value ? row["UserID"].ToString() : string.Empty;
            productInfo.ProductID = row["ProductID"] != DBNull.Value ? row["ProductID"].ToString() : string.Empty;
            productInfo.Btype = row["Btype"] != DBNull.Value ? row["Btype"].ToString() : string.Empty;
            productInfo.Stype = row["Stype"] != DBNull.Value ? row["Stype"].ToString() : string.Empty;
            productInfo.ProductName = row["ProductName"] != DBNull.Value ? row["ProductName"].ToString() : string.Empty;
            productInfo.ProductIntroduce = row["ProductIntroduce"] != DBNull.Value ? row["ProductIntroduce"].ToString() : string.Empty;
            productInfo.ProductImageAdd = row["ProductImageAdd"] != DBNull.Value ? row["ProductImageAdd"].ToString() : string.Empty;
            productInfo.Price = row["Price"] != DBNull.Value ? row["Price"].ToString() : string.Empty;
            productInfo.Phone = row["Phone"] != DBNull.Value ? row["Phone"].ToString() : string.Empty;
            productInfo.ProductNum = row["ProductNum"] != DBNull.Value ? row["ProductNum"].ToString() : string.Empty;
            productInfo.Address = row["Address"] != DBNull.Value ? row["Address"].ToString() : string.Empty;
            productInfo.QQ = row["QQ"] != DBNull.Value ? row["QQ"].ToString() : string.Empty;
            productInfo.ReleaseTime = Convert.ToDateTime(row["ReleaseTime"]);
            productInfo.State = row["State"] != DBNull.Value ? row["State"].ToString() : string.Empty;
            productInfo.Collection = Convert.ToInt32(row["Collection"]);
        }
        #endregion




        #region 读取大分类下的数据
        public List<ProductInfo> GetBList(string id, int start, int end)
        {

            string sql = "select * from (select row_number() over(order by ReleaseTime desc) as num,* from T_Product where Btype=@id and State='1') as t where t.num>=@start and t.num<=@end ";
            SqlParameter[] pars ={
                                     new SqlParameter("@id",SqlDbType.Char,2),
                                     new SqlParameter("@start",SqlDbType.Int),
                                     new SqlParameter ("@end",SqlDbType.Int)

                                 };
            pars[0].Value = id;
            pars[1].Value = start;
            pars[2].Value = end;

            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            List<ProductInfo> list = null;
            if (da.Rows.Count > 0)
            {
                list = new List<ProductInfo>();
                ProductInfo productInfo = null;
                foreach (DataRow row in da.Rows)
                {
                    productInfo = new ProductInfo();
                    LoadEntity(productInfo, row);
                    list.Add(productInfo);
                }
            }
            return list;
        }
        #endregion



        #region 读取小分类下的数据
        public List<ProductInfo> GetSList(string id, int start, int end)
        {

            string sql = "select * from (select row_number() over(order by ReleaseTime desc) as num,* from T_Product where Stype=@id and State='1') as t where t.num>=@start and t.num<=@end";
            SqlParameter[] pars ={
                                     new SqlParameter("@id",SqlDbType.Char,4),
                                     new SqlParameter("@start",SqlDbType.Int),
                                     new SqlParameter ("@end",SqlDbType.Int)

                                 };
            pars[0].Value = id;
            pars[1].Value = start;
            pars[2].Value = end;

            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            List<ProductInfo> list = null;
            if (da.Rows.Count > 0)
            {
                list = new List<ProductInfo>();
                ProductInfo productInfo = null;
                foreach (DataRow row in da.Rows)
                {
                    productInfo = new ProductInfo();
                    LoadEntity(productInfo, row);
                    list.Add(productInfo);
                }
            }
            return list;
        }
        #endregion




        #region 搜索结果列表
        //public List<ProductInfo> GetSearchList(string id, int start, int end)
        //{

        //    string sql = "select * from (select row_number() over(order by ReleaseTime desc) as num,* from T_Product where ProductName like @ProductName and State='1') as t where t.num>=@start and t.num<=@end ";

        //    SqlParameter[] pars ={
        //                             new SqlParameter("@ProductName",SqlDbType.NVarChar,50),
        //                             new SqlParameter("@start",SqlDbType.Int),
        //                             new SqlParameter ("@end",SqlDbType.Int)

        //                         };
        //    pars[0].Value = "%" + id + "%";
        //    pars[1].Value = start;
        //    pars[2].Value = end;

        //    DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
        //    List<ProductInfo> list = null;
        //    if (da.Rows.Count > 0)
        //    {
        //        list = new List<ProductInfo>();
        //        ProductInfo productInfo = null;
        //        foreach (DataRow row in da.Rows)
        //        {
        //            productInfo = new ProductInfo();
        //            LoadEntity(productInfo, row);
        //            list.Add(productInfo);
        //        }
        //    }
        //    return list;
        //} 
        #endregion


        #region 个人中心列表
        public List<ProductInfo> GetCenterList(string UserID, int start, int end)
        {

            string sql = "select * from (select row_number() over(order by ReleaseTime desc) as num,* from T_Product where UserID=@UserID) as t where t.num>=@start and t.num<=@end ";
            SqlParameter[] pars ={
                                     new SqlParameter("@UserID",SqlDbType.VarChar,20),
                                     new SqlParameter("@start",SqlDbType.Int),
                                     new SqlParameter ("@end",SqlDbType.Int)

                                 };
            pars[0].Value = UserID;
            pars[1].Value = start;
            pars[2].Value = end;

            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            List<ProductInfo> Centerlist = null;
            if (da.Rows.Count > 0)
            {
                Centerlist = new List<ProductInfo>();
                ProductInfo CenterproductInfo = null;
                foreach (DataRow row in da.Rows)
                {
                    CenterproductInfo = new ProductInfo();
                    LoadEntity(CenterproductInfo, row);
                    Centerlist.Add(CenterproductInfo);
                }
            }
            return Centerlist;
        }
        #endregion






        /// <summary>
        /// 大类总数
        /// </summary>
        /// <returns></returns>
        #region 大分类商品总数
        public int GetBRecordCount(string id)
        {
            string sql = "select count(*) from T_Product where Btype=@id and State='1'";
            SqlParameter[] pars ={
                                    new SqlParameter("@id",SqlDbType.Char,2)
                                };
            pars[0].Value = id;
            return Convert.ToInt32(SqlHelper.ExecuteScalare(sql, CommandType.Text, pars));
        }
        #endregion




        /// <summary>
        /// 小类总数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region 小分类商品总数
        public int GetSRecordCount(string id)
        {
            string sql = "select count(*) from T_Product where Stype=@id and State='1'";
            SqlParameter[] pars ={
                                    new SqlParameter("@id",SqlDbType.Char,4)
                                };
            pars[0].Value = id;
            return Convert.ToInt32(SqlHelper.ExecuteScalare(sql, CommandType.Text, pars));
        }
        #endregion


        #region 搜索商品总数
        public int GetSearchRecordCount(string id)
        {
            //string sql = "select count(*) from T_Product where (Stype like @Stype or Btype like @Btype or ProductName like @ProductName) and State='1'";
            //SqlParameter[] pars ={
            //                        new SqlParameter("@Stype",SqlDbType.Char,4),
            //                         new SqlParameter("@Btype",SqlDbType.Char,2),
            //                         new SqlParameter("@ProductName",SqlDbType.NVarChar,50)
            //                    };
            //pars[0].Value = "%" + id + "%";
            //pars[1].Value = "%" + id + "%";
            //pars[2].Value = "%" + id + "%";
            //return Convert.ToInt32(SqlHelper.ExecuteScalare(sql, CommandType.Text, pars));
            string sql = "select count(*) from Pro where (ProductName like @ProductName and State='1') or (BTypeName like @BTypeName and State='1') or (STypeName like @STypeName and State='1')";
            SqlParameter[] pars ={
                                     new SqlParameter("@ProductName",SqlDbType.NVarChar,50),
                                      new SqlParameter("@BTypeName",SqlDbType.NVarChar,50),
                                       new SqlParameter("@STypeName",SqlDbType.NVarChar,50)
                                };
            pars[0].Value = "%" + id + "%";
            pars[1].Value = "%" + id + "%";
            pars[2].Value = "%" + id + "%";
            return Convert.ToInt32(SqlHelper.ExecuteScalare(sql, CommandType.Text, pars));
        }
        #endregion



        #region 管理员页面商品总数
        public int GlyGetPageCount()
        {
            string sql = "select count(*) from T_Product where State='0'";
            return Convert.ToInt32(SqlHelper.ExecuteScalare(sql, CommandType.Text));
        }
        #endregion



        #region 个人中心商品总数
        public int GetCenterRecordCount(string UserID)
        {
            string sql = "select count(*) from T_Product where UserID=@UserID";
            SqlParameter[] pars ={
                                    new SqlParameter("@UserID",SqlDbType.VarChar,20)
                                };
            pars[0].Value = UserID;
            return Convert.ToInt32(SqlHelper.ExecuteScalare(sql, CommandType.Text, pars));
        }
        #endregion




        /// <summary>
        ///获取详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region 获取详细信息
        public List<ProductInfo> GetDetail(string nid)
        {
            string sql = "select * from T_Product where ProductID=@nid";
            SqlParameter[] pars ={
                                     new SqlParameter("@nid",SqlDbType.Char,20)
                                 };
            pars[0].Value = nid;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            List<ProductInfo> productlist = null;
            if (da.Rows.Count > 0)
            {
                productlist = new List<ProductInfo>();
                ProductInfo productInfo = null;
                foreach (DataRow row in da.Rows)
                {
                    productInfo = new ProductInfo();
                    LoadEntity(productInfo, row);
                    productlist.Add(productInfo);
                }
            }
            return productlist;
        }
        #endregion



        /// <summary>
        /// 读取商品详细页与商品同类热销商品
        /// </summary>
        /// <returns></returns>
        #region 读取商品详细页与商品同类热销商品
        public List<ProductInfo> GetHotList(string proid)
        {
            string sql = "select top 4 * from T_Product where Stype=(select Stype from T_Product where ProductID=@proid) and State='1' order by ReleaseTime desc";
            SqlParameter[] pars ={
                                     new SqlParameter("@proid",SqlDbType.Char,20)
                                };
            pars[0].Value = proid;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            List<ProductInfo> productList = null;
            if (da.Rows.Count > 0)
            {
                productList = new List<ProductInfo>();

                foreach (DataRow row in da.Rows)
                {
                    ProductInfo productInfo = null;
                    productInfo = new ProductInfo();
                    LoadEntity(productInfo, row);
                    productList.Add(productInfo);
                }
            }
            return productList;
        }
        #endregion



        /////////查询测试////////////////////////////
        #region 搜索结果列表
        public List<ProductInfo> GetCSSearchList(string id, int start, int end)
        {

            string sql = "select * from (select row_number() over(order by ReleaseTime desc) as num,* from Pro where (ProductName like @ProductName and State='1') or (BTypeName like @BTypeName) or (STypeName like @STypeName)) as t where t.num>=@start and t.num<=@end ";

            SqlParameter[] pars ={
                                     new SqlParameter("@ProductName",SqlDbType.NVarChar,50),
                                      new SqlParameter("@BTypeName",SqlDbType.NVarChar,50),
                                       new SqlParameter("@STypeName",SqlDbType.NVarChar,50),
                                     new SqlParameter("@start",SqlDbType.Int),
                                     new SqlParameter ("@end",SqlDbType.Int)

                                 };
            pars[0].Value = "%" + id + "%";
            pars[1].Value = "%" + id + "%";
            pars[2].Value = "%" + id + "%";
            pars[3].Value = start;
            pars[4].Value = end;

            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            List<ProductInfo> list = null;
            if (da.Rows.Count > 0)
            {
                list = new List<ProductInfo>();
                ProductInfo productInfo = null;
                foreach (DataRow row in da.Rows)
                {
                    productInfo = new ProductInfo();
                    Load(productInfo, row);
                    list.Add(productInfo);
                }
            }
            return list;
        }
        #endregion

        //获取卖家其他商品
        //public List<ProductInfo> GetOther(string otherid)
        //{
        //    string sql = "select * from T_Product where UserID=@otherid and State='1' order by ReleaseTime desc";
        //    SqlParameter[] pars ={
        //                             new SqlParameter("@otherid",SqlDbType.VarChar,20)
        //                        };
        //    pars[0].Value = otherid;
        //    DataTable da = SqlHelper.GetTable(sql, CommandType.Text,pars);
        //    List<ProductInfo> OtherList = null;
        //    if (da.Rows.Count > 0)
        //    {
        //        OtherList = new List<ProductInfo>();

        //        foreach (DataRow row in da.Rows)
        //        {
        //            ProductInfo productInfo = null;
        //            productInfo = new ProductInfo();
        //            LoadEntity(productInfo, row);
        //            OtherList.Add(productInfo);
        //        }
        //    }
        //    return OtherList;
        //}

        public int GetOtherCount(string UserID)
        {
            string sql = "select count(*) from T_Product where UserID=@UserID and State='1'";
            SqlParameter[] pars ={
                                    new SqlParameter("@UserID",SqlDbType.VarChar,20)
                                };
            pars[0].Value = UserID;
            return Convert.ToInt32(SqlHelper.ExecuteScalare(sql, CommandType.Text, pars));
        }

        public List<ProductInfo> GetOther(string UserID, int start, int end)
        {

            string sql = "select * from (select row_number() over(order by ReleaseTime desc) as num,* from T_Product where UserID=@UserID and State='1') as t where t.num>=@start and t.num<=@end ";
            SqlParameter[] pars ={
                                     new SqlParameter("@UserID",SqlDbType.VarChar,20),
                                     new SqlParameter("@start",SqlDbType.Int),
                                     new SqlParameter ("@end",SqlDbType.Int)

                                 };
            pars[0].Value = UserID;
            pars[1].Value = start;
            pars[2].Value = end;

            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            List<ProductInfo> otherlist = null;
            if (da.Rows.Count > 0)
            {
                otherlist = new List<ProductInfo>();
                ProductInfo otherproductInfo = null;
                foreach (DataRow row in da.Rows)
                {
                    otherproductInfo = new ProductInfo();
                    LoadEntity(otherproductInfo, row);
                    otherlist.Add(otherproductInfo);
                }
            }
            return otherlist;
        }



        #region 测试关系转对象
        public void Load(ProductInfo productInfo, DataRow row)
        {
            productInfo.Id = Convert.ToInt32(row["Id"]);
            productInfo.UserID = row["UserID"] != DBNull.Value ? row["UserID"].ToString() : string.Empty;
            productInfo.ProductID = row["ProductID"] != DBNull.Value ? row["ProductID"].ToString() : string.Empty;
            productInfo.Btype = row["Btype"] != DBNull.Value ? row["Btype"].ToString() : string.Empty;
            productInfo.Stype = row["Stype"] != DBNull.Value ? row["Stype"].ToString() : string.Empty;
            productInfo.ProductName = row["ProductName"] != DBNull.Value ? row["ProductName"].ToString() : string.Empty;
            productInfo.ProductIntroduce = row["ProductIntroduce"] != DBNull.Value ? row["ProductIntroduce"].ToString() : string.Empty;
            productInfo.ProductImageAdd = row["ProductImageAdd"] != DBNull.Value ? row["ProductImageAdd"].ToString() : string.Empty;
            productInfo.Price = row["Price"] != DBNull.Value ? row["Price"].ToString() : string.Empty;
            productInfo.Phone = row["Phone"] != DBNull.Value ? row["Phone"].ToString() : string.Empty;
            productInfo.ProductNum = row["ProductNum"] != DBNull.Value ? row["ProductNum"].ToString() : string.Empty;
            productInfo.Address = row["Address"] != DBNull.Value ? row["Address"].ToString() : string.Empty;
            productInfo.QQ = row["QQ"] != DBNull.Value ? row["QQ"].ToString() : string.Empty;
            productInfo.ReleaseTime = Convert.ToDateTime(row["ReleaseTime"]);
            productInfo.State = row["State"] != DBNull.Value ? row["State"].ToString() : string.Empty;
            //productInfo.Collection = Convert.ToInt32(row["Collection"]);
            ///////////////////
            productInfo.SId = row["SId"] != DBNull.Value ? row["SId"].ToString() : string.Empty;
            productInfo.Stype = row["Stype"] != DBNull.Value ? row["Stype"].ToString() : string.Empty;
            productInfo.STypeName = row["STypeName"] != DBNull.Value ? row["STypeName"].ToString() : string.Empty;
            productInfo.BId = row["BId"] != DBNull.Value ? row["BId"].ToString() : string.Empty;
            productInfo.Btype = row["Btype"] != DBNull.Value ? row["Btype"].ToString() : string.Empty;
            productInfo.BTypeName = row["BTypeName"] != DBNull.Value ? row["BTypeName"].ToString() : string.Empty;
        }
        #endregion

        //更新收藏数量
        public int updateCollection(string id)
        {
           
            string sql = "update T_Product set Collection=((select Collection from T_Product where Id=@id)+1) where Id=@id";
            SqlParameter[] pars =
            {
                new SqlParameter("@id",SqlDbType.Int)
            };
            pars[0].Value = id;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        }

        //读取收藏的商品
        public List<ProductInfo> getCollectionList(string collectionId)
        {
            string[] collectionArrayId = collectionId.Split(';');
            List<ProductInfo> collectionList = null;
                string sql = "select * from T_Product";
                DataTable da = SqlHelper.Get(sql, CommandType.Text);
                if (da.Rows.Count > 0)
                {
                    collectionList = new List<ProductInfo>();
                    ProductInfo productInfo = null;
                    foreach (DataRow row in da.Rows)
                    {
                    for(int i = 0; i < collectionArrayId.Length; i++)
                    {
                        if (row["Id"].ToString() == collectionArrayId[i])
                        {
                            productInfo = new ProductInfo();
                            LoadEntity(productInfo, row);
                            collectionList.Add(productInfo);
                        }
                            
                    }
                }
                    
                }
            return collectionList;
        }

    }
}
