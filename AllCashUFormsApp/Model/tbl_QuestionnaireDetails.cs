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
    
    public partial class tbl_QuestionnaireDetails
    {
        public int QuestionnaireDetailsID { get; set; }
        public Nullable<int> QuestionnaireID { get; set; }
        public Nullable<int> Seq { get; set; }
        public string Question { get; set; }
        public Nullable<int> NextQuestionnaireID { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
    }
}
