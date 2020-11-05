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
    
    public partial class tbl_TL_ArCustomer
    {
        public string CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerRefCode { get; set; }
        public short Seq { get; set; }
        public string CustTitle { get; set; }
        public string CustName { get; set; }
        public string CustShortName { get; set; }
        public Nullable<int> CustomerTypeID { get; set; }
        public string BillTo { get; set; }
        public string ShipTo { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Contact { get; set; }
        public string AddressNo { get; set; }
        public string lane { get; set; }
        public string Street { get; set; }
        public Nullable<int> AreaID { get; set; }
        public Nullable<int> DistrictID { get; set; }
        public Nullable<int> ProvinceID { get; set; }
        public string PostalCode { get; set; }
        public string Moo { get; set; }
        public string Email { get; set; }
        public byte CreditDay { get; set; }
        public string TaxId { get; set; }
        public Nullable<bool> VatType { get; set; }
        public Nullable<decimal> VatRate { get; set; }
        public string DiscountType { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public string EmpID { get; set; }
        public Nullable<int> PriceGroupID { get; set; }
        public string WHID { get; set; }
        public string SalAreaID { get; set; }
        public int ShopTypeID { get; set; }
        public System.DateTime CrDate { get; set; }
        public string CrUser { get; set; }
        public Nullable<System.DateTime> EdDate { get; set; }
        public string EdUser { get; set; }
        public bool FlagDel { get; set; }
        public bool FlagSend { get; set; }
        public bool FlagMember { get; set; }
        public bool FlagBill { get; set; }
        public int NetPoint { get; set; }
        public string CustomerSAPCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Nullable<System.DateTime> GPSDate { get; set; }
        public bool IsNewMember { get; set; }
        public bool FlagNew { get; set; }
        public bool FlagEdit { get; set; }
        public byte[] CustomerImg { get; set; }
        public bool PromotionVanID { get; set; }
    }
}
