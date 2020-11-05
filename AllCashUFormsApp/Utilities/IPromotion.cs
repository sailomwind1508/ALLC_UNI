using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp
{
    interface IPromotion
    {
        List<T> GetData<T>(List<T> obj, object condition);
    }
}
