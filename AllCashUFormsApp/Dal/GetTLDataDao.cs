using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp
{
    public static class GetTLDataDao
    {
        public static DataTable GetTLData_TL_POMaster1(this tbl_POMaster tbl_POMaster, Dictionary<string, object> _params)//
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "proc_get_TL_data_tbl_TL_POMaster1";
                dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DataTable GetTLData_TL_POMaster2(this tbl_POMaster tbl_POMaster, Dictionary<string, object> _params)//
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "proc_get_TL_data_tbl_TL_POMaster2";
                dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DataTable GetTL_data_tbl_TL_PODetail(this tbl_POMaster tbl_POMaster, Dictionary<string, object> _params)//
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "proc_get_TL_data_tbl_TL_PODetail";
                dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DataTable GetTL_data_tbl_TL_ArCustomerShelf(this tbl_POMaster tbl_POMaster, Dictionary<string, object> _params)//
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "proc_get_TL_data_tbl_TL_ArCustomerShelf";
                dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DataTable GetTL_data_tbl_TL_CustomerCode(this tbl_POMaster tbl_POMaster, Dictionary<string, object> _params)//
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "proc_get_TL_data_tbl_TL_CustomerCode";
                dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DataTable GetTL_data_tbl_TL_Visit(this tbl_POMaster tbl_POMaster, Dictionary<string, object> _params)//
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "proc_get_TL_data_tbl_TL_Visit";
                dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DataTable GetTL_data_tbl_TL_VisitStock(this tbl_POMaster tbl_POMaster, Dictionary<string, object> _params)//
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "proc_get_TL_data_tbl_TL_VisitStock";
                dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DataTable GetTL_data_tbl_TL_ArCustomer(this tbl_POMaster tbl_POMaster, Dictionary<string, object> _params)//
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "proc_get_TL_data_tbl_TL_ArCustomer";
                dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //public static void GetTL_Data(Dictionary<string, object> _params,DataTable TL_POMaster1, DataTable TL_POMaster2, DataTable TL_PODetail,DataTable TL_ArCustomerShelf,DataTable TL_CustomerCode,DataTable TL_Visit,DataTable TL_VisitStock,DataTable TL_ArCustomer)//
        //{
        //    try
        //    {
        //        string sql = "proc_get_TL_data_tbl_TL_POMaster1";
        //        TL_POMaster1 = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);

        //        sql = "proc_get_TL_data_tbl_TL_POMaster2";
        //        TL_POMaster2 = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);

        //        sql = "proc_get_TL_data_tbl_TL_PODetail";
        //        TL_PODetail = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);

        //        sql = "proc_get_TL_data_tbl_TL_ArCustomerShelf";
        //        TL_PODetail = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);

        //        sql = "proc_get_TL_data_tbl_TL_CustomerCode";
        //        TL_CustomerCode = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);

        //        sql = "proc_get_TL_data_tbl_TL_Visit";
        //        TL_Visit = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);

        //        sql = "proc_get_TL_data_tbl_TL_VisitStock";
        //        TL_VisitStock = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);

        //        sql = "proc_get_TL_data_tbl_TL_ArCustomer";
        //        TL_ArCustomer = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);

        //    }
        //    catch (Exception ex)
        //    { 

        //    }
        //}
    }
}
