using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp
{
    public interface IPRDetailDao
    {
        string BulkSave(List<tbl_PRDetail> otbl_PRDetail);
        string BulkUpdate(List<tbl_PRDetail> otbl_PRDetail);
        string BulkDelete(List<tbl_PRDetail> otbl_PRDetail);
        string BulkMerge(List<tbl_PRDetail> otbl_PRDetail);
    }
}
