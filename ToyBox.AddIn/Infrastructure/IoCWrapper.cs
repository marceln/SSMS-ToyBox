using EnvDTE;
using EnvDTE80;
using LightInject;

namespace ToyBox.SsmsAddIn.Infrastructure
{
    internal class IoCWrapper
    {
        private readonly ServiceContainer _container;

        public IoCWrapper()
        {
            _container = new ServiceContainer();
            _container.EnableAnnotatedPropertyInjection();
            _container.RegisterFrom<CompositionRoot>();
        }

        public void RegisterAddInBaseDeps(DTE2 application, AddIn addInInstance)
        {
            _container.RegisterInstance(typeof(DTE2), application);
            _container.RegisterInstance(typeof(AddIn), addInInstance);
        }

        public ServiceContainer Container { get { return _container; } }
    }
}
