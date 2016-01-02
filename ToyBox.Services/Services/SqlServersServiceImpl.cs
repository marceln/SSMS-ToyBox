using System.Collections.Generic;
using LightInject;
using ToyBox.Models.Models;
using ToyBox.Services.SSMS;

namespace ToyBox.Services.Services
{
    internal class SqlServersServiceImpl : ISqlServersService
    {
        #region Properties

        [Inject]
        public ObjectExplorerAdapter ObjectExplorerAdapter { get; set; }

        #endregion

        #region ISqlServersService implementation

        public IEnumerable<ServerModel> GetAvailableServers()
        {
            return ObjectExplorerAdapter.ActiveServers;
        }
        
        #endregion
    }
}
