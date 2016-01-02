using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToyBox.Models.Enums;
using ToyBox.SsmsAddIn.UserInterface.Actions.ActionsImpl;

namespace ToyBox.SsmsAddIn.UserInterface.ActionChoosers
{
    public class ActionChooserFactory
    {

        #region Private data

        private readonly List<IActionChooser> _actionChoosers; 

        #endregion

        #region Constructor

        public ActionChooserFactory()
        {
            _actionChoosers = new List<IActionChooser>();
            RegisterActionChoosers();
        }

        #endregion

        #region Public methods

        public IActionChooser GetActionChooser(DbObjectType objectType)
        {
            var chooser = _actionChoosers.FirstOrDefault(a => a.ObjectType == objectType);
            if (chooser == null)
            {
                throw new InvalidOperationException("Invalid object type requested");
            }

            return chooser;
        }

        #endregion

        #region Private implementation

        private void RegisterActionChoosers()
        {
            RegisterActionChoosersForGeneric();   
            RegisterActionChoosersForScalarFunction();
            RegisterActionChoosersForStoredProcedure();
            RegisterActionChoosersForTable();
            RegisterActionChoosersForTableValuedFunction();
        }

        private void RegisterActionChoosersForTable()
        {
            var actionChooser = new ActionChooserImpl(DbObjectType.Table);
            actionChooser.Actions.Add(new LocateInObjectExplorerActionImpl());
            actionChooser.Actions.Add(new ScriptAlterActionImpl());
            actionChooser.Actions.Add(new ScriptCreateActionImpl());
            actionChooser.Actions.Add(new ScriptDropActionImpl());
            actionChooser.Actions.Add(new ScriptInsertActionImpl());
            actionChooser.Actions.Add(new ScriptSelectActionImpl());
            actionChooser.Actions.Add(new ScriptUpdateActionImpl());

            _actionChoosers.Add(actionChooser);
        }

        private void RegisterActionChoosersForStoredProcedure()
        {
            var actionChooser = new ActionChooserImpl(DbObjectType.StoredProcedure);
            actionChooser.Actions.Add(new LocateInObjectExplorerActionImpl());
            actionChooser.Actions.Add(new ScriptAlterActionImpl());
            actionChooser.Actions.Add(new ScriptCreateActionImpl());
            actionChooser.Actions.Add(new ScriptDropActionImpl());

            _actionChoosers.Add(actionChooser);
        }

        private void RegisterActionChoosersForScalarFunction()
        {
            var actionChooser = new ActionChooserImpl(DbObjectType.ScalarFunction);
            actionChooser.Actions.Add(new LocateInObjectExplorerActionImpl());
            actionChooser.Actions.Add(new ScriptAlterActionImpl());
            actionChooser.Actions.Add(new ScriptCreateActionImpl());
            actionChooser.Actions.Add(new ScriptDropActionImpl());

            _actionChoosers.Add(actionChooser);
        }

        private void RegisterActionChoosersForTableValuedFunction()
        {
            var actionChooser = new ActionChooserImpl(DbObjectType.TableValuedFunction);
            actionChooser.Actions.Add(new LocateInObjectExplorerActionImpl());
            actionChooser.Actions.Add(new ScriptAlterActionImpl());
            actionChooser.Actions.Add(new ScriptCreateActionImpl());
            actionChooser.Actions.Add(new ScriptDropActionImpl());

            _actionChoosers.Add(actionChooser);
        }

        private void RegisterActionChoosersForGeneric()
        {
            var actionChooser = new ActionChooserImpl(DbObjectType.Generic);
            actionChooser.Actions.Add(new LocateInObjectExplorerActionImpl());
            actionChooser.Actions.Add(new ScriptAlterActionImpl());
            actionChooser.Actions.Add(new ScriptCreateActionImpl());
            actionChooser.Actions.Add(new ScriptDropActionImpl());

            _actionChoosers.Add(actionChooser);
        }

        #endregion
    }
}
