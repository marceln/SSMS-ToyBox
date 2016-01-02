using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToyBox.Models.Enums;
using ToyBox.SsmsAddIn.UserInterface.Actions;

namespace ToyBox.SsmsAddIn.UserInterface.ActionChoosers
{
    public interface IActionChooser
    {
        List<IAction> Actions { get; }
        DbObjectType ObjectType { get; }
    }
}
