using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterBlocks.Core;
using BetterBlocks.UI.Views;
using Eto.Drawing;
using Eto.Forms;
using Rhino;
using Rhino.Input.Custom;

namespace BetterBlocks.UI.EtoCommands
{
    public class RenameBlockDefinitionCommand : InstanceDefinitionCommandBase
    {
        public RenameBlockDefinitionCommand()
        {
            MenuText = "Rename";
            ToolTip = "Renames the selected Block definition";
        }

        protected override void OnExecuted(EventArgs e)
        {
            base.OnExecuted(e);

            var renameDialog = new GetStringDialog("Rename Block");

            if (renameDialog.ShowModal() == DialogResult.Ok)
            {
                var doc = RhinoDoc.ActiveDoc;

                Actions.RenameInstanceDefinitions(_definitions, doc, renameDialog.StringResult);
                doc.Modified = true;
            }
        }
    }
}
