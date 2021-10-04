using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class give_reward
    {
        private PromotionRuleModel _give_reward;
        public PromotionRuleModel Give_Reward { get { return _give_reward; } set { _give_reward = value; } }

        public give_reward()
        {
            _give_reward = new PromotionRuleModel();
        }

        public void SetSubGive(PromotionRuleModel give, tbl_HQ_Promotion pro, decimal totalUnitAmt,
            int roundHit, int conditionAmount, decimal disCountAmt, int useAmount, decimal productGroupAfterCalc, ProductPromotionModel pp = null)
        {
            give.PromotionID = pro.PromotionID;
            give.ProductGroupID = pro.SKUGroupID1;
            give.PromotionType = pro.PromotionType;
            give.ProductGroupBeforeCalc = totalUnitAmt;
            give.ConditionAmount = conditionAmount;
            give.DisCountPattern = pro.DisCountPattern;
            give.DisCountAmt = disCountAmt;
            give.RewardID = pro.RewardID;
            give.RoundHit = roundHit;
            give.PromotionPattern = pro.PromotionPattern;
            give.StepCondition = pro.StepCondition1;
            give.IsUseCoupon = pro.IsUseCoupon != null ? pro.IsUseCoupon.Value : false;
            //give.IsUseStep = pro.IsUseStep != null ? pro.IsUseStep.Value : false;

            if (pro.PromotionPattern.ToLower() == "prd")
                give.IgnoreApplied = pro.IgnoreApplied.Value;

            give.ProductGroupAfterCalc = productGroupAfterCalc;

            var use = new PromotionUseModel();
            if (pp != null)
            {
                use.PromotionID = pro.PromotionID;
                use.UseType = pro.StepCondition1;
                use.UseAmount = useAmount;
                use.UseQuantity = pp.TotalQty;
                use.UsePrice = pp.TotalPrice;
                use.UseSKUAmt = pp.SkuAmt;
            }

            give.Use = use;
        }

    }
}
