using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace AllCashUFormsApp.Controller
{
    public class Customer : BaseControl, IObject
    {
        private Func<tbl_PRMaster, bool> _docTypePredicate = null;

        public virtual Func<tbl_PRMaster, bool> docTypePredicate
        {
            get { return _docTypePredicate; }
            set
            {
                _docTypePredicate = value;
            }
        }

        public Customer() : base("")
        {
            _docTypePredicate = (x => x.DocTypeCode == "");
        }

        public string GenCustSAPCode(string branch = "")
        {
            string ret = string.Empty;
            try
            {
                var branchs = GetBranch();
                if (branchs != null && branchs.Count > 0)
                {
                    var b = branchs.First();

                    int maxNo = 99;
                    var customerSAPCodes = GetCustomerInfo(x => !string.IsNullOrEmpty(x.CustomerSAPCode));
                    if (customerSAPCodes != null && customerSAPCodes.Count > 0)
                    {
                        var maxCustSAPCode = customerSAPCodes.Max(x => Convert.ToInt32(x.CustomerSAPCode));
                        maxNo = maxCustSAPCode + 1;
                    }
                    else
                        maxNo += 1;

                    string str = "";
                    for (int i = 0; i < b.RunLength; i++)
                    {
                        if (maxNo.ToString().Length < b.RunLength)
                            str += "0";
                    }

                    if (maxNo.ToString().Length < b.RunLength)
                    {

                    }

                    string.Join(b.Prefix1, b.Prefix2, maxNo);
                }
                return ret;
            }
            catch (Exception ex)
            {

                ex.WriteLog(this.GetType());
                return string.Empty;
            }
        }

        public List<tbl_ArCustomer> GetCustomerInfo(Func<tbl_ArCustomer, bool> condition = null)
        {
            if (condition != null)
                return new tbl_ArCustomer().SelectEntity(condition);
            else
                return new tbl_ArCustomer().SelectAllEntity();
        }

        public List<tbl_ArCustomer> GetSelectCustomerID(string CustomerID, int flagDel)
        {
            return new tbl_ArCustomer().SelectCustomerID(CustomerID, flagDel);
        }

        public DataTable GetCustDetails(Func<tbl_ArCustomer, bool> func)
        {
            try
            {
                List<tbl_ArCustomer> tbl_ArCustomers = new List<tbl_ArCustomer>();
                tbl_ArCustomers = new tbl_ArCustomer().SelectEntity(func).ToList();

                List<tbl_ShopType> tbl_ShopTypes = new List<tbl_ShopType>();
                tbl_ShopTypes = new tbl_ShopType().Select(s => tbl_ArCustomers.Select(c => c.ShopTypeID).Contains(s.ShopTypeID));

                var query = from c in tbl_ArCustomers
                            join s in tbl_ShopTypes on c.ShopTypeID equals s.ShopTypeID
                            select new
                            {
                                CustomerID = c.CustomerID,
                                CustName = c.CustName,
                                BillTo = c.BillTo,
                                ShopTypeName = s.ShopTypeName,
                                Seq = c.Seq,
                                WHID = c.WHID,
                                SalAreaID = c.SalAreaID
                            };
                return query.ToDataTable();
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable GetCustTable(Func<tbl_ArCustomer, bool> func)
        {
            DataTable dtcust = GetCustDetails(func);
            DataTable _dt = new DataTable();
            _dt.Columns.Add("เลือก", typeof(bool));
            _dt.Columns.Add("รหัสลูกค้า", typeof(string));
            _dt.Columns.Add("ชื่อลูกค้า", typeof(string));
            _dt.Columns.Add("ที่อยู่", typeof(string));
            _dt.Columns.Add("ประเภทร้านค้า", typeof(string));
            _dt.Columns.Add("Seq", typeof(string));
            for (int i = 0; i < dtcust.Rows.Count; i++)
            {
                _dt.Rows.Add(false, dtcust.Rows[i]["CustomerID"], dtcust.Rows[i]["CustName"], dtcust.Rows[i]["BillTo"], dtcust.Rows[i]["ShopTypeName"], dtcust.Rows[i]["Seq"]);
            }
            return _dt;
        }

        public List<tbl_ArCustomer> GetAllData(Func<tbl_ArCustomer, bool> func = null)
        {
            if (func != null)
            {
                return (new tbl_ArCustomer()).SelectAll().Where(func).OrderBy(x => x.Seq).ToList();
            }
            else
            {
                return (new tbl_ArCustomer()).SelectAll().OrderBy(x => x.WHID).ThenBy(x => x.SalAreaID).ThenBy(x => x.Seq).ToList();
            }
        }

        public virtual int AddData(tbl_ArCustomer tbl_ArCustomer)
        {
            return tbl_ArCustomer.Insert();
        }

        public int UpdateData(tbl_ArCustomer tbl_ArCustomer)
        {
            return tbl_ArCustomer.Update();
        }

        public int UpdateListData(List<tbl_ArCustomer> tbl_ArCustomers)
        {
            return tbl_ArCustomers.Update();
        }

        public int RemoveData(tbl_ArCustomer tbl_ArCustomer)
        {
            return tbl_ArCustomer.Delete();
        }

        public virtual DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                List<tbl_ArCustomer> tbl_ArCustomers = new List<tbl_ArCustomer>();
                //Func<tbl_ArCustomer, bool> func = (x => x.FlagDel == false);
                tbl_ArCustomers = (new tbl_ArCustomer()).SelectAll();

                List<tbl_ShopType> tbl_ShopTypes = new List<tbl_ShopType>();
                tbl_ShopTypes = (new tbl_ShopType()).SelectAll();

                List<tbl_SalArea> tbl_SalAreas = new List<tbl_SalArea>();
                tbl_SalAreas = (new tbl_SalArea()).SelectAll();

                var query = from c in tbl_ArCustomers
                            join sht in tbl_ShopTypes on c.ShopTypeID equals sht.ShopTypeID
                            join sa in tbl_SalAreas on c.SalAreaID equals sa.SalAreaID
                            select new
                            {
                                CustomerID = c.CustomerID,
                                CustomerCode = c.CustomerCode,
                                CustName = c.CustName,
                                CustomerRefCode = c.CustomerRefCode,
                                ShopTypeName = sht.ShopTypeName,
                                SalAreaName = sa.SalAreaName,
                                SalAreaID = sa.SalAreaID,
                                WHID = c.WHID,
                                Seq = c.Seq,
                                FlagMember = c.FlagMember,
                                CreditDay = c.CreditDay,
                                BillTo = c.BillTo,
                                Contact = c.Contact,
                                Telephone = c.Telephone
                            };

                DataTable newTable = query.ToList().ToDataTable();
                newTable.Clear();

                foreach (var rowInfo in query.OrderBy(x => x.WHID).ThenBy(x => x.SalAreaID).ThenBy(x => x.Seq).ToList())
                {
                    newTable.Rows.Add(rowInfo.CustomerID, rowInfo.CustomerCode, rowInfo.CustName, rowInfo.CustomerRefCode, rowInfo.ShopTypeName, rowInfo.SalAreaName,
                        rowInfo.SalAreaID, rowInfo.WHID, rowInfo.Seq, rowInfo.FlagMember, rowInfo.CreditDay, rowInfo.BillTo, rowInfo.Contact);
                }

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable GetCustomerPre()
        {
            return new tbl_ArCustomer().GetCustomerPre();
        }

        public DataTable GetDataTable(Func<tbl_ArCustomer, bool> predicate)
        {
            try
            {
                List<tbl_ArCustomer> tbl_ArCustomers = new List<tbl_ArCustomer>();
                tbl_ArCustomers = (new tbl_ArCustomer()).SelectAll().Where(predicate).ToList();

                List<tbl_ShopType> tbl_ShopTypes = new List<tbl_ShopType>();
                tbl_ShopTypes = (new tbl_ShopType()).SelectAll();

                List<tbl_SalArea> tbl_SalAreas = new List<tbl_SalArea>();
                tbl_SalAreas = (new tbl_SalArea()).SelectAll();

                var query = from c in tbl_ArCustomers
                            join sht in tbl_ShopTypes on c.ShopTypeID equals sht.ShopTypeID
                            join sa in tbl_SalAreas on c.SalAreaID equals sa.SalAreaID
                            select new
                            {
                                CustomerID = c.CustomerID,
                                CustomerCode = c.CustomerCode,
                                CustName = c.CustName,
                                CustomerRefCode = c.CustomerRefCode,
                                ShopTypeName = sht.ShopTypeName,
                                SalAreaName = sa.SalAreaName,
                                SalAreaID = sa.SalAreaID,
                                WHID = c.WHID,
                                Seq = c.Seq,
                                FlagMember = c.FlagMember,
                                CreditDay = c.CreditDay,
                                BillTo = c.BillTo,
                                Contact = c.Contact,
                                Telephone = c.Telephone
                            };

                DataTable newTable = query.ToList().ToDataTable();
                newTable.Clear();

                foreach (var rowInfo in query.OrderBy(x => x.WHID).ThenBy(x => x.SalAreaID).ThenBy(x => x.Seq).ToList())
                {
                    newTable.Rows.Add(rowInfo.CustomerID, rowInfo.CustomerCode, rowInfo.CustName, rowInfo.CustomerRefCode, rowInfo.ShopTypeName, rowInfo.SalAreaName,
                        rowInfo.SalAreaID, rowInfo.WHID, rowInfo.Seq, rowInfo.FlagMember, rowInfo.CreditDay, rowInfo.BillTo, rowInfo.Contact);
                }

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable GetDataTableByCondition(Func<tbl_ArCustomer, bool> predicate)
        {
            DataTable dt = new DataTable();

            if (predicate != null)
            {
                dt = GetDataTable(predicate);
            }
            else
            {
                dt = GetDataTable();
            }

            return dt;
        }

        public virtual DataTable GetDataTableByCondition(string[] filters)
        {
            DataTable dt = new DataTable();
            List<tbl_ArCustomer> tbl_ArCustomers = new List<tbl_ArCustomer>();

            if (filters != null)
            {

            }
            else
            {
                dt = GetDataTable();
            }

            return dt;
        }

        public DataTable GetCustomerData(Dictionary<string, object> _params)
        {
            return (new tbl_ArCustomer()).GetCustomerData(_params);
        }

        public DataTable GetCustomerImage(Dictionary<string, object> _params)
        {
            return (new tbl_ArCustomer()).GetCustomerImage(_params);
        }

        public DataTable GetTransferCustomerData(Dictionary<string, object> _params)
        {
            return (new tbl_ArCustomer()).GetTransferCustomerData(_params);
        }

        public List<tbl_ArCustomer> SelectCustomerList(string CustomerID)
        {
            return (new tbl_ArCustomer()).SelectSingle(CustomerID);
        }

        public List<tbl_ArCustomer> SelectMaxCustomerID(string FormatCustomerID)
        {
            return new tbl_ArCustomer().SelectMaxCustomerID(FormatCustomerID);
        }

        public List<tbl_ArCustomer> GetCustomerByWHID(string WHID)
        {
            return (new tbl_ArCustomer()).GetCustomerByWHID(WHID);
        }

        public DataTable GetCustomerByWHID_DataTable(string WHID)
        {
            return (new tbl_ArCustomer()).GetCustomerByWHID_DataTable(WHID);
        }

        public DataTable GetCustomerByWHID_DataTable(string WHID, string SalAreaID)
        {
            return (new tbl_ArCustomer()).GetCustomerByWHID_DataTable(WHID, SalAreaID);
        }

        public DataTable GetCountCustomer()
        {
            return (new tbl_ArCustomer()).GetCountCustomer();
        }

        public DataTable GetServerImagePath(string BranchID)
        {
            return (new tbl_ArCustomer()).GetServerImagePath(BranchID);
        }

        public bool ManualUpdateCustomerImage()
        {
            return new tbl_ArCustomer().ManualUpdateCustomerImage();
        }
    }
}
