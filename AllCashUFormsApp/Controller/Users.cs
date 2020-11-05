using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace AllCashUFormsApp.Controller
{
    class Users : IObject
    {
        public List<tbl_Users> GetAllData()
        {
            return (new tbl_Users()).SelectAll();
        }

        public virtual int AddData(tbl_Users tbl_Users)
        {
            return tbl_Users.Insert();
        }

        public int UpdateData(tbl_Users tbl_Users)
        {
            return tbl_Users.Update();
        }

        public int RemoveData(tbl_Users tbl_Users)
        {
            return tbl_Users.Delete();
        }

        public virtual DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                var query = (new tbl_Users()).SelectAll();

                //DataTable newTable = new DataTable();
                //newTable.Columns.Add("EmpID", typeof(string));
                //newTable.Columns.Add("EmpCode", typeof(string));
                //newTable.Columns.Add("EmpName", typeof(string));

                //foreach (var r in query)
                //{
                //    newTable.Rows.Add(r.EmpID, r.EmpCode, string.Join(" ", r.TitleName, r.FirstName));
                //}

                return query.ToDataTable();
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable GetDataTable(Func<tbl_Users, bool> predicate)
        {
            try
            {
                var query = (new tbl_Users()).Select(predicate);

                //DataTable newTable = new DataTable();
                //newTable.Columns.Add("EmpID", typeof(string));
                //newTable.Columns.Add("EmpCode", typeof(string));
                //newTable.Columns.Add("EmpName", typeof(string));

                //foreach (var r in query)
                //{
                //    newTable.Rows.Add(r.EmpID, r.EmpCode, string.Join(" ", r.TitleName, r.FirstName));
                //}

                return query.ToDataTable(); ;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable GetDataTableByCondition(Func<tbl_Users, bool> predicate)
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
            List<tbl_Users> tbl_Userss = new List<tbl_Users>();

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
