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
            // TODO: This does not work. We need to supply a nesting depth and also an index
            // to know where the part is located inside of its parent
            // probably best to pass those when GetChildren is called
            // we could construct a tree-like data structure where a definition node
            // knows its immediate parent, its root and its partIndex inside of the immediate parent
            // this way we can always know where each definition is referenced from
            // For this we will need to traverse the whole instance definition tree on startup
            // and after every change, which is super costly :/
            // On the brighter side: this will allow us to handle user strings on nested definitions :)
            var def = GetDefinition(mainId);
            var nested = GetDefinition(nestedId);
            var debug = def.GetObjectIds();
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
