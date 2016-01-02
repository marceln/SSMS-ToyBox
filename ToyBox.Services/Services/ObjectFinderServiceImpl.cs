using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToyBox.Models.Enums;
using ToyBox.Models.Models;
using ToyBox.Models.Query;

namespace ToyBox.Services.Services
{
    internal class ObjectFinderServiceImpl : IObjectFinderService
    {

        #region Static data

        private static readonly string ObjectFinderQuery;

        static ObjectFinderServiceImpl()
        {
            ObjectFinderQuery = ObjectLookupQueryProvider.FindObject;
        }

        #endregion

        #region Private data

        private readonly IBasicQueryExecutorService _queryExecutor;

        #endregion

        #region Constructor

        public ObjectFinderServiceImpl(IBasicQueryExecutorService queryExecutor)
        {
            _queryExecutor = queryExecutor;
        }

        #endregion

        #region IObjectFinderService implementation

        public async Task<IEnumerable<DatabaseObjectModel>> FindObjectAsync(DatabaseModel db, ServerModel server)
        {
            return await FindObjectInternal(db, server);
        }

        #endregion

        #region Private implementation

        private async Task<IEnumerable<DatabaseObjectModel>> FindObjectInternal(DatabaseModel db, ServerModel server)
        {
            var results = (await _queryExecutor.ReadAsync<DatabaseObjectModel>(server.ConnectionString, FormatLookupQuery(db.Name))).ToList();
            results.ForEach(r => r.Database = db);

            return results;
        }

        private string FormatLookupQuery(string databaseName)
        {
            return String.Format(ObjectFinderQuery, databaseName);
        }

        #endregion

    }
}
