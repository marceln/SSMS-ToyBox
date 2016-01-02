using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyBox.Models.Models;

namespace ToyBox.Services
{
    public interface ILookupCacheService
    {
        IEnumerable<ServerModel> GetServers();
        Task<IEnumerable<DatabaseModel>> GetDatabasesForServer(ServerModel server);
        Task<IEnumerable<DatabaseModel>> GetDatabasesForAllServers();

        Task<IEnumerable<ObjectLookupResultModel>> LookupTerm(string term);
        Task<IEnumerable<ObjectLookupResultModel>> LookupTerm(string term, ServerModel server);
        Task<IEnumerable<ObjectLookupResultModel>> LookupTerm(string term, ServerModel server, DatabaseModel database);
    }
}
