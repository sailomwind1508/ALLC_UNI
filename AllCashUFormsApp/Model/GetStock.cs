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
    
    public partial class GetStock
    {
        public string ProductID { get; set; }
        public Nullable<short> Seq { get; set; }
        public string ProductName { get; set; }
        public decimal QtyOnHand { get; set; }
        public decimal OrderQty { get; set; }
        public string DocNo { get; set; }
        public Nullable<System.DateTime> CrDate { get; set; }
        public Nullable<System.DateTime> EdDate { get; set; }
        public string FromWHID { get; set; }
        public string ToWHID { get; set; }
    }
}