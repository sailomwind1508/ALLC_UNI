using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace AllCashUFormsApp.Controller
{
    public class PRDPromotion : PromotionBase
    {
        public List<PromotionRuleModel> CalcPromotionPRD(tbl_HQ_Promotion pro, List<string> productGroup, decimal totalPrice, decimal totalQty, decimal skuAmt)
        {
            List<PromotionRuleModel> promotionList = new List<PromotionRuleModel>();
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
                        reward = VerifyRecomputable(promotionList, pro, total, false);
                    }
                }
                else //period amount
                {
                    if (string.IsNullOrEmpty(pro.ConditionStart.ToString()) &&
                        string.IsNullOrEmpty(pro.ConditionEnd.ToString()) &&
                        (total >= pro.ConditionStart && total <= pro.ConditionEnd))
                    {
                        reward = VerifyRecomputable(promotionList, pro, total, true);
                    }
                }

                promotionList.Add(reward);
            }
            catch (Exception)
            {
                promotionList = new List<PromotionRuleModel>();
                throw;
            }

            return promotionList;
        }

        private PromotionRuleModel VerifyRecomputable(List<PromotionRuleModel> promotionList, tbl_HQ_Promotion pro, decimal total, bool isPeriod)
        {
            PromotionRuleModel reward = new PromotionRuleModel();
            try
            {
                if (pro.Recomputable.Value) //is recomputable = true
                {
                    reward = CalcPromotion(pro, total, isPeriod);
                }
                else //is recomputable = false
                {
                    if (promotionList.Any(x => x.ProductGroupID == pro.SKUGroupID1)) //has hit promotion
                    {
                        //Do nothing
                    }
                    else //no hit promotion
                    {
                        reward = CalcPromotion(pro, total, isPeriod);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return reward;
        }

        private PromotionRuleModel CalcPromotion(tbl_HQ_Promotion pro, decimal total, bool isPeriod = false)
        {
            PromotionRuleModel reward = new PromotionRuleModel();
            try
            {
                int roundHit = 0;

                if (isPeriod)
                {
                    if (total <= pro.ConditionEnd)
                        roundHit = 1;
                    else if (total > pro.ConditionEnd)
                        roundHit = Convert.ToInt32(total / pro.ConditionEnd);
                }
                else
                    roundHit = Convert.ToInt32(total / pro.ConditionStart);

                reward = PromotionFactory(pro, total, roundHit);
            }
            catch (Exception ex)
            {
                return null;
            }

            return reward;
        }

        private PromotionRuleModel PromotionFactory(tbl_HQ_Promotion pro, decimal total, int roundHit)
        {
            PromotionRuleModel ret = new PromotionRuleModel();
            give_reward give_reward = new give_reward();
            
            try
            {
                switch (pro.DisCountPattern)
                {
                    case "disc_baht_qty":
                        {
                            give_reward = new give_disc_baht_qty();
                            ret = ((give_disc_baht_qty)give_reward).Give(pro, total, roundHit);
                        }
                        break;
                    case "fix_baht_qty":
                        {
                            give_reward = new give_fix_baht_qty();
                            ret = ((give_fix_baht_qty)give_reward).Give(pro, total, roundHit);
                        }
                        break;
                    case "disc_perc_amt":
                        {
                            give_reward = new give_disc_perc_amt();
                            ret = ((give_disc_perc_amt)give_reward).Give(pro, total, roundHit);
                        }
                        break;
                    case "free=free":
                        {
                            give_reward = new give_free_free();
                            ret = ((give_free_free)give_reward).Give(pro, total, roundHit);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }

            return ret;
        }
    }
}
