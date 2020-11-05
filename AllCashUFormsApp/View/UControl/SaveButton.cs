using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.View.UControl
{
    public partial class SaveButton : Button
    {
        public SaveButton()
        {
            InitializeComponent();
        }

        public SaveButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
