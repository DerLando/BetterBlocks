using Rhino;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealBlocksDataManager.Library.Internal.DataAccess.Base
{
    internal abstract class TableDataAccessBase
    {
        #region private fields

        /// <summary>
        /// The <see cref="RhinoDoc"/> of which the <see cref="InstanceDefinitionTable"/> gets accessed
        /// </summary>
        internal readonly RhinoDoc _doc;

        #endregion

        #region Constructor

        public TableDataAccessBase()
        {
            _doc = RhinoDoc.ActiveDoc;
        }

        #endregion
    }
}
