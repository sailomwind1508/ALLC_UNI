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
    
    public partial class tbl_SimOrderItem
    {
        public string BranchID { get; set; }
        public string ItemID { get; set; }
        public string WHID { get; set; }
        public string CustomerID { get; set; }
        public System.DateTime DocDate { get; set; }
        public byte RevisionNo { get; set; }
        public string DocNo { get; set; }
        public string ProductID { get; set; }
        public string ProductShortName { get; set; }
        public string ProductName { get; set; }
        public int OrderUom { get; set; }
        public string ProductUomName { get; set; }
        public string ProductUomNameTH { get; set; }
        public double ReceivedQty { get; set; }
        public double UnitPrice { get; set; }
        public byte VatType { get; set; }
        public string LineDiscountType { get; set; }
        public double LineDiscount { get; set; }
        public double LineTotal { get; set; }
        public int Status { get; set; }
        public bool FlagSend { get; set; }
    }
}
