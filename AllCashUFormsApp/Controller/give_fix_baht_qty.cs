using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class give_fix_baht_qty : give_reward, Igive_reward
    {
        public PromotionRuleModel Give(tbl_HQ_Promotion pro, decimal totalUnitAmt, int roundHit, bool isPeriod, ProductPromotionModel pp = null)
        {
            PromotionRuleModel give = Give_Reward;
            try
            {
                decimal _disCountAmt = (pro.ConditionStart.Value - pro.DisCountAmt.Value) * roundHit;
                int _useAmount = pro.ConditionStart.Value * roundHit;
                decimal _productGroupAfterCalc = give.ProductGroupBeforeCalc - give.DisCountAmt;

                SetSubGive(give, pro, totalUnitAmt, roundHit, pro.ConditionStart.Value, _disCountAmt, _useAmount, _productGroupAfterCalc, pp);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return give;
        }
    }
}
