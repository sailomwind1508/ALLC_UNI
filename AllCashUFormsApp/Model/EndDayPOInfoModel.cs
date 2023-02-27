using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Model
{
    public class EndDayPOInfoModel
    {
        public string DocNo { get; set; }
        public string WHID { get; set; }
        public string CustomerID { get; set; }
        public string CustName { get; set; }
        public decimal BeforeVat { get; set; }
        public decimal VatAmt { get; set; }
        public decimal TotalDue { get; set; }
        public string CustInvNo { get; set; }
    }
}
