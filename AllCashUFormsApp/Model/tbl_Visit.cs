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
    
    public partial class tbl_Visit
    {
        public string VisitID { get; set; }
        public string BranchID { get; set; }
        public string WHID { get; set; }
        public System.DateTime VisitDate { get; set; }
        public string CustomerID { get; set; }
        public bool VisitStatus { get; set; }
        public Nullable<int> CauseID { get; set; }
        public string CauseRemark { get; set; }
        public System.DateTime CrDate { get; set; }
        public string CrUser { get; set; }
        public Nullable<System.DateTime> EdDate { get; set; }
        public string EdUser { get; set; }
        public bool FlagDel { get; set; }
        public bool FlagSend { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
