using System;
using System.Drawing;
using System.Linq;
using EnvDTE;
using LightInject;
using ToyBox.SsmsAddIn.UserInterface;
using ToyBox.SsmsAddIn.UserInterface.Controllers;
using ToyBox.UI;

namespace ToyBox.SsmsAddIn.Commands
{
    internal class CommandsHandler
    {

        #region Properties

        [Inject]
        public Lazy<IObjectLookupController> NavigateToObjectController { get; set; }

        #endregion

        #region Constructor

        public CommandsHandler(MainMenuManager mainMenuManager)
        {
            MainMenuManager = mainMenuManager;
        }

        #endregion

        #region Private properties
        
        private MainMenuManager MainMenuManager { get; set; }

        #endregion

        #region Public methods

        public vsCommandStatus QueryCommandStatus(string commandName)
        {
            if (MainMenuManager.RegisteredCommands.Any(i => i.Name == commandName))
            {
                return (vsCommandStatus) vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
            }

            return vsCommandStatus.vsCommandStatusUnsupported;
        }

        public bool ExecuteCommand(string commandName)
        {
            bool result = false;
            var actualCommand = commandName.Split(new[] {"."}, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();

            switch (actualCommand)
            {
                case CommandNames.AddInCommandNavigateToObject:
                    NavigateToObjectController.Value.StartNavigationForm();
                    result = true; 
                    break;
            }

            return result; 
        }

        #endregion
    }
}
