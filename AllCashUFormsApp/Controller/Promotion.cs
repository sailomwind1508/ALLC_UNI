using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace AllCashUFormsApp.Controller
{
    public class Promotion : PromotionBase, IObject
    {
        public List<PromotionRuleModel> CalculatePromotion(List<tbl_PODetail_PRE> productList)
        {
            List<PromotionRuleModel> hitProList = new List<PromotionRuleModel>();

            try
            {

                var cDate = DateTime.Now;

                PRDPromotion prdPro = new PRDPromotion();
                prdPro.PreCalcPromotion(this, productList, hitProList);

                TXNPromotion txnPro = new TXNPromotion();
                txnPro.PreCalcPromotion(this, productList, hitProList);

            }
            catch (Exception ex)
            {
                hitProList = null;
                ex.WriteLog(this.GetType());
            }

            return hitProList;
        }

        public List<PromotionRuleModel> CalculatePromotion(List<tbl_PODetail> productList, string customerID = "", string whid = "")
        {
            List<PromotionRuleModel> hitProList = new List<PromotionRuleModel>();

            try
            {

                var cDate = DateTime.Now;

                PRDPromotion prdPro = new PRDPromotion();
                prdPro.PreCalcPromotion(this, productList, hitProList, customerID, whid);

                TXNPromotion txnPro = new TXNPromotion();
                txnPro.PreCalcPromotion(this, productList, hitProList, customerID, whid);

            }
            catch (Exception ex)
            {
                hitProList = null;
                ex.WriteLog(this.GetType());
            }

            return hitProList;
        }

        public List<tbl_HQ_Promotion_Hit> GetAllData(Func<tbl_HQ_Promotion_Hit, bool> predicate = null)
        {
            if (predicate == null)
                return (new tbl_HQ_Promotion_Hit()).SelectAll();
            else
                return (new tbl_HQ_Promotion_Hit()).Select(predicate);
        }

        public virtual int AddData(tbl_HQ_Promotion_Hit tbl_HQ_Promotion_Hit)
        {
            return tbl_HQ_Promotion_Hit.Insert();
        }

        public int UpdateData(tbl_HQ_Promotion_Hit tbl_HQ_Promotion_Hit)
        {
            return tbl_HQ_Promotion_Hit.Update();
        }

        public int RemoveData(tbl_HQ_Promotion_Hit tbl_HQ_Promotion_Hit)
        {
            return tbl_HQ_Promotion_Hit.Delete();
        }

        public virtual DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                List<tbl_HQ_Promotion_Hit> tbl_HQ_Promotion_Hits = new List<tbl_HQ_Promotion_Hit>();
                tbl_HQ_Promotion_Hits = (new tbl_HQ_Promotion_Hit()).SelectAll();
                var allOfPromotionIDs = tbl_HQ_Promotion_Hits.Select(a => a.PromotionID).ToList();
                var allOfRewardIDs = tbl_HQ_Promotion_Hits.Select(a => a.RewardID).ToList();

                List<tbl_HQ_Promotion_Master> tbl_HQ_Promotion_Masters = new List<tbl_HQ_Promotion_Master>();
                Func<tbl_HQ_Promotion_Master, bool> proMTFunc = (x => allOfPromotionIDs.Contains(x.PromotionID));
                tbl_HQ_Promotion_Masters = (new tbl_HQ_Promotion_Master()).Select(proMTFunc);

                List<tbl_HQ_Promotion> tbl_HQ_Promotions = new List<tbl_HQ_Promotion>();
                Func<tbl_HQ_Promotion, bool> proFunc = (x => allOfPromotionIDs.Contains(x.PromotionID));
                tbl_HQ_Promotions = (new tbl_HQ_Promotion()).Select(proFunc);

                List<tbl_HQ_Reward> tbl_HQ_Rewards = new List<tbl_HQ_Reward>();
                Func<tbl_HQ_Reward, bool> rewardFunc = (x => allOfRewardIDs.Contains(x.RewardID));
                tbl_HQ_Rewards = (new tbl_HQ_Reward()).Select(rewardFunc);

                var query = from pt in tbl_HQ_Promotion_Hits
                            join pm in tbl_HQ_Promotion_Masters on pt.PromotionID equals pm.PromotionID
                            join p in tbl_HQ_Promotions on pt.PromotionID equals p.PromotionID
                            join r in tbl_HQ_Rewards on pt.RewardID equals r.RewardID
                            select new
                            {
                                No = 0,
                                DocNo = pt.DocNo,
                                PromotionID = pt.PromotionID,
                                PromotionName = pm.PromotionName,
                                SKUGroupID = pt.SKUGroupID,
                                DisCountAmt = pt.DisCountAmt,
                                SKUGroupRewardID = pt.SKUGroupRewardID,
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
                    newTable.Rows.Add(index, r.DocNo, r.PromotionID, r.PromotionName, r.SKUGroupID, r.DisCountAmt, r.SKUGroupRewardID, r.SKUGroupRewardAmt,
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
            List<tbl_HQ_Promotion_Hit> tbl_HQ_Promotion_Hits = new List<tbl_HQ_Promotion_Hit>();

            if (filters != null)
            {
                tbl_HQ_Promotion_Hits = (new tbl_HQ_Promotion_Hit()).SelectAll().Where(x => filters.Contains(x.PromotionID)).ToList();
                dt = tbl_HQ_Promotion_Hits.ToDataTable();
            }
            else
            {
                dt = GetDataTable();
            }

            return dt;
        }

    }
}
