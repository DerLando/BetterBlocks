using System;
using System.Collections.Generic;
using System.Text;
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
                Id = definition.Id,
                Index = definition.Index,
                IsInUse = definition.InUse(2),
                Name = definition.HasName? definition.Name : "Unnamed",
                ObjectCount = definition.ObjectCount,
                ObjectIds = definition.GetObjectIds(),
            };
        }
    }
}
