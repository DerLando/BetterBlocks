using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RealBlocksDataManager.Library.Extensions;
using RealBlocksDataManager.Library.Factories;
using RealBlocksDataManager.Library.Internal.DataAccess;
using RealBlocksDataManager.Library.Models;

namespace RealBlocksDataManager.Library.DataAccess
{
    public static class InstanceDefinitionData
    {
        /// <summary>
        /// Returns all Instance definition models that can be generated
        /// from the currently active <see cref="Rhino.RhinoDoc"/>
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<InstanceDefinitionModel> GetInstanceDefinitions()
        {
            var dataAccess = new InstanceTableDataAccess();
            return dataAccess
                .GetDocumentInstanceDefinitions()
                .Select(InstanceDefinitionModelFactory.Create)
                ;
        }

        /// <summary>
        /// Returns all Instance definition models that are of type assembly
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<InstanceDefinitionModel> GetAssemblies()
        {
            var dataAccess = new InstanceTableDataAccess();
            return dataAccess
                    .GetDocumentInstanceDefinitions()
                    .Where(d => !d.IsRoot())
                    .Select(InstanceDefinitionModelFactory.Create)
                ;
        }

        /// <summary>
        /// Gets all the first level children of a Instance definition
        /// given its id
        /// </summary>
        /// <param name="id">The id of the Instance definition to find the children of</param>
        /// <returns></returns>
        public static IEnumerable<InstanceDefinitionModel> GetChildrenById(Guid id)
        {
            var dataAccess = new InstanceTableDataAccess();
            return dataAccess
                .GetNestedDefinitions(id)
                .Select(InstanceDefinitionModelFactory.Create);
        }
    }
}
