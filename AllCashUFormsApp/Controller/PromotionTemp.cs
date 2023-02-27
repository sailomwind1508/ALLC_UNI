using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace AllCashUFormsApp.Controller
{
    public class PromotionTemp : PromotionBase, IObject
    {
        public List<tbl_HQ_Promotion_Hit_Temp> GetAllData()
        {
            return (new tbl_HQ_Promotion_Hit_Temp()).SelectAll();
        }

        public virtual DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                List<tbl_HQ_Promotion_Hit_Temp> tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();
                tbl_HQ_Promotion_Hit_Temps = GetAllData();
                var allOfPromotionIDs = tbl_HQ_Promotion_Hit_Temps.Select(a => a.PromotionID).ToList();
                var allOfRewardIDs = tbl_HQ_Promotion_Hit_Temps.Select(a => a.RewardID).ToList();

                Func<tbl_HQ_Promotion_Master, bool> proMTFunc = (x => allOfPromotionIDs.Contains(x.PromotionID));
                List<tbl_HQ_Promotion_Master> tbl_HQ_Promotion_Masters = GetHQPromotionMaster(proMTFunc);

                //List<tbl_HQ_SKUGroup> tbl_HQ_SKUGroups = new List<tbl_HQ_SKUGroup>();
                //Func<tbl_HQ_SKUGroup, bool> grpFunc = (x => tbl_HQ_Promotion_Hit_Temps.Select(a => a.SKUGroupID).Contains(x.SKUGroupID));
                //tbl_HQ_SKUGroups = (new tbl_HQ_SKUGroup()).Select(grpFunc);

                //List<tbl_Product> tbl_Products = new List<tbl_Product>();
                //Func<tbl_Product, bool> prdFunc = (x => tbl_HQ_SKUGroups.Select(a => a.SKU_ID).Contains(x.ProductCode));
                //tbl_Products = (new tbl_Product()).Select(prdFunc);

                Func<tbl_HQ_Promotion, bool> proFunc = (x => allOfPromotionIDs.Contains(x.PromotionID));
                List<tbl_HQ_Promotion> tbl_HQ_Promotions = GetHQPromotion(proFunc);

                Func<tbl_HQ_Reward, bool> rewardFunc = (x => allOfRewardIDs.Contains(x.RewardID));
                List<tbl_HQ_Reward> tbl_HQ_Rewards = GetHQReward(rewardFunc);

                var query = from pt in tbl_HQ_Promotion_Hit_Temps
                            join pm in tbl_HQ_Promotion_Masters on pt.PromotionID equals pm.PromotionID
                            join p in tbl_HQ_Promotions on pt.PromotionID equals p.PromotionID
                            join r in tbl_HQ_Rewards on pt.RewardID equals r.RewardID
                            //join g in tbl_HQ_SKUGroups on pt.SKUGroupID equals g.SKUGroupID
                            //join prd in tbl_Products on g.SKU_ID equals prd.ProductCode
                            select new
                            {
                                Choose = false,
                                No = 0,
                                DocNo = pt.DocNo,
                                PromotionID = pt.PromotionID,
                                PromotionName = pm.PromotionName,
                                SKUGroupID = pt.SKUGroupID,
                                DisCountAmt = pt.DisCountAmt,
                                SKUGroupRewardID = pt.SKUGroupRewardID,
                                //SKUGroupRewardName = prd.ProductName,
                                SKUGroupRewardAmt = pt.SKUGroupRewardAmt,
                                RewardName = r.RewardName,
                                EffectiveDate = p.EffectiveDate,
                                ExpireDate = p.ExpireDate
                            };

                DataTable newTable = query.ToList().ToDataTable();
                newTable.Clear();

                var data = query.OrderBy(x => x.PromotionID).ToList();
                int index = 0;
                foreach (var r in data)
                {
                    index++;
                    newTable.Rows.Add(false, index, r.DocNo, r.PromotionID, r.PromotionName, r.SKUGroupID, r.DisCountAmt, r.SKUGroupRewardID, r.SKUGroupRewardAmt,
                        r.RewardName, r.EffectiveDate, r.ExpireDate);
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
