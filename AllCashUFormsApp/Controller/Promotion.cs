using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace AllCashUFormsApp.Controller
{
    public class Promotion : PromotionBase
    {
        public void CalculatePromotion(List<tbl_PODetail> productList)
        {
            try
            {
                var cDate = DateTime.Now;

                List<string> listProductGroup = new List<string>();
                List<string> listSku = new List<string>();
                decimal totalPrice = 0;
                decimal totalQty = 0;
                decimal skuAmt = 0;
                List<string> skuList = new List<string>();
                List<tbl_HQ_SKUGroup> allPrdGroup = new List<tbl_HQ_SKUGroup>();

                foreach (tbl_PODetail prd in productList)
                {
                    allPrdGroup = GetPromotion_ProductGroup(prd.ProductID);
                    var SKUGroupIDList = allPrdGroup.Select(y => y.SKUGroupID);
                    listProductGroup.AddRange(SKUGroupIDList.Distinct().ToList());
                }

                listProductGroup = listProductGroup.Distinct().ToList();

                foreach (string groupId in listProductGroup)
                {
                    var proList = GetPRDPromotion(groupId); //get promotion
                    if (proList != null && proList.Count > 0)  //prd
                    {
                        proList = proList.OrderBy(x => x.PromotionPriority).ToList(); //order promotion

                        foreach (tbl_HQ_Promotion pro in proList)
                        {
                            if (pro.PromotionPattern.ToLower() == "prd")
                            {
                                foreach (tbl_PODetail prd in productList)
                                {
                                    if (allPrdGroup.Any(x => x.SKU_ID == prd.ProductID))
                                    {
                                        totalPrice += prd.LineTotal.Value;
                                        totalQty += prd.OrderQty.Value;

                                        if (listSku.All(x => x != prd.ProductID))
                                            listSku.Add(prd.ProductID);
                                    }

                                    var checkSkuGroupExc = GetPromotion_ProductGroupEXC(prd.ProductID);
                                    if (checkSkuGroupExc.Count > 0)
                                    {
                                        totalPrice -= prd.LineTotal.Value;
                                        totalQty -= prd.OrderQty.Value;

                                        listSku.Remove(prd.ProductID);
                                    }
                                }

                                skuAmt = Convert.ToDecimal(listSku.Distinct().ToList().Count);

                                PRDPromotion prdPro = new PRDPromotion();
                                prdPro.CalcPromotionPRD(pro, listProductGroup, totalPrice, totalQty, skuAmt);
                            }
                        }
                    }
                    else //txn
                    {

                    }                   
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public List<tbl_HQ_Promotion> GetAllData()
        {
            return (new tbl_HQ_Promotion()).SelectAll();
        }

        public virtual int AddData(tbl_HQ_Promotion tbl_HQ_Promotion)
        {
            return tbl_HQ_Promotion.Insert();
        }

        public int UpdateData(tbl_HQ_Promotion tbl_HQ_Promotion)
        {
            return tbl_HQ_Promotion.Update();
        }

        public int RemoveData(tbl_HQ_Promotion tbl_HQ_Promotion)
        {
            return tbl_HQ_Promotion.Delete();
        }

        public virtual DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                List<tbl_HQ_Promotion> tbl_HQ_Promotions = new List<tbl_HQ_Promotion>();
                tbl_HQ_Promotions = (new tbl_HQ_Promotion()).SelectAll();

                return tbl_HQ_Promotions.ToDataTable();
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public virtual DataTable GetDataTableByCondition(string[] filters)
        {
            DataTable dt = new DataTable();
            List<tbl_HQ_Promotion> tbl_HQ_Promotions = new List<tbl_HQ_Promotion>();

            if (filters != null)
            {
                tbl_HQ_Promotions = (new tbl_HQ_Promotion()).SelectAll().Where(x => filters.Contains(x.PromotionID)).ToList();
                dt = tbl_HQ_Promotions.ToDataTable();
            }
            else
            {
                dt = GetDataTable();
            }

            return dt;
        }

    }
}
