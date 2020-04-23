using System;
using System.Collections.Generic;
using System.Text;
using RealBlocksDataManager.Library.Extensions;
using RealBlocksDataManager.Library.Models;
using Rhino.DocObjects;

namespace RealBlocksDataManager.Library.Factories
{
    public static class InstanceDefinitionModelFactory
    {
        public static InstanceDefinitionModel Create(InstanceDefinition definition)
        {
            return new InstanceDefinitionModel
            {
                Description = definition.Description,
                Id = definition.Id,
                Index = definition.Index,
                IsInUse = definition.InUse(2),
                IsAssembly = !definition.IsRoot(),
                Name = definition.HasName? definition.Name : "Unnamed",
                ObjectCount = definition.ObjectCount,
                ObjectIds = definition.GetObjectIds(),
            };
        }
    }
}
