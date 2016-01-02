using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EnvDTE;
using LightInject;
using ToyBox.SsmsAddIn.Commands;

namespace ToyBox.SsmsAddIn.UserInterface
{
    internal class UserInterfaceInitializer
    {

        #region Injected properties

        [Inject]
        public MainMenuManager MainMenuManager { get; set; }

        #endregion

        #region Public methods

        public void Setup()
        {
            MainMenuManager.AddMainMenu();
            CreateMainMenuEntries();
        }

        #endregion

        #region Private methods

        private void CreateMainMenuEntries()
        {
            MainMenuManager.RegisterCommand("Navigate to object", CommandNames.AddInCommandNavigateToObject, "global::ALT+SHIFT+L");
        }

        #endregion

    }
}
