using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterBlocks.Core;
using Rhino.DocObjects;
using Rhino.DocObjects.Tables;

namespace BetterBlocks.Data
{
    /// <summary>
    /// Data class storing information about an <see cref="InstanceDefinition"/>
    /// </summary>
    public class InstanceDefinitionItem
    {
        #region Public properties

        /// <summary>
        /// Index of the <see cref="InstanceDefinition"/> in the <see cref="InstanceDefinitionTable"/>
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Given Name of the <see cref="InstanceDefinition"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of the <see cref="InstanceDefinition"/>, either Assembly or Root
        /// </summary>
        public InstanceDefinitionType Type { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default parameter-less constructor
        /// </summary>
        public InstanceDefinitionItem() { }

        /// <summary>
        /// Default constructor,
        /// If you know the <see cref="InstanceDefinitionType"/> beforehand consider using
        /// the faster object initializer
        /// </summary>
        /// <param name="definition"></param>
        public InstanceDefinitionItem(InstanceDefinition definition)
        {
            Id = definition.Id;
            Name = definition.Name;

            Type = definition.IsRoot() ? InstanceDefinitionType.Root : InstanceDefinitionType.Assembly;
        }

        #endregion
    }
}
