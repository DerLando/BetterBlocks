using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterBlocks.Core;
using BetterBlocks.UI.Views;

namespace BetterBlocks.UI.EtoCommands
{
    class UsedByBlockDefinitions : InstanceDefinitionCommandBase
    {
        public UsedByBlockDefinitions()
        {
            MenuText = "UsedBy";
            ToolTip = "Generates a table of 'used by' relations for selected Blocks";
        }

        protected override void OnExecuted(EventArgs e)
        {
            base.OnExecuted(e);
            List<BlockUsedBy> usedBy = new List<BlockUsedBy>();
            foreach (var definition in _definitions.Distinct())
            {
                usedBy.AddRange(Factory.CreateUsedBys(definition));
            }

            using (var ub = new BlockUsedByDialog(usedBy))
            {
                ub.ShowModal();
            }

        }
    }
}
