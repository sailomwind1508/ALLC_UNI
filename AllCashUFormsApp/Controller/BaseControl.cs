using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class BaseControl : Promotion
    {
        private tbl_POMaster _tbl_POMaster = null;
        private List<tbl_PODetail> _tbl_PODetails = null;
        private tbl_PRMaster _tbl_PRMaster = null;
        private List<tbl_PRDetail> _tbl_PRDetails = null;

        private List<tbl_InvMovement> _tbl_InvMovements = null;
        private List<tbl_InvWarehouse> _tbl_InvWarehouses = null;
        private List<tbl_InvTransaction> _tbl_InvTransactions = null;
        private tbl_IVMaster _tbl_IVMaster = null;
        private List<tbl_IVDetail> _tbl_IVDetails = null;
        private List<tbl_DocRunning> _tbl_DocRunning = null;
        private Func<tbl_POMaster, bool> _docTypepredicate = null;

        private string _docTypeCode;

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

            _tbl_InvMovements = new List<tbl_InvMovement>();
            _tbl_InvWarehouses = new List<tbl_InvWarehouse>();
            _tbl_InvTransactions = new List<tbl_InvTransaction>();
            _tbl_DocRunning = new List<tbl_DocRunning>();

            _docTypepredicate = (x => x.DocTypeCode == docTypeCode);
            _docTypeCode = docTypeCode;
        }

        public virtual void GetDocData(string docNo)
        {
            if (!string.IsNullOrEmpty(docNo))
            {
                Func<tbl_POMaster, bool> poPredicate = (x => x.DocNo == docNo);
                Func<tbl_PODetail, bool> pdtPredicate = (x => x.DocNo == docNo);

                var _tbl_POMaster = new tbl_POMaster().Select(poPredicate);
                if (_tbl_POMaster != null && _tbl_POMaster.Count > 0) //PO
                {
                    tbl_POMaster = _tbl_POMaster[0];
                }
                else
                    tbl_POMaster = new tbl_POMaster();

                if (string.IsNullOrEmpty(tbl_POMaster.DocNo)) //PR
                {
                    Func<tbl_PRMaster, bool> prPredicate = (x => x.DocNo == docNo);
                    var _tbl_PRMaster = new tbl_PRMaster().Select(prPredicate);
                    if (_tbl_PRMaster != null && _tbl_PRMaster.Count > 0)
                    {
                        tbl_PRMaster = _tbl_PRMaster[0];
                    }
                    else
                    {
                        tbl_PRMaster = new tbl_PRMaster();
                    }
                }

                tbl_PODetails = new tbl_PODetail().Select(pdtPredicate); //PO

                if (tbl_PODetails.Count == 0) //PR
                {
                    Func<tbl_PRDetail, bool> prDtPredicate = (x => x.DocNo == docNo);
                    var _tbl_PRDetails = new tbl_PRDetail().Select(prDtPredicate);
                    if (_tbl_PRDetails != null && _tbl_PRDetails.Count > 0)
                    {
                        tbl_PRDetails = _tbl_PRDetails;
                    }
                    else
                    {
                        tbl_PODetails.Clear();
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

            return ret.All(x => x == 1) ? 1 : 0;
        }

        public virtual int UpdateData()
        {
            List<int> ret = new List<int>();
            try
            {
                //PO--------------------------------------------------------------------------------
                if (_tbl_POMaster != null && !string.IsNullOrEmpty(_tbl_POMaster.DocNo))
                    ret.Add(_tbl_POMaster.Update());

                if (_tbl_PODetails != null && _tbl_PODetails.Count > 0)
                {
                    foreach (var tbl_PODetail in _tbl_PODetails)
                    {
                        ret.Add(tbl_PODetail.Update());
                    }
                }
                //PO--------------------------------------------------------------------------------

                //PR--------------------------------------------------------------------------------
                if (_tbl_PRMaster != null && !string.IsNullOrEmpty(_tbl_PRMaster.DocNo))
                    ret.Add(_tbl_PRMaster.Update());

                if (_tbl_PRDetails != null && _tbl_PRDetails.Count > 0)
                {
                    foreach (var tbl_PRDetail in _tbl_PRDetails)
                    {
                        ret.Add(tbl_PRDetail.Update());
                    }
                }
                //PR--------------------------------------------------------------------------------

                //InvTransactions--------------------------------------------------------------------------------
                if (_tbl_InvTransactions != null && _tbl_InvTransactions.Count > 0)
                {
                    foreach (var tbl_InvTransactions in _tbl_InvTransactions)
                    {
                        ret.Add(tbl_InvTransactions.Update());
                    }
                }
                //InvTransactions--------------------------------------------------------------------------------

                //InvMovements--------------------------------------------------------------------------------
                if (_tbl_InvMovements != null && _tbl_InvMovements.Count > 0)
                {
                    foreach (var tbl_InvMovement in _tbl_InvMovements)
                    {
                        ret.Add(tbl_InvMovement.Update());
                    }
                }
                //InvMovements--------------------------------------------------------------------------------

                //InvWarehouses--------------------------------------------------------------------------------
                if (_tbl_InvWarehouses != null && _tbl_InvWarehouses.Count > 0)
                {
                    foreach (var tbl_InvWarehouse in _tbl_InvWarehouses)
                    {
                        ret.Add(tbl_InvWarehouse.Update());
                    }
                }
                //InvWarehouses--------------------------------------------------------------------------------

                //IV--------------------------------------------------------------------------------
                if (_tbl_IVMaster != null && !string.IsNullOrEmpty(_tbl_IVMaster.DocNo))
                    ret.Add(_tbl_IVMaster.Update());

                if (_tbl_IVDetails != null && _tbl_IVDetails.Count > 0)
                {
                    foreach (var _tbl_IVDetail in _tbl_IVDetails)
                    {
                        ret.Add(_tbl_IVDetail.Update());
                    }
                }
                //IV--------------------------------------------------------------------------------

                //DocRunning--------------------------------------------------------------------------------
                if (_tbl_DocRunning != null && _tbl_DocRunning.Count > 0)
                {
                    foreach (var tbl_DocRunning in _tbl_DocRunning)
                    {
                        ret.Add(tbl_DocRunning.Update());
                    }
                }
                //DocRunning--------------------------------------------------------------------------------
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                ex.WriteLog(this.GetType());
            }

            return ret.All(x => x == 1) ? 1 : 0;
        }

        public virtual int RemovePODetails()
        {
            List<int> ret = new List<int>();
            try
            {
                if (_tbl_PODetails != null && _tbl_PODetails.Count > 0)
                {
                    Func<tbl_PODetail, bool> tbl_PODetailPre = (x => x.DocNo == _tbl_PODetails[0].DocNo);
                    var oldTbl_PODetails = new tbl_PODetail().Select(tbl_PODetailPre);
                    foreach (var tbl_PODetail in oldTbl_PODetails)
                    {
                        ret.Add(tbl_PODetail.Delete());
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

        public virtual int RemovePRDetails()
        {
            List<int> ret = new List<int>();
            try
            {
                if (_tbl_PRDetails != null && _tbl_PRDetails.Count > 0)
                {
                    Func<tbl_PRDetail, bool> tbl_PRDetailPre = (x => x.DocNo == _tbl_PRDetails[0].DocNo);
                    var oldTbl_PRDetails = new tbl_PRDetail().Select(tbl_PRDetailPre);
                    foreach (var tbl_PRDetail in oldTbl_PRDetails)
                    {
                        ret.Add(tbl_PRDetail.Delete());
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

        public virtual int RemoveInvMovements()
        {
            List<int> ret = new List<int>();

            try
            {
                if (_tbl_InvMovements != null && _tbl_InvMovements.Count > 0)
                {
                    foreach (var item in _tbl_InvMovements)
                    {
                        Func<tbl_InvMovement, bool> tbl_InvMovementPre = (x => x.ProductID == item.ProductID && x.WHID == item.WHID && x.RefDocNo == item.RefDocNo);
                        var delTbl_InvMovements = new tbl_InvMovement().Select(tbl_InvMovementPre);
                        if (delTbl_InvMovements != null && delTbl_InvMovements.Count > 0)
                        {
                            foreach (tbl_InvMovement tbl_InvMovement in delTbl_InvMovements)
                            {
                                ret.Add(tbl_InvMovement.Delete());
                            }

                        }
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

        public virtual int RemoveInvTransaction()
        {
            List<int> ret = new List<int>();

            try
            {
                if (_tbl_InvTransactions != null && _tbl_InvTransactions.Count > 0)
                {
                    foreach (var item in _tbl_InvTransactions)
                    {
                        Func<tbl_InvTransaction, bool> tbl_InvTransactionPre = (x => x.ProductID == item.ProductID && x.WHFrom == item.WHFrom && x.RefDocNo == item.RefDocNo);
                        var deltbl_InvTransactions = new tbl_InvTransaction().Select(tbl_InvTransactionPre);
                        if (deltbl_InvTransactions != null && deltbl_InvTransactions.Count > 0)
                        {
                            foreach (tbl_InvTransaction tbl_InvTransaction in deltbl_InvTransactions)
                            {
                                ret.Add(tbl_InvTransaction.Delete());
                            }

                        }
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

        public List<tbl_POMaster> GetAllPOMaster()
        {
            return (new tbl_POMaster()).Select(_docTypepredicate);
        }

        public tbl_POMaster GetPOMaster(string docNo)
        {
            if (!string.IsNullOrEmpty(docNo))
            {
                Func<tbl_POMaster, bool> poPredicate = (x => x.DocNo == docNo);

                var _po = new tbl_POMaster().Select(poPredicate);
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

        public tbl_PRMaster GetPRMaster(string docNo)
        {
            if (!string.IsNullOrEmpty(docNo))
            {
                Func<tbl_PRMaster, bool> prPredicate = (x => x.DocNo == docNo);

                var _pr = new tbl_PRMaster().Select(prPredicate);
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

        public List<tbl_POMaster> GetPOMaster(Func<tbl_POMaster, bool> tbl_POMasterPre)
        {
            return new tbl_POMaster().Select(tbl_POMasterPre);
        }

        public List<tbl_PODetail> GetPODetails(Func<tbl_PODetail, bool> predicate)
        {
            return new tbl_PODetail().Select(predicate);
        }

        public List<tbl_PODetail> GetPODetails(string docNo)
        {
            if (!string.IsNullOrEmpty(docNo))
            {
                Func<tbl_PODetail, bool> pdtPredicate = (x => x.DocNo == docNo);
                return new tbl_PODetail().Select(pdtPredicate);
            }
            else
                return null;
        }

        public List<tbl_PRDetail> GetPRDetails(string docNo)
        {
            if (!string.IsNullOrEmpty(docNo))
            {
                Func<tbl_PRDetail, bool> prdtPredicate = (x => x.DocNo == docNo);
                return new tbl_PRDetail().Select(prdtPredicate);
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
            Func<tbl_POMaster, bool> poPredicate = (x => x.DocNo == docNo);
            return new tbl_POMaster().Select(poPredicate).Count > 0;
        }

        public bool CheckExistsPR(string docNo)
        {
            Func<tbl_PRMaster, bool> prPredicate = (x => x.DocNo == docNo);
            return new tbl_PRMaster().Select(prPredicate).Count > 0;
        }

        public string GenDocNo(string docTypeCode, string whCode = null)
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
                var allData = new tbl_Employee().SelectAll();
                tbl_Employee = allData.FirstOrDefault(x => x.EmpID == empID);

                return tbl_Employee;
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

                tbl_Employee = GetEmployee(user.EmpID);

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

        public List<tbl_Product> GetProduct()
        {
            return new tbl_Product().SelectAll();
        }

        public List<tbl_Product> GetProduct(Func<tbl_Product, bool> predicate)
        {
            return new tbl_Product().Select(predicate);
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
            Func<tbl_InvWarehouse, bool> tbl_InvWarehousePre = (x => x.ProductID == productID && x.WHID == whID);
            return new tbl_InvWarehouse().Select(tbl_InvWarehousePre);
        }

        public List<tbl_InvMovement> GetInvMovement(string docNo)
        {
            Func<tbl_InvMovement, bool> tbl_InvMovementPre = (x => x.RefDocNo == docNo);
            return new tbl_InvMovement().Select(tbl_InvMovementPre);
        }

        public List<tbl_InvTransaction> GetInvTransaction(string docNo)
        {
            Func<tbl_InvTransaction, bool> tbl_InvTransactionPre = (x => x.RefDocNo == docNo);
            return new tbl_InvTransaction().Select(tbl_InvTransactionPre);
        }

        public List<tbl_ProductUom> GetUOM(Func<tbl_ProductUom, bool> tbl_ProductUomPre)
        {
            return new tbl_ProductUom().Select(tbl_ProductUomPre);
        }

        public List<tbl_ProductUomSet> GetUOMSet(Func<tbl_ProductUomSet, bool> tbl_ProductUomSetPre)
        {
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
                return new tbl_BranchWarehouse().Select(predicate)[0];
            }
            else
                return null;
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

        public List<tbl_SalArea> GetSaleArea(Func<tbl_SalArea, bool> condition = null)
        {
            if (condition != null)
                return new tbl_SalArea().Select(condition);
            else
                return new tbl_SalArea().SelectAll();
        }

        public List<tbl_SalAreaDistrict> GetSaleAreaDistrict(Func<tbl_SalAreaDistrict, bool> condition = null)
        {
            if (condition != null)
                return new tbl_SalAreaDistrict().Select(condition);
            else
                return new tbl_SalAreaDistrict().SelectAll();
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

    }
}
