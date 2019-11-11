using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Rhino.DocObjects;

namespace BetterBlocks.Core
{
    public static class Actions
    {
        public static bool RenameInstanceDefinition(InstanceDefinition definition, RhinoDoc doc, string newName)
        {
            return doc.InstanceDefinitions.Modify(definition, newName, definition.Description, false);
        }
    }
}
