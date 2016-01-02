using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToyBox.Models.Enums;
using ToyBox.SsmsAddIn.UserInterface.Actions;

namespace ToyBox.SsmsAddIn.UserInterface.ActionChoosers
{
    internal class ActionChooserImpl : IActionChooser
    {
        public ActionChooserImpl(DbObjectType objectType)
        {
            ObjectType = objectType;
            Actions = new List<IAction>();
        }

        public List<IAction> Actions { get; private set; }
        public DbObjectType ObjectType { get; private set; }
    }
}
