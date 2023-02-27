using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;


namespace AllCashUFormsApp.Controller
{
    public class EndDay : BaseControl, IObject
    {
        private tbl_SaleBranchSummary _tbl_SaleBranchSummary = null;

        public tbl_SaleBranchSummary tbl_SaleBranchSummary
        {
            get { return _tbl_SaleBranchSummary; }
            set
            {
                _tbl_SaleBranchSummary = value;
            }
        }

        public EndDay() : base("ED")
        {
            this.tbl_IVMaster = new tbl_IVMaster();
            this.tbl_IVDetails = new List<tbl_IVDetail>();
        }

        public bool ValidateSendToHQ(DateTime docDate)
        {
            bool ret = false; //true = no send, false = sent
            try
            {
                DataTable newTable = new DataTable();

                string sql = "proc_EndDay_Verify_SendToHQ";

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@DocDate", docDate.ToString("yyyyMMdd", new CultureInfo("en-US")));

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);
                if (newTable != null && newTable.Rows.Count > 0)
                {
                    if (newTable.Rows[0][0].ToString() == "0") //0 = no send to HQ, 1 = sent to HQ
                        ret = true;
                }

                return ret;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return false;
            }
        }

        public List<tbl_IVDetail> GetIVDetails(DateTime docDate)
        {
            return new tbl_IVDetail().Select(docDate);
        }

        public List<tbl_IVMaster> GetIVMaster(DateTime docDate)
        {
            return new tbl_IVMaster().Select(docDate);
        }

        public List<tbl_IVDetail> GetIVDetails(Func<tbl_IVDetail, bool> con = null)
        {
            if (con == null)
                return new tbl_IVDetail().SelectAll();
            else
                return new tbl_IVDetail().Select(con);
        }

        public List<tbl_IVMaster> GetIVMaster(Func<tbl_IVMaster, bool> con = null)
        {
            if (con == null)
                return new tbl_IVMaster().SelectAll();
            else
                return new tbl_IVMaster().Select(con);
        }

        public int UpdatePOMaster(tbl_POMaster tbl_POMaster)
        {
            return tbl_POMaster.Update();
        }

        public int UpdatePOMasterSQL(string sqlCmd)
        {
            return tbl_POMaster.UpdateSQL(sqlCmd);
        }

        public int UpdatePODetailsSQL(string sqlCmd)
        {
            return (new tbl_PODetail()).UpdateSQL(sqlCmd);

        }
        public int ExecuteSQLCommand(string sqlCmd)
        {
            string msg = "start ExecuteSQLCommand";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                ret = My_DataTable_Extensions.ExecuteSQLScalar(sqlCmd, CommandType.Text);
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(this.GetType());
            }

            msg = "end ExecuteSQLCommand";
            msg.WriteLog(null);

