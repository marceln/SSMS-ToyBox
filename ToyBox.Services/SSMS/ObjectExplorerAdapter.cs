using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Microsoft.SqlServer.Management.SqlStudio.Explorer;
using Microsoft.SqlServer.Management.UI.VSIntegration;
using Microsoft.SqlServer.Management.UI.VSIntegration.ObjectExplorer;
using ToyBox.Models.Helpers;
using ToyBox.Models.Models;
using ToyBox.Models.SSMS;

namespace ToyBox.Services.SSMS
{
    public class ObjectExplorerAdapter
    {
        #region Private data

        private readonly ObjectExplorerService _objectExplorerService;
        private ContextService _contextService;
        private readonly ReaderWriterLockSlim _activeServersLock;


        private readonly List<ServerModel> _activeServers;

        #endregion

        #region Constructor

        public ObjectExplorerAdapter()
        {
            _objectExplorerService = (ObjectExplorerService)ServiceCache.ServiceProvider.GetService(typeof(IObjectExplorerService));
            _activeServers = new List<ServerModel>();
            _activeServersLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

            InitInternal();
        }

        #endregion

        #region API

        public ObjectExplorerContext GetCurrentContext()
        {
            var currentNode = GetSelectedNode();
            return GetCurrentContext(currentNode);
        }

        #endregion

        #region Properties

        public ReadOnlyCollection<ServerModel> ActiveServers
        {
            get
            {
                _activeServersLock.EnterReadLock();
                try
                {
                    return _activeServers.AsReadOnly();
                }
                finally
                {
                    _activeServersLock.ExitReadLock();
                }
            }
        }

        #endregion

        #region Private methods

        private INodeInformation GetSelectedNode()
        {
            INodeInformation[] nodes;
            int nodeCount;
            _objectExplorerService.GetSelectedNodes(out nodeCount, out nodes);

            return (nodeCount > 0 ? nodes[0] : null);
        }

        private INodeInformation GetServerNode(INodeInformation node)
        {
            if (node == null)
            {
                return null;
            }

            while (node.Parent != null)
            {
                node = node.Parent;
            }

            return node;
        }

        private ObjectExplorerContext GetCurrentContext(INodeInformation node)
        {
            if (node == null)
            {
                return null;
            }

            var serverNode = GetServerNode(node);
            var databaseMatch = ObjectExplorerNodeRegex.DbRegexStart.Match(node.Context);

            if (serverNode == null)
            {
                return null;
            }

            return new ObjectExplorerContext()
            {
                Server = serverNode.InvariantName,
                ConnectionString = node.Connection.ConnectionString,
                CurrentNodeName = node.Name,
                Database = databaseMatch.Success? null:databaseMatch.Groups["Database"].Value
            };

        }

        #endregion

        #region Initialization

        private void InitInternal()
        {
            foreach (var component in _objectExplorerService.Container.Components)
            {
                var service = component as ContextService;
                if (service != null)
                {
                    _contextService = service;
                    break;
                }
            }

            _contextService.ObjectExplorerContext.ItemsChanged.Event += ObjectExplorerContext_ItemsChanged;
            _contextService.ObjectExplorerContext.ItemsRemoved.Event += ObjectExplorerContext_ItemsRemoved;
            _contextService.ObjectExplorerContext.CurrentContextChanged += ObjectExplorerContext_CurrentContextChanged;
        }

        #endregion

        #region Event handlers

        private void ObjectExplorerContext_CurrentContextChanged(object sender, NodesChangedEventArgs args)
        {
        }

        private void ObjectExplorerContext_ItemsRemoved(object sender, NodesChangedEventArgs args)
        {

        }

        private void ObjectExplorerContext_ItemsChanged(object sender, NodesChangedEventArgs args)
        {
            var serverNodes = args.ChangedNodes.Where(n => n.UrnPath == ObjectExplorerViewContexts.Server).ToList();
            if (!serverNodes.Any())
            {
                return;
            }

            _activeServersLock.EnterWriteLock();
            try
            {
                foreach (var serverNode in serverNodes)
                {
                    string connectionString = serverNode.Connection.ConnectionString.ToLowerInvariant();
                    if (_activeServers.All(n => n.ConnectionString != connectionString))
                    {
                        _activeServers.Add(new ServerModel()
                        {
                            ConnectionString = connectionString,
                            Name = serverNode.InvariantName
                        });
                    }
                }
            }
            finally
            {
                _activeServersLock.ExitWriteLock();
            }
        }

        #endregion
    }
}
