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
    
    public partial class tbl_InvMovement
    {
        public int TransactionID { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string RefDocNo { get; set; }
        public System.DateTime TrnDate { get; set; }
        public string TrnType { get; set; }
        public string DocTypeCode { get; set; }
        public string WHID { get; set; }
        public string FromWHID { get; set; }
        public string ToWHID { get; set; }
        public decimal TrnQtyIn { get; set; }
        public decimal TrnQtyOut { get; set; }
        public decimal TrnQty { get; set; }
        public System.DateTime CrDate { get; set; }
        public Nullable<System.DateTime> EdDate { get; set; }
        public string ProductGroupCode { get; set; }
        public string ProductGroupName { get; set; }
        public string ProductSubGroupCode { get; set; }
        public string ProductSubGroupName { get; set; }
        public bool FlagSend { get; set; }
    }
}
