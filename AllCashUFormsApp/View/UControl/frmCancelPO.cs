using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using AllCashUFormsApp.View.Page;

namespace AllCashUFormsApp.View.UControl
{
    public partial class frmCancelPO : Form
    {
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();
        private string formName = "";
        private string formText = "";
        PreOrder bu = new PreOrder();
        string _docno;
        string _user;
        DateTime _docDate = new DateTime();
        bool isComplete = false;

        public frmCancelPO()
        {
            InitializeComponent();
        }

        private void frmCancelPO_Load(object sender, EventArgs e)
        {
            this.Text = formText;

            btnSubmit.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void PreparePopupForm(string type, string frmName, string popUPText, string docno, DateTime docdate, string user)//DateTime docdate, string whid)
        {
            formName = frmName;
            formText = popUPText;
            _docno = docno;
            _docDate = docdate;
            _user = user;
        }

        public bool GetIsComplete()
        {
            return isComplete;
        }

        private void ConfirmCancelPO(string reason)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                if (!string.IsNullOrEmpty(_docno))
                {
                    bool result = false;
                    var ret = bu.UpdateRL(_docno, _docDate, _user, reason);
                    if (ret != null && ret.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(ret.Rows[0]["Result"].ToString()))
                            result = true;
                    }

                    Cursor.Current = Cursors.Default;
                    if (result)
                        isComplete = true;
                    else
                        isComplete = false;

                    this.Close();
                }
            }
            catch (Exception ex)
            {

                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ConfirmCancelPO(btnCancel.Text);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            ConfirmCancelPO(btnSubmit.Text);
        }
    }
}
