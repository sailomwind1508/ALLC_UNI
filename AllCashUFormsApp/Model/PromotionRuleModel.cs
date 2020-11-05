using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Model
{
    public class PromotionRuleModel
    {
        public string PromotionID { get; set; }
        public int UseAmount { get; set; }
        public int ConditionAmount { get; set; }
        public string ProductGroupID { get; set; }
        public decimal ProductGroupBeforeCalc { get; set; }
        public decimal ProductGroupAfterCalc { get; set; }
        public string DisCountPattern { get; set; }
        public decimal DisCountAmt { get; set; }
        public string PruductGroupRewardID { get; set; }
        public int PruductGroupRewardAmt { get; set; }
        public string RewardID { get; set; }
        
    }
}
