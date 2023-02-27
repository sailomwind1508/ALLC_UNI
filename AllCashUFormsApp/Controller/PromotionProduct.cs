using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace AllCashUFormsApp.Controller
{
    public class PromotionProduct : PromotionBase, IObject
    {
        public List<tbl_HQ_Promotion_Hit_Temp> GetAllData()
        {
            return (new tbl_HQ_Promotion_Hit_Temp()).SelectAll();
        }

        public virtual DataTable GetDataTable(bool isPopup = true)
        {
            DataTable newTable = new DataTable();
            try
            {
                List<tbl_HQ_Promotion_Hit_Temp> tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();
                tbl_HQ_Promotion_Hit_Temps = GetAllData();

                var proInfo = GetHQPromotion(a => tbl_HQ_Promotion_Hit_Temps.Select(x => x.PromotionID).Contains(a.PromotionID));
                if (proInfo.Count > 0)
                {
                    if (proInfo[0].PromotionType == "mmch")
                    {
                        tbl_HQ_Promotion_Hit_Temps = tbl_HQ_Promotion_Hit_Temps.Where(x => x.PromotionID == proInfo[0].PromotionID).ToList();
                    }
                }

                if (tbl_HQ_Promotion_Hit_Temps.Count > 0)
                {
                    var freeProducts = new tbl_Product().SelectAll();
                    var skuList = new tbl_HQ_SKUGroup().SelectAll();
                    skuList = skuList.Where(x => tbl_HQ_Promotion_Hit_Temps.Select(a => a.SKUGroupRewardID).Contains(x.SKUGroupID) || tbl_HQ_Promotion_Hit_Temps.Select(a => a.SKUGroupRewardID2).Contains(x.SKUGroupID)).ToList();
                    freeProducts = freeProducts.Where(x => skuList.Select(a => a.SKU_ID).Contains(x.ProductID)).ToList();
                    var mmchPro = tbl_HQ_Promotion_Hit_Temps.First();

                    var query = from p in freeProducts
                                select new
                                {
                                    Choose = p.ProductSubGroupID == 23 ? false : true,
                                    ProductID = p.ProductID,
                                    ProductName = p.ProductShortName,
                                    ProductQty = p.ProductSubGroupID == 23 ? 0 : mmchPro.SKUGroupRewardAmt.Value, //premium = 0 , non premium = SKUGroupRewardAmt edit by sailom 29/11/2021
                                    Seq = p.Seq,
                                    PromotionID = tbl_HQ_Promotion_Hit_Temps[0].PromotionID,
                                    ProductSubGroupID = p.ProductSubGroupID
                                };

                    newTable = query.ToList().ToDataTable();
                    newTable.Clear();

                    var data = query.OrderBy(x => x.Seq).ToList();
                    foreach (var r in data)
                    {
                        newTable.Rows.Add(r.Choose, r.ProductID, r.ProductName, r.ProductQty, r.Seq, r.PromotionID, r.ProductSubGroupID);
                    }
                }

                return newTable;

            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public virtual DataTable GetDataTableByCondition(string[] filters)
        {
            DataTable dt = new DataTable();
            List<tbl_HQ_Promotion_Hit_Temp> tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();

            if (filters != null)
            {
                tbl_HQ_Promotion_Hit_Temps = (new tbl_HQ_Promotion_Hit_Temp()).SelectAll().Where(x => filters.Contains(x.PromotionID)).ToList();
                dt = tbl_HQ_Promotion_Hit_Temps.ToDataTable();
            }
            else
            {
                dt = GetDataTable();
            }

            return dt;
        }

    }
}
