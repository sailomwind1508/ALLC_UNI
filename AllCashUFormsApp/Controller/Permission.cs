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

namespace AllCashUFormsApp.Controller
{
    public class Permission : BaseControl
    {
        public Permission() : base("")
        {
            
        }

        public int UpdateAdmRoleControl(List<tbl_AdmRoleControl> objs)
        {
            List<int> ret = new List<int>();
            try
            {
                foreach (var item in objs)
                {
                    ret.Add(item.Update());
                }

            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return 0;
            }
            
            return ret.All(x => x == 1) ? 1 : 0;
        }

        public virtual DataTable GetDataTable(Func<tbl_AdmRoleControl, bool> func = null)
        {
            try
            {
                //List<tbl_AdmRoleControl> tbl_AdmRoleControls = new List<tbl_AdmRoleControl>();
                //tbl_AdmRoleControls = (new tbl_AdmRoleControl()).Select(func);

                //List<tbl_MstMenu> tbl_MstMenus = new List<tbl_MstMenu>();
                //tbl_MstMenus = (new tbl_MstMenu()).SelectAll().Where(x => tbl_AdmRoleControls.Select(a => a.ControlID).ToList().Contains(x.MenuID)).ToList();

                List<tbl_MstMenu> tbl_MstMenus = new List<tbl_MstMenu>();
                tbl_MstMenus = (new tbl_MstMenu()).SelectAll();

                List<tbl_AdmRoleControl> tbl_AdmRoleControls = new List<tbl_AdmRoleControl>();
                tbl_AdmRoleControls = (new tbl_AdmRoleControl()).Select(func);

                DataTable newTable = new DataTable();
                newTable.Columns.Add("RoleID", typeof(int));
                newTable.Columns.Add("ControlID", typeof(int));
                //newTable.Columns.Add("MenuImage", typeof(Image));
                newTable.Columns.Add("MenuName", typeof(string));
                newTable.Columns.Add("Visible", typeof(bool));
                newTable.Columns.Add("Enable", typeof(bool));
                newTable.Columns.Add("MenuText", typeof(string));

                foreach (var m in tbl_MstMenus)
                {
                    var roleC = tbl_AdmRoleControls.FirstOrDefault(x => x.ControlID == m.MenuID);
                    if (roleC != null)
                    {
                        newTable.Rows.Add(roleC.RoleID, roleC.ControlID, m.MenuName, roleC.Visible, roleC.Enable, m.MenuText);
                    }
                    else
                    {
                        if (newTable.Rows.Count > 0)
                        {
                            var row0 = newTable.Rows[0];
                            newTable.Rows.Add(row0["RoleID"], m.MenuID, m.MenuName, false, false, m.MenuText);
                        }
                    }
                }
                //foreach (var r in tbl_AdmRoleControls)
                //{
                //    var menu = tbl_MstMenus.FirstOrDefault(x => x.MenuID == r.ControlID);
                //    if (menu != null)
                //    {
                //        newTable.Rows.Add(r.RoleID, r.ControlID, menu.MenuName, r.Visible, r.Enable, menu.MenuText);
                //    }
                //}

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        public virtual DataTable GetDataTableByCondition(Func<tbl_AdmRoleControl, bool> func = null)
        {
            DataTable dt = new DataTable();

            if (func != null)
            {
                dt = GetDataTable(func);
            }
            else
            {
                dt = GetDataTable();
            }

            return dt;
        }

        public List<tbl_AdmRoleControl> VerifyPermission(int roleID)
        {
            List<tbl_AdmRoleControl> ret = new List<tbl_AdmRoleControl>();
            try
            {
                var tbl_AdmRoleControls = new List<tbl_AdmRoleControl>();
                tbl_AdmRoleControls = (new tbl_AdmRoleControl()).Select(x => x.RoleID == roleID);

                ret = tbl_AdmRoleControls;

                return ret;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return new List<tbl_AdmRoleControl>();
            }
        }
    }
}
