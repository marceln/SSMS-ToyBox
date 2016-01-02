using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EnvDTE;
using LightInject;
using ToyBox.Models.Models;
using ToyBox.Models.SSMS;
using ToyBox.Services;
using ToyBox.Services.SSMS;
using ToyBox.UI.Model;
using ToyBox.UI.ObjectLookup;

namespace ToyBox.SsmsAddIn.UserInterface.Controllers.Impl
{
    internal class ObjectLookupControllerImpl : IObjectLookupController
    {

        #region Private data

        private ObjectExplorerContext _activeContext;
        private ServerModel _activeServerModel;
        private DatabaseModel _activeDatabaseModel;
        private string _activeLookupText;
        private bool _suspendLookups;

        #endregion

        #region Properties

        [Inject]
        public ObjectLookupForm LookupForm { get; set; }


        [Inject]
        public ILookupCacheService ObjectCache { get; set; }

        [Inject]
        public ObjectExplorerAdapter ObjectExplorerAdapter { get; set; }

        #endregion

        #region Constructor

        public ObjectLookupControllerImpl()
        {
            _suspendLookups = false;
        }

        #endregion

        #region INavigateToObjectController implementation

        public void StartNavigationForm()
        {
            LookupForm.DataBindDatabases += LookupFormOnDataBindDatabases;
            LookupForm.DataBindServers += LookupFormOnDataBindServers;
            LookupForm.DatabaseChanged += LookupFormOnDatabaseChanged;
            LookupForm.ServerChanged += LookupFormOnServerChanged;
            LookupForm.LookupTextChanged += LookupFormOnLookupTextChanged;

            LookupForm.Show();
        }

        #endregion

        #region Lookup form event handlers

        private void LookupFormOnDataBindServers(object sender, EventArgs eventArgs)
        {
            _suspendLookups = true;
            Debug.WriteLine("ObjectLookupControllerImpl.LookupFormOnDataBindServers");

            var allServers = ObjectCache.GetServers().ToList();
            allServers.Insert(0, new ServerModel() { Name = "All" });

            _activeServerModel = ActiveContext != null ? ActiveContext.ToServerModel() : allServers.FirstOrDefault();
            LookupForm.SetDatabaseServersDataSource(allServers, _activeServerModel);
            _suspendLookups = false;
        }

        private async void LookupFormOnDataBindDatabases(object sender, EventArgs eventArgs)
        {
            _suspendLookups = true;
            Debug.WriteLine("ObjectLookupControllerImpl.LookupFormOnDataBindDatabases");

            List<DatabaseModel> allDatabasesForServer = null;
            if (String.IsNullOrEmpty(_activeServerModel.ConnectionString))
            {
                allDatabasesForServer = (await ObjectCache.GetDatabasesForAllServers()).ToList();
            }
            else
            {
                allDatabasesForServer = (await ObjectCache.GetDatabasesForServer(_activeServerModel)).ToList();
            }
            
            allDatabasesForServer.Insert(0, new DatabaseModel() { Id = -1, Name = "All" });

            var currentDatabaseName = ActiveContext != null ? ActiveContext.Database : string.Empty;
            _activeDatabaseModel = allDatabasesForServer.FirstOrDefault(d => String.Compare(d.Name, currentDatabaseName, StringComparison.InvariantCultureIgnoreCase) == 0) ??
                                   allDatabasesForServer.FirstOrDefault();

            LookupForm.SetDatabasesDataSource(allDatabasesForServer, _activeDatabaseModel);
            _suspendLookups = false;
        }

        private async void LookupFormOnLookupTextChanged(object sender, ItemChangedEventArgs<string> itemChangedEventArgs)
        {
            if (_activeServerModel == null || _activeDatabaseModel == null || _suspendLookups)
            {
                return;
            }

            Debug.WriteLine("ObjectLookupControllerImpl.LookupFormOnLookupTextChanged {0}", new object[] { itemChangedEventArgs.ChangedItem });
            LookupForm.PrepareForLookupStart();

            _activeLookupText = itemChangedEventArgs.ChangedItem ?? String.Empty;
            var lookupResults = await GetResults(_activeLookupText);
            lookupResults = lookupResults.OrderBy(l => l.MatchWeight).ThenBy(l => l.DatabaseObject.ObjectName);

            LookupForm.SetLookupResultsDataSource(lookupResults);

            LookupForm.PrepareForLookupFinish();
        }

        private void LookupFormOnServerChanged(object sender, ItemChangedEventArgs<ServerModel> itemChangedEventArgs)
        {
            Debug.WriteLine("ObjectLookupControllerImpl.LookupFormOnServerChanged {0}", new object[] { itemChangedEventArgs.ChangedItem.Name });

            _activeServerModel = itemChangedEventArgs.ChangedItem;
            LookupFormOnLookupTextChanged(this, new ItemChangedEventArgs<string>() { ChangedItem = _activeLookupText });
        }

        private void LookupFormOnDatabaseChanged(object sender, ItemChangedEventArgs<DatabaseModel> itemChangedEventArgs)
        {
            Debug.WriteLine("ObjectLookupControllerImpl.LookupFormOnDatabaseChanged {0}", new object[] { itemChangedEventArgs.ChangedItem.Name });

            _activeDatabaseModel = itemChangedEventArgs.ChangedItem;
            LookupFormOnLookupTextChanged(this, new ItemChangedEventArgs<string>() { ChangedItem = _activeLookupText });
        }

        #endregion

        #region Private implementation

        private ObjectExplorerContext ActiveContext
        {
            get { return _activeContext ?? (_activeContext = ObjectExplorerAdapter.GetCurrentContext()); }
        }

        private async Task<IEnumerable<ObjectLookupResultModel>> GetResults(string term)
        {
            IEnumerable<ObjectLookupResultModel> results = null;

            //Check if we're searching in all databases, regardless what server. 
            if (_activeDatabaseModel.Id == -1)
            {
                //Is it all servers?
                if (String.IsNullOrEmpty(_activeServerModel.ConnectionString))
                {
                    results = await ObjectCache.LookupTerm(_activeLookupText);
                }
                else
                {
                    results = await ObjectCache.LookupTerm(_activeLookupText, _activeServerModel);
                }
            }
            else
            {
                //This is the case for a specific database and a specific server (or all servers)
                results = await ObjectCache.LookupTerm(_activeLookupText, _activeDatabaseModel.Server, _activeDatabaseModel);
            }

            return results;
        }

        #endregion

    }
}
