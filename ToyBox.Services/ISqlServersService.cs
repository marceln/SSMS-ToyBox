using System.Collections.Generic;
using ToyBox.Models.Models;

namespace ToyBox.Services
{
    public interface ISqlServersService
    {

        IEnumerable<ServerModel> GetAvailableServers();

    }
}
