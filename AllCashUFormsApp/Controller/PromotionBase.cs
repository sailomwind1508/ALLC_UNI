using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class PromotionBase : IPromotion
    {
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

        public List<tbl_HQ_Promotion> GetTXNPromotion(string productGroupID)
        {
            List<tbl_HQ_Promotion> ret = new List<tbl_HQ_Promotion>();
            ret = new tbl_HQ_Promotion().SelectAll().Where(x => x.PromotionPattern.ToLower() == "txn").ToList();

            return ret;
        }
    }
}
