using System;
using System.Collections.Generic;
using System.Text;

namespace AllCashUFormsApp
{
    public class CCBoxItem
    {
        private string val;
        public string Value
        {
            get { return val; }
            set { val = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string key;
        public string Key { get { return key; } set { key = value; } }

        public CCBoxItem()
        {
        }

        public CCBoxItem(string name, string val)
        {
            this.name = name;
            this.val = val;
        }

        //public CCBoxItem(string name, string value)
        //{
        //    this.name = name;
        //    this.key = value;
        //}

        public override string ToString()
        {
            return string.Format("name: '{0}', value: {1}", name, val);
        }
    }

}
