using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.View.UControl
{
    public partial class RemoveButton : Button
    {
        public RemoveButton()
        {
            //InitializeComponent();
        }

        public RemoveButton(IContainer container)
        {
            container.Add(this);

            //InitializeComponent();
        }
    }
}
