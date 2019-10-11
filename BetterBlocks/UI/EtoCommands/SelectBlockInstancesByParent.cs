using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Forms;
using Rhino.DocObjects;

namespace BetterBlocks.UI.EtoCommands
{
    public class SelectBlockInstancesByParent : Command
    {
        private InstanceDefinition _definition { get;  set; }

        public SelectBlockInstancesByParent(InstanceDefinition definition)
        {
            _definition = definition;

            MenuText = "Select";
            ToolTip = "Happy little test";
        }

        protected override void OnExecuted(EventArgs e)
        {
            base.OnExecuted(e);

            foreach (var instanceObject in _definition.GetReferences(0))
            {
                instanceObject.Select(true, true);
            }
        }
    }
}
