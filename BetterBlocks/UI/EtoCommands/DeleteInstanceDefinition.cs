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

            var modified = false;
            foreach (var definition in _definitions)
            {
                if (!Actions.DeleteInstanceDefinition(definition, doc))
                {
                    RhinoApp.WriteLine($"Could not delete {definition}");
                }
                else
                {
                    RhinoApp.WriteLine($"Deleted {definition}!");
                    modified = true;
                }
            }

            if (modified) doc.Modified = true;

        }
    }
}
