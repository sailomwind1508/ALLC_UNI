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
    
    public partial class GetTwoBill
    {
        public string DocNo { get; set; }
        public string DocTypeCode { get; set; }
        public string DocStatus { get; set; }
        public System.DateTime DocDate { get; set; }
        public string CustomerID { get; set; }
        public short Line { get; set; }
        public string ProductID { get; set; }
        public int OrderUom { get; set; }
        public int BaseQty { get; set; }
        public Nullable<decimal> OrderQty { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<byte> VatType { get; set; }
        public Nullable<decimal> LineTotal { get; set; }
        public string LineDiscountType { get; set; }
        public decimal LineDiscountRate { get; set; }
        public decimal LineDiscount { get; set; }
        public string LineRemark { get; set; }
        public int FreeQty { get; set; }
        public byte FreeUom { get; set; }
        public int FreeUnit { get; set; }
        public string WHID { get; set; }
    }
}