﻿using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace AllCashUFormsApp.View.Page
{
    public partial class frmSearchAddress : Form
    {
        Province bu = new Province();
        public frmSearchAddress()
        {
            InitializeComponent();
        }
        private void cbbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbbArea.SelectedIndex != 0)
            {
                CbbAreaToCbbDistrict(cbbArea,cbbDistrict);
            }
        }
        private void CbbAreaToCbbDistrict(ComboBox cbb_Area, ComboBox cbb_District)
        {
            int AreaID = Convert.ToInt32(cbb_Area.SelectedValue);
            var districtList = new List<tbl_MstDistrict>();
            districtList.AddRange(bu.GetMstDistrict(x => x.AreaID == AreaID && x.FlagDel == false));
            cbb_District.BindDropdownList(districtList, "DistrictName", "DistrictID");
        }
        private void cbbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbProvince.SelectedIndex >= 0 && cbbArea.SelectedIndex >= 0)
            {
                string Province2 = cbbProvince.SelectedValue.ToString();
                int ProvinceID = Convert.ToInt32(cbbProvince.SelectedValue);
                var AreaList = new List<tbl_MstArea>();
                AreaList.AddRange(bu.GetMstArea(x => x.ProvinceID == ProvinceID && x.FlagDel == false));
                cbbArea.BindDropdownList(AreaList, "AreaName", "AreaID");

                if (cbbArea.SelectedIndex == 0)
                {
                    CbbAreaToCbbDistrict(cbbArea, cbbDistrict);
                }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void PrePareSendDataAdress(string DistrictCode,int AreaID,string areaName,int DistrictID,string districtName,int ProvinceID,string provinceName,string AddressNo,string Street,string lane,string PostalCode)
        {
            string BillTo = "";

            if (!string.IsNullOrEmpty(txtAddressNo.Text))
            {
                BillTo += AddressNo;
            }
            if (!string.IsNullOrEmpty(txtLane.Text))
            {
                BillTo += " ซ." + lane;
            }
            if (!string.IsNullOrEmpty(txtStreet.Text))
            {
                BillTo += " ถ." + Street;
            }
            if (!string.IsNullOrEmpty(cbbDistrict.Text))
            {
                BillTo += " ต." + districtName;
            }
            if (!string.IsNullOrEmpty(cbbArea.Text))
            {
                BillTo += " อ." + areaName;
            }
            if (!string.IsNullOrEmpty(cbbProvince.Text))
            {
                BillTo += " จ." + provinceName;
            }
            if (!string.IsNullOrEmpty(txtPostalCode.Text))
            {
                BillTo += " " + PostalCode;
            }

            if (frmCustomerInfo.SAM.Page == "Customer")
            {
                frmCustomerInfo.SAM.DistrictCode = DistrictCode;
                frmCustomerInfo.SAM.AreaID = AreaID;
                frmCustomerInfo.SAM.areaName = areaName;
                frmCustomerInfo.SAM.DistrictID = DistrictID;
                frmCustomerInfo.SAM.districtName = districtName;
                frmCustomerInfo.SAM.ProvinceID = ProvinceID;
                frmCustomerInfo.SAM.provinceName = provinceName;
                frmCustomerInfo.SAM.AddressNo = AddressNo;
                frmCustomerInfo.SAM.Street = Street;
                frmCustomerInfo.SAM.lane = lane;
                frmCustomerInfo.SAM.PostalCode = PostalCode;
                frmCustomerInfo.SAM.billTo = BillTo;
            }
            else if (frmSupplierInfo.SAM.Page == "Supplier")
            {
                frmSupplierInfo.SAM.DistrictCode = DistrictCode;
                frmSupplierInfo.SAM.AreaID = AreaID;
                frmSupplierInfo.SAM.areaName = areaName;
                frmSupplierInfo.SAM.DistrictID = DistrictID;
                frmSupplierInfo.SAM.districtName = districtName;
                frmSupplierInfo.SAM.ProvinceID = ProvinceID;
                frmSupplierInfo.SAM.provinceName = provinceName;
                frmSupplierInfo.SAM.AddressNo = AddressNo;
                frmSupplierInfo.SAM.Street = Street;
                frmSupplierInfo.SAM.lane = lane;
                frmSupplierInfo.SAM.PostalCode = PostalCode;
                frmSupplierInfo.SAM.billTo = BillTo;
            }
        }
        private void SendDataAddress()
        {
            try
            {
                int DistrictID = Convert.ToInt32(cbbDistrict.SelectedValue);//

                List<tbl_MstDistrict> districtList = bu.GetMstDistrict(x => x.DistrictID == DistrictID);

                if (!string.IsNullOrEmpty(txtAddressNo.Text) && !string.IsNullOrEmpty(cbbProvince.Text) && !string.IsNullOrEmpty(cbbDistrict.Text))
                {
                    string DistrictCode = districtList[0].DistrictCode;
                    int AreaID = Convert.ToInt32(cbbArea.SelectedValue);
                    string areaName = cbbArea.Text;
                    string districtName = cbbDistrict.Text;
                    int ProvinceID = Convert.ToInt32(cbbProvince.SelectedValue);
                    string provinceName = cbbProvince.Text;

                    string allAddress = txtAddressNo.Text;
                    string filter = "";

                    var chk = txtAddressNo.Text.ToCharArray();

                    string AddressNo = txtAddressNo.Text;
                    string Street = txtStreet.Text;
                    string lane = txtLane.Text;
                    string PostalCode = txtPostalCode.Text;

                    PrePareSendDataAdress(DistrictCode, AreaID, areaName, DistrictID, districtName, ProvinceID, provinceName, AddressNo, Street, lane, PostalCode);

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                return;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SendDataAddress();
        }
        private void PrePare_CbbProvince(ComboBox cbb)
        {
            var ProvinceList = new List<tbl_MstProvince>();
            ProvinceList.AddRange(bu.GetMstProvince(x=>x.FlagDel == false));
            cbb.BindDropdownList(ProvinceList, "ProvinceName", "ProvinceID");
        }
        private void SelectIndexEqualsZero()
        {
            int ProvinceID = Convert.ToInt32(cbbProvince.SelectedValue);//
            var AreaList = new List<tbl_MstArea>();
            AreaList.AddRange(bu.GetMstArea(x => x.ProvinceID == ProvinceID && x.FlagDel == false));
            cbbArea.BindDropdownList(AreaList, "AreaName", "AreaID");

            if (cbbArea.SelectedIndex == 0)
            {
                int AreaID = Convert.ToInt32(cbbArea.SelectedValue); //
                var districtList = new List<tbl_MstDistrict>();
                districtList.AddRange(bu.GetMstDistrict(x => x.AreaID == AreaID && x.FlagDel == false));
                cbbDistrict.BindDropdownList(districtList, "DistrictName", "DistrictID");
            }
        }
        private void SendDataToCombobox(int ProvinceID,int AreaID,int DistrictID, string AddressNo,string Street,string lane,string PostalCode)
        {
            cbbProvince.SelectedValue = ProvinceID;//
            var AreaList = new List<tbl_MstArea>();
            AreaList.AddRange(bu.GetMstArea(x => x.ProvinceID == ProvinceID && x.FlagDel == false));//
            cbbArea.BindDropdownList(AreaList, "AreaName", "AreaID");

            if (cbbArea.SelectedIndex == 0)
            {
                cbbArea.SelectedValue = AreaID;//
                var districtList = new List<tbl_MstDistrict>();
                districtList.AddRange(bu.GetMstDistrict(x => x.AreaID == AreaID && x.FlagDel == false)); //
                cbbDistrict.BindDropdownList(districtList, "DistrictName", "DistrictID");
                cbbDistrict.SelectedValue = DistrictID;
            }

            txtAddressNo.Text = AddressNo;
            txtStreet.Text = Street;
            txtLane.Text = lane;
            txtPostalCode.Text = PostalCode;
        }
        private void frmSearchAddress_Load(object sender, EventArgs e)
        {
            PrePare_CbbProvince(cbbProvince);

            if (cbbProvince.SelectedIndex == 0)
            {
                if (frmCustomerInfo.SAM.ProvinceID == 0 && frmCustomerInfo.SAM.Page == "Customer" || frmSupplierInfo.SAM.ProvinceID == 0 && frmSupplierInfo.SAM.Page == "Supplier")
                {
                    SelectIndexEqualsZero();
                }
                else if (frmCustomerInfo.SAM.ProvinceID > 0 && frmCustomerInfo.SAM.Page == "Customer")
                {
                    int ProvinceID = frmCustomerInfo.SAM.ProvinceID;
                    int AreaID = frmCustomerInfo.SAM.AreaID;
                    int DistrictID = frmCustomerInfo.SAM.DistrictID;
                    string AddressNo = frmCustomerInfo.SAM.AddressNo;
                    string Street = frmCustomerInfo.SAM.Street;
                    string lane = frmCustomerInfo.SAM.lane;
                    string PostalCode = frmCustomerInfo.SAM.PostalCode;
                    SendDataToCombobox(ProvinceID, AreaID, DistrictID,AddressNo , Street, lane,PostalCode);
                }
                else if (frmSupplierInfo.SAM.ProvinceID > 0 && frmSupplierInfo.SAM.Page == "Supplier")
                {
                    int ProvinceID = frmSupplierInfo.SAM.ProvinceID;
                    int AreaID = frmSupplierInfo.SAM.AreaID;
                    int DistrictID = frmSupplierInfo.SAM.DistrictID;
                    string AddressNo = frmSupplierInfo.SAM.AddressNo;
                    string Street = frmSupplierInfo.SAM.Street;
                    string lane = frmSupplierInfo.SAM.lane;
                    string PostalCode = frmSupplierInfo.SAM.PostalCode;
                    SendDataToCombobox(ProvinceID, AreaID, DistrictID, AddressNo, Street, lane, PostalCode);
                }
            }
        }
        private void cbbDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
 
