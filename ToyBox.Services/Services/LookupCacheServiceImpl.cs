using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightInject;
using ToyBox.Models.Models;
using ToyBox.Services.SSMS;

namespace ToyBox.Services.Services
{
    internal class LookupCacheServiceImpl : ILookupCacheService
    {

        #region Private data

        private List<ServerModel> _serversCache;
        private readonly Dictionary<string, List<DatabaseModel>> _databasesCache;
        private readonly Dictionary<string, List<DatabaseObjectModel>> _resultsCache;

        #endregion

        #region Properties

        [Inject]
        public ISqlServersService ServersService { get; set; }

        [Inject]
        public IDatabasesService DatabasesService { get; set; }

        [Inject]
        public IObjectFinderService ObjectFinderService { get; set; }

        [Inject]
        public ObjectExplorerAdapter ObjectExplorerAdapter { get; set; }

        [Inject]
        public IObjectFilteringService FilteringService { get; set; }

        #endregion

        #region Constructor

        public LookupCacheServiceImpl()
        {
            _databasesCache = new Dictionary<string, List<DatabaseModel>>();
            _resultsCache = new Dictionary<string, List<DatabaseObjectModel>>();
        }

        #endregion

        #region ILookupCacheService implementation

        public IEnumerable<ServerModel> GetServers()
        {
            return _serversCache ?? (_serversCache = ServersService.GetAvailableServers().ToList());
        }

        public async Task<IEnumerable<DatabaseModel>> GetDatabasesForServer(ServerModel server)
        {
            if (_databasesCache.ContainsKey(server.ConnectionString))
            {
                return await Task.FromResult(_databasesCache[server.ConnectionString]);
            }


            var databases = (await DatabasesService.GetDatabases(server)).ToList();
            _databasesCache.Add(server.ConnectionString, databases);

            return databases;
        }

        public async Task<IEnumerable<DatabaseModel>> GetDatabasesForAllServers()
        {
            var allTasks = GetServers().Select(GetDatabasesForServer).ToList();
            return (await Task.WhenAll(allTasks)).SelectMany(i => i.ToList());
        }

        public async Task<IEnumerable<ObjectLookupResultModel>> LookupTerm(string term)
        {
            var databases = await GetDatabasesForAllServers();
            var allTasks = databases.Select(db => LookupTerm(term, db.Server, db)).ToList();

            return (await Task.WhenAll(allTasks)).SelectMany(i => i.ToList());
        }

        public async Task<IEnumerable<ObjectLookupResultModel>> LookupTerm(string term, ServerModel server)
        {
            var databases = await GetDatabasesForServer(server);
            var allTasks = databases.Select(db => LookupTerm(term, server, db)).ToList();

            return (await Task.WhenAll(allTasks)).SelectMany(i => i.ToList());
        }

        public async Task<IEnumerable<ObjectLookupResultModel>> LookupTerm(string term, ServerModel server, DatabaseModel database)
        {
            var cacheKey = GetUniqueKeyForDatabaseAndServer(database, server);
            List<DatabaseObjectModel> allDatabaseObjects = null;
            if (_resultsCache.ContainsKey(cacheKey))
            {
                allDatabaseObjects = _resultsCache[cacheKey];
            }
            else
            {
                allDatabaseObjects = (await ObjectFinderService.FindObjectAsync(database, server)).ToList();
                _resultsCache[cacheKey] = allDatabaseObjects;
            }

            return await FilteringService.FilterObjectsAsync(term, allDatabaseObjects);
        }

        #endregion

        #region Private implementation

        private string GetUniqueKeyForDatabaseAndServer(DatabaseModel database, ServerModel server)
        {
            return String.Format("{0}-{1}", database.Id, server.Name.ToUpperInvariant());
        }

        #endregion

    }
}
