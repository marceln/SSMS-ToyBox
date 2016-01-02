using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyBox.SsmsAddIn.UserInterface.Actions
{
    public interface IAction
    {
        IActionParameters Parameters { get; set; }
        string DisplayName { get; }
        Guid Identifier { get; }

        void Execute();
    }
}
