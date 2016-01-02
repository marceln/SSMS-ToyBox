using System;
using Microsoft.VisualStudio.CommandBars;

namespace ToyBox.SsmsAddIn.UserInterface
{
    internal class MainMenuHelper
    {
        public static int GetMenuBarMenuIndex(CommandBar menuBar, string caption)
        {
            var toolsBarAndIndex = GetCommandBarPopupAndIndex(menuBar, caption);
            return toolsBarAndIndex.Item1 == null ? menuBar.Controls.Count : toolsBarAndIndex.Item2;
        }


        private static Tuple<CommandBar, int> GetCommandBarPopupAndIndex(CommandBar parentCommandBar, string commandBarPopupName)
        {
            CommandBar commandBar = null;
            int index = 0;

            foreach (CommandBarControl commandBarControl in parentCommandBar.Controls)
            {
                if (commandBarControl.Type == MsoControlType.msoControlPopup)
                {
                    CommandBarPopup commandBarPopup = (CommandBarPopup)commandBarControl;

                    index++;
                    if (commandBarPopup.CommandBar.Visible && commandBarPopup.CommandBar.Name == commandBarPopupName)
                    {
                        commandBar = commandBarPopup.CommandBar;
                        break;
                    }
                }
            }

            return new Tuple<CommandBar, int>(commandBar, index);
        }
    }
}
