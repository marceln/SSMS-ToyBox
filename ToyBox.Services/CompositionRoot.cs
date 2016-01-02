using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LightInject;
using Microsoft.VisualStudio.PlatformUI;
using ToyBox.Models.Models;
using ToyBox.Models.SSMS;
using ToyBox.Services.Services;
using ToyBox.Services.SSMS;

namespace ToyBox.Services
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ObjectExplorerAdapter>(new PerContainerLifetime());

            serviceRegistry.Register<ISqlServersService, SqlServersServiceImpl>(new PerContainerLifetime());
            serviceRegistry.Register<IBasicQueryExecutorService, BasicQueryExecutorServiceImpl>(new PerContainerLifetime());
            serviceRegistry.Register<IDatabasesService, DatabasesServiceImpl>(new PerContainerLifetime());
            serviceRegistry.Register<IObjectFinderService, ObjectFinderServiceImpl>(new PerContainerLifetime());
            serviceRegistry.Register<ILookupCacheService, LookupCacheServiceImpl>();
            serviceRegistry.Register<IObjectFilteringService, ObjectFilteringServiceImpl>(new PerContainerLifetime());

            serviceRegistry.Register(f => ContextFactory(f));
        }

        private ActiveObjectExplorerContext ContextFactory(IServiceFactory factory)
        {
            var adapter = factory.GetInstance<ObjectExplorerAdapter>();
            var currentContext = adapter.GetCurrentContext();
            return new ActiveObjectExplorerContext()
            {
                ConnectionString = currentContext.ConnectionString,
                CurrentNodeName = currentContext.CurrentNodeName,
                Database = currentContext.Database,
                Server = currentContext.Server
            };
        }

    }
}
