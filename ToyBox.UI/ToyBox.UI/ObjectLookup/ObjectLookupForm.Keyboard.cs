using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ToyBox.UI.ObjectLookup
{
    public partial class ObjectLookupForm
    {

        #region Keyboard shortcuts event handlers

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        //TODO refactor this
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_KEYUP = 0x101;
            const int WM_CHAR = 0x102;
            const int WM_SYSCHAR = 0x106;
            const int WM_SYSKEYDOWN = 0x104;
            const int WM_SYSKEYUP = 0x105;
            const int WM_IME_CHAR = 0x286;

            if (msg.Msg == WM_KEYDOWN)
            {
                var e = new KeyEventArgs(((Keys)((int)((long)msg.WParam))) | ModifierKeys);
                if (e.Control && keyData.HasFlag(Keys.S))
                {
                    ctrlObjectLookupContainer.ServersComboBox.Focus();
                    ctrlObjectLookupContainer.ServersComboBox.DroppedDown = true;
                    return true;
                }

                if (e.Control && e.KeyCode.HasFlag(Keys.D))
                {
                    ctrlObjectLookupContainer.DatabasesComboBox.Focus();
                    ctrlObjectLookupContainer.DatabasesComboBox.DroppedDown = true;
                    return true;
                }

                if (ctrlObjectLookupContainer.DatabasesComboBox.Focused)
                {
                    if (ShouldKeyCloseDropDown(e.KeyCode))
                    {
                        ctrlObjectLookupContainer.DatabasesComboBox.DroppedDown = false;
                    }

                    if (DropDownChangeCommitted(e.KeyCode))
                    {
                        var selectedIndex = ctrlObjectLookupContainer.DatabasesComboBox.FindStringExact(ctrlObjectLookupContainer.DatabasesComboBox.Text);
                        if (selectedIndex >= 0)
                        {
                            ctrlObjectLookupContainer.DatabasesComboBox.SelectedItem = ctrlObjectLookupContainer.DatabasesComboBox.Items[selectedIndex];
                            ctrlObjectLookupContainer.DatabasesComboBox.Update();
                        }

                        DatabasesComboBoxOnSelectionChangeCommitted(this, EventArgs.Empty);
                    }
                }

                if (ctrlObjectLookupContainer.ServersComboBox.Focused)
                {
                    if (ShouldKeyCloseDropDown(e.KeyCode))
                    {
                        ctrlObjectLookupContainer.ServersComboBox.DroppedDown = false;
                    }

                    if (DropDownChangeCommitted(e.KeyCode))
                    {
                        var selectedIndex = ctrlObjectLookupContainer.ServersComboBox.FindStringExact(ctrlObjectLookupContainer.ServersComboBox.Text);
                        if (selectedIndex >= 0)
                        {
                            ctrlObjectLookupContainer.ServersComboBox.SelectedItem = ctrlObjectLookupContainer.ServersComboBox.Items[selectedIndex];
                            ctrlObjectLookupContainer.ServersComboBox.Update();
                        }

                        ServersComboBoxOnSelectionChangeCommitted(this, EventArgs.Empty);
                    }
                }

                if (ctrlObjectLookupContainer.LookupTextBox.Focused)
                {
                    if (e.KeyData == Keys.Up ||
                        e.KeyData == Keys.Down ||
                        e.KeyData == Keys.PageDown ||
                        e.KeyData == Keys.PageUp)
                    {
                        SendMessage(ctrlLookupResultsListBox.Handle, (UInt32)msg.Msg, msg.WParam, msg.LParam);
                        return true;
                    }
                }

                if (e.KeyData == Keys.Enter || e.KeyData == Keys.Return)
                {
                    ProcessResultItemSelected();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_MOUSEWHEEL = 0x020A;
            if (m.Msg == WM_MOUSEWHEEL)
            {
                if (!ctrlObjectLookupContainer.ServersComboBox.Focused &&
                    !ctrlObjectLookupContainer.DatabasesComboBox.Focused &&
                    m.HWnd != ctrlLookupResultsListBox.Handle)
                {
                    SendMessage(ctrlLookupResultsListBox.Handle, (UInt32)m.Msg, m.WParam, m.LParam);
                    return;
                }
            }
            base.WndProc(ref m);
        }

        private bool ShouldKeyCloseDropDown(Keys key)
        {
            return !(key == Keys.Up ||
                   key == Keys.Down ||
                   key == Keys.Left ||
                   key == Keys.Right ||
                   key == Keys.Enter ||
                   key == Keys.Return);
        }

        private bool DropDownChangeCommitted(Keys key)
        {
            return key == Keys.Enter ||
                   key == Keys.Return;
        }

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);


        #endregion

    }
}
