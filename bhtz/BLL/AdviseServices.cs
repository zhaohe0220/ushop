using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class AdviseServices
    {
        DAL.AdviseDal AdviseDal = new DAL.AdviseDal();
        public bool AddAdvise(AdviseInfo adviseInfo)
        {
            return AdviseDal.AddAdvise(adviseInfo) > 0;
        } 
    }
}
