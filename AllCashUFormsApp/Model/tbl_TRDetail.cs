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
    
    public partial class tbl_TRDetail
    {
        public string DocNo { get; set; }
        public string ProductID { get; set; }
        public short Line { get; set; }
        public string ProductName { get; set; }
        public int OrderUom { get; set; }
        public Nullable<decimal> OrderQty { get; set; }
        public Nullable<decimal> SendQty { get; set; }
        public Nullable<decimal> ReceivedQty { get; set; }
        public Nullable<decimal> RejectedQty { get; set; }
        public decimal StockedQty { get; set; }
        public Nullable<decimal> UnitCost { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<byte> VatType { get; set; }
        public Nullable<decimal> LineTotal { get; set; }
        public string LineRemark { get; set; }
        public System.DateTime CrDate { get; set; }
        public string CrUser { get; set; }
        public Nullable<System.DateTime> EdDate { get; set; }
        public string EdUser { get; set; }
        public bool FlagDel { get; set; }
        public bool FlagSend { get; set; }
    }
}
