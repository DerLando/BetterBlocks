using System;
using System.Collections.Generic;
using RealBlocksDataManager.Library.Extensions;
using Rhino;
using Rhino.DocObjects;
using Rhino.DocObjects.Tables;

namespace RealBlocksDataManager.Library.Internal.DataAccess
{
    internal class InstanceTableDataAccess
    {
        #region private fields

        /// <summary>
        /// The <see cref="RhinoDoc"/> of which the <see cref="InstanceDefinitionTable"/> gets accessed
        /// </summary>
        private readonly RhinoDoc _doc;

        #endregion

        #region Constructor

        public InstanceTableDataAccess()
        {
            _doc = RhinoDoc.ActiveDoc;
        }

        #endregion

        #region public methods

        public IEnumerable<InstanceDefinition> GetDocumentInstanceDefinitions()
        {
            return _doc.InstanceDefinitions;
        }

        public InstanceDefinition GetDefinition(Guid id)
        {
            return _doc.InstanceDefinitions.FindId(id);
        }

        public IEnumerable<InstanceDefinition> GetNestedDefinitions(Guid id)
        {
            return GetNestedDefinitions(GetDefinition(id));
        }

        public IEnumerable<InstanceDefinition> GetNestedDefinitions(InstanceDefinition definition)
        {
            return definition.GetPartDefinitions();
        }

        #endregion
    }
}
