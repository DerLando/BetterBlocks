using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            // if we don't have a definition yet, there is nothing to do
            if (_definitions is null) return;

            Commands.Hidden.bbHiddenSelectBlockInstancesByParent.Instance.SetDefinition(_definitions);
            RhinoApp.RunScript("bbHiddenSelectBlockInstancesByParent", false);
        }
    }
}
