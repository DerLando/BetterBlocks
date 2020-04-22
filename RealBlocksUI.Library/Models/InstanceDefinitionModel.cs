using System;
using Rhino.DocObjects;
using Rhino.DocObjects.Tables;

namespace RealBlocksUI.Library.Models
{
    public class InstanceDefinitionModel
    {
        #region Public properties

        /// <summary>
        /// Globally unique identifier of the <see cref="InstanceDefinition"/>
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Index of the <see cref="InstanceDefinition" /> in the <see cref="InstanceDefinitionTable"/>
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Given Name of the <see cref="InstanceDefinition"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Number of <see cref="RhinoObject"/>s that are part of this <see cref="InstanceDefinition"/>
        /// </summary>
        public int ObjectCount { get; set; }

        /// <summary>
        /// GUIDs of all objects that are part of this definition,
        /// should be of same length as ObjectCount
        /// </summary>
        public Guid[] ObjectIds { get; set; }

        /// <summary>
        /// Determines if the definition is inserted or referenced
        /// in the Document it comes from
        /// </summary>
        public bool IsInUse { get; set; }

        /// <summary>
        /// Determines if the definition is assembled from other definitions
        /// or a 'pure' definition made up only of geometry
        /// </summary>
        public bool IsAssembly { get; set; }

        // TODO: A InstanceDefinition does not store user strings,
        // TODO: If we want to keep track of those we have to query the Inserts
        // TODO: f.e.: InstanceDefinition.GetReferences(2).ForEach(o => o.GetUserStrings())

        #endregion

    }
}
