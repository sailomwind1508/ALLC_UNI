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
    
    public partial class tbl_DocSignature
    {
        public int AutoID { get; set; }
        public string DocNo { get; set; }
        public string DocTypeCode { get; set; }
        public Nullable<System.DateTime> DocDate { get; set; }
        public string WHID { get; set; }
        public byte[] ImgSign { get; set; }
        public string Remark { get; set; }
        public System.DateTime CrDate { get; set; }
    }
}
