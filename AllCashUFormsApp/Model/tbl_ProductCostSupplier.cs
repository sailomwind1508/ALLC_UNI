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
    
    public partial class tbl_ProductCostSupplier
    {
        public string SupplierID { get; set; }
        public string ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OldUnitPrice { get; set; }
        public System.DateTime DateChanged { get; set; }
        public bool FlagSend { get; set; }
    }
}
