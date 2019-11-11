using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.DocObjects;

namespace BetterBlocks.Core
{
    public class BlockCount
    {
        public string Name { get; set; }
        public int TopLevel { get; set; }
        public int Nested { get; set; }
        public int Total { get; set; }

        public BlockCount(InstanceDefinition definition)
        {
            Name = definition.Name;
            //Nested = definition.GetReferences(2).Length;
            Nested = definition.GetNestedCount();
            TopLevel = definition.GetReferences(0).Length;
            Total = Nested + TopLevel;
        }
    }
}
