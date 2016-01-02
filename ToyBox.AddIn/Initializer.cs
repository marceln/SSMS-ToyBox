using LightInject;
using ToyBox.Services.SSMS;
using ToyBox.SsmsAddIn.Commands;
using ToyBox.SsmsAddIn.UserInterface;

namespace ToyBox.SsmsAddIn
{
    internal class Initializer
    {
        #region Injected properties

        [Inject]
        public UserInterfaceInitializer UiInitializer { get; set; }

        [Inject]
        public ObjectExplorerAdapter ObjectExplorerAdapter{ get; set; }

        #endregion

        #region Public methods

        public void OnAddInStartup()
        {
            UiInitializer.Setup();
        }

        #endregion
    }
}
