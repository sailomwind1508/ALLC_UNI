using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace AllCashUFormsApp.View.UControl
{
    public partial class FlatButton : Button
    {
        private Color CurrentBackColor;

        private Color onHoverBackColor = Color.DarkBlue;

        public FlatButton()
        {
            InitializeComponent();
            this.BackColor = Color.Azure;
            this.ForeColor = Color.Black;
            this.CurrentBackColor = BackColor;
        }

        public FlatButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public Color OnHoverBackColor
        {
            get { return onHoverBackColor; }
            set { onHoverBackColor = value; Invalidate(); }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            CurrentBackColor = onHoverBackColor;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            CurrentBackColor = BackColor;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            CurrentBackColor = Color.RoyalBlue;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            CurrentBackColor = BackColor;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.FillRectangle(new SolidBrush(CurrentBackColor), 0, 0, Width, Height);
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(pevent.Graphics, Text, Font, new Point(Width + 3, Height / 2), ForeColor, flags);
        }
    }
}
