using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.View.Page;
using AllCashUFormsApp.Model;
using AllCashUFormsApp.Controller;
using System.Globalization;

namespace AllCashUFormsApp.View.UControl
{
    public partial class frmExcel : Form
    {
        MasterDataControl bu = new MasterDataControl();
        public frmExcel()
        {
            InitializeComponent();
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Title = "Open Excel";
                openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    txtPathExcel.Text = openFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                return;
            }
        }
        private void btnUpLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = "";

                if (string.IsNullOrEmpty(txtPathExcel.Text))
                {
                    msg = "เลือกไฟล์ Excel ก่อนกดปุ่ม UpLoad !!";
                    msg.ShowWarningMessage();
                    return;
                }

                List<string> SheetName = new List<string>();
                SheetName.Add("[tbl_HQ_Promotion$]");
                SheetName.Add("[tbl_HQ_Promotion_Master$]");
                SheetName.Add("[tbl_HQ_Reward$]");
                SheetName.Add("[tbl_HQ_SKUGroup$]");
                SheetName.Add("[tbl_HQ_SKUGroup_EXC$]");

                List<DataTable> newTable = new List<DataTable>();
                newTable = newTable.ReadExxel(txtPathExcel.Text, SheetName);//new

                grdHQPromotion.DataSource = newTable[0];
                grdHQPromotionMaster.DataSource = newTable[1];
                grdHQReward.DataSource = newTable[2];
                grdHQSKUGroup.DataSource = newTable[3];
                grdHQSKUGroupExc.DataSource = newTable[4];
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                return;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void grdHQPromotion_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdHQPromotion.SetRowPostPaint(sender, e, Font);
        }
        private void grdHQPromotionMaster_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdHQPromotionMaster.SetRowPostPaint(sender, e, Font);
        }
        private void grdHQReward_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdHQReward.SetRowPostPaint(sender, e, Font);
        }
        private void grdHQSKUGroup_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdHQSKUGroup.SetRowPostPaint(sender, e, Font);
        }
        private void grdHQSKUGroupExc_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdHQSKUGroupExc.SetRowPostPaint(sender, e, Font);
        }
        private void Save()
        {
            string cfMsg = "ต้องการส่งข้อมูลใช่หรือไม่?";
            string title = "ยืนยันการส่ง!!";

            if (!cfMsg.ConfirmMessageBox(title))
                return;
            int Promotion = grdHQPromotion.Rows.Count;
            int PromotionMaster = grdHQPromotionMaster.Rows.Count;
            int Reward = grdHQReward.Rows.Count;
            int SKUGroup = grdHQSKUGroup.Rows.Count;
            int SKUGroupEXC = grdHQSKUGroupExc.Rows.Count;

            if (Promotion == 0 && PromotionMaster == 0 && Reward == 0 && SKUGroup == 0 && SKUGroupEXC == 0)
            {
                string msg = "ไม่พบข้อมูลที่จะ IMPORT กรุณาอัพโหลดข้อมูลก่อน IMPORT !!";
                msg.ShowWarningMessage();
                return;
                
            }
            else
            {
                string msg = "";

                int retHQPromotion = 0;
                int retHQPromotion_Master = 0;
                int retHQReward = 0;
                int retHQSKUGroup = 0;
                int retHQSKUGroup_EXC = 0;

                retHQPromotion = Save_HQPromotion();
                retHQPromotion_Master = Save_HQPromotionMaster();
                retHQReward = Save_HQReward();
                retHQSKUGroup = Save_HQSKUGroup();
                retHQSKUGroup_EXC = Save_HQSKUGroup_EXC();

                if (retHQPromotion == 0)
                {
                    msg += "ส่งข้อมูล tbl_HQ_Promotion ไม่สำเร็จ !!\n";
                }

                if (retHQPromotion_Master == 0)
                {
                    msg += "ส่งข้อมูล tbl_HQ_Promotion ไม่สำเร็จ !!\n";
                }

                if (retHQReward == 0)
                {
                    msg += "ส่งข้อมูล tbl_HQ_Reward ไม่สำเร็จ !!\n";
                }

                if (retHQSKUGroup == 0)
                {
                    msg += "ส่งข้อมูล tbl_HQ_SKUGroup ไม่สำเร็จ !!\n";
                }

                if (retHQSKUGroup_EXC == 0)
                {
                    msg += "ส่งข้อมูล tbl_HQ_SKUGroup_EXC ไม่สำเร็จ !!\n";
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    msg.ShowErrorMessage();
                }
                else
                {
                    msg = "ส่งข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();
                    this.Close();
                }
            }

           
        }
        private void PrePareSave_HQPromotion(tbl_HQ_Promotion HQ_Promotion,DataRow r)
        {
            HQ_Promotion.PromotionType = r["PromotionType"].ToString();
            HQ_Promotion.PromotionPattern = r["PromotionPattern"].ToString();
            HQ_Promotion.StepCondition1 = r["StepCondition1"].ToString();

            if (!string.IsNullOrEmpty(r["SKUGroupID1"].ToString()) && r["SKUGroupID1"].ToString() != "NULL")
            {
                HQ_Promotion.SKUGroupID1 = r["SKUGroupID1"].ToString();
            }
            
            HQ_Promotion.ConditionStart = Convert.ToInt32(r["ConditionStart"]);

            if (r["ConditionEnd"].ToString() != "NULL" && !string.IsNullOrEmpty(r["ConditionEnd"].ToString()))
            {
                HQ_Promotion.ConditionEnd = Convert.ToInt32(r["ConditionEnd"]);
            }

            HQ_Promotion.DisCountPattern = r["DisCountPattern"].ToString();

            if (r["DisCountAmt"].ToString() != "NULL" && !string.IsNullOrEmpty(r["DisCountAmt"].ToString()))
            {
                HQ_Promotion.DisCountAmt = Convert.ToDecimal(r["DisCountAmt"]);
            }

            if (r["PruductGroupRewardID"].ToString() != "NULL" && !string.IsNullOrEmpty(r["PruductGroupRewardID"].ToString()))
            {
                HQ_Promotion.PruductGroupRewardID = r["PruductGroupRewardID"].ToString();
            }

            if (r["PruductGroupRewardAmt"].ToString() != "NULL" && !string.IsNullOrEmpty(r["PruductGroupRewardAmt"].ToString()))
            {
                HQ_Promotion.PruductGroupRewardAmt = Convert.ToInt32(r["PruductGroupRewardAmt"]);
            }

            if (r["PruductGroupRewardID2"].ToString() != "NULL" && !string.IsNullOrEmpty(r["PruductGroupRewardID2"].ToString()))
            {
                HQ_Promotion.PruductGroupRewardID2 = r["PruductGroupRewardID2"].ToString();
            }

            if (r["PruductGroupRewardAmt2"].ToString() != "NULL" && !string.IsNullOrEmpty(r["PruductGroupRewardAmt2"].ToString()))
            {
                HQ_Promotion.PruductGroupRewardAmt2 = Convert.ToInt32(r["PruductGroupRewardAmt2"]);
            }

            if (!string.IsNullOrEmpty(r["StepCondition2"].ToString()) && r["StepCondition2"].ToString() != "NULL")
            {
                HQ_Promotion.StepCondition2 = r["StepCondition2"].ToString();
            }

            if (r["SKUGroupID2"].ToString() != "NULL" && !string.IsNullOrEmpty(r["SKUGroupID2"].ToString()))
            {
                HQ_Promotion.SKUGroupID2 = Convert.ToInt32(r["SKUGroupID2"]);
            }

            HQ_Promotion.RewardID = r["RewardID"].ToString();

            if (r["PromotionPriority"].ToString() != "NULL" && !string.IsNullOrEmpty(r["PromotionPriority"].ToString()))
            {
                HQ_Promotion.PromotionPriority = Convert.ToInt32(r["PromotionPriority"]);
            }

            if (r["HitLimit"].ToString() != "NULL" && !string.IsNullOrEmpty(r["HitLimit"].ToString()))
            {
                HQ_Promotion.HitLimit = Convert.ToInt32(r["HitLimit"]);
            }

            string Recomputable = r["Recomputable"].ToString();

            if (r["Recomputable"].ToString() != "NULL" && !string.IsNullOrEmpty(r["Recomputable"].ToString()))
            {
                HQ_Promotion.Recomputable = Convert.ToInt32(r["Recomputable"]) == 0 ? false : true;
            }

            if (r["IgnoreApplied"].ToString() != "NULL" && !string.IsNullOrEmpty(r["IgnoreApplied"].ToString()))
            {
                HQ_Promotion.IgnoreApplied = Convert.ToInt32(r["IgnoreApplied"]) == 0 ? false : true;
            }

            if (r["IsUseCoupon"].ToString() != "NULL" && !string.IsNullOrEmpty(r["IsUseCoupon"].ToString()))
            {
                HQ_Promotion.IsUseCoupon = Convert.ToInt32(r["IsUseCoupon"]) == 0 ? false : true;
            }

            if (!string.IsNullOrEmpty(r["EffectiveDate"].ToString()))
            {
                DateTime newDate = new DateTime();
                newDate = PrePareNewDatetimeFormat2(Convert.ToDateTime(r["EffectiveDate"]));
                HQ_Promotion.EffectiveDate = newDate;
            }

            if (!string.IsNullOrEmpty(r["ExpireDate"].ToString()))
            {
                DateTime newDate = new DateTime();
                newDate = PrePareNewDatetimeFormat2(Convert.ToDateTime(r["ExpireDate"]));
                HQ_Promotion.ExpireDate = newDate;
            }

            if (r["PlusSaleFrom"].ToString() != "NULL" && !string.IsNullOrEmpty(r["PlusSaleFrom"].ToString()))
            {
                HQ_Promotion.PlusSaleFrom = Convert.ToInt32(r["PlusSaleFrom"]);
            }
        }
        private DateTime PrePareNewDatetimeFormat2(DateTime TempDateTime)
        {
            int year = 0;

            if (TempDateTime.Year > 2500)
            {
                year = Convert.ToInt32(TempDateTime.Year - 543);
            }
            else
            {
                year = Convert.ToInt32(TempDateTime.Year + 543);
            }
             
            int months = Convert.ToInt32(TempDateTime.Month);
            int days = Convert.ToInt32(TempDateTime.Day);

            int hour = Convert.ToInt32(TempDateTime.Hour);
            int min = Convert.ToInt32(TempDateTime.Minute);
            int second = Convert.ToInt32(TempDateTime.Second);
            int Millisecond = Convert.ToInt32(TempDateTime.Millisecond);

            DateTime newDate = new DateTime(year, months, days, hour, min, second, Millisecond);

            return newDate;
        }
        private int Save_HQPromotion()
        {
            int ret = 0;

            var HQ_PromotionList = new List<tbl_HQ_Promotion>();
         

            var Promotion = bu.GetSelectHQPromotion();

            DataTable dt = new DataTable();

            dt = (DataTable)grdHQPromotion.DataSource;

            DateTime dtpDate = DateTime.Now;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    string PromotionID = r["PromotionID"].ToString();

                    var HQ_Promotion = new tbl_HQ_Promotion();

                    if (!string.IsNullOrEmpty(PromotionID))
                    {
                        HQ_Promotion = Promotion.FirstOrDefault(x => x.PromotionID == PromotionID);

                        if (HQ_Promotion != null) //Edit
                        {
                            PrePareSave_HQPromotion(HQ_Promotion,r);
                           
                            HQ_Promotion.UpdateDate = DateTime.Now;
                            HQ_Promotion.UpdateBy = Helper.tbl_Users.Username;
                        }
                        else //Insert
                        {
                            HQ_Promotion = new tbl_HQ_Promotion();
                            HQ_Promotion.PromotionID = PromotionID;

                            PrePareSave_HQPromotion(HQ_Promotion,r);

                            HQ_Promotion.CreatedDate = DateTime.Now;
                            HQ_Promotion.CreateBy = Helper.tbl_Users.Username;

                            HQ_Promotion.UpdateDate = null;
                            HQ_Promotion.UpdateBy = null;
                        }
                        HQ_PromotionList.Add(HQ_Promotion);
                    }
                }

                foreach (var item in HQ_PromotionList)
                {
                    ret = bu.UpdateHQPromotionData(item);
                }
               
            }

            return ret;
        }
        private int Save_HQPromotionMaster()
        {
            int ret = 0;
            try
            {
                
                var PromotionMasterList = new List<tbl_HQ_Promotion_Master>();

                DataTable dt = new DataTable();
                dt = (DataTable)grdHQPromotionMaster.DataSource;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        var PromotionMaster = new tbl_HQ_Promotion_Master();

                        string PromotionID = r["PromotionID"].ToString();

                        if (!string.IsNullOrEmpty(PromotionID))
                        {
                            PromotionMaster.PromotionID = PromotionID;
                            PromotionMaster.PromotionName = r["PromotionName"].ToString();
                            PromotionMasterList.Add(PromotionMaster);
                        }
                    }
                }

                foreach (var item in PromotionMasterList)
                {
                    ret = bu.UpdateHQ_Promotion_MasterData(item);
                }

            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
            return ret;
        }
        private int Save_HQReward()
        {
            int ret = 0;
            try
            {
               
                var tbl_HQ_RewardList = new List<tbl_HQ_Reward>();

                DataTable dt = new DataTable();
                dt = (DataTable)grdHQReward.DataSource;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        string RewardID = r["RewardID"].ToString();

                        if (!string.IsNullOrEmpty(RewardID))
                        {
                            var tbl_HQ_Reward = new tbl_HQ_Reward();
                            tbl_HQ_Reward.RewardID = RewardID;
                            tbl_HQ_Reward.RewardName = r["RewardName"].ToString();
                            tbl_HQ_RewardList.Add(tbl_HQ_Reward);
                        }
                    }
                }
                foreach (var item in tbl_HQ_RewardList)
                {
                    ret = bu.UpdateHQ_RewardData(item);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
            return ret;
        }
        private int Save_HQSKUGroup()
        {
            int ret = 0;
            try
            {
                var tbl_HQ_SKUGroupList = new List<tbl_HQ_SKUGroup>();

                DataTable dt = new DataTable();
                dt = (DataTable)grdHQSKUGroup.DataSource;

                foreach (DataRow r in dt.Rows)
                {
                    string SKUGroupID = r["SKUGroupID"].ToString();

                    if (!string.IsNullOrEmpty(SKUGroupID))
                    {
                        var tbl_HQ_SKUGroup = new tbl_HQ_SKUGroup();

                        tbl_HQ_SKUGroup.SKUGroupID = SKUGroupID;
                        tbl_HQ_SKUGroup.SKU_ID = r["SKU_ID"].ToString();
                        tbl_HQ_SKUGroupList.Add(tbl_HQ_SKUGroup);
                    }
                }

                foreach (var item in tbl_HQ_SKUGroupList)
                {
                    ret = bu.UpdateHQ_SKUGroupData(item);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
            return ret;
        }
        private int Save_HQSKUGroup_EXC()
        {
            int ret = 0;
            try
            {
                var SKUGroupEXC_List = new List<tbl_HQ_SKUGroup_EXC>();

                DataTable dt = new DataTable();
                dt = (DataTable)grdHQSKUGroupExc.DataSource;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        string SKU_ID = r["SKU_ID"].ToString();

                        if (!string.IsNullOrEmpty(SKU_ID))
                        {
                            var SKUGroupEXC = new tbl_HQ_SKUGroup_EXC();

                            SKUGroupEXC.SKU_ID = SKU_ID;
                            SKUGroupEXC_List.Add(SKUGroupEXC);
                        }
                    }

                    foreach (var item in SKUGroupEXC_List)
                    {
                        ret = bu.UpdateSKUGroup_ExcData(item);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
            return ret;
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}
