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
    
    public partial class tbl_PaidDetail
    {
        public int PaidID { get; set; }
        public int AutoID { get; set; }
        public int BranchID { get; set; }
        public short ShiftNo { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
        public decimal TotalSale { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal NetTotal { get; set; }
        public string IBDocNo { get; set; }
        public string INDocNo { get; set; }
        public System.DateTime CrDate { get; set; }
        public string CrUser { get; set; }
        public Nullable<System.DateTime> EdDate { get; set; }
        public string EdUser { get; set; }
        public bool FlagDel { get; set; }
        public bool FlagSend { get; set; }
    }
}