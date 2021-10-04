using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace AllCashUFormsApp.Controller
{
    public class give_free_free : give_reward, Igive_reward
    {
        public PromotionRuleModel Give(tbl_HQ_Promotion pro, decimal totalUnitAmt, int roundHit, bool isPeriod, ProductPromotionModel pp = null)
        {
            PromotionRuleModel give = Give_Reward;
            try
            {
                decimal _disCountAmt = 0;
                int _useAmount = 0;
                if (pro.ConditionEnd != null)
                {
                    _useAmount = pro.ConditionEnd.Value * roundHit;
                }
                if (pro.ConditionStart != null)
                {
                    _useAmount = pro.ConditionStart.Value * roundHit;
                }

                decimal _productGroupAfterCalc = give.ProductGroupBeforeCalc - give.DisCountAmt;

                SetSubGive(give, pro, totalUnitAmt, roundHit, pro.ConditionStart.Value, _disCountAmt, _useAmount, totalUnitAmt, pp);
                if (pro.ConditionEnd != null)
                {
                    give.ConditionAmount = pro.ConditionEnd.Value;
                }
                if (pro.ConditionStart != null)
                {
                    give.ConditionAmount = pro.ConditionStart.Value;
                }

                give.PruductGroupRewardID = pro.PruductGroupRewardID;
                give.PruductGroupRewardAmt = pro.PruductGroupRewardAmt.Value * roundHit;

                if (pro.PruductGroupRewardAmt2 != null)
                {
                    give.PruductGroupRewardID2 = pro.PruductGroupRewardID2;
                    give.PruductGroupRewardAmt2 = pro.PruductGroupRewardAmt2.Value * roundHit;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return give;
        }
    }
}
