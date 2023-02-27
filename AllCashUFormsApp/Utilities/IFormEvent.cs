using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp
{
    public interface IFormEvent
    {
        void InitPage();

        void InitHeader();

        void InitialData();

        void Save();

        void SetDefaultGridViewEvent(DataGridView grd);

        bool ValidateSave();

    }
}
