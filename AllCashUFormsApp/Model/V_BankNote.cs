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
    
    public partial class V_BankNote
    {
        public Nullable<long> PK { get; set; }
        public string DocNo { get; set; }
        public string WHID { get; set; }
        public Nullable<System.DateTime> Docdate { get; set; }
        public decimal Send { get; set; }
        public decimal Deposit { get; set; }
        public decimal Transfer { get; set; }
        public decimal Cheque { get; set; }
        public decimal TotalSend { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<bool> FlagDel { get; set; }
    }
}
