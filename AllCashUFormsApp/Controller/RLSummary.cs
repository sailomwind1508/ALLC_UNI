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
    public class RLSummary : BaseControl
    {
        private Func<tbl_PRMaster, bool> _rlDocTypePredicate = null;

        public virtual Func<tbl_PRMaster, bool> rlDocTypePredicate
        {
            get { return _rlDocTypePredicate; }
            set
            {
                _rlDocTypePredicate = value;
            }
        }

        public RLSummary() : base("RL")
        {
            _rlDocTypePredicate = (x => x.DocTypeCode.Trim() == "RL");
        }

        public int RemoveRLPRDetail(tbl_PRDetail tbl_PRDetail)
        {
            return tbl_PRDetail.Delete();
        }

        public int UpdateRLPRDetail(tbl_PRDetail tbl_PRDetail)
        {
            return tbl_PRDetail.Update();
        }

        public DataTable GetRLSummary(string WHID, DateTime DocDate)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("proc_getRLSummary", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add(new SqlParameter("@WHID", WHID));

                    string _docdate = DocDate.ToString("yyyyMMdd", new CultureInfo("en-US"));
                    cmd.Parameters.Add(new SqlParameter("@DocDate", _docdate));

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);

                    sda.Fill(ds, "proc_getRLSummary");
                    dt = ds.Tables[0];
                }

                return dt;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg.ShowErrorMessage();
                return null;
            }
        }

        public DataTable GetUOMSet(string proID)
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += "select top 1 t3.UomSetID as 'UomSetID',t3.BaseQty as 'BaseQty' from tbl_PRDetail t1 ";
                sql += "left join tbl_Product t2 on t2.ProductID = t1.ProductID ";
                sql += "left join tbl_ProductUomSet t3 on t3.ProductID = t2.ProductID and t2.PurchaseUomID = t3.UomSetID ";
                sql += "where t1.ProductID = '" + proID + "'";
                SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg.ShowErrorMessage();
                return null;
            }
        }

        public int UpdatePRMasterData(tbl_PRMaster tbl_PRMaster)
        {
            return tbl_PRMaster.Update();
        }

    }
}
