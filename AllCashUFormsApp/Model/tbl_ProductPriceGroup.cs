//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AllCashUFormsApp.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_ProductPriceGroup
    {
        public int PriceGroupID { get; set; }
        public string ProductID { get; set; }
        public int ProductUomID { get; set; }
        public Nullable<decimal> SellPrice { get; set; }
        public Nullable<decimal> BuyPrice { get; set; }
        public Nullable<decimal> SellPriceVat { get; set; }
        public Nullable<decimal> BuyPriceVat { get; set; }
        public Nullable<System.DateTime> CrDate { get; set; }
        public string CrUser { get; set; }
        public Nullable<System.DateTime> EdDate { get; set; }
        public string EdUser { get; set; }
        public bool FlagDel { get; set; }
        public bool FlagSend { get; set; }
        public bool FlagNew { get; set; }
        public bool FlagEdit { get; set; }
        public Nullable<decimal> ComPrice { get; set; }
    }
}
