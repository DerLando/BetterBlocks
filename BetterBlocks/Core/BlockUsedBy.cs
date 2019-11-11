using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.DocObjects;

namespace BetterBlocks.Core
{
    public class BlockUsedBy
    {
        public string Name { get; set; }
        public string[] UsedBy { get; set; }

        public BlockUsedBy(InstanceDefinition definition)
        {
            Name = definition.Name;

            var containers = definition.GetContainers();
            UsedBy = (from container in containers select container.Name).ToArray();

        }
    }
}
