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
    
    public partial class tbl_MstCycle
    {
        public string CYear { get; set; }
        public short CNo { get; set; }
        public string CName { get; set; }
        public Nullable<System.DateTime> CStartDate { get; set; }
        public Nullable<System.DateTime> CEndDate { get; set; }
        public Nullable<System.DateTime> CrDate { get; set; }
        public string CrUser { get; set; }
        public Nullable<System.DateTime> EdDate { get; set; }
        public string EdUser { get; set; }
        public bool FlagDel { get; set; }
        public bool FlagSend { get; set; }
    }
}