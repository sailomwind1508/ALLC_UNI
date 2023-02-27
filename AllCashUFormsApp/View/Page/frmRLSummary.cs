using AllCashUFormsApp.Model;
using AllCashUFormsApp.View.UControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Controller;
using System.Globalization;

namespace AllCashUFormsApp.View.Page
{
    /// <summary>
    /// Last edit by sailom 09/08/2021
    /// </summary>
    public partial class frmRLSummary : Form
    {
        //PRMaster buPRMaster = new PRMaster();
        //PRDetail buPRDetail = new PRDetail();

        RLSummary buRLSummary = new RLSummary();
        BranchWarehouse buWH = new BranchWarehouse();
        RL bu = new RL();
        Product buPro = new Product();
        MenuBU menuBU = new MenuBU();

        List<Control> searchBranchControls = new List<Control>();

        static DataTable dtRLSummary = new DataTable(); // ข้อมูลที่ดึงจาก Store
        static DataTable dtRLPro = new DataTable();

        List<tbl_PRMaster> tbl_PRMasters = new List<tbl_PRMaster>();
        List<tbl_ProductUomSet> allprdUOMSets = new List<tbl_ProductUomSet>();
        Dictionary<string, DataTable> rlList = new Dictionary<string, DataTable>();

        public frmRLSummary()
        {
            InitializeComponent();

            searchBranchControls = new List<Control>() { txtBranchCode, txtBranchName };
        }

        #region private methods

        private void InitPage()
        {
            var menu = buWH.GetAllFromMenu().FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (menu != null)
            {
                FormHeader.Text = menu.FormText;
                FormHeader.BackColor = ColorTranslator.FromHtml("#7AD1F9");
            }

            var headerPic = bu.tbl_MstMenu.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (headerPic != null)
            {
                FormPic.Image = headerPic.MenuImage.byteArrayToImage();
                FormPic.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            dtpDocDate.SetDateTimePickerFormat();
        }

        private void InitialData()
        {
            var b = buWH.GetBranch();
            if (b != null)
            {
                this.BindData("FromBranchID", searchBranchControls, b[0].BranchID);
            }

            txtBranchName.DisableTextBox(true);

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, "");
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;


            grdRL.AutoGenerateColumns = false;

            ReadOnlyGridView(true);

            allprdUOMSets = bu.GetUOMSet();
        }

        private void PrepareVanData()
        {
            DataTable dtVan = new DataTable();
            if (dtVan.Columns.Count == 0)
            {
                dtVan.Columns.Add("WHCode", typeof(string));
                dtVan.Columns.Add("WHName", typeof(string));
                dtVan.Columns.Add("Status", typeof(string));
            }

            DataTable dtallvan = new DataTable();
            dtallvan = buWH.GetBranchWareHouseTable(x => x.WHType != 0); // edit by sailom .k 03/03/2022 for support pre-order// WHType == 1);

            Func<tbl_PRMaster, bool> func = (x => x.DocDate.ToDateTimeFormatString() == dtpDocDate.Value.ToDateTimeFormatString() && x.DocTypeCode == "RL");

            var _tbl_PRMasters = bu.GetPRMaster("RL", func);
            if (_tbl_PRMasters != null && _tbl_PRMasters.Count > 0)
            {
                foreach (DataRow r in dtallvan.Rows)
                {
                    string whid = r["WHID"].ToString();
                    string whName = r["WHName"].ToString();
                    string status = "ไม่เบิก";

                    var pr = _tbl_PRMasters.FirstOrDefault(x => x.ToWHID == whid);
                    if (pr != null)
                    {
                        if (pr.DocStatus == "3")
                        {
                            status = "ขอเบิก";
                        }
                        if (pr.DocStatus == "4")
                        {
                            status = "เบิกสำเร็จแล้ว";
                        }
                        if (pr.DocStatus == "5")
                        {
                            status = "ยกเลิกใบเบิก";
                        }
                    }

                    dtVan.Rows.Add(whid, whName, status);
                }
            }
            else
            {
                foreach (DataRow r in dtallvan.Rows)
                {
                    string whid = r["WHID"].ToString();
                    string whName = r["WHName"].ToString();
                    string status = "ไม่เบิก";

                    dtVan.Rows.Add(whid, whName, status);
                }
            }

            grdVan.DataSource = dtVan;
            grdVan.AutoGenerateColumns = false;
        }

