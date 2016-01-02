using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightInject;
using ToyBox.Models.Models;
using ToyBox.Models.Query;

namespace ToyBox.Services.Services
{
    internal class DatabasesServiceImpl : IDatabasesService
    {
        [Inject]
        public IBasicQueryExecutorService QueryExecutor { get; set; }

        public async Task<IEnumerable<DatabaseModel>> GetDatabases(ServerModel server)
        {
            var databases = (await QueryExecutor.ReadAsync<DatabaseModel>(server.ConnectionString, GeneralQueriesProvider.Databases)).ToList();
            databases.ForEach(d => d.Server = server);

            return databases;
        }
    }
}
