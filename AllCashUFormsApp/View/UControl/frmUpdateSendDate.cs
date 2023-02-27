using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Model;
using AllCashUFormsApp.Controller;
using System.Globalization;

namespace AllCashUFormsApp.View.UControl
{
    public partial class frmUpdateSendDate : Form
    {
        public static string whcode;
        public static DateTime dateSend;
        SendData bu = new SendData();
        BankNote buBankNote = new BankNote();
        public static bool confirmUpdate = false;
        public static string UserName = "";
        public frmUpdateSendDate()
        {
            InitializeComponent();
        }

        private void frmUpdateSendDate_Load(object sender, EventArgs e)
        {
            try
            {
                dtpDateSend.SetDateTimePickerFormat();
                dtpUpdateDate.SetDateTimePickerFormat();
                txtWHCode.Text = whcode;
                txtWHCode.DisableTextBox(true);

                dtpUpdateDate.Value = DateTime.Now;
                dtpDateSend.Value = dateSend;
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string _oldDocDate = dtpDateSend.Value.ToString("yyyyMMdd", new CultureInfo("en-US"));
                string _newDocDate = dtpUpdateDate.Value.ToString("yyyyMMdd", new CultureInfo("en-US"));

                string msg = "";

                var _dateSend = new DateTime(dtpDateSend.Value.Year, dtpDateSend.Value.Month, dtpDateSend.Value.Day).Ticks;
                var _updateDate = new DateTime(dtpUpdateDate.Value.Year, dtpUpdateDate.Value.Month, dtpUpdateDate.Value.Day).Ticks;

                if (_updateDate == _dateSend)
                {
                    msg = "วันที่ไม่มีการเปลี่ยนแปลง !!";
                    msg.ShowWarningMessage();
                    return;
                }

                var result = bu.ValidatePOMaster(_newDocDate, txtWHCode.Text);
                if (result == 1)
                    msg = "วันที่เตรียมข้อมูลใหม่ ไม่มีการเปลี่ยนแปลง !!";

                if (!string.IsNullOrEmpty(msg))
                {
                    msg.ShowWarningMessage();
                    return;
                }

                confirmUpdate = false;
                frmConfirmUpdateTabletData frm = new frmConfirmUpdateTabletData();
                frm.ShowDialog();

                if (confirmUpdate == false)
                    return;

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                Cursor.Current = Cursors.WaitCursor;

                var SendData = bu.GetSendDataSingle(_oldDocDate, txtWHCode.Text);

                var listPOMaster = buBankNote.GetPOMaster_ByWHID(_oldDocDate, txtWHCode.Text);

                var updateDate = new DateTime(dtpUpdateDate.Value.Year, dtpUpdateDate.Value.Month, dtpUpdateDate.Value.Day);

                var _edDate = DateTime.Now;
                var _edUser = UserName;
                var updateSendData = new tbl_SendData();
                updateSendData = SendData[0];
                updateSendData.DateSend = updateDate;

                var poMaster = new List<tbl_POMaster>();

                var listWHCode = txtWHCode.Text.Split('V').ToList();
                string _WHCode = listWHCode[0] + listWHCode[1];
                
                string dateFormat = dtpUpdateDate.Value.ToString("yyMMdd", new CultureInfo("th-TH"));
                string docFormat = _WHCode + dateFormat;

                for (int i = 0; i < listPOMaster.Count; i++)
                {
                    var data = new tbl_POMaster();
                    data = listPOMaster[i];
                    data.Remark = data.DocNo;
                    data.EdDate = _edDate;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
                    data.EdUser = _edUser;
                    data.DocDate = updateDate;

                    if (poMaster.Count > 0)
                    {
                        var maxDocNo = poMaster.OrderByDescending(x=>x.DocNo).First();
                        var subDocNo = maxDocNo.DocNo.Substring(11, 3);
                        int running = Convert.ToInt32(subDocNo) + 1;
                        data.DocNo = docFormat + Convert.ToString(running);
                    }
                    else
                    {
                        data.DocNo = docFormat + "101";
                    }

                    poMaster.Add(data);
                }

                List<int> ret = new List<int>();

                ret.Add(buBankNote.UpdateDatePOMaster(poMaster));

                ret.Add(bu.UpdateSendData(_oldDocDate, _newDocDate, txtWHCode.Text, _edUser));

                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@DocDate", _newDocDate);
                _params.Add("@WHID", txtWHCode.Text);

                ret.Add(bu.UpdateInvMovementData(_params));

                if (ret.All(x=>x == 1))
                {
                    msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();
                    this.Close();
                }
                else
                {
                    this.ShowProcessErr();
                }

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                ex.Message.ShowErrorMessage();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
