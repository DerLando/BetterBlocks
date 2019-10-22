using Eto.Forms;
using Rhino.DocObjects;

namespace BetterBlocks.UI.EtoCommands
{
    public class InstanceDefinitionCommandBase : Command
    {
        protected InstanceDefinition _definition = null;

        public void SetDefinition(InstanceDefinition definition)
        {
            _definition = definition;
        }
    }
}