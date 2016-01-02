using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToyBox.UI.ObjectLookup
{
    public partial class ObjectLookupForm
    {
        private void InitializePositioning()
        {
            if (_parentBounds == Rectangle.Empty)
            {
                StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                StartPosition = FormStartPosition.Manual;
                Left = _parentBounds.Width / 2 - Width / 2;
                Top = _parentBounds.Height / 2 - Height / 2;
            }
        }

        private void InitializeSizing()
        {
            var screenHeight = Screen.FromControl(this).Bounds.Height;
            Height = screenHeight - Top - 20;

            ctrlLookupResultsListBox.SizeChanged += CtrlLookupResultsListBoxOnSizeChanged;
            CalculateMaxSizeForResultBox();
        }

        private void CtrlLookupResultsListBoxOnSizeChanged(object sender, EventArgs eventArgs)
        {
            Size = new Size(ctrlLookupResultsListBox.Size.Width + this.Padding.Left + this.Padding.Right + 8, ctrlLookupResultsListBox.Top + ctrlLookupResultsListBox.Size.Height);
        }

        private void CalculateMaxSizeForResultBox()
        {
            var screenSize = Screen.FromControl(this).Bounds.Size;
            var maxWidthForResultsBox = screenSize.Width - this.Left;
            var maxHeightForResultsBox = screenSize.Height - PointToScreen(ctrlLookupResultsListBox.Location).Y;

            ctrlLookupResultsListBox.MaximumSize = new Size(maxWidthForResultsBox, maxHeightForResultsBox);
        }
    }
}
