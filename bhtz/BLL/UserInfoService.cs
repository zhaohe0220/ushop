using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
   
    
    public class UserInfoService
    {
       UserInfoDal UserInfoDal = new UserInfoDal();


       #region 登录
       public UserInfo GetUserInfo(string userName, string userPwd)
       {
           return UserInfoDal.GetUserInfo(userName, userPwd);
       } 
       #endregion
        public bool UserRegister(UserInfo userInfo)
        {
            return UserInfoDal.UserRegister(userInfo)>0;
        }

        public int checkName(string name)
        {
            int num = UserInfoDal.checkName(name);
            return num;
        }

       public bool updatepwd(UserInfo userinfo)
       {
           return UserInfoDal.updatepwd(userinfo) > 0;
       }


       public List<UserInfo> GetUser(string id)
       {
           List<UserInfo> userInfo = UserInfoDal.GetUser(id);
           return userInfo;
       }



       public bool updatemes(UserInfo userInfo)
       {
           return UserInfoDal.updatemes(userInfo) > 0;
       }

        public bool updateState(string checkuser)
        {
            return UserInfoDal.updateState(checkuser) > 0;
        }


        public bool addUserMes(UserInfo userinfo)
        {
            return UserInfoDal.addUserMes(userinfo) > 0;
        }
    }
}
