using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ToyBox.Models.Models;

namespace ToyBox.UI.ObjectLookup
{
    public partial class ObjectLookupForm
    {

        #region Events

        public event EventHandler DataBindServers = (s, e) => { };
        public event EventHandler DataBindDatabases = (s, e) => { };

        #endregion

        #region Private data

        private BindingList<ServerModel> _databaseServers;
        private BindingList<DatabaseModel> _databases;
        private BindingList<ObjectLookupResultModel> _lookupResults;

        #endregion

        #region Properties

        public ServerModel SelectedServer { get { return (ServerModel)ctrlObjectLookupContainer.ServersComboBox.SelectedItem; } }

        public DatabaseModel SelectedDatabase { get { return (DatabaseModel)ctrlObjectLookupContainer.DatabasesComboBox.SelectedItem; } }

        #endregion

        #region Data Binding

        public void SetDatabaseServersDataSource(IEnumerable<ServerModel> servers, ServerModel currentServer)
        {
            _databaseServers.RaiseListChangedEvents = false;
            _databaseServers.Clear();
            servers.ToList().ForEach(s => _databaseServers.Add(s));
            _databaseServers.RaiseListChangedEvents = true;
            _databaseServers.ResetBindings();

            SetCurrentDatabaseServer(currentServer);
        }

        public void SetDatabasesDataSource(IEnumerable<DatabaseModel> databases, DatabaseModel currentDatabase)
        {
            _databases.RaiseListChangedEvents = false;
            _databases.Clear();
            databases.ToList().ForEach(d => _databases.Add(d));
            _databases.RaiseListChangedEvents = true; 
            _databases.ResetBindings();

            SetCurrentDatabase(currentDatabase);
        }

        public void SetLookupResultsDataSource(IEnumerable<ObjectLookupResultModel> results)
        {
            _lookupResults.RaiseListChangedEvents = false;
            _lookupResults.Clear();
            results.ToList().ForEach(r => _lookupResults.Add(r));

            _lookupResults.RaiseListChangedEvents = true;
            _lookupResults.ResetBindings();
        }

        private void InitializeBindingSources()
        {
            _databaseServers = new BindingList<ServerModel>();
            _databases = new BindingList<DatabaseModel>();
            _lookupResults = new BindingList<ObjectLookupResultModel>();

            ctrlObjectLookupContainer.ServersComboBox.DataSource = _databaseServers;
            ctrlObjectLookupContainer.DatabasesComboBox.DataSource = _databases;
            ctrlLookupResultsListBox.DataSource = _lookupResults;

            ctrlObjectLookupContainer.ServersComboBox.DisplayMember = "Name";
            ctrlObjectLookupContainer.ServersComboBox.ValueMember = "ConnectionString";

            ctrlObjectLookupContainer.DatabasesComboBox.DisplayMember = "Name";
            ctrlObjectLookupContainer.DatabasesComboBox.ValueMember = "Name";
        }

        #endregion

        #region Private implementation

        private void SetCurrentDatabaseServer(ServerModel currentServer)
        {
            ctrlObjectLookupContainer.ServersComboBox.SelectedItem = _databaseServers.FirstOrDefault(s => String.Compare(s.Name, currentServer.Name, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        private void SetCurrentDatabase(DatabaseModel database)
        {
            ctrlObjectLookupContainer.DatabasesComboBox.SelectedItem = _databases.FirstOrDefault(s => String.Compare(s.Name, database.Name, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        #endregion

    }
}
