using System.Collections.Generic;
using System.Threading.Tasks;
using ToyBox.Models.Models;

namespace ToyBox.Services
{
    public interface IDatabasesService
    {

        Task<IEnumerable<DatabaseModel>> GetDatabases(ServerModel server);

    }
}
