using System;
using System.Collections.Generic;
using System.Linq;
using RealBlocksDataManager.Library.Extensions;
using RealBlocksDataManager.Library.Internal.DataAccess.Base;
using RealBlocksDataManager.Library.Internal.Models;
using Rhino;
using Rhino.DocObjects;
using Rhino.DocObjects.Tables;

namespace RealBlocksDataManager.Library.Internal.DataAccess
{
    internal class InstanceTableDataAccess : TableDataAccessBase
    {
        #region public methods

        /// <summary>
        /// Gets all Instance definitions for the currently active <see cref="RhinoDoc"/>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<InstanceDefinition> GetDocumentInstanceDefinitions()
        {
            return _doc.InstanceDefinitions;
        }

        /// <summary>
        /// Gets the instance definition with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public InstanceDefinition GetDefinition(Guid id)
        {
            return _doc.InstanceDefinitions.FindId(id);
        }

        /// <summary>
        /// Gets all top level nested Instance definitions for the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<InstanceDefinition> GetNestedDefinitions(Guid id)
        {
            return GetNestedDefinitions(GetDefinition(id));
        }

        /// <summary>
        /// Gets all top level nested Instance definitions for the given instance definition
        /// </summary>
        /// <param name="definition"></param>
        /// <returns></returns>
        public IEnumerable<InstanceDefinition> GetNestedDefinitions(InstanceDefinition definition)
        {
            return definition.GetPartDefinitions();
        }

        /// <summary>
        /// Gets the index of the nested definition inside the main definition
        /// </summary>
        /// <param name="mainId">Id of main or root definition</param>
        /// <param name="nestedId">Id of nested definition</param>
        /// <returns></returns>
        public int GetNestingIndex(Guid mainId, Guid nestedId)
        {
            return Array.IndexOf(
                GetDefinition(mainId).GetObjectIds(),
                nestedId
                );
        }

        /// <summary>
        /// Gets the given instance definition as inserted, relative to its root
        /// </summary>
        /// <param name="mainId">Id of main or root definition</param>
        /// <param name="nestedId">Id of nested definition</param>
        /// <returns></returns>
        public InsertedInstanceModel GetNestedRelativeInserted(Guid mainId, Guid nestedId)
        {
            var index = GetNestingIndex(mainId, nestedId);

            return GetDefinition(mainId) // Get definition for mainId
                .GetPartInstances() // Get its Parts as RhinoObjects
                .Where(pi => pi.InstanceDefinition.Id == nestedId) // Get the nested instance
                .Select(pi => new InsertedInstanceModel(
                    pi.InstanceDefinition,
                    new InstanceInsertionModel(
                        pi.InsertionPoint,
                        pi.InstanceXform)))
                .FirstOrDefault();
        }

        #endregion
    }
}
