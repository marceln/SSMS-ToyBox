using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToyBox.UI.CustomControls
{
    internal class BaseListBox : ListBox
    {
        private readonly Color _backColor = Color.FromArgb(239, 239, 242);

        public BaseListBox()
        {
            BorderStyle = BorderStyle.None;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        /// <summary>
        /// Override, so we avoid virtual member call in constructor.
        /// </summary>
        public override Color BackColor
        {
            get
            {
                return _backColor;
            }
        }
    }
}
