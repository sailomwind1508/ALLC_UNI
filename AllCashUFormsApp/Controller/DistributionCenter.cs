using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace AllCashUFormsApp.Controller
{
    public class DistributionCenter : BaseControl, IObject
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

        public DistributionCenter() : base("")
        {
            _docTypePredicate = (x => x.DocTypeCode == "");
        }

        public virtual DataTable GetDataTable(string whid, string prdGroupID, string prdSubGroupID, List<string> prdCodeList, DateTime fDate, DateTime tDate)
        {
            return null;
        }

        public virtual DataTable GetDataTableByCondition(string[] filters)
        {
            DataTable dt = new DataTable();

            if (filters != null)
            {

            }
            else
            {
                dt = GetDataTable();
            }

            return dt;
        }

        public DataTable GetDataTable(bool isPopup = true)
        {
            return null;
        }

        #region tabDepo

        public DataTable GetDepoTable(Func<tbl_Branch, bool> func)
        {
            try
            {
                List<tbl_Branch> tbl_Branchs = new List<tbl_Branch>();
                //if (!string.IsNullOrEmpty(text))
                //{
                //    Func<tbl_Branch, bool> tbl_BranchFunc = (x => x.BranchCode.Contains(text) || x.BranchName.Contains(text));
                //    tbl_Branchs = (new tbl_Branch()).Select(tbl_BranchFunc);
                //}
                //else
                tbl_Branchs = (new tbl_Branch()).Select(func);

                List<tbl_MstProvince> tbl_MstProvinces = new List<tbl_MstProvince>();
                Func<tbl_MstProvince, bool> tbl_MstProvinceFunc = (x => x.ProvinceID == tbl_Branchs[0].ProvinceID);
                tbl_MstProvinces = (new tbl_MstProvince()).Select(tbl_MstProvinceFunc);

                List<tbl_MstDistrict> tbl_MstDistricts = new List<tbl_MstDistrict>();
                Func<tbl_MstDistrict, bool> tbl_MstDistrictFunc = (x => x.DistrictID == tbl_Branchs[0].DistrictID);
                tbl_MstDistricts = (new tbl_MstDistrict()).Select(tbl_MstDistrictFunc);

                List<tbl_MstArea> tbl_MstAreas = new List<tbl_MstArea>();
                Func<tbl_MstArea, bool> tbl_MstAreaFunc = (x => x.AreaID == tbl_Branchs[0].AreaID);
                tbl_MstAreas = (new tbl_MstArea()).Select(tbl_MstAreaFunc);

                List<tbl_PriceGroup> tbl_PriceGroups = new List<tbl_PriceGroup>();
                Func<tbl_PriceGroup, bool> tbl_PriceGroupFunc = (x => x.PriceGroupID == tbl_Branchs[0].PriceGroupID);
                tbl_PriceGroups = (new tbl_PriceGroup()).Select(tbl_PriceGroupFunc);

                var query = from b in tbl_Branchs
                            join p in tbl_MstProvinces on b.ProvinceID equals p.ProvinceID
                            join d in tbl_MstDistricts on b.DistrictID equals d.DistrictID
                            join a in tbl_MstAreas on b.AreaID equals a.AreaID
                            join pr in tbl_PriceGroups on b.PriceGroupID equals pr.PriceGroupID
                            select new
                            {
                                BranchCode = b.BranchCode,
                                BranchName = b.BranchName,
                                BranchRefCode = b.BranchRefCode,
                                ProvinceName = p.ProvinceName,
                                DistrictName = d.DistrictName,
                                AreaName = a.AreaName,
                                PriceGroupName = pr.PriceGroupName,
                                Remark = b.Remark

                            };

                DataTable _dt = new DataTable("DepoTable");
                _dt.Columns.Add("รหัสเดโป้", typeof(string));
                _dt.Columns.Add("ชื่อเดโป้", typeof(string));
                _dt.Columns.Add("รหัส SAP", typeof(string));
                _dt.Columns.Add("จังหวัด", typeof(string));
                _dt.Columns.Add("เขต", typeof(string));
                _dt.Columns.Add("แขวง", typeof(string));
                _dt.Columns.Add("กลุ่มราคา", typeof(string));
                _dt.Columns.Add("หมายเหตุ", typeof(string));

                var data = query.ToList();
                foreach (var r in data)
                {
                    _dt.Rows.Add(r.BranchCode, r.BranchName, r.BranchRefCode, r.ProvinceName, r.DistrictName, r.AreaName, r.PriceGroupName, r.Remark);
                }

                return _dt;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        #endregion

        #region tabEmp

        public List<BranchEmployeeModel> GetEmpDetails(Func<tbl_Employee, bool> func)
        {
            try
            {
                List<tbl_Employee> tbl_Employees = new List<tbl_Employee>();
                //if (!string.IsNullOrEmpty(text))
                //{
                //    Func<tbl_Employee, bool> tbl_EmployeeFunc = (x => x.EmpCode.Contains(text) || x.TitleName.Contains(text) || x.FirstName.Contains(text) || x.LastName.Contains(text));
                //    tbl_Employees = (new tbl_Employee()).Select(tbl_EmployeeFunc);
                //}
                //else
                tbl_Employees = (new tbl_Employee()).Select(func);

                List<tbl_Users> tbl_UsersList = new List<tbl_Users>();
                Func<tbl_Users, bool> tbl_UsersListFunc = (x => tbl_Employees.Select(a => a.EmpID).Contains(x.EmpID));
                tbl_UsersList = new tbl_Users().Select(tbl_UsersListFunc);

                List<tbl_Roles> tbl_RolesList = new List<tbl_Roles>();
                Func<tbl_Roles, bool> tbl_RolesListFunc = (x => tbl_UsersList.Select(a => a.RoleID).Contains(x.RoleID));
                tbl_RolesList = new tbl_Roles().Select(tbl_RolesListFunc);

                List<tbl_Position> tbl_PositionList = new List<tbl_Position>();
                Func<tbl_Position, bool> tbl_PositionFunc = (x => tbl_Employees.Select(a => a.PositionID).Contains(x.PositionID));
                tbl_PositionList = new tbl_Position().Select(tbl_PositionFunc);

                List<tbl_Department> tbl_Departments = new List<tbl_Department>();
                Func<tbl_Department, bool> tbl_DepartmentFunc = (x => tbl_Employees.Select(a => a.DepartmentID).Contains(x.DepartmentID));
                tbl_Departments = (new tbl_Department()).Select(tbl_DepartmentFunc);

                var query = from e in tbl_Employees
                            join u in tbl_UsersList on e.EmpID equals u.EmpID
                            join r in tbl_RolesList on u.RoleID equals r.RoleID
                            join p in tbl_PositionList on e.PositionID equals p.PositionID
                            join dp in tbl_Departments on e.DepartmentID equals dp.DepartmentID
                            select new
                            {
                                EmpCode = e.EmpCode,
                                FullName = string.Join(" ", e.TitleName, e.FirstName, e.LastName),
                                DepartmentName = dp.DepartmentName,
                                DepartmentID = e.DepartmentID,
                                PositionName = p.PositionName,
                                PositionID = e.PositionID,
                                MgrName = string.Empty,
                                Mobile = e.Mobile,
                                CrDate = e.CrDate != null ? e.CrDate.ToDateTimeFormatString().ToString() : "",
                                CrUser = e.CrUser,
                                EdDate = e.EdDate != null && e.EdDate.Value != null ? e.EdDate.Value.ToDateTimeFormatString().ToString() : "",
                                EdUser = e.EdUser,
                                FlagDel = e.FlagDel,

                                EmpIDCard = e.EmpIDCard,
                                IDCard = e.IDCard,
                                TitleName = e.TitleName,
                                FirstName = e.FirstName,
                                LastName = e.LastName,

                                Username = u.Username,
                                Password = u.Password,
                                RoleID = u.RoleID,
                                EmpID = e.EmpID,
                            };

                List<BranchEmployeeModel> branchEmployeeModelList = new List<BranchEmployeeModel>();
                foreach (var e in query.ToList())
                {
                    BranchEmployeeModel branchEmployeeModel = new BranchEmployeeModel()
                    {
                        EmpCode = e.EmpCode,
                        FullName = e.FullName,
                        DepartmentName = e.DepartmentName,
                        DepartmentID = e.DepartmentID,
                        PositionName = e.PositionName,
                        PositionID = e.PositionID,
                        MgrName = e.MgrName,
                        Mobile = e.Mobile,
                        CrDate = e.CrDate,
                        CrUser = e.CrUser,
                        EdDate = e.EdDate,
                        EdUser = e.EdUser,
                        FlagDel = e.FlagDel,

                        Emp_ID_Card = e.EmpIDCard,
                        IDCard = e.IDCard,
                        TitleName = e.TitleName,
                        FirstName = e.FirstName,
                        LastName = e.LastName,

                        Username = e.Username,
                        Password = e.Password,
                        RoleID = e.RoleID,
                        EmpID = e.EmpID,
                    };

                    branchEmployeeModelList.Add(branchEmployeeModel);
                }

                return branchEmployeeModelList;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable GetEmpTable(Func<tbl_Employee, bool> func)
        {
            try
            {
                DataTable query = GetEmpDetails(func).ToDataTable();

                DataTable _dt = new DataTable("DepoTable");
                _dt.Columns.Add("รหัสพนักงาน", typeof(string));
                _dt.Columns.Add("ชื่อ-นามสกุล", typeof(string));
                _dt.Columns.Add("แผนก", typeof(string));
                _dt.Columns.Add("ตำแหน่ง", typeof(string));
                _dt.Columns.Add("ชื่อหัวหน้า", typeof(string));
                _dt.Columns.Add("เบอร์โทร", typeof(string));
                _dt.Columns.Add("วันที่เพิ่ม", typeof(string));
                _dt.Columns.Add("เพิ่มโดย", typeof(string));
                _dt.Columns.Add("วันที่แก้ไข", typeof(string));
                _dt.Columns.Add("แก้ไขโดย", typeof(string));
                _dt.Columns.Add("ยกเลิก", typeof(bool));

                for (int i = 0; i < query.Rows.Count; i++)
                {
                    _dt.Rows.Add(query.Rows[i]["EmpCode"], query.Rows[i]["FullName"], query.Rows[i]["DepartmentName"], query.Rows[i]["PositionName"], query.Rows[i]["MgrName"],
                        query.Rows[i]["Mobile"], query.Rows[i]["CrDate"], query.Rows[i]["CrUser"],
                        query.Rows[i]["EdDate"], query.Rows[i]["EdUser"], query.Rows[i]["FlagDel"]);
                }

                return _dt;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        #endregion

        #region tabWarehouse

        public DataTable GetBWHTable(Func<tbl_BranchWarehouse, bool> condition)
        {
            try
            {
                //List<string> bwhList = new List<string>() { "1000", "1800", "1900" };
                var query = new tbl_BranchWarehouse().SelectAll().Where(x => x.VanType == 0).Where(condition).ToList();

                DataTable _dt = new DataTable("BWHTable");
                _dt.Columns.Add("ลำดับ", typeof(string));
                _dt.Columns.Add("รหัสคลัง", typeof(string));
                _dt.Columns.Add("ชื่อคลัง", typeof(string));

                foreach (var item in query)
                {
                    _dt.Rows.Add(item.WHSeq, item.WHCode, item.WHName);
                }

                return _dt;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        #endregion

        #region tabVan

        public DataTable GetVanTable(Func<tbl_BranchWarehouse, bool> condition, int vanType)
        {
            try
            {
                List<tbl_BranchWarehouse> tbl_BranchWarehouseList = new List<tbl_BranchWarehouse>();
                if (vanType != -1)
                    tbl_BranchWarehouseList = new tbl_BranchWarehouse().SelectAll().Where(x => x.VanType != 0 && x.VanType == vanType).Where(condition).ToList();
                else
                    tbl_BranchWarehouseList = new tbl_BranchWarehouse().SelectAll().Where(x => x.VanType != 0).Where(condition).ToList();

                List<tbl_VanType> tbl_VanTypeList = new List<tbl_VanType>();
                Func<tbl_VanType, bool> tbl_VanTypeFunc = (x => tbl_BranchWarehouseList.Select(a => a.WHType).Contains(x.WHType));
                tbl_VanTypeList = new tbl_VanType().Select(tbl_VanTypeFunc);

                List<tbl_Employee> tbl_Employees = new List<tbl_Employee>();
                Func<tbl_Employee, bool> tbl_EmployeesFunc = (x => tbl_BranchWarehouseList.Select(a => a.SaleEmpID).Contains(x.EmpID));
                tbl_Employees = (new tbl_Employee()).Select(tbl_EmployeesFunc);

                var query = from bwh in tbl_BranchWarehouseList
                            //join v in tbl_VanTypeList on bwh.WHType equals v.AutoID
                            join e in tbl_Employees on bwh.SaleEmpID equals e.EmpID
                            select new
                            {
                                WHCode = bwh.WHCode,
                                WHName = bwh.WHName,
                                SaleEmpID = bwh.SaleEmpID,
                                SaleEmpName = string.Join(" ", e.TitleName, e.FirstName, e.LastName),
                                //WHType = bwh.WHType,
                                Name = GetVanDetail(tbl_VanTypeList, bwh.WHType, bwh.VanType).Name,
                                DriverEmpID = bwh.DriverEmpID,
                                DriverEmpName = GetEmpDetail(tbl_Employees, bwh.DriverEmpID),
                                HelperEmpID = bwh.HelperEmpID,
                                HelperEmpName = GetEmpDetail(tbl_Employees, bwh.HelperEmpID),
                                License = bwh.License,
                                POSNo = bwh.POSNo
                            };

                DataTable _dt = new DataTable("VanTable");
                _dt.Columns.Add("รหัสแวน", typeof(string));
                _dt.Columns.Add("ชื่อแวน", typeof(string));
                _dt.Columns.Add("ประเภทแวน", typeof(string));
                _dt.Columns.Add("รหัสพนักงานขาย", typeof(string));
                _dt.Columns.Add("พนักงานขาย", typeof(string));
                //_dt.Columns.Add("", typeof(string));
                _dt.Columns.Add("รหัสพนักงานขับรถ", typeof(string));
                _dt.Columns.Add("พนักงานขับรถ", typeof(string));
                //_dt.Columns.Add("", typeof(string));
                _dt.Columns.Add("รหัสพนักงานผู้ช่วย", typeof(string));
                _dt.Columns.Add("พนักงานผู้ช่วย", typeof(string));
                //_dt.Columns.Add("", typeof(string));
                _dt.Columns.Add("เลขทะเบียน", typeof(string));
                _dt.Columns.Add("เลข POS", typeof(string));

                foreach (var item in query)
                {
                    _dt.Rows.Add(item.WHCode, item.WHName, item.Name, item.SaleEmpID, item.SaleEmpName, item.DriverEmpID, item.DriverEmpName,
                        item.HelperEmpID, item.HelperEmpName, item.License, item.POSNo);

                    //_dt.Rows.Add(item.WHCode, item.WHName, item.Name, item.SaleEmpID, item.SaleEmpName, "", item.DriverEmpID, item.DriverEmpName,
                    //    "", item.HelperEmpID, item.HelperEmpName, "", item.License, item.POSNo);
                }

                return _dt;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        private string GetEmpDetail(List<tbl_Employee> tbl_Employees, string empID)
        {
            string ret = "";

            var e = tbl_Employees.FirstOrDefault(x => x.EmpID == empID);

            if (e != null)
                ret = string.Join(" ", e.TitleName, e.FirstName, e.LastName);
           
            return ret;
        }

        public tbl_VanType GetVanDetail(List<tbl_VanType> tbl_VanTypeList, int whType, int vanType)
        {
            tbl_VanType ret = new tbl_VanType();

            var wh = tbl_VanTypeList.FirstOrDefault(x => x.AutoID == vanType && x.WHType == whType);

            if (wh != null)
                ret = wh;
            else
                ret = null;

            return ret;
        }

        #endregion

        #region tabMarket

        public DataTable GetMKTTable(Func<tbl_SalArea, bool> condition)
        {
            try
            {
                List<tbl_SalArea> tbl_SalAreaList = new List<tbl_SalArea>();
                tbl_SalAreaList = new tbl_SalArea().Select(condition);

                List<tbl_ArCustomer> tbl_ArCustomerList = new List<tbl_ArCustomer>();
                Func<tbl_ArCustomer, bool> tbl_ArCustomerFunc = (x => tbl_SalAreaList.Select(a => a.SalAreaID).Contains(x.SalAreaID));
                tbl_ArCustomerList = new tbl_ArCustomer().SelectAll().Where(tbl_ArCustomerFunc).ToList();

                List<tbl_Zone> tbl_ZoneList = new List<tbl_Zone>();
                tbl_ZoneList = new tbl_Zone().SelectAll();

                var query = from sa in tbl_SalAreaList
                            select new
                            {
                                SalAreaID = sa.SalAreaID,
                                SalAreaCode = sa.SalAreaCode,
                                SalAreaName = sa.SalAreaName,
                                Seq = sa.Seq,
                                ZoneName = GetZoneDetail(tbl_ZoneList, sa.ZoneID),
                                CountCustomer = tbl_ArCustomerList.Count(x => x.SalAreaID == sa.SalAreaID)
                            };

                DataTable _dt = new DataTable("VanTable");
                _dt.Columns.Add("รหัสตลาด", typeof(string));
                _dt.Columns.Add("รหัสลำดับ", typeof(string));
                _dt.Columns.Add("ชื่อตลาด", typeof(string));
                _dt.Columns.Add("ลำดับ", typeof(string));
                _dt.Columns.Add("ประเภท", typeof(string));
                _dt.Columns.Add("จำนวนลูกค้า", typeof(string));

                foreach (var item in query)
                {
                    _dt.Rows.Add(item.SalAreaID, item.SalAreaCode, item.SalAreaName, item.Seq, item.ZoneName, item.CountCustomer);
                }

                return _dt;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        private string GetZoneDetail(List<tbl_Zone> tbl_Zones, int zoneID)
        {
            string ret = "";

            var z = tbl_Zones.FirstOrDefault(x => x.ZoneID == zoneID);

            if (z != null)
                ret = z.ZoneName;

            return ret;
        }

        #endregion

    }
}
