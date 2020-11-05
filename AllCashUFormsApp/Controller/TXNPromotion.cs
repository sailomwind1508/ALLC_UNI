using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class TXNPromotion : PromotionBase
    {
        public void CalcPromotionPRD(tbl_HQ_Promotion pro, List<string> productGroup, decimal totalPrice, decimal totalQty, decimal skuAmt)
        {
            try
            {
                decimal total = 0;
                switch (pro.StepCondition1)
                {
                    case "qty": total = totalQty; break;
                    case "amt": total = totalPrice; break;
                    case "sku": total = skuAmt; break;
                    default:
                        break;
                }

                PromotionRuleModel reward = new PromotionRuleModel();

                if (string.IsNullOrEmpty(pro.ConditionStart.ToString()) &&
                    (pro.ConditionEnd == null || string.IsNullOrEmpty(pro.ConditionEnd.ToString()))) //normal
                {
                    if (total >= pro.ConditionStart)
                    {
                        reward = CalcNormalPromotion(pro, total);
                    }
                }
                else //period amount
                {
                    if (string.IsNullOrEmpty(pro.ConditionStart.ToString()) &&
                        string.IsNullOrEmpty(pro.ConditionEnd.ToString()) &&
                        (total >= pro.ConditionStart && total <= pro.ConditionEnd))
                    {
                        reward = CalcPeriodPromotion(pro, total);

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void VerifyIgnorApplied(List<PromotionRuleModel> promotionList, tbl_HQ_Promotion pro, decimal total, bool isPeriod)
        {
            try
            {
                if (!pro.IgnoreApplied.Value) //IgnoreApplied = false
                {
                    if (promotionList.Any(x => x.ProductGroupID == pro.SKUGroupID1)) //has hit promotion
                    {
                        decimal proAmt = promotionList.Sum(x => x.ConditionAmount);
                        if (proAmt > 0)
                        {
                            decimal totalDueAmt = Convert.ToDecimal(pro.ConditionEnd.Value) - proAmt;
                        }
                    }
                }
                else //IgnoreApplied = true
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private PromotionRuleModel CalcNormalPromotion(tbl_HQ_Promotion pro, decimal total)
        {
            PromotionRuleModel reward = new PromotionRuleModel();
            try
            {
                int roundHit = 0;
                roundHit = Convert.ToInt32(total / pro.ConditionStart);


                switch (pro.DisCountPattern)
                {
                    case "disc_baht_qty":
                        {
                            give_disc_baht_qty give = new give_disc_baht_qty();
                            give.Give(pro, total, roundHit);
                            reward = give.Give_Reward;
                        }
                        break;
                    case "fix_baht_qty":
                        {
                            give_fix_baht_qty give = new give_fix_baht_qty();
                            give.Give(pro, total, roundHit);
                            reward = give.Give_Reward;
                        }
                        break;
                    case "disc_perc_amt":
                        {
                            give_disc_perc_amt give = new give_disc_perc_amt();
                            give.Give(pro, total, roundHit);
                            reward = give.Give_Reward;
                        }
                        break;
                    case "free=free":
                        {
                            give_free_free give = new give_free_free();
                            give.Give(pro, total, roundHit);
                            reward = give.Give_Reward;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return reward;
        }

        private PromotionRuleModel CalcPeriodPromotion(tbl_HQ_Promotion pro, decimal total)
        {
            PromotionRuleModel reward = new PromotionRuleModel();
            try
            {
                int roundHit = 0;
                if (total <= pro.ConditionEnd)
                {
                    roundHit = 1;
                }
                else if (total > pro.ConditionEnd)
                {
                    roundHit = Convert.ToInt32(total / pro.ConditionEnd);
                }

                switch (pro.DisCountPattern)
                {
                    case "disc_baht_qty":
                        {
                            give_disc_baht_qty give = new give_disc_baht_qty();
                            give.Give(pro, total, roundHit);
                            reward = give.Give_Reward;
                        }
                        break;
                    case "fix_baht_qty":
                        {
                            give_fix_baht_qty give = new give_fix_baht_qty();
                            give.Give(pro, total, roundHit);
                            reward = give.Give_Reward;
                        }
                        break;
                    case "disc_perc_amt":
                        {
                            give_disc_perc_amt give = new give_disc_perc_amt();
                            give.Give(pro, total, roundHit);
                            reward = give.Give_Reward;
                        }
                        break;
                    case "free=free":
                        {
                            give_free_free give = new give_free_free();
                            give.Give(pro, total, roundHit);
                            reward = give.Give_Reward;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return reward;
        }
    }
}
