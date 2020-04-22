﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RealBlocksDataManager.Library.Factories;
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
    }
}