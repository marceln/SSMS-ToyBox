using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using ToyBox.Models.Models;
using ToyBox.UI.Model;

namespace ToyBox.UI.ObjectLookup
{
    public partial class ObjectLookupForm
    {

        #region Events

        public event EventHandler<ItemChangedEventArgs<ServerModel>> ServerChanged = (s, e) => { };
        public event EventHandler<ItemChangedEventArgs<DatabaseModel>> DatabaseChanged = (s, e) => { };
        public event EventHandler<ItemChangedEventArgs<string>> LookupTextChanged = (s, e) => { };
        public event EventHandler<ItemChangedEventArgs<ObjectLookupResultModel>> ItemSelected = (s, e) => { };

        #endregion

        #region Private data

        private Timer _startLookupTimer;

        #endregion

        #region Public methods

        public void PrepareForLookupStart()
        {
            ctrlObjectLookupContainer.ShowLoadingProgress(true);
        }

        public void PrepareForLookupFinish()
        {
            ctrlObjectLookupContainer.ShowLoadingProgress(false);
            ctrlLookupResultsListBox.Visible = true;
        }

        #endregion

        #region Initialization

        private void InitializeLookupTimer()
        {
            _startLookupTimer = new Timer { Interval = 300 };
            _startLookupTimer.Tick += StartLookupTimerOnTick;
        }

        private void InitializeControlEventHandlers()
        {
            ctrlObjectLookupContainer.ServersComboBox.SelectionChangeCommitted += ServersComboBoxOnSelectionChangeCommitted;
            ctrlObjectLookupContainer.DatabasesComboBox.SelectionChangeCommitted += DatabasesComboBoxOnSelectionChangeCommitted;
            ctrlObjectLookupContainer.ServersComboBox.SelectedIndexChanged += ServersComboBox_SelectedIndexChanged;
            ctrlObjectLookupContainer.DatabasesComboBox.SelectedIndexChanged += DatabasesComboBox_SelectedIndexChanged;
            ctrlObjectLookupContainer.LookupTextBox.TextChanged += LookupTextBoxOnTextChanged;
        }

        private void InitializeInteraction()
        {
            ctrlObjectLookupContainer.LookupTextBox.Focus();
            ctrlLookupResultsListBox.Visible = false; 
        }

        #endregion

        #region Control events

        private void LookupTextBoxOnTextChanged(object sender, EventArgs eventArgs)
        {
            //Reset the timer
            _startLookupTimer.Stop();
            _startLookupTimer.Start();
        }

        private void DatabasesComboBoxOnSelectionChangeCommitted(object sender, EventArgs eventArgs)
        {
            Debug.WriteLine("DatabasesComboBox_SelectedIndexChanged {0}", new object[] { ctrlObjectLookupContainer.DatabasesComboBox.SelectedIndex });

            if (ctrlObjectLookupContainer.DatabasesComboBox.SelectedItem != null)
            {
                DatabaseChanged(this,
                    new ItemChangedEventArgs<DatabaseModel>()
                    {
                        ChangedItem = (DatabaseModel)ctrlObjectLookupContainer.DatabasesComboBox.SelectedItem
                    });
            }
            else
            {
                ctrlObjectLookupContainer.DatabasesComboBox.SelectedItem = _databases.FirstOrDefault();
            }

            //Yes, BeginInvoke. Focus must be changed from the combo AFTER the selected index changed code is executed. 
            //Weird. Otherwise the change is not committed properly and the wrong value is displayed in the combo. 
            BeginInvoke((MethodInvoker)FocusAndSelectLookupTextBox);
        }

        private void ServersComboBoxOnSelectionChangeCommitted(object sender, EventArgs eventArgs)
        {
            Debug.WriteLine("ServersComboBox_SelectedIndexChanged {0}", new object[] { ctrlObjectLookupContainer.ServersComboBox.SelectedIndex });

            if (ctrlObjectLookupContainer.ServersComboBox.SelectedItem != null)
            {
                ServerChanged(this,
                    new ItemChangedEventArgs<ServerModel>()
                    {
                        ChangedItem = (ServerModel)ctrlObjectLookupContainer.ServersComboBox.SelectedItem
                    });
            }
            else
            {
                ctrlObjectLookupContainer.ServersComboBox.SelectedItem = _databaseServers.FirstOrDefault();
            }

            //Yes, BeginInvoke. Focus must be changed from the combo AFTER the selected index changed code is executed. 
            //Weird. Otherwise the change is not committed properly and the wrong value is displayed in the combo. 
            BeginInvoke((MethodInvoker)FocusAndSelectLookupTextBox);
        }

        /// <summary>
        /// Called only when the selected index is changed via lookup/autocomplete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabasesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Called only when the selected index is changed via lookup/autocomplete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void StartLookupTimerOnTick(object sender, EventArgs eventArgs)
        {
            _startLookupTimer.Stop();
            LookupTextChanged(this, new ItemChangedEventArgs<string>() { ChangedItem = ctrlObjectLookupContainer.LookupTextBox.Text });
        }

        #endregion

        #region Private implementation

        private void FocusAndSelectLookupTextBox()
        {
            ctrlObjectLookupContainer.LookupTextBox.Focus();
            ctrlObjectLookupContainer.LookupTextBox.SelectAll();
        }

        #endregion

        #region Result selection

        private void ProcessResultItemSelected()
        {
            int selectedIndex = ctrlLookupResultsListBox.SelectedIndex;
            if (selectedIndex >= 0)
            {
                ItemSelected(this, new ItemChangedEventArgs<ObjectLookupResultModel>()
                {
                    ChangedItem = _lookupResults[selectedIndex]
                });
            }
        }

        #endregion

    }
}