        private void PrepareRLData() //search Bind grdVan
        {
            try
            {
                PrepareVanData();

                VerifyVanRL();

                //dtRLSummary = new DataTable(); //กดค้นหา ให้ Clear ข้อมูล

                //var allVan = buWH.GetAllBranchWarehouse(x => x.WHType == 1).ToList();

                ////List<DataTable> dtList = new List<DataTable>();
                //DataTable dtGetRL = new DataTable();

                ////foreach (var item in allVan)
                ////{
                ////    dtGetRL = buRLSummary.GetRLSummary(item.WHID, dtpDocDate.Value); // Search From Store
                ////    dtList.Add(dtGetRL);
                ////}

                ////string whidFirst = allVan.OrderBy(x => x.WHID).First().WHID;
                ////dtGetRL = buRLSummary.GetRLSummary(whidFirst, dtpDocDate.Value); // Search From Store

                //if (dtGetRL.Rows.Count > 0)
                //{
                //    bool isRL = false;
                //    DateTime docdate = DateTime.Now;

                //    foreach (DataRow r in dtGetRL.Rows)
                //    {
                //        string DocNo = r["DocNo"].ToString();
                //        string WHID = r["WHID"].ToString();
                //        if (!string.IsNullOrEmpty(DocNo) && !string.IsNullOrEmpty(WHID))
                //        {
                //            isRL = true;
                //            docdate = Convert.ToDateTime(r["DocDate"]); // เก็บวันที่
                //            break; // ถ้า DocNo กับ WHID มีข้อมูลให้จบ Loop
                //        }
                //    }

                //    if (isRL == true)
                //    {
                //        if (dtRLSummary.Columns.Count == 0)
                //        {
                //            dtRLSummary = dtGetRL.Clone(); // คัดลอกเฉพาะคอลัมน์
                //        }
                //        foreach (DataRow r in dtGetRL.Rows)
                //        {
                //            dtRLSummary.Rows.Add(r[0], r[1], r[2], r[3], r[4], r[5], r[6], r[7],
                //                r[8], r[9], r[10], r[11], docdate, r[13]);
                //        }
                //    }
                //}

                //if (dtRLSummary.Rows.Count > 0) //มีข้อมูล  สถานะ ขอเบิก = ไม่มี ซ่อน
                //{
                //    StatusColumnGridVan(); //

                //    BindDataGridRL();
                //}
                //else
                //{
                //    dtRLSummary.Clear();
                //    grdRL.DataSource = dtRLSummary;
                //    BindVan();
                //}

                //BindVan();
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void BindDataGridRL() // CellClick // ข้อมูลจาก Store มาผสมกับ Product
        {
            try
            {
                DataTable dtGetRL = new DataTable();
                if (grdVan.CurrentRow != null)
                {
                    string WHID = grdVan.CurrentRow.Cells["colWHID"].Value.ToString();
                    bool hasOldData = false;

                    if (rlList.Count > 0)
                    {
                        var tmp = rlList.FirstOrDefault(x => x.Key == WHID);
                        if (tmp.Key != null)
                        {
                            dtGetRL = tmp.Value;
                            hasOldData = true;
                        }
                    }

                    if (!hasOldData)
                        dtGetRL = buRLSummary.GetRLSummary(WHID, dtpDocDate.Value);

                    //dtGetRL = buRLSummary.GetRLSummary(WHID, dtpDocDate.Value);

                    if (dtGetRL.Rows.Count > 0)
                    {
                        dtRLSummary = dtGetRL;

                        //Manage Temp---------------------------------------
                        if (rlList.Any(x => x.Key == WHID))
                            rlList.Remove(WHID);

                        rlList.Add(WHID, dtRLSummary);
                        //Manage Temp---------------------------------------

                        DataTable dtGetPro = new DataTable();

                        dtGetPro = buPro.GetProductTable(x => !x.IsFulfill && !x.FlagDel);

                        if (dtRLPro.Columns.Count == 0)
                        {
                            dtRLPro.Columns.Add("WHID", typeof(string));
                            dtRLPro.Columns.Add("ProductID", typeof(string));
                            dtRLPro.Columns.Add("PrdName", typeof(string));

                            dtRLPro.Columns.Add("CarQty", typeof(decimal));
                            dtRLPro.Columns.Add("PckQty", typeof(decimal));

                            dtRLPro.Columns.Add("RLCarQty", typeof(decimal));
                            dtRLPro.Columns.Add("RLPckQty", typeof(decimal));

                            dtRLPro.Columns.Add("RL1000CarQty", typeof(decimal));
                            dtRLPro.Columns.Add("RL1000PckQty", typeof(decimal));

                            dtRLPro.Columns.Add("ProductName", typeof(string));
                        }

                        var temp = dtGetRL.AsEnumerable().ToList();
                        if (dtRLPro.Rows.Count > 0)
                        {
                            dtRLPro.Rows.Clear();
                        }

                        foreach (DataRow r in dtGetPro.Rows)
                        {
                            string proName = r["ProductName"].ToString();
                            string prdName = r["ProductID"].ToString() + " : " + r["ProductShortName"].ToString();
                            string proID = r["ProductID"].ToString();
                            DataRow item = temp.FirstOrDefault(x => x.Field<string>("ProductID") == proID
                                    && x.Field<string>("WHID") == WHID);
                            if (item != null)
                            {
                                dtRLPro.Rows.Add(item["WHID"].ToString(), item["ProductID"].ToString(), prdName
                                    , item["VANCarQty"], item["VANPckQty"]
                                    , item["RLCarQty"], item["RLPckQty"]
                                    , item["RL1000CarQty"], item["RL1000PckQty"], item["ProductName"].ToString());
                            }
                            else
                            {
                                dtRLPro.Rows.Add(WHID, proID, prdName, 0, 0, 0, 0, 0, 0, proName);
                            }
                        }
                        grdRL.DataSource = dtRLPro;
                        //StatusColumnGridVan();
                    }
                    else
                    {
                        dtRLPro.Clear();
                        grdRL.DataSource = dtRLPro;
                    }

                    Func<tbl_PRMaster, bool> func = (x => x.DocDate.ToDateTimeFormatString() == dtpDocDate.Value.ToDateTimeFormatString()
                    && x.DocTypeCode == "RL" && x.DocStatus == "3" && x.ToWHID == WHID);

                    var _tbl_PRMasters = bu.GetPRMaster("RL", func);
                    if (btnEdit.Enabled == false && _tbl_PRMasters != null && _tbl_PRMasters.Count > 0)
                    {
                        ReadOnlyGridView(false);
                    }
                    else
                    {
                        ReadOnlyGridView(true);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void CheckEnableEdit()
        {
            //var tmp = dtRLSummary.AsEnumerable().ToList();

            //DataRow item = tmp.FirstOrDefault(x => x.Field<string>("DocStatus") == "3" && x.Field<string>("DocTypeCode") == "RL");
            Func<tbl_PRMaster, bool> func = (x => x.DocDate.ToDateTimeFormatString() == dtpDocDate.Value.ToDateTimeFormatString() && x.DocTypeCode == "RL"
            && x.DocStatus == "3" && (x.DocNo.Length > 12 || x.DocNo.Contains("V")));

            tbl_PRMasters = bu.GetPRMaster("RL", func);
            if (tbl_PRMasters != null && tbl_PRMasters.Count > 0)
            {
                btnEdit.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
            }
        }

        private void PrepareInvMovement(tbl_PRMaster tbl_PRMaster, List<tbl_PRDetail> tbl_PRDetailsList)
        {
            //bu.tbl_InvMovements.Clear();

            //var invMms = bu.tbl_InvMovements;
            var prDts = tbl_PRDetailsList;
            var pr = tbl_PRMaster;
            var prod = bu.GetProduct();
            var prodGroup = bu.GetProductGroup();
            var prodSubGroup = bu.GetProductSubGroup();

            foreach (var prDt in tbl_PRDetailsList)
            {
                for (int i = 0; i < 2; i++)
                {
                    var invMm = new tbl_InvMovement();

                    invMm.ProductID = prDt.ProductID;
                    invMm.ProductName = prDt.ProductName;
                    invMm.RefDocNo = prDt.DocNo;
                    invMm.TrnDate = dtpDocDate.Value.ToDateTimeFormat(); //crDate.ToDateTimeFormat();
                    invMm.TrnType = "T";
                    invMm.DocTypeCode = pr.DocTypeCode;

                    decimal unitQty = 0;

                    var prdUOMSets = bu.GetProductUOMSet(allprdUOMSets, prDt.ProductID);
                    if (prdUOMSets != null && prdUOMSets.Count > 0)
                    {
                        //if (prDt.OrderUom != 2)
                        //    unitQty = (prDt.ReceivedQty.Value * prdUOMSets[0].BaseQty);
                        var uom = bu.GetUOMSet().FirstOrDefault(x => x.ProductID == prDt.ProductID && x.UomSetID == prDt.OrderUom);

                        if (uom != null)//if (prDt.OrderUom != 2)
                            unitQty = (prDt.ReceivedQty.Value * uom.BaseQty);
                        else
                            unitQty = prDt.ReceivedQty.Value;
                    }
                    else
                    {
                        unitQty = prDt.ReceivedQty.Value;
                    }

                    if (i == 0)
                    {
                        invMm.WHID = pr.ToWHID;
                        invMm.FromWHID = "";
                        invMm.ToWHID = "";
                        invMm.TrnQtyIn = unitQty;
                        invMm.TrnQtyOut = 0;
                        invMm.TrnQty = unitQty;
                    }
                    else
                    {
                        invMm.WHID = pr.FromWHID;
                        invMm.FromWHID = pr.FromWHID;
                        invMm.ToWHID = pr.ToWHID;
                        invMm.TrnQtyIn = 0;
                        invMm.TrnQtyOut = unitQty;
                        invMm.TrnQty = -(unitQty);
                    }

                    invMm.CrDate = DateTime.Now;

                    var prodItem = prod.FirstOrDefault(x => x.ProductID == prDt.ProductID);
                    var prodGroupItem = prodGroup.FirstOrDefault(x => x.ProductGroupID == prodItem.ProductGroupID);
                    var prodSubGroupItem = prodSubGroup.FirstOrDefault(x => x.ProductSubGroupID == prodItem.ProductSubGroupID);

                    invMm.ProductGroupCode = prodGroupItem.ProductGroupCode;
                    invMm.ProductGroupName = prodGroupItem.ProductGroupName;
                    invMm.ProductSubGroupCode = prodSubGroupItem.ProductSubGroupCode;
                    invMm.ProductSubGroupName = prodSubGroupItem.ProductSubGroupName;
                    invMm.FlagSend = false;

                    bu.tbl_InvMovements.Add(invMm);
                }
            }
        }

        private void PrepareQtyOnHand(tbl_InvWarehouse invWh, string productID, string whid, decimal unitQty)
        {
            var invWhItem = bu.GetInvWarehouse(productID, whid);

            if (invWhItem != null && invWhItem.Count > 0)
            {
                if (whid.Contains("1000"))
                    invWh.QtyOnHand = invWhItem[0].QtyOnHand - unitQty;
                else
                    invWh.QtyOnHand = invWhItem[0].QtyOnHand + unitQty;
            }
            else
            {
                if (whid.Contains("1000"))
                    invWh.QtyOnHand = -unitQty;
                else
                    invWh.QtyOnHand = +unitQty;
            }
        }

        private void PrepareInvWarehouseFrom(bool editFlag = false) // 1000
        {
            bu.tbl_InvWarehouses.Clear();

            bool Flag = true;

            SubPrepareInvWarehouse(Flag);
        }

        private void PrepareInvWarehouseTo() // van
        {
            bool Flag = false;

            SubPrepareInvWarehouse(Flag);
        }

        private void ReadOnlyGridView(bool flag)
        {
            grdRL.Columns["colRLCarQty"].ReadOnly = flag; //เบิกใหญ่
            grdRL.Columns["colRLPckQty"].ReadOnly = flag; //เบิกเล็ก
        }

        //private void StatusColumnGridVan()// ถ้าเลือก แวนที่ไม่มีใน 
        //{
        //    //BindVan();

        //    var allWHID = (from DataRow r in dtRLSummary.Rows select r["WHID"]).Distinct();

        //    for (int i = 0; i < grdVan.Rows.Count; i++)
        //    {
        //        foreach (var item in allWHID)
        //        {
        //            if (grdVan.Rows[i].Cells["colWHID"].Value.ToString() == item.ToString())
        //            {
        //                grdVan.Rows[i].Cells["ColStatus"].Value = "ขอเบิก";
        //                break;
        //            }
        //        }
        //    }
        //    string WHID = grdVan.CurrentRow.Cells["colWHID"].Value.ToString();

        //    //for (int i = 0; i < grdVan.Rows.Count; i++)//
        //    //{
        //    //    string status = grdVan.Rows[i].Cells["ColStatus"].Value.ToString();
        //    //    if (status == "ไม่เบิก" )
        //    //    {
        //    //        grdVan.Rows[i].Visible = false;
        //    //    } 

        //    //}
        //    //grdVan.Sort(grdVan.Columns["ColStatus"], ListSortDirection.Ascending);
        //}

        private void SubPrepareInvWarehouse(bool flag)
        {
            List<tbl_InvWarehouse> tbl_InvWarehouses = new List<tbl_InvWarehouse>();
            //tbl_InvWarehouses = bu.GetInvWarehouse();
            var invWhs = bu.tbl_InvWarehouses;
            var prDts = bu.tbl_PRDetails;

            DateTime EdDate = DateTime.Now;

            foreach (var prDt in prDts)
            {
                var invWh = new tbl_InvWarehouse();


                invWh.ProductID = prDt.ProductID;

                string WHID = "";

                if (flag == true)    //true 1000 
                {
                    WHID = tbl_PRMasters.FirstOrDefault(X => X.DocNo == prDt.DocNo).FromWHID;
                }

                else if (flag == false) //false van
                {
                    WHID = tbl_PRMasters.FirstOrDefault(X => X.DocNo == prDt.DocNo).ToWHID;
                }

                invWh.WHID = WHID;

                invWh.EdDate = EdDate;
                invWh.EdUser = Helper.tbl_Users.Username;
                invWh.CrDate = EdDate;
                invWh.CrUser = Helper.tbl_Users.Username;


                decimal unitQty = 0;

                var prdUOMSets = bu.GetProductUOMSet(allprdUOMSets, prDt.ProductID);  // เช็ค UOMsetID ว่าเป็น Pack หรือ Car

                if (prdUOMSets != null && prdUOMSets.Count > 0)
                {
                    //if (prDt.OrderUom != 2)
                    //    unitQty = (prDt.ReceivedQty.Value * prdUOMSets[0].BaseQty);
                    var uom = bu.GetUOMSet().FirstOrDefault(x => x.ProductID == prDt.ProductID && x.UomSetID == prDt.OrderUom);

                    if (uom != null)//if (prDt.OrderUom != 2)
                        unitQty = (prDt.ReceivedQty.Value * uom.BaseQty);
                    else
                        unitQty = prDt.ReceivedQty.Value;

                    var chkInvWhs = invWhs.FirstOrDefault(x => x.WHID == WHID && x.ProductID == prDt.ProductID);

                    if (chkInvWhs != null)
                    {
                        foreach (var item in invWhs)
                        {
                            if (item.WHID == WHID && item.ProductID == prDt.ProductID)
                            {
                                if (item.WHID.Contains("1000"))
                                {
                                    item.QtyOnHand = item.QtyOnHand - unitQty;
                                }
                                else
                                {
                                    item.QtyOnHand = item.QtyOnHand + unitQty;
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        PrepareQtyOnHand(invWh, prDt.ProductID, WHID, unitQty);

                        invWh.QtyOnOrder = 0;
                        invWh.QtyOnBackOrder = 0;
                        invWh.QtyInTransit = 0;
                        invWh.QtyOutTransit = 0;
                        invWh.QtyOnReject = 0;
                        invWh.MinimumQty = 0;
                        invWh.MaximumQty = 0;
                        invWh.ReOrderQty = 0;

                        invWh.FlagDel = false;
                        invWh.FlagSend = false;

                        invWhs.Add(invWh);
                    }
                }
            }
        }

        private void Save()
        {
            try
            {
                List<string> VanName = new List<string>();
                List<string> validateWHID = new List<string>();

                foreach (DataTable item in rlList.Values)
                {
                    var List_allWHID = (from DataRow r in item.Rows select r["WHID"]).Distinct(); // WHID

                    foreach (var listVan in List_allWHID)
                    {
                        var tmp = item.AsEnumerable().ToList();
                        DataRow checkRLQty = tmp.FirstOrDefault(x => x.Field<string>("WHID") == listVan.ToString()
                             && (x.Field<Decimal>("RLCarQty") > 0 || x.Field<Decimal>("RLPckQty") > 0));
                        if (checkRLQty != null)
                        {

                        }
                        else
                        {
                            for (int i = 0; i < grdVan.Rows.Count; i++)
                            {
                                string WHID = grdVan.Rows[i].Cells["colWHID"].Value.ToString();
                                if (listVan.ToString() == WHID)
                                {
                                    validateWHID.Add(WHID);
                                    string WHName = grdVan.Rows[i].Cells["colWHName"].Value.ToString();
                                    VanName.Add("--> แวน : " + WHName);
                                }
                            }
                        }
                    }//loop validate ถ้ากรอกเบิกเป็นศูนย์ทั้งหมด ในแต่ละแวนจะแจ้งเตือน
                }

                if (VanName.Count > 0)
                {
                    string message = "จำนวนขอเบิกต้องไม่เป็น 0 \n\n" + string.Join("\n", VanName);
                    message.ShowWarningMessage();
                }

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่? ";
                string title = "ยืนยันการบันทึก!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                List<int> saveFlag = new List<int>();

                string _docdate = dtpDocDate.Value.ToDateTimeFormatString();

                validateWHID = validateWHID.Distinct().ToList();

                var filterRL = new Dictionary<string, DataTable>();
                var tmpRL = rlList.Where(x => !validateWHID.Contains(x.Key)).ToList(); //filter rl = 0
                foreach (var item in tmpRL)
                {
                    filterRL.Add(item.Key, item.Value);
                }

                foreach (var rlItem in filterRL)
                {
                    int ret = 0;

                    tbl_PRMasters = new List<tbl_PRMaster>();

                    //BaseControl GetPRMaster
                    Func<tbl_PRMaster, bool> func = (x => x.DocDate.ToDateTimeFormatString() == _docdate && x.DocTypeCode == "RL" && x.DocStatus == "3" && x.ToWHID == rlItem.Key);

                    tbl_PRMasters = bu.GetPRMaster("RL", func);

                    foreach (var item in tbl_PRMasters)
                    {
                        item.DocStatus = "4";
                        item.EdDate = DateTime.Now;
                        item.EdUser = Helper.tbl_Users.Username;
                    }//แก้ไขข้อมูลใน PRMASTER


                    foreach (var item in tbl_PRMasters)
                    {
                        ret = buRLSummary.UpdatePRMasterData(item);
                    }    //บันทึกข้อมูลลง PRMASTER

                    if (ret == 1) // แก้ไข PRMASTER สำเร็จ 
                    {
                        List<tbl_PRDetail> PRDetails_List = new List<tbl_PRDetail>();
                        foreach (var item in tbl_PRMasters)
                        {
                            //BaseControl GetPRDetail
                            PRDetails_List = bu.GetPRDetails("RL", x => x.DocNo == item.DocNo.ToString());
                            foreach (var item2 in PRDetails_List)
                            {
                                ret = buRLSummary.RemoveRLPRDetail(item2);
                            }
                        }  //ลบข้อมูลใน PRDETAIL ที่ DocNo ตรงกับ PRMASTER ทั้งหมด


                        if (ret == 1) //ลบข้อมูล PRDETAIL สำเร็จ 
                        {
                            List<tbl_PRDetail> tbl_PRDetailsList = new List<tbl_PRDetail>();
                            bu.tbl_PRDetails.Clear();
                            bu.tbl_PRDetails = tbl_PRDetailsList;

                            short lineNo = 0;

                            foreach (DataRow r in rlItem.Value.Rows)
                            {
                                decimal RLCarQty = Convert.ToDecimal(r["RLCarQty"]); //หีบใหญ่
                                decimal RLPckQty = Convert.ToDecimal(r["RLPckQty"]);  //หีบเล็ก
                                if (RLCarQty == 0 && RLPckQty == 0)
                                {

                                }
                                else
                                {
                                    string proID = r["ProductID"].ToString();
                                    string proName = r["ProductName"].ToString();

                                    List<tbl_ProductUomSet> tbl_ProductUomSetList = new List<tbl_ProductUomSet>();
                                    tbl_ProductUomSetList = buWH.GetUOMSet(x => x.ProductID == proID);

                                    tbl_PRDetail tbl_PRDetails = new tbl_PRDetail();
                                    decimal totalQty = 0; // จำนวนขอเบิก 

                                    if (RLCarQty > 0 && RLPckQty > 0) // เบิกใหญ่ กับ เบิกเล็ก  //มีทั้งสองให้แปลงเป็น แพ็คทั้งหมด
                                    {
                                        int baseQty = tbl_ProductUomSetList[0].BaseQty;
                                        totalQty = (baseQty * RLCarQty) + RLPckQty; // (จำนวนแพ็คพื้นฐาน * หีบ) + แพ็ค

                                        var list = tbl_ProductUomSetList.Where(x => x.BaseQty == 1).ToList();
                                        tbl_PRDetails.OrderUom = list[0].UomSetID; //Pack
                                    }

                                    else if (RLCarQty > 0 && RLPckQty == 0) //เบิกใหญ่
                                    {
                                        totalQty = RLCarQty;
                                        tbl_PRDetails.OrderUom = tbl_ProductUomSetList[0].UomSetID; // 1
                                    }

                                    else if (RLCarQty == 0 && RLPckQty > 0) // เบิกเล็ก
                                    {
                                        totalQty = RLPckQty;

                                        var list = tbl_ProductUomSetList.Where(x => x.BaseQty == 1).ToList();
                                        tbl_PRDetails.OrderUom = list[0].UomSetID; //Pack 2
                                    }

                                    string whid = r["WHID"].ToString();

                                    string docno = "";
                                    var prMaster = tbl_PRMasters.FirstOrDefault(x => x.ToWHID == whid);
                                    if (prMaster != null)
                                    {
                                        docno = prMaster.DocNo;
                                    }

                                    //foreach (var item in tbl_PRMasters)
                                    //{
                                    //    if (item.ToWHID == whid)
                                    //    {
                                    //        docno = item.DocNo;
                                    //        break;
                                    //    }
                                    //}

                                    tbl_PRDetails.DocNo = docno;

                                    tbl_PRDetails.ProductID = proID;
                                    tbl_PRDetails.ProductName = proName;

                                    tbl_PRDetails.Line = lineNo;


                                    tbl_PRDetails.SendQty = totalQty; // null   //จำนวนโอนให้

                                    tbl_PRDetails.ReceivedQty = totalQty; // null //จำนวนโอนให้

                                    tbl_PRDetails.RejectedQty = 0; // 

                                    tbl_PRDetails.OrderQty = 0; //

                                    tbl_PRDetails.StockedQty = 0;  //no null
                                    tbl_PRDetails.UnitCost = 0;  // null
                                    tbl_PRDetails.UnitPrice = 0; // null

                                    tbl_PRDetails.VatType = 0; // null
                                    tbl_PRDetails.LineTotal = 0; // null
                                    tbl_PRDetails.LineRemark = "";

                                    tbl_PRDetails.CrDate = DateTime.Now;
                                    tbl_PRDetails.CrUser = Helper.tbl_Users.Username;

                                    tbl_PRDetails.FlagDel = false;
                                    tbl_PRDetails.FlagSend = false;

                                    tbl_PRDetailsList.Add(tbl_PRDetails);

                                    lineNo++;
                                }

                            }  // Finish Loop  // Insert ข้อมูลใหม่ลงใน PRDETAIL
                            bu.tbl_InvMovements.Clear();

                            foreach (var tbl_PRMaster in tbl_PRMasters)
                            {
                                var _tbl_PRDetailsList = tbl_PRDetailsList.Where(x => x.DocNo == tbl_PRMaster.DocNo).ToList();
                                PrepareInvMovement(tbl_PRMaster, _tbl_PRDetailsList);
                            }

                            PrepareInvWarehouseFrom();

                            PrepareInvWarehouseTo();

                            ret = bu.UpdateData();

                            if (ret == 1)
                            {
                                saveFlag.Add(ret);
                            }
                        }
                    }
                }

                if (saveFlag.All(x => x == 1))
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    //btnSearchDepo.PerformClick();

                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;

                    //ReadOnlyGridView(true);

                    //PrepareVanData();
                    btnGetRL2.PerformClick();
                }
                else
                {
                    this.ShowProcessErr();
                    return;
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void SetCellStyle(DataGridViewRow row, string colName, Color _color, bool isBold = false)
        {
            row.Cells[colName].Style.ForeColor = _color;

            if (isBold)
                row.Cells[colName].Style.Font = new Font(grdRL.Font, FontStyle.Bold);
        }

        private void VerifyVanRL()
        {
            var prList = bu.GetAllPRMaster(dtpDocDate.Value);
            if (prList.Count > 0)
            {
                for (int i = 0; i < grdVan.Rows.Count; i++)
                {
                    string status = "ไม่เบิก";
                    bool isRL = false;

                    var tmp = prList.Select(a => a.ToWHID).Distinct().ToList();
                    if (tmp.Contains(grdVan.Rows[i].Cells["colWHID"].Value.ToString()))
                    {
                        var pr = prList.FirstOrDefault(x => x.ToWHID == grdVan.Rows[i].Cells["colWHID"].Value.ToString());
                        if (pr != null)
                        {
                            if (pr.DocStatus == "3")
                            {
                                status = "ขอเบิก";

                                grdVan.Rows[i].Cells["ColStatus"].Style.BackColor = Color.Yellow;
                                grdVan.Rows[i].Cells["colWHName"].Style.BackColor = Color.Yellow;
                            }
                            else if (pr.DocStatus == "4")
                            {
                                status = "เบิกสำเร็จแล้ว";

                                grdVan.Rows[i].Cells["ColStatus"].Style.BackColor = Color.Green;
                                grdVan.Rows[i].Cells["colWHName"].Style.BackColor = Color.Green;
                            }
                            else if (pr.DocStatus == "5")
                            {
                                status = "ยกเลิกใบเบิก";

                                grdVan.Rows[i].Cells["ColStatus"].Style.BackColor = Color.Red;
                                grdVan.Rows[i].Cells["colWHName"].Style.BackColor = Color.Red;
                            }
                        }

                        grdVan.Rows[i].Cells["ColStatus"].Value = status;

                        if (status != "ไม่เบิก")
                        {
                            isRL = true;
                            grdVan.Rows[i].Cells["ColStatus"].Style.Font = new Font(FontFamily.GenericMonospace, 9.5F, FontStyle.Bold);  //new Font(grdRL.Font, FontStyle.Bold);
                            grdVan.Rows[i].Cells["colWHName"].Style.Font = new Font(FontFamily.GenericMonospace, 9.5F, FontStyle.Bold);
                        }
                    }

                    if (!isRL)
                    {
                        grdVan.Rows[i].Cells["ColStatus"].Value = "ไม่เบิก";

                        grdVan.Rows[i].Cells["ColStatus"].Style.BackColor = Color.White;
                        grdVan.Rows[i].Cells["colWHName"].Style.BackColor = Color.White;

                        grdVan.Rows[i].Cells["ColStatus"].Style.ForeColor = Color.Gray;
                        grdVan.Rows[i].Cells["colWHName"].Style.ForeColor = Color.Gray;
                    }
                }
            }
        }

        #endregion

        #region event methods

        private void frmRLSummary_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

            InitPage();

            InitialData();

            //btnGetRL2.PerformClick();
            //PrepareVanData();
        }

        private void frmRLSummary_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        private void btnGetRL_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            rlList = new Dictionary<string, DataTable>();

            PrepareRLData(); //search ข้อมูล RL จากวันที่มาเก็บไว้ใน Temp

            BindDataGridRL(); // CellClick // ข้อมูลจาก Store มาผสมกับ Product

            CheckEnableEdit();

            ReadOnlyGridView(true);

            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            btnPrint.Enabled = true;

            MemoryManagement.FlushMemory();

            Cursor.Current = Cursors.Default;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;

            if (grdVan.CurrentRow != null)
            {
                var currentRow = grdVan.CurrentRow.Index;

                string whid = grdVan.Rows[currentRow].Cells["colWHID"].Value.ToString();

                Func<tbl_PRMaster, bool> func = (x => x.DocDate.ToDateTimeFormatString() == dtpDocDate.Value.ToDateTimeFormatString()
                    && x.DocTypeCode == "RL" && x.DocStatus == "3" && x.ToWHID == whid);

                var _tbl_PRMasters = bu.GetPRMaster("RL", func);
                if (_tbl_PRMasters != null && _tbl_PRMasters.Count > 0)
                {
                    ReadOnlyGridView(false);
                }
                else
                {
                    ReadOnlyGridView(true);
                }
            }

            //ReadOnlyGridView(false); // เปิด

            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnSearchDepo_Click(object sender, EventArgs e)
        {
            this.OpenFromBranchIDPopup(searchBranchControls, "เลือกเดโป้/สาขา");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            PrepareRLData(); //search ข้อมูล RL จากวันที่มาเก็บไว้ใน Temp

            BindDataGridRL(); // CellClick // ข้อมูลจาก Store มาผสมกับ Product

            CheckEnableEdit();

            ReadOnlyGridView(true);

            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnEdit.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBranchCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
            else
            {
                return;
            }
        }

        private void txtBranchCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtBranchCode.Text != "")
                {
                    var BranchList = buWH.GetBranch(x => x.BranchCode == txtBranchCode.Text);
                    if (BranchList.Count > 0)
                    {
                        txtBranchCode.Text = BranchList[0].BranchCode;
                        txtBranchName.Text = BranchList[0].BranchName;
                    }
                    else
                    {
                        txtBranchCode.Clear();
                        txtBranchName.Clear();
                        txtBranchCode.Focus();
                        return;
                    }
                }
            }
        }

        private void grdVan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            BindDataGridRL();

            MemoryManagement.FlushMemory();

            Cursor.Current = Cursors.Default;
        }

        private void grdVan_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdVan.SetRowPostPaint(sender, e, this.Font);
        }

        private void grdRL_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdRL.SetRowPostPaint(sender, e, this.Font);
            var grid = sender as DataGridView;
 
            //foreach (DataGridViewRow row in grdRL.Rows)
            {
                var row = grid.Rows[e.RowIndex];
                var _1000CarQty = Convert.ToInt32(row.Cells["colRL1000CarQty"].Value);
                var _1000PckQty = Convert.ToInt32(row.Cells["colRL1000PckQty"].Value);
                var _vanCarQty = Convert.ToInt32(row.Cells["colCarQty"].Value);
                var _vanPckQty = Convert.ToInt32(row.Cells["colPckQty"].Value);

                var _rlCarQty = 0;
                var _rlPckQty = 0;
                try
                {
                    if (!string.IsNullOrEmpty(row.Cells["colRLCarQty"].EditedFormattedValue.ToString()))
                        _rlCarQty = Convert.ToInt32(row.Cells["colRLCarQty"].EditedFormattedValue);

                    if (!string.IsNullOrEmpty(row.Cells["colRLPckQty"].EditedFormattedValue.ToString()))
                        _rlPckQty = Convert.ToInt32(row.Cells["colRLPckQty"].EditedFormattedValue);
                }
                catch 
                {
                    if (!string.IsNullOrEmpty(row.Cells["colRLCarQty"].Value.ToString()))
                        _rlCarQty = Convert.ToInt32(row.Cells["colRLCarQty"].Value);


                    if (!string.IsNullOrEmpty(row.Cells["colRLPckQty"].Value.ToString()))
                        _rlPckQty = Convert.ToInt32(row.Cells["colRLPckQty"].Value);
                }

                if (_1000CarQty < 0)
                    SetCellStyle(row, "colRL1000CarQty", Color.Red, true);
                if (_1000PckQty < 0)
                    SetCellStyle(row, "colRL1000PckQty", Color.Red, true);
                if (_1000CarQty == 0)
                    SetCellStyle(row, "colRL1000CarQty", Color.Gray);
                if (_1000PckQty == 0)
                    SetCellStyle(row, "colRL1000PckQty", Color.Gray);

                if (_vanCarQty < 0)
                    SetCellStyle(row, "colCarQty", Color.Red, true);
                if (_vanPckQty < 0)
                    SetCellStyle(row, "colPckQty", Color.Red, true);
                if (_vanCarQty == 0)
                    SetCellStyle(row, "colCarQty", Color.Gray);
                if (_vanPckQty == 0)
                    SetCellStyle(row, "colPckQty", Color.Gray);

                if (_rlCarQty > 0)
                    SetCellStyle(row, "colRLCarQty", Color.Green, true);
                else
                    SetCellStyle(row, "colRLCarQty", Color.Gray);

                if (_rlPckQty > 0)
                    SetCellStyle(row, "colRLPckQty", Color.Green, true);
                else
                    SetCellStyle(row, "colRLPckQty", Color.Gray);
            }
        }

        private void grdRL_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string RLCarQty_cell = grdRL.CurrentRow.Cells["colRLCarQty"].Value.ToString();  //เบิกใหญ่
            string RLPckQty_cell = grdRL.CurrentRow.Cells["colRLPckQty"].Value.ToString(); //เบิกเล็ก
            string whid = grdVan.CurrentRow.Cells["colWHID"].Value.ToString();
            string prdID = grdRL.Rows[e.RowIndex].Cells["colProductID"].Value.ToString();

            if (dtRLSummary != null && dtRLSummary.Rows.Count > 0)
            {
                var grd = sender as DataGridView;
                if (e.ColumnIndex == 6) //Car
                {
                    grdRL.Rows[e.RowIndex].Cells[7].Value = 0;
                }
                else if (e.ColumnIndex == 7) //Pack
                {
                    grdRL.Rows[e.RowIndex].Cells[6].Value = 0;
                }

                var tmp = dtRLSummary.AsEnumerable().ToList();

                DataRow item = tmp.FirstOrDefault(x => x.Field<string>("WHID") == whid && x.Field<string>("ProductID") == prdID);

                if (item != null)  // มีสินค้า ID ใน แวนนี้  ทำการอัพเดท
                {
                    decimal RLCarQty = 0;
                    decimal RLPckQty = 0;
                    
                    if (!string.IsNullOrEmpty(RLCarQty_cell) && !string.IsNullOrEmpty(RLPckQty_cell))
                    {
                        RLCarQty = Convert.ToDecimal(RLCarQty_cell); //เบิกใหญ่
                        RLPckQty = Convert.ToDecimal(RLPckQty_cell); //เบิกเล็ก

                        if (e.ColumnIndex == 6)
                            RLPckQty = 0;
                        if (e.ColumnIndex == 7)
                            RLCarQty = 0;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(RLCarQty_cell))
                        {
                            grdRL.CurrentRow.Cells["colRLCarQty"].Value = 0;
                        }
                        if (string.IsNullOrEmpty(RLPckQty_cell))
                        {
                            grdRL.CurrentRow.Cells["colRLPckQty"].Value = 0;
                        }
                    }

                    foreach (DataRow r in dtRLSummary.Rows)
                    {
                        if (r["WHID"].ToString() == whid && r["ProductID"].ToString() == prdID)
                        {
                            r["RLPckQty"] = RLPckQty;// เบิกเล็ก
                            r["RLCarQty"] = RLCarQty;// เบิกใหญ่
                        }
                    }
                }

                if (rlList.Any(x => x.Key == whid))
                    rlList.Remove(whid);

                rlList.Add(whid, dtRLSummary);
            }
        }

        private void grdRL_KeyPress(object sender, KeyPressEventArgs e)
        {
            int[] numberCell = new int[] { 6, 7 };
            grdRL.SetCellNumberOnly(sender, e, numberCell.ToList());
        }

        private void grdRL_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;

            tb.KeyPress -= grdRL_KeyPress;
            tb.KeyPress += grdRL_KeyPress;
        }

        #endregion

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            FormHelper.ShowPrintingReportName = true; //edit by sailom .k 07/01/2022

            var _tbl_PRMasters = new List<tbl_PRMaster>();
            for (int i = 0; i < grdVan.Rows.Count; i++)
            {
                string whid = grdVan.Rows[i].Cells["colWHID"].Value.ToString();

                Func<tbl_PRMaster, bool> func = (x => x.DocDate.ToDateTimeFormatString() == dtpDocDate.Value.ToDateTimeFormatString()
                    && x.DocTypeCode == "RL" && x.DocStatus == "4" && x.ToWHID == whid && (x.DocNo.Length > 12 || x.DocNo.Contains("V")));

                _tbl_PRMasters.AddRange(bu.GetPRMaster("RL", func));

            }

            if (_tbl_PRMasters != null && _tbl_PRMasters.Count > 0)
            {
                foreach (var item in _tbl_PRMasters)
                {
                    Dictionary<string, object> _params = new Dictionary<string, object>();
                    _params.Add("@DocNo", item.DocNo);
                    this.OpenCrystalReportsPopup("ใบโอนย้ายสินค้า", "Form_RL.rpt", "Form_RL", _params);

                    Cursor.Current = Cursors.Default;
                }
            }
            else
            {
                Cursor.Current = Cursors.Default;
                string msg = "ไม่พบเอกสารที่สามารถพิมพ์ได้!!!";
                msg.ShowInfoMessage();
            }
        }

        private void grdRL_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //var grd = sender as DataGridView;
            //if (e.ColumnIndex == 6) //Car
            //{
            //    grdRL.Rows[e.RowIndex].Cells[7].ReadOnly = true;
            //}
            //else if (e.ColumnIndex == 7) //Pack
            //{
            //    grdRL.Rows[e.RowIndex].Cells[6].ReadOnly = true;
            //}
        }
    }
}
