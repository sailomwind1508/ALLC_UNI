﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Model
{
    public class CustomerInfo
    {
        public bool Choose { get; set; }
        public string CustomerID { get; set; }
        public string CustName { get; set; }
        public string BillTo { get; set; }
        public string ShopTypeName { get; set; }
        public short Seq { get; set; }
    }
}