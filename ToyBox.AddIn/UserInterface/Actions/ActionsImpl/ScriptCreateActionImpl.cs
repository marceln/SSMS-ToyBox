using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyBox.SsmsAddIn.UserInterface.Actions.ActionsImpl
{
    internal class ScriptCreateActionImpl : AbstractActionImpl
    {

        #region AbstractActionImpl implementation

        public override string DisplayName
        {
            get { return Resources.ActionText_CreateObject; }
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
