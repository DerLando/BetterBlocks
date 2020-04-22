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
    public class InstanceDefinitionStructure
    {
        #region private fields

        private readonly InstanceDefinitionTable _table;

        #endregion

        #region Constructor

        public InstanceDefinitionStructure(InstanceDefinitionTable table)
        {
            _table = table;
        }

        #endregion

        /// <summary>
        /// Gets all assemblies defined in the <see cref="InstanceDefinitionTable"/>
        /// </summary>
        /// <returns></returns>
        public List<InstanceDefinitionItem> GetAssemblies()
        {
            return _table
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
        /// <param name="item"></param>
        /// <returns></returns>
        private InstanceDefinition GetRhinoDefinition(InstanceDefinitionItem item)
        {
            return GetRhinoDefinition(item.Id);
        }

        private InstanceDefinition GetRhinoDefinition(Guid id)
        {
            return _table.Find(id, true);
        }

        /// <summary>
        /// Gets the top level content of a <see cref="InstanceDefinitionItem"/>
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public List<InstanceDefinitionItem> GetInstanceDefinitionContents(InstanceDefinitionItem item)
        {
            var definition = GetRhinoDefinition(item);

            return GetInstanceDefinitionContents(definition);
        }

        /// <summary>
        /// Gets the top level content of a <see cref="InstanceDefinitionItem"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<InstanceDefinitionItem> GetInstanceDefinitionContents(Guid id)
        {
            var definition = GetRhinoDefinition(id);

            return GetInstanceDefinitionContents(definition);
        }

        private List<InstanceDefinitionItem> GetInstanceDefinitionContents(InstanceDefinition definition)
        {
            return definition
                .GetPartDefinitions()
                .Select(def => new InstanceDefinitionItem(def))
                .ToList();
        }
    }
}
