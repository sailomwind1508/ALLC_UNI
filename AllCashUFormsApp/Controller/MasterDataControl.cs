using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class MasterDataControl : BaseControl
    {
        private Func<tbl_PRMaster, bool> _docTypePredicate = null;

        public virtual Func<tbl_PRMaster, bool> docTypePredicate
        {
            get { return _docTypePredicate; }
            set
            {
                _docTypePredicate = value;
            }
        }

        public MasterDataControl() : base("")
        {
            _docTypePredicate = (x => x.DocTypeCode == "");
        }

        //public bool CallSendAllPromotionInfo(Dictionary<string, object> _params)//
        //{
        //    bool ret = false;
        //    try
        //    {
        //        return new tbl_HQ_Promotion().CallSendAllPromotionInfo(_params);
        //    }
        //    catch (Exception ex)
        //    {
        //        ret = false;
        //    }
        //    return ret;
        //}

        public DataTable GetApSupplierTypeData(int flagDel, string text)
        {
            return new tbl_ApSupplierType().GetApSupplierTypeData(flagDel, text);
        }

        public int UpdateApSupplierTypeData(tbl_ApSupplierType tbl_ApSupplierType)
        {
            return tbl_ApSupplierType.Update();
        }

        public DataTable GetApSupplierData(int flagDel, string text)
        {
            return new tbl_ApSupplier().GetApSupplierData(flagDel, text);
        }

        public int UpdateApSupplierData(tbl_ApSupplier tbl_ApSupplier)
        {
            return tbl_ApSupplier.Update();
        }

        public DataTable GetPositionData(int flagDel, string Search)
        {
            return (new tbl_Position()).GetPositionData(flagDel, Search);
        }

        public int UpdatePositionData(tbl_Position tbl_Position)
        {
            return tbl_Position.Update();
        }

        public int UpdateDepartmentData(tbl_Department tbl_Department)
        {
            return tbl_Department.Update();
        }

        public DataTable GetDepartmentData(int flagDel, string Search)
        {
            return (new tbl_Department()).GetDepartmentData(flagDel, Search);
        }

        public int UpdateProductUomData(tbl_ProductUom tbl_ProductUom)
        {
            return tbl_ProductUom.Update();
        }

        public DataTable GetProductUomData(int flagDel, string Search)
        {
            return (new tbl_ProductUom()).GetProductUomData(flagDel, Search);
        }

        public DataTable GetProductRemarkRejectData(string search, int flagDel)
        {
            return (new tbl_ProductRemarkReject()).GetProductRemarkRejectData(search, flagDel);
        }

        public int UpdateProductRemarkRejectData(tbl_ProductRemarkReject tbl_ProductRemarkReject)
        {
            return tbl_ProductRemarkReject.Update();
        }

        public DataTable GetDocumentTypeData(string Search)
        {
            return (new tbl_DocumentType()).GetDocumentTypeData(Search);
        }

        public int UpdateDocumentTypeData(tbl_DocumentType tbl_DocumentType)
        {
            return tbl_DocumentType.Update();
        }

        public DataTable GetDocRunning(string DocTypeCode, string YearDoc, string MonthDoc)
        {
            return (new tbl_DocRunning()).GetDocRunning(DocTypeCode, YearDoc, MonthDoc);
        }

        public DataTable GetBranchData()
        {
            try
            {
                List<tbl_Branch> tbl_Branchs = new List<tbl_Branch>();
                tbl_Branchs = (new tbl_Branch()).SelectAll();

                return tbl_Branchs.ToDataTable();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable GetPriceGroupData(string Search, int flagDel)
        {
            return (new tbl_PriceGroup()).GetPriceGroupDataGridView(Search, flagDel);
        }

        public DataTable GetPrdUomSetData(int ProductGroupID, int ProductSubGroupID, string ProductID, bool flagPrdPriceTable)
        {
            return (new tbl_ProductUomSet()).GetPrdUomSetData(ProductGroupID, ProductSubGroupID, ProductID, flagPrdPriceTable);
        }

        public int UpdateProductPriceGroupData(tbl_ProductPriceGroup tbl_ProductPriceGroup)
        {
            return tbl_ProductPriceGroup.Update();
        }

        public int RemoveProductPriceGroup(tbl_ProductPriceGroup tbl_ProductPriceGroup)
        {
            return tbl_ProductPriceGroup.Delete();
        }

        public int UpdatePriceGroupData(tbl_PriceGroup tbl_PriceGroup)
        {
            return tbl_PriceGroup.Update();
        }

        public DataTable GetDisplayImageData(int flagDel, string search)
        {
            return new tbl_DisplayImage().GetDisplayImageData(flagDel, search);
        }

        public int UpdateDisplayImageData(tbl_DisplayImage tbl_DisplayImage)
        {
            return tbl_DisplayImage.Update();
        }

        public DataTable GetProductFlavourData(int flagDel, string Search)
        {
            return (new tbl_ProductFlavour()).GetProductFlavourData(flagDel, Search);
        }

        public int UpdateProductFlavourData(tbl_ProductFlavour tbl_ProductFlavour)
        {
            return tbl_ProductFlavour.Update();
        }

        public DataTable GetSendData_Product(Dictionary<string, object> _params)
        {
            try
            {
                return new tbl_Product().GetSendData_Product(_params);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable Get_proc_SendProductInfo_GetDataTable()//
        {
            try
            {
                return new tbl_Branch().Get_proc_SendProductInfo_GetDataTable();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool CallSendDataProduct(Dictionary<string, object> _params)//
        {
            bool ret = false;
            try
            {
                return new tbl_Product().CallSendDataProduct(_params);
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }

        public DataTable GetTLData_TL_POMaster1(Dictionary<string, object> _params)//
        {
            try
            {
                return new tbl_POMaster().GetTLData_TL_POMaster1(_params);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetTLData_TL_POMaster2(Dictionary<string, object> _params)//
        {
            try
            {
                return new tbl_POMaster().GetTLData_TL_POMaster2(_params);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetTL_data_tbl_TL_PODetail(Dictionary<string, object> _params)//
        {
            try
            {
                return new tbl_POMaster().GetTL_data_tbl_TL_PODetail(_params);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetTL_data_tbl_TL_ArCustomerShelf(Dictionary<string, object> _params)//
        {
            try
            {
                return new tbl_POMaster().GetTL_data_tbl_TL_ArCustomerShelf(_params);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetTL_data_tbl_TL_CustomerCode(Dictionary<string, object> _params)//
        {
            try
            {
                return new tbl_POMaster().GetTL_data_tbl_TL_CustomerCode(_params);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetTL_data_tbl_TL_Visit(Dictionary<string, object> _params)//
        {
            try
            {
                return new tbl_POMaster().GetTL_data_tbl_TL_Visit(_params);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetTL_data_tbl_TL_VisitStock(Dictionary<string, object> _params)//
        {
            try
            {
                return new tbl_POMaster().GetTL_data_tbl_TL_VisitStock(_params);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetTL_data_tbl_TL_ArCustomer(Dictionary<string, object> _params)//
        {
            try
            {
                return new tbl_POMaster().GetTL_data_tbl_TL_ArCustomer(_params);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetHQ_Promotion_MasterData(string search)
        {
            return new tbl_HQ_Promotion_Master().GetHQ_Promotion_MasterData(search);
        }//

        public int UpdateHQ_Promotion_MasterData(tbl_HQ_Promotion_Master tbl_HQ_Promotion_Master)//new
        {
            return tbl_HQ_Promotion_Master.Update();
        }

        public DataTable GetHQ_RewardData(string search)
        {
            return new tbl_HQ_Reward().GetHQ_RewardData(search);
        }//

        public int UpdateHQ_RewardData(tbl_HQ_Reward tbl_HQ_Reward)
        {
            return tbl_HQ_Reward.Update();
        }//

        public DataTable GetHQ_SKUGroupData(string search)
        {
            return new tbl_HQ_SKUGroup().GetHQ_SKUGroupData(search);
        }//

        public int UpdateHQ_SKUGroupData(tbl_HQ_SKUGroup tbl_HQ_SKUGroup)
        {
            return tbl_HQ_SKUGroup.Update();
        }//new

        public DataTable GetHQ_SKUGroup_ExcData(string search)
        {
            return new tbl_HQ_SKUGroup_EXC().GetHQ_SKUGroup_ExcData(search);
        }//

        public int UpdateHQ_SKUGroup_ExcData(tbl_HQ_SKUGroup_EXC tbl_HQ_SKUGroup_EXC, string OldSKU_ID)
        {
            return tbl_HQ_SKUGroup_EXC.UpdateSKUGroupExc(OldSKU_ID);
        }//

        public int UpdateSKUGroup_ExcData(tbl_HQ_SKUGroup_EXC tbl_HQ_SKUGroup_EXC)
        {
            return tbl_HQ_SKUGroup_EXC.Update();
        }//

        public DataTable GetHQ_PromotionData(string search)
        {
            return new tbl_HQ_Promotion().GetHQ_PromotionData(search);
        }//

        public List<tbl_HQ_Promotion> GetSelectHQPromotion(string PromotionID = "")
        {
            return new tbl_HQ_Promotion().SelectPromotion(PromotionID);
        }

        public int UpdateHQPromotionData(tbl_HQ_Promotion tbl_HQ_Promotion)
        {
            return tbl_HQ_Promotion.Update();
        }

        public List<tbl_HQ_Promotion_Master> GetSelectHQPromotionID_Master(string PromotionID)
        {
            return new tbl_HQ_Promotion_Master().SelectPromotionID_Master(PromotionID);
        }

        public bool CallSendAllPromotionInfo(Dictionary<string, object> _params)//
        {
            bool ret = false;
            try
            {
                return new tbl_HQ_Promotion().CallSendAllPromotionInfo(_params);
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }

        public bool CallSendAllProductPriceGroupData(Dictionary<string, object> _params)//
        {
            bool ret = false;
            try
            {
                return new tbl_ProductPriceGroup().CallSendAllProductPriceGroupData(_params);
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }
    }
}
