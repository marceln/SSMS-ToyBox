using System.Linq;
using LightInject;
using ToyBox.SsmsAddIn.Commands;
using ToyBox.SsmsAddIn.UserInterface;
using ToyBox.SsmsAddIn.UserInterface.Controllers;
using ToyBox.SsmsAddIn.UserInterface.Controllers.Impl;

namespace ToyBox.SsmsAddIn.Infrastructure
{
    internal class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            ComposeControllers(serviceRegistry);
            ComposeInternalServices(serviceRegistry);
        }

        private static void ComposeControllers(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IObjectLookupController, ObjectLookupControllerImpl>();
        }

        private static void ComposeInternalServices(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<MainMenuManager>(new PerContainerLifetime());
            serviceRegistry.Register<CommandsHandler>();
            serviceRegistry.Register<Initializer>(new PerContainerLifetime());
            serviceRegistry.Register<UserInterfaceInitializer>(new PerContainerLifetime());
        }
    }
}
