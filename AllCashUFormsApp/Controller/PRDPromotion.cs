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
        public void PreCalcPromotion(Promotion bu, List<tbl_PODetail_PRE> productList, List<PromotionRuleModel> hitProList)
        {
            try
            {
                List<tbl_HQ_SKUGroup> allPrdGroup = new List<tbl_HQ_SKUGroup>();
                //List<tbl_HQ_SKUGroup_EXC> allExcPrdGroup = new List<tbl_HQ_SKUGroup_EXC>();

                foreach (tbl_PODetail_PRE prd in productList)
                {
                    allPrdGroup.AddRange(GetPromotion_ProductGroup(prd.ProductID)); //get match product
                    //allExcPrdGroup.AddRange(GetPromotion_ProductGroupEXC(prd.ProductID)); //get not match product
                }

                var matchGroupItems = allPrdGroup.Select(y => new { SKUGroupID = y.SKUGroupID, ProductID = y.SKU_ID }).Distinct().ToList();
                //var notMatchGroupItems = allExcPrdGroup.Select(y => new { SKUGroupID = y.SKUGroupID, ProductID = y.SKU_ID }).Distinct().ToList();
                //listProductGroup = listProductGroup.Distinct().ToList();

                var listOfSKUGroupID = matchGroupItems.Select(x => x.SKUGroupID).Distinct().ToList();
                foreach (string skuGroupID in listOfSKUGroupID)
                {
                    List<string> listSku = new List<string>();

                    decimal unitPrice = 0;
                    decimal totalPrice = 0;
                    decimal totalQty = 0;
                    decimal skuAmt = 0;

                    foreach (tbl_PODetail_PRE prd in productList)
                    {
                        if (matchGroupItems.Any(x => x.SKUGroupID == skuGroupID && x.ProductID == prd.ProductID)) //sum product
                        {
                            //unitPrice = prd.UnitPrice.Value;

                            totalPrice += prd.LineTotal.Value;
                            totalQty += bu.GetOrderQty(bu, prd); //prd.OrderQty.Value;
                            unitPrice = bu.GetSellPriceVat(bu, prd); //calc unit price

                            if (listSku.All(x => x != prd.ProductID))
                                listSku.Add(prd.ProductID);
                        }

                        //foreach (var itemExc in notMatchGroupItems)
                        //{
                        //    if (itemExc.SKUGroupID == skuGroupID && itemExc.ProductID == prd.ProductID) //except product
                        //    {
                        //        totalPrice -= prd.LineTotal.Value;
                        //        totalQty -= bu.GetOrderQty(bu, prd); //prd.OrderQty.Value;

                        //        listSku.Remove(prd.ProductID);
                        //    }
                        //}

                    }

                    if (listSku != null && listSku.Count > 0) //count sku
                        skuAmt = Convert.ToDecimal(listSku.Distinct().ToList().Count);

                    var proList = GetPRDPromotion(skuGroupID); //get prd promotion

                    if (proList != null && proList.Count > 0)  //prd
                    {
                        ProductPromotionModel pp = new ProductPromotionModel();

                        pp.SKUGroupID = skuGroupID;
                        pp.TotalPrice = totalPrice;
                        pp.TotalQty = totalQty;
                        pp.SkuAmt = skuAmt;
                        pp.UnitPrice = unitPrice;

                        proList = proList.OrderBy(x => x.PromotionPriority).ToList(); //order promotion

                        foreach (tbl_HQ_Promotion pro in proList)
                        {
                            bool verifyCalPro = false;
                            if (pro.PlusSaleFrom != null)// plus sale from------------------------------------------
                            {
                                if (pp.TotalQty >= pro.PlusSaleFrom)
                                {
                                    verifyCalPro = true;
                                }
                            }
                            else
                            {
                                verifyCalPro = true;
                            }

                            if (verifyCalPro)
                            {
                                var promotionList = new List<PromotionRuleModel>();
                                promotionList = CalcPromotion(pro, hitProList, pp);

                                if (promotionList != null && promotionList.Count > 0)
                                    hitProList.AddRange(promotionList);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void PreCalcPromotion(Promotion bu, List<tbl_PODetail> productList, List<PromotionRuleModel> hitProList, string customerID = "")
        {
            try
            {
                List<tbl_HQ_SKUGroup> allPrdGroup = new List<tbl_HQ_SKUGroup>();
                //List<tbl_HQ_SKUGroup_EXC> allExcPrdGroup = new List<tbl_HQ_SKUGroup_EXC>();

                foreach (tbl_PODetail prd in productList)
                {
                    allPrdGroup.AddRange(GetPromotion_ProductGroup(prd.ProductID)); //get match product
                    //allExcPrdGroup.AddRange(GetPromotion_ProductGroupEXC(prd.ProductID)); //get not match product
                }

                var matchGroupItems = allPrdGroup.Select(y => new { SKUGroupID = y.SKUGroupID, ProductID = y.SKU_ID }).Distinct().ToList();
                //var notMatchGroupItems = allExcPrdGroup.Select(y => new { SKUGroupID = y.SKUGroupID, ProductID = y.SKU_ID }).Distinct().ToList();
                //listProductGroup = listProductGroup.Distinct().ToList();

                var listOfSKUGroupID = matchGroupItems.Select(x => x.SKUGroupID).Distinct().ToList();
                foreach (string skuGroupID in listOfSKUGroupID)
                {
                    List<string> listSku = new List<string>();

                    decimal unitPrice = 0;
                    decimal totalPrice = 0;
                    decimal totalQty = 0;
                    decimal skuAmt = 0;

                    foreach (tbl_PODetail prd in productList)
                    {
                        if (matchGroupItems.Any(x => x.SKUGroupID == skuGroupID && x.ProductID == prd.ProductID)) //sum product
                        {
                            //unitPrice = prd.UnitPrice.Value;

                            totalPrice += prd.LineTotal.Value;
                            totalQty += bu.GetOrderQty(bu, prd); //prd.OrderQty.Value;
                            unitPrice = bu.GetSellPriceVat(bu, prd); //calc unit price

                            if (listSku.All(x => x != prd.ProductID))
                                listSku.Add(prd.ProductID);
                        }

                        //foreach (var itemExc in notMatchGroupItems)
                        //{
                        //    if (itemExc.SKUGroupID == skuGroupID && itemExc.ProductID == prd.ProductID) //except product
                        //    {
                        //        totalPrice -= prd.LineTotal.Value;
                        //        totalQty -= bu.GetOrderQty(bu, prd); //prd.OrderQty.Value;

                        //        listSku.Remove(prd.ProductID);
                        //    }
                        //}

                    }

                    if (listSku != null && listSku.Count > 0) //count sku
                        skuAmt = Convert.ToDecimal(listSku.Distinct().ToList().Count);

                    var proList = GetPRDPromotion(skuGroupID); //get prd promotion

                    if (proList != null && proList.Count > 0)  //prd
                    {
                        ProductPromotionModel pp = new ProductPromotionModel();

                        pp.SKUGroupID = skuGroupID;
                        pp.TotalPrice = totalPrice;
                        pp.TotalQty = totalQty;
                        pp.SkuAmt = skuAmt;
                        pp.UnitPrice = unitPrice;

                        proList = proList.OrderBy(x => x.PromotionPriority).ToList(); //order promotion

                        foreach (tbl_HQ_Promotion pro in proList)
                        {
                            bool verifyCalPro = false;
                            if (pro.PlusSaleFrom != null)// plus sale from------------------------------------------
                            {
                                if (pp.TotalQty >= pro.PlusSaleFrom)
                                {
                                    verifyCalPro = true;
                                }
                            }
                            else
                            {
                                verifyCalPro = true;
                            }

                            if (verifyCalPro)
                            {
                                bool isCalPro = false;
                                //for support u-online last edit by sailom .k 18-06-2021=====================
                                if (pro.ShopTypeID != null)
                                {
                                    isCalPro = CheckPromotionShopType(bu, pro, customerID);
                                } //===========================================================================
                                else
                                {
                                    isCalPro = true;
                                }

                                if (isCalPro)
                                {
                                    var promotionList = new List<PromotionRuleModel>();
                                    promotionList = CalcPromotion(pro, hitProList, pp);

                                    if (promotionList != null && promotionList.Count > 0)
                                        hitProList.AddRange(promotionList);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<PromotionRuleModel> CalcPromotion(tbl_HQ_Promotion pro, List<PromotionRuleModel> _promotionList, ProductPromotionModel pp)
        {
            List<PromotionRuleModel> promotionList = new List<PromotionRuleModel>();
            try
            {
                decimal total = 0;
                switch (pro.StepCondition1)
                {
                    case "qty": total = pp.TotalQty; break;
                    case "amt": total = pp.TotalPrice; break;
                    case "sku": total = pp.SkuAmt; break;
                    default:
                        break;
                }

                PromotionRuleModel reward = new PromotionRuleModel();

                if (!string.IsNullOrEmpty(pro.ConditionStart.ToString()) &&
                    (pro.ConditionEnd == null || string.IsNullOrEmpty(pro.ConditionEnd.ToString()))) //normal
                {
                    if (total >= pro.ConditionStart)
                    {
                        reward = VerifyRecomputable(_promotionList, pro, total, false, pp);
                    }
                }
                else //period amount
                {
                    if (!string.IsNullOrEmpty(pro.ConditionStart.ToString()) &&
                        !string.IsNullOrEmpty(pro.ConditionEnd.ToString()))
                    {
                        if (total >= pro.ConditionStart && total <= pro.ConditionEnd)
                        {
                            reward = VerifyRecomputable(_promotionList, pro, total, true, pp);
                        }
                    }
                }

                if (reward != null && reward.PromotionID != null)
                    if (reward.DisCountPattern == "free=free")
                    {
                        if (reward.PruductGroupRewardAmt > 0)
                        {
                            promotionList.Add(reward);
                        }
                    }
                    else
                        promotionList.Add(reward);
            }
            catch (Exception)
            {
                promotionList = new List<PromotionRuleModel>();
                throw;
            }

            return promotionList;
        }

        private PromotionRuleModel VerifyRecomputable(List<PromotionRuleModel> promotionList, tbl_HQ_Promotion pro, decimal total, bool isPeriod, ProductPromotionModel pp)
        {
            PromotionRuleModel reward = null;
            try
            {
                if (pro.Recomputable != null && pro.Recomputable.Value) //is recomputable = true
                {
                    if (pro.IsUseCoupon != null && pro.IsUseCoupon.Value)
                    {
                        decimal useAmount = 0.00m;
                        useAmount = promotionList.Where(x => x.ProductGroupID == pro.SKUGroupID1 && x.IsUseCoupon).Sum(x => x.Use.UseAmount);
                        total = total - useAmount;
                        reward = Calculate(pro, total, isPeriod, false, pp);
                    }
                    else
                        reward = Calculate(pro, total, isPeriod, false, pp);
                }
                else //is recomputable = false
                {
                    if (promotionList.Any(x => x.ProductGroupID == pro.SKUGroupID1)) //has hit promotion
                    {
                        //Do nothing
                        reward = null;
                    }
                    else //no hit promotion
                    {
                        reward = Calculate(pro, total, isPeriod, false, pp);
                    }
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
