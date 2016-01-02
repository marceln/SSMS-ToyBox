using System.Windows.Forms;
using ToyBox.UI.CustomControls;

namespace ToyBox.UI.ObjectLookup
{
    public partial class ObjectLookupControl : BaseControl
    {
        public ObjectLookupControl()
        {
            InitializeComponent();
            ctrlLoadingCircle.Color = ColorTheme.ThemeHue;
            ctrlLoadingCircle.OuterCircleRadius = 12;
            ctrlLoadingCircle.InnerCircleRadius = 8;
            ctrlLoadingCircle.RotationSpeed = 90;
        }

        public ComboBox ServersComboBox { get { return ctlComboServer; } }

        public ComboBox DatabasesComboBox { get { return ctlComboDatabase; } }

        public TextBox LookupTextBox { get { return ctlTxtLookup; } }

        public void ShowLoadingProgress(bool show)
        {
            ctrlLoadingCircle.Active = show;
            ctrlLoadingCircle.Visible = show;
        }

    }
}
