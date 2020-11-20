using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class PromotionBase : BaseControl, IPromotion
    {
        private Func<tbl_HQ_Promotion_Hit, bool> _predicate = null;
        public virtual Func<tbl_HQ_Promotion_Hit, bool> Predicate
        {
            get { return _predicate; }
            set
            {
                _predicate = value;
            }
        }
        private List<tbl_HQ_Promotion_Hit> _tbl_HQ_Promotion_Hits = null;
        public virtual List<tbl_HQ_Promotion_Hit> tbl_HQ_Promotion_Hits
        {
            get { return _tbl_HQ_Promotion_Hits; }
            set
            {
                _tbl_HQ_Promotion_Hits = value;
            }
        }
        private List<tbl_HQ_Promotion_Hit_Temp> _tbl_HQ_Promotion_Hit_Temps = null;
        public virtual List<tbl_HQ_Promotion_Hit_Temp> tbl_HQ_Promotion_Hit_Temps
        {
            get { return _tbl_HQ_Promotion_Hit_Temps; }
            set
            {
                _tbl_HQ_Promotion_Hit_Temps = value;
            }
        }

        public PromotionBase() : base("Promotion")
        {
            _predicate = null;
        }

        public List<tbl_HQ_SKUGroup> GetHQSKUGroup(Func<tbl_HQ_SKUGroup, bool> condition)
        {
            List<tbl_HQ_SKUGroup> tbl_HQ_SKUGroups = new List<tbl_HQ_SKUGroup>();
            tbl_HQ_SKUGroups = (new tbl_HQ_SKUGroup()).Select(condition);

            return tbl_HQ_SKUGroups;
        }

        public List<tbl_HQ_Promotion> GetHQPromotion(Func<tbl_HQ_Promotion, bool> condition)
        {
            List<tbl_HQ_Promotion> tbl_HQ_Promotions = new List<tbl_HQ_Promotion>();
            tbl_HQ_Promotions = (new tbl_HQ_Promotion()).Select(condition);

            return tbl_HQ_Promotions;
        }

        public List<tbl_HQ_Promotion_Master> GetHQPromotionMaster(Func<tbl_HQ_Promotion_Master, bool> condition)
        {
            List<tbl_HQ_Promotion_Master> tbl_HQ_Promotion_Masters = new List<tbl_HQ_Promotion_Master>();
            tbl_HQ_Promotion_Masters = (new tbl_HQ_Promotion_Master()).Select(condition);

            return tbl_HQ_Promotion_Masters;
        }

        public List<tbl_HQ_Reward> GetHQReward(Func<tbl_HQ_Reward, bool> condition)
        {
            List<tbl_HQ_Reward> tbl_HQ_Rewards = new List<tbl_HQ_Reward>();
            tbl_HQ_Rewards = (new tbl_HQ_Reward()).Select(condition);

            return tbl_HQ_Rewards;
        }

        public decimal GetOrderQty(Promotion bu, tbl_PODetail prd)
        {
            decimal unitQty = 0;
            Func<tbl_ProductUomSet, bool> tbl_ProductUomSetPre = (x => x.BaseUomID == 2 && x.ProductID == prd.ProductID);
            var prdUOMSets = bu.GetUOMSet(tbl_ProductUomSetPre);
            if (prdUOMSets != null && prdUOMSets.Count > 0)
            {
                if (prd.OrderUom == 1)
                    unitQty = (prd.OrderQty.Value * prdUOMSets[0].BaseQty);
                else
                    unitQty = prd.OrderQty.Value;
            }
            else
            {
                unitQty = prd.OrderQty.Value;
            }

            return unitQty;
        }

        public PromotionRuleModel Calculate(tbl_HQ_Promotion pro, decimal total, decimal ignorAmt, bool isPeriod, bool isTxn, ProductPromotionModel pp = null)
        {
            PromotionRuleModel reward = new PromotionRuleModel();
            try
            {
                decimal roundHitTmp = 0;
                if (isTxn)
                    total = total - ignorAmt;

                int conditionAmt = 0;
                if (isPeriod)
                {
                    roundHitTmp = 1;
                    conditionAmt = pro.ConditionEnd.Value;
                }
                else
                {
                    roundHitTmp = total / pro.ConditionStart.Value;
                    conditionAmt = pro.ConditionStart.Value;
                }

                if (pro.Limit != null && roundHitTmp > pro.Limit)
                {
                    roundHitTmp = pro.Limit.Value;
                }

                int roundHit = 0;
                if (roundHitTmp > 0)
                    roundHit = Convert.ToInt32(roundHitTmp.ToString().Split('.')[0]);

                ProductPromotionModel _pp = new ProductPromotionModel();
                if (!isTxn)
                {
                    switch (pro.StepCondition1)
                    {
                        case "qty": 
                            {
                                _pp.TotalQty = conditionAmt * roundHit;
                                _pp.TotalPrice = (conditionAmt * pp.UnitPrice) * roundHit;
                                _pp.SkuAmt = 0;
                            } break;
                        case "amt":
                            {
                                _pp.TotalQty = pp.TotalQty; //pendind
                                _pp.TotalPrice = conditionAmt * roundHit;
                                _pp.SkuAmt = 0;
                            }
                            break;
                        case "sku":
                            {
                                _pp.TotalQty = 0;
                                _pp.TotalPrice = 0;
                                _pp.SkuAmt = 1 * roundHit;
                            }
                            break;
                        default:
                            break;
                    }
                    _pp.UnitPrice = pp.UnitPrice;

                }
                else
                    _pp = null;

                reward = PromotionFactory(pro, total, roundHit, isPeriod, _pp);
            }
            catch (Exception ex)
            {
                return null;
            }

            return reward;
        }

        private PromotionRuleModel PromotionFactory(tbl_HQ_Promotion pro, decimal total, int roundHit, bool isPeriod, ProductPromotionModel pp = null)
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
                            ret = ((give_disc_baht_qty)give_reward).Give(pro, total, roundHit, isPeriod, pp);
                        }
                        break;
                    case "fix_baht_qty":
                        {
                            give_reward = new give_fix_baht_qty();
                            ret = ((give_fix_baht_qty)give_reward).Give(pro, total, roundHit, isPeriod, pp);
                        }
                        break;
                    case "disc_perc_amt":
                        {
                            give_reward = new give_disc_perc_amt();
                            ret = ((give_disc_perc_amt)give_reward).Give(pro, total, roundHit, isPeriod, pp);
                        }
                        break;
                    case "free=free":
                        {
                            give_reward = new give_free_free();
                            ret = ((give_free_free)give_reward).Give(pro, total, roundHit, isPeriod, pp);
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

        public List<T> GetData<T>(List<T> obj, object condition)
        {
            obj = new List<T>();
            try
            {
                //var selObj = obj.Where(func);
                string typeofString = typeof(T).ToString();
                switch (typeofString)
                {
                    case "AllCashUFormsApp.Model.tbl_HQ_SKUGroup": 
                        {
                            foreach (var item in new tbl_HQ_SKUGroup().Select(condition))
                                obj.Add((T)ObjectHelper.ChangeType(item, typeof(T)));
                        } break;
                    case "AllCashUFormsApp.Model.tbl_HQ_SKUGroup_EXC":
                        {
                            foreach (var item in new tbl_HQ_SKUGroup_EXC().Select(condition))
                                obj.Add((T)ObjectHelper.ChangeType(item, typeof(T)));
                        }
                        break;
                    case "AllCashUFormsApp.Model.tbl_HQ_Promotion_Master":
                        {
                            foreach (var item in new tbl_HQ_Promotion_Master().Select(condition))
                                obj.Add((T)ObjectHelper.ChangeType(item, typeof(T)));
                        }
                        break;
                    case "AllCashUFormsApp.Model.tbl_HQ_Reward":
                        {
                            foreach (var item in new tbl_HQ_Reward().Select(condition))
                                obj.Add((T)ObjectHelper.ChangeType(item, typeof(T)));
                        }
                        break;
                    case "AllCashUFormsApp.Model.tbl_HQ_Promotion":
                        {
                            foreach (var item in new tbl_HQ_Promotion().Select(condition))
                                obj.Add((T)ObjectHelper.ChangeType(item, typeof(T)));
                        }
                        break;
                    default:
                        break;
                }
                //if (ret != null && ret.Count() > 0)
                //{
                //    ret = selObj;
                //}
            }
            catch (Exception ex)
            {
                throw;
            }

            return obj;
        }

        public List<tbl_HQ_SKUGroup> GetPromotion_ProductGroup(string sku_id)
        {
            List<tbl_HQ_SKUGroup> ret = new List<tbl_HQ_SKUGroup>();
            //Func<tbl_HQ_ProductGroup, bool> func = (x => x.SKU_ID.Trim() == sku_id.Trim());
            ret = GetData(ret, sku_id);

            return ret;
        }

        public List<tbl_HQ_SKUGroup_EXC> GetPromotion_ProductGroupEXC(string sku_id)
        {
            List<tbl_HQ_SKUGroup_EXC> ret = new List<tbl_HQ_SKUGroup_EXC>();
            //Func<tbl_HQ_ProductGroup_EXC, bool> func = (x => x.SKU_ID.Trim() == sku_id.Trim());
            ret = GetData(ret, sku_id);

            return ret;
        }

        public List<tbl_HQ_Promotion> GetPRDPromotion(string productGroupID)
        {
            List<tbl_HQ_Promotion> ret = new List<tbl_HQ_Promotion>();
            //Func<tbl_HQ_Promotion, bool> func = (x => x.ProductGroupID.Trim() == productGroupID.Trim());
            ret = GetData(ret, productGroupID);

            return ret;
        }

        public List<tbl_HQ_Promotion> GetTXNPromotion()
        {
            List<tbl_HQ_Promotion> ret = new List<tbl_HQ_Promotion>();
            ret = new tbl_HQ_Promotion().SelectAll().Where(x => x.PromotionPattern.ToLower() == "txn").ToList();

            return ret;
        }

        public virtual int AddData()
        {
            List<int> ret = new List<int>();
            try
            {
                if (_tbl_HQ_Promotion_Hits != null && _tbl_HQ_Promotion_Hits.Count > 0)
                {
                    foreach (var tbl_HQ_Promotion_Hit in _tbl_HQ_Promotion_Hits)
                    {
                        ret.Add(tbl_HQ_Promotion_Hit.Insert());
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                ex.WriteLog(this.GetType());
            }

            return ret.All(x => x == 1) ? 1 : 0;
        }

        public virtual int UpdateData()
        {
            List<int> ret = new List<int>();
            try
            {
                //Promotion--------------------------------------------------------------------------------

                if (_tbl_HQ_Promotion_Hits != null && _tbl_HQ_Promotion_Hits.Count > 0)
                {
                    foreach (var _tbl_HQ_Promotion_Hit in _tbl_HQ_Promotion_Hits)
                    {
                        ret.Add(_tbl_HQ_Promotion_Hit.Update());
                    }
                }
                //Promotion--------------------------------------------------------------------------------

            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                ex.WriteLog(this.GetType());
            }

            return ret.All(x => x == 1) ? 1 : 0;
        }

        public virtual int RemoveData()
        {
            List<int> ret = new List<int>();
            try
            {
                if (_tbl_HQ_Promotion_Hits != null && _tbl_HQ_Promotion_Hits.Count > 0)
                {
                    Func<tbl_HQ_Promotion_Hit, bool> tbl_HQ_Promotion_HitPre = (x => x.PK == _tbl_HQ_Promotion_Hits[0].PK);
                    var oldtbl_HQ_Promotion_Hit = new tbl_HQ_Promotion_Hit().Select(tbl_HQ_Promotion_HitPre);
                    foreach (var tbl_HQ_Promotion_Hit in oldtbl_HQ_Promotion_Hit)
                    {
                        ret.Add(tbl_HQ_Promotion_Hit.Delete());
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                ex.WriteLog(this.GetType());
            }

            return ret.All(x => x == 1) ? 1 : 0;
        }

        public virtual int AddTempData()
        {
            List<int> ret = new List<int>();
            try
            {
                if (_tbl_HQ_Promotion_Hit_Temps != null && _tbl_HQ_Promotion_Hit_Temps.Count > 0)
                {
                    foreach (var _tbl_HQ_Promotion_Hit_Temp in _tbl_HQ_Promotion_Hit_Temps)
                    {
                        ret.Add(_tbl_HQ_Promotion_Hit_Temp.Insert());
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                ex.WriteLog(this.GetType());
            }

            return ret.All(x => x == 1) ? 1 : 0;
        }

        public virtual int UpdateTempData()
        {
            List<int> ret = new List<int>();
            try
            {
                //Promotion--------------------------------------------------------------------------------

                if (_tbl_HQ_Promotion_Hit_Temps != null && _tbl_HQ_Promotion_Hit_Temps.Count > 0)
                {
                    foreach (var _tbl_HQ_Promotion_Hit_Temp in _tbl_HQ_Promotion_Hit_Temps)
                    {
                        ret.Add(_tbl_HQ_Promotion_Hit_Temp.Update());
                    }
                }
                //Promotion--------------------------------------------------------------------------------

            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                ex.WriteLog(this.GetType());
            }

            return ret.All(x => x == 1) ? 1 : 0;
        }

        public virtual int RemoveTempData()
        {
            List<int> ret = new List<int>();
            try
            {
                var temp_tbl_HQ_Promotion_Hit_Temps = (new tbl_HQ_Promotion_Hit_Temp()).SelectAll();
                if (temp_tbl_HQ_Promotion_Hit_Temps != null && temp_tbl_HQ_Promotion_Hit_Temps.Count > 0)
                {
                    foreach (var tbl_HQ_Promotion_Hit_Temp in temp_tbl_HQ_Promotion_Hit_Temps)
                    {
                        ret.Add(tbl_HQ_Promotion_Hit_Temp.Delete());
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                ex.WriteLog(this.GetType());
            }

            return ret.All(x => x == 1) ? 1 : 0;
        }

        public List<tbl_HQ_Promotion_Hit> GetPromotionHit(Func<tbl_HQ_Promotion_Hit, bool> condition = null)
        {
            _tbl_HQ_Promotion_Hits = new List<tbl_HQ_Promotion_Hit>();
            try
            {
                if (condition != null)
                    _tbl_HQ_Promotion_Hits = new tbl_HQ_Promotion_Hit().Select(condition);
                else
                    _tbl_HQ_Promotion_Hits = new tbl_HQ_Promotion_Hit().SelectAll();
            }
            catch (Exception)
            {

                throw;
            }

            return tbl_HQ_Promotion_Hits;
        }

    }
}
