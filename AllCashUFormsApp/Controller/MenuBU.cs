using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class MenuBU
    {
        public List<tbl_MstMenu> GetAllData()
        {
            return (new tbl_MstMenu()).SelectAll();
        }

        public int AddData(tbl_MstMenu tbl_MstMenu)
        {
            return tbl_MstMenu.Insert();
        }

        public int UpdateData(tbl_MstMenu tbl_MstMenu)
        {
            return tbl_MstMenu.Update();
        }

        public int RemoveData(tbl_MstMenu tbl_MstMenu)
        {
            return tbl_MstMenu.Delete();
        }
    }
}
