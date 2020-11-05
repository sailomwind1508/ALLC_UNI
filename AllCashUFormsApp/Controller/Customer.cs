using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace AllCashUFormsApp.Controller
{
    public class Customer : IObject
    {
        public List<tbl_ArCustomer> GetAllData()
        {
            return (new tbl_ArCustomer()).SelectAll();
        }

        public virtual int AddData(tbl_ArCustomer tbl_ArCustomer)
        {
            return tbl_ArCustomer.Insert();
        }

        public int UpdateData(tbl_ArCustomer tbl_ArCustomer)
        {
            return tbl_ArCustomer.Update();
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
                Func<tbl_ArCustomer, bool> func = (x => x.FlagDel == false);
                tbl_ArCustomers = (new tbl_ArCustomer()).Select(func);

                List<tbl_ShopType> tbl_ShopTypes = new List<tbl_ShopType>();
                tbl_ShopTypes = (new tbl_ShopType()).SelectAll();

                List<tbl_SalArea> tbl_SalAreas = new List<tbl_SalArea>();
                tbl_SalAreas = (new tbl_SalArea()).SelectAll();

                var query = from c in tbl_ArCustomers
                            join sht in tbl_ShopTypes on c.ShopTypeID equals sht.ShopTypeID
                            join sa in tbl_SalAreas on c.SalAreaID equals sa.SalAreaID
                            select new
                            {
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
                    newTable.Rows.Add(rowInfo.CustomerCode, rowInfo.CustName, rowInfo.CustomerRefCode, rowInfo.ShopTypeName, rowInfo.SalAreaName, 
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
    }
}
