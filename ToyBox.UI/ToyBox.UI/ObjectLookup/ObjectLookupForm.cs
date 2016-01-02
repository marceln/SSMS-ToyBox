using System;
using System.Diagnostics;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace ToyBox.UI.ObjectLookup
{
    public partial class ObjectLookupForm : PopupForm
    {

        #region Private data

        private Rectangle _parentBounds;

        #endregion

        #region Constructors

        public ObjectLookupForm()
        {
            _parentBounds = Rectangle.Empty;
            InitInternal();
        }

        public ObjectLookupForm(Rectangle parentBounds): this() 
        {
            _parentBounds = parentBounds;
        }

        #endregion

        #region Overrides

        protected override Control GetContainer()
        {
            return tableLayoutPanel1;
        }
        
        #endregion
        
        #region Private methods

        private void InitInternal()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                InitializePositioning();
                InitializeBindingSources();
                InitializeControlEventHandlers();
                InitializeLookupTimer();
                InitializeInteraction();
                InitializeSizing();
            }
        }

        #endregion

        #region Form events

        private void ObjectLookupForm_Load(object sender, EventArgs e)
        {
            DataBindServers(this, EventArgs.Empty);
            DataBindDatabases(this, EventArgs.Empty);
        }

        private void ObjectLookupForm_Shown(object sender, EventArgs e)
        {
            FocusAndSelectLookupTextBox();
        }
        #endregion

    }
}
