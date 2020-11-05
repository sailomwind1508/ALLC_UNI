using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Model
{
    public class ProductMovementModel
    {
        public string ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int UomSetID { get; set; }
        public int ProductGroupID { get; set; }
        public int ProductSubGroupID { get; set; }
        public int BaseQty { get; set; }
        public int ImpLargeQty { get; set; }
        public int ImpSmallQty { get; set; }
        public decimal InQty { get; set; }
        public decimal OutQty { get; set; }
        public decimal QtyOnHandLarge { get; set; }
        public decimal QtyOnHandSmall { get; set; }
        public DateTime TrnDate { get; set; }
        public string WHID { get; set; }

        public string RefDocNo { get; set; }
        public string TrnType { get; set; }
        public string ToWHID { get; set; }
        public decimal ForwardQty { get; set; }
        public decimal DTBalance { get; set; }
    }
}
