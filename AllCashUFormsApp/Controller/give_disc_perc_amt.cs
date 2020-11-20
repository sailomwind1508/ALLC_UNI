using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class give_disc_perc_amt : give_reward, Igive_reward
    {
        public PromotionRuleModel Give(tbl_HQ_Promotion pro, decimal totalUnitAmt, int roundHit, bool isPeriod, ProductPromotionModel pp = null)
        {
            PromotionRuleModel give = Give_Reward;
            try
            {
                decimal totalDiscount = 0;

                if (isPeriod)
                {
                    totalDiscount = totalUnitAmt * (pro.DisCountAmt.Value / 100);
                }
                else
                {
                    for (int i = 0; i < roundHit; i++)
                    {
                        decimal _disCountAmt = Convert.ToDecimal(pro.ConditionStart) * (pro.DisCountAmt.Value / 100);
                        totalDiscount += _disCountAmt;
                    }
                }

                int _useAmount = pro.ConditionStart.Value * roundHit;
                decimal _productGroupAfterCalc = give.ProductGroupBeforeCalc - give.DisCountAmt;

                SetSubGive(give, pro, totalUnitAmt, roundHit, pro.ConditionStart.Value, totalDiscount, _useAmount, _productGroupAfterCalc, pp);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return give;
        }
    }
}
