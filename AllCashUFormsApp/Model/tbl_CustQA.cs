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
    
    public partial class tbl_CustQA
    {
        public long PK { get; set; }
        public string CustomerID { get; set; }
        public Nullable<int> QuestionnaireID { get; set; }
        public Nullable<int> QuestionnaireDetailsID { get; set; }
        public Nullable<bool> SelectFlag { get; set; }
        public string Text { get; set; }
        public Nullable<decimal> Score { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string WHID { get; set; }
    }
}
