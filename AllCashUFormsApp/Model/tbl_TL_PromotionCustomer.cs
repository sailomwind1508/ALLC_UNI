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
    
    public partial class tbl_TL_PromotionCustomer
    {
        public int AutoID { get; set; }
        public string CustomerID { get; set; }
        public int CYear { get; set; }
        public int CMonth { get; set; }
        public System.DateTime CDate { get; set; }
        public int PromotionID { get; set; }
        public string ShelfID { get; set; }
        public string DocNo { get; set; }
        public string WHID { get; set; }
        public bool FlagNew { get; set; }
        public bool FlagEdit { get; set; }
        public System.DateTime CrDate { get; set; }
        public string CrUser { get; set; }
        public Nullable<System.DateTime> EdDate { get; set; }
        public string EdUser { get; set; }
        public bool FlagDel { get; set; }
        public bool FlagSend { get; set; }
    }
}
