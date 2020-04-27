using RealBlocksDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBlocksDataManager.Library.Factories
{
    public static class NestedInstanceDefinitionModelFactory
    {
        public static NestedInstanceDefinitionModel Create(InstanceDefinitionModel model)
        {
            return new NestedInstanceDefinitionModel
            {
                Model = model,
                Root = null,
                Parent = null,
                PartIndex = -1
            };
        }

        public static NestedInstanceDefinitionModel Create(InstanceDefinitionModel model,
            InstanceDefinitionModel root, InstanceDefinitionModel parent, int partIndex)
        {
            return new NestedInstanceDefinitionModel
            {
                Model = model,
                Root = root,
                Parent = parent,
                PartIndex = partIndex
            };
        }
    }
}
