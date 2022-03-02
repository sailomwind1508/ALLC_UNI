using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class BaseControl
    {
        private tbl_POMaster _tbl_POMaster = null;
        private List<tbl_PODetail> _tbl_PODetails = null;

        private tbl_POMaster_PRE _tbl_POMaster_PRE = null;
        private List<tbl_PODetail_PRE> _tbl_PODetails_PRE = null;


        private tbl_PRMaster _tbl_PRMaster = null;
        private List<tbl_PRDetail> _tbl_PRDetails = null;

        private List<tbl_InvMovement> _tbl_InvMovements = null;
        private List<tbl_InvWarehouse> _tbl_InvWarehouses = null;
        private List<tbl_InvTransaction> _tbl_InvTransactions = null;
        private List<tbl_PriceGroup> _tbl_PriceGroups = null;
        private tbl_IVMaster _tbl_IVMaster = null;
        private List<tbl_IVDetail> _tbl_IVDetails = null;
        private List<tbl_PayDetail> _tbl_PayDetails = null;
        private List<tbl_PayMaster> _tbl_PayMasters = null;
        private List<tbl_DocRunning> _tbl_DocRunning = null;
        private Func<tbl_POMaster, bool> _docTypepredicate = null;
        private List<tbl_ArCustomer> _tbl_ArCustomers = null;
        private List<tbl_ShopType> _tbl_ShopTypes = null;
        private List<tbl_ArCustomerShelf> _tbl_ArCustomerShelfs = null;
        private static List<tbl_Branch> _tbl_Branchs = new List<tbl_Branch>();
        private static List<tbl_Company> _tbl_Companies = new List<tbl_Company>();

        private string _docTypeCode;

        public virtual List<tbl_Branch> tbl_Branchs
        {
            get { return _tbl_Branchs; }
            set
            {
                _tbl_Branchs = value;
            }
        }

        public virtual List<tbl_Company> tbl_Companies
        {
            get { return _tbl_Companies; }
            set
            {
                _tbl_Companies = value;
            }
        }

        public virtual tbl_POMaster tbl_POMaster
        {
            get { return _tbl_POMaster; }
            set
            {
                _tbl_POMaster = value;
            }
        }

        public virtual List<tbl_PODetail> tbl_PODetails
        {
            get { return _tbl_PODetails; }
            set
            {
                _tbl_PODetails = value;
            }
        }

        public virtual tbl_POMaster_PRE tbl_POMaster_PRE
        {
            get { return _tbl_POMaster_PRE; }
            set
            {
                _tbl_POMaster_PRE = value;
            }
        }

        public virtual List<tbl_PODetail_PRE> tbl_PODetails_PRE
        {
            get { return _tbl_PODetails_PRE; }
            set
            {
                _tbl_PODetails_PRE = value;
            }
        }

        public virtual tbl_PRMaster tbl_PRMaster
        {
            get { return _tbl_PRMaster; }
            set
            {
                _tbl_PRMaster = value;
            }
        }
        public virtual List<tbl_PRDetail> tbl_PRDetails
        {
            get { return _tbl_PRDetails; }
            set
            {
                _tbl_PRDetails = value;
            }
        }
        public virtual List<tbl_InvMovement> tbl_InvMovements
        {
            get { return _tbl_InvMovements; }
            set
            {
                tbl_InvMovements = value;
            }
        }
        public virtual List<tbl_InvWarehouse> tbl_InvWarehouses
        {
            get { return _tbl_InvWarehouses; }
            set
            {
                _tbl_InvWarehouses = value;
            }
        }
        public virtual List<tbl_InvTransaction> tbl_InvTransactions
        {
            get { return _tbl_InvTransactions; }
            set
            {
                _tbl_InvTransactions = value;
            }
        }
        public virtual List<tbl_PriceGroup> tbl_PriceGroups
        {
            get { return _tbl_PriceGroups; }
            set
            {
                _tbl_PriceGroups = value;
            }
        }
        public virtual List<tbl_ArCustomer> tbl_ArCustomers
        {
            get { return _tbl_ArCustomers; }
            set
            {
                _tbl_ArCustomers = value;
            }
        }
        public virtual List<tbl_ArCustomerShelf> tbl_ArCustomerShelfs
        {
            get { return _tbl_ArCustomerShelfs; }
            set
            {
                _tbl_ArCustomerShelfs = value;
            }
        }
        public virtual List<tbl_ShopType> tbl_ShopTypes
        {
            get { return _tbl_ShopTypes; }
            set
            {
                _tbl_ShopTypes = value;
            }
        }
        public virtual tbl_IVMaster tbl_IVMaster
        {
            get { return _tbl_IVMaster; }
            set
            {
                _tbl_IVMaster = value;
            }
        }
        public virtual List<tbl_IVDetail> tbl_IVDetails
        {
            get { return _tbl_IVDetails; }
            set
            {
                _tbl_IVDetails = value;
            }
        }
        public virtual List<tbl_PayDetail> tbl_PayDetails
        {
            get { return _tbl_PayDetails; }
            set
            {
                _tbl_PayDetails = value;
            }
        }
        public virtual List<tbl_PayMaster> tbl_PayMasters
        {
            get { return _tbl_PayMasters; }
            set
            {
                _tbl_PayMasters = value;
            }
        }
        public virtual List<tbl_DocRunning> tbl_DocRunning
        {
            get { return _tbl_DocRunning; }
            set
            {
                _tbl_DocRunning = value;
            }
        }
        public virtual Func<tbl_POMaster, bool> docTypepredicate
        {
            get { return _docTypepredicate; }
            set
            {
                _docTypepredicate = value;
            }
        }

        public BaseControl(string docTypeCode)
        {
            _tbl_POMaster = new tbl_POMaster();
            _tbl_PODetails = new List<tbl_PODetail>();

            _tbl_PRMaster = new tbl_PRMaster();
            _tbl_PRDetails = new List<tbl_PRDetail>();

            _tbl_IVMaster = new tbl_IVMaster();
            _tbl_IVDetails = new List<tbl_IVDetail>();

            _tbl_InvMovements = new List<tbl_InvMovement>();
            _tbl_InvWarehouses = new List<tbl_InvWarehouse>();
            _tbl_InvTransactions = new List<tbl_InvTransaction>();
            _tbl_DocRunning = new List<tbl_DocRunning>();
            _tbl_PriceGroups = new List<tbl_PriceGroup>();//
            _tbl_ArCustomers = new List<tbl_ArCustomer>();//
            _tbl_ArCustomerShelfs = new List<tbl_ArCustomerShelf>();
            _tbl_ShopTypes = new List<tbl_ShopType>();//
            _tbl_PayDetails = new List<tbl_PayDetail>();//
            _tbl_PayMasters = new List<tbl_PayMaster>();//
            _docTypepredicate = (x => x.DocTypeCode.Trim() == docTypeCode.Trim());
            _docTypeCode = docTypeCode;

            //_tbl_Branchs = new List<tbl_Branch>();
            //_tbl_Companies = new List<tbl_Company>();
        }

        public List<tbl_ArCustomer> GetCustomerSalArea(string SalAreaID)
        {
            return new tbl_ArCustomer().SelectSalArea(SalAreaID);
        }

        public DataTable GetOverStockData(DateTime sDate)
        {
            try
            {
                DataTable newTable = new DataTable(); // tbl_POMaster.ToDataTable();

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@SDate", sDate.ToString("yyyyMMdd", new CultureInfo("en-US")));

                string sql = "proc_RTD_OverStock_GetDataTable";

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable GetOverStockPreOrderData(DateTime sDate)
        {
            try
            {
                DataTable newTable = new DataTable(); // tbl_POMaster.ToDataTable();

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@SDate", sDate.ToString("yyyyMMdd", new CultureInfo("en-US")));

                string sql = "proc_PreOrder_GetOverStock";

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        //public virtual void GetDocData(string docNo, string docTypeCode = "")
        //{
        //    if (!string.IsNullOrEmpty(docNo))
        //    {
        //        Func<tbl_POMaster, bool> poPredicate = (x => x.DocNo == docNo);
        //        Func<tbl_PODetail, bool> pdtPredicate = (x => x.DocNo == docNo);

        //        var _tbl_POMaster = new tbl_POMaster().Select(poPredicate);
        //        if (_tbl_POMaster != null && _tbl_POMaster.Count > 0) //PO
        //        {
        //            tbl_POMaster = _tbl_POMaster[0];
        //        }
        //        else
        //            tbl_POMaster = new tbl_POMaster();

        //        if (string.IsNullOrEmpty(tbl_POMaster.DocNo)) //PR
        //        {
        //            Func<tbl_PRMaster, bool> prPredicate = (x => x.DocNo == docNo);
        //            var _tbl_PRMaster = new tbl_PRMaster().Select(prPredicate);
        //            if (_tbl_PRMaster != null && _tbl_PRMaster.Count > 0)
        //            {
        //                tbl_PRMaster = _tbl_PRMaster[0];
        //            }
        //            else
        //            {
        //                tbl_PRMaster = new tbl_PRMaster();
        //            }
        //        }

        //        if (string.IsNullOrEmpty(tbl_PRMaster.DocNo)) //IV
        //        {
        //            Func<tbl_IVMaster, bool> ivPredicate = (x => x.DocNo == docNo);
        //            var _tbl_IVMaster = new tbl_IVMaster().Select(ivPredicate);
        //            if (_tbl_IVMaster != null && _tbl_IVMaster.Count > 0)
        //            {
        //                tbl_IVMaster = _tbl_IVMaster[0];
        //            }
        //            else
        //            {
        //                tbl_IVMaster = new tbl_IVMaster();
        //            }
        //        }

        //        tbl_PODetails = new tbl_PODetail().Select(pdtPredicate); //PO

        //        if (tbl_PODetails.Count == 0) //PR
        //        {
        //            Func<tbl_PRDetail, bool> prDtPredicate = (x => x.DocNo == docNo);
        //            var _tbl_PRDetails = new tbl_PRDetail().Select(prDtPredicate);
        //            if (_tbl_PRDetails != null && _tbl_PRDetails.Count > 0)
        //            {
        //                tbl_PRDetails = _tbl_PRDetails;
        //            }
        //            else
        //            {
        //                tbl_PRDetails.Clear();
        //            }
        //        }

        //        if (tbl_PRDetails.Count == 0) //IV
        //        {
        //            Func<tbl_IVDetail, bool> ivDtPredicate = (x => x.DocNo == docNo);
        //            var _tbl_IVDetails = new tbl_IVDetail().Select(ivDtPredicate);
        //            if (_tbl_IVDetails != null && _tbl_IVDetails.Count > 0)
        //            {
        //                tbl_IVDetails = _tbl_IVDetails;
        //            }
        //            else
        //            {
        //                tbl_IVDetails.Clear();
        //            }
        //        }

        //    }
        //}

        public virtual void GetDocData(string docNo, string docTypeCode = "")
        {
            if (!string.IsNullOrEmpty(docNo))
            {
                if (docTypeCode == "V")
                {
                    //Func<tbl_IVMaster, bool> ivPredicate = (x => x.DocNo == docNo);
                    var _tbl_IVMaster = new tbl_IVMaster().SelectExists(docNo);
                    if (_tbl_IVMaster != null && _tbl_IVMaster.Count > 0)
                    {
                        tbl_IVMaster = _tbl_IVMaster[0];
                    }
                    else
                    {
                        tbl_IVMaster = new tbl_IVMaster();
                    }

                    //Func<tbl_IVDetail, bool> ivDtPredicate = (x => x.DocNo == docNo);
                    //var _tbl_IVDetails = new tbl_IVDetail().Select(ivDtPredicate);

                    string sqlFilter = " DocNo = '" + docNo + "' ";
                    var _tbl_IVDetails = new tbl_IVDetail().Select(sqlFilter); //edit by sailom 14-06-2021
                    if (_tbl_IVDetails != null && _tbl_IVDetails.Count > 0)
                    {
                        tbl_IVDetails = _tbl_IVDetails;
                    }
                    else
                    {
                        tbl_IVDetails.Clear();
                    }

                }
                else if (docTypeCode == "RE" || docTypeCode == "OD" || docTypeCode == "IV" || docTypeCode == "IM" || docTypeCode == "IV2" || docTypeCode == "RT") //IV2 is new Pre-Order
                {
                    if (docTypeCode == "IV2") //IV2 is new Pre-Order
                    {
                        docTypeCode = "IV";

                        Func<tbl_POMaster_PRE, bool> poPredicate_PRE = (x => x.DocNo == docNo);
                        Func<tbl_PODetail_PRE, bool> pdtPredicate_PRE = (x => x.DocNo == docNo);

                        var _tbl_POMaster_PRE = new tbl_POMaster_PRE().SelectExists(docNo); //new tbl_POMaster().Select(poPredicate, docTypeCode);
                        if (_tbl_POMaster_PRE != null && _tbl_POMaster_PRE.Count > 0) //PO
                        {
                            tbl_POMaster_PRE = _tbl_POMaster_PRE[0];
                        }
                        else
                            tbl_POMaster_PRE = new tbl_POMaster_PRE();

                        tbl_PODetails_PRE = new tbl_PODetail_PRE().Select(pdtPredicate_PRE, (docTypeCode == "IM" ? "IV" : docTypeCode), docNo); //PO Details Pre-Order
                    }
                    else
                    {
                        Func<tbl_POMaster, bool> poPredicate = (x => x.DocNo == docNo);
                        Func<tbl_PODetail, bool> pdtPredicate = (x => x.DocNo == docNo);

                        var _tbl_POMaster = new tbl_POMaster().SelectExists(docNo); //new tbl_POMaster().Select(poPredicate, docTypeCode);
                        if (_tbl_POMaster != null && _tbl_POMaster.Count > 0) //PO
                        {
                            tbl_POMaster = _tbl_POMaster[0];
                        }
                        else
                            tbl_POMaster = new tbl_POMaster();

                        tbl_PODetails = new tbl_PODetail().Select(pdtPredicate, (docTypeCode == "IM" ? "IV" : docTypeCode), docNo); //PO Details

                    }
                }
                else if (docTypeCode == "RL" || docTypeCode == "RB" || docTypeCode == "RJ" || docTypeCode == "TR")
                {
                    //Func<tbl_PRMaster, bool> prPredicate = (x => x.DocNo == docNo);
                    var _tbl_PRMaster = new tbl_PRMaster().SelectExists(docNo, docTypeCode);
                    if (_tbl_PRMaster != null && _tbl_PRMaster.Count > 0)
                    {
                        tbl_PRMaster = _tbl_PRMaster[0];
                    }
                    else
                    {
                        tbl_PRMaster = new tbl_PRMaster();
                    }

                    //Func<tbl_PRDetail, bool> prDtPredicate = (x => x.DocNo == docNo);
                    //var _tbl_PRDetails = new tbl_PRDetail().Select(prDtPredicate, docTypeCode);

                    string sqlFilter = " t1.DocNo = '" + docNo + "' ";
                    var _tbl_PRDetails = new tbl_PRDetail().Select(sqlFilter, docTypeCode); //edit by sailom 14-06-2021

                    if (_tbl_PRDetails != null && _tbl_PRDetails.Count > 0)
                    {
                        tbl_PRDetails = _tbl_PRDetails;
                    }
                    else
                    {
                        tbl_PRDetails.Clear();
                    }
                }
            }
        }

        //public virtual void GetREData(string docNo)
        //{
        //    if (!string.IsNullOrEmpty(docNo))
        //    {
        //        Func<tbl_POMaster, bool> poPredicate = (x => x.DocNo == docNo);
        //        Func<tbl_PODetail, bool> pdtPredicate = (x => x.DocNo == docNo);

        //        var _tbl_POMaster = new tbl_POMaster().Select(poPredicate);
        //        if (_tbl_POMaster != null && _tbl_POMaster.Count > 0)
        //        {
        //            tbl_POMaster = new tbl_POMaster().Select(poPredicate)[0];
        //        }

        //        tbl_PODetails = new tbl_PODetail().Select(pdtPredicate);
        //    }
        //}

        public virtual int AddData()
        {
            string msg = "start BaseControl=>AddData";
            msg.WriteLog(this.GetType());

            List<int> ret = new List<int>();
            try
            {
                if (_tbl_POMaster != null)
                    ret.Add(_tbl_POMaster.Insert());

                if (_tbl_PODetails != null && _tbl_PODetails.Count > 0)
                {
                    foreach (var tbl_PODetail in _tbl_PODetails)
                    {
                        ret.Add(tbl_PODetail.Insert());
                    }
                }

                if (_tbl_PRMaster != null)
                    ret.Add(_tbl_PRMaster.Insert());

                if (_tbl_InvTransactions != null && _tbl_InvTransactions.Count > 0)
                {
                    foreach (var tbl_InvTransactions in _tbl_InvTransactions)
                    {
                        ret.Add(tbl_InvTransactions.Insert());
                    }
                }

                if (_tbl_InvMovements != null && _tbl_InvMovements.Count > 0)
                {
                    foreach (var tbl_InvMovement in _tbl_InvMovements)
                    {
                        ret.Add(tbl_InvMovement.Insert());
                    }
                }

                if (_tbl_InvWarehouses != null && _tbl_InvWarehouses.Count > 0)
                {
                    foreach (var tbl_InvWarehouse in _tbl_InvWarehouses)
                    {
                        ret.Add(tbl_InvWarehouse.Insert());
                    }
                }

                if (_tbl_InvTransactions != null && _tbl_InvTransactions.Count > 0)
                {
                    foreach (var tbl_InvTransactions in _tbl_InvTransactions)
                    {
                        ret.Add(tbl_InvTransactions.Insert());
                    }
                }


            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                ex.WriteLog(this.GetType());
            }

            msg = "end BaseControl=>AddData";
            msg.WriteLog(this.GetType());

            return ret.All(x => x == 1) ? 1 : 0;
        }

        public virtual int UpdateData(string docTypeCode = "")
        {
            int result = 0;
            List<int> ret = new List<int>();

            try
            {
                DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString);

                //PO--------------------------------------------------------------------------------
                if (_tbl_POMaster != null && !string.IsNullOrEmpty(_tbl_POMaster.DocNo))
                    ret.Add(_tbl_POMaster.UpdateEntity(db));

                if (_tbl_PODetails != null && _tbl_PODetails.Count > 0)
                {
                    ret.Add(_tbl_PODetails.UpdateEntity(db));
                    //foreach (var tbl_PODetail in _tbl_PODetails)
                    //{
                    //    ret.Add(tbl_PODetail.Update());
                    //}
                }
                //PO--------------------------------------------------------------------------------

                //PO Pre-Odrer--------------------------------------------------------------------------------
                if (_tbl_POMaster_PRE != null && !string.IsNullOrEmpty(_tbl_POMaster_PRE.DocNo))
                    ret.Add(_tbl_POMaster_PRE.UpdateEntity(db));

                if (_tbl_PODetails_PRE != null && _tbl_PODetails_PRE.Count > 0)
                {
                    ret.Add(_tbl_PODetails_PRE.UpdateEntity(db));
                    //foreach (var tbl_PODetail in _tbl_PODetails)
                    //{
                    //    ret.Add(tbl_PODetail.Update());
                    //}
                }
                //PO Pre-Odrer--------------------------------------------------------------------------------

                //PR--------------------------------------------------------------------------------
                if (_tbl_PRMaster != null && !string.IsNullOrEmpty(_tbl_PRMaster.DocNo))
                    ret.Add(_tbl_PRMaster.UpdateEntity(db));

                if (_tbl_PRDetails != null && _tbl_PRDetails.Count > 0)
                {
                    ret.Add(_tbl_PRDetails.UpdateEntity(db));
                    //foreach (var tbl_PRDetail in _tbl_PRDetails)
                    //{
                    //    ret.Add(tbl_PRDetail.Update());
                    //}
                }
                //PR--------------------------------------------------------------------------------

                //InvTransactions--------------------------------------------------------------------------------
                if (_tbl_InvTransactions != null && _tbl_InvTransactions.Count > 0)
                {
                    ret.Add(_tbl_InvTransactions.UpdateEntity(db));
                    //foreach (var tbl_InvTransactions in _tbl_InvTransactions)
                    //{
                    //    ret.Add(tbl_InvTransactions.Update());
                    //}
                }
                //InvTransactions--------------------------------------------------------------------------------

                //InvMovements--------------------------------------------------------------------------------
                if (_tbl_InvMovements != null && _tbl_InvMovements.Count > 0)
                {
                    ret.Add(_tbl_InvMovements.UpdateEntity(db));
                    //foreach (var tbl_InvMovement in _tbl_InvMovements)
                    //{
                    //    ret.Add(tbl_InvMovement.Update());
                    //}
                }
                //InvMovements--------------------------------------------------------------------------------

                //IV--------------------------------------------------------------------------------
                if (_tbl_IVMaster != null && !string.IsNullOrEmpty(_tbl_IVMaster.DocNo))
                    ret.Add(_tbl_IVMaster.UpdateEntity(db));

                if (_tbl_IVDetails != null && _tbl_IVDetails.Count > 0)
                {
                    ret.Add(_tbl_IVDetails.UpdateEntity(db));
                    //foreach (var _tbl_IVDetail in _tbl_IVDetails)
                    //{
                    //    ret.Add(_tbl_IVDetail.Update());
                    //}
                }
                //IV--------------------------------------------------------------------------------

                //DocRunning--------------------------------------------------------------------------------
                if (_tbl_DocRunning != null && _tbl_DocRunning.Count > 0)
                {
                    ret.Add(_tbl_DocRunning.UpdateEntity(db));
                    //foreach (var tbl_DocRunning in _tbl_DocRunning)
                    //{
                    //    ret.Add(tbl_DocRunning.Update());
                    //}
                }
                //DocRunning--------------------------------------------------------------------------------
                //PriceGroup--------------------------------------------------------------------------------
                if (_tbl_PriceGroups != null && _tbl_PriceGroups.Count > 0)
                {
                    foreach (var tbl_PriceGroup in _tbl_PriceGroups)
                    {
                        ret.Add(tbl_PriceGroup.UpdateEntity(db));
                    }
                }
                //PriceGroup--------------------------------------------------------------------------------

                //ShopType----------------------------------------------------------------------------------
                if (_tbl_ShopTypes != null && _tbl_ShopTypes.Count > 0)
                {
                    foreach (var tbl_ShopTypes in _tbl_ShopTypes)
                    {
                        ret.Add(tbl_ShopTypes.UpdateEntity(db));
                    }
                }
                //ShopType----------------------------------------------------------------------------------

                //Customer----------------------------------------------------------------------------------
                if (_tbl_ArCustomers != null && _tbl_ArCustomers.Count > 0)
                {
                    foreach (var tbl_ArCustomers in _tbl_ArCustomers)
                    {
                        ret.Add(tbl_ArCustomers.UpdateEntity(db));
                    }
                }
                //

                //CustomerShelf-------------------------------------------------------------------------------
                if (_tbl_ArCustomerShelfs != null && _tbl_ArCustomerShelfs.Count > 0)
                {
                    foreach (var tbl_ArCustomerShelfs in _tbl_ArCustomerShelfs)
                    {
                        ret.Add(tbl_ArCustomerShelfs.UpdateEntity(db));
                    }
                }
                //---------------------------------------------------------------------------------------------


                //PayDetail--------------------------------------------------------------------------------
                if (_tbl_PayDetails != null && _tbl_PayDetails.Count > 0)
                {
                    ret.Add(_tbl_PayDetails.UpdateEntity(db));
                    //foreach (var tbl_PayDetails in _tbl_PayDetails)
                    //{
                    //    ret.Add(tbl_PayDetails.Update());
                    //}
                }
                //PayDetail--------------------------------------------------------------------------------

                //PayMaster--------------------------------------------------------------------------------
                if (_tbl_PayMasters != null && _tbl_PayMasters.Count > 0)
                {
                    ret.Add(_tbl_PayMasters.UpdateEntity(db));
                    //foreach (var tbl_PayMasters in _tbl_PayMasters)
                    //{
                    //    ret.Add(tbl_PayMasters.Update());
                    //}
                }
                //PayMaster-----------------

                //InvWarehouses--------------------------------------------------------------------------------
                if (_tbl_InvWarehouses != null && _tbl_InvWarehouses.Count > 0)
                {
                    //ret.Add(_tbl_InvWarehouses.UpdateEntity(db));

                    foreach (var tbl_InvWarehouse in _tbl_InvWarehouses)
                    {
                        ret.Add(tbl_InvWarehouse.Update());
                    }
                }
                //InvWarehouses--------------------------------------------------------------------------------

                if (ret.All(x => x == 1))
                    result = db.SaveChanges();

                if (ret.Any(x => x == 0)) //reverse data
                {
                    ReverseData(docTypeCode);
                }

            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                ex.WriteLog(this.GetType());
            }

            return result != 0 ? 1 : 0;  //return ret.All(x => x == 1) ? 1 : 0;
        }

        public virtual int PerformUpdateData(string docTypeCode = "")
        {
            int result = 0;
            List<int> ret = new List<int>();
            bool isUpdateEntity = false;

            try
            {
                DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString);

                //PO--------------------------------------------------------------------------------
                if (_tbl_POMaster != null && !string.IsNullOrEmpty(_tbl_POMaster.DocNo))
                {
                    ret.Add(_tbl_POMaster.UpdateEntity(db));
                    isUpdateEntity = true;
                }

                if (_tbl_PODetails != null && _tbl_PODetails.Count > 0)
                {
                    ret.Add(_tbl_PODetails.PerformUpdate(db)); //edit by sailom 14/12/2021
                    //ret.Add(_tbl_PODetails.UpdateEntity(db));
                    //foreach (var tbl_PODetail in _tbl_PODetails)
                    //{
                    //    ret.Add(tbl_PODetail.Update());
                    //}
                }
                //PO--------------------------------------------------------------------------------

                //PO Pre-Odrer--------------------------------------------------------------------------------
                if (_tbl_POMaster_PRE != null && !string.IsNullOrEmpty(_tbl_POMaster_PRE.DocNo))
                { 
                    ret.Add(_tbl_POMaster_PRE.UpdateEntity(db));
                    isUpdateEntity = true;
                }

                if (_tbl_PODetails_PRE != null && _tbl_PODetails_PRE.Count > 0)
                {
                    ret.Add(_tbl_PODetails_PRE.PerformUpdate(db)); //edit by sailom 14/12/2021
                    //ret.Add(_tbl_PODetails_PRE.UpdateEntity(db));
                    //foreach (var tbl_PODetail in _tbl_PODetails)
                    //{
                    //    ret.Add(tbl_PODetail.Update());
                    //}
                }
                //PO Pre-Odrer--------------------------------------------------------------------------------

                //PR--------------------------------------------------------------------------------
                if (_tbl_PRMaster != null && !string.IsNullOrEmpty(_tbl_PRMaster.DocNo))
                {
                    ret.Add(_tbl_PRMaster.UpdateEntity(db));
                    isUpdateEntity = true;
                }

                if (_tbl_PRDetails != null && _tbl_PRDetails.Count > 0)
                {
                    ret.Add(_tbl_PRDetails.PerformUpdate(db)); //edit by sailom 14/12/2021
                    //foreach (var tbl_PRDetail in _tbl_PRDetails)
                    //{
                    //    ret.Add(tbl_PRDetail.Update());
                    //}
                }
                //PR--------------------------------------------------------------------------------

                //InvTransactions--------------------------------------------------------------------------------
                if (_tbl_InvTransactions != null && _tbl_InvTransactions.Count > 0)
                {
                    ret.Add(_tbl_InvTransactions.PerformUpdate(db)); //edit by sailom 14/12/2021
                    //ret.Add(_tbl_InvTransactions.UpdateEntity(db));
                    //foreach (var tbl_InvTransactions in _tbl_InvTransactions)
                    //{
                    //    ret.Add(tbl_InvTransactions.Update());
                    //}
                }
                //InvTransactions--------------------------------------------------------------------------------

                //InvMovements--------------------------------------------------------------------------------
                if (_tbl_InvMovements != null && _tbl_InvMovements.Count > 0)
                {
                    ret.Add(_tbl_InvMovements.PerformUpdate(db)); //edit by sailom 14/12/2021
                    //ret.Add(_tbl_InvMovements.UpdateEntity(db));
                    //foreach (var tbl_InvMovement in _tbl_InvMovements)
                    //{
                    //    ret.Add(tbl_InvMovement.Update());
                    //}
                }
                //InvMovements--------------------------------------------------------------------------------

                //IV--------------------------------------------------------------------------------
                if (_tbl_IVMaster != null && !string.IsNullOrEmpty(_tbl_IVMaster.DocNo))
                {
                    ret.Add(_tbl_IVMaster.UpdateEntity(db));
                    isUpdateEntity = true;
                }

                if (_tbl_IVDetails != null && _tbl_IVDetails.Count > 0)
                {
                    ret.Add(_tbl_IVDetails.PerformUpdate(db)); //edit by sailom 14/12/2021
                    //ret.Add(_tbl_IVDetails.UpdateEntity(db));
                    //foreach (var _tbl_IVDetail in _tbl_IVDetails)
                    //{
                    //    ret.Add(_tbl_IVDetail.Update());
                    //}
                }
                //IV--------------------------------------------------------------------------------

                //DocRunning--------------------------------------------------------------------------------
                if (_tbl_DocRunning != null && _tbl_DocRunning.Count > 0)
                {
                    ret.Add(_tbl_DocRunning.PerformUpdate(db)); //edit by sailom 07/02/2022

                    //ret.Add(_tbl_DocRunning.UpdateEntity(db));
                    //isUpdateEntity = true;

                    //foreach (var tbl_DocRunning in _tbl_DocRunning)
                    //{
                    //    ret.Add(tbl_DocRunning.Update());
                    //}
                }
                //DocRunning--------------------------------------------------------------------------------
                //PriceGroup--------------------------------------------------------------------------------
                if (_tbl_PriceGroups != null && _tbl_PriceGroups.Count > 0)
                {
                    foreach (var tbl_PriceGroup in _tbl_PriceGroups)
                    {
                        ret.Add(tbl_PriceGroup.UpdateEntity(db));
                    }
                    isUpdateEntity = true;
                }
                //PriceGroup--------------------------------------------------------------------------------

                //ShopType----------------------------------------------------------------------------------
                if (_tbl_ShopTypes != null && _tbl_ShopTypes.Count > 0)
                {
                    foreach (var tbl_ShopTypes in _tbl_ShopTypes)
                    {
                        ret.Add(tbl_ShopTypes.UpdateEntity(db));
                    }
                    isUpdateEntity = true;
                }
                //ShopType----------------------------------------------------------------------------------

                //Customer----------------------------------------------------------------------------------
                if (_tbl_ArCustomers != null && _tbl_ArCustomers.Count > 0)
                {
                    foreach (var tbl_ArCustomers in _tbl_ArCustomers)
                    {
                        ret.Add(tbl_ArCustomers.UpdateEntity(db));
                    }
                    isUpdateEntity = true;
                }
                //

                //CustomerShelf-------------------------------------------------------------------------------
                if (_tbl_ArCustomerShelfs != null && _tbl_ArCustomerShelfs.Count > 0)
                {
                    foreach (var tbl_ArCustomerShelfs in _tbl_ArCustomerShelfs)
                    {
                        ret.Add(tbl_ArCustomerShelfs.UpdateEntity(db));
                    }
                    isUpdateEntity = true;
                }
                //---------------------------------------------------------------------------------------------


                //PayDetail--------------------------------------------------------------------------------
                if (_tbl_PayDetails != null && _tbl_PayDetails.Count > 0)
                {
                    ret.Add(_tbl_PayDetails.PerformUpdate(db));  //edit by sailom .k 10/01/2022
                    //ret.Add(_tbl_PayDetails.UpdateEntity(db));
                    //isUpdateEntity = true;

                    //foreach (var tbl_PayDetails in _tbl_PayDetails)
                    //{
                    //    ret.Add(tbl_PayDetails.Update());
                    //}
                }
                //PayDetail--------------------------------------------------------------------------------

                //PayMaster--------------------------------------------------------------------------------
                if (_tbl_PayMasters != null && _tbl_PayMasters.Count > 0)
                {
                    ret.Add(_tbl_PayMasters.UpdateEntity(db));
                    isUpdateEntity = true;
                    //foreach (var tbl_PayMasters in _tbl_PayMasters)
                    //{
                    //    ret.Add(tbl_PayMasters.Update());
                    //}
                }
                //PayMaster-----------------

                //InvWarehouses--------------------------------------------------------------------------------
                if (_tbl_InvWarehouses != null && _tbl_InvWarehouses.Count > 0)
                {
                    ////edit by sailom .k 14/12/2021---------------------------------------
                    //string prodIDs = "";
                    //prodIDs = string.Join(",", GetProduct().Where(x => !x.FlagDel).Select(x => x.ProductID).ToList());
                    //foreach (var whid in _tbl_InvWarehouses.Select(x => x.WHID).Distinct().ToList())
                    //{
                    //    ret.Add(_tbl_InvWarehouses.First().ReCalc(whid, prodIDs));
                    //}
                    ////edit by sailom .k 14/12/2021---------------------------------------

                    //ret.Add(_tbl_InvWarehouses.UpdateEntity(db));

                    //foreach (var tbl_InvWarehouse in _tbl_InvWarehouses)
                    //{
                    //    ret.Add(tbl_InvWarehouse.Update());
                    //}

                    ret.Add(_tbl_InvWarehouses.PerformUpdate(db));
                }
                //InvWarehouses--------------------------------------------------------------------------------

                if (ret.All(x => x == 1))
                {
                    if (isUpdateEntity)
                        result = db.SaveChanges();
                    else
                        result = ret.All(x => x == 1) ? 1 : 0;
                }

                if (ret.Any(x => x == 0)) //reverse data
                {
                    ReverseData(docTypeCode);
                }

            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                ex.WriteLog(this.GetType());
            }

            return result != 0 ? 1 : 0;  //return ret.All(x => x == 1) ? 1 : 0;
        }

        private void ReverseData(string docTypeCode = "")
        {
            string msg = "start BaseControl=>ReverseData";
            msg.WriteLog(this.GetType());

            List<int> ret = new List<int>();
            try
            {
                //PO--------------------------------------------------------------------------------
                if (_tbl_POMaster != null && !string.IsNullOrEmpty(_tbl_POMaster.DocNo))
                    ret.Add(_tbl_POMaster.Delete());

                if (_tbl_PODetails != null && _tbl_PODetails.Count > 0)
                {
                    //foreach (var tbl_PODetail in _tbl_PODetails)
                    //{
                    //    ret.Add(tbl_PODetail.Delete());
                    //}
                    ret.Add(_tbl_PODetails.BulkRemove());
                }
                //PO--------------------------------------------------------------------------------

                //PR--------------------------------------------------------------------------------
                if (_tbl_PRMaster != null && !string.IsNullOrEmpty(_tbl_PRMaster.DocNo))
                    ret.Add(_tbl_PRMaster.Delete());

                if (_tbl_PRDetails != null && _tbl_PRDetails.Count > 0)
                {
                    //foreach (var tbl_PRDetail in _tbl_PRDetails)
                    //{
                    //    ret.Add(tbl_PRDetail.Delete());
                    //}
                    ret.Add(_tbl_PRDetails.BulkRemove());
                }
                //PR--------------------------------------------------------------------------------

                //InvTransactions--------------------------------------------------------------------------------
                if (_tbl_InvTransactions != null && _tbl_InvTransactions.Count > 0)
                {
                    //foreach (var tbl_InvTransactions in _tbl_InvTransactions)
                    //{
                    //    ret.Add(tbl_InvTransactions.Delete());
                    //}
                    ret.Add(_tbl_InvTransactions.BulkRemove());
                }
                //InvTransactions--------------------------------------------------------------------------------

                //InvMovements--------------------------------------------------------------------------------
                if (_tbl_InvMovements != null && _tbl_InvMovements.Count > 0)
                {
                    //foreach (var tbl_InvMovement in _tbl_InvMovements)
                    //{
                    //    ret.Add(tbl_InvMovement.Delete());
                    //}
                    ret.Add(_tbl_InvMovements.BulkRemove());
                }
                //InvMovements--------------------------------------------------------------------------------

                //IV--------------------------------------------------------------------------------
                if (_tbl_IVMaster != null && !string.IsNullOrEmpty(_tbl_IVMaster.DocNo))
                    ret.Add(_tbl_IVMaster.Delete());

                if (_tbl_IVDetails != null && _tbl_IVDetails.Count > 0)
                {
                    //foreach (var _tbl_IVDetail in _tbl_IVDetails)
                    //{
                    //    ret.Add(_tbl_IVDetail.Delete());
                    //}
                    ret.Add(_tbl_IVDetails.BulkRemove());
                }
                //IV--------------------------------------------------------------------------------

                //DocRunning--------------------------------------------------------------------------------
                this.PrepareDocRunning(docTypeCode, true);
                if (_tbl_DocRunning != null && _tbl_DocRunning.Count > 0)
                {
                    foreach (var tbl_DocRunning in _tbl_DocRunning)
                    {
                        ret.Add(tbl_DocRunning.Update());
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                ex.WriteLog(this.GetType());
            }

            msg = "end BaseControl=>ReverseData";
            msg.WriteLog(this.GetType());
        }

        public virtual int RemovePODetails(string docNo)
        {
            string msg = "start BaseControl=>RemovePODetails";
            msg.WriteLog(this.GetType());

            List<int> ret = new List<int>();
            try
            {
                if (_tbl_PODetails != null && _tbl_PODetails.Count > 0)
                {
                    ////Func<tbl_PODetail, bool> tbl_PODetailPre = (x => x.DocNo == _tbl_PODetails[0].DocNo);
                    //var oldTbl_PODetails = new tbl_PODetail().Select(docNo);
                    //foreach (var tbl_PODetail in oldTbl_PODetails)
                    //{
                    //    ret.Add(tbl_PODetail.Delete());
                    //}
                    ret.Add(_tbl_PODetails.BulkRemove());
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                ex.WriteLog(this.GetType());
            }

            msg = "end BaseControl=>RemovePODetails";
            msg.WriteLog(this.GetType());

            return ret.All(x => x == 1) ? 1 : 0;
        }

        public virtual int RemovePRDetails(string docNo)
        {
            string msg = "start BaseControl=>RemovePRDetails";
            msg.WriteLog(this.GetType());

            List<int> ret = new List<int>();
            try
            {
                if (_tbl_PRDetails != null && _tbl_PRDetails.Count > 0)
                {
                    ////Func<tbl_PRDetail, bool> tbl_PRDetailPre = (x => x.DocNo == _tbl_PRDetails[0].DocNo);
                    //var oldTbl_PRDetails = new tbl_PRDetail().Select(docNo);
                    //foreach (var tbl_PRDetail in oldTbl_PRDetails)
                    //{
                    //    ret.Add(tbl_PRDetail.Delete());
                    //}
                    ret.Add(_tbl_PRDetails.BulkRemove());
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                ex.WriteLog(this.GetType());
            }

            msg = "end BaseControl=>RemovePRDetails";
            msg.WriteLog(this.GetType());

            return ret.All(x => x == 1) ? 1 : 0;
        }

        public virtual int RemoveInvMovements(string refDocNo)
        {
            string msg = "start BaseControl=>RemoveInvMovements";
            msg.WriteLog(this.GetType());

            List<int> ret = new List<int>();
            var allInvMovements = new tbl_InvMovement().Select(refDocNo);
            try
            {
                if (_tbl_InvMovements != null && _tbl_InvMovements.Count > 0)
                {
                    //foreach (var item in _tbl_InvMovements)
                    //{
                    //    Func<tbl_InvMovement, bool> tbl_InvMovementPre = (x => x.ProductID == item.ProductID && x.WHID == item.WHID && x.RefDocNo == item.RefDocNo);
                    //    var delTbl_InvMovements = allInvMovements.Where(tbl_InvMovementPre).ToList();// new tbl_InvMovement().Select(tbl_InvMovementPre);
                    //    if (delTbl_InvMovements != null && delTbl_InvMovements.Count > 0)
                    //    {
                    //        foreach (tbl_InvMovement tbl_InvMovement in delTbl_InvMovements)
                    //        {
                    //            ret.Add(tbl_InvMovement.Delete());
                    //        }

                    //    }
                    //}
                    ret.Add(_tbl_InvMovements.BulkRemove());
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                ex.WriteLog(this.GetType());
            }

            msg = "end BaseControl=>RemoveInvMovements";
            msg.WriteLog(this.GetType());

            return ret.All(x => x == 1) ? 1 : 0;
        }

        public virtual int RemoveInvTransaction()
        {
            string msg = "start BaseControl=>RemoveInvTransaction";
            msg.WriteLog(this.GetType());

            List<int> ret = new List<int>();
            var allInvTransaction = new tbl_InvTransaction().SelectAll();
            try
            {
                if (_tbl_InvTransactions != null && _tbl_InvTransactions.Count > 0)
                {
                    //foreach (var item in _tbl_InvTransactions)
                    //{
                    //    Func<tbl_InvTransaction, bool> tbl_InvTransactionPre = (x => x.ProductID == item.ProductID && x.WHFrom == item.WHFrom && x.RefDocNo == item.RefDocNo);
                    //    var deltbl_InvTransactions = allInvTransaction.Where(tbl_InvTransactionPre).ToList(); // new tbl_InvTransaction().Select(tbl_InvTransactionPre);
                    //    if (deltbl_InvTransactions != null && deltbl_InvTransactions.Count > 0)
                    //    {
                    //        foreach (tbl_InvTransaction tbl_InvTransaction in deltbl_InvTransactions)
                    //        {
                    //            ret.Add(tbl_InvTransaction.Delete());
                    //        }

                    //    }
                    //}
                    ret.Add(_tbl_InvTransactions.BulkRemove());
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                ex.WriteLog(this.GetType());
            }

            msg = "end BaseControl=>RemoveInvTransaction";
            msg.WriteLog(this.GetType());

            return ret.All(x => x == 1) ? 1 : 0;
        }

        public virtual int RemoveIVMaster()
        {
            string msg = "start BaseControl=>RemoveIVMaster";
            msg.WriteLog(this.GetType());

            List<int> ret = new List<int>();
            try
            {
                if (_tbl_IVMaster != null)
                {
                    Func<tbl_IVMaster, bool> _tbl_IVMasterPre = (x => x.DocNo == _tbl_IVDetails[0].DocNo);
                    var oldTbl_IVMaster = new tbl_IVMaster().Select(_tbl_IVMasterPre);
                    foreach (var tbl_IVMaster in oldTbl_IVMaster)
                    {
                        ret.Add(tbl_IVMaster.Delete());
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                ex.WriteLog(this.GetType());
            }

            msg = "end BaseControl=>RemoveIVMaster";
            msg.WriteLog(this.GetType());

            return ret.All(x => x == 1) ? 1 : 0;
        }

        public virtual int RemoveIVDetails()
        {
            string msg = "start BaseControl=>RemoveIVDetails";
            msg.WriteLog(this.GetType());

            List<int> ret = new List<int>();
            try
            {
                if (_tbl_IVDetails != null && _tbl_IVDetails.Count > 0)
                {
                    //Func<tbl_IVDetail, bool> _tbl_IVDetailPre = (x => x.DocNo == _tbl_IVDetails[0].DocNo);
                    //var oldTbl_IVDetails = new tbl_IVDetail().Select(_tbl_IVDetailPre);
                    //foreach (var tbl_IVDetail in oldTbl_IVDetails)
                    //{
                    //    ret.Add(tbl_IVDetail.Delete());
                    //}
                    ret.Add(_tbl_IVDetails.BulkRemove());
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                ex.WriteLog(this.GetType());
            }

            msg = "end BaseControl=>RemoveIVDetails";
            msg.WriteLog(this.GetType());

            return ret.All(x => x == 1) ? 1 : 0;
        }

        public List<tbl_POMaster> GetAllPOMaster(string docTypeCode, Func<tbl_POMaster, bool> condition = null)
        {
            if (condition != null)
                return new tbl_POMaster().Select(condition, docTypeCode);
            else
                return new tbl_POMaster().SelectAll(docTypeCode);
        }

        public tbl_POMaster GetPOMaster(string docNo)
        {
            if (!string.IsNullOrEmpty(docNo))
            {
                //Func<tbl_POMaster, bool> poPredicate = (x => x.DocNo == docNo);

                var _po = new tbl_POMaster().SelectExists(docNo); //new tbl_POMaster().Select(poPredicate);
                if (_po != null && _po.Count > 0)
                {
                    var po = _po[0];
                    return po;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public tbl_POMaster_PRE GetPOMasterPRE(string docNo)
        {
            if (!string.IsNullOrEmpty(docNo))
            {
                //Func<tbl_POMaster, bool> poPredicate = (x => x.DocNo == docNo);

                var _po = new tbl_POMaster_PRE().SelectExists(docNo); //new tbl_POMaster().Select(poPredicate);
                if (_po != null && _po.Count > 0)
                {
                    var po = _po[0];
                    return po;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<tbl_POMaster> GetPOMasterByCustInvNo(string custInvNo)
        {
            if (!string.IsNullOrEmpty(custInvNo))
            {
                //Func<tbl_POMaster, bool> poPredicate = (x => x.DocNo == docNo);

                var _po = new tbl_POMaster().SelectExistsCustInvNO(custInvNo); //new tbl_POMaster().Select(poPredicate);
                if (_po != null && _po.Count > 0)
                {
                    return _po;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<tbl_Company> GetAllCompany()
        {
            return (new tbl_Company()).SelectAll();
        }

        /// <summary>
        /// For end day
        /// </summary>
        /// <returns></returns>
        public List<tbl_PRMaster> GetAllPRMaster()
        {
            return new tbl_PRMaster().SelectAll();
        }

        public List<tbl_PRMaster> GetAllPRMaster(DateTime docDate)
        {
            return new tbl_PRMaster().Select(docDate);
        }

        /// <summary>
        /// For end day
        /// </summary>
        /// <returns></returns>
        public List<tbl_POMaster> GetAllPOMaster()
        {
            return new tbl_POMaster().SelectAll();
        }

        public List<tbl_POMaster> GetAllPOMaster(DateTime docDate)
        {
            return new tbl_POMaster().Select(docDate);
        }

        public List<tbl_PODetail> GetAllPODetails(DateTime docDate)
        {
            return new tbl_PODetail().Select(docDate);
        }

        /// <summary>
        /// For end day
        /// </summary>
        /// <returns></returns>
        public List<tbl_PODetail> GetAllPODetails()
        {
            return new tbl_PODetail().SelectAll();
        }

        public tbl_PRMaster GetPRMaster(string docNo)
        {
            if (!string.IsNullOrEmpty(docNo))
            {
                //Func<tbl_PRMaster, bool> prPredicate = (x => x.DocNo == docNo);
                var _pr = new tbl_PRMaster().SelectExists(docNo); //new tbl_PRMaster().Select(prPredicate);
                if (_pr != null && _pr.Count > 0)
                {
                    var pr = _pr[0];
                    return pr;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<tbl_PRMaster> GetPRMaster(string docTypeCode, Func<tbl_PRMaster, bool> func)
        {
            return new tbl_PRMaster().Select(func, docTypeCode);
        }

        public List<tbl_PRDetail> GetPRDetails(string docTypeCode, Func<tbl_PRDetail, bool> func)
        {
            return new tbl_PRDetail().Select(func, docTypeCode);
        }

        public List<tbl_POMaster> GetPOMaster(string docTypeCode, Func<tbl_POMaster, bool> predicate = null)
        {
            if (predicate == null)
                return new tbl_POMaster().SelectAll(docTypeCode);
            else
                return new tbl_POMaster().Select(predicate, docTypeCode);
        }

        public List<tbl_PODetail> GetPODetails(string docTypeCode, Func<tbl_PODetail, bool> predicate = null)
        {
            if (predicate == null)
                return new tbl_PODetail().SelectAll(docTypeCode);
            else
                return new tbl_PODetail().Select(predicate, docTypeCode);
        }

        public List<tbl_PODetail> GetPODetails(string docNo)
        {
            if (!string.IsNullOrEmpty(docNo))
            {
                return new tbl_PODetail().Select(docNo);
                //Func<tbl_PODetail, bool> pdtPredicate = (x => x.DocNo == docNo);
                //return new tbl_PODetail().Select(pdtPredicate);
            }
            else
                return null;
        }

        public List<tbl_PODetail> GetPODetails(List<string> docNoList)
        {
            if (docNoList != null && docNoList.Count > 0)
            {
                return new tbl_PODetail().Select(docNoList);
            }
            else
                return null;
        }

        public List<tbl_PODetail_PRE> GetPODetailsPRE(string docNo)
        {
            if (!string.IsNullOrEmpty(docNo))
            {
                return new tbl_PODetail_PRE().Select(docNo);
                //Func<tbl_PODetail, bool> pdtPredicate = (x => x.DocNo == docNo);
                //return new tbl_PODetail().Select(pdtPredicate);
            }
            else
                return null;
        }

        public List<tbl_PRDetail> GetPRDetails(string docNo)
        {
            if (!string.IsNullOrEmpty(docNo))
            {
                return new tbl_PRDetail().Select(docNo);

                //Func<tbl_PRDetail, bool> prdtPredicate = (x => x.DocNo == docNo);
                //return new tbl_PRDetail().Select(prdtPredicate);
            }
            else
                return null;
        }

        public List<tbl_DiscountType> GetDiscountType()
        {
            return new tbl_DiscountType().SelectAll();
        }

        public bool CheckExistsPO(string docNo)
        {
            //Func<tbl_POMaster, bool> poPredicate = (x => x.DocNo == docNo);
            return new tbl_POMaster().SelectExists(docNo).Count > 0;
        }

        public bool CheckExistsPO_PRE(string docNo)
        {
            //Func<tbl_POMaster, bool> poPredicate = (x => x.DocNo == docNo);
            return new tbl_POMaster_PRE().SelectExists(docNo).Count > 0;
        }

        public bool CheckExistsPR(string docNo)
        {
            return new tbl_PRMaster().SelectExists(docNo).Count > 0;

            //Func<tbl_PRMaster, bool> prPredicate = (x => x.DocNo == docNo);
            //return new tbl_PRMaster().Select(prPredicate).Count > 0;
        }

        public bool CheckExistsIV(string docNo)
        {
            return new tbl_IVMaster().SelectExists(docNo).Count > 0;
            //Func<tbl_IVMaster, bool> ivPredicate = (x => x.DocNo == docNo);
            //return new tbl_IVMaster().Select(ivPredicate).Count > 0;
        }

        public bool CheckExistsCustomer(string custID)
        {
            Func<tbl_ArCustomer, bool> custPredicate = (x => x.CustomerID == custID);
            return new tbl_ArCustomer().SelectAll().Where(custPredicate).Count() > 0;
        }

        public bool CheckExistsCustomerShelf(string ShelfID)
        {
            Func<tbl_ArCustomerShelf, bool> custShelfPredicate = (x => x.ShelfID == ShelfID);
            return new tbl_ArCustomerShelf().Select(custShelfPredicate).Count > 0;
        }

        public bool CheckExistsShopType(int ShopTypeID)
        {
            Func<tbl_ShopType, bool> shopTypePredicate = (x => x.ShopTypeID == ShopTypeID);
            return new tbl_ShopType().SelectAllFlagPredi(shopTypePredicate).Count > 0;
        }

        public bool CheckExistsPriceGroup(int PriceGroupID)
        {
            Func<tbl_PriceGroup, bool> pgPredicate = (x => x.PriceGroupID == PriceGroupID);
            return new tbl_PriceGroup().Select(pgPredicate).Count > 0;
        }

        public string GenDocNo(string docTypeCode, string whCode = null)
        {
            return new DocTypeCode().GenDocNo(docTypeCode, whCode);
        }

        public string GenCustSAPCode(string docTypeCode, string whCode = null)
        {
            return new DocTypeCode().GenDocNo(docTypeCode, whCode);
        }

        public List<tbl_DocRunning> GetDocRunning(Func<tbl_DocRunning, bool> predicate)
        {
            return new tbl_DocRunning().Select(predicate);
        }

        public tbl_ApSupplier GetSupplier(string supplierCode)
        {
            tbl_ApSupplier tbl_ApSupplier = new tbl_ApSupplier();
            try
            {
                var allData = new Supplier().GetAllData();
                tbl_ApSupplier = allData.FirstOrDefault(x => x.SupplierCode == supplierCode);

                return tbl_ApSupplier;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public tbl_Company GetCompany()
        {
            tbl_Company tbl_Company = new tbl_Company();
            try
            {
                var allData = new tbl_Company().SelectAll();
                tbl_Company = allData.FirstOrDefault();

                return tbl_Company;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public tbl_Users GetUser(int userID)
        {
            tbl_Users tbl_Users = new tbl_Users();
            try
            {
                var allData = new tbl_Users().SelectAll();
                tbl_Users = allData.FirstOrDefault(x => x.UserID == userID);

                return tbl_Users;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public List<tbl_Branch> GetBranch(Func<tbl_Branch, bool> condition = null)
        {
            if (condition != null)
                return new tbl_Branch().Select(condition);
            else
                return new tbl_Branch().SelectAll();
        }

        public List<tbl_DocumentStatus> GetDocStatus()
        {
            return new tbl_DocumentStatus().SelectAll();
        }

        public List<tbl_DocumentType> GetDocumentType()
        {
            return new tbl_DocumentType().SelectAll();
        }

        public tbl_Employee GetEmployee(string empID)
        {
            tbl_Employee tbl_Employee = new tbl_Employee();
            try
            {
                //Last edit by sailom .k 07/02/2022

                //var allData = new tbl_Employee().SelectAll();
                //tbl_Employee = allData.FirstOrDefault(x => x.EmpID == empID);
                //return tbl_Employee;

                return new tbl_Employee().SelectSingle(empID);
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public List<tbl_Employee> GetEmployee(Func<tbl_Employee, bool> condition = null)
        {
            if (condition != null)
                return new tbl_Employee().Select(condition);
            else
                return new tbl_Employee().SelectAll();
        }

        public tbl_Employee GetEmployeeByUserName(string userName)
        {
            tbl_Employee tbl_Employee = new tbl_Employee();
            try
            {
                var allUser = new tbl_Users().SelectAll();
                var user = allUser.FirstOrDefault(x => x.Username.ToLower() == userName.ToLower());
                if (user != null)
                    tbl_Employee = GetEmployee(user.EmpID);
                else
                {
                    var bwh = GetBranchWarehouse(x => x.WHID == userName);
                    if (bwh != null)
                    {
                        tbl_Employee = GetEmployee(bwh.SaleEmpID);
                    }
                    else
                    {
                        tbl_Employee = GetEmployee(userName);
                    }
                }

                return tbl_Employee;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public List<tbl_ProductUom> GetUOM()
        {
            return new tbl_ProductUom().SelectAll();
        }

        public List<tbl_ProductUom> GetProductUOM(string productID)
        {
            try
            {
                List<tbl_ProductPriceGroup> tbl_ProductPriceGroup = new List<tbl_ProductPriceGroup>();
                Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductID == productID);
                tbl_ProductPriceGroup = (new tbl_ProductPriceGroup()).Select(tbl_ProductPriceGroupPre);

                List<tbl_ProductUom> tbl_ProductUom = new List<tbl_ProductUom>();
                Func<tbl_ProductUom, bool> tbl_ProductUomPre = (x => tbl_ProductPriceGroup.Select(p => p.ProductUomID).Contains(x.ProductUomID));
                tbl_ProductUom = (new tbl_ProductUom()).Select(tbl_ProductUomPre).ToList();

                return tbl_ProductUom;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public List<tbl_ProductUom> GetProductUOM(List<string> productIDList)
        {
            try
            {
                List<tbl_ProductPriceGroup> tbl_ProductPriceGroup = new List<tbl_ProductPriceGroup>();
                Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => productIDList.Contains(x.ProductID));
                tbl_ProductPriceGroup = (new tbl_ProductPriceGroup()).Select(tbl_ProductPriceGroupPre);

                List<tbl_ProductUom> tbl_ProductUom = new List<tbl_ProductUom>();
                Func<tbl_ProductUom, bool> tbl_ProductUomPre = (x => tbl_ProductPriceGroup.Select(p => p.ProductUomID).Contains(x.ProductUomID));
                tbl_ProductUom = (new tbl_ProductUom()).Select(tbl_ProductUomPre).ToList();

                return tbl_ProductUom;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public List<tbl_Product> GetProduct()
        {
            return new tbl_Product().SelectAll();
        }

        public List<tbl_Product> GetProduct(Func<tbl_Product, bool> predicate)
        {
            return new tbl_Product().Select(predicate);
        }

        public tbl_Product GetProduct(string productID)
        {
            return new tbl_Product().SelectSingle(productID);
        }

        public int GetMinProductUOM(Func<tbl_Product, bool> predicate)
        {
            int ret = 0;
            List<tbl_Product> tbl_Product = (new tbl_Product()).Select(predicate);
            if (tbl_Product != null && tbl_Product.Count > 0)
            {
                Func<tbl_ProductUomSet, bool> tbl_ProductUomSetPre = (x => tbl_Product.Select(p => p.ProductID).Contains(x.ProductID)); // && x.UomSetID == 1);
                tbl_ProductUomSet tbl_ProductUomSet = (new tbl_ProductUomSet()).Select(tbl_ProductUomSetPre).OrderBy(x => x.BaseQty).FirstOrDefault();
                if (tbl_ProductUomSet != null)
                {
                    ret = tbl_ProductUomSet.UomSetID;
                }
            }

            return ret;
        }

        public List<tbl_ProductGroup> GetProductGroup()
        {
            return new tbl_ProductGroup().SelectAll();
        }

        public List<tbl_ProductSubGroup> GetProductSubGroup()
        {
            return new tbl_ProductSubGroup().SelectAll();
        }

        public List<tbl_InvWarehouse> GetInvWarehouse(string productID, string whID)
        {
            return new tbl_InvWarehouse().Select(productID, whID);
            //Func<tbl_InvWarehouse, bool> tbl_InvWarehousePre = (x => x.ProductID == productID && x.WHID == whID);
            //return new tbl_InvWarehouse().Select(tbl_InvWarehousePre);
        }

        public List<tbl_InvWarehouse> GetInvWarehouse(string whID)
        {
            return new tbl_InvWarehouse().Select(whID);
            //Func<tbl_InvWarehouse, bool> tbl_InvWarehousePre = (x => x.ProductID == productID && x.WHID == whID);
            //return new tbl_InvWarehouse().Select(tbl_InvWarehousePre);
        }

        public List<tbl_InvMovement> GetStockMovement(string productID, string whID)
        {
            return new tbl_InvMovement().SelectStock(productID, whID);
        }

        public List<tbl_InvMovement> GetTotalStockMovement(List<string> productID, string whID)
        {
            return new tbl_InvMovement().SelectTotalStock(productID, whID);
        }

        public List<tbl_InvMovement> ValidatetStockMovement()
        {
            return new tbl_InvMovement().ValidateStock();
        }

        public List<tbl_InvWarehouse> GetInvWarehouse(Func<tbl_InvWarehouse, bool> func = null)
        {
            if (func != null)
            {
                return new tbl_InvWarehouse().Select(func);
            }
            else
            {
                return new tbl_InvWarehouse().SelectAll();
            }
        }

        public List<tbl_InvMovement> GetInvMovement(string docNo)
        {
            return new tbl_InvMovement().Select(docNo);

            //Func<tbl_InvMovement, bool> tbl_InvMovementPre = (x => x.RefDocNo == docNo);
            //return new tbl_InvMovement().Select(tbl_InvMovementPre);
        }

        public List<tbl_PriceGroup> GetPriceGroup(int pricegroupID)
        {
            Func<tbl_PriceGroup, bool> tbl_PriceGroupPre = (x => x.PriceGroupID == pricegroupID);
            return new tbl_PriceGroup().Select(tbl_PriceGroupPre);
        }

        public List<tbl_ArCustomer> GetCustomer(string custID)
        {
            Func<tbl_ArCustomer, bool> tbl_ArCustomerPre = (x => x.CustomerID == custID);
            return new tbl_ArCustomer().SelectSingle(custID);
        }

        public List<tbl_ShopType> GetShopType(int shopTypeID)
        {
            Func<tbl_ShopType, bool> tbl_ShopTypePre = (x => x.ShopTypeID == shopTypeID);
            return new tbl_ShopType().SelectAllFlagPredi(tbl_ShopTypePre);
        }

        public List<tbl_InvTransaction> GetInvTransaction(string docNo)
        {
            return new tbl_InvTransaction().Select(docNo);

            //Func<tbl_InvTransaction, bool> tbl_InvTransactionPre = (x => x.RefDocNo == docNo);
            //return new tbl_InvTransaction().Select(tbl_InvTransactionPre);
        }

        public List<tbl_ProductUom> GetUOM(Func<tbl_ProductUom, bool> tbl_ProductUomPre)
        {
            return new tbl_ProductUom().Select(tbl_ProductUomPre);
        }

        public List<tbl_DisplayImage> GetDisplayImage(Func<tbl_DisplayImage, bool> condition = null)
        {
            if (condition != null)
                return new tbl_DisplayImage().Select(condition);
            else
                return new tbl_DisplayImage().SelectAll();
        }

        public List<tbl_ProductSubGroup> GetProductSubGroup(Func<tbl_ProductSubGroup, bool> condition = null)
        {
            if (condition != null)
                return new tbl_ProductSubGroup().Select(condition);
            else
                return new tbl_ProductSubGroup().SelectAll();
        }

        public List<tbl_ProductUomSet> GetUOMSet(Func<tbl_ProductUomSet, bool> tbl_ProductUomSetPre = null)
        {
            if (tbl_ProductUomSetPre == null)
                return new tbl_ProductUomSet().SelectAll();
            else
                return new tbl_ProductUomSet().Select(tbl_ProductUomSetPre);
        }

        public List<tbl_ProductPriceGroup> GetProductPriceGroup(Func<tbl_ProductPriceGroup, bool> predicate = null)
        {
            if (predicate != null)
                return new tbl_ProductPriceGroup().Select(predicate);
            else
                return new tbl_ProductPriceGroup().SelectAll();
        }

        public tbl_BranchWarehouse GetBranchWarehouse(Func<tbl_BranchWarehouse, bool> predicate)
        {
            var _tbl_BranchWarehouse = new tbl_BranchWarehouse().Select(predicate);
            if (_tbl_BranchWarehouse != null && _tbl_BranchWarehouse.Count > 0)
            {
                return _tbl_BranchWarehouse[0];//new tbl_BranchWarehouse().Select(predicate)[0]; Last edit by sailom.k 14/09/2021 tunning performance
            }
            else
                return null;
        }

        public tbl_BranchWarehouse GetBranchWarehouse(string whCode, int vanType)
        {
            return new tbl_BranchWarehouse().SelectSingle(whCode, vanType);
        }

        public tbl_BranchWarehouse GetBranchWarehouse(string whCode)
        {
            return new tbl_BranchWarehouse().SelectSingle(whCode);
        }

        public List<tbl_BranchWarehouse> GetAllBranchWarehouse(Func<tbl_BranchWarehouse, bool> condition = null)
        {
            if (condition != null)
                return new tbl_BranchWarehouse().Select(condition);
            else
                return new tbl_BranchWarehouse().SelectAll();
        }

        public tbl_SaleBranchSummary GetSaleBranchSummary(Func<tbl_SaleBranchSummary, bool> predicate)
        {
            var _tbl_SaleBranchSummary = new tbl_SaleBranchSummary().Select(predicate);
            if (_tbl_SaleBranchSummary != null && _tbl_SaleBranchSummary.Count > 0)
            {
                return new tbl_SaleBranchSummary().Select(predicate)[0];
            }
            else
                return null;
        }

        public tbl_SaleBranchSummary ValidateCheckEndDay(string branchID, DateTime saleDate)
        {
            return new tbl_SaleBranchSummary().Select(branchID, saleDate);
        }

        public List<tbl_SaleBranchSummary> GetAllSaleBranchSummary()
        {
            return new tbl_SaleBranchSummary().SelectAll();
        }

        public List<tbl_Cause> GetCause()
        {
            return new tbl_Cause().SelectAll();
        }

        public List<tbl_AdmFormList> GetAllFromMenu()
        {
            return new tbl_AdmFormList().SelectAll();
        }

        public List<tbl_MstPart> GetAllMstPart()
        {
            return new tbl_MstPart().SelectAll();
        }

        public List<tbl_PriceGroup> GetAllPriceGroup()
        {
            return new tbl_PriceGroup().SelectAll();
        }

        public List<tbl_BranchGroup> GetAllBranchGroup()
        {
            return new tbl_BranchGroup().SelectAll();
        }

        public List<tbl_ProductFlavour> GetProductFlavour(Func<tbl_ProductFlavour, bool> condition = null)
        {
            if (condition == null)
                return new tbl_ProductFlavour().SelectAll();
            else
                return new tbl_ProductFlavour().Select(condition);
        }

        public List<tbl_ProductUom> GetProductUom(Func<tbl_ProductUom, bool> condition = null)
        {
            if (condition == null)
                return new tbl_ProductUom().SelectAllNonFlag();
            else
                return new tbl_ProductUom().SelectNonFlag(condition);
        }

        public List<tbl_Department> GetDepartment(Func<tbl_Department, bool> condition = null)
        {
            if (condition == null)
                return new tbl_Department().SelectAllNonFlag();
            else
                return new tbl_Department().SelectNonFlag(condition);
        }

        public List<tbl_Position> GetPosition(Func<tbl_Position, bool> condition = null)
        {
            if (condition == null)
                return new tbl_Position().SelectAllNonFlag();
            else
                return new tbl_Position().SelectNonFlag(condition);
        }

        public List<tbl_ApSupplier> GetSupplier(Func<tbl_ApSupplier, bool> condition = null)
        {
            if (condition == null)
                return new tbl_ApSupplier().SelectAllNonFlag();
            else
                return new tbl_ApSupplier().SelectNonFlag(condition);
        }

        public List<tbl_ApSupplierType> GetSupplierType(Func<tbl_ApSupplierType, bool> condition = null)
        {
            if (condition == null)
                return new tbl_ApSupplierType().SelectAllNonFlag();
            else
                return new tbl_ApSupplierType().SelectNonFlag(condition);
        }

        public List<tbl_ProductRemarkReject> GetProductRemarkReject(Func<tbl_ProductRemarkReject, bool> condition = null)
        {
            if (condition == null)
                return new tbl_ProductRemarkReject().SelectAll();
            else
                return new tbl_ProductRemarkReject().Select(condition);
        }

        public List<tbl_DocumentType> GetDocumentType(Func<tbl_DocumentType, bool> condition = null)
        {
            if (condition != null)
                return new tbl_DocumentType().SelectNonFlag(condition);
            else
                return new tbl_DocumentType().SelectAllNonFlag();
        }

        public List<tbl_MstProvince> GetMstProvince(Func<tbl_MstProvince, bool> condition = null)
        {
            if (condition != null)
                return new tbl_MstProvince().Select(condition);
            else
                return new tbl_MstProvince().SelectAll();
        }

        public List<tbl_MstDistrict> GetMstDistrict(Func<tbl_MstDistrict, bool> condition = null)
        {
            if (condition != null)
                return new tbl_MstDistrict().Select(condition);
            else
                return new tbl_MstDistrict().SelectAll();
        }

        public List<tbl_MstArea> GetMstArea(Func<tbl_MstArea, bool> condition = null)
        {
            if (condition != null)
                return new tbl_MstArea().Select(condition);
            else
                return new tbl_MstArea().SelectAll();
        }

        public List<Model.tbl_MstProvince> GetAllProvince()
        {
            return new Model.tbl_MstProvince().SelectAll();
        }

        public List<tbl_Department> GetAllDepartment()
        {
            return new tbl_Department().SelectAll();
        }

        public List<tbl_Roles> GetAllRoles()
        {
            return new tbl_Roles().SelectAll();
        }

        public List<tbl_Position> GetAllPosition()
        {
            return new tbl_Position().SelectAll();
        }

        public Dictionary<string, string> GetAllTitleName()
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            var titileList = new tbl_Employee().SelectAll().Select(x => x.TitleName).Distinct().ToList();
            foreach (var item in titileList)
            {
                ret.Add(item, item);
            }

            return ret;
        }

        public List<tbl_VanType> GetWHType(Func<tbl_VanType, bool> condition = null)
        {
            if (condition != null)
                return new tbl_VanType().Select(condition);
            else
                return new tbl_VanType().SelectAll();
        }

        public List<Model.tbl_ShopType> GetAllShopType()
        {
            return new Model.tbl_ShopType().SelectAll();
        }

        public List<Model.tbl_ShopType> GetShopTypeALLFlag()
        {
            return new Model.tbl_ShopType().SelectAllFlag();
        }

        public List<tbl_ArCustomerType> GetAllCustomerType()
        {
            return new tbl_ArCustomerType().SelectAll();
        }

        public List<tbl_BranchWarehouse> GetAllVan()
        {
            return new tbl_BranchWarehouse().SelectAll();
        }

        public List<tbl_ShopType> GetShopType(Func<tbl_ShopType, bool> condition = null)
        {
            if (condition != null)
                return new tbl_ShopType().Select(condition);
            else
                return new tbl_ShopType().SelectAll();
        }

        public List<tbl_ShopType> GetShopTypeALL(Func<tbl_ShopType, bool> condition = null)
        {
            if (condition != null)
                return new tbl_ShopType().SelectFlag(condition);
            else
                return new tbl_ShopType().SelectAllFlag();
        }

        public List<tbl_ShopTypeGroup> GetShopTypeGroup(Func<tbl_ShopTypeGroup, bool> condition = null)
        {
            if (condition != null)
                return new tbl_ShopTypeGroup().Select(condition);
            else
                return new tbl_ShopTypeGroup().SelectAll();
        }

        public List<tbl_PayMaster> GetPayMaster(Func<tbl_PayMaster, bool> condition = null)
        {
            if (condition != null)
                return new tbl_PayMaster().Select(condition);
            else
                return new tbl_PayMaster().SelectAll();
        }

        public List<tbl_PayDetail> GetPayDetail(Func<tbl_PayDetail, bool> condition = null)
        {
            if (condition != null)
                return new tbl_PayDetail().Select(condition);
            else
                return new tbl_PayDetail().SelectAll();
        }

        public List<tbl_SalAreaDistrict> GetSaleAreaDistrict(Func<tbl_SalAreaDistrict, bool> condition = null)
        {
            if (condition != null)
                return new tbl_SalAreaDistrict().Select(condition);
            else
                return new tbl_SalAreaDistrict().SelectAll();
        }

        public List<tbl_ArCustomer> GetCustomer(Func<tbl_ArCustomer, bool> condition = null)
        {
            if (condition != null)
                return new tbl_ArCustomer().SelectAll().Where(condition).ToList();
            else
                return new tbl_ArCustomer().SelectAll();
        }

        public List<tbl_ArCustomerType> GetCustomerType(Func<tbl_ArCustomerType, bool> condition = null)
        {
            if (condition != null)
                return new tbl_ArCustomerType().SelectFlag(condition).ToList();
            else
                return new tbl_ArCustomerType().SelectAllFlag();
        }

        public List<tbl_ArCustomerShelf> GetCustomerShelf(Func<tbl_ArCustomerShelf, bool> condition = null)
        {
            if (condition != null)
                return new tbl_ArCustomerShelf().Select(condition);
            else
                return new tbl_ArCustomerShelf().SelectAll();
        }

        public List<tbl_ArCustomerShelf> GetCustomerShelf(string ShelfID)
        {
            Func<tbl_ArCustomerShelf, bool> tbl_ArCustomerShelfPre = (x => x.ShelfID == ShelfID);
            return new tbl_ArCustomerShelf().Select(tbl_ArCustomerShelfPre);
        }

        public List<tbl_ArCustomerShelf> GetCustomerShelfByCustID(string customerID)
        {
            //Func<tbl_ArCustomerShelf, bool> tbl_ArCustomerShelfPre = (x => x.ShelfID == ShelfID);
            return new tbl_ArCustomerShelf().SelectByCustID(customerID);
        }

        public List<tbl_PriceGroup> GetPriceGroup(Func<tbl_PriceGroup, bool> condition = null)
        {
            if (condition != null)
                return new tbl_PriceGroup().Select(condition);
            else
                return new tbl_PriceGroup().SelectAll();
        }

        public List<tbl_SalArea> GetSaleArea()
        {
            return new tbl_SalArea().SelectAll();
        }

        public List<tbl_SalArea> GetAllMarket()
        {
            return new tbl_SalArea().SelectAll();
        }

        public Dictionary<string, string> GetAllMarketDic()
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            var marketList = new tbl_SalAreaDistrict().SelectAll().Select(x => x.SalAreaID).Distinct().ToList();
            foreach (var item in marketList)
            {
                ret.Add(item, item);
            }

            return ret;
        }

        public Dictionary<string, string> GetAllVanDic()
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            var vanList = new tbl_BranchWarehouse().SelectAll().Select(x => x.WHCode).ToList();
            foreach (var item in vanList)
            {
                ret.Add(item, item);
            }

            return ret;
        }

        //public Dictionary<string, string> GetAllTitleName()
        //{
        //    Dictionary<string, string> ret = new Dictionary<string, string>();
        //    var titileList = new tbl_Employee().SelectAll().Select(x => x.TitleName).Distinct().ToList();
        //    foreach (var item in titileList)
        //    {
        //        ret.Add(item, item);
        //    }

        //    return ret;
        //}


        //public List<tbl_VanType> GetWHType(Func<tbl_VanType, bool> condition = null)
        //{
        //    if (condition != null)
        //        return new tbl_VanType().Select(condition);
        //    else
        //        return new tbl_VanType().SelectAll();
        //}

        public List<tbl_BranchWarehouse> GetAllVanCon(Func<tbl_BranchWarehouse, bool> condition = null)
        {
            if (condition != null)
                return new tbl_BranchWarehouse().Select(condition);
            else
                return new tbl_BranchWarehouse().SelectAll();
        }

        public List<tbl_SalArea> GetSaleArea(Func<tbl_SalArea, bool> condition = null)
        {
            if (condition != null)
                return new tbl_SalArea().Select(condition);
            else
                return new tbl_SalArea().SelectAll();
        }

        public List<tbl_Zone> GetZone(Func<tbl_Zone, bool> condition = null)
        {
            if (condition != null)
                return new tbl_Zone().Select(condition);
            else
                return new tbl_Zone().SelectAll();
        }

        public List<tbl_Users> GetUser(Func<tbl_Users, bool> condition = null)
        {
            if (condition != null)
                return new tbl_Users().Select(condition);
            else
                return new tbl_Users().SelectAll();
        }

        public List<tbl_SendData> GetSendData(Func<tbl_SendData, bool> condition = null)
        {
            if (condition != null)
                return new tbl_SendData().Select(condition);
            else
                return new tbl_SendData().SelectAll();
        }

        public bool UpdateCustomerSAPCode(string customerID)
        {
            string msg = "start BaseControl=>UpdateCustomerSAPCode->customerID";
            msg.WriteLog(this.GetType());

            bool ret = false;

            try
            {
                using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("proc_update_customer_sap_code", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add(new SqlParameter("@CustomerID", customerID.Trim()));
                    var result = cmd.ExecuteNonQuery();
                    ret = true;

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                ret = false;
            }

            msg = "end BaseControl=>UpdateCustomerSAPCode->customerID";
            msg.WriteLog(this.GetType());

            return ret;
        }

        public bool UpdateCustomerSAPCode(string customerID, string ivDocNo, string poDocNo)
        {
            string msg = "start BaseControl=>UpdateCustomerSAPCode->customerID,ivDocNo,poDocNo";
            msg.WriteLog(this.GetType());

            bool ret = false;

            try
            {
                using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("proc_update_customer_sap_code", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add(new SqlParameter("@CustomerID", customerID.Trim()));
                    var result = cmd.ExecuteNonQuery();
                    ret = true;

                    con.Close();
                }

                //verify iv details when diff po details Last edit by sailom .k 18/10/2021------------------------
                if (ret)
                {
                    VerifyIVDetails(ivDocNo, poDocNo);
                }
                //verify iv details when diff po details Last edit by sailom .k 18/10/2021------------------------
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                ret = false;
            }

            msg = "end BaseControl=>UpdateCustomerSAPCode->customerID,ivDocNo,poDocNo";
            msg.WriteLog(this.GetType());

            return ret;
        }

        public bool VerifyIVDetails(string ivDocNo, string poDocNo)
        {
            try
            {
                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@IVDocNo", ivDocNo);
                sqlParmas.Add("@PODocNo", poDocNo);

                string sql = "proc_tbl_IVDetail_fix";

                My_DataTable_Extensions.ExecuteSQLScalar(sql, CommandType.StoredProcedure, sqlParmas);

                return true;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return false;
            }
        }

        public List<tbl_CfgSetting> GetCfgSetting()
        {
            return new tbl_CfgSetting().SelectAll();
        }

        public List<tbl_HQ_Promotion_Master> GetHQ_Promotion_Master(Func<tbl_HQ_Promotion_Master, bool> condition = null)
        {
            if (condition != null)
                return new tbl_HQ_Promotion_Master().Select(condition);
            else
                return new tbl_HQ_Promotion_Master().SelectAll();
        }

        public List<tbl_HQ_Reward> GetHQ_Reward(Func<tbl_HQ_Reward, bool> condition = null)
        {
            if (condition != null)
                return new tbl_HQ_Reward().Select(condition);
            else
                return new tbl_HQ_Reward().SelectAll();
        }

        public List<tbl_HQ_SKUGroup> GetHQ_SKUGroup(Func<tbl_HQ_SKUGroup, bool> condition = null)
        {
            if (condition != null)
                return new tbl_HQ_SKUGroup().Select(condition);
            else
                return new tbl_HQ_SKUGroup().SelectAll();
        }

        public List<tbl_HQ_SKUGroup_EXC> GetHQ_SKUGroupExc(Func<tbl_HQ_SKUGroup_EXC, bool> condition = null)
        {
            if (condition != null)
                return new tbl_HQ_SKUGroup_EXC().Select(condition);
            else
                return new tbl_HQ_SKUGroup_EXC().SelectAll();
        }
    }
}
