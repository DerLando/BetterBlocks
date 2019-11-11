using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterBlocks.Core;
using BetterBlocks.UI.Views;
using Rhino;

namespace BetterBlocks.UI.EtoCommands
{
    public class CountBlockInstances : InstanceDefinitionCommandBase
    {
        public CountBlockInstances()
        {
            MenuText = "Count";
            ToolTip = "Generates a table of counts for selected Blocks";
        }

        protected override void OnExecuted(EventArgs e)
        {
            base.OnExecuted(e);

            Actions.CountInstanceDefinitions(_definitions, out var counts);

            using (var bd = new BlockCountDialog(counts))
            {
                bd.ShowModal();
            }

        }
    }
}
