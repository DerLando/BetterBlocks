using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterBlocks.Core;
using Rhino;

namespace BetterBlocks.UI.EtoCommands
{
    public class DeleteInstanceDefinition : InstanceDefinitionCommandBase
    {
        public DeleteInstanceDefinition()
        {
            MenuText = "Delete";
            ToolTip = "Deletes selected Blocks and all references";
        }

        protected override void OnExecuted(EventArgs e)
        {
            base.OnExecuted(e);

            var doc = RhinoDoc.ActiveDoc;

            Actions.DeleteInstanceDefinitions(_definitions, doc);

            doc.Modified = true;

        }
    }
}
