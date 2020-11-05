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
    }
}
