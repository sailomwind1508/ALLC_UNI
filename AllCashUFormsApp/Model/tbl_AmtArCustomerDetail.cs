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
    
    public partial class tbl_AmtArCustomerDetail
    {
        public string CustomerID { get; set; }
        public short Seq { get; set; }
        public string CustTitle { get; set; }
        public string CustName { get; set; }
        public string CustShotName { get; set; }
        public string Contact { get; set; }
        public string BillTo { get; set; }
        public string AreaName { get; set; }
        public string DistrictName { get; set; }
        public string ProvinceName { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public int ShopTypeID { get; set; }
        public string ShopTypeName { get; set; }
        public string AmtArCustID { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Nullable<System.DateTime> GPSDate { get; set; }
        public byte[] CustomerImg { get; set; }
        public bool FlagShelf { get; set; }
    }
}