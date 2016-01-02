using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyBox.SsmsAddIn.UserInterface.Actions.ActionsImpl
{
    internal abstract class AbstractActionImpl : IAction
    {

        #region Private data 

        private readonly Guid _identifier;

        #endregion

        #region Constructor 

        protected AbstractActionImpl()
        {
            _identifier = Guid.NewGuid();
        }

        #endregion

        #region IAction implementation

        public IActionParameters Parameters { get; set; }

        public abstract string DisplayName { get; }

        public Guid Identifier
        {
            get { return _identifier; }
        }

        public abstract void Execute();

        #endregion

    }
}
