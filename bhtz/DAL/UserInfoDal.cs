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
    public class UserInfoDal
    {
        #region 登录
        public UserInfo GetUserInfo(string userName, string userPwd)
        {
            string sql = "select * from T_Users where (Name=@Name and Pwd=@Pwd and state='1') or (Email=@Name and Pwd=@Pwd and state='1')";
            SqlParameter[] pars = {
                                      new SqlParameter("@Name",SqlDbType.VarChar,20),
                                      new SqlParameter("@Pwd",SqlDbType.VarChar,20),
                                  };
            pars[0].Value = userName;
            pars[1].Value = userPwd;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            UserInfo userInfo = null;
            if (da.Rows.Count > 0)
            {
                userInfo = new UserInfo();
                LoadEntity(userInfo, da.Rows[0]);
            }
            return userInfo;
        } 
        #endregion

        //验证用户名是否存在
        public int checkName(string name)
        {
            string sql = "select count(*) from T_Users where Name=@name";
            SqlParameter[] pars ={
                                    new SqlParameter("@name",SqlDbType.VarChar,20)
                                };
            pars[0].Value = name;
            return Convert.ToInt32(SqlHelper.ExecuteScalare(sql, CommandType.Text, pars));
        }

        //注册
        public int UserRegister(UserInfo userInfo)
        {
            string sql = "insert into T_Users(Name,Pwd,Email,state)values(@name,@pwd,@email,@state)";
            SqlParameter[] pars ={
                                    new SqlParameter("@name",SqlDbType.VarChar,20),
                                    new SqlParameter("@pwd",SqlDbType.VarChar,20),
                                    new SqlParameter("@email",SqlDbType.VarChar,50),
                                    new SqlParameter("@state",SqlDbType.Char,1)
                                };
            pars[0].Value = userInfo.Name;
            pars[1].Value = userInfo.Pwd;
            pars[2].Value = userInfo.Email;
            pars[3].Value = userInfo.state;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        }

        #region 关系转对象
        public void LoadEntity(UserInfo userInfo, DataRow row)
        {
            userInfo.Id = Convert.ToInt32(row["Id"]);
            userInfo.Name = row["Name"] != DBNull.Value ? row["Name"].ToString() : string.Empty;
            userInfo.Pwd = row["Pwd"] != DBNull.Value ? row["Pwd"].ToString() : string.Empty;
         
            userInfo.Phone = row["Phone"] != DBNull.Value ? row["Phone"].ToString() : string.Empty;
            userInfo.QQ = row["QQ"] != DBNull.Value ? row["QQ"].ToString() : string.Empty;
            userInfo.Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : string.Empty;
        } 
        #endregion

        #region 密码更新
        public int updatepwd(UserInfo UserInfo)
        {
            string sql = "update T_Users set Pwd=@Pwd where Name=@Name";
            SqlParameter[] pars ={
                                    new SqlParameter("@Pwd",SqlDbType.VarChar,20),
                                     new SqlParameter("@Name",SqlDbType.VarChar,20)
                                };
            pars[0].Value = UserInfo.Pwd;
            pars[1].Value = UserInfo.Name;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        }
        #endregion



        public List<UserInfo> GetUser(string id)
        {
            string sql = "select * from T_Users where Name=@id";
            SqlParameter[] pars ={
                                     new SqlParameter("@id",SqlDbType.VarChar,20)
                                 };
            pars[0].Value = id;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            List<UserInfo> UserList = null;
            if (da.Rows.Count > 0)
            {
                UserList = new List<UserInfo>();
                foreach (DataRow row in da.Rows)
                {
                    UserInfo userInfo = null;
                    userInfo = new UserInfo();
                    LoadEntity(userInfo, row);
                    UserList.Add(userInfo);
                }
            }
            return UserList;
        }


        //更新用户状态
        public int updateState(string checkuser)
        {
            string sql = "update T_Users set state=@state where Name=@Name";
            SqlParameter[] pars ={
                                      new SqlParameter("@state",SqlDbType.Char,1),
                                     new SqlParameter("@Name",SqlDbType.VarChar,20)
                                };
            pars[0].Value = "1";
            pars[1].Value = checkuser;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        }

        public int addUserMes(UserInfo userinfo)
        {
            string sql = "update T_Users set Phone=@Phone,QQ=@QQ where Name=@Name";
            SqlParameter[] pars ={
                                     new SqlParameter("@Phone",SqlDbType.VarChar,20),
                                      new SqlParameter("@QQ",SqlDbType.VarChar,20),
                                     new SqlParameter("@Name",SqlDbType.VarChar,20)
                                };
            pars[0].Value = userinfo.Phone;
            pars[1].Value = userinfo.QQ;
            pars[2].Value = userinfo.Name;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        }


        #region 个人信息更新
        public int updatemes(UserInfo UserInfo)
        {
            string sql = "update T_Users set Phone=@Phone,QQ=@QQ,Email=@Email where Name=@Name";
            SqlParameter[] pars ={
                                     new SqlParameter("@Phone",SqlDbType.VarChar,20),
                                      new SqlParameter("@QQ",SqlDbType.VarChar,20),
                                      new SqlParameter("@Email",SqlDbType.VarChar,50),
                                     new SqlParameter("@Name",SqlDbType.VarChar,20)
                                };
            if (UserInfo.Phone == null)
            {
                pars[0].Value = "";
            }
            else { pars[0].Value = UserInfo.Phone; }
            if (UserInfo.QQ == null)
            {
                pars[1].Value = "";
            }
            else { pars[1].Value = UserInfo.QQ; }
            if (UserInfo.Email == null)
            {
                pars[2].Value = "";
            }
            else { pars[2].Value = UserInfo.Email; }

            pars[3].Value = UserInfo.Name;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        }
        #endregion
    }
}
