using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp
{
    public class DataGridColumn
    {
        public string DataPropertyName { get; set; }
        public string Name { get; set; }
        public string HeaderText { get; set; }
        public int Width { get; set; }
        public DataGridViewAutoSizeColumnMode AutoSizeColumnMode { get; set; }
        public DataGridViewContentAlignment Alignment { get; set; }
        public string Format { get; set; }
        public DataGridViewColumn ColoumnType { get; set; }
        public bool AddNumberInFirstRow { get; set; }
        public bool AddSearchAddOn { get; set; }

        public DataGridColumn()
        {
            AddNumberInFirstRow = false;
            AddSearchAddOn = false;
        }
    }
}
