using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    interface Igive_reward
    {
        PromotionRuleModel Give(tbl_HQ_Promotion pro, decimal totalUnitAmt, int roundHit, bool isPeriod, ProductPromotionModel pp = null);
    }
}
