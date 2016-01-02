using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToyBox.UI
{
    public partial class BaseControl : UserControl
    {
        private readonly Color _controlBackColor = Color.FromArgb(236, 240, 241);
        private readonly Color _controlBorderColor = Color.FromArgb(52, 152, 219);
        private readonly Color _controlTextColor = Color.FromArgb(127, 140, 141);

        #region Constructor

        public BaseControl()
        {
            InitializeComponent();
            InitInternal();
            AdjustColorsForChildren();
        }

        #endregion

        #region Overrides

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            using (Pen pen = new Pen(_controlBorderColor, 2))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, Width, Height);   
            }
        }

        #endregion

        #region Private implementation

        private void InitInternal()
        {
            BackColor = _controlBackColor;
            Padding = new Padding(2, 2, 2, 2);
        }

        private void AdjustColorsForChildren()
        {
            foreach (Control control in Controls)
            {
                if (control is Label)
                {
                    control.ForeColor = _controlTextColor;
                }
            }
        }

        #endregion

    }
}
