using System;
using System.Collections.Generic;
using EnvDTE;
using EnvDTE80;
using Microsoft.SqlServer.Management.UI.VSIntegration;
using Microsoft.VisualStudio.CommandBars;

namespace ToyBox.SsmsAddIn.UserInterface
{
    internal class MainMenuManager : IDisposable
    {
        #region Private data

        private CommandBarControl _mainMenu;
        private readonly List<Command> _commands;

        #endregion

        #region Constructor

        public MainMenuManager(DTE2 application, AddIn addInInstance)
        {
            _commands = new List<Command>();
            Application = application;
            AddInInstance = addInInstance;
        }

        #endregion

        #region Private Properties

        private DTE2 Application { get; set; }

        private AddIn AddInInstance { get; set; }

        #endregion

        #region Properties

        public IEnumerable<Command> RegisteredCommands
        {
            get { return _commands.AsReadOnly(); }
        }

        #endregion

        #region Public API

        public void AddMainMenu()
        {
            CommandBar mainMenu = ((CommandBars)ServiceCache.ExtensibilityModel.CommandBars)["MenuBar"];

            _mainMenu = mainMenu.Controls.Add(
                MsoControlType.msoControlPopup,
                Type.Missing,
                Type.Missing,
                MainMenuHelper.GetMenuBarMenuIndex(mainMenu, "Tools") + 1,
                true);

            _mainMenu.Caption = "ToyBox";
            _mainMenu.Visible = true;
        }

        public void RegisterCommand(string text, string name, string shortcut)
        {
            object[] contextGUIDS = new object[] { };
            Commands2 commands = (Commands2)Application.Commands;
            CommandBarPopup menuPopup = (CommandBarPopup)_mainMenu;

            try
            {
                Command command = null;

                try
                {
                    command = Application.Commands.Item(String.Format("{0}.{1}", AddInInstance.ProgID, name), -1);

#if DEBUG
                    command.Delete();
                    command = null;
#endif
                }
                catch
                { }

                if (command == null)
                {
                    command = commands.AddNamedCommand(AddInInstance,
                            name,
                            text,
                            text,
                            true,
                            0,
                            ref contextGUIDS,
                            (int)(vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled));

                }

                if ((command != null) && (menuPopup != null))
                {
                    var item = (CommandBarButton)command.AddControl(menuPopup.CommandBar, 1);
                    item.Caption = text;
                    item.Style = MsoButtonStyle.msoButtonCaption;
                    item.BeginGroup = false;
                    item.Visible = true;

                    command.Bindings = shortcut;
                    _commands.Add(command);
                }
            }
            catch (System.ArgumentException)
            {
            }
        }

        #endregion


        #region IDisposable implementation

        public void Dispose()
        {
            _commands.Clear();
        }

        #endregion

    }
}
