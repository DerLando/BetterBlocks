using System.Collections.Generic;
using System.Linq;
using Eto.Forms;
using Rhino.DocObjects;

namespace BetterBlocks.UI.EtoCommands
{
    public class InstanceDefinitionCommandBase : Command
    {
        protected InstanceDefinition[] _definitions = null;

        public void SetDefinition(IEnumerable<InstanceDefinition> definitions)
        {
            if (definitions is null)
            {
                _definitions = new InstanceDefinition[0];
            }
            else
            {
                _definitions = definitions.ToArray();
            }
        }
    }
}