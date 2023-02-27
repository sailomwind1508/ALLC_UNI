using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Model
{
    public class PromotionUseModel
    {
        public string PromotionID { get; set; }
        public string UseType { get; set; }
        public int UseAmount { get; set; }
        public decimal UseQuantity { get; set; }
        public decimal UsePrice { get; set; }
        public decimal UseSKUAmt { get; set; }
    }
}
