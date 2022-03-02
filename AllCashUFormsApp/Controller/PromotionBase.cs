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
        private Dictionary<string, int> _mMCHProList = null;
        public virtual Dictionary<string, int> mMCHProList
        {
            get { return _mMCHProList; }
            set
            {
                _mMCHProList = value;
            }
        }

        public PromotionBase() : base("Promotion")
        {
            _predicate = null;
        }

        ///// <summary>
        ///// for support u-online last edit by sailom .k 18-06-2021
        ///// </summary>
        ///// <param name="pro"></param>
        ///// <param name="customerID"></param>
        ///// <returns></returns>
        //public bool CheckPromotionShopType(Promotion bu, tbl_HQ_Promotion pro, string customerID)
        //{
        //    bool result = false;
        //    try
        //    {
        //        //for support u-online last edit by sailom .k 18-06-2021=====================
        //        if (pro.ShopTypeID != null)
        //        {
        //            var cust = bu.GetCustomer(customerID);
        //            if (cust != null && cust.Count > 0)
        //            {
        //                if (cust.FirstOrDefault() != null && pro.ShopTypeID.Value == cust.FirstOrDefault().ShopTypeID)
        //                {
        //                    result = true;
        //                }
        //            }
        //        } //===========================================================================
        //    }
        //    catch (Exception ex)
        //    {
        //        result = false;
        //        throw ex;
        //    }

        //    return result;
        //}

        public List<tbl_HQ_SKUGroup> GetListHQSKUGroup(object list)
        {
            List<tbl_HQ_SKUGroup> tbl_HQ_SKUGroups = new List<tbl_HQ_SKUGroup>();
            tbl_HQ_SKUGroups = new tbl_HQ_SKUGroup().SelectListSKUGroup(list).ToList();

            return tbl_HQ_SKUGroups;
        }

        public List<tbl_HQ_SKUGroup> GetHQSKUGroup(Func<tbl_HQ_SKUGroup, bool> condition = null)
        {
            List<tbl_HQ_SKUGroup> tbl_HQ_SKUGroups = new List<tbl_HQ_SKUGroup>();
            if (condition != null)
            {
                tbl_HQ_SKUGroups = (new tbl_HQ_SKUGroup()).Select(condition);
            }
            else
                tbl_HQ_SKUGroups = (new tbl_HQ_SKUGroup()).SelectAll();


            return tbl_HQ_SKUGroups;
        }

        public List<tbl_HQ_SKUGroup_EXC> GetHQSKUGroup_EXC(Func<tbl_HQ_SKUGroup_EXC, bool> condition = null)
        {
            List<tbl_HQ_SKUGroup_EXC> tbl_HQ_SKUGroup_EXCs = new List<tbl_HQ_SKUGroup_EXC>();
            if (condition != null)
            {
                tbl_HQ_SKUGroup_EXCs = (new tbl_HQ_SKUGroup_EXC()).Select(condition);
            }
            else
                tbl_HQ_SKUGroup_EXCs = (new tbl_HQ_SKUGroup_EXC()).SelectAll();


            return tbl_HQ_SKUGroup_EXCs;
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

        public decimal GetOrderQty(Promotion bu, tbl_PODetail prd, List<tbl_ProductUomSet> allprdUOMSets)
        {
            decimal unitQty = 0;
           
            var prdUOMSets = bu.GetProductUOMSet(allprdUOMSets, prd.ProductID);

            if (prdUOMSets != null && prdUOMSets.Count > 0)
            {
                if (prd.OrderUom == 1)
                {
                    unitQty = (prd.OrderQty.Value * prdUOMSets[0].BaseQty);
                }
                else
                    unitQty = prd.OrderQty.Value;
            }
            else
            {
                unitQty = prd.OrderQty.Value;
            }

            return unitQty;
        }

        public decimal GetOrderQty(Promotion bu, tbl_PODetail_PRE prd)
        {
            decimal unitQty = 0;
            var allprdUOMSets = bu.GetUOMSet();
            var prdUOMSets = bu.GetProductUOMSet(allprdUOMSets, prd.ProductID);

            if (prdUOMSets != null && prdUOMSets.Count > 0)
            {
                if (prd.OrderUom == 1)
                {
                    unitQty = (prd.OrderQty.Value * prdUOMSets[0].BaseQty);
                }
                else
                    unitQty = prd.OrderQty.Value;
            }
            else
            {
                unitQty = prd.OrderQty.Value;
            }

            return unitQty;
        }

        public decimal GetSellPriceVat(Promotion bu, tbl_PODetail prd, List<tbl_ProductUomSet> allUomSet)
        {
            decimal sellPriceVat = 0;

            Func<tbl_ProductPriceGroup, bool> func = (x => x.ProductID == prd.ProductID);
            var productPriceGroups = bu.GetProductPriceGroup(func);
            if (productPriceGroups != null && productPriceGroups.Count > 0)
            {
                //var allUomSet = GetUOMSet();
                //if (poDt.OrderUom != 2) //06042021 by sailom.k
                var minUomID = 2;
                minUomID = allUomSet.GetMinUOM(prd);

                var uomSet = allUomSet.FirstOrDefault(x => x.ProductID == prd.ProductID && x.UomSetID == prd.OrderUom);
                if (uomSet != null && uomSet.BaseQty > 1) //prd.OrderUom == 1) //15022021
                {
                    sellPriceVat = productPriceGroups.First(x => x.ProductUomID == minUomID).SellPriceVat.Value;
                }
                else
                    sellPriceVat = productPriceGroups.First().SellPriceVat.Value;
            }
            else
                sellPriceVat = prd.UnitPrice.Value;

            return sellPriceVat;
        }

        public decimal GetSellPriceVat(Promotion bu, tbl_PODetail_PRE prd)
        {
            decimal sellPriceVat = 0;

            Func<tbl_ProductPriceGroup, bool> func = (x => x.ProductID == prd.ProductID);
            var productPriceGroups = bu.GetProductPriceGroup(func);
            if (productPriceGroups != null && productPriceGroups.Count > 0)
            {
                var allUomSet = GetUOMSet();
                //if (poDt.OrderUom != 2) //06042021 by sailom.k
                var minUomID = 2;
                minUomID = allUomSet.GetMinUOM(prd);

                var uomSet = allUomSet.FirstOrDefault(x => x.ProductID == prd.ProductID && x.UomSetID == prd.OrderUom);
                if (uomSet != null && uomSet.BaseQty > 1) //prd.OrderUom == 1) //15022021
                {
                    sellPriceVat = productPriceGroups.First(x => x.ProductUomID == minUomID).SellPriceVat.Value;
                }
                else
                    sellPriceVat = productPriceGroups.First().SellPriceVat.Value;
            }
            else
                sellPriceVat = prd.UnitPrice.Value;

            return sellPriceVat;
        }

        public PromotionRuleModel Calculate(tbl_HQ_Promotion pro, decimal total, bool isPeriod, bool isTxn, ProductPromotionModel pp = null)
        {
            PromotionRuleModel reward = new PromotionRuleModel();
            try
            {
                decimal roundHitTmp = 0;
                //if (isTxn)
                //    total = total - ignorAmt;

                int conditionAmt = 0;
                if (isPeriod)
                {
                    roundHitTmp = 1;
                    conditionAmt = pro.ConditionEnd.Value;
                }
                else
                {
                    //if (!string.IsNullOrEmpty(pro.StepCondition2)) //pro a+b
                    //{
                    //    roundHitTmp = total / pro.SKUGroupID2.Value;
                    //}
                    //else
                    roundHitTmp = total / pro.ConditionStart.Value;

                    conditionAmt = pro.ConditionStart.Value;
                }

                if (pro.HitLimit != null && roundHitTmp > pro.HitLimit)
                {
                    roundHitTmp = pro.HitLimit.Value;
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

        public List<T> GetData<T>(List<T> obj, object condition, string whid = "")
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
                            foreach (var item in new tbl_HQ_SKUGroup().SelectListSKU(condition))
                                obj.Add((T)ObjectHelper.ChangeType(item, typeof(T)));
                        } break;
                    case "AllCashUFormsApp.Model.tbl_HQ_SKUGroup_EXC":
                        {
                            foreach (var item in new tbl_HQ_SKUGroup_EXC().SelectListSKU(condition))
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
                            foreach (var item in new tbl_HQ_Promotion().SelectPRD(condition, whid))
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

        public List<tbl_HQ_SKUGroup> GetPromotion_ProductGroup(string sku_id, string whid = "")
        {
            List<tbl_HQ_SKUGroup> ret = new List<tbl_HQ_SKUGroup>();
            //Func<tbl_HQ_ProductGroup, bool> func = (x => x.SKU_ID.Trim() == sku_id.Trim());
            ret = GetData(ret, sku_id);

            return ret;
        }

        public List<tbl_HQ_SKUGroup> GetPromotionListProductGroup(List<string> sku_id, string whid = "")
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

        public List<tbl_HQ_SKUGroup_EXC> GetPromotionListProductGroupEXC(List<string> sku_id)
        {
            List<tbl_HQ_SKUGroup_EXC> ret = new List<tbl_HQ_SKUGroup_EXC>();
            //Func<tbl_HQ_ProductGroup_EXC, bool> func = (x => x.SKU_ID.Trim() == sku_id.Trim());
            ret = GetData(ret, sku_id);

            return ret;
        }

        public List<tbl_HQ_Promotion> GetPRDPromotion(string productGroupID, string whid = "")
        {
            List<tbl_HQ_Promotion> ret = new List<tbl_HQ_Promotion>();
            //Func<tbl_HQ_Promotion, bool> func = (x => x.ProductGroupID.Trim() == productGroupID.Trim());
            ret = GetData(ret, productGroupID, whid);

            return ret;
        }

        public List<tbl_HQ_Promotion> GetTXNPromotion(string whid = "")
        {
            Func<tbl_BranchWarehouse, bool> predicate = x => x.WHID == whid;
            int? saleTypeID = (new tbl_BranchWarehouse()).Select(predicate).First().SaleTypeID;

            List<tbl_HQ_Promotion> ret = new List<tbl_HQ_Promotion>();
            ret = new tbl_HQ_Promotion().SelectTXN(saleTypeID.Value);

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
                    Func<tbl_HQ_Promotion_Hit, bool> tbl_HQ_Promotion_HitPre = (x => x.DocNo == _tbl_HQ_Promotion_Hits[0].DocNo);
                    var oldtbl_HQ_Promotion_Hit = new tbl_HQ_Promotion_Hit().Select(tbl_HQ_Promotion_HitPre);
                    foreach (var tbl_HQ_Promotion_Hit in oldtbl_HQ_Promotion_Hit)
                    {
                        ret.Add(tbl_HQ_Promotion_Hit.Delete());
                    }
                }
                else //no data to delete 18-06-2021 by sailom.k
                {
                    ret.Add(1);
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

        public virtual int RemoveTempData(tbl_HQ_Promotion_Hit_Temp temp)
        {
            List<int> ret = new List<int>();
            try
            {
                var temp_tbl_HQ_Promotion_Hit_Temps = (new tbl_HQ_Promotion_Hit_Temp()).SelectAll().Where(x => x.PromotionID == temp.PromotionID).ToList();
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
