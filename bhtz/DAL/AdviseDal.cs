using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class AdviseDal
    {
        public int AddAdvise(AdviseInfo AdviseInfo)
        {
            string sql = "insert into T_Advise(Advise)values(@Advise)";
            SqlParameter[] pars ={
                                    new SqlParameter("@Advise",SqlDbType.NVarChar,1000)
                                };
            pars[0].Value = AdviseInfo.Advise;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        } 
    }
}
