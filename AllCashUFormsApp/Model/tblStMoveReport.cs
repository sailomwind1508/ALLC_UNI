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
    
    public partial class tblStMoveReport
    {
        public string WHID { get; set; }
        public string ProductID { get; set; }
        public System.DateTime TrnDate { get; set; }
        public Nullable<decimal> BeginQty { get; set; }
        public Nullable<decimal> ReceiveQty { get; set; }
        public Nullable<decimal> SalesQty { get; set; }
        public Nullable<decimal> TransferQty { get; set; }
        public Nullable<decimal> RemainQty { get; set; }
        public Nullable<decimal> Qty { get; set; }
    }
}
