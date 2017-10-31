using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    //public class ProductInfo
    //{
    //    public int Id { get; set; }
    //    public string UserID { get; set; }
    //    public string ProductID { get; set; }
    //    public string Btype { get; set; }
    //    public string Stype { get; set; }
    //    public string ProductName { get; set; }
    //    public string ProductIntroduce { get; set; }
    //    public string ProductImageAdd { get; set; }
    //    public string Price { get; set; }
    //    public DateTime ReleaseTime { get; set; }
    //    public string Phone { get; set; }
    //    public string ProductNum { get; set; }
    //    public string Address { get; set; }
    //    public string QQ { get; set; }
    //    public string State { get; set; }
    //}


    ///////////////////////////////////////////

    public class ProductInfo
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public string ProductID { get; set; }
        public string Btype { get; set; }
        public string Stype { get; set; }
        public string ProductName { get; set; }
        public string ProductIntroduce { get; set; }
        public string ProductImageAdd { get; set; }
        public string Price { get; set; }
        public DateTime ReleaseTime { get; set; }
        public string Phone { get; set; }
        public string ProductNum { get; set; }
        public string Address { get; set; }
        public string QQ { get; set; }
        public string State { get; set; }
        public int Collection { get; set; }
      
        ////////////////////////
      

        public string STypeID { get; set; }
        public string STypeName { get; set; }
        public string SId { get; set; }
        public string BTypeID { get; set; }
        public string BTypeName { get; set; }
        public string BId { get; set; }
    }


}
