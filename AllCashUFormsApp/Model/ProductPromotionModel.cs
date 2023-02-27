using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Model
{
    public class ProductPromotionModel
    {
        public string SKUGroupID { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalQty { get; set; }
        public decimal SkuAmt { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
