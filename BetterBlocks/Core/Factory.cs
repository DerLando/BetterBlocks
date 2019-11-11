using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.DocObjects;

namespace BetterBlocks.Core
{
    public static class Factory
    {
        public static BlockUsedBy[] CreateUsedBys(InstanceDefinition definition)
        {
            var containers = definition.GetContainers();
            // check null containers
            if (containers.Length == 0) return new[] {new BlockUsedBy {Name = definition.Name}};

            var usedBys = new BlockUsedBy[containers.Length];

            usedBys[0] = new BlockUsedBy{Name = definition.Name, UsedBy = containers[0].Name};

            if (containers.Length > 1)
            {
                for (int i = 1; i < containers.Length; i++)
                {
                    usedBys[i] = new BlockUsedBy{UsedBy = containers[i].Name};
                }
            }

            return usedBys;
        }
    }
}
