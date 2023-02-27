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
        private List<string> _notMatchGroupItems = new List<string>();

        public void PreCalcPromotion(Promotion bu, List<tbl_PODetail_PRE> productList, List<PromotionRuleModel> hitProList)
        {
            try
            {
                List<string> listSku = new List<string>();
                decimal totalPrice = 0;
                decimal totalQty = 0;
                decimal skuAmt = 0;

                List<tbl_HQ_SKUGroup_EXC> allExcPrdGroup = new List<tbl_HQ_SKUGroup_EXC>();
                foreach (tbl_PODetail_PRE prd in productList)
                {
                    allExcPrdGroup.AddRange(GetPromotion_ProductGroupEXC(prd.ProductID)); //get not match product
                }

                var notMatchGroupItems = allExcPrdGroup.Select(y => new { ProductID = y.SKU_ID }).Distinct().ToList();
                _notMatchGroupItems.AddRange(notMatchGroupItems.Select(x => x.ProductID).ToList());

                foreach (tbl_PODetail_PRE prd in productList)
                {

                    totalPrice += prd.LineTotal.Value; //(prd.OrderQty.Value * prd.UnitPrice.Value); //prd.LineTotal.Value;
                    totalQty += bu.GetOrderQty(bu, prd); //prd.OrderQty.Value;

                    if (listSku.All(x => x != prd.ProductID))
                        listSku.Add(prd.ProductID);

                    foreach (var itemExc in notMatchGroupItems)
                    {
                        if (itemExc.ProductID == prd.ProductID) //except product
                        {
                            totalPrice -= prd.LineTotal.Value; //(prd.OrderQty.Value * prd.UnitPrice.Value); //prd.LineTotal.Value;
                            totalQty -= bu.GetOrderQty(bu, prd); //prd.OrderQty.Value;

                            listSku.Remove(prd.ProductID);
                        }
                    }
                }

                if (listSku != null && listSku.Count > 0)
                    skuAmt = Convert.ToDecimal(listSku.Distinct().ToList().Count);

                var proTxnList = GetTXNPromotion();
                if (proTxnList != null && proTxnList.Count > 0) //txn
                {
                    proTxnList = proTxnList.OrderBy(x => x.PromotionPriority).ToList(); //order promotion

                    foreach (tbl_HQ_Promotion pro in proTxnList)
                    {
                        var promotionList = new List<PromotionRuleModel>();
                        promotionList = CalcPromotion(hitProList, pro, totalPrice, totalQty, skuAmt);

                        if (promotionList != null && promotionList.Count > 0)
                            hitProList.AddRange(promotionList);

                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void PreCalcPromotion(Promotion bu, List<tbl_PODetail> productList, List<PromotionRuleModel> hitProList, string customerID = "", string whid = "")
        {
            try
            {
                List<string> listSku = new List<string>();
                decimal totalPrice = 0;
                decimal totalQty = 0;
                decimal skuAmt = 0;

                List<tbl_HQ_SKUGroup_EXC> allExcPrdGroup = new List<tbl_HQ_SKUGroup_EXC>();
                allExcPrdGroup.AddRange(GetPromotionListProductGroupEXC(productList.Select(x => x.ProductID).ToList()));
                //foreach (tbl_PODetail prd in productList)
                //{
                //    allExcPrdGroup.AddRange(GetPromotion_ProductGroupEXC(prd.ProductID)); //get not match product
                //}

                var notMatchGroupItems = allExcPrdGroup.Select(y => new { ProductID = y.SKU_ID }).Distinct().ToList();
                _notMatchGroupItems.AddRange(notMatchGroupItems.Select(x => x.ProductID).ToList());

                var allprdUOMSets = bu.GetUOMSet();

                foreach (tbl_PODetail prd in productList)
                {

                    totalPrice += prd.LineTotal.Value; //(prd.OrderQty.Value * prd.UnitPrice.Value); //
                    totalQty += bu.GetOrderQty(bu, prd, allprdUOMSets); //prd.OrderQty.Value;

                    if (listSku.All(x => x != prd.ProductID))
                        listSku.Add(prd.ProductID);

                    foreach (var itemExc in notMatchGroupItems)
                    {
                        if (itemExc.ProductID == prd.ProductID) //except product
                        {
                            totalPrice -= prd.LineTotal.Value; //(prd.OrderQty.Value * prd.UnitPrice.Value); //prd.LineTotal.Value;
                            totalQty -= bu.GetOrderQty(bu, prd, allprdUOMSets); //prd.OrderQty.Value;

                            listSku.Remove(prd.ProductID);
                        }
                    }
                }

                if (listSku != null && listSku.Count > 0)
                    skuAmt = Convert.ToDecimal(listSku.Distinct().ToList().Count);

                var proTxnList = GetTXNPromotion(whid);
                if (proTxnList != null && proTxnList.Count > 0) //txn
                {
                    proTxnList = proTxnList.OrderBy(x => x.PromotionPriority).ToList(); //order promotion

                    foreach (tbl_HQ_Promotion pro in proTxnList)
                    {
                        bool isCalPro = false;
                        ////for support u-online last edit by sailom .k 18-06-2021=====================
                        //if (pro.ShopTypeID != null)
                        //{
                        //    isCalPro = CheckPromotionShopType(bu, pro, customerID);
                        //} //===========================================================================
                        //else
                        {
                            isCalPro = true;
                        }

                        if (isCalPro)
                        {
                            var promotionList = new List<PromotionRuleModel>();

                            promotionList = CalcPromotion(hitProList, pro, totalPrice, totalQty, skuAmt);

                            if (promotionList != null && promotionList.Count > 0)
                                hitProList.AddRange(promotionList);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<PromotionRuleModel> CalcPromotion(List<PromotionRuleModel> hitPrdList, tbl_HQ_Promotion pro, decimal totalPrice, decimal totalQty, decimal skuAmt)
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
                decimal ignorAmt = 0;
                ignorAmt = VerifyIgnorApplied(hitPrdList, pro);
                total = total - ignorAmt;

                if (!string.IsNullOrEmpty(pro.ConditionStart.ToString()) &&
                    (pro.ConditionEnd == null || string.IsNullOrEmpty(pro.ConditionEnd.ToString()))) //normal
                {
                    if (total >= pro.ConditionStart)
                    {
                        reward = Calculate(pro, total, false, true);
                    }
                }
                else //period amount
                {
                    if (!string.IsNullOrEmpty(pro.ConditionStart.ToString()) &&
                        !string.IsNullOrEmpty(pro.ConditionEnd.ToString()))
                    {
                        if (total >= pro.ConditionStart && total <= pro.ConditionEnd)
                        {
                            reward = Calculate(pro, total, true, true);
                        }
                        //if (total >= pro.ConditionStart)
                        //{
                        //    reward = Calculate(pro, total, ignorAmt, true, true);
                        //}
                    }
                }

                if (reward != null && reward.PromotionID != null)
                    promotionList.Add(reward);
            }
            catch (Exception)
            {
                promotionList = new List<PromotionRuleModel>();
                throw;
            }

            return promotionList;
        }

        private decimal VerifyIgnorApplied(List<PromotionRuleModel> hitPrdList, tbl_HQ_Promotion pro)
        {
            decimal ignorAmt = 0;
            try
            {
                bool isCalc = true;

                var proList = hitPrdList.Where(x => x.PromotionPattern.ToLower() == "prd").ToList();

                //Distribute discount prd promotion edit by sailom 30402021-------------------------------------------------------------------------------------------------------------------------------
                var listProductGroupID = proList.Select(y => y.ProductGroupID).ToList();

                var skuG = new List<tbl_HQ_SKUGroup>();
                if (listProductGroupID.Count > 0)
                    GetListHQSKUGroup(listProductGroupID);

                foreach (var item in skuG)
                {
                    if (_notMatchGroupItems.Contains(item.SKU_ID))
                    {
                        isCalc = false;
                    }
                }

                if (!isCalc)
                {
                    return ignorAmt;
                }
                //Distribute discount prd promotion edit by sailom 30402021-------------------------------------------------------------------------------------------------------------------------------

                foreach (var hitProItem in proList)
                {
                    if (hitProItem != null)
                    {
                        if (hitProItem.IgnoreApplied) //IgnoreApplied = false
                        {
                            if (pro.StepCondition1 == "qty")
                            {
                                ignorAmt += hitProItem.Use.UseQuantity;
                            }
                            else if (pro.StepCondition1 == "amt")
                            {
                                ignorAmt += hitProItem.Use.UsePrice;
                            }
                            else
                            {
                                ignorAmt += hitProItem.Use.UseSKUAmt;
                            }
                        }

                    }
                }
                    
                decimal discountTxn = 0;
                discountTxn = hitPrdList.Where(x => x.DisCountPattern != "free=free" && x.PromotionType != "mmch").Sum(x => x.DisCountAmt);
                //discountTxn = hitPrdList.Where(x => x.PromotionPattern.ToLower() == "txn" && x.DisCountPattern != "free=free").Sum(x => x.DisCountAmt);
                if (discountTxn > 0)
                {
                    ignorAmt += discountTxn;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return ignorAmt;
        }
    }
}
