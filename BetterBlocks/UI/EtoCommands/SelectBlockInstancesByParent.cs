using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Forms;
using Rhino;
using Rhino.DocObjects;

namespace BetterBlocks.UI.EtoCommands
{
    public class SelectBlockInstancesByParent : InstanceDefinitionCommandBase
    {
        public SelectBlockInstancesByParent()
        {
            MenuText = "Select";
            ToolTip = "Select all top-level occurrences of this block definition in the document";
        }

        protected override void OnExecuted(EventArgs e)
        {
            base.OnExecuted(e);

            if (_definition is null) return;

            foreach (var instanceObject in _definition.GetReferences(0))
            {
                instanceObject.Select(true, true);
            }

            RhinoDoc.ActiveDoc.Views.Redraw();
        }
    }
}
