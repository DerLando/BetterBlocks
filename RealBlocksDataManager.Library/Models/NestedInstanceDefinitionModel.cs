using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBlocksDataManager.Library.Models
{
    /// <summary>
    /// A Model of a <see cref="InstanceDefinition"/> which also contains
    /// nesting information
    /// </summary>
    public class NestedInstanceDefinitionModel
    {
        /// <summary>
        /// Main Model this is referring to
        /// </summary>
        public InstanceDefinitionModel Model { get; set; }

        /// <summary>
        /// The root Model, can be null
        /// </summary>
        public InstanceDefinitionModel Root { get; set; }

        /// <summary>
        /// The direct parent, the model is nested in
        /// can be null
        /// </summary>
        public InstanceDefinitionModel Parent { get; set; }

        /// <summary>
        /// The Part Index inside of the Parents parts
        /// Will be -1 if no Parent / no root
        /// </summary>
        public int PartIndex { get; set; }
    }
}
