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
    
    public partial class tbl_error_logs
    {
        public int pk { get; set; }
        public string user_code { get; set; }
        public string form_name { get; set; }
        public string function_name { get; set; }
        public string err_desc { get; set; }
        public Nullable<System.DateTime> time_log { get; set; }
    }
}
