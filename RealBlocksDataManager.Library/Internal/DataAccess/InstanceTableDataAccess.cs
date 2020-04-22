using System.Collections.Generic;
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

        #endregion
    }
}
