    using System;
using System.Collections.Generic;
using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
using System.Data;
    using System.Drawing.Drawing2D;
    using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToyBox.UI
{
    public partial class PopupForm : Form
    {
        #region Constants

        public const int CsDropshadow = 0x00020000;
        public readonly Color FormBackColor = Color.DeepPink;

        #endregion

        #region Private data

        #endregion

        #region Constructor

        public PopupForm()
        {
            InitializeComponent();

            TransparencyKey = FormBackColor;
            BackColor = FormBackColor;
            TopMost = true; 
        }

        #endregion

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams p = base.CreateParams;
                p.ClassStyle = p.ClassStyle | CsDropshadow;
                return p;
            }
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupClipRegion();
        }

        private GraphicsPath CalculateFormShape()
        {
            var path = new GraphicsPath();

            foreach (Control c in GetContainer().Controls)
            {
                if (c.Visible)
                {
                    path.AddRectangle(new Rectangle(c.Left, c.Top, c.Width, c.Height));
                }
            }

            return path; 
        }

        private void SetupClipRegion()
        {
            //Also check of the container is OK. In subclasses the container is not initialized at the point the
            //PopupForm() constructor and therefore InitializeComponent is called. 
            if (!DesignMode && GetContainer() != null)
            {
                Region = new Region(CalculateFormShape());
            }
        }

        protected virtual Control GetContainer()
        {
            return this;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetupClipRegion();
        }

        protected new bool DesignMode
        {
            get
            {
                if (base.DesignMode)
                    return true;

                return LicenseManager.UsageMode == LicenseUsageMode.Designtime;
            }
        }
    }
}
