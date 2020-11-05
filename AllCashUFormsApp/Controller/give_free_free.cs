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
        public PromotionRuleModel Give(tbl_HQ_Promotion pro, decimal totalUnitAmt, int roundHit)
        {
            PromotionRuleModel give = Give_Reward;
            try
            {
                give.PromotionID = pro.PromotionID;
                give.ProductGroupID = pro.SKUGroupID1;
                give.ProductGroupBeforeCalc = totalUnitAmt;
                give.ConditionAmount = pro.ConditionStart.Value;
                give.DisCountPattern = pro.DisCountPattern;
                //give.DisCountAmt = (totalUnitAmt - (totalUnitAmt * (pro.DisCountAmt.Value / 100))) * roundHit;
                give.RewardID = pro.RewardID;
                give.PruductGroupRewardID = pro.PruductGroupRewardID;
                give.PruductGroupRewardAmt = pro.PruductGroupRewardAmt.Value * roundHit;

                give.UseAmount = pro.ConditionStart.Value * roundHit;

                give.ProductGroupAfterCalc = totalUnitAmt;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return give;
        }
    }
}
