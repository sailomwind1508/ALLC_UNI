using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp
{
    interface IObject
    {
        DataTable GetDataTable(bool isPopup = true);

        DataTable GetDataTableByCondition(string[] filters);
    }
}
