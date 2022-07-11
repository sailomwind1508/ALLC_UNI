﻿using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.View.Page
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class frmCustomerMap : Form
    {
        private string htmlPath = ConfigurationManager.AppSettings["LocalPath"];
        SaleAreaDistrict bu = new SaleAreaDistrict();
        Customer buCust = new Customer();
        SendData buData = new SendData();
        private static string URLPathImage = "";

        public frmCustomerMap()
        {
            InitializeComponent();

            //string ServerName = Connection.ConnectionString;
            //string imgPathTmp = ServerName.Split('=')[1].Split(',')[0].ToString();
            //imgPathTmp = @"http://" + imgPathTmp + @":82/CU";
            //if (bu.tbl_Branchs[0].BranchID == "104") //เป็นศูนย์ที่ใช้ IP
            //{
            //    imgPathTmp = ServerName.Split('=')[1].Split(';')[0].ToString();
            //    imgPathTmp = @"http://" + imgPathTmp + @"/CU";
            //}
            //URLPathImage = imgPathTmp;


            try
            {
                string _BranchID = bu.tbl_Branchs[0].BranchID; //ADISORN 15/06-2022

                var _dt = buCust.GetServerImagePath(_BranchID);
                string _ServerNameFromCenter = "";
                string imgPathTmp = "";
                //last edit by sailom .k 02/07/2022-----------------------------------------------------
                var preOrderFlag = false;  //cash van
                var allbwh = bu.GetAllBranchWarehouse();
                if (allbwh.Count > 0)
                {
                    if (allbwh.Any(x => x.WHType == 2)) //pre-order
                    {
                        preOrderFlag = true;
                    }
                }

                if (_dt.Rows.Count > 0 && !preOrderFlag) //cash van
                {
                    _ServerNameFromCenter = _dt.Rows[0].Field<string>("ServerPath");
                    imgPathTmp = _ServerNameFromCenter.Split(',')[0].ToString();
                    imgPathTmp = @"http://" + imgPathTmp + @":82/CU";
                }

                if (preOrderFlag) //เป็นศูนย์ที่ใช้ IP //pre-order
                {
                    _ServerNameFromCenter = _dt.Rows[0].Field<string>("ServerPath");
                    imgPathTmp = _ServerNameFromCenter.Split(',')[0].ToString();
                    imgPathTmp = @"http://" + imgPathTmp + @"/CU";

                    if (_BranchID == "104")
                    {
                        string ServerName = Connection.ConnectionString;
                        imgPathTmp = ServerName.Split('=')[1].Split(';')[0].ToString();
                        imgPathTmp = @"http://" + imgPathTmp + @"/CU";
                    }
                }
                URLPathImage = imgPathTmp;
                //last edit by sailom .k 02/07/2022-----------------------------------------------------
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        #region Method
        private void InitPage()
        {
            var menu = bu.tbl_AdmFormList.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
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
        }

        private void InitialData()
        {
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnSave.Enabled = false;

            webBrowser1.AllowWebBrowserDrop = false;
            webBrowser1.IsWebBrowserContextMenuEnabled = false;
            webBrowser1.WebBrowserShortcutsEnabled = false;
            webBrowser1.ObjectForScripting = this;
            //string _path = "D:\\United Foods\\SC_05-05-2022\\ALLC_UNI\\AllCashUFormsApp\\Map.html";
            string path = Path.Combine(htmlPath, "Map.html");
            webBrowser1.Navigate(path);

            SetTreeView();
        }

        private void SetTreeView()
        {
            int xx = 0;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var dtCountCust = buCust.GetCountCustomer();
                var _sumCust = dtCountCust.Compute("SUM(CountCust)", "CountCust <> 0");

                treeView1.Nodes.Clear();
                var branch = bu.GetBranch();
                string _BranchName = branch[0].BranchName + " (" + _sumCust.ToString() + ")";
                treeView1.Nodes.Add(branch[0].BranchID, _BranchName);

                var dtVan = buData.GetWHID_FromSendData();
                for (int i = 0; i < dtVan.Rows.Count; i++)
                {
                    int index = treeView1.Nodes.IndexOfKey(dtVan.Rows[i].Field<string>("BranchID"));
                    object sumWHID = null;
                    string _WHID = dtVan.Rows[i].Field<string>("WHID");
                    var countCust = dtCountCust.AsEnumerable().Where(x => x.Field<string>("WHID") == _WHID).ToList();
                    if (countCust != null && countCust.Count > 0)
                    {
                        sumWHID = dtCountCust.Compute("SUM(CountCust)", "WHID = '" + _WHID + "'");
                    }
                    //var sumWHID = dtCountCust.Compute("SUM(CountCust)", "WHID = '"+_WHID + "'");
                    string _countWHID = "";
                    if (sumWHID != null)
                    {
                        _countWHID = dtVan.Rows[i].Field<string>("WHName") + " (" + Convert.ToInt32(sumWHID).ToString() + ")";
                    }

                    if (index != -1 && !string.IsNullOrEmpty(_countWHID)) //last edit by sailom .k 07/06/2022
                        treeView1.Nodes[index].Nodes.Add(_WHID, _countWHID);
                }

                var _dtSaleArea = bu.GetSalAreaDistrict_BySendData();


                for (int i = 0; i < _dtSaleArea.Rows.Count; i++)
                {
                    string _whid = _dtSaleArea.Rows[i].Field<string>("WHID");
                    int index = treeView1.Nodes[0].Nodes.IndexOfKey(_whid);
                    string _SalAreaID = _dtSaleArea.Rows[i].Field<string>("SalAreaID");
                    var dr = dtCountCust.AsEnumerable().FirstOrDefault(x => x.Field<string>("SalAreaID") == _SalAreaID);

                    string _SalAreaName = "";
                    if (dr != null)
                    {
                        _SalAreaName = _dtSaleArea.Rows[i].Field<string>("SalAreaName") + " (" + Convert.ToInt32(dr[0]).ToString() + ")";
                    }

                    xx = i;

                    if (index != -1 && !string.IsNullOrEmpty(_SalAreaID) && !string.IsNullOrEmpty(_SalAreaName)) //last edit by sailom .k 07/06/2022
                        treeView1.Nodes[0].Nodes[index].Nodes.Add(_SalAreaID, _SalAreaName);
                }

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                xx.ToString().ShowErrorMessage();
                ex.Message.ShowErrorMessage();
            }
        }

        private string GenerateLocationString(List<tbl_ArCustomer> list)
        {
            //List<string> listRow = new List<string>();

            //for (int i = 0; i < list.Count; i++)
            //{
            //    if (!string.IsNullOrEmpty(list[i].Latitude) && !string.IsNullOrEmpty(list[i].Longitude))
            //    {
            //        string _CustImage = "";
            //        //_CustImage = list[i].CustImage;
            //        if (!string.IsNullOrEmpty(list[i].CustImage) && list[i].CustImage.Contains("Images"))
            //        {
            //            list[i].CustImage = list[i].CustImage + " ";
            //            string CustImagePath = list[i].CustImage.Split('~')[1].Split(' ')[0].ToString();
            //            _CustImage = URLPathImage + CustImagePath;
            //        }

            //        var row = string.Join("|", new string[] { list[i].Latitude, list[i].Longitude, _CustImage, list[i].CustName, list[i].BillTo,  });
            //        listRow.Add(row);
            //    }
            //}

            //string sendData = string.Join(",", listRow);

            return "";
        }

        private string GenerateLocationString(DataTable dt)
        {
            List<string> listRow = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                if (!string.IsNullOrEmpty(row["Latitude"].ToString()) && !string.IsNullOrEmpty(row["Longitude"].ToString()))
                {
                    string _CustImage = "";
                    //_CustImage = list[i].CustImage;
                    if (!string.IsNullOrEmpty(row["CustImage"].ToString()) && row["CustImage"].ToString().Contains("Images"))
                    {
                        var custImageTmp = row["CustImage"].ToString() + " ";
                        string CustImagePath = custImageTmp.Split('~')[1].Split(' ')[0].ToString();
                        _CustImage = URLPathImage + CustImagePath;
                    }
                    else
                    {
                        _CustImage = "Images/no-photos.png";
                    }

                    string _Lat = row["Latitude"].ToString();
                    string _Long = row["Longitude"].ToString();

                    string _billTo = row["BillTo"].ToString().Replace("\r", "");
                    _billTo = row["BillTo"].ToString().Replace("\r\n", "");
                    if (listRow.FirstOrDefault(x => x.ToString().Contains(_Lat + "|" + _Long)) != null && listRow.Count > 0)
                    {

                    }
                    else
                    {
                        var _row = string.Join("|", new string[] { row["Latitude"].ToString(), row["Longitude"].ToString()
                        , _CustImage, row["CustName"].ToString(), _billTo, row["MarkerImage"].ToString()
                        , row["Telephone"].ToString() });
                        listRow.Add(_row);
                    }

                }
            }

            string sendData = string.Join(",", listRow);

            return sendData;
        }

        #endregion

        #region Event
        private void frmCustomerMap_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();
        }

        private void btnSearchMap_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var listWHID = new List<string>();
                var _listSalArea = new List<string>();

                for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
                {
                    //if (treeView1.Nodes[0].Nodes[i].Checked)
                    //{
                    //listWHID.Add(treeView1.Nodes[0].Nodes[i].Name);

                    for (int x = 0; x < treeView1.Nodes[0].Nodes[i].Nodes.Count; x++)
                    {
                        if (treeView1.Nodes[0].Nodes[i].Nodes[x].Checked)
                        {
                            listWHID.Add(treeView1.Nodes[0].Nodes[i].Name);
                            _listSalArea.Add(treeView1.Nodes[0].Nodes[i].Nodes[x].Name);
                        }
                    }
                    //}
                }

                var distinctWHID = listWHID.Distinct().ToList();
                var allWHID = string.Join(",", distinctWHID);
                var allSalAreaID = string.Join(",", _listSalArea);

                var dt = buCust.GetCustomerByWHID_DataTable(allWHID, allSalAreaID);
                //var list = buCust.GetCustomerByWHID(allWHID);//

                //var list2 = new List<tbl_ArCustomer>();
                //list2.Add(new tbl_ArCustomer 
                //{ Latitude = "15.2792792792793"
                //    , Longitude = "104.840971637079"
                //    , CustImage = "http://UBN.DNSDOJO.NET:82/CU/Images/V01/4123301011577.jpg"
                //    , CustName = "ศรายุทธ การค้า"
                //    , BillTo = "91 ม.6 ต.หัวเรือ อ.เมืองอุบลราชธานี จ.อุบลราชธานี" 
                //});

                string sendData = GenerateLocationString(dt);
                object[] args = { sendData };
                webBrowser1.Document.InvokeScript("GenLocation", args);

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                foreach (TreeNode tn in e.Node.Nodes)
                {
                    tn.Checked = e.Node.Checked;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCustomerMap_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}