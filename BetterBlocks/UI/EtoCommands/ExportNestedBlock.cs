using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterBlocks.Core;
using BetterBlocks.UI.Views;
using Eto.Forms;
using Rhino;
using Rhino.DocObjects;

namespace BetterBlocks.UI.EtoCommands
{
    public class ExportNestedBlock : InstanceDefinitionCommandBase
    {
        public ExportNestedBlock()
        {
            MenuText = "Export";
            ToolTip = "Export all selected Blocks to individual Files";
        }

        protected override void OnExecuted(EventArgs e)
        {
            base.OnExecuted(e);

            var rootTree = _nested[0].GetRootTreeByRootDepth();

            var fd =  new SaveFileDialog
            {
                Title = "Export Block",
            };

            fd.Filters.Add(new FileFilter("Rhino file", "*.3dm"));

            if (fd.ShowDialog(Rhino.UI.RhinoEtoApp.MainWindow) == DialogResult.Ok)
            {
                Actions.ExportNestedBlock(_nested[0], fd.FileName);
            }

        }
    }
}
