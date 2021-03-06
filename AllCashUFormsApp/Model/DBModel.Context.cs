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
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbl_AdmAuthorize> tbl_AdmAuthorize { get; set; }
        public virtual DbSet<tbl_AdmControlList> tbl_AdmControlList { get; set; }
        public virtual DbSet<tbl_AdmFormList> tbl_AdmFormList { get; set; }
        public virtual DbSet<tbl_AdmMenuList> tbl_AdmMenuList { get; set; }
        public virtual DbSet<tbl_AdmRoleControl> tbl_AdmRoleControl { get; set; }
        public virtual DbSet<tbl_ApSupplier> tbl_ApSupplier { get; set; }
        public virtual DbSet<tbl_ApSupplierType> tbl_ApSupplierType { get; set; }
        public virtual DbSet<tbl_ArCustomerShelf> tbl_ArCustomerShelf { get; set; }
        public virtual DbSet<tbl_ArCustomerType> tbl_ArCustomerType { get; set; }
        public virtual DbSet<tbl_Banknote> tbl_Banknote { get; set; }
        public virtual DbSet<tbl_Branch> tbl_Branch { get; set; }
        public virtual DbSet<tbl_BranchGroup> tbl_BranchGroup { get; set; }
        public virtual DbSet<tbl_BranchLog> tbl_BranchLog { get; set; }
        public virtual DbSet<tbl_BranchRental> tbl_BranchRental { get; set; }
        public virtual DbSet<tbl_BranchSupervisor> tbl_BranchSupervisor { get; set; }
        public virtual DbSet<tbl_BranchTerritory> tbl_BranchTerritory { get; set; }
        public virtual DbSet<tbl_BranchWarehouse> tbl_BranchWarehouse { get; set; }
        public virtual DbSet<tbl_BranchWarehouseData> tbl_BranchWarehouseData { get; set; }
        public virtual DbSet<tbl_Cause> tbl_Cause { get; set; }
        public virtual DbSet<tbl_CfgConnection> tbl_CfgConnection { get; set; }
        public virtual DbSet<tbl_CfgKeyField> tbl_CfgKeyField { get; set; }
        public virtual DbSet<tbl_CfgPosMachine> tbl_CfgPosMachine { get; set; }
        public virtual DbSet<tbl_CfgSetting> tbl_CfgSetting { get; set; }
        public virtual DbSet<tbl_Company> tbl_Company { get; set; }
        public virtual DbSet<tbl_Department> tbl_Department { get; set; }
        public virtual DbSet<tbl_DisplayImage> tbl_DisplayImage { get; set; }
        public virtual DbSet<tbl_DocRunning> tbl_DocRunning { get; set; }
        public virtual DbSet<tbl_DocSendUpdate> tbl_DocSendUpdate { get; set; }
        public virtual DbSet<tbl_DocSignature> tbl_DocSignature { get; set; }
        public virtual DbSet<tbl_DocumentStatus> tbl_DocumentStatus { get; set; }
        public virtual DbSet<tbl_DocumentType> tbl_DocumentType { get; set; }
        public virtual DbSet<tbl_Employee> tbl_Employee { get; set; }
        public virtual DbSet<tbl_error_logs> tbl_error_logs { get; set; }
        public virtual DbSet<tbl_ErrorLog> tbl_ErrorLog { get; set; }
        public virtual DbSet<tbl_Expense> tbl_Expense { get; set; }
        public virtual DbSet<tbl_InvMovement> tbl_InvMovement { get; set; }
        public virtual DbSet<tbl_InvTransaction> tbl_InvTransaction { get; set; }
        public virtual DbSet<tbl_InvWarehouse> tbl_InvWarehouse { get; set; }
        public virtual DbSet<tbl_IVDetail> tbl_IVDetail { get; set; }
        public virtual DbSet<tbl_IVMaster> tbl_IVMaster { get; set; }
        public virtual DbSet<tbl_LtyQuestion> tbl_LtyQuestion { get; set; }
        public virtual DbSet<tbl_LytAnswer> tbl_LytAnswer { get; set; }
        public virtual DbSet<tbl_LytChoice> tbl_LytChoice { get; set; }
        public virtual DbSet<tbl_LytCustTypeReward> tbl_LytCustTypeReward { get; set; }
        public virtual DbSet<tbl_LytMember> tbl_LytMember { get; set; }
        public virtual DbSet<tbl_LytPointBonus> tbl_LytPointBonus { get; set; }
        public virtual DbSet<tbl_LytPointByProduct> tbl_LytPointByProduct { get; set; }
        public virtual DbSet<tbl_LytPointCycle> tbl_LytPointCycle { get; set; }
        public virtual DbSet<tbl_LytPointMovement> tbl_LytPointMovement { get; set; }
        public virtual DbSet<tbl_LytPointType> tbl_LytPointType { get; set; }
        public virtual DbSet<tbl_LytPointTypePerBill> tbl_LytPointTypePerBill { get; set; }
        public virtual DbSet<tbl_LytRedeem> tbl_LytRedeem { get; set; }
        public virtual DbSet<tbl_LytReward> tbl_LytReward { get; set; }
        public virtual DbSet<tbl_MstArea> tbl_MstArea { get; set; }
        public virtual DbSet<tbl_MstCycle> tbl_MstCycle { get; set; }
        public virtual DbSet<tbl_MstDistrict> tbl_MstDistrict { get; set; }
        public virtual DbSet<tbl_MstMenu> tbl_MstMenu { get; set; }
        public virtual DbSet<tbl_MstPart> tbl_MstPart { get; set; }
        public virtual DbSet<tbl_MstProvince> tbl_MstProvince { get; set; }
        public virtual DbSet<tbl_PaidDetail> tbl_PaidDetail { get; set; }
        public virtual DbSet<tbl_PaidMaster> tbl_PaidMaster { get; set; }
        public virtual DbSet<tbl_PayDetail> tbl_PayDetail { get; set; }
        public virtual DbSet<tbl_PayMaster> tbl_PayMaster { get; set; }
        public virtual DbSet<tbl_PODetail> tbl_PODetail { get; set; }
        public virtual DbSet<tbl_POMaster> tbl_POMaster { get; set; }
        public virtual DbSet<tbl_Position> tbl_Position { get; set; }
        public virtual DbSet<tbl_PRDetail> tbl_PRDetail { get; set; }
        public virtual DbSet<tbl_PreSaleWarehouse> tbl_PreSaleWarehouse { get; set; }
        public virtual DbSet<tbl_PriceGroup> tbl_PriceGroup { get; set; }
        public virtual DbSet<tbl_PRMaster> tbl_PRMaster { get; set; }
        public virtual DbSet<tbl_Product> tbl_Product { get; set; }
        public virtual DbSet<tbl_ProductBrand> tbl_ProductBrand { get; set; }
        public virtual DbSet<tbl_ProductChanged> tbl_ProductChanged { get; set; }
        public virtual DbSet<tbl_ProductCompany> tbl_ProductCompany { get; set; }
        public virtual DbSet<tbl_ProductCostHistory> tbl_ProductCostHistory { get; set; }
        public virtual DbSet<tbl_ProductCostSupplier> tbl_ProductCostSupplier { get; set; }
        public virtual DbSet<tbl_ProductFlavour> tbl_ProductFlavour { get; set; }
        public virtual DbSet<tbl_ProductGroup> tbl_ProductGroup { get; set; }
        public virtual DbSet<tbl_ProductPriceBranch> tbl_ProductPriceBranch { get; set; }
        public virtual DbSet<tbl_ProductPriceCustomer> tbl_ProductPriceCustomer { get; set; }
        public virtual DbSet<tbl_ProductPriceGroup> tbl_ProductPriceGroup { get; set; }
        public virtual DbSet<tbl_ProductPromotionBom> tbl_ProductPromotionBom { get; set; }
        public virtual DbSet<tbl_ProductRemarkReject> tbl_ProductRemarkReject { get; set; }
        public virtual DbSet<tbl_ProductSubGroup> tbl_ProductSubGroup { get; set; }
        public virtual DbSet<tbl_ProductType> tbl_ProductType { get; set; }
        public virtual DbSet<tbl_ProductUom> tbl_ProductUom { get; set; }
        public virtual DbSet<tbl_ProductUomSet> tbl_ProductUomSet { get; set; }
        public virtual DbSet<tbl_Promotion> tbl_Promotion { get; set; }
        public virtual DbSet<tbl_PromotionBranch> tbl_PromotionBranch { get; set; }
        public virtual DbSet<tbl_PromotionCustomer> tbl_PromotionCustomer { get; set; }
        public virtual DbSet<tbl_PromotionPack> tbl_PromotionPack { get; set; }
        public virtual DbSet<tbl_PromotionProduct> tbl_PromotionProduct { get; set; }
        public virtual DbSet<tbl_PromotionReward> tbl_PromotionReward { get; set; }
        public virtual DbSet<tbl_PromotionVanProduct> tbl_PromotionVanProduct { get; set; }
        public virtual DbSet<tbl_Roles> tbl_Roles { get; set; }
        public virtual DbSet<tbl_RouteOnsite> tbl_RouteOnsite { get; set; }
        public virtual DbSet<tbl_SalArea> tbl_SalArea { get; set; }
        public virtual DbSet<tbl_SalAreaDistrict> tbl_SalAreaDistrict { get; set; }
        public virtual DbSet<tbl_SalAreaVisit> tbl_SalAreaVisit { get; set; }
        public virtual DbSet<tbl_SaleBranchDaily> tbl_SaleBranchDaily { get; set; }
        public virtual DbSet<tbl_SaleBranchSummary> tbl_SaleBranchSummary { get; set; }
        public virtual DbSet<tbl_SaleExpenseDetail> tbl_SaleExpenseDetail { get; set; }
        public virtual DbSet<tbl_SaleExpenseMaster> tbl_SaleExpenseMaster { get; set; }
        public virtual DbSet<tbl_SaleYearTarget> tbl_SaleYearTarget { get; set; }
        public virtual DbSet<tbl_SendData> tbl_SendData { get; set; }
        public virtual DbSet<tbl_ShopType> tbl_ShopType { get; set; }
        public virtual DbSet<tbl_ShopTypeGroup> tbl_ShopTypeGroup { get; set; }
        public virtual DbSet<tbl_SimCustomer> tbl_SimCustomer { get; set; }
        public virtual DbSet<tbl_SimOrder> tbl_SimOrder { get; set; }
        public virtual DbSet<tbl_SimOrderItem> tbl_SimOrderItem { get; set; }
        public virtual DbSet<tbl_TargetMaster> tbl_TargetMaster { get; set; }
        public virtual DbSet<tbl_TL_ArCustomer_Test> tbl_TL_ArCustomer_Test { get; set; }
        public virtual DbSet<tbl_TL_ArCustomerShelf> tbl_TL_ArCustomerShelf { get; set; }
        public virtual DbSet<tbl_TL_DocSignature> tbl_TL_DocSignature { get; set; }
        public virtual DbSet<tbl_TL_PODetail> tbl_TL_PODetail { get; set; }
        public virtual DbSet<tbl_TL_POMaster> tbl_TL_POMaster { get; set; }
        public virtual DbSet<tbl_TL_PRDetail> tbl_TL_PRDetail { get; set; }
        public virtual DbSet<tbl_TL_PRMaster> tbl_TL_PRMaster { get; set; }
        public virtual DbSet<tbl_TL_PromotionCustomer> tbl_TL_PromotionCustomer { get; set; }
        public virtual DbSet<tbl_TL_Visit> tbl_TL_Visit { get; set; }
        public virtual DbSet<tbl_TL_VisitStock> tbl_TL_VisitStock { get; set; }
        public virtual DbSet<tbl_TLDetail> tbl_TLDetail { get; set; }
        public virtual DbSet<tbl_TLMaster> tbl_TLMaster { get; set; }
        public virtual DbSet<tbl_TmpPRDetail> tbl_TmpPRDetail { get; set; }
        public virtual DbSet<tbl_TRDetail> tbl_TRDetail { get; set; }
        public virtual DbSet<tbl_TRMaster> tbl_TRMaster { get; set; }
        public virtual DbSet<tbl_Users> tbl_Users { get; set; }
        public virtual DbSet<tbl_VanType> tbl_VanType { get; set; }
        public virtual DbSet<tbl_Visit> tbl_Visit { get; set; }
        public virtual DbSet<tbl_VisitStock> tbl_VisitStock { get; set; }
        public virtual DbSet<tbl_Zone> tbl_Zone { get; set; }
        public virtual DbSet<tbl_AmtArCustomer> tbl_AmtArCustomer { get; set; }
        public virtual DbSet<tbl_AmtArCustomerDetail> tbl_AmtArCustomerDetail { get; set; }
        public virtual DbSet<tbl_CfgFixedHolidays> tbl_CfgFixedHolidays { get; set; }
        public virtual DbSet<tbl_CfgFloatingHolidays> tbl_CfgFloatingHolidays { get; set; }
        public virtual DbSet<tbl_DelArCustomer> tbl_DelArCustomer { get; set; }
        public virtual DbSet<tbl_LytReportGroup> tbl_LytReportGroup { get; set; }
        public virtual DbSet<tbl_LytReportMenu> tbl_LytReportMenu { get; set; }
        public virtual DbSet<tbl_MstGrade> tbl_MstGrade { get; set; }
        public virtual DbSet<tbl_ProductPriceHistory> tbl_ProductPriceHistory { get; set; }
        public virtual DbSet<tbl_PromotionVan> tbl_PromotionVan { get; set; }
        public virtual DbSet<tbl_ReportGroup> tbl_ReportGroup { get; set; }
        public virtual DbSet<tbl_ReportMenu> tbl_ReportMenu { get; set; }
        public virtual DbSet<tbl_TargetDetail> tbl_TargetDetail { get; set; }
        public virtual DbSet<tbl_VMIDetail> tbl_VMIDetail { get; set; }
        public virtual DbSet<tbl_VMIMaster> tbl_VMIMaster { get; set; }
        public virtual DbSet<tbl_VMISafety> tbl_VMISafety { get; set; }
        public virtual DbSet<tbl_VMISetting> tbl_VMISetting { get; set; }
        public virtual DbSet<tbl_ArCustomer> tbl_ArCustomer { get; set; }
        public virtual DbSet<tbl_DiscountType> tbl_DiscountType { get; set; }
        public virtual DbSet<tbl_TL_ArCustomer> tbl_TL_ArCustomer { get; set; }
        public virtual DbSet<tbl_HQ_Promotion> tbl_HQ_Promotion { get; set; }
        public virtual DbSet<tbl_HQ_Promotion_Master> tbl_HQ_Promotion_Master { get; set; }
        public virtual DbSet<tbl_HQ_Reward> tbl_HQ_Reward { get; set; }
        public virtual DbSet<tbl_HQ_SKUGroup> tbl_HQ_SKUGroup { get; set; }
        public virtual DbSet<tbl_HQ_SKUGroup_EXC> tbl_HQ_SKUGroup_EXC { get; set; }
    }
}
