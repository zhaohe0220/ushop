using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class CollectionDal
    {
        public int checkUser(int userid)
        {
            //判断userid是否存在
            string sql = "select count(*) from T_Collection where UserId=@userId";
            SqlParameter[] pars =
            {
                new SqlParameter("@userId",SqlDbType.Int)
            };
            pars[0].Value = userid;
            return Convert.ToInt32(SqlHelper.ExecuteScalare(sql, CommandType.Text, pars));
        }
        //添加用户名插入收藏
        public int addCollection(string id, int userId)
        {
            id = id + ";";
            string sql = "insert into T_Collection(UserId,Collection) values(@userId,@id)";
            SqlParameter[] pars =
                {
                new SqlParameter("@userId",SqlDbType.Int),
                new SqlParameter("@id",SqlDbType.VarChar,500)
            };
            pars[0].Value = userId;
            pars[1].Value = id;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        }
        //更新收藏
        public int updateCollection(string id, int userId)
        {
            id = id + ";";
            string sql = "update T_Collection set Collection=(select Collection from T_Collection where UserId=@UserId)+'" + id + "' where UserId=@userId";
            SqlParameter[] pars =
                {
                new SqlParameter("@userId",SqlDbType.Int)
            };
            pars[0].Value = userId;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        }

        public Model.CollectionInfo getCollection(int userId)
        {
            string sql = "select * from T_Collection where UserId=@userId";
            SqlParameter[] pars =
            {
                new SqlParameter("@userId",SqlDbType.Int)
            };
            pars[0].Value = userId;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            CollectionInfo collection = null;
            if (da.Rows.Count > 0)
            {
                collection = new CollectionInfo();
                LoadEntity(collection, da.Rows[0]);
            }
            return collection;
        }

        public int deleteCollection(int userId, string id, string collectionId)
        {
            collectionId = collectionId.Replace(id + ";", "");
            string sql = "update T_Collection set Collection=@Collection where UserId=@userId";
            SqlParameter[] pars =
            {
                new SqlParameter("@Collection",SqlDbType.VarChar,500),
                new SqlParameter("@userId",SqlDbType.Int)
            };
            pars[0].Value = collectionId;
            pars[1].Value = userId;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);

        }


        //关系转对象
        public void LoadEntity(CollectionInfo collectionInfo, DataRow row)
        {
            collectionInfo.Id = Convert.ToInt32(row["Id"]);
            collectionInfo.UserID = Convert.ToInt32(row["Id"]);
            collectionInfo.Collection = row["collection"] != DBNull.Value ? row["collection"].ToString() : string.Empty;
        }
    }
}
