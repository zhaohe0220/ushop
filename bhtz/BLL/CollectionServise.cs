using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public class CollectionServise
    {
        DAL.CollectionDal collectionDal = new DAL.CollectionDal();
        /// <summary>
        /// 验证用户是否存在
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool checkUser(int userid)
        {
            return collectionDal.checkUser(userid)>0;
        }
        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool addCollection(string id,int userId)
        {
            return collectionDal.addCollection(id,userId) > 0;
        }
        /// <summary>
        /// 更新收藏
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool updateCollection(string id,int userId)
        {
            return collectionDal.updateCollection(id, userId) > 0;
        }

        /// <summary>
        /// 读取用户收藏的商品全部Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Model.CollectionInfo getCollection(int userId)
        {
            return collectionDal.getCollection(userId);
        }

        /// <summary>
        /// 删除收藏
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool deleteCollection(int userId, string id,string collectionId)
        {
            return collectionDal.deleteCollection(userId,id, collectionId) > 0;
        }
    }
}
