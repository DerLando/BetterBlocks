using System.Collections.Generic;
using System.Linq;
using BetterBlocks.Core;
using Eto.Forms;
using Rhino.DocObjects;

namespace BetterBlocks.UI.EtoCommands
{
    public class InstanceDefinitionCommandBase : Command
    {
        protected InstanceDefinition[] _definitions = null;
        protected NestedBlock[] _nested = null;

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

        public void SetNested(IEnumerable<NestedBlock> nestedBlocks)
        {
            if (nestedBlocks is null)
            {
                _nested = new NestedBlock[0];
            }
            else
            {
                _nested = nestedBlocks.ToArray();
            }
        }
    }
}