﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class DB_ALL_CASH_UNIEntities : DbContext
    {
        public DB_ALL_CASH_UNIEntities(string conStr)
            : base(conStr)
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<tbl_AdmControlList> tbl_AdmControlList { get; set; }
        public virtual DbSet<tbl_AdmFormList> tbl_AdmFormList { get; set; }
        public virtual DbSet<tbl_AdmMenuList> tbl_AdmMenuList { get; set; }
        public virtual DbSet<tbl_AdmRoleControl> tbl_AdmRoleControl { get; set; }
        public virtual DbSet<tbl_ApSupplier> tbl_ApSupplier { get; set; }
        public virtual DbSet<tbl_ApSupplierType> tbl_ApSupplierType { get; set; }
        public virtual DbSet<tbl_ArCustomer> tbl_ArCustomer { get; set; }
        public virtual DbSet<tbl_ArCustomerMapping> tbl_ArCustomerMapping { get; set; }
        public virtual DbSet<tbl_ArCustomerShelf> tbl_ArCustomerShelf { get; set; }
        public virtual DbSet<tbl_ArCustomerType> tbl_ArCustomerType { get; set; }
        public virtual DbSet<tbl_Branch> tbl_Branch { get; set; }
        public virtual DbSet<tbl_BranchCycle> tbl_BranchCycle { get; set; }
        public virtual DbSet<tbl_BranchGroup> tbl_BranchGroup { get; set; }
        public virtual DbSet<tbl_BranchWarehouse> tbl_BranchWarehouse { get; set; }
        public virtual DbSet<tbl_Cause> tbl_Cause { get; set; }
        public virtual DbSet<tbl_CfgConnection> tbl_CfgConnection { get; set; }
        public virtual DbSet<tbl_CfgKeyField> tbl_CfgKeyField { get; set; }
        public virtual DbSet<tbl_CfgPosMachine> tbl_CfgPosMachine { get; set; }
        public virtual DbSet<tbl_CfgSetting> tbl_CfgSetting { get; set; }
        public virtual DbSet<tbl_Company> tbl_Company { get; set; }
        public virtual DbSet<tbl_CoreCard> tbl_CoreCard { get; set; }
        public virtual DbSet<tbl_CoreCardReason> tbl_CoreCardReason { get; set; }
        public virtual DbSet<tbl_CustQA> tbl_CustQA { get; set; }
        public virtual DbSet<tbl_Department> tbl_Department { get; set; }
        public virtual DbSet<tbl_DiscountType> tbl_DiscountType { get; set; }
        public virtual DbSet<tbl_DisplayImage> tbl_DisplayImage { get; set; }
        public virtual DbSet<tbl_DocRunning> tbl_DocRunning { get; set; }
        public virtual DbSet<tbl_DocSendUpdate> tbl_DocSendUpdate { get; set; }
        public virtual DbSet<tbl_DocSignature> tbl_DocSignature { get; set; }
        public virtual DbSet<tbl_DocumentStatus> tbl_DocumentStatus { get; set; }
        public virtual DbSet<tbl_DocumentType> tbl_DocumentType { get; set; }
        public virtual DbSet<tbl_Employee> tbl_Employee { get; set; }
        public virtual DbSet<tbl_error_logs> tbl_error_logs { get; set; }
        public virtual DbSet<tbl_HQ_Promotion> tbl_HQ_Promotion { get; set; }
        public virtual DbSet<tbl_HQ_Promotion_Hit> tbl_HQ_Promotion_Hit { get; set; }
        public virtual DbSet<tbl_HQ_Promotion_Hit_Temp> tbl_HQ_Promotion_Hit_Temp { get; set; }
        public virtual DbSet<tbl_HQ_Promotion_Master> tbl_HQ_Promotion_Master { get; set; }
        public virtual DbSet<tbl_HQ_Reward> tbl_HQ_Reward { get; set; }
        public virtual DbSet<tbl_HQ_SKUGroup> tbl_HQ_SKUGroup { get; set; }
        public virtual DbSet<tbl_HQ_SKUGroup_EXC> tbl_HQ_SKUGroup_EXC { get; set; }
        public virtual DbSet<tbl_InvMovement> tbl_InvMovement { get; set; }
        public virtual DbSet<tbl_InvTransaction> tbl_InvTransaction { get; set; }
        public virtual DbSet<tbl_InvWarehouse> tbl_InvWarehouse { get; set; }
        public virtual DbSet<tbl_IVDetail> tbl_IVDetail { get; set; }
        public virtual DbSet<tbl_IVMaster> tbl_IVMaster { get; set; }
        public virtual DbSet<tbl_MstArea> tbl_MstArea { get; set; }
        public virtual DbSet<tbl_MstCycle> tbl_MstCycle { get; set; }
        public virtual DbSet<tbl_MstDistrict> tbl_MstDistrict { get; set; }
        public virtual DbSet<tbl_MstMenu> tbl_MstMenu { get; set; }
        public virtual DbSet<tbl_MstPart> tbl_MstPart { get; set; }
        public virtual DbSet<tbl_MstProvince> tbl_MstProvince { get; set; }
        public virtual DbSet<tbl_MstProvince_Mapping> tbl_MstProvince_Mapping { get; set; }
        public virtual DbSet<tbl_PaidDetail> tbl_PaidDetail { get; set; }
        public virtual DbSet<tbl_PaidMaster> tbl_PaidMaster { get; set; }
        public virtual DbSet<tbl_PayDetail> tbl_PayDetail { get; set; }
        public virtual DbSet<tbl_PayMaster> tbl_PayMaster { get; set; }
        public virtual DbSet<tbl_PODetail> tbl_PODetail { get; set; }
        public virtual DbSet<tbl_PODetail_PRE> tbl_PODetail_PRE { get; set; }
        public virtual DbSet<tbl_POMaster> tbl_POMaster { get; set; }
        public virtual DbSet<tbl_Position> tbl_Position { get; set; }
        public virtual DbSet<tbl_PRDetail> tbl_PRDetail { get; set; }
        public virtual DbSet<tbl_PreSaleWarehouse> tbl_PreSaleWarehouse { get; set; }
        public virtual DbSet<tbl_PriceGroup> tbl_PriceGroup { get; set; }
        public virtual DbSet<tbl_PRMaster> tbl_PRMaster { get; set; }
        public virtual DbSet<tbl_Product> tbl_Product { get; set; }
        public virtual DbSet<tbl_ProductBrand> tbl_ProductBrand { get; set; }
        public virtual DbSet<tbl_ProductFlavour> tbl_ProductFlavour { get; set; }
        public virtual DbSet<tbl_ProductGroup> tbl_ProductGroup { get; set; }
        public virtual DbSet<tbl_ProductPriceGroup> tbl_ProductPriceGroup { get; set; }
        public virtual DbSet<tbl_ProductRemarkReject> tbl_ProductRemarkReject { get; set; }
        public virtual DbSet<tbl_ProductSubGroup> tbl_ProductSubGroup { get; set; }
        public virtual DbSet<tbl_ProductType> tbl_ProductType { get; set; }
        public virtual DbSet<tbl_ProductUom> tbl_ProductUom { get; set; }
        public virtual DbSet<tbl_ProductUomSet> tbl_ProductUomSet { get; set; }
        public virtual DbSet<tbl_Questionnaire> tbl_Questionnaire { get; set; }
        public virtual DbSet<tbl_QuestionnaireDetails> tbl_QuestionnaireDetails { get; set; }
        public virtual DbSet<tbl_RemainingStock> tbl_RemainingStock { get; set; }
        public virtual DbSet<tbl_Roles> tbl_Roles { get; set; }
        public virtual DbSet<tbl_SalArea> tbl_SalArea { get; set; }
        public virtual DbSet<tbl_SalAreaDistrict> tbl_SalAreaDistrict { get; set; }
        public virtual DbSet<tbl_SalAreaVisit> tbl_SalAreaVisit { get; set; }
        public virtual DbSet<tbl_SaleBranchSummary> tbl_SaleBranchSummary { get; set; }
        public virtual DbSet<tbl_SalesTrace> tbl_SalesTrace { get; set; }
        public virtual DbSet<tbl_SaleType> tbl_SaleType { get; set; }
        public virtual DbSet<tbl_SendData> tbl_SendData { get; set; }
        public virtual DbSet<tbl_ShopType> tbl_ShopType { get; set; }
        public virtual DbSet<tbl_ShopTypeGroup> tbl_ShopTypeGroup { get; set; }
        public virtual DbSet<tbl_TL_ArCustomer> tbl_TL_ArCustomer { get; set; }
        public virtual DbSet<tbl_TL_ArCustomer_HIS> tbl_TL_ArCustomer_HIS { get; set; }
        public virtual DbSet<tbl_TL_ArCustomerShelf> tbl_TL_ArCustomerShelf { get; set; }
        public virtual DbSet<tbl_TL_ArCustomerShelf_HIS> tbl_TL_ArCustomerShelf_HIS { get; set; }
        public virtual DbSet<tbl_TL_CustomerCode> tbl_TL_CustomerCode { get; set; }
        public virtual DbSet<tbl_TL_CustomerCode_HIS> tbl_TL_CustomerCode_HIS { get; set; }
        public virtual DbSet<tbl_TL_CustQA> tbl_TL_CustQA { get; set; }
        public virtual DbSet<tbl_TL_PODetail> tbl_TL_PODetail { get; set; }
        public virtual DbSet<tbl_TL_PODetail_HIS> tbl_TL_PODetail_HIS { get; set; }
        public virtual DbSet<tbl_TL_POMaster> tbl_TL_POMaster { get; set; }
        public virtual DbSet<tbl_TL_POMaster_HIS> tbl_TL_POMaster_HIS { get; set; }
        public virtual DbSet<tbl_TL_SalesTrace> tbl_TL_SalesTrace { get; set; }
        public virtual DbSet<tbl_TL_SalesTrace_HIS> tbl_TL_SalesTrace_HIS { get; set; }
        public virtual DbSet<tbl_TL_Visit> tbl_TL_Visit { get; set; }
        public virtual DbSet<tbl_TL_Visit_HIS> tbl_TL_Visit_HIS { get; set; }
        public virtual DbSet<tbl_TL_VisitShopImage> tbl_TL_VisitShopImage { get; set; }
        public virtual DbSet<tbl_TL_VisitShopImage_HIS> tbl_TL_VisitShopImage_HIS { get; set; }
        public virtual DbSet<tbl_TL_VisitStock> tbl_TL_VisitStock { get; set; }
        public virtual DbSet<tbl_TL_VisitStock_HIS> tbl_TL_VisitStock_HIS { get; set; }
        public virtual DbSet<tbl_TLDetail> tbl_TLDetail { get; set; }
        public virtual DbSet<tbl_TLMaster> tbl_TLMaster { get; set; }
        public virtual DbSet<tbl_TmpPRDetail> tbl_TmpPRDetail { get; set; }
        public virtual DbSet<tbl_TRDetail> tbl_TRDetail { get; set; }
        public virtual DbSet<tbl_TRMaster> tbl_TRMaster { get; set; }
        public virtual DbSet<tbl_UpdateInvWH> tbl_UpdateInvWH { get; set; }
        public virtual DbSet<tbl_Users> tbl_Users { get; set; }
        public virtual DbSet<tbl_VANStock> tbl_VANStock { get; set; }
        public virtual DbSet<tbl_VanType> tbl_VanType { get; set; }
        public virtual DbSet<tbl_Visit> tbl_Visit { get; set; }
        public virtual DbSet<tbl_VisitShopImage> tbl_VisitShopImage { get; set; }
        public virtual DbSet<tbl_VisitStock> tbl_VisitStock { get; set; }
        public virtual DbSet<tbl_Zone> tbl_Zone { get; set; }
        public virtual DbSet<tblStMoveReport> tblStMoveReports { get; set; }
        public virtual DbSet<tbl_AmtArCustomer> tbl_AmtArCustomer { get; set; }
        public virtual DbSet<tbl_AmtArCustomerDetail> tbl_AmtArCustomerDetail { get; set; }
        public virtual DbSet<tbl_BranchWarehouseMapping> tbl_BranchWarehouseMapping { get; set; }
        public virtual DbSet<tbl_CfgFixedHolidays> tbl_CfgFixedHolidays { get; set; }
        public virtual DbSet<tbl_CfgFloatingHolidays> tbl_CfgFloatingHolidays { get; set; }
        public virtual DbSet<tbl_MstGrade> tbl_MstGrade { get; set; }
        public virtual DbSet<tbl_MstWorkDate> tbl_MstWorkDate { get; set; }
        public virtual DbSet<tbl_MstWorkDate_HIS> tbl_MstWorkDate_HIS { get; set; }
        public virtual DbSet<tbl_ReportGroup> tbl_ReportGroup { get; set; }
        public virtual DbSet<tbl_ReportMenu> tbl_ReportMenu { get; set; }
        public virtual DbSet<Temp_Rep_Visit_Per_Day_PO> Temp_Rep_Visit_Per_Day_PO { get; set; }
        public virtual DbSet<AllSaleEmployeeView> AllSaleEmployeeViews { get; set; }
        public virtual DbSet<ArCustomerView> ArCustomerViews { get; set; }
        public virtual DbSet<GetBranchWarehouse> GetBranchWarehouses { get; set; }
        public virtual DbSet<GetCoreCardReason> GetCoreCardReasons { get; set; }
        public virtual DbSet<GetCust_Routing> GetCust_Routing { get; set; }
        public virtual DbSet<GetProduct> GetProducts { get; set; }
        public virtual DbSet<GetPromotion> GetPromotions { get; set; }
        public virtual DbSet<GetProvinceAndDistrict> GetProvinceAndDistricts { get; set; }
        public virtual DbSet<GetSetting> GetSettings { get; set; }
        public virtual DbSet<GetStock> GetStocks { get; set; }
        public virtual DbSet<GetTotalBill> GetTotalBills { get; set; }
        public virtual DbSet<GetTwoBill> GetTwoBills { get; set; }
        public virtual DbSet<GetTwoStock> GetTwoStocks { get; set; }
        public virtual DbSet<ProductView> ProductViews { get; set; }
        public virtual DbSet<ReceiveTabletDataView> ReceiveTabletDataViews { get; set; }
        public virtual DbSet<ReceiveTabletDataView_Lastest> ReceiveTabletDataView_Lastest { get; set; }
        public virtual DbSet<Rpt_DSR> Rpt_DSR { get; set; }
        public virtual DbSet<Rpt_DSR_KPI> Rpt_DSR_KPI { get; set; }
        public virtual DbSet<SaleEmployeeView> SaleEmployeeViews { get; set; }
        public virtual DbSet<V_BankNote> V_BankNote { get; set; }
        public virtual DbSet<V_CountVisit_UBON> V_CountVisit_UBON { get; set; }
        public virtual DbSet<V_CUSTOMERDT> V_CUSTOMERDT { get; set; }
        public virtual DbSet<V_CUSTOMERHD> V_CUSTOMERHD { get; set; }
        public virtual DbSet<V_GetVanStock> V_GetVanStock { get; set; }
        public virtual DbSet<V_SummaryRL> V_SummaryRL { get; set; }
        public virtual DbSet<V_SummaryRL_Get> V_SummaryRL_Get { get; set; }
        public virtual DbSet<V_SummaryRL_Stock> V_SummaryRL_Stock { get; set; }
        public virtual DbSet<V_UBNDashboard> V_UBNDashboard { get; set; }
        public virtual DbSet<tbl_POMaster_PRE> tbl_POMaster_PRE { get; set; }
    }
}
