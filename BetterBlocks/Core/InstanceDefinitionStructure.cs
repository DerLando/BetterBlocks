using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterBlocks.Data;
using Rhino;
using Rhino.DocObjects;
using Rhino.DocObjects.Tables;

namespace BetterBlocks.Core
{
    /// <summary>
    /// A helper class to query information about <see cref="InstanceDefinition"/>s
    /// </summary>
    public static class InstanceDefinitionStructure
    {
        /// <summary>
        /// Gets all assemblies defined in the <see cref="InstanceDefinitionTable"/>
        /// </summary>
        /// <param name="table">Table to query for assemblies</param>
        /// <returns></returns>
        public static List<InstanceDefinitionItem> GetAssemblies(InstanceDefinitionTable table)
        {
            return table
                .Where(def => !def.IsRoot())
                .Select(def => new InstanceDefinitionItem
                    {
                        Id = def.Id,
                        Name = def.Name,
                        Type = InstanceDefinitionType.Assembly
                    }
                )
                .ToList();
        }

        /// <summary>
        /// Gets the <see cref="InstanceDefinition"/> corresponding to a given <see cref="InstanceDefinitionItem"/>
        /// </summary>
        /// <param name="table"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private static InstanceDefinition GetRhinoDefinition(InstanceDefinitionTable table, InstanceDefinitionItem item)
        {
            return table.Find(item.Id, true);
        }

        /// <summary>
        /// Gets the top level content of a <see cref="InstanceDefinitionItem"/>
        /// </summary>
        /// <param name="table"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static List<InstanceDefinitionItem> GetInstanceDefinitionContents(InstanceDefinitionTable table, InstanceDefinitionItem item)
        {
            var definition = GetRhinoDefinition(table, item);

            return definition
                .GetPartDefinitions()
                .Select(def => new InstanceDefinitionItem(def))
                .ToList();
        }
    }
}