            return ret;
        }

        public int UpdatePOCustInvNO(tbl_POMaster tbl_POMaster)
        {
            return tbl_POMaster.UpdateCustInvNo();
        }

        public int UpdatePRMaster(tbl_PRMaster tbl_PRMaster)
        {
            return tbl_PRMaster.Update();
        }

        public int UpdatePRMasterSQL(string sqlCmd)
        {
            return tbl_PRMaster.UpdateSQL(sqlCmd);
        }

        public int RemovePOIVMaster(tbl_IVMaster tbl_IVMaster)
        {
            List<int> ret = new List<int>();
            try
            {
                if (tbl_IVMaster != null)
                {
                    //edit by sailom 14-06-2021
                    //var _tbl_IVMasters = new tbl_IVMaster().Select(x => x.DocNo == tbl_IVMaster.DocNo);
                    var _tbl_IVMasters = new tbl_IVMaster().SelectExists(tbl_IVMaster.DocNo, "");
                    foreach (var item in _tbl_IVMasters)
                    {
                        ret.Add(item.Delete());
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

        public int RemovePOIVDetails(List<tbl_IVDetail> tbl_IVDetails)
        {
            List<int> ret = new List<int>();
            try
            {
                if (tbl_IVDetails != null && tbl_IVDetails.Count > 0)
                {
                    //edit by sailom 14-06-2021
                    string sqlFilter = "";
                    string docNos = "";
                    int i = 0;
                    foreach (var _docNoItem in tbl_IVDetails.Select(a => a.DocNo).ToList())
                    {
                        if (i == tbl_IVDetails.Count - 1)
                            docNos += "'" + _docNoItem + "' ";
                        else
                            docNos += "'" + _docNoItem + "', ";

                        i++;
                    }

                    sqlFilter = " AND DocNo IN (" + docNos + ")";
                    //var _tbl_IVMasters = new tbl_IVDetail().Select(x => tbl_IVDetails.Select(a => a.DocNo).Contains(x.DocNo));
                    var _tbl_IVMasters = new tbl_IVDetail().Select(sqlFilter);
                    foreach (var item in _tbl_IVMasters)
                    {
                        ret.Add(item.Delete());
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

        public virtual int AddData(tbl_SaleBranchSummary tbl_SaleBranchSummary)
        {
            return tbl_SaleBranchSummary.Insert();
        }

        public int UpdateSaleBranchSummaryData(tbl_SaleBranchSummary tbl_SaleBranchSummary)
        {
            return tbl_SaleBranchSummary.Update();
        }

        public int RemoveData(tbl_SaleBranchSummary tbl_SaleBranchSummary)
        {
            return tbl_SaleBranchSummary.Delete();
        }

        public bool FixIVDetails(string ivDocNo, string whid, DateTime docdate)
        {
            bool ret = false;

            try
            {
                using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("proc_tbl_IVDetail_EndDay_fix", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add(new SqlParameter("@IVDocNo", ivDocNo));
                    cmd.Parameters.Add(new SqlParameter("@WHID", whid));
                    cmd.Parameters.Add(new SqlParameter("@DocDate", docdate.ToString("yyyyMMdd", new CultureInfo("en-US"))));
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

            return ret;
        }

        public DataTable GetEndDay_PO(DateTime docDate, string docTypeCode, bool isEndDayProcess, bool isSum = true)
        {
            try
            {
                List<tbl_POMaster> tbl_POMasters = new List<tbl_POMaster>();
                List<tbl_POMaster> tbl_POMasterTmps = new List<tbl_POMaster>();

                //edit by sailom 14-06-2021
                //tbl_POMasterTmps = (new tbl_POMaster()).Select(x => x.DocStatus == "4" && x.DocTypeCode.Trim() == docTypeCode.Trim() && x.DocDate.ToShortDateString() == docDate.ToShortDateString());
                string sqlFilter = " DocStatus = 4 AND DocTypeCode = '" + docTypeCode.Trim() + "' AND CAST(DocDate AS DATE) = '" + docDate.ToString("yyyyMMdd", new CultureInfo("en-US")) + "' ";
                tbl_POMasterTmps = (new tbl_POMaster()).Select(sqlFilter);

                //edit by sailom 14-06-2021
                //var tbl_IVMasters = (new tbl_IVMaster()).Select(x => x.DocDate.ToShortDateString() == docDate.ToShortDateString());// && x.Remark == "สร้างใบกำกับจากการปิดวัน");
                var tbl_IVMasters = (new tbl_IVMaster()).Select(docDate);
                if (isEndDayProcess)
                {
                    tbl_IVMasters = tbl_IVMasters.Where(x => string.IsNullOrEmpty(x.CustInvNO)).ToList();
                }

                foreach (var item in tbl_POMasterTmps)
                {
                    bool isPOEndDay = false;
                    if (tbl_IVMasters.Count > 0)
                    {
                        var checkCondition = tbl_IVMasters.Any(x => item.CustInvNO == x.DocNo);
                        if (checkCondition)
                            isPOEndDay = true;
                    }

                    if (string.IsNullOrEmpty(item.CustInvNO))
                        isPOEndDay = true;

                    if (isPOEndDay)
                    {
                        tbl_POMasters.Add(item);
                    }
                }

                List<tbl_Employee> tbl_Employees = new List<tbl_Employee>();
                tbl_Employees = (new tbl_Employee()).Select(x => tbl_POMasters.Select(a => a.SaleEmpID.Trim()).ToList().Contains(x.EmpID.Trim())).ToList();
                //List <tbl_ArCustomer> tbl_ArCustomers = new List<tbl_ArCustomer>();
                //tbl_ArCustomers = (new tbl_ArCustomer()).SelectAll().Where(x => tbl_POMasters.Select(a => a.CustomerID.Trim()).ToList().Contains(x.CustomerID.Trim())).ToList();

                var data = from po in tbl_POMasters
                               //join cust in tbl_ArCustomers
                           join emp in tbl_Employees
                           on po.SaleEmpID.Trim() equals emp.EmpID.Trim()
                           select new EndDayPOInfoModel
                           {
                               DocNo = po.DocNo,
                               WHID = po.WHID,
                               CustomerID = emp.EmpID,//po.CustomerID,
                               CustName = string.IsNullOrEmpty(po.CustInvNO) ? string.Join(" ", emp.TitleName, emp.FirstName, emp.LastName) : tbl_IVMasters.First(x => x.DocNo == po.CustInvNO).CustName, //last edit by sailom 27/01/2022
                               BeforeVat = Convert.ToDecimal(po.IncVat.Value - po.Discount.Value - po.VatAmt.Value - po.ExcVat.Value).ToDecimalN2(), //fix before vat amount value edit by sailom.k 26/04/2022 //Convert.ToDecimal(((po.Amount - po.ExcVat - po.Discount) * 100) / (100 + po.VatRate)).ToDecimalN2(),
                               VatAmt = po.VatAmt.Value.ToDecimalN2(),
                               TotalDue = po.TotalDue.ToDecimalN2(),
                               CustInvNo = (string.IsNullOrEmpty(po.CustInvNO) ? "" : po.CustInvNO) //fix error duplicate row. When create deal by back-end and create deal by tablet with the same whid edit by sailom 28/03/2022
                           };

                DataTable newTable = new DataTable();

                newTable.Columns.Add("DocNo", typeof(string));
                newTable.Columns.Add("VAN", typeof(string));
                newTable.Columns.Add("CustomerID", typeof(string));
                newTable.Columns.Add("พนักงานขาย/ร้านค้า", typeof(string));  //last edit by sailom 27/01/2022 
                newTable.Columns.Add("ก่อนภาษี", typeof(decimal));
                newTable.Columns.Add("ภาษี", typeof(decimal));
                newTable.Columns.Add("รวมภาษี", typeof(decimal));
                newTable.Columns.Add("เลขใบกำกับภาษี", typeof(string));

                var totalData = data.ToList();

                //if (isSum)
                {
                    List<EndDayPOInfoModel> sumEndDayPOInfoModel = new List<EndDayPOInfoModel>();
                    var loop = totalData.Select(x => new { CustInvNo = x.CustInvNo, WHID = x.WHID }).Distinct().ToList();
                    foreach (var r in loop)
                    {
                        var emptyCustInvNo = totalData.Where(x => string.IsNullOrEmpty(x.CustInvNo) && x.CustInvNo == r.CustInvNo && x.WHID == r.WHID).ToList();
                        if (emptyCustInvNo != null && emptyCustInvNo.Count > 0)
                        {
                            var edpm = new EndDayPOInfoModel();
                            edpm.BeforeVat = emptyCustInvNo.Sum(x => x.BeforeVat);
                            edpm.VatAmt = emptyCustInvNo.Sum(x => x.VatAmt);
                            edpm.TotalDue = emptyCustInvNo.Sum(x => x.TotalDue);
                            edpm.CustomerID = emptyCustInvNo[0].CustomerID;
                            edpm.CustName = emptyCustInvNo[0].CustName;
                            edpm.WHID = emptyCustInvNo[0].WHID;
                            edpm.CustInvNo = emptyCustInvNo[0].CustInvNo;
                            edpm.DocNo = string.Join("|", emptyCustInvNo.Select(a => a.DocNo).ToList());
                            sumEndDayPOInfoModel.Add(edpm);
                        }

                        var docRefItem = totalData.Where(x => !string.IsNullOrEmpty(x.CustInvNo) && x.CustInvNo == r.CustInvNo && x.WHID == r.WHID).ToList();
                        if (docRefItem != null && docRefItem.Count > 0)
                        {
                            var edpm = new EndDayPOInfoModel();
                            edpm.BeforeVat = docRefItem.Sum(x => x.BeforeVat);
                            edpm.VatAmt = docRefItem.Sum(x => x.VatAmt);
                            edpm.TotalDue = docRefItem.Sum(x => x.TotalDue);
                            edpm.CustomerID = docRefItem[0].CustomerID;
                            edpm.CustName = docRefItem[0].CustName;
                            edpm.WHID = docRefItem[0].WHID;
                            edpm.CustInvNo = docRefItem[0].CustInvNo;
                            edpm.DocNo = string.Join("|", docRefItem.Select(a => a.DocNo).ToList());
                            sumEndDayPOInfoModel.Add(edpm);
                        }
                    }



                    //foreach (var r in totalData)
                    //{
                    //    var emptyCustInvNo = sumEndDayPOInfoModel.FirstOrDefault(x => string.IsNullOrEmpty(x.CustInvNo) && x.WHID == r.WHID);
                    //    if (sumEndDayPOInfoModel.Count > 0 && emptyCustInvNo != null)
                    //    {
                    //        emptyCustInvNo.BeforeVat += r.BeforeVat;
                    //        emptyCustInvNo.VatAmt += r.VatAmt;
                    //        emptyCustInvNo.TotalDue += r.TotalDue;

                    //        emptyCustInvNo.CustomerID = r.CustomerID;
                    //        emptyCustInvNo.CustName = r.CustName;
                    //    }

                    //    var docRefItem = sumEndDayPOInfoModel.FirstOrDefault(x => !string.IsNullOrEmpty(x.CustInvNo) && x.WHID == r.WHID);
                    //    if (sumEndDayPOInfoModel.Count > 0 && docRefItem != null)
                    //    {
                    //        docRefItem.BeforeVat += r.BeforeVat;
                    //        docRefItem.VatAmt += r.VatAmt;
                    //        docRefItem.TotalDue += r.TotalDue;

                    //        docRefItem.CustomerID = r.CustomerID;
                    //        docRefItem.CustName = r.CustName;
                    //    }

                    //    //else
                    //    //    sumEndDayPOInfoModel.Add(r);
                    //}
                    foreach (var r in sumEndDayPOInfoModel.OrderBy(x => x.WHID).ThenBy(a => a.DocNo).ToList())
                    {
                        newTable.Rows.Add(r.DocNo, r.WHID, r.CustomerID, r.CustName, r.BeforeVat, r.VatAmt, r.TotalDue, r.CustInvNo);
                    }
                }
                //else
                //{
                //    foreach (var r in totalData)
                //    {
                //        newTable.Rows.Add(r.DocNo, r.WHID, r.CustomerID, r.CustName, r.BeforeVat, r.VatAmt, r.TotalDue, r.CustInvNo);
                //    }
                //}

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable GetEndDay_PODetails(DateTime docDate, string docTypeCode)
        {
            try
            {
                DataTable newTable = new DataTable();

                string sql = "proc_EndDay_GetDataTable";

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@DocTypeCode", docTypeCode);
                sqlParmas.Add("@DocDate", docDate.ToString("yyyyMMdd", new CultureInfo("en-US")));


                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);

                //List<tbl_POMaster> tbl_POMasters = new List<tbl_POMaster>();
                //tbl_POMasters = (new tbl_POMaster()).Select(x => x.DocStatus == "4" && x.DocTypeCode.Trim() == docTypeCode.Trim() && x.DocDate.ToShortDateString() == docDate.ToShortDateString());

                //List<tbl_PODetail> tbl_PODetails = new List<tbl_PODetail>();
                //tbl_PODetails = (new tbl_PODetail()).Select(x => tbl_POMasters.Select(a => a.DocNo).ToList().Contains(x.DocNo));

                //List<tbl_Branch> tbl_Branchs = new List<tbl_Branch>();
                //tbl_Branchs = (new tbl_Branch()).Select(x => tbl_POMasters.Select(a => a.WHID.Substring(0, 3)).ToList().Contains(x.BranchID));

                //List<tbl_ProductUomSet> tbl_ProductUomSets = new List<tbl_ProductUomSet>();
                //tbl_ProductUomSets = (new tbl_ProductUomSet()).Select(x => x.BaseUomID == 2 && x.UomSetID == 1 && tbl_PODetails.Select(a => a.ProductID).ToList().Contains(x.ProductID));

                //var data = from po in tbl_POMasters
                //           join pdt in tbl_PODetails on po.DocNo equals pdt.DocNo
                //           join uom in tbl_ProductUomSets on pdt.ProductID equals uom.ProductID
                //           join b in tbl_Branchs on po.WHID.Substring(0, 3) equals b.BranchID
                //           select new
                //           {
                //               WHID = po.WHID,
                //               DocRef = po.DocRef,
                //               ProductID = pdt.ProductID,
                //               FactoryCode = b.FactoryCode,
                //               FactoryLocation = b.FactoryLocation,
                //               BranchRefCode = b.BranchRefCode,
                //               SAPPlantID = b.SAPPlantID,
                //               ReceivedQty = pdt.OrderUom == 1 ? pdt.ReceivedQty * uom.BaseQty : pdt.ReceivedQty,
                //               DocNo = po.DocNo,
                //               DocDate = po.DocDate,
                //               CrDate = pdt.CrDate
                //           };

                //DataTable newTable = new DataTable();

                //newTable.Columns.Add("VAN", typeof(string));
                //newTable.Columns.Add("เลขที่โอน", typeof(string));
                //newTable.Columns.Add("รหัสสินค้า", typeof(string));
                //newTable.Columns.Add("จากคลัง", typeof(string));
                //newTable.Columns.Add("จากสถานที่", typeof(string));
                //newTable.Columns.Add("ถึงคลัง", typeof(string));
                //newTable.Columns.Add("ถึงสถานที่", typeof(string));
                //newTable.Columns.Add("จำนวนที่โอน", typeof(decimal));
                //newTable.Columns.Add("เลขที่อ้างอิง", typeof(string));
                //newTable.Columns.Add("วันที่โอน", typeof(string));
                //newTable.Columns.Add("วันที่บันทึก", typeof(string));

                //foreach (var r in data)
                //{
                //    newTable.Rows.Add(r.WHID, r.DocRef, r.ProductID, r.FactoryCode, r.FactoryLocation, r.BranchRefCode, r.SAPPlantID, r.ReceivedQty, r.DocNo, r.DocDate.ToDateTimeFormatString(), r.CrDate.ToDateTimeFormatString());
                //}

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable GetEndDay_PRDetails(DateTime docDate, string docTypeCode)
        {
            try
            {
                DataTable newTable = new DataTable();

                string sql = "proc_EndDay_GetDataTable";

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@DocTypeCode", docTypeCode);
                sqlParmas.Add("@DocDate", docDate.ToString("yyyyMMdd", new CultureInfo("en-US")));


                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);

                //List<tbl_PRMaster> tbl_PRMasters = new List<tbl_PRMaster>();
                //tbl_PRMasters = (new tbl_PRMaster()).Select(x => x.DocStatus == "4" && x.DocTypeCode.Trim() == docTypeCode.Trim() && x.DocDate.ToShortDateString() == docDate.ToShortDateString());

                //List<tbl_PRDetail> tbl_PRDetails = new List<tbl_PRDetail>();
                //tbl_PRDetails = (new tbl_PRDetail()).Select(x => tbl_PRMasters.Select(a => a.DocNo).ToList().Contains(x.DocNo));

                //List<tbl_Branch> tbl_Branchs = new List<tbl_Branch>();
                //tbl_Branchs = (new tbl_Branch()).Select(x => tbl_PRMasters.Select(a => a.FromBranchID).ToList().Contains(x.BranchID));

                //List<tbl_ProductUomSet> tbl_ProductUomSets = new List<tbl_ProductUomSet>();
                //tbl_ProductUomSets = (new tbl_ProductUomSet()).Select(x => x.BaseUomID == 2 && x.UomSetID == 1 && tbl_PRDetails.Select(a => a.ProductID).ToList().Contains(x.ProductID));

                //var data = from po in tbl_PRMasters
                //           join pdt in tbl_PRDetails on po.DocNo equals pdt.DocNo
                //           join uom in tbl_ProductUomSets on pdt.ProductID equals uom.ProductID
                //           join b in tbl_Branchs on po.FromBranchID equals b.BranchID
                //           select new
                //           {
                //               WHID = po.FromBranchID,
                //               DocRef = po.DocRef,
                //               ProductID = pdt.ProductID,
                //               FactoryCode = b.FactoryCode,
                //               FactoryLocation = b.FactoryLocation,
                //               BranchRefCode = b.BranchRefCode,
                //               SAPPlantID = b.SAPPlantID,
                //               ReceivedQty = pdt.OrderUom == 1 ? pdt.SendQty * uom.BaseQty : pdt.SendQty,
                //               DocNo = po.DocNo,
                //               DocDate = po.DocDate,
                //               CrDate = pdt.CrDate
                //           };

                //DataTable newTable = new DataTable();

                //newTable.Columns.Add("VAN", typeof(string));
                //newTable.Columns.Add("เลขที่โอน", typeof(string));
                //newTable.Columns.Add("รหัสสินค้า", typeof(string));
                //newTable.Columns.Add("จากคลัง", typeof(string));
                //newTable.Columns.Add("จากสถานที่", typeof(string));
                //newTable.Columns.Add("ถึงคลัง", typeof(string));
                //newTable.Columns.Add("ถึงสถานที่", typeof(string));
                //newTable.Columns.Add("จำนวนที่โอน", typeof(decimal));
                //newTable.Columns.Add("เลขที่อ้างอิง", typeof(string));
                //newTable.Columns.Add("วันที่โอน", typeof(string));
                //newTable.Columns.Add("วันที่บันทึก", typeof(string));

                //foreach (var r in data)
                //{
                //    newTable.Rows.Add(r.WHID, r.DocRef, r.ProductID, r.FactoryCode, r.FactoryLocation, r.BranchRefCode, r.SAPPlantID, r.ReceivedQty, r.DocNo, r.DocDate.ToDateTimeFormatString(), r.CrDate.ToDateTimeFormatString());
                //}

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public virtual DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                List<tbl_POMaster> tbl_POMaster = new List<tbl_POMaster>();
                tbl_POMaster = (new tbl_POMaster()).Select(docTypepredicate);

                var docStatus = GetDocStatus();

                DataTable newTable = new DataTable();

                return newTable;
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

            if (filters != null)
            {

            }
            else
            {
                dt = GetDataTable();
            }

            return dt;
        }

        public DataTable VerifyFlagBill(string docDate)
        {
            return new tbl_ArCustomer().VerifyFlagBill(docDate);
        }

        public List<tbl_POMaster> SelectCustomer_POMaster(string CustomerID)
        {
            return new tbl_POMaster().SelectCustomer_POMaster(CustomerID);
        }

    }
}
