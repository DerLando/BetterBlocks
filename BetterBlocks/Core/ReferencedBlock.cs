using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.DocObjects;

namespace BetterBlocks.Core
{
    public class ReferencedBlock
    {
        public InstanceDefinition Definition { get; set; }
        public ChildBlockInsertionParameters BlockInsertionParameters { get; set; }

        public ReferencedBlock(InstanceDefinition definition, ChildBlockInsertionParameters blockInsertionParameters)
        {
            Definition = definition;
            BlockInsertionParameters = blockInsertionParameters;
        }

        public ReferencedBlock(NestedBlock nested)
        {
            Definition = nested.Definition;
            BlockInsertionParameters = nested.RelativeInsertion;
        }
    }
}
